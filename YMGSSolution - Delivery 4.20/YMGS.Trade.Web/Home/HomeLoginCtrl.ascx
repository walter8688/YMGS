<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeLoginCtrl.ascx.cs"
    Inherits="YMGS.Trade.Web.Home.HomeLoginCtrl" %>
<%--<asp:UpdatePanel ID=updLoginForm runat=server UpdateMode=Conditional>
<ContentTemplate>--%>
<asp:Panel ID="pnlLogin" runat="server">
    <input name="txtUserName" id="txtUserName" runat="server" type="text" class="logusername"
        placeholder="<%$ Resources:GlobalLanguage,DefaultUserName %>" onkeyup="filterUnSaveStr(this)" onpaste="filterUnSaveStr(this)" />
    <input name="txtPassword" id="txtPassword" runat="server" onpaste="return false" oncopy="return false" type="password" class="logusername"
        placeholder="<%$ Resources:GlobalLanguage,DeafalutPassword %>" />
    <div class="logdiv"> 
        <asp:Button ID="btnLogin" runat="server" CssClass="loginbtn" Text="<%$ Resources:GlobalLanguage,Login %>"
            OnClick="btnLogin_Click" CausesValidation="false" />
        <div style="float:left">
            <table class="noBorderTable logdivlink">
                <tr>
                    <td>
            <asp:HyperLink runat="server" ID="hyRegister" Text="<%$ Resources:GlobalLanguage,Register %>"></asp:HyperLink>
                    </td>
                    <td>|</td>
                    <td>
            <asp:HyperLink runat="server" ID="hyForgetPwd" Text="<%$ Resources:GlobalLanguage,forgetpsw %>"></asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
  </div>
</asp:Panel>
<asp:Panel ID="pnlUserInfo" runat="server">
    <table cellspacing="8px">
        <tr>
            <td>
                <asp:Label ID="lblLoginName" runat="server"></asp:Label>
                <asp:ImageButton ID="imgBtnMessage" ImageUrl="~/Images/mail.png" runat="server" 
                    onclick="imgBtnMessage_Click" />
            </td>
        </tr>
        <tr>
            <td><span class="azh">
            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:GlobalLanguage,CurAccount %>"></asp:Label><b><asp:Label
                ID="lblCurFund" runat="server"></asp:Label></b> </span>
            </td>
        </tr>
        <tr>
            <td> <asp:HyperLink ID="hlMyAccount" runat="server" Text="<%$ Resources:GlobalLanguage,myaccount %>"
            class="loglinkfont"></asp:HyperLink>&nbsp;
            <asp:HyperLink ID="hlonlinecharge" runat="server" Text="<%$ Resources:GlobalLanguage,OnlineCharge %>"
            class="loglinkfont"></asp:HyperLink>
            <span class="azh">
            <asp:HyperLink ID="hlmytrade" runat="server" Text="<%$ Resources:GlobalLanguage,HisTradeReport %>"
            class="loglinkfont"></asp:HyperLink>&nbsp;
            </span>
            </td>
          <tr>
            <td> <asp:Button ID="btnLogout" runat="server" CssClass="loginbtn" Text="<%$ Resources:GlobalLanguage,SecureLogout %>"
        CausesValidation="false" OnClick="btnLogout_Click" />
            </td>
        </tr>
    </table>
    <br />   
</asp:Panel>
<%--</ContentTemplate>
</asp:UpdatePanel>
--%>