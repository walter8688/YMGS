///<reference name="MicrosoftAjax.js"/>

Type.registerNamespace("YMGS.Trade.Web.Football.Controls");

YMGS.Trade.Web.Football.Controls.FootballMatchList = function (element) {
    YMGS.Trade.Web.Football.Controls.FootballMatchList.initializeBase(this, [element]);

    this._footballCalander = null;
    this._footballContent = null;
    this._events = new Sys.EventHandlerList();

    this._loadFootballCalanderHandler = Function.createDelegate(this, this._processLoadFootballCalanderHandler);
    this._loadFootballCalanderHandlerError = Function.createDelegate(this, this._processLoadFootballCalanderHandlerError);

    this._loadFootballMatchsHandler = Function.createDelegate(this, this._processLoadFootballMatchsHandler);
    this._loadFootballMatchsHandlerError = Function.createDelegate(this, this._processLoadFootballMatchsHandlerError);
};

YMGS.Trade.Web.Football.Controls.FootballMatchList.prototype = {
    _calanderDataSource: null,
    _dataSource: null,
    _clientId: null,
    _language: null,
    _noDataMessageStr: null,
    _footballCalanderId: null,
    _isAutoRefresh: null,
    _marketFlagList: null,

    _get_events: function () {
        return this._events;
    },

    get_footballMatchList: function () {
        return this._footballCalander;
    },

    set_footballMatchList: function (value) {
        this._footballCalander = value;
    },

    get_footballContent: function () {
        return this._footballContent;
    },

    set_footballContent: function (value) {
        this._footballContent = value;
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
        YMGS.Trade.Web.Football.Controls.FootballMatchList.callBaseMethod(this, 'initialize');

        this.add_betEvent(BetEventHandler);

        //构建足球日期
        if (this._calanderDataSource != null) {
            if (this._calanderDataSource.length > 0) {
                this._initializeFootballCanlander();
            }
        }

        //构建比赛市场
        if (this._dataSource != null) {
            if (this._dataSource.length > 0) {
                this._initializeFootballMatchs();
            }
            else {
                this._initFootballNoDataContent();
            }
        }
        //自动刷新
        if (this._isAutoRefresh) {
            var that = this;
            setInterval(function () {
                if (typeof (that) === 'undefined')
                    return false;
                if (!that._isAutoRefresh)
                    return false;
                YMGS.Trade.Web.Services.FootballMatchListService.LoadFootballGameList(that._footballCalanderId, that._language, that._loadFootballMatchsHandler, that._loadFootballMatchsHandlerError, null);
            }, 60000);
        }
    },


    //供下注时调用
    refresh: function () {
        YMGS.Trade.Web.Services.FootballMatchListService.LoadFootballGameList(this._footballCalanderId, this._language, this._loadFootballMatchsHandler, this._loadFootballMatchsHandlerError, null);
    },

    _initFootballNoDataContent: function () {
        this._disposeFootballMatchs();
        var div = document.createElement('div');
        $(div).prop('class', 'iphp-notice').text(this._noDataMessageStr);
        this._footballContent.appendChild(div);
    },

    _initializeFootballMatchs: function () {
        this._disposeFootballMatchs();
        this._addFootballMatchs();
    },

    _disposeFootballMatchs: function () {
        $('[id$=footballContent]').html('');
    },

    _addFootballMatchs: function () {
        for (var i = 0, arr; arr = this._dataSource[i++]; ) {
            this._footballContent.appendChild(this._addFootballMatch(arr));
        }
    },

    _addFootballMatch: function (fb) {
        var divWrapper = document.createElement('div');
        $(divWrapper).prop('class', 'market');
        divWrapper.appendChild(this._constructMarketHeader(fb));
        divWrapper.appendChild(this._constructMarketContainer(fb));
        return divWrapper;
    },

    _constructMarketHeader: function (fb) {
        var divHeader = document.createElement('div'),
        h2 = document.createElement('h2'),
        a = document.createElement('a'),
        span = document.createElement('span'),
        em = document.createElement('em');
        $(span).prop('class', 'open-arrow');
        $(em).prop('class', 'market-title').text(fb.EventName);
        a.appendChild(span);
        a.appendChild(em);
        h2.appendChild(a);
        $(divHeader).prop('class', 'market-header');
        accordion($(divHeader));
        divHeader.appendChild(h2);
        return divHeader;
    },

    _constructMarketContainer: function (fb) {
        var divContainer = document.createElement('div'),
        tbl = document.createElement('table');
        $(tbl).prop('cellspacing', '0');
        $(divContainer).prop('class', 'mod-coupon');
        tbl.appendChild(this._constructMarketTableHeader(fb));
        tbl.appendChild(this._constructMarketTableBody(fb));
        divContainer.appendChild(tbl);
        return divContainer;
    },

    _constructMarketTableHeader: function (fb) {
        var thead = document.createElement('thead'),
        tr = document.createElement('tr'),
        thFirst = document.createElement('th'),
        thLast = document.createElement('th'),
        spanLast = document.createElement('span');
        $(tr).prop('class', 'home-away-headings');
        $(thFirst).prop('class', 'header').prop('colSpan', '4');
        $(thLast).prop('class', 'other-markets');
        $(spanLast).prop('class', 'hide-offscreen');
        tr.appendChild(thFirst);
        thLast.appendChild(spanLast);
        //Home Away Draw
        for (var i = 0, arr; arr = this._marketFlagList[i++]; ) {
            var th = document.createElement('th'),
            span = document.createElement('span');
            $(th).prop('colSpan', '2').prop('align', 'center');
            $(span).text(arr.MarketFlagName);
            th.appendChild(span);
            tr.appendChild(th);
        }
        tr.appendChild(thLast);
        thead.appendChild(tr);
        return thead;
    },

    _constructMarketTableBody: function (fb) {
        var tbody = document.createElement('tbody');
        for (var i = 0, arr; arr = fb.FootballMatchs[i++]; ) {
            var tr = document.createElement('tr');
            $(tr).prop('class', 'betContainer');
            alternatelyDisplay($(tr));
            tr.appendChild(this._constructMarketTableBodyFirstTd(arr));
            for (var j = 0, market; market = arr.FootballMatchMarkets[j++]; ) {
                tr.appendChild(this._constructMarketTableBodyBetBackInfoTd(market, arr.MatchStatusClass));
                tr.appendChild(this._constructMarketTableBodyBetLayInfoTd(market, arr.MatchStatusClass));
            }
            tr.appendChild(this._constructMarketTableBodyStatusTd(arr));
            tr.appendChild(this._constructMarketTableBodyOtherMarketTd(arr));
            tbody.appendChild(tr);
        }
        return tbody;
    },

    _constructMarketTableBodyFirstTd: function (match) {
        var td = document.createElement('td'),
        div = document.createElement('div'),
        a = document.createElement('a'),
        spanHome = document.createElement('span'),
        spanScore = document.createElement('span'),
        spanGuest = document.createElement('span'),
        spanStart = document.createElement('span');
        $(td).prop('class', 'name').prop('colSpan', '4');
        $(div).prop('class', 'matchContainer');
        $(a).prop('class', 'description').prop('title', 'View match').prop('href', match.MatchLink);
        $(spanHome).prop('class', 'home-team').text(match.HomeTeamName);
        $(spanScore).prop('class', 'inplaynow-score').text(match.CurrentScore);
        $(spanGuest).prop('class', 'away-team').text(match.GuestTeamName);
        $(spanStart).prop('class', 'dtstart').text(match.CustomParam_1);
        a.appendChild(spanHome);
        a.appendChild(spanScore);
        a.appendChild(spanGuest);
        div.appendChild(a);
        div.appendChild(spanStart);
        td.appendChild(div);
        return td;
    },

    _constructMarketTableBodyBetBackInfoTd: function (market, status) {
        var td = document.createElement('td'),
        btn = document.createElement('input'),
        layOddsValue = market.LayOdds == 0 ? '' : market.LayOdds.toFixed(2);
        $(td).prop('class', 'cta'),
        that = this;
        $(btn).prop('type', 'button').prop('class', 'cta cta-back').prop('value', layOddsValue);
        if (status !== "") {
            btn.setAttribute('disabled', 'true');
        }
        else {
            $(btn).click(function () {
                //SetBetBackCookie(market.MatchId, 1, market.MarketId, 1, market.LayOdds, market.BackMatchAmouts, market.MatchName, market.MatchNameEn, market.MarketName, market.MarketNameEn, market.MarketTmpName, market.MarketTmpNameEn);
                SetBetBackCookie(market.MatchId, 1, market.MarketId, 1, market.LayOdds, 50, market.MatchName, market.MatchNameEn, market.MarketName, market.MarketNameEn, market.MarketTmpName, market.MarketTmpNameEn);
                var args = new YMGS.Trade.Web.Football.Controls.BetEventArgs(that._clientId);
                that.OnBetEvent(args);
            });
        }
        td.appendChild(btn);
        return td;
    },

    _constructMarketTableBodyBetLayInfoTd: function (market, status) {
        var td = document.createElement('td'),
        btn = document.createElement('input'),
        backOddsValue = market.BackOdds == 0 ? '' : market.BackOdds.toFixed(2),
        that = this;
        $(td).prop('class', 'cta');
        $(btn).prop('type', 'button').prop('class', 'cta cta-lay').prop('value', backOddsValue);
        if (status !== "") {
            btn.setAttribute('disabled', 'true');
        }
        else {
            $(btn).click(function () {
                //SetBetLayCookie(market.MatchId, 1, market.MarketId, 1, market.BackOdds, market.BackMatchAmouts, market.MatchName, market.MatchNameEn, market.MarketName, market.MarketNameEn, market.MarketTmpName, market.MarketTmpNameEn);
                SetBetLayCookie(market.MatchId, 1, market.MarketId, 1, market.BackOdds, 50, market.MatchName, market.MatchNameEn, market.MarketName, market.MarketNameEn, market.MarketTmpName, market.MarketTmpNameEn);
                var args = new YMGS.Trade.Web.Football.Controls.BetEventArgs(that._clientId);
                that.OnBetEvent(args);
            });
        }
        td.appendChild(btn);
        return td;
    },

    _constructMarketTableBodyStatusTd: function (match) {
        var td = document.createElement('td'),
        div = document.createElement('div'),
        span = document.createElement('span');
        $(div).prop('class', match.MatchStatusClass);
        div.setAttribute('style', 'width: 296px; margin-left: -299px; height:2.3em;')
        $(span).text(match.MatchLimitBetStatus);
        div.appendChild(span);
        td.appendChild(div);
        return td;
    },

    _constructMarketTableBodyOtherMarketTd: function (match) {
        var td = document.createElement('td'),
         a = document.createElement('a');
        $(td).prop('class', 'other-markets');
        $(a).prop('title', 'View full market').prop('href', match.MatchLink)
        td.appendChild(a);
        return td;
    },

    _initializeFootballCanlander: function () {
        this._disposeFootballCanlander();
        this._addFootballCanlanderItems();
    },

    _disposeFootballCanlander: function () {
        $('[id$=footballMatchList]').html('');
    },

    _addFootballCanlanderItems: function () {
        for (var i = 0, arr; arr = this._calanderDataSource[i++]; ) {
            this._footballCalander.appendChild(this._addFootballCanlanderItem(arr, i));
        }
        this._footballCalander.setAttribute('class', 'market-tabs');
    },

    _addFootballCanlanderItem: function (fbCal, index) {
        var li = document.createElement('li'),
        a = document.createElement('a'),
        that = this;
        $(li).prop('class', 'tab').attr('cid', fbCal.CalendarDateId);
        if (index == 1) {
            $(li).prop('class', 'tab-selected');
        }
        $(li).click(function () {
            $('.market-tabs li').each(function () {
                if ($(this).prop('class') == 'tab-selected') {
                    $(this).removeClass('tab-selected').addClass('tab');
                }
            });
            $(this).removeClass('tab').addClass('tab-selected');
            that.set_footballCalanderId($(this).attr('cid'));
            YMGS.Trade.Web.Services.FootballMatchListService.LoadFootballGameList(that._footballCalanderId, that._language, that._loadFootballMatchsHandler, that._loadFootballMatchsHandlerError, null);
            return false;
        });
        $(a).text(fbCal.WeekCalendarName);
        li.appendChild(a);
        return li;
    },

    _processLoadFootballCalanderHandler: function (serviceOperationResult) {

    },

    _processLoadFootballCalanderHandlerError: function (error) {
        var errorMessage = error.get_message();
        //alert(errorMessage);
    },

    _processLoadFootballMatchsHandler: function (serviceOperationResult) {
        if (serviceOperationResult.IsSucceeded) {
            this._dataSource = serviceOperationResult.Value;
            if (this._dataSource.length > 0) {
                this._initializeFootballMatchs();
            }
            else {
                this._initFootballNoDataContent();
            }
        }
        else {
            //alert(serviceOperationResult.Message);
        }
    },

    _processLoadFootballMatchsHandlerError: function (error) {
        var errorMessage = error.get_message();
        //alert(errorMessage);
    },

    dispose: function () {
        YMGS.Trade.Web.Football.Controls.FootballMatchList.callBaseMethod(this, "dispose");
        this.remove_betEvent(BetEventHandler);
    }

};

YMGS.Trade.Web.Football.Controls.FootballMatchList.registerClass("YMGS.Trade.Web.Football.Controls.FootballMatchList",Sys.UI.Control);

GenerateProps(YMGS.Trade.Web.Football.Controls.FootballMatchList, [
    'calanderDataSource',
    'dataSource',
    'clientId',
    'language',
    'noDataMessageStr',
    'footballCalanderId',
    'isAutoRefresh',
    'marketFlagList'
]);

if (typeof (Sys) !== 'undefined') {
    Sys.Application.notifyScriptLoaded();
}

