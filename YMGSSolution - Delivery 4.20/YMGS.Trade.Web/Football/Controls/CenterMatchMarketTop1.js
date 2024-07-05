///<reference name="MicrosoftAjax.js"/>
Type.registerNamespace("YMGS.Trade.Web.Football.Controls")

YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop1 = function (element) {
    YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop1.initializeBase(this, [element]);

    this._div = null;
    this._events = new Sys.EventHandlerList();

    //加载比赛的某种市场的详细数据
    this._loadMatchMarketDetailsHandler = Function.createDelegate(this, this._processLoadMatchMarketDetails);

    this._loadErrorHandler = Function.createDelegate(this, this._processLoadError);
};

YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop1.prototype = {
    _dataSource: null,
    _clientId: null,
    _language: 0,
    _buttonLst: null,
    _matchID: 0,
    _betBO: Object,
    _showCash: null,
    _showGoIn: null,
    _showRule: null,
    _showRefresh: null,
    _autoRefresh: null,

    _get_events: function () {
        return this._events;
    },

    get_autoRefresh: function () {
        return this._autoRefresh;
    },

    set_autoRefresh: function (value) {
        this._autoRefresh = value;
    },

    get_divMatchMarketTop1: function () {
        return this._div;
    },

    set_divMatchMarketTop1: function (value) {
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
        YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop1.callBaseMethod(this, 'initialize');
        this.add_betEvent(BetEventHandler);
        if (this._dataSource != null) {
            if (this._dataSource.MarketInfo.length > 0) {
                this._buildMatchMarketDetails();
            }
            else {
                this._autoRefresh = false;
            }
        }
        else {
            this._autoRefresh = false;
        }
        //alert("bo" + this._betBO.isOpen);

        var that = this;
        setInterval(function () {
            if (typeof (that) === 'undefined')
                return false;
            if (!that._autoRefresh)
                return false;
            YMGS.Trade.Web.Services.MatchMarketService.GetTop1MatchMarketDetailsList(that._language, that._matchID, that._betBO, that._loadMatchMarketDetailsHandler, that._loadErrorHandler, null);
        }, 60000);
    },

    _buildMatchMarketDetails: function () {
        this._ClearUserControlData();
        this._div.appendChild(this._AddMatchMarketDetails());
    },

    //构建单个的Top1
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
        var h2 = document.createElement("h2");
        $(h2).prop("class", "header-runnershtwo");
        //往header里加元素以及数据
        var a = document.createElement("a");
        var span = document.createElement("span");
        var em = document.createElement("em");
        var spanName = document.createElement("span");
        $(a).prop("class", "headerban");
        if (this._autoRefresh) {
            $(span).prop("class", "arrow-open");
        }
        else {
            $(span).prop("class", "arrow-close");
        }
        $(em).prop("class", "market-title");
        //$(spanName).text(this._controlTitle + " " + matchHeaderInfo.MStartDate);
        //alert(this._betBO.BetTypeName_EN);
        $(spanName).text(this._language == 1 ? this._betBO.BetTypeName_CN : this._betBO.BetTypeName_EN + " " + matchHeaderInfo.MStartDate);
        a.appendChild(span);
        a.appendChild(spanName)
        a.appendChild(em);
        var divIconContainer = document.createElement("div");

        //是否显示刷新
        if (this._showRefresh) {
            divIconContainer.appendChild(this._DeclareButtonRefresh());
        }
        //是否显示Rules
        if (this._showRule) {
            divIconContainer.appendChild(this._DeclareButtonRules());
        }
        //是否显示走地
        if (this._showGoIn) {
            divIconContainer.appendChild(this._DeclareButtonG(matchHeaderInfo.isZouDi));
        }
        //是否显示Cash Out
        if (this._showCash) {
            divIconContainer.appendChild(this._DeclareButtonM());
        }
        //比赛名称
        h2.appendChild(a);
        h2.appendChild(divIconContainer)
        if (this._autoRefresh) {
            $(divIconContainer).prop("class", "infoContainer");
        }
        else {
            $(divIconContainer).prop("class", "marker-selected");
        }
        div.appendChild(h2);
        //折叠
        accordionTop1($(div), this);
        //返回h2
        return div;
    },

    //收缩折叠按钮
    _DeclareButtonM: function () {
        var M = document.createElement("span");
        $(M).prop("class", "bf-icon-cashout1").prop("title", this._buttonLst.CashOutTitleName);
        return M;
    },

    //走地按钮(是否走地,true：是)
    _DeclareButtonG: function (isZouDi) {
        var spanGoin = document.createElement("span");
        if (isZouDi) {
            $(spanGoin).prop("class", "going-inplay-icon-enabled").prop("title", this._buttonLst.GoInPlayTitleName);
        }
        else {
            $(spanGoin).prop("class", "going-inplay-icon-disabled").prop("title", this._buttonLst.GoInPlayTitleName);
        }
        return spanGoin;
    },

    //Rules按钮
    _DeclareButtonRules: function () {
        var Rules = document.createElement("a");
        $(Rules).prop("class", "bf-icon-rulestw").prop("title", this._buttonLst.RulesTitleName);
        var that = this;
        $(Rules).click(function () {
            //alert(that._dataSource.MarketInfo[0].RulesLinkAdd);
            //window.showModalDialog();
            window.showModalDialog(that._dataSource.MarketInfo[0].RulesLinkAdd, window, 'dialogWidth:700px;dialogHeight:500px;scroll:yes');
            //YMGS.Trade.Web.Services.MatchMarketService.GetTop1MatchMarketDetalsList(that._language, that._matchID, that._marketTmpID, that._loadMatchMarketDetailsHandler, that._loadErrorHandler, null);
            return false;
        });
        return Rules;
    },

    //刷新按钮
    _DeclareButtonRefresh: function () {
        //刷新
        var Refresh = document.createElement("a"),
        that = this;
        $(Refresh).prop("class", "bf-icon-refresh1").prop("title", this._buttonLst.RefreshTitleName);
        $(Refresh).click(function () {
            YMGS.Trade.Web.Services.MatchMarketService.GetTop1MatchMarketDetailsList(that._language, that._matchID, that._betBO, that._loadMatchMarketDetailsHandler, that._loadErrorHandler, null);
            return false;
        });
        return Refresh;
    },

    //比赛市场信息
    _AddMatchMarketContainer: function () {
        //返回一个div的比赛的市场信息
        var div = document.createElement("div");
        $(div).prop("class", "cont-runners");
        var Table = document.createElement("table");
        Table.setAttribute("class", "");
        Table.setAttribute("border", "0");
        Table.appendChild(this._AddMatchMarketContainerHeader());
        Table.appendChild(this._AddMatchMarketContainerBody());

        div.appendChild(Table);
        if (!this._autoRefresh) {
            div.setAttribute("style", "display:none");
        }
        //        if (this._betBO.isOpen) {

        //        }
        //        else {
        //            div.setAttribute("style", "display:none");
        //        }
        return div;
    },

    //比赛市场的具体信息的Header
    _AddMatchMarketContainerHeader: function () {
        var thead = document.createElement("thead");
        var theadTR = document.createElement("tr");
        $(theadTR).prop("class", "back-lay-rules-heading");


        var thFirst = document.createElement('th');
        var spanAmount = document.createElement('span');
        var TotalDealAmount = 0;
        for (var i = 0, arr; arr = this._dataSource.MarketInfo[i++]; ) {
            TotalDealAmount = TotalDealAmount + arr.DealAmount;
        }
        $(thFirst).prop('class', 'matched-values');
        $(spanAmount).text(this._buttonLst.MatchedTitleName + "￥ " + TotalDealAmount);
        thFirst.appendChild(spanAmount);
        theadTR.appendChild(thFirst);

        //投注
        //--投注标识
        var thBack1 = document.createElement("th");
        var spanBackLabel = document.createElement('span');
        $(spanBackLabel).text(this._buttonLst.BackTitleName);
        $(thBack1).prop("class", "matched-valuesTop1");
        thBack1.appendChild(spanBackLabel);
        theadTR.appendChild(thBack1);

        //受注
        var thLay1 = document.createElement("th");
        var spanLayLabel = document.createElement('span');
        $(spanLayLabel).text(this._buttonLst.LayTitleName);
        $(thLay1).prop("class", "matched-valuesTop1");
        thLay1.appendChild(spanLayLabel);
        theadTR.appendChild(thLay1);

        thead.appendChild(theadTR);
        return thead;
    },

    _AddMatchMarketContainerBody: function () {
        var tbody = document.createElement('tbody');
        for (var i = 0, arr; arr = this._dataSource.MarketInfo[i++]; ) {
            tbody.appendChild(this._AddMarketDetailsTR(arr));
        }
        tbody.appendChild(this._constructViewFullMarketTR());
        return tbody;
    },

    _AddMarketDetailsTR: function (arr) {
        var tr = document.createElement('tr');
        $(tr).prop('class', 'runner-row');
        //市场名称
        tr.appendChild(this._AddMarketDetailsBodyFirstTD(arr));
        //投注
        tr.appendChild(this._AddMarketDetailsBodyBackTD(arr, arr.LayOT1, arr.MStatusClass, true));
        //受注
        tr.appendChild(this._AddMarketDetailsBodyLayTD(arr, arr.BackOT1, arr.MStatusClass, true));
        //遮罩层
        tr.appendChild(this._constructMarketTableBodyStatusTd(arr));
        return tr;
    },

    _AddMarketDetailsBodyFirstTD: function (data) {
        var td = document.createElement("td");
        var spanMarketName = document.createElement("span");
        $(spanMarketName).prop("class", "sel-name").text(data.MKName);
        $(td).prop("class", "runner-name");
        td.appendChild(spanMarketName);
        return td;
    },

    _AddMarketDetailsBodyBackTD: function (arr, backOdds, status, isTop1) {
        var td = document.createElement("td");
        var backBtn = document.createElement("button");
        backBtn.setAttribute("type", "button");
        var spanOdds = document.createElement("span");

        var backOddsValue = backOdds == 0 ? '' : backOdds;
        $(td).prop("class", "odds");
        if (isTop1) {
            $(backBtn).prop("class", "bet-button cta-back");
        }
        else {
            $(backBtn).prop("class", "bet-button");
        }
        $(spanOdds).prop("class", "price").text(backOddsValue);
        backBtn.appendChild(spanOdds);

        if (status !== "") {
            backBtn.setAttribute('disabled', 'true');
        }
        else {
            var that = this;
            $(backBtn).click(function () {
                //SetBetBackCookie(that._dataSource.MatchInfo.MatchId, that._dataSource.MatchInfo.MatchType, arr.MarketId, arr.MarketTmpId, backOdds, arr.LayAT1, that._dataSource.MatchInfo.MatchName_CN, that._dataSource.MatchInfo.MatchName_EN, arr.MKName_CN, arr.MKName_EN, arr.MKTmpName_CN, arr.MKTmpName_EN);
                SetBetBackCookie(that._dataSource.MatchInfo.MatchId, that._dataSource.MatchInfo.MatchType, arr.MarketId, arr.MarketTmpId, backOdds, 50, that._dataSource.MatchInfo.MatchName_CN, that._dataSource.MatchInfo.MatchName_EN, arr.MKName_CN, arr.MKName_EN, arr.MKTmpName_CN, arr.MKTmpName_EN);
                var args = new YMGS.Trade.Web.Football.Controls.BetEventArgs(that._clientId);
                that.OnBetEvent(args);
            });
        }
        td.appendChild(backBtn);
        return td;
    },

    _AddMarketDetailsBodyLayTD: function (arr, layOdds, status, isTop1) {
        var td = document.createElement("td");
        var layBtn = document.createElement("button");
        layBtn.setAttribute("type", "button");
        var spanOdds = document.createElement("span");

        var layOddsValue = layOdds == 0 ? '' : layOdds;
        $(td).prop('class', 'odds');
        if (isTop1) {
            $(layBtn).prop("class", "bet-button cta-lay");
        }
        else {
            $(layBtn).prop("class", "bet-button");
        }
        $(spanOdds).prop("class", "price").text(layOddsValue);
        layBtn.appendChild(spanOdds);
        if (status !== "") {
            layBtn.setAttribute('disabled', 'true');
        }
        else {
            var that = this;
            $(layBtn).click(function () {
                //SetBetLayCookie(that._dataSource.MatchInfo.MatchId, that._dataSource.MatchInfo.MatchType, arr.MarketId, arr.MarketTmpId, layOdds, arr.BackAT1, that._dataSource.MatchInfo.MatchName_CN, that._dataSource.MatchInfo.MatchName_EN, arr.MKName_CN, arr.MKName_EN, arr.MKTmpName_CN, arr.MKTmpName_EN);
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
        div.setAttribute('style', 'width: 97px; margin-left: -97px; height:2.3em;')
        $(span).text(arr.DivCharacter);
        div.appendChild(span);
        td.appendChild(div);
        return td;
    },

    _constructViewFullMarketTR: function () {
        var arr = this._dataSource.MatchInfo;
        var tr = document.createElement('tr');
        var td = document.createElement('td');
        var a = document.createElement("a");
        $(tr).prop("class", "matched-viewFullTr");
        $(td).prop('colspan', '4').prop('scope', 'col').prop("class", "matched-viewFull");
        var viewFullMarketLinkAdd = arr.ViewFullADD + "&OrderNO=" + this._betBO.OrdNo;
        $(a).prop('href', viewFullMarketLinkAdd).text(this._buttonLst.ViewFullMarketTitleName);
        td.appendChild(a);
        tr.appendChild(td);
        return tr;
    },

    _ClearUserControlData: function () {
        $(this._div).html("");
    },

    _processLoadMatchMarketDetails: function (serviceOperationResult) {
        if (serviceOperationResult.IsSucceeded) {
            this._ClearUserControlData();
            this._dataSource = serviceOperationResult.Value.matchSource;
            this._betBO = serviceOperationResult.Value.betOrder;
            if (this._dataSource.MarketInfo.length > 0) {
                this._buildMatchMarketDetails();
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
        YMGS.Trade.Web.Services.MatchMarketService.GetTop1MatchMarketDetailsList(this._language, this._matchID, this._betBO, this._loadMatchMarketDetailsHandler, this._loadErrorHandler, null);
    },

    dispose: function () {
        YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop1.callBaseMethod(this, 'dispose');
        this.remove_betEvent(BetEventHandler);
    }
};


YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop1.registerClass("YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop1", Sys.UI.Control);

GenerateProps(YMGS.Trade.Web.Football.Controls.CenterMatchMarketTop1, [
    'dataSource',
    'clientId',
    'language',
    'buttonLst',
    'matchID',
    'betBO',
    'showCash',
    'showGoIn',
    'showRule',
    'showRefresh'
]);

if (typeof (Sys) !== 'undefined') {
    Sys.Application.notifyScriptLoaded();
}