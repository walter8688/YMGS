///<reference name="MicrosoftAjax.js"/>
Type.registerNamespace("YMGS.Trade.Web.Football.Controls")

YMGS.Trade.Web.Football.Controls.MatchRealInfo = function (element) {
    YMGS.Trade.Web.Football.Controls.MatchRealInfo.initializeBase(this, [element]);

    this._div = null;
    this._events = new Sys.EventHandlerList();

    //加载比赛的某种市场的详细数据
    this._loadMatchMarketDetailsHandler = Function.createDelegate(this, this._processLoadMatchRealDetails);

    this._loadErrorHandler = Function.createDelegate(this, this._processLoadError);
};

YMGS.Trade.Web.Football.Controls.MatchRealInfo.prototype = {
    _dataSource: null,
    _clientId: null,
    _language: 0,
    _matchID: 0,

    _get_events: function () {
        return this._events;
    },

    get_divMatchRealInfo: function () {
        return this._div;
    },

    set_divMatchRealInfo: function (value) {
        this._div = value;
    },

    initialize: function () {
        YMGS.Trade.Web.Football.Controls.MatchRealInfo.callBaseMethod(this, 'initialize');

        if (this._dataSource != null) {
            this._buildMatchRealDetails();
        }

        var that = this;
        setInterval(function () {
            if (typeof (that) === 'undefined')
                return false;
            YMGS.Trade.Web.Services.MatchMarketService.GetMatchRealTitleInfo(that._language, that._matchID, that._loadMatchMarketDetailsHandler, that._loadErrorHandler, null);
        }, 60000);

    },

    _buildMatchRealDetails: function () {
        this._ClearUserControlData();
        this._div.appendChild(this._AddMatchRealDetails(this._dataSource));
    },

    //构建比赛的title信息
    _AddMatchRealDetails: function (obj) {
        var divMatch = document.createElement('div'),
        spanHome = document.createElement('span'),
        spanScore = document.createElement('span'),
        spanGuest = document.createElement('span'),
        spanCustom = document.createElement('span');
        $(spanHome).prop('class', 'hometeam').text(obj.HomeTeamName);
        $(spanScore).prop('class', 'inplaynowscore').text(obj.CurrentScore);
        $(spanGuest).prop('class', 'awayteam').text(obj.GuestTeamName);
        $(spanCustom).prop('class', 'dtstart').text(obj.CustomParam_1);
        $(divMatch).prop('class', 'matchRealInfo');
        $(divMatch).append($(spanHome)).append($(spanScore)).append($(spanGuest)).append($(spanCustom));
        divMatch.appendChild(spanCustom);
        return divMatch;
    },

    _ClearUserControlData: function () {
        $('[id$=divMatchRealInfo]').html("");
    },

    _processLoadMatchRealDetails: function (serviceOperationResult) {
        if (serviceOperationResult.IsSucceeded) {
            this._ClearUserControlData();
            this._dataSource = serviceOperationResult.Value;
            if (this._dataSource != null) {
                this._buildMatchRealDetails();
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
        YMGS.Trade.Web.Services.MatchMarketService.GetMatchRealTitleInfo(this._language, this._matchID, this._loadMatchMarketDetailsHandler, this._loadErrorHandler, null);
    },

    dispose: function () {
        YMGS.Trade.Web.Football.Controls.MatchRealInfo.callBaseMethod(this, 'dispose');
    }
};


YMGS.Trade.Web.Football.Controls.MatchRealInfo.registerClass("YMGS.Trade.Web.Football.Controls.MatchRealInfo", Sys.UI.Control);

GenerateProps(YMGS.Trade.Web.Football.Controls.MatchRealInfo, [
    'dataSource',
    'clientId',
    'language',
    'matchID'
]);

if (typeof (Sys) !== 'undefined') {
    Sys.Application.notifyScriptLoaded();
}