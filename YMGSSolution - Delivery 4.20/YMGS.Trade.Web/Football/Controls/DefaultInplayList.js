///<reference name="MicrosoftAjax.js"/>

Type.registerNamespace("YMGS.Trade.Web.Football.Controls");





YMGS.Trade.Web.Football.Controls.DefaultInplayList = function (element) {
    YMGS.Trade.Web.Football.Controls.DefaultInplayList.initializeBase(this, [element]);

    this._inPlayContent = null;
    this._events = new Sys.EventHandlerList();

    this._loadInplayMatchListHandler = Function.createDelegate(this, this._processLoadInplayMatchListHandler);
    this._loadInplayMatchListErrorHandler = Function.createDelegate(this, this._processLoadInplayMatchListErrorHandler);
};

YMGS.Trade.Web.Football.Controls.DefaultInplayList.prototype = {
    _clientId: null,
    _language: null,
    _noDataMessageStr: null,
    _showAllStr: null,
    _footballStr: null,
    _isAutoRefresh: null,
    _isShowAll: null,
    _marketFlagList: null,
    _footballList: null,
    _isRefreshing: null,

    _get_events: function () {
        return this._events;
    },

    get_inPlayContent: function () {
        return this._inPlayContent;
    },

    set_inPlayContent: function (value) {
        this._inPlayContent = value;
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
        YMGS.Trade.Web.Football.Controls.DefaultInplayList.callBaseMethod(this, 'initialize');

        this.add_betEvent(BetEventHandler);

        //构造市场
        if (this._footballList != null) {
            if (this._footballList.length > 0) {
                this._initializeInPlayContent();
            }
            else {
                this._initInPlayNoDataContent();
            }
        }
        

        //自动刷新
        if (this._isAutoRefresh) {
            var that = this;
            //setInterval(that.refreshFootballMatchs, 5000, that);
            setInterval(function () {
                if (typeof (that) === 'undefined')
                    return false;
                if (!that._isAutoRefresh)
                    return false;
                if (!that._isRefreshing) {
                    YMGS.Trade.Web.Services.DefaultInplayService.LoadDefaultInplayData(that._isShowAll, that._language, that._loadInplayMatchListHandler, that._loadInplayMatchListErrorHandler, null);
                }
            }, 60000);
        }
    },

    _initInPlayNoDataContent: function () {
        this._disposeInPlayContent();
        var div = document.createElement('div');
        $(div).prop('class', 'iphp-notice').text(this._noDataMessageStr);
        this._inPlayContent.appendChild(div);
    },

    _initializeInPlayContent: function () {
        this._disposeInPlayContent();
        this._constructInPlayMarkets();
        this._isRefreshing = false;
    },

    _constrcutMarkets: function () {
        this._inPlayContent.appendChild(this._constrcutInPlayMarketContainer());
    },

    _constructInPlayMarkets: function () {
        this._inPlayContent.appendChild(this._constructInPlayMarketAccordion());
        this._inPlayContent.appendChild(this._constrcutInPlayMarketContainer());
    },

    _constructInPlayMarketAccordion: function () {
        var divHeader = document.createElement('div'),
        h2 = document.createElement('h2'),
        a = document.createElement('a'),
        btn = document.createElement('input'),
        span = document.createElement('span'),
        em = document.createElement('em'),
        that = this;
        $(span).prop('class', 'open-arrow');
        $(em).prop('class', 'market-title').text(this._footballStr);
        a.appendChild(span);
        a.appendChild(em);
        $(btn).prop('type', 'button').prop('class', 'bf-icon-refresh').prop('title', 'refresh');
        $(btn).mouseover(function () {
            $(this).prop('class', 'bf-icon-refresh-selected');
        }).mouseout(function () {
            $(this).prop('class', 'bf-icon-refresh');
        });
        $(btn).click(function () {
            if (!that._isRefreshing) {
                YMGS.Trade.Web.Services.DefaultInplayService.LoadDefaultInplayData(that._isShowAll, that._language, that._loadInplayMatchListHandler, that._loadInplayMatchListErrorHandler, null);
            }
            return false;
        });
        h2.appendChild(a);
        h2.appendChild(btn);
        $(divHeader).prop('class', 'market-header');
        accordion($(divHeader));
        divHeader.appendChild(h2);
        return divHeader;
    },

    _constrcutInPlayMarketContainer: function () {
        var div = document.createElement('div');
        $(div).prop('class', 'mod-coupon');
        div.appendChild(this._constructInPlayMarketTbl());
        return div;
    },

    _constructInPlayMarketTbl: function () {
        var tbl = document.createElement('table');
        $(tbl).prop('cellspacing', '0');
        tbl.appendChild(this._constructInPlayMarketTblHeader());
        tbl.appendChild(this._constrcutInPlayMarketTblBody());
        return tbl;
    },

    _constructInPlayMarketTblHeader: function () {
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

    _constrcutInPlayMarketTblBody: function () {
        return this._constructInPlayTblMatchTRByEvent();
    },

    _constructInPlayTblMatchTRByEvent: function () {
        var tbody = document.createElement('tbody'),
        count = 0;
        for (var i = 0, event; event = this._footballList[i++]; ) {
            tbody.appendChild(this._constrcutInPlayTblEventTR(event));
            for (var j = 0, match; match = event.FootballMatchs[j++]; ) {
                count++;
                if (!this._isShowAll && count <= 10) {
                    tbody.appendChild(this._constructInPlayTblMatchTR(match));
                }
                if (this._isShowAll) {
                    tbody.appendChild(this._constructInPlayTblMatchTR(match));
                }
            }
        }
        if (!this._isShowAll) {
            tbody.appendChild(this._constructInPlayShowAllTR());
        }
        return tbody;
    },

    _constructInPlayTblMatchTR: function (obj) {
        var tr = document.createElement('tr');
        $(tr).prop('class', 'betContainer');
        tr.appendChild(this._constrcutInPlayTblMatchTRNameTd(obj));
        for (var i = 0, market; market = obj.FootballMatchMarkets[i++]; ) {
            tr.appendChild(this._constrcutInPlayTblMatchTRBetBackTd(market, obj.MatchStatusClass));
            tr.appendChild(this._constrcutInPlayTblMatchTRBetLayTd(market, obj.MatchStatusClass));
        }
        tr.appendChild(this._constrcutInPlayTblMatchTRStatusTd(obj));
        tr.appendChild(this._constrcutInPlayTblMatchTROtherMarketsTd(obj));
        return tr;
    },

    _constructInPlayShowAllTR: function () {
        var tr = document.createElement('tr'),
        td = document.createElement('td'), s
        a = document.createElement('a'),
        span = document.createElement('span'),
        that = this;
        if (!that._isShowAll) {
            $(span).text(this._showAllStr);
        }
        $(a).prop('class', 'view-all').prop('href', 'javascript:void(0);').append($(span));
        $(a).click(function () {
            that._isShowAll = true;
            YMGS.Trade.Web.Services.DefaultInplayService.LoadDefaultInplayData(that._isShowAll, that._language, that._loadInplayMatchListHandler, that._loadInplayMatchListErrorHandler, null);
            return false;
        });
        $(td).prop('colspan', '10').append($(a));
        $(tr).prop('align', 'right').append($(td));
        return tr;

    },

    _constrcutInPlayTblMatchTROtherMarketsTd: function (obj) {
        var td = document.createElement('td'),
         a = document.createElement('a');
        $(td).prop('class', 'other-markets');
        $(a).prop('title', 'View full market').prop('href', obj.MatchLink)
        td.appendChild(a);
        return td;

    },

    _constrcutInPlayTblMatchTRStatusTd: function (obj) {
        var td = document.createElement('td'),
        div = document.createElement('div'),
        span = document.createElement('span');
        $(div).prop('class', obj.MatchStatusClass);
        div.setAttribute('style', 'width: 296px; margin-left: -299px; height:2.3em;')
        $(span).text(obj.MatchLimitBetStatus);
        div.appendChild(span);
        td.appendChild(div);
        return td;
    },

    _constrcutInPlayTblMatchTRBetBackTd: function (obj, status) {
        var td = document.createElement('td'),
        btn = document.createElement('input'),
        layOddsValue = obj.LayOdds == 0 ? '' : obj.LayOdds.toFixed(2),
        that = this;
        $(td).prop('class', 'cta');
        $(btn).prop('type', 'button').prop('class', 'cta cta-back').prop('value', layOddsValue);
        if (status !== "") {
            btn.setAttribute('disabled', 'true');
        }
        else {
            $(btn).click(function () {
                SetBetBackCookie(obj.MatchId, 1, obj.MarketId, 1, obj.LayOdds, 50, obj.MatchName, obj.MatchNameEn, obj.MarketName, obj.MarketNameEn, obj.MarketTmpName, obj.MarketTmpNameEn);
                var args = new YMGS.Trade.Web.Football.Controls.BetEventArgs(that._clientId);
                that.OnBetEvent(args);
            });
        }
        td.appendChild(btn);
        return td;
    },

    _constrcutInPlayTblMatchTRBetLayTd: function (obj, status) {
        var td = document.createElement('td'),
        btn = document.createElement('input'),
        backOddsValue = obj.BackOdds == 0 ? '' : obj.BackOdds.toFixed(2),
        that = this;
        $(td).prop('class', 'cta');
        $(btn).prop('type', 'button').prop('class', 'cta cta-lay').prop('value', backOddsValue);
        if (status !== "") {
            btn.setAttribute('disabled', 'true');
        }
        else {
            $(btn).click(function () {
                SetBetLayCookie(obj.MatchId, 1, obj.MarketId, 1, obj.BackOdds, 50, obj.MatchName, obj.MatchNameEn, obj.MarketName, obj.MarketNameEn, obj.MarketTmpName, obj.MarketTmpNameEn);
                var args = new YMGS.Trade.Web.Football.Controls.BetEventArgs(that._clientId);
                that.OnBetEvent(args);
            });
        }
        td.appendChild(btn);
        return td;
    },

    _constrcutInPlayTblMatchTRNameTd: function (obj) {
        var td = document.createElement('td');
        $(td).prop('class', 'name').prop('colspan', '4').prop('scope', 'col');
        //--运行成功后 取消。
        //td.appendChild(this._constrcutInPlayTblMatchTRNameTdDivStar(obj));
        td.appendChild(this._constrcutInPlayTblMatchTRNameTdDivMatch(obj));
        return td;
    },

    //收藏标记  待取消
    _constrcutInPlayTblMatchTRNameTdDivStar: function (obj) {
        var divStar = document.createElement('div'),
        a = document.createElement('a'),
        matchId = obj.MatchId,
        that = this;
        $(a).prop('class', obj.MatchFavedCalss).prop('href', 'javascript:void(0);').attr('faved', obj.IsMatchFaved).attr('id', 'a_' + matchId);

        $(divStar).prop('class', 'starContainer').append($(a));
        return divStar;
    },

    _constrcutInPlayTblMatchTRNameTdDivMatch: function (obj) {
        var divMatch = document.createElement('div'),
        a = document.createElement('a'),
        spanHome = document.createElement('span'),
        spanScore = document.createElement('span'),
        spanGuest = document.createElement('span'),
        spanCustom = document.createElement('span');
        $(spanHome).prop('class', 'home-team').text(obj.HomeTeamName);
        $(spanScore).prop('class', 'inplaynow-score').text(obj.CurrentScore);
        $(spanGuest).prop('class', 'away-team').text(obj.GuestTeamName);
        $(spanCustom).prop('class', 'dtstart').text(obj.CustomParam_1);
        $(a).prop('class', 'description').prop('title', 'View match').prop('href', obj.MatchLink);
        $(divMatch).prop('class', 'matchContainer');
        $(a).append($(spanHome)).append($(spanScore)).append($(spanGuest)).append($(spanCustom));
        divMatch.appendChild(a);
        divMatch.appendChild(spanCustom);
        return divMatch;
    },

    _constrcutInPlayTblEventTR: function (obj) {
        var tr = document.createElement('tr')
        $(tr).prop('class', 'inplaynow-competition-header');
        tr.appendChild(this._constrcutInPlayTblEventTRTd(obj));
        return tr;
    },

    _constrcutInPlayTblEventTRTd: function (obj) {
        var td = document.createElement('td'),
        a = document.createElement('a'),
        span = document.createElement('span');
        $(td).prop('colspan', '8').prop('scope', 'col');
        $(a).prop('class', 'ipn-competition-name first').prop('title', 'View event').prop('href', 'javascript:void(0);')
        $(span).text(obj.EventName);
        a.appendChild(span);
        td.appendChild(span);
        return td;
    },

    _disposeInPlayContent: function () {
        $('[id$=inPlayContent]').html('');
    },

    _processLoadInplayMatchListHandler: function (serviceOperationResult) {
        if (serviceOperationResult.IsSucceeded) {
            this._footballList = serviceOperationResult.Value;
            if (this._footballList.length > 0) {
                this._isRefreshing = true;
                this._initializeInPlayContent();
            }
            else {
                this._initInPlayNoDataContent();
            }
        }
        else {
            //alert(serviceOperationResult.Message);
        }
    },

    _processLoadInplayMatchListErrorHandler: function (error) {
        var errorMessage = error.get_message();
        //alert(errorMessage);
    },

    refresh: function () {
        if (!this._isRefreshing) {
            YMGS.Trade.Web.Services.DefaultInplayService.LoadDefaultInplayData(this._isShowAll, this._language, this._loadInplayMatchListHandler, this._loadInplayMatchListErrorHandler, null);
        }
        return false;
    },

    dispose: function () {
        YMGS.Trade.Web.Football.Controls.DefaultInplayList.callBaseMethod(this, 'dispose');
        this.remove_betEvent(BetEventHandler);
    }
};

YMGS.Trade.Web.Football.Controls.DefaultInplayList.registerClass("YMGS.Trade.Web.Football.Controls.DefaultInplayList", Sys.UI.Control);

GenerateProps(YMGS.Trade.Web.Football.Controls.DefaultInplayList, [
    'clientId',
    'language',
    'noDataMessageStr',
    'showAllStr',
    'footballStr',
    'isAutoRefresh',
    'isShowAll',
    'marketFlagList',
    'footballList',
]);

if (typeof (Sys) !== 'undefined') {
    Sys.Application.notifyScriptLoaded();
}
