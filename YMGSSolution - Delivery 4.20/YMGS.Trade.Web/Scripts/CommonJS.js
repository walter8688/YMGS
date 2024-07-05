$(function () {
    $(".logusername,.searchTerms").focus(function () {
        $(this).addClass("icss");
        if ($(this).val() == this.defaultValue) {
            $(this).val("");
        }
    }).blur(function () {
        $(this).removeClass("icss");
        if ($(this).val() == '') {
            $(this).val(this.defaultValue);
        }
    });

});

function redirect(ui) {
    if (typeof (ui.item) == "undefined")
        return
    document.location.href = "Default.aspx";
}

function reloadcode(pic) {
    document.getElementById(pic).src = 'ValidateCode.aspx?' + Math.random();
}

function checkbutton(buttonId,checkbox) {
    
    
    if (checkbox.checked)
        document.getElementById(buttonId).disabled = "";
    else
        document.getElementById(buttonId).disabled = "disabled";
}

//解决UpdatePanel和jQuery冲突的问题
function load() {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
}

//验证电子邮箱格式是否正确
function isEmail(strEmail) {
    if (strEmail.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1)
        return true; return false;
}
//验证是否是正整数
function isPositiveInteger(s) {
    var pattern = /^[0-9]+$/;
    if (pattern.exec(s))
        return true; return false;
}
//非空检验
function chkIsNull(objID) {
    if ($.trim($('[id$=' + objID + ']').val()) == "") {
        return true;
    }
    return false;
}

//验证登陆用户名 由6-14位长度的字母、数字、下划线组成，不可有空格。
function validateLoginName(obj) {
    var reg = /^\w{6,14}$/;
    var objVal = $('[id$=' + obj + ']').val();;
    if (reg.exec(objVal))
        return true;return false;
}
//验证密码 由6-14字符构成，包括字母、数字、下划线。不能包含用户名、姓名、出生日期。
function validatePassword(obj1,obj2) {
    var reg = /^\w{6,14}$/;
    var obj1Val = $('[id$=' + obj1 + ']').val();
    var obj2Val = $('[id$=' + obj2 + ']').val();
    if (reg.exec(obj1Val) && obj1Val.indexOf(obj2Val) < 0)
        return true;return false;
}

function GenerateProps(objectType, props) {
    for (var p in props) {
        var propName = '_' + props[p];
        objectType.prototype[propName] = null;

        eval('objectType.prototype[\'get' + propName + '\'] = function() { return this[\'' + propName + '\']; }');
        eval('objectType.prototype[\'set' + propName + '\'] = function(value) { this[\'' + propName + '\'] = value; }');
    }
}

function getDefaultOdds() {
    return 1.01;
}

function getDefaultMaxOdds() {
    return 3000;
}

function getDefaultMaxAmount() {
    return 200000000;
}

