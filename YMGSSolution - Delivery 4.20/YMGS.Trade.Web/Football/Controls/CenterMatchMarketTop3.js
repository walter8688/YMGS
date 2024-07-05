///<reference name="MicrosoftAjax.js"/>
Type.registerNamespace("YMGS.Trade.Web.Football.Controls")

YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop3 = function (element) {
    YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop3.initializeBase(this, [element]);

    this._div = null;
    this._events = new Sys.EventHandlerList();

    //加载比赛的某种市场的详细数据
    this._loadMatchMarketDetailsHandler = Function.createDelegate(this, this._processLoadMatchMarketDetails);

    this._loadErrorHandler = Function.createDelegate(this, this._processLoadError);
};

YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop3.prototype = {
    _dataSource: null,
    _clientId: null,
    _language: 0,
    _buttonLst: null,
    _matchID: 0,
    _marketNameTitle: null,
    _isDisplayShowAll: false,
    _param: null,
    _showCash: null,
    _showGoIn: null,
    _showRule: null,
    _showRefresh: null,

    _get_events: function () {
        return this._events;
    },

    get_divMatchMarketTop3: function () {
        return this._div;
    },

    set_divMatchMarketTop3: function (value) {
        this._div = value;
    },

    add_betEvent: function (handler) {
        var e = Function._validateParams(arguments,
                    [{ name: "handler", type: Function}]);
        if (e) throw e;
        this._get_events().addHandler("BetEvent", handler);
    },
    remove_betEvent: function (handler) {
        var e = Function._validateParams(arguments,
                    [{ name: "handler", type: Function}]);
        if (e) throw e;
        this._get_events().removeHandler("BetEvent", handler);
    },
    OnBetEvent: function (eventArgs) {
        var e = Function._validateParams(arguments,
                    [{ name: "eventArgs", type: Sys.EventArgs}]);
        if (e) throw e;
        var handler = this._get_events().getHandler("BetEvent");
        if (handler) {
            handler(this, eventArgs);
        }
    },

    initialize: function () {
        YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop3.callBaseMethod(this, 'initialize');

        this.add_betEvent(BetEventHandler);
        if (this._dataSource != null) {
            if (this._dataSource.MarketAllInfo.length > 0 || this._dataSource.MarketSummaryInfo.length > 0) {
                this._buildMatchMarketDetails();
            }
        }

        var that = this;
        setInterval(function () {
            if (typeof (that) === 'undefined')
                return false;
            YMGS.Trade.Web.Services.MatchMarketService.GetTop3MatchMarketDetalsList(that._language, that._matchID, that._param, that._dataSource.isShowAll, that._loadMatchMarketDetailsHandler, that._loadErrorHandler, null);
        }, 60000);

    },

    _buildMatchMarketDetails: function () {
        this._ClearUserControlData();
        this._div.appendChild(this._AddMatchMarketDetails());
    },

    //构建单个的Top3
    _AddMatchMarketDetails: function () {
        var div = document.createElement("div");
        var divContainer = document.createElement("div");
        $(div).prop("class", "mod-marketview");
        $(divContainer).prop("class", "mkt-runners-container");
        //添加header--div market-header
        divContainer.appendChild(this._AddMatchHeaderInfo());
        //添加body--div market-container
        divContainer.appendChild(this._AddMatchMarketContainer());
        div.appendChild(divContainer);
        return div;
    },

    _AddMatchHeaderInfo: function () {
        var matchHeaderInfo = this._dataSource.MatchInfo;
        //返回一个div的header
        var div = document.createElement("div");
        $(div).prop("class", "header-runners");
        //往header里加元素以及数据
        //-----按钮的div
        var divBtnContainer = document.createElement("div");
        $(divBtnContainer).prop("class", "buttonsContainer");
        //是否显示刷新
        if (this._showRefresh) {
            divBtnContainer.appendChild(this._DeclareButtonRefresh());
        }
        //是否显示Rules
        if (this._showRule) {
            divBtnContainer.appendChild(this._DeclareButtonRules());
        }
        //--span的div
        var divInfoContainer = document.createElement("div");
        $(divInfoContainer).prop("class", "infoContainer");
        //是否显示Cash Out
        if (this._showGoIn) {
            divInfoContainer.appendChild(this._DeclareButtonG(matchHeaderInfo.isZouDi));
        }
        //是否显示折叠
        if (this._showCash) {
            divInfoContainer.appendChild(this._DeclareButtonM());
        }

        div.appendChild(divBtnContainer);
        div.appendChild(divInfoContainer);
        div.appendChild(this._constructMatchInfo(matchHeaderInfo));
        //返回div
        return div;
    },

    _constructMatchInfo: function (data) {
        var divMatchContainer = document.createElement("div");
        $(divMatchContainer).prop("class", "total-matched-container");
        //比赛名称--市场名称
        var matchName = data.MatchName + " - " + this._marketNameTitle + " " + data.MStartDate;
        var spanMktName = document.createElement("span");
        $(spanMktName).prop("class", "mkt-name").text(matchName);
        divMatchContainer.appendChild(spanMktName);
        //title
        var matchDealT = document.createElement("span");
        //$(matchDealT).prop("class", "total-matched-lbl").text(this._buttonLst.MatchedTitleName);
        $(matchDealT).prop("class", "mkt-name").text(this._buttonLst.MatchedTitleName);
        divMatchContainer.appendChild(matchDealT);
        //成交量
        var TotalDealAmount = 0;
        for (var i = 0, arr; arr = this._dataSource.MarketAllInfo[i++]; ) {
            TotalDealAmount = TotalDealAmount + arr.DealAmount;
        }
        var matchDealAm = document.createElement("span");
        $(matchDealAm).prop("class", "mkt-name").text("￥" + TotalDealAmount);
        divMatchContainer.appendChild(matchDealAm);
        return divMatchContainer;
    },

    //收缩按钮
    _DeclareButtonM: function () {
        var M = document.createElement("span");
        $(M).prop("class", "cashout-icon").prop("title", this._buttonLst.CashOutTitleName);
        return M;
    },

    //走地按钮(是否走地,true：是)
    _DeclareButtonG: function (isZouDi) {
        var spanGoin = document.createElement("span");
        if (isZouDi) {
            $(spanGoin).prop("class", "mkt-status-icon-enabled").text(this._buttonLst.GoInPlayTitleName);
        }
        else {
            $(spanGoin).prop("class", "mkt-status-icon-disabled").text(this._buttonLst.GoInPlayTitleName);
        }
        return spanGoin;
    },

    //Rules按钮
    _DeclareButtonRules: function () {
        var Rules = document.createElement("input");
        $(Rules).prop("type", "button").prop("class", "bf-icon-rules").prop('value', this._buttonLst.RulesTitleName).prop("title", this._buttonLst.RulesTitleName)
        var that = this;
        $(Rules).click(function () {
            //alert(that._dataSource.MarketInfo[0].RulesLinkAdd);
            window.showModalDialog(that._dataSource.MarketAllInfo[0].RulesLinkAdd, window, 'dialogWidth:700px;dialogHeight:500px;scroll:yes');
            return false;
        });
        return Rules;
    },

    //刷新按钮
    _DeclareButtonRefresh: function () {
        //刷新
        var Refresh = document.createElement("input"),
        that = this;
        $(Refresh).prop("type", "button").prop("class", "mkt-refresh-btn").prop("title", this._buttonLst.RefreshTitleName);
        $(Refresh).click(function () {
            //alert("refresh");
            YMGS.Trade.Web.Services.MatchMarketService.GetTop3MatchMarketDetalsList(that._language, that._matchID, that._param, that._dataSource.isShowAll, that._loadMatchMarketDetailsHandler, that._loadErrorHandler, null);
            return false;
        });
        return Refresh;
    },

    //比赛市场信息
    _AddMatchMarketContainer: function () {
        //返回一个div的比赛的市场信息
        var div = document.createElement("div");
        div.setAttribute("class", "cont-runners");
        var Table = document.createElement("table");
        Table.setAttribute("class", "");
        Table.setAttribute("border", "0");
        Table.appendChild(this._AddMatchMarketContainerHeader());
        Table.appendChild(this._AddMatchMarketContainerBody());

        div.appendChild(Table);
        return div;
    },

    //比赛市场的具体信息的Header
    _AddMatchMarketContainerHeader: function () {
        var thead = document.createElement("thead");
        var theadTR = document.createElement("tr");
        $(theadTR).prop("class", "back-lay-rules-heading");

        if (this._isDisplayShowAll) {
            theadTR.appendChild(this._AddShowSumaryAndShowAll());
        }
        else {
            var thFirst = document.createElement('th');
            $(thFirst).prop('class', 'matched-values');
            theadTR.appendChild(thFirst);
        }
        //投注
        var thBack3 = document.createElement("th");
        $(thBack3).prop("class", "back");
        var thBack2 = document.createElement("th");
        $(thBack2).prop("class", "back");
        //--投注标识
        var thBack1 = document.createElement("th");
        var spanBackLabel = document.createElement('span');
        var spanBackPic = document.createElement('span');
        $(thBack1).prop("class", "back");
        $(spanBackLabel).prop("class", "back-label").text(this._buttonLst.BackTitleName);
        $(spanBackPic).prop("class", "back-all");
        thBack1.appendChild(spanBackLabel);
        thBack1.appendChild(spanBackPic);

        theadTR.appendChild(thBack3);
        theadTR.appendChild(thBack2);
        theadTR.appendChild(thBack1);

        //受注
        var thLay1 = document.createElement("th");
        var spanLayLabel = document.createElement('span');
        var spanLayPic = document.createElement('span');
        $(thLay1).prop("class", "lay");
        $(spanLayLabel).prop("class", "lay-label").text(this._buttonLst.LayTitleName);
        $(spanLayPic).prop("class", "lay-all");
        thLay1.appendChild(spanLayLabel);
        thLay1.appendChild(spanLayPic);

        var thLay2 = document.createElement("th");
        $(thLay2).prop("class", "lay");
        var thLay3 = document.createElement("th");
        $(thLay3).prop("class", "lay");

        theadTR.appendChild(thLay1);
        theadTR.appendChild(thLay2);
        theadTR.appendChild(thLay3);

        thead.appendChild(theadTR);
        return thead;
    },

    _AddShowSumaryAndShowAll: function () {
        var theadTH2 = document.createElement("th");
        $(theadTH2).prop('class', 'matched-values');
        var spanLines = document.createElement("span");
        $(spanLines).text(this._buttonLst.LinesTitleName + " ");
        theadTH2.appendChild(spanLines);
        var that = this;

        if (this._dataSource.isShowAll) {
            //显示All，Summary能点击，All不能点击，All的颜色是蓝色
            //$(aShowAll).
            var aSummary = document.createElement("a");
            $(aSummary).prop('href', 'javascript:void(0);').text(this._buttonLst.SummaryTitleName);
            $(aSummary).click(function () {
                //alert("summary");
                YMGS.Trade.Web.Services.MatchMarketService.GetTop3MatchMarketDetalsList(that._language, that._matchID, that._param, false, that._loadMatchMarketDetailsHandler, that._loadErrorHandler, null);
                return false;
            });
            theadTH2.appendChild(aSummary);
            var spanFilter = document.createElement("span");
            $(spanFilter).prop("class", "matched-filter").text("|");
            theadTH2.appendChild(spanFilter);
            var spanA = document.createElement("span");
            $(spanA).text(this._buttonLst.ShowAllTitleName);
            theadTH2.appendChild(spanA);
        }
        else {
            //显示Summary，All能点击，Summary不能点击，Summary的颜色是蓝色
            var spanSummary = document.createElement("span");
            $(spanSummary).text(this._buttonLst.SummaryTitleName);
            theadTH2.appendChild(spanSummary);
            var spanFilter = document.createElement("span");
            $(spanFilter).prop("class", "matched-filter").text("|");
            theadTH2.appendChild(spanFilter);
            var aShowAll = document.createElement("a");
            $(aShowAll).prop('href', 'javascript:void(0);').text(this._buttonLst.ShowAllTitleName);
            $(aShowAll).click(function () {
                //alert("all");
                YMGS.Trade.Web.Services.MatchMarketService.GetTop3MatchMarketDetalsList(that._language, that._matchID, that._param, true, that._loadMatchMarketDetailsHandler, that._loadErrorHandler, null);
                return false;
            });
            theadTH2.appendChild(aShowAll);
        }
        return theadTH2;
    },

    _AddMatchMarketContainerBody: function () {
        var tbody = document.createElement('tbody');
        if (this._dataSource.isShowAll) {
            var allData = this._dataSource.MarketAllInfo;
            //alert("allData");
            for (var i = 0, arr; arr = allData[i++]; ) {
                tbody.appendChild(this._AddMarketDetailsTR(arr));
            }
        }
        else {
            var summaryData = this._dataSource.MarketSummaryInfo;
            //alert("summaryData");
            for (var i = 0, arr; arr = summaryData[i++]; ) {
                tbody.appendChild(this._AddMarketDetailsTR(arr));
            }
        }

        return tbody;
    },

    _AddMarketDetailsTR: function (arr) {
        var tr = document.createElement('tr');
        $(tr).prop('class', 'runner-row');
        //市场名称
        tr.appendChild(this._AddMarketDetailsBodyFirstTD(arr));
        //投注
        tr.appendChild(this._AddMarketDetailsBodyBackTD(arr, arr.LayOT3, arr.LayAT3, arr.MStatusClass, false));
        tr.appendChild(this._AddMarketDetailsBodyBackTD(arr, arr.LayOT2, arr.LayAT2, arr.MStatusClass, false));
        tr.appendChild(this._AddMarketDetailsBodyBackTD(arr, arr.LayOT1, arr.LayAT1, arr.MStatusClass, true));
        //受注
        tr.appendChild(this._AddMarketDetailsBodyLayTD(arr, arr.BackOT1, arr.BackAT1, arr.MStatusClass, true));
        tr.appendChild(this._AddMarketDetailsBodyLayTD(arr, arr.BackOT2, arr.BackAT2, arr.MStatusClass, false));
        tr.appendChild(this._AddMarketDetailsBodyLayTD(arr, arr.BackOT3, arr.BackAT3, arr.MStatusClass, false));
        //遮罩层
        tr.appendChild(this._constructMarketTableBodyStatusTd(arr));
        return tr;
    },

    _AddMarketDetailsBodyFirstTD: function (data) {
        var td = document.createElement("td");
        var spanMarketName = document.createElement("span");
        //var a = document.createElement("a");
        //$(a).prop("class", "mkt-activity").prop('href', 'javascript:void(0);');
        $(spanMarketName).prop("class", "sel-name").text(data.MKName);
        $(td).prop("class", "runner-name");
        //td.appendChild(a);
        td.appendChild(spanMarketName);
        return td;
    },

    _AddMarketDetailsBodyBackTD: function (arr, backOdds, backMatchAmouts, status, isTop1) {
        var td = document.createElement("td");
        var backBtn = document.createElement("button");
        backBtn.setAttribute("type", "button");
        var spanOdds = document.createElement("span");
        var spanAmounts = document.createElement("span");
        var backOddsValue = backOdds == 0 ? '' : backOdds;
        var backMarketAmounts = backMatchAmouts == "" ? "" : "￥" + backMatchAmouts;
        var that = this;
        $(td).prop("class", "odds");
        if (isTop1) {
            $(backBtn).prop("class", "bet-button cta-back");
        }
        else {
            $(backBtn).prop("class", "bet-button");
        }
        $(spanOdds).prop("class", "price").text(backOddsValue);
        $(spanAmounts).prop("class", "size").text(backMarketAmounts);
        backBtn.appendChild(spanOdds);
        backBtn.appendChild(spanAmounts);
        if (status !== "") {
            backBtn.setAttribute('disabled', 'true');
        }
        else {
            $(backBtn).click(function () {
                //SetBetBackCookie(that._dataSource.MatchInfo.MatchId, that._dataSource.MatchInfo.MatchType, arr.MarketId, arr.MarketTmpId, backOdds, backMatchAmouts, that._dataSource.MatchInfo.MatchName_CN, that._dataSource.MatchInfo.MatchName_EN, arr.MKName_CN, arr.MKName_EN, arr.MKTmpName_CN, arr.MKTmpName_EN);
                SetBetBackCookie(that._dataSource.MatchInfo.MatchId, that._dataSource.MatchInfo.MatchType, arr.MarketId, arr.MarketTmpId, backOdds, 50, that._dataSource.MatchInfo.MatchName_CN, that._dataSource.MatchInfo.MatchName_EN, arr.MKName_CN, arr.MKName_EN, arr.MKTmpName_CN, arr.MKTmpName_EN);
                var args = new YMGS.Trade.Web.Football.Controls.BetEventArgs(that._clientId);
                that.OnBetEvent(args);
            });
        }
        td.appendChild(backBtn);
        return td;
    },

    _AddMarketDetailsBodyLayTD: function (arr, layOdds, layMatchAmouts, status, isTop1) {
        var td = document.createElement("td");
        var layBtn = document.createElement("button");
        layBtn.setAttribute("type", "button");
        var spanOdds = document.createElement("span");
        var spanAmounts = document.createElement("span");
        var layOddsValue = layOdds == 0 ? '' : layOdds;
        var layMarketAmounts = layMatchAmouts == "" ? "" : "￥" + layMatchAmouts;
        var that = this;
        $(td).prop('class', 'odds');
        if (isTop1) {
            $(layBtn).prop("class", "bet-button cta-lay");
        }
        else {
            $(layBtn).prop("class", "bet-button");
        }
        $(spanOdds).prop("class", "price").text(layOddsValue);
        $(spanAmounts).prop("class", "size").text(layMarketAmounts);
        layBtn.appendChild(spanOdds);
        layBtn.appendChild(spanAmounts);
        if (status !== "") {
            layBtn.setAttribute('disabled', 'true');
        }
        else {
            $(layBtn).click(function () {
                //SetBetLayCookie(that._dataSource.MatchInfo.MatchId, that._dataSource.MatchInfo.MatchType, arr.MarketId, arr.MarketTmpId, layOdds, layMatchAmouts, that._dataSource.MatchInfo.MatchName_CN, that._dataSource.MatchInfo.MatchName_EN, arr.MKName_CN, arr.MKName_EN, arr.MKTmpName_CN, arr.MKTmpName_EN);
                SetBetLayCookie(that._dataSource.MatchInfo.MatchId, that._dataSource.MatchInfo.MatchType, arr.MarketId, arr.MarketTmpId, layOdds, 50, that._dataSource.MatchInfo.MatchName_CN, that._dataSource.MatchInfo.MatchName_EN, arr.MKName_CN, arr.MKName_EN, arr.MKTmpName_CN, arr.MKTmpName_EN);
                var args = new YMGS.Trade.Web.Football.Controls.BetEventArgs(that._clientId);
                that.OnBetEvent(args);
            });
        }
        td.appendChild(layBtn);
        return td;
    },

    _constructMarketTableBodyStatusTd: function (arr) {
        var td = document.createElement('td'),
        div = document.createElement('div'),
        span = document.createElement('span');
        $(div).prop('class', arr.MStatusClass);
        div.setAttribute('style', 'width: 294px; margin-left: -294px; height:2.5em;')
        $(span).text(arr.DivCharacter);
        div.appendChild(span);
        td.appendChild(div);
        return td;
    },

    _ClearUserControlData: function () {
        $('[id$=divMatchMarketTop3]').html("");
    },

    _processLoadMatchMarketDetails: function (serviceOperationResult) {
        if (serviceOperationResult.IsSucceeded) {
            this._ClearUserControlData();
            this._dataSource = serviceOperationResult.Value.matchSource;
            this._marketNameTitle = serviceOperationResult.Value.titleName;
            if (this._dataSource != null) {
                if (this._dataSource.MarketAllInfo.length > 0 || this._dataSource.MarketSummaryInfo.length > 0) {
                    this._buildMatchMarketDetails();
                }
            }
        }
        else {
            //alert(serviceOperationResult.Message);
        }
    },

    _processLoadError: function (error) {
        var errorMessage = error.get_message();
        //alert(errorMessage);
    },

    //公开的方法
    refresh: function () {
        YMGS.Trade.Web.Services.MatchMarketService.GetTop3MatchMarketDetalsList(this._language, this._matchID, this._param, this._dataSource.isShowAll, this._loadMatchMarketDetailsHandler, this._loadErrorHandler, null);
    },

    dispose: function () {
        YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop3.callBaseMethod(this, 'dispose');
        this.remove_betEvent(BetEventHandler);
    }
};


YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop3.registerClass("YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop3", Sys.UI.Control);

GenerateProps(YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop3, [
    'dataSource',
    'clientId',
    'language',
    'buttonLst',
    'matchID',
    'marketNameTitle',
    'isDisplayShowAll',
    'param',
    'showCash',
    'showGoIn',
    'showRule',
    'showRefresh'
]);

if (typeof (Sys) !== 'undefined') {
    Sys.Application.notifyScriptLoaded();
}