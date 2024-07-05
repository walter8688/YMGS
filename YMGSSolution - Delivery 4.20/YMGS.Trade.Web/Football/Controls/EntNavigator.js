///<reference name="MicrosoftAjax.js"/>

Type.registerNamespace("YMGS.Trade.Web.Football.Controls");

YMGS.Trade.Web.Football.Controls.EntNavigator = function (element) {
    YMGS.Trade.Web.Football.Controls.EntNavigator.initializeBase(this, [element]);

    this._ul = null;
    this._events = new Sys.EventHandlerList();

    //加载Navigator
    this._loadNavigatorHandler = Function.createDelegate(this, this._processLoadNavigator);

    this._loadErrorHandler = Function.createDelegate(this, this._processLoadError);

};

YMGS.Trade.Web.Football.Controls.EntNavigator.prototype = {
    _dataSource: null,
    _clientId: null,
    _language: null,
    _selectedNavigatorId: null,

    _get_events: function () {
        return this._events;
    },

    get_entNavigator: function () {
        return this._ul;
    },

    set_entNavigator: function (value) {
        this._ul = value;
    },

    //构造函数
    initialize: function () {
        YMGS.Trade.Web.Football.Controls.EntNavigator.callBaseMethod(this, 'initialize');
        if (this._dataSource != null) {
            if (this._dataSource.length > 0) {
                this._buildNavigator();
            }
        }
    },
    //析构函数
    dispose: function () {
        YMGS.Trade.Web.Football.Controls.EntNavigator.callBaseMethod(this, 'dispose');
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
        var li = document.createElement("li"),
        a = document.createElement("a"),
        span = document.createElement("span"),
        that = this;
        //Dom Element <a>
        $(a).prop("href", nav.NavigatorLinkAddress);
        if (nav.NavigatorId == this._selectedNavigatorId) {
            $(a).prop('class', 'selected');
        }
        $(span).text(nav.NavigatorName);
        a.appendChild(span);
        li.appendChild(a);
        return li;
    },

    _ClearNavigator: function () {
        $('[id$=entNavigator]').html("");
    },

    _processLoadNavigator: function (serviceOperationResult) {

    },

    _processLoadError: function (error) {
        var errorMessage = error.get_message();
        alert(errorMessage);
    }
};

YMGS.Trade.Web.Football.Controls.EntNavigator.registerClass("YMGS.Trade.Web.Football.Controls.EntNavigator", Sys.UI.Control);

GenerateProps(YMGS.Trade.Web.Football.Controls.EntNavigator, [
    'dataSource',
    'clientId',
    'language',
    'selectedNavigatorId'
]);


if (typeof (Sys) !== 'undefined') {
    Sys.Application.notifyScriptLoaded();
}