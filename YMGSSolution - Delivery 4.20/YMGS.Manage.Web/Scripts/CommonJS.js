$(function () {
    function leftfor() {
        var e = document.body.offsetHeight;
        var a = e / 2 - 41 + "px";
        $("#leftpre").css("top", a);
    }

    setInterval(leftfor, 50);
    $("#sosocont").hide();
    $("#sobtn").click(function () {
        $("#sosocont").toggle();
    });
});

function leftgo() {

    document.getElementById("leftpre").className = "shoubin01";
    document.getElementById("leftcont").style.display = "none";
    document.getElementById("lrdiv").className = "lrdiv01";
    document.getElementById("conright").className = "contrightcssy01";
}



function rightgo() {

    document.getElementById("leftpre").className = "shoubin";
    document.getElementById("leftcont").style.display = "block";
    document.getElementById("lrdiv").className = "lrdiv";
    document.getElementById("conright").className = "contrightcssy";

}
function sgo() {
    if (document.getElementById("leftcont").style.display == "block" || document.getElementById("leftcont").style.display == "") {

        leftgo();
    }
    else {
        rightgo();
    }
}
//////////////////////////页面上可公用的方法//////////////////////////
//显示确认提示
function showConfirm(msg) {
    return window.confirm(msg);
}
//非空检验
function chkIsNull(objID) {
    if ($.trim($('[id$=' + objID + ']').val()) == "") {
        return true;
    }
    return false;
}

//是否数字
function isDigit(s) {
    var patrn = /^[0-9]{1,20}$/;
    if (patrn.exec(s))
        return true; return false;
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
        return true;return false;
}

function IsNumber(string,sign)
{
    var number;
    if (string==null) return false;
    if ((sign!=null) && (sign!="-") && (sign!="+"))
    {
        alert("参数出错");
        return false;
    }
    number = new Number(string);
    if (isNaN(number)) {
        return false;
    }
    else if ((sign == null) || (sign == "-" && number < 0) || (sign == "+" && number > 0)) {
        return true;
    }
    else {
        return false;
    }
}

function checkUserFund(fund) {
    if (!$.isNumeric(fund)) {
        return false;
    }
    if (fund < 0) {
        return false;
    }
    return true;
}

function isValidDate(dateObj) {
    var temp = dateObj;
    if (!(dateObj.length == 10)) {
        return false;
    }
    var exp = new RegExp("\\d{4}-\\d{2}-\\d{2}");
    if (!exp.test(temp)) {
        return false;
    }
    var month = temp.substring(5, 7);

    if (month < 1 || month > 12) {
        return false;
    }
    var day = temp.substring(8, 10);
    if (day < 1 || day > 31) {
        return false;
    }
    return true;
}

function GenerateProps(objectType, props) {
    for (var p in props) {
        var propName = '_' + props[p];
        objectType.prototype[propName] = null;

        eval('objectType.prototype[\'get' + propName + '\'] = function() { return this[\'' + propName + '\']; }');
        eval('objectType.prototype[\'set' + propName + '\'] = function(value) { this[\'' + propName + '\'] = value; }');
    }
}

//解决UpdatePanel和jQuery冲突的问题
function load() {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
}

Date.prototype.DateAdd = function (strInterval, Number) {
    var dtTmp = this;
    switch (strInterval) {
        case 's': return new Date(Date.parse(dtTmp) + (1000 * Number));
        case 'n': return new Date(Date.parse(dtTmp) + (60000 * Number));
        case 'h': return new Date(Date.parse(dtTmp) + (3600000 * Number));
        case 'd': return new Date(Date.parse(dtTmp) + (86400000 * Number));
        case 'w': return new Date(Date.parse(dtTmp) + ((86400000 * 7) * Number));
        case 'q': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number * 3, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'm': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'y': return new Date((dtTmp.getFullYear() + Number), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
    }
}

String.prototype.toDate = function () {
    var temp = this.toString();
    temp = temp.replace(/-/g, "/");
    var date = new Date(Date.parse(temp));
    return date;
}

function getCurSysDate() {
    var url = '../GameControl/GetServerDateHandler.ashx';
    $.ajax({
        type: 'post',
        url: url,
        success: function (data) {
            $('#lblCurSysDate').text(data)
        }
    })
}

$(function () {
    getCurSysDate();
})


