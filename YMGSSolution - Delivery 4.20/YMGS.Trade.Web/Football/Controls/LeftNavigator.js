///<reference name="MicrosoftAjax.js"/>

Type.registerNamespace("YMGS.Trade.Web.Football.Controls");

YMGS.Trade.Web.Football.Controls.LeftNavigator = function (element) {
    YMGS.Trade.Web.Football.Controls.LeftNavigator.initializeBase(this, [element]);

    this._ul = null;
    this._events = new Sys.EventHandlerList();

    //加载Navigator
    this._loadNavigatorHandler = Function.createDelegate(this, this._processLoadNavigator);

    this._loadErrorHandler = Function.createDelegate(this, this._processLoadError);

};

YMGS.Trade.Web.Football.Controls.LeftNavigator.prototype = {
    _dataSource: null,
    _clientId: null,
    _language: null,

    _get_events: function () {
        return this._events;
    },

    get_leftNavigator: function () {
        return this._ul;
    },

    set_leftNavigator: function (value) {
        this._ul = value;
    },

    //构造函数
    initialize: function () {
        YMGS.Trade.Web.Football.Controls.LeftNavigator.callBaseMethod(this, 'initialize');
        if (this._dataSource != null) {
            if (this._dataSource.length > 0) {
                this._buildNavigator();
            }
        }
    },
    //析构函数
    dispose: function () {
        YMGS.Trade.Web.Football.Controls.LeftNavigator.callBaseMethod(this, 'dispose');
    },
    //构建Navigator
    _buildNavigator: function () {
        this._ClearNavigator();
        this._AddLiItems();
    },

    _AddLiItems: function () {
        for (var i = 0, arr; arr = this._dataSource[i++]; ) {
            this._ul.appendChild(this._AddLiItem(arr));
        }
    },

    _AddLiItem: function (nav) {
        var navigatorTypeId = nav.NavigatorTypeId,
        li = document.createElement("li"),
        a = document.createElement("a"),
        span = document.createElement("span"),
        that = this;
        //Dom Element <a>
        $(a).prop("href", nav.NavigatorLinkAddress);
        if (navigatorTypeId != 7) {
            $(a).text(nav.NavigatorName);
            $(span).prop("class", "parent has-children");
        }
        else {
            if (nav.isZouDi) {
                $(span).prop("class", "is-inplay");
            }
            else {
                $(span).prop("class", "going-inplay");
            }
            $(span).text(nav.NavigatorName);
        }
        $(a).click(function () {
            if (navigatorTypeId == 5 || navigatorTypeId == 6 || navigatorTypeId == 7)
                return true;
            YMGS.Trade.Web.Services.LeftNavigatorService.LoadLeftNavigator(nav.SearchCondition, that._language, that._loadNavigatorHandler, that._loadErrorHandler, null);
            return false;
        });
        a.appendChild(span);
        li.appendChild(a);
        return li;
    },

    _ClearNavigator: function () {
        $('[id$=leftNavigator]').html("");
    },

    _processLoadNavigator: function (serviceOperationResult) {
        if (serviceOperationResult.IsSucceeded) {
            this._dataSource = serviceOperationResult.Value;
            this._buildNavigator();
        }
        else {
            alert(serviceOperationResult.Message);
        }
    },

    _processLoadError: function (error) {
        var errorMessage = error.get_message();
        alert(errorMessage);
    }
};

YMGS.Trade.Web.Football.Controls.LeftNavigator.registerClass("YMGS.Trade.Web.Football.Controls.LeftNavigator", Sys.UI.Control);

GenerateProps(YMGS.Trade.Web.Football.Controls.LeftNavigator, [
    'dataSource',
    'clientId',
    'language'
]);


if (typeof (Sys) !== 'undefined') {
    Sys.Application.notifyScriptLoaded();
}