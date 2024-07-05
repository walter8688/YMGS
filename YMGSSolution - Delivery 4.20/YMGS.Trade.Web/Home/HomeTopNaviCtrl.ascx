<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeTopNaviCtrl.ascx.cs" Inherits="YMGS.Trade.Web.Home.HomeTopNaviCtrl" %>
<script type="text/javascript" src="Scripts/jquery.ui.core.js"></script>
<script type="text/javascript" src="Scripts/jquery.ui.widget.js"></script>
<script type="text/javascript" src="Scripts/jquery.ui.position.js"></script>
<script type="text/javascript" src="Scripts/jquery.ui.menu.js"></script>
<script type="text/javascript" src="Scripts/jquery.ui.autocomplete.js"></script>

<div class="homenavone">
    <div class="homelogo">
        <asp:Image ID="Image1" runat="server" ImageUrl="<%$ Resources:GlobalLanguage,logoaddress%>" style="width: 140px; height: 45px" />
    </div>
    <div class="ui-widget">
        <input name="" id="txtFind"   type="text" runat="server" class="searchTerms" 
        value="<%$ Resources:GlobalLanguage,SearchTips%>" onkeyup="filterUnSaveStr(this)" onpaste="filterUnSaveStr(this)" style=" margin-left:200px;" />
        <span class="searchIcon"></span>
        <asp:Button id="btnFind" runat="server" CssClass="homefindbtn" />
    </div><%--请输入您要搜索的内容--%>
</div>
<div class="homenavtwo">
    <asp:LinkButton ID="lbtSports" runat="server" CausesValidation="False"></asp:LinkButton>
    <span></span>
    <asp:LinkButton ID="lblInPlay" runat="server" CausesValidation="False"></asp:LinkButton>
    <span></span>
    <asp:LinkButton ID="lblFootball" runat="server" CausesValidation="False"></asp:LinkButton>
    <span></span>
    <asp:LinkButton ID="lbtEntertainment" runat="server" CausesValidation="False"></asp:LinkButton>
</div>
<script type="text/javascript">
    //HomePage Search
    $(function () {
        $('[id$=txtFind]').autocomplete({
            source: "Public/SearchHandler.ashx?lan=" + $('[id$=languagemark]').val(),
            minLength: 1,
            select: function (event, ui) {
                if (ui.item.id == "") {
                    return false;
                }
                var curUrl = window.location.href;
                if (curUrl.indexOf('?') != -1) {
                    curUrl = curUrl.substring(0, curUrl.indexOf('?'));
                }
                var redirectUrl = curUrl + ui.item.id;
                // redirect to select item's page
                window.location.href = redirectUrl;
            }
        });

        $('[id$=txtFind]').focusin(function (event) {
            $('.loginbtn').prop("disabled", "true");
        });

        $('[id$=txtFind]').focusout(function (event) {
            $('.loginbtn').prop("disabled", "");
        });

    });
</script>
