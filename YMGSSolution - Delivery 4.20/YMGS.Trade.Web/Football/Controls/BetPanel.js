///<reference name="MicrosoftAjax.js"/>
Type.registerNamespace("YMGS.Trade.Web.Football.Controls");

//BetEventArgs
YMGS.Trade.Web.Football.Controls.BetEventArgs = function (betSender) {
    var e = Function._validateParams(arguments,
                [{ name: "betSender", type: String}]);
    if (e) throw e;
    YMGS.Trade.Web.Football.Controls.BetEventArgs.initializeBase(this);
    this.betSender = betSender;
}
YMGS.Trade.Web.Football.Controls.BetEventArgs.registerClass(
  'YMGS.Trade.Web.Football.Controls.BetEventArgs', Sys.EventArgs);

//BetPanelControl
YMGS.Trade.Web.Football.Controls.BetPanel = function (element) {
    YMGS.Trade.Web.Football.Controls.BetPanel.initializeBase(this, [element]);
    this._backPanle = null;
    this._layPanle = null;
    this._betFootPanel = null;
    this._events = new Sys.EventHandlerList();

    this._placeBetHandler = Function.createDelegate(this, this._processPlaceBetHandler);
    this._placeBetHandlerError = Function.createDelegate(this, this._processPlaceBetHandlerError);
};