function getDefaultMinAmount() {
    return 50;
}
/*
function Achangestakeprofit(source) {
    var defaultOdds = getDefaultOdds();
    var stake = $(source).val();
    var profit = $(source).parent().siblings().find(".backprofit");
    if (stake == "")
        stake = 1;
    if (isNaN(stake)) {
        stake = 1;
    }
    else {
        stake = parseFloat(stake).toFixed(2);
        if (stake < getDefaultMinAmount()) {
            stake = getDefaultMinAmount();
        }
        if (stake > getDefaultMaxAmount()) {
            stake = getDefaultMaxAmount();
        }
    }
    $(source).val(stake);
    // alert(stake);
    var odds = $(source).parent().siblings().find(".odds").val();
    if (isNaN(odds)) {
        odds = defaultOdds;
    }
    else {
        odds = parseFloat(odds).toFixed(2);
        if (odds < defaultOdds) {
            odds = defaultOdds;
        }
        if (odds > getDefaultMaxOdds()) {
            odds = getDefaultMaxOdds();
        }
    }
    $(source).parent().siblings().find(".odds").val(odds);
    // alert($(source).parent().siblings().find(".MARKET_ID").text());
    EditBackEntity($(source).parent().siblings().find(".MARKET_ID").text(), odds, stake, $(source).parent().siblings().find(".MATCHTYPE").text());
    var p = stake * (odds - 1);

    profit.text(p.toFixed(2));
    sumProfit();

}
function changestakeprofit(source) {
    var defaultOdds = getDefaultOdds();
    var stake = $(source).val();
    var profit = $(source).parent().siblings().find(".layprofit");
    if (stake == "")
        stake = 1;
    if (isNaN(stake)) {
        stake = 1;
    }
    else {
        stake = parseFloat(stake).toFixed(2);
        if (stake < getDefaultMinAmount()) {
            stake = getDefaultMinAmount();
        }
        if (stake > getDefaultMaxAmount()) {
            stake = getDefaultMaxAmount();
        }
    }
    $(source).val(stake);
    // alert(stake);
    var odds = $(source).parent().siblings().find(".odds").val();
    if (isNaN(odds)) {
        odds = defaultOdds;
    }
    else {
        odds = parseFloat(odds).toFixed(2);
        if (odds < defaultOdds) {
            odds = defaultOdds;
        }
        if (odds > getDefaultMaxOdds()) {
            odds = getDefaultMaxOdds();
        }
    }

    $(source).parent().siblings().find(".odds").val(odds);

    EditLayEntity($(source).parent().siblings().find(".MARKET_ID").text(), odds, stake, $(source).parent().siblings().find(".MATCHTYPE").text());
    var p = stake * (odds - 1);

    profit.text(p.toFixed(2));
    sumProfit();

}

function changeprofit(source) {
    defaultOdds = getDefaultOdds();
    //var tempodds = 0;
    var stake = 0;
    var profit = $(source).closest(".oddstd").siblings().find(".layprofit");
    var odds = $(source).val();

    if (isNaN(odds)) {
        odds = defaultOdds;
    }
    else {
        odds = parseFloat(odds).toFixed(2);
        if (odds < defaultOdds) {
            odds = defaultOdds;
        }
        if (odds > getDefaultMaxOdds()) {
            odds = getDefaultMaxOdds();
        }
    }

    $(source).val(odds);
    // alert($(source).parent().siblings().find(".stake").size());
    stake = $(source).closest(".oddstd").siblings().find(".stake").val();
    if (stake == "")
        stake = 1;
    if (isNaN(stake)) {
        stake = 0;
    }
    else {
        stake = parseFloat(stake).toFixed(2);
        if (stake < getDefaultMinAmount()) {
            stake = getDefaultMinAmount();
        }
        if (stake > getDefaultMaxAmount()) {
            stake = getDefaultMaxAmount();
        }
    }
    //  alert($(source).closest(".oddstd").siblings().find(".MARKET_ID").text());
    $(source).closest(".oddstd").siblings().find(".stake").val(stake);
    var p = stake * (odds - 1);
    EditLayEntity($(source).closest(".oddstd").siblings().find(".MARKET_ID").text(), odds, stake, $(source).closest(".oddstd").siblings().find(".MATCHTYPE").text());
    profit.text(p.toFixed(2));
    sumProfit();
}

function Checkodds(source, args) {
    var defaultOdds = getDefaultOdds();
    var odds = $(source).siblings(".odds").val();
    //  var stake = $(source).siblings(".stake").val();
    //  var profit = $(source).siblings(".profit").text();

    if (isNaN(odds) && odds < defaultOdds && odds > getDefaultMaxOdds()) {
        args.IsValid = false;
    }
    else {
        args.IsValid = true;
    }

}
function Checkbackstake(source, args) {
    var stake = $(source).siblings(".backstake").val();
    if (isNaN(stake)) {
        args.IsValid = false;
    }
    else {
        if (parseFloat(stake) < getDefaultMinAmount() || parseFloat(stake) > getDefaultMaxAmount()) {
            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }
    }

}
function Checkstake(source, args) {
    var stake = $(source).siblings(".stake").val();
    if (isNaN(stake)) {
        args.IsValid = false;
    }
    else {
        if (parseFloat(stake) < getDefaultMinAmount() || parseFloat(stake) > getDefaultMaxAmount()) {
            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }
    }

}


function Achangeprofit(source) {
    defaultOdds = getDefaultOdds();
    //var tempodds = 0;
    var stake = 0;
    var profit = $(source).closest(".oddstd").siblings().find(".backprofit");
    var odds = $(source).val();

    if (isNaN(odds)) {
        odds = defaultOdds;
    }
    else {
        odds = parseFloat(odds).toFixed(2);
        if (odds < defaultOdds) {
            odds = defaultOdds;
        }
        if (odds > getDefaultMaxOdds()) {
            odds = getDefaultMaxOdds();
        }
    }

    $(source).val(odds);
    // alert($(source).parent().siblings().find(".stake").size());
    stake = $(source).closest(".oddstd").siblings().find(".backstake").val();
    if (stake == "")
        stake = 1;
    if (isNaN(stake)) {
        stake = 0;
    }
    else {
        stake = parseFloat(stake).toFixed(2);
        if (stake < getDefaultMinAmount()) {
            stake = getDefaultMinAmount();
        }
        if (stake > getDefaultMaxAmount()) {
            stake = getDefaultMaxAmount();
        }
    }

    $(source).closest(".oddstd").siblings().find(".backstake").val(stake);
    var p = stake * (odds - 1);
    EditBackEntity($(source).closest(".oddstd").siblings().find(".MARKET_ID").text(), odds, stake, $(source).closest(".oddstd").siblings().find(".MATCHTYPE").text());
    profit.text(p.toFixed(2));
    sumProfit();
}

function EditBackEntity(marketid, odds, amount, matchtype) {
    //            var role = new YMGS.Data.Entity.MatchMarcketInfo();
    //            role.MARKET_ID = parseInt(marketid, 10);
    //            role.odds = odds;
    //            role.AMOUNTS = amount;
    //            role.MATCHTYPE = matchtype;
    //            PageMethods.EditBack(role, ontestmethodSeccuss);
    return false;
}
function EditLayEntity(marketid, odds, amount, matchtype) {
    //            var role = new YMGS.Data.Entity.MatchMarcketInfo();
    //            role.MARKET_ID = parseInt(marketid, 10);
    //            role.odds = odds;
    //            role.AMOUNTS = amount;
    //            role.MATCHTYPE = matchtype;
    //            PageMethods.EditLay(role, ontestmethodSeccuss);
    return false;
}*/
function ontestmethodSeccuss(result, context, methodName) {

}
//过滤非法字符
var filterUnSaveStr = function (obj) {
    var event = arguments[0] || window.event;
    if (event.keyCode >= 0 && event.keyCode <= 64)
        return;
    var objVal = $(obj).val();
    if (objVal.indexOf('<') != 0 || objVal.indexOf('>') != 0) {
        $(obj).val(objVal.replace('<', '').replace('>', ''));
    }
}
//Dom loaded
$(function () {
    var pageId = $('[id$=hdfPageId]').val();
    if (pageId == "1") {
        $('[id$=main-wrapper]').prop('style')['margin'] = '0 339px 0 0';
    }

    var objs = $('body').parent().parent();
    if ($(objs).width() > $('body').width() && $('body').width() >= window.screen.width - 30) {
        $('body').attr('style', 'overflow-x:hidden;')
    }
})
//document宽度超过2000？？
$(window).resize(function () {
    var objs = $('body').parent().parent();
    if ($(objs).width() > $('body').width() && $('body').width() >= window.screen.width - 30) {
        $('body').attr('style', 'overflow-x:hidden;')
    }
    else {
        $('body').attr('style', 'overflow-x:scroll;')
    }
});
//BetPanel更新事件
function OddsChange(sender, editRow, betType) {
    if (typeof sender === 'undefined')
        return false;
    if (sender == null)
        return false;
    if (typeof editRow === 'undefined')
        return false;
    if (editRow == null)
        return false;
    var oddsObj = $(sender),
        amountsObj = $(sender).parent().parent().next().find('input'),
        profitObj = $(sender).parent().parent().next().next(),
        odds = oddsObj.val(),
        amounts = amountsObj.val(),
        profit, editRowStr, newRowStr, newRow, betCookie;
    
    if (!$.isNumeric(odds)) {
        oddsObj.val(getDefaultOdds());
    }
    odds = parseFloat(odds).toFixed(2);
    if (odds < getDefaultOdds()) {
        oddsObj.val(getDefaultOdds());
    }
    if (odds > getDefaultMaxOdds()) {
        oddsObj.val(getDefaultMaxOdds());
    }
    if (!$.isNumeric(amounts)) {
        amountsObj.val(getDefaultMinAmount());
    }
    amounts = parseFloat(amounts).toFixed(2);
    if (amounts < getDefaultMinAmount()) {
        amountsObj.val(getDefaultMinAmount());
    }
    if (amounts > getDefaultMaxAmount()) {
        amountsObj.val(getDefaultMaxAmount());
    }
    odds = parseFloat(oddsObj.val()).toFixed(2);
    amounts = parseFloat(amountsObj.val()).toFixed(2);
    oddsObj.val(odds);
    amountsObj.val(amounts);
    profit = (parseFloat($(oddsObj).val()) - 1) * amountsObj.val();
    profitObj.text(profit.toFixed(2));
    //update cookie
    editRowStr = jsonToString(editRow) + ',';

    newRow = editRow;
    newRow.Odds = oddsObj.val();
    newRow.matchAmounts = amountsObj.val();
    newRowStr = jsonToString(newRow) + ',';

    //replace cookie
    betCookie = GetCookie(betType).replace(editRowStr, newRowStr);
    SetCookie(betType, betCookie, 1);
    $('.total-liability span').text(getLiability());
}
//获取风险
function getLiability() {
    var betBackData = GetCookie('Back'),
    betLayData = GetCookie('Lay'),
    totalLiability = 0.00,
    matchAmounts,
    matchOdds;
    if (betBackData != null) {
        betBackData = '[' + betBackData.substring(0, betBackData.length - 1) + ']';
        betBackData = $.parseJSON(betBackData);
        for (var i = 0, arr; arr = betBackData[i++]; ) {
            matchAmounts = arr.matchAmounts == null ? getDefaultMinAmount() : (parseFloat(arr.matchAmounts) < getDefaultMinAmount() ? getDefaultMinAmount() : parseFloat(arr.matchAmounts));
            totalLiability += parseFloat(matchAmounts);
        }
    }

    if (betLayData != null) {
        betLayData = '[' + betLayData.substring(0, betLayData.length - 1) + ']';
        betLayData = $.parseJSON(betLayData);
        for (var i = 0, arr; arr = betLayData[i++]; ) {
            matchOdds = parseFloat(arr.Odds).toFixed(2) < getDefaultOdds() ? 2.00 : parseFloat(arr.Odds).toFixed(2);
            matchAmounts = arr.matchAmounts == null ? getDefaultMinAmount() : (parseFloat(arr.matchAmounts) < getDefaultMinAmount() ? getDefaultMinAmount() : parseFloat(arr.matchAmounts));
            totalLiability += parseFloat((matchOdds - 1) * matchAmounts);
        }
    }
    return totalLiability.toFixed(2);
}

//handle cookie
function SetCookie(objName, objValue, objHours) {
    var str = objName + "=" + escape(objValue);
    if (objHours > 0) {
        var date = new Date();
        var ms = objHours * 3600 * 1000;
        date.setTime(date.getTime() + ms);
        str += "; expires=" + date.toGMTString();
    }
    document.cookie = str;
}
//获取Cookie
function GetCookie(name) {
    var result = null;
    var myCookie = document.cookie + ';';
    var searchName = name + '=';
    var startOfCookie = myCookie.indexOf(searchName);
    var endOfCookie;
    if (startOfCookie != -1) {
        startOfCookie += searchName.length;
        endOfCookie = myCookie.indexOf(';', startOfCookie);
        result = unescape(myCookie.substring(startOfCookie, endOfCookie));
    }
    return result;
}

//删除Cookie
function DelCookie(name) {
    var ThreeDays = 3 * 24 * 60 * 60 * 1000;
    var expDate = new Date();
    expDate.setTime(expDate.getTime() - ThreeDays);
    document.cookie = name + '=;expires=' + expDate.toGMTString();
}
//获取下注的Cookie格式
function GetBetCookieValue(matchId, matchType, marketId, marketTmpId, Odds, matchAmounts,
matchName, matchNameEn, marketName, marketNameEn, marketTmpName, marketTmpNameEn) {
    matchAmounts = (matchAmounts == null || matchAmounts == '') ? getDefaultMinAmount() : (parseFloat(matchAmounts) < getDefaultMinAmount() ? getDefaultMinAmount() : parseFloat(matchAmounts));
    Odds = (Odds == null || Odds == '') ? parseFloat(2).toFixed(2) : (parseFloat(Odds).toFixed(2) < getDefaultOdds() ? parseFloat(2).toFixed(2) : parseFloat(Odds).toFixed(2));
    return '{"matchId":' + matchId + ',' + '"matchType":' + matchType + ',' + '"marketId":' + marketId + ',' +
    '"marketTmpId":' + marketTmpId + ',' + '"Odds":' + '"' + Odds + '"' + ',' + '"matchAmounts":' + matchAmounts + ',' +
    '"matchName":' + '"' + matchName + '"' + ',' + '"matchNameEn":' + '"' + matchNameEn + '"' + ',' + '"marketName":' + '"' + marketName + '"' + ',' +
    '"marketNameEn":' + '"' + marketNameEn + '"' + ',' + '"marketTmpName":' + '"' + marketTmpName + '"' + ',' + '"marketTmpNameEn":' + '"' + marketTmpNameEn + '"' + '},';
}
//存储投注的Cookie
function SetBetBackCookie(matchId, matchType, marketId, marketTmpId, Odds, matchAmounts, matchName, matchNameEn, marketName, marketNameEn, marketTmpName, marketTmpNameEn) {
    var cookieValue = GetBetCookieValue(matchId, matchType, marketId, marketTmpId, Odds, matchAmounts, matchName, matchNameEn, marketName, marketNameEn, marketTmpName, marketTmpNameEn);
    var curCookieValue = GetCookie('Back');
    if (curCookieValue !== null) {
        if (curCookieValue.indexOf(cookieValue) > -1) {
            cookieValue = curCookieValue.replace(cookieValue, '');
        }
        else {
            cookieValue = curCookieValue + cookieValue;
        }
    }
    else {
        cookieValue = cookieValue;
    }
    SetCookie('Back', cookieValue, null);
}
//存储受注的Cookie
function SetBetLayCookie(matchId, matchType, marketId, marketTmpId, Odds, matchAmounts, matchName, matchNameEn, marketName, marketNameEn, marketTmpName, marketTmpNameEn) {
    var cookieValue = GetBetCookieValue(matchId, matchType, marketId, marketTmpId, Odds, matchAmounts, matchName, matchNameEn, marketName, marketNameEn, marketTmpName, marketTmpNameEn);
    var curCookieValue = GetCookie('Lay');
    if (curCookieValue != null) {
        if (curCookieValue.indexOf(cookieValue) > -1) {
            cookieValue = curCookieValue.replace(cookieValue, '');
        }
        else {
            cookieValue = curCookieValue + cookieValue;
        }
    }
    else {
        cookieValue = cookieValue;
    }
    SetCookie('Lay', cookieValue, 1);
}
//Json格式转换成String
function jsonToString(obj) {
    var THIS = this;
    switch (typeof (obj)) {
        case 'string':
            return '"' + obj.replace(/(["\\])/g, '\\$1') + '"';
        case 'array':
            return '[' + obj.map(THIS.jsonToString).join(',') + ']';
        case 'object':
            if (obj instanceof Array) {
                var strArr = [];
                var len = obj.length;
                for (var i = 0; i < len; i++) {
                    strArr.push(THIS.jsonToString(obj[i]));
                }
                return '[' + strArr.join(',') + ']';
            } else if (obj == null) {
                return 'null';

            } else {
                var string = [];
                for (var property in obj) string.push(THIS.jsonToString(property) + ':' + THIS.jsonToString(obj[property]));
                return '{' + string.join(',') + '}';
            }
        case 'number':
            return obj;
        case false:
            return obj;
    }
}
