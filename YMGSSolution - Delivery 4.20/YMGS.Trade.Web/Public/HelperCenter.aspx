<%@ Page EnableEventValidation="false" ViewStateEncryptionMode="Never" Language="C#"
    AutoEventWireup="true" CodeBehind="HelperCenter.aspx.cs" Inherits="YMGS.Trade.Web.Public.HelperCenter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="YMGS" TagName="MemberTopCtrl" Src="~/MasterPage/MemberTopCtrl.ascx" %>
<%@ Register TagPrefix="YMGS" TagName="MemberFooterCtrl" Src="~/MasterPage/MemberFooterCtrl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="link1" runat="server" href="~/Css/HelpCenterCss.css" rel="Stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        //        function onerrorfunction(e) {
        //            alert("error");
        //        };
        //        window.addEventListener("error", onerrorfunction, false);

        //        window.onerror = function () { alert("error"); }
    </script>
</head>
<body style="height: 100%;">
    <form id="form1" runat="server">
    <div>
        <div class="hytop">
            <div class="hycont">
                <div class="hycontleft">
<%--                    <img src="../Images/memlog.png" />--%>
                    <asp:Image ID="Image1" runat="server" ImageUrl="<%$ Resources:GlobalLanguage,logoaddress%>" style="width: 140px; height: 45px" />
                    </div>
                <div class="hycontright">
                </div>
            </div>
        </div>
        <div id="content">
            <div class="bifaleft">
                <ul class="nav">
                    <%=LeftNavigatorStr %>
                </ul>
            </div>
            <div class="bifaright">
                <iframe frameborder="0" id="WebPath" name="WebPath" onload="setTimeout('iFrameHeight()', '50');"
                    scrolling="yes" style="width: 100%; border-style: hidden;"></iframe>
            </div>
        </div>
        <div class="divfoot">
            <YMGS:MemberFooterCtrl ID="footer" runat="server" />
        </div>
    </div>
    <script type="text/javascript">
        function iFrameHeight() {
            var ifm = document.getElementById("WebPath");
            var subWeb = document.frames ? document.frames["WebPath"].document : ifm.contentDocument;
            if (ifm != null && subWeb != null) {
                //alert(subWeb.body.scrollHeight);
                //ifm.height = subWeb.body.scrollHeight;
                ifm.height = $(window).height() - 125;
            }
        }

        //document宽度超过2000？？
        $(window).resize(function () {
            var contentHeight = $(window).height() - 125;
            $('#content').height(contentHeight);

            var ifm = document.getElementById("WebPath");
            var subWeb = document.frames ? document.frames["WebPath"].document : ifm.contentDocument;
            if (ifm != null && subWeb != null) {
                ifm.height = $(window).height() - 125;
            }
        });

        $(function () {
            //隐藏所有ul
            $(".nav").find("ul").hide();

            $(".hassubmenu").bind("click", function () {
                $(this).next("ul").slideToggle("slow");
                $(this).toggleClass("expand");
            });

            $("a").bind("click", function () {
                $(this).parent().siblings().find("ul").slideUp("slow");
                $(".nav").find(".selected").removeClass("selected");
                $(this).addClass("selected");
            });

            var contentHeight = $(window).height() - 125;
            $('#content').height(contentHeight);

        });

    </script>
    </form>
</body>
</html>