YMGS.Trade.Web.Football.Controls.BetPanel.prototype = {
    _betBackData: null,
    _betLayData: null,
    _language: null,
    _betPanelUIText: null,

    _get_events: function () {
        return this._events;
    },
    get_betBackData: function () {
        return GetCookie('Back');
    },

    get_betLayData: function () {
        return GetCookie('Lay');
    },

    get_backPanle: function () {
        return this._backPanle;
    },

    set_backPanle: function (value) {
        this._backPanle = value;
    },

    get_layPanle: function () {
        return this._backPanle;
    },

    set_layPanle: function (value) {
        this._layPanle = value;
    },

    get_betFootPanel: function () {
        return this._betFootPanel;
    },

    set_betFootPanel: function (value) {
        this._betFootPanel = value;
    },

    add_betSuccessEvent: function (handler) {
        var e = Function._validateParams(arguments,
                    [{ name: "handler", type: Function}]);
        if (e) throw e;
        this._get_events().addHandler("BetSuccessEvent", handler);
    },

    remove_betSuccessEvent: function (handler) {
        var e = Function._validateParams(arguments,
                    [{ name: "handler", type: Function}]);
        if (e) throw e;
        this._get_events().removeHandler("BetSuccessEvent", handler);
    },

    OnBetSuccessEvent: function () {
        //        var e = Function._validateParams(arguments,
        //                    [{ name: "eventArgs", type: Sys.EventArgs}]);
        //        if (e) throw e;
        var handler = this._get_events().getHandler("BetSuccessEvent");
        if (handler) {
            handler(this, null);
        }
    },

    initialize: function () {
        YMGS.Trade.Web.Football.Controls.BetPanel.callBaseMethod(this, 'initialize');
        this.add_betSuccessEvent(BetSuccessEventHandler);
        this.buildBetPanel();
    },

    buildBetPanel: function () {
        //back
        var betBackData = this.get_betBackData();
        if (betBackData == null) {
            $('[id$=backPanle]').html('');
        }
        else {
            betBackData = '[' + betBackData.substring(0, betBackData.length - 1) + ']';
            this._betBackData = $.parseJSON(betBackData).sort(function (a, b) { return a.matchId - b.matchId });
            for (var i = 0, arr; arr = this._betBackData[i++]; ) {
                arr.Odds = parseFloat(arr.Odds).toFixed(2);
            }
            if (this._betBackData.length < 1) {
                $('[id$=backPanle]').html('');
            }
            else {
                this._buildBetBackPanel(this._betBackData);
            }
        }
        //lay
        var betLayData = this.get_betLayData();
        if (betLayData == null) {
            $('[id$=layPanle]').html('');
        }
        else {
            betLayData = '[' + betLayData.substring(0, betLayData.length - 1) + ']';
            this._betLayData = $.parseJSON(betLayData).sort(function (a, b) { return a.matchId - b.matchId });
            for (var i = 0, arr; arr = this._betLayData[i++]; ) {
                arr.Odds = parseFloat(arr.Odds).toFixed(2);
            }
            if (this._betLayData.length < 1) {
                $('[id$=layPanle]').html('');
            }
            else {
                this._buildBetLayPanel(this._betLayData);
            }
        }
        //footer
        if ((betBackData != null && betBackData != '[]') || (betLayData != null && betLayData != '[]')) {
            this._buildBetFooter();
            $('[id$=betFootPanel]').addClass('betfooter');
        }
        else {
            $('[id$=betFootPanel]').html('');
            $('[id$=betFootPanel]').removeClass('betfooter');
        }
    },

    _buildBetBackPanel: function (data) {
        $('[id$=backPanle]').html('');
        this._backPanle.appendChild(this._buildBetBackPanelTbl(data));
    },

    _buildBetLayPanel: function (data) {
        $('[id$=layPanle]').html('');
        this._layPanle.appendChild(this._buildBetLayPanelTbl(data));
    },

    _buildBetBackPanelTbl: function (data) {
        var tbl = document.createElement('table');
        $(tbl).prop('class', 'back').prop('cellspacing', '0');
        $(tbl).append(this._buildBetBackPanelThead());
        $(tbl).append(this._buildBetBackPanelTbody(data));
        return tbl;
    },

    _buildBetLayPanelTbl: function (data) {
        var tbl = document.createElement('table');
        $(tbl).prop('class', 'lay').prop('cellspacing', '0');
        $(tbl).append(this._buildBetLayPanelThead());
        $(tbl).append(this._buildBetLayPanelTbody(data));
        return tbl;
    },

    _buildBetBackPanelThead: function () {
        var thead = document.createElement('thead'),
        tr = document.createElement('tr'),
        thBet = document.createElement('th'),
        thOdds = document.createElement('th'),
        thStake = document.createElement('th'),
        thProfit = document.createElement('th'),
        span = document.createElement('span');
        $(span).text(this._betPanelUIText.BackBetFor);
        $(thBet).prop('scope', 'col').prop('colspan', '2').prop('class', 'runner').append($(span));
        $(thOdds).prop('scope', 'col').prop('class', 'odds').text(this._betPanelUIText.Odds);
        $(thStake).prop('scope', 'col').prop('class', 'stake').text(this._betPanelUIText.Stake);
        $(thProfit).prop('scope', 'col').prop('class', 'profit').text(this._betPanelUIText.Profit);
        $(tr).append($(thBet)).append($(thOdds)).append($(thStake)).append($(thProfit));
        $(thead).append($(tr));
        return thead;
    },

    _buildBetLayPanelThead: function () {
        var thead = document.createElement('thead'),
        tr = document.createElement('tr'),
        thBet = document.createElement('th'),
        thOdds = document.createElement('th'),
        thStake = document.createElement('th'),
        thProfit = document.createElement('th'),
        span = document.createElement('span');
        $(span).text(this._betPanelUIText.LayBetAgainst);
        $(thBet).prop('scope', 'col').prop('colspan', '2').prop('class', 'runner').append($(span));
        $(thOdds).prop('scope', 'col').prop('class', 'odds').text(this._betPanelUIText.BackerOdds);
        $(thStake).prop('scope', 'col').prop('class', 'stake').text(this._betPanelUIText.BackerStake);
        $(thProfit).prop('scope', 'col').prop('class', 'profit').text(this._betPanelUIText.YourLiability);
        $(tr).append($(thBet)).append($(thOdds)).append($(thStake)).append($(thProfit));
        $(thead).append($(tr));
        return thead;
    },

    _buildBetBackPanelTbody: function (data) {
        var tbody = document.createElement('tbody'),
        tempMatchName;

        for (var i = 0, arr; arr = data[i++]; ) {
            if (tempMatchName != arr.matchName) {
                tempMatchName = arr.matchName;
                //Match name row
                var trMatch = document.createElement('tr'),
                thMatch = document.createElement('th'),
                spanMatch = document.createElement('span'),
                matchName = this._language == 1 ? arr.matchName : arr.matchNameEn;
                $(spanMatch).prop('class', 'event-name').text(matchName);
                $(thMatch).prop('colspan', '5').append($(spanMatch));
                $(trMatch).append($(thMatch))
                $(tbody).append($(trMatch));
            }
            //Market row
            var trMarket = document.createElement('tr'),
            tdDel = document.createElement('td'),
            btnDel = document.createElement('input'),
            tdRunner = document.createElement('td'),
            spanTeam = document.createElement('span'),
            spanMarket = document.createElement('span'),
            tdOdds = document.createElement('td'),
            spanOdds = document.createElement('span'),
            textOdds = document.createElement('input'),
            btnUp = document.createElement('button'),
            btnDown = document.createElement('button'),
            tdStake = document.createElement('td'),
            spanStake = document.createElement('span'),
            textStake = document.createElement('input'),
            tdProfit = document.createElement('td'),
            marketName = this._language == 1 ? arr.marketName : arr.marketNameEn,
            marketTmpName = this._language == 1 ? arr.marketTmpName : arr.marketTmpNameEn,
            //matchAmouts = arr.matchAmounts == null ? getDefaultMinAmount() : (parseFloat(arr.matchAmounts) < getDefaultMinAmount() ? getDefaultMinAmount() : parseFloat(arr.matchAmounts)),
            //odds = parseFloat(arr.Odds).toFixed(2) < getDefaultOdds() ? 2.00 : parseFloat(arr.Odds).toFixed(2),
            profit = ((arr.Odds - 1) * arr.matchAmounts).toFixed(2),
            that = this;
            //Delete
            $(btnDel).prop('type', 'button').prop('value', 'remove').attr('delId', i - 1);
            $(btnDel).click(function () {
                var delRow = jsonToString(data[$(this).attr('delId')]) + ',';
                var backCookie = GetCookie('Back').replace(delRow, '');
                SetCookie('Back', backCookie, 1);
                that.buildBetPanel();
            });
            $(tdDel).prop('class', 'delete').append($(btnDel));
            //Market
            $(spanTeam).prop('class', 'team-name').text(marketName);
            $(spanMarket).prop('class', 'market-name').text(marketTmpName);
            $(tdRunner).prop('class', 'runner').append($(spanTeam)).append($(spanMarket));
            //Odds
            $(textOdds).prop('class', 'text odds').prop('type', 'text').prop('maxlength', '8').val(arr.Odds).attr('editId', i - 1);
            $(textOdds).focusout(function () {
                var editRow = data[$(this).attr('editId')];
                OddsChange($(this), editRow, 'Back');
                return false;
            });
            $(btnUp).prop('class', 'bf-spinner-increment').prop('title', 'Increment').text('Increment').attr('editId', i - 1);
            $(btnUp).click(function () {
                var obj = $(this).prev(),
                odds = parseFloat(obj.val()) + 1,
                editRow = data[$(this).attr('editId')];
                if (odds > getDefaultMaxOdds())
                    odds = getDefaultMaxOdds();
                obj.val(odds);
                OddsChange(obj, editRow, 'Back');
                return false;
            });
            $(btnDown).prop('class', 'bf-spinner-decrement').prop('title', 'Decrement').text('Decrement').attr('editId', i - 1);
            $(btnDown).click(function () {
                var obj = $(this).prev().prev(),
                odds = parseFloat(obj.val()) - 1,
                editRow = data[$(this).attr('editId')];
                if (odds < getDefaultOdds())
                    odds = getDefaultOdds();
                obj.val(odds);
                OddsChange(obj, editRow, 'Back');
                return false;
            });
            $(spanOdds).prop('class', 'bf-spinner bf-spinner-content').append($(textOdds)).append($(btnUp)).append($(btnDown));
            $(tdOdds).append($(spanOdds));
            //Stake
            $(textStake).prop('class', 'text stake numeric').prop('type', 'text').prop('maxlength', '7').prop('max', '9999999').prop('min', '50').val(arr.matchAmounts).attr('editId', i - 1);
            $(textStake).focusout(function () {
                var editRow = data[$(this).attr('editId')];
                OddsChange($(this).parent().parent().prev().find('input'), editRow, 'Back');
                return false;
            });
            $(spanStake).prop('class', 'stake').append($(textStake));
            $(tdStake).prop('class', 'stake-cell').append($(spanStake));
            //Profit
            $(tdProfit).prop('class', 'profit').text(profit);

            $(trMarket).prop('class', 'bet').append($(tdDel)).append($(tdRunner)).append($(tdOdds)).append($(tdStake)).append($(tdProfit));
            $(tbody).append($(trMarket))
        }
        return tbody;
    },

    _buildBetLayPanelTbody: function (data) {
        var tbody = document.createElement('tbody'),
        tempMatchName;
        for (var i = 0, arr; arr = data[i++]; ) {
            if (tempMatchName != arr.matchName) {
                tempMatchName = arr.matchName;
                //Match name row
                var trMatch = document.createElement('tr'),
                thMatch = document.createElement('th'),
                spanMatch = document.createElement('span'),
                matchName = this._language == 1 ? arr.matchName : arr.matchNameEn;
                $(spanMatch).prop('class', 'event-name').text(matchName);
                $(thMatch).prop('colspan', '5').append($(spanMatch));
                $(trMatch).append($(thMatch))
                $(tbody).append($(trMatch));
            }
            //Market row
            var trMarket = document.createElement('tr'),
            tdDel = document.createElement('td'),
            btnDel = document.createElement('input'),
            tdRunner = document.createElement('td'),
            spanTeam = document.createElement('span'),
            spanMarket = document.createElement('span'),
            tdOdds = document.createElement('td'),
            spanOdds = document.createElement('span'),
            textOdds = document.createElement('input'),
            btnUp = document.createElement('button'),
            btnDown = document.createElement('button'),
            tdStake = document.createElement('td'),
            spanStake = document.createElement('span'),
            textStake = document.createElement('input'),
            tdProfit = document.createElement('td'),
            marketName = this._language == 1 ? arr.marketName : arr.marketNameEn,
            marketTmpName = this._language == 1 ? arr.marketTmpName : arr.marketTmpNameEn,
            //matchAmouts = arr.matchAmounts == null ? getDefaultMinAmount() : (parseFloat(arr.matchAmounts) < getDefaultMinAmount() ? getDefaultMinAmount() : parseFloat(arr.matchAmounts)),
            //odds = parseFloat(arr.Odds).toFixed(2) < getDefaultOdds() ? 2.00 : parseFloat(arr.Odds).toFixed(2),
            profit = ((arr.Odds - 1) * arr.matchAmounts).toFixed(2),
            that = this;
            //Delete
            $(btnDel).prop('type', 'button').prop('value', 'remove').attr('delId', i - 1);
            $(btnDel).click(function () {
                var delRow = jsonToString(data[$(this).attr('delId')]) + ',';
                var layCookie = GetCookie('Lay').replace(delRow, '');
                SetCookie('Lay', layCookie, 1);
                that.buildBetPanel();
            });
            $(tdDel).prop('class', 'delete').append($(btnDel));
            //Market
            $(spanTeam).prop('class', 'team-name').text(marketName);
            $(spanMarket).prop('class', 'market-name').text(marketTmpName);
            $(tdRunner).prop('class', 'runner').append($(spanTeam)).append($(spanMarket));
            //Odds
            $(textOdds).prop('class', 'text odds').prop('type', 'text').prop('maxlength', '8').val(arr.Odds).attr('editId', i - 1);
            $(textOdds).focusout(function () {
                var editRow = data[$(this).attr('editId')];
                OddsChange($(this), editRow, 'Lay');
                return false;
            });
            $(btnUp).prop('class', 'bf-spinner-increment').prop('title', 'Increment').text('Increment').attr('editId', i - 1);
            $(btnUp).click(function () {
                var obj = $(this).prev(),
                odds = parseFloat(obj.val()) + 1,
                editRow = data[$(this).attr('editId')];
                if (odds > getDefaultMaxOdds())
                    odds = getDefaultMaxOdds();
                obj.val(odds);
                OddsChange(obj, editRow, 'Lay');
                return false;
            });
            $(btnDown).prop('class', 'bf-spinner-decrement').prop('title', 'Decrement').text('Decrement').attr('editId', i - 1);
            $(btnDown).click(function () {
                var obj = $(this).prev().prev(),
                odds = parseFloat(obj.val()) - 1,
                editRow = data[$(this).attr('editId')];
                if (odds < getDefaultOdds())
                    odds = getDefaultOdds();
                obj.val(odds);
                OddsChange(obj, editRow, 'Lay');
                return false;
            });
            $(spanOdds).prop('class', 'bf-spinner bf-spinner-content').append($(textOdds)).append($(btnUp)).append($(btnDown));
            $(tdOdds).append($(spanOdds));
            //Stake
            $(textStake).prop('class', 'text stake numeric').prop('type', 'text').prop('maxlength', '7').prop('max', '9999999').prop('min', '2').val(arr.matchAmounts).attr('editId', i - 1);
            $(textStake).focusout(function () {
                var editRow = data[$(this).attr('editId')];
                OddsChange($(this).parent().parent().prev().find('input'), editRow, 'Lay');
                return false;
            });
            $(spanStake).prop('class', 'stake').append($(textStake));
            $(tdStake).prop('class', 'stake-cell').append($(spanStake));
            //Profit
            $(tdProfit).prop('class', 'profit').text(profit);

            $(trMarket).prop('class', 'bet').append($(tdDel)).append($(tdRunner)).append($(tdOdds)).append($(tdStake)).append($(tdProfit));
            $(tbody).append($(trMarket))
        }
        return tbody;
    },

    _buildBetFooter: function () {
        $('[id$=betFootPanel]').html('');
        this._betFootPanel.appendChild(this._buildBetFooterTotal());
        this._betFootPanel.appendChild(this._buildBetFooterButton());
    },

    _buildBetFooterTotal: function () {
        var div = document.createElement('div'),
        divLiability = document.createElement('div')
        span = document.createElement('span');
        $(span).text(getLiability()).prop('class', 'total-liability');
        $(divLiability).text(this._betPanelUIText.Liability).prop('class', 'total-liability').append($(span));
        $(div).prop('class', 'totals').append($(divLiability));
        return div;
    },

    _buildBetFooterButton: function () {
        var div = document.createElement('div'),
        ul = document.createElement('ul'),
        liCancle = document.createElement('li'),
        liBet = document.createElement('li'),
        inputCancle = document.createElement('input'),
        inputBet = document.createElement('input'),
        that = this;

        $(inputBet).prop('id', 'betInput').prop('type', 'button').prop('value', this._betPanelUIText.PlaceBets).prop('class', 'cta cta-primary cta-disabled');
        $(inputBet).click(function () {
            var result = window.confirm(that._betPanelUIText.ConfrimBet);
            if (result == true) {
                $("#betInput").attr("disabled", true);
                setTimeout(function () {
                    $("#betInput").removeAttr("disabled");
                }, 1000);
                YMGS.Trade.Web.Services.BetService.PlaceBet(that._language, that._betBackData, that._betLayData, that._placeBetHandler, that._placeBetHandlerError, null);
            }
        });
        $(liBet).append($(inputBet));
        $(inputCancle).prop('type', 'button').prop('value', this._betPanelUIText.CancelAllSelection).prop('class', 'cta cta-minor cta-secondary');
        $(inputCancle).click(function () {
            DelCookie('Back');
            DelCookie('Lay');
            that.buildBetPanel();
        });
        $(liCancle).prop('class', 'minor').append($(inputCancle));
        $(ul).append($(liBet)).append($(liCancle));
        $(div).prop('class', 'buttons').append($(ul));
        return div;
    },

    _processPlaceBetHandler: function (serviceOperationResult) {
        if (serviceOperationResult.IsSucceeded) {
            $("#betInput").attr("disabled", true);
            var succeedMessage = serviceOperationResult.Value;
            alert(succeedMessage);
            DelCookie('Back');
            DelCookie('Lay');
            this._betBackData = null;
            this._betLayData = null;
            this.buildBetPanel();
            this.OnBetSuccessEvent();
        }
        else {
            alert(serviceOperationResult.Message);
        }
    },

    _processPlaceBetHandlerError: function (error) {
        var errorMessage = error.get_message();
        alert(errorMessage);
    },

    dispose: function () {
        YMGS.Trade.Web.Football.Controls.BetPanel.callBaseMethod(this, 'dispose');
        this.remove_betSuccessEvent(BetSuccessEventHandler);
    }
};
YMGS.Trade.Web.Football.Controls.BetPanel.registerClass("YMGS.Trade.Web.Football.Controls.BetPanel", Sys.UI.Control);

GenerateProps(YMGS.Trade.Web.Football.Controls.BetPanel, [
    'language',
    'betPanelUIText',
]);


if (typeof (Sys) !== 'undefined') {
    Sys.Application.notifyScriptLoaded();
}
