var globalPGB = null;
function ASPProgressBar(_id, imgUrl, autoRun, preTimeOver, showText) {
    this.id = _id;
    globalPGB = this;
    this.imgUrl = imgUrl;
    this.autoRun = autoRun;
    this.val = 0;
    this.preTimeOver = preTimeOver;
    this.elePBar = document.getElementById(this.id);
    this.elepf = document.getElementById(this.id + "_F");
    this.elept = document.getElementById(this.id + "_T");
    this.elepp = document.getElementById(this.id + "_P");
    this.elepi = document.getElementById(this.id + "_I");
    if (showText == null || showText.length <= 0) {
        this.showText = 'Is running,please wait...';
    }
    else {
        this.showText = showText;
    }
}

ASPProgressBar.prototype.write = function () {
    var str = "";
    str += '<table id="' + this.id + '" border=0  width=100% height=100% style ="display:none">';
    str += '<tr height=100%><td valign=middle align=center id="' + this.id + '_td' + '">';
    str += '<table border=0 >';
    str += '<tr><td>';
    if (this.imgUrl != null && this.imgUrl.length > 0) {
        str += '<img id="' + this.id + '_I" src= "' + this.imgUrl + '" />';
    }
    str += '</td></tr>';
    str += '<tr><td align =left ><span id="' + this.id + '_T" style="FONT-SIZE: 12px;"></span>	</td></tr>';
    str += '<tr height=10>';
    str += '<td style="border-width:1px 1px 1px 1px;border-color:#A0A0A4;border-style:Solid;background-color:White;';
    str += 'padding:1 1 1 1;" height=10 align =left >';
    str += '<div id="' + this.id + '_F"   style="background-color:#0000AA;width:0%;"></div>';
    str += '</td></tr>';
    str += '	<tr><td align=center><span id="' + this.id + '_P" style="FONT-SIZE: 12px;"></span></td></tr>';
    str += '	</table></td></tr></table>';
    document.write(str);
    var oriTable = $("#" + this.id);
    var oriTd = $("#" + this.id + '_td');
    oriTd.css("width", oriTable.width());
    oriTd.css("height", oriTable.height());
    this.elePBar = document.getElementById(this.id);
    this.elepf = document.getElementById(this.id + "_F");
    this.elept = document.getElementById(this.id + "_T");
    this.elepp = document.getElementById(this.id + "_P");
    this.elepi = document.getElementById(this.id + "_I");
}

ASPProgressBar.prototype.show = function () {
    if (ASPProgressBar.Run.ID != null) {
        window.clearTimeout(ASPProgressBar.Run.ID);
    }
    this.elePBar.style.display = "block";
    this.val = 0;
    this.dval = null;
    this.elepf.style.width = this.val + "%";
    if (this.autoRun) {
        this.elept.innerHTML = this.showText + this.elepf.style.width;
        ASPProgressBar.Run(this.id, 100);
    }
    else {
        this.elepp.innerHTML = this.val + "%";
    }
}

ASPProgressBar.prototype.hide = function () {
    this.elePBar.style.display = "none";
}

ASPProgressBar.prototype.hide = function (url) {
    this.elePBar.style.display = "none";
    window.location = url;
}

ASPProgressBar.prototype.setProgress = function (val, text, timeover, imgurl) {
    if (ASPProgressBar.Run.ID != null) {
        window.clearTimeout(ASPProgressBar.Run.ID);
    }
    if (this.dval != null) {
        this.val = this.dval;
        this.elepf.style.width = this.dval + "%";
        this.elepp.innerHTML = this.dval + "%";
    }
    this.dval = val;
    this.elept.innerHTML = text;
    if (timeover == null) {
        timeover = 10;
    }
    this.preTimeOver = timeover;
    if (imgurl != null && imgurl.length > 0) {
        this.elepi.src = imgurl;
    }
    ASPProgressBar.Run(this.id, val);
}

ASPProgressBar.prototype.setShowText = function (showText) {
    this.showText = showText;
}
ASPProgressBar.prototype.setImageUrl = function (imgUrl) {
    this.imgUrl = imgUrl;
    if (imgUrl != null && imgUrl.length > 0) {
        this.elepi.src = imgUrl;
    }
}

ASPProgressBar.Run = function (id, val) {
    var obj = globalPGB;
    if (obj.val == null) {
        obj.val = 0;
    }
    obj.elepf.style.width = obj.val + "%";

    if (obj.autoRun) {
        obj.elept.innerHTML = obj.showText + obj.elepf.style.width;
    }
    else {
        obj.elepp.innerHTML = obj.val + "%";
    }
    if (obj.val < val) {
        obj.val += 1;
        var ps = obj.preTimeOver * 1000 / (val - obj.val);
        ASPProgressBar.Run.ID = window.setTimeout('ASPProgressBar.Run("' + obj.id + '",' + val + ')', ps);
    }
}
