///<reference name="MicrosoftAjax.js"/>
Type.registerNamespace("YMGS.Trade.Web.Home");

//HomeLogin Control
YMGS.Trade.Web.Home.HomeLoginClientCtrl = function (element) {
    YMGS.Trade.Web.Home.HomeLoginClientCtrl.initializeBase(this, [element]);
    this._loginPanel = null;
    this._userInfoPanel = null;
    this._events = new Sys.EventHandlerList();

    this._autoLoginHandler = Function.createDelegate(this, this._processAutoLoginHandler);
    this._autoLoginHandlerError = Function.createDelegate(this, this._processAutoLoginHandlerError);

    this._loginHandler = Function.createDelegate(this, this._processLoginHandler);
    this._loginHandlerError = Function.createDelegate(this, this._processLoginHandlerError);

    this._logoutHandler = Function.createDelegate(this, this._processLogoutHandler);
    this._logoutHandlerError = Function.createDelegate(this, this._processLogoutHandlerError);

    this._updateUserFundHandler = Function.createDelegate(this, this._processUpdateUserHandler);
    this._updateUserFundHandlerError = Function.createDelegate(this, this._processUpdateUserHandlerError);
};

YMGS.Trade.Web.Home.HomeLoginClientCtrl.prototype = {
    _isLogin: null,
    _defaultUserNameCookie: null,
    _homeLoginClientCtrlUIText: null,
    _language: null,
    _userId: null,
    _userName: null,
    _userFund: null,

    get_defaultUserNameCookie: function () {
        return this._defaultUserNameCookie;
    },

    get_loginPanel: function () {
        return this._loginPanel;
    },

    set_loginPanel: function (value) {
        this._loginPanel = value;
    },

    get_userInfoPanel: function () {
        return this._userInfoPanel;
    },

    set_userInfoPanel: function (value) {
        this._userInfoPanel = value;
    },

    initialize: function () {
        YMGS.Trade.Web.Home.HomeLoginClientCtrl.callBaseMethod(this, 'initialize');
        this._buildHomeLoginPanel();
    },

    _buildHomeLoginPanel: function () {
        this._destoyLoginPanel();
        this._destoyUserInfoPanel();
        if (this._isLogin) {
            YMGS.Trade.Web.Services.HomeLoginService.GetUserFund(this._userId, this._autoLoginHandler, this._autoLoginHandlerError, null);
            return false;
        }
        else {
            this._buildLoginPanel();
        }

    },

    _destoyLoginPanel: function () {
        $(this._loginPanel).html('');
    },

    _buildLoginPanel: function () {
        var textUserName = document.createElement('input'),
        textPassword = document.createElement('input'),
        divLogin = document.createElement('div'),
        btnLogin = document.createElement('input'),
        div = document.createElement('div'),
        tbl = document.createElement('table'),
        tr = document.createElement('tr'),
        aJoin = document.createElement('a'),
        tdJoin = document.createElement('td'),
        td = document.createElement('td'),
        aForget = document.createElement('a'),
        tdForget = document.createElement('td'),
        that = this;

        $(textUserName).prop('id', 'txtUserName').prop('type', 'text').prop('class', 'logusername').prop('placeholder', this._homeLoginClientCtrlUIText.DefaultUserName).attr('onpaste', 'filterUnSaveStr(this)');
        $(textPassword).prop('id', 'txtPassword').prop('type', 'password').prop('class', 'logusername').prop('placeholder', this._homeLoginClientCtrlUIText.DeafalutPassword).attr('onpaste', 'return false;');

        $(textUserName).keyup(function () {
            filterUnSaveStr(this);
        }).keypress(function (evt) {
            if (evt == null) {
                evt = event;
            }
            if (evt.keyCode == 13) {
                YMGS.Trade.Web.Services.HomeLoginService.UserLogin(that._language, $(textUserName).val(), $(textPassword).val(), that._loginHandler, that._loginHandlerError, null);
                return false;
            }
        });

        $(textPassword).keypress(function (evt) {
            if (evt == null) {
                evt = event;
            }
            if (evt.keyCode == 13) {
                YMGS.Trade.Web.Services.HomeLoginService.UserLogin(that._language, $(textUserName).val(), $(textPassword).val(), that._loginHandler, that._loginHandlerError, null);
                return false;
            }
        });

        $(aJoin).prop('class', 'homeloginlink').prop('href', this._homeLoginClientCtrlUIText.UserRegisterFrmURL).text(this._homeLoginClientCtrlUIText.Register);
        $(tdJoin).append($(aJoin));
        $(td).text('|');
        $(aForget).prop('class', 'homeloginlink').prop('href', this._homeLoginClientCtrlUIText.ForgotPasswordURL).text(this._homeLoginClientCtrlUIText.ForgetPWD);
        $(tdForget).append(aForget);

        $(tr).append($(tdJoin)).append($(td)).append($(tdForget));

        $(tbl).prop('class', 'noBorderTable logdivlink').append($(tr));
        //div.appendChild(tbl);
        $(div).append($(tbl));

        $(btnLogin).prop('id', 'btnLogin').prop('type', 'button').prop('class', 'loginbtn').prop('value', this._homeLoginClientCtrlUIText.Login);
        $(btnLogin).click(function () {
            YMGS.Trade.Web.Services.HomeLoginService.UserLogin(that._language, $(textUserName).val(), $(textPassword).val(), that._loginHandler, that._loginHandlerError, null);
            return false;
        });

        $(divLogin).prop('class', 'logdiv').append($(btnLogin)).append($(div));

        $(this._loginPanel).append($(textUserName)).append($(textPassword)).append($(divLogin));
    },

    _destoyUserInfoPanel: function () {
        $(this._userInfoPanel).html('');
    },

    _buildUserInfoPanel: function () {
        var tbl = document.createElement('table');
        $(tbl).prop('cellspacing', '8px');
        $(tbl).append(this._buildUserInfoPanelLoginRow());
        $(tbl).append(this._buildUserInfoPanelBanlanceRow());
        $(tbl).append(this._buildUserInfoPanelLinkRow());
        $(tbl).append(this._buildUserInfoPanelLogoutRow());
        $(this._userInfoPanel).append($(tbl));
    },

    _buildUserInfoPanelLoginRow: function () {
        var tr = document.createElement('tr'),
        td = document.createElement('td'),
        span = document.createElement('span'),
        image = document.createElement('input');

        $(image).prop('type', 'image').prop('src', 'Images/mail.png');
        $(image).click(function () {
            window.location.href = 'MemberShip/SystemAutoMessageFrm.aspx';
            return false;
        });
        $(span).text(this._userName);
        $(td).append($(span)).append($(image));
        $(tr).append($(td));
        return tr;
    },

    _buildUserInfoPanelBanlanceRow: function () {
        var tr = document.createElement('tr'),
        td = document.createElement('td'),
        span = document.createElement('span'),
        b = document.createElement('b'),
        spanTitle = document.createElement('span'),
        spanFund = document.createElement('span');

        $(spanFund).prop('id', 'userFund').text(this._userFund);
        $(b).append($(spanFund));
        $(spanTitle).text(this._homeLoginClientCtrlUIText.CurAccount);
        $(span).append($(spanTitle)).append($(b));
        $(td).append($(span));
        $(tr).append($(td));
        return tr;
    },

    _buildUserInfoPanelLinkRow: function () {
        var tr = document.createElement('tr'),
        td = document.createElement('td'),
        aMyAccount = document.createElement('a'),
        aCharge = document.createElement('a'),
        aMyTrade = document.createElement('a');
        $(aMyAccount).prop('class', 'loglinkfont').prop('href', this._homeLoginClientCtrlUIText.MemberShipHomeFrmURL).text(this._homeLoginClientCtrlUIText.Myaccount);
        $(aCharge).prop('class', 'loglinkfont').prop('href', this._homeLoginClientCtrlUIText.OnlineChargeFrmURL).text(this._homeLoginClientCtrlUIText.OnlineCharge);
        $(aMyTrade).prop('class', 'loglinkfont').prop('href', this._homeLoginClientCtrlUIText.MyTradeFrmURL).text(this._homeLoginClientCtrlUIText.HisTradeReport);
        $(td).prop('id', 'userInfoLinkTd').append($(aMyAccount)).append($(aCharge)).append($(aMyTrade));
        $(tr).append($(td));
        return tr;
    },

    _buildUserInfoPanelLogoutRow: function () {
        var tr = document.createElement('tr'),
        td = document.createElement('td'),
        input = document.createElement('input'),
        that = this;

        $(input).prop('class', 'loginbtn').prop('type', 'button').prop('value', this._homeLoginClientCtrlUIText.SecureLogout);
        $(input).click(function () {
            YMGS.Trade.Web.Services.HomeLoginService.UserLogout(that._logoutHandler, that._logoutHandlerError, null);
            return false;
        });
        $(td).append($(input));
        $(tr).append($(td));
        return tr;
    },

    _processAutoLoginHandler: function (serviceOperationResult) {
        if (serviceOperationResult.IsSucceeded) {
            var succeedValue = serviceOperationResult.Value[0];
            this._userFund = succeedValue.UserFund;
            this._buildUserInfoPanel();
        }
        else {
            alert(serviceOperationResult.Message);
        }
    },

    _processAutoLoginHandlerError: function (error) {
        var errorMessage = error.get_message();
        alert(errorMessage);
    },

    _processLoginHandler: function (serviceOperationResult) {
        if (serviceOperationResult.IsSucceeded) {
            var succeedValue = serviceOperationResult.Value[0];
            if (succeedValue.indexOf('Success') > -1) {
                var userDetail = succeedValue.split('@');
                this._isLogin = true;
                this._userName = userDetail[2];
                this._userFund = userDetail[1];
                this._destoyLoginPanel();
                this._destoyUserInfoPanel();
                this._buildUserInfoPanel();
                return false;
            } else {
                window.location.href = succeedValue;
            }
        }
        else {
            alert(serviceOperationResult.Message);
        }
    },

    _processLoginHandlerError: function (error) {
        var errorMessage = error.get_message();
        alert(errorMessage);
    },

    _processLogoutHandler: function (serviceOperationResult) {
        if (serviceOperationResult.IsSucceeded) {
            var succeedValue = serviceOperationResult.Value[0];
            this._isLogin = false;
            this._userName = null;
            this._userFund = null;
            window.location.href = succeedValue
        }
        else {
            alert(serviceOperationResult.Message);
        }
    },

    _processLogoutHandlerError: function (error) {
        var errorMessage = error.get_message();
        alert(errorMessage);
    },

    updateUserFund: function () {
        if (this._userId == null)
            return false;
        YMGS.Trade.Web.Services.HomeLoginService.GetUserFund(this._userId, this._updateUserFundHandler, this._updateUserFundHandlerError, null);
        return false;
    },

    _processUpdateUserHandler: function (serviceOperationResult) {
        if (serviceOperationResult.IsSucceeded) {
            var succeedValue = serviceOperationResult.Value[0];
            this._userId = succeedValue.USER_ID;
            this._userFund = succeedValue.UserFund;
            $('[id$=userFund]').text(this._userFund);
        }
        else {
            alert(serviceOperationResult.Message);
        }
    },

    _processUpdateUserHandlerError: function (error) {
        var errorMessage = error.get_message();
        alert(errorMessage);
    },

    dispose: function () {
        YMGS.Trade.Web.Home.HomeLoginClientCtrl.callBaseMethod(this, 'dispose');
    }
};

YMGS.Trade.Web.Home.HomeLoginClientCtrl.registerClass("YMGS.Trade.Web.Home.HomeLoginClientCtrl", Sys.UI.Control);

GenerateProps(YMGS.Trade.Web.Home.HomeLoginClientCtrl, [
    'homeLoginClientCtrlUIText',
    'language',
    'userId',
    'userName',
    'userFund',
    'isLogin',
]);

if (typeof (Sys) !== 'undefined') {
    Sys.Application.notifyScriptLoaded();
}
