<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberTopCtrl.ascx.cs" Inherits="YMGS.Trade.Web.MasterPage.MemberTopCtrl" %>
<div class="hytop">
  <div class="hycont">
    <div class="hycontleft">    <asp:Image ID="Image1" runat="server" ImageUrl="<%$ Resources:GlobalLanguage,logoaddress%>" style="width: 140px; height: 45px" />
   </div>
    <div class="hycontright">
        <asp:HyperLink ID=lblHomePage runat=server CssClass="fontw" Text="<%$ Resources: GlobalLanguage,ReturnHomePage %>"></asp:HyperLink>
        <asp:LinkButton ID=btnLogout runat=server CssClass="fontw"
            Text="<%$ Resources: GlobalLanguage,SecureLogout %>" CausesValidation=false onclick="btnLogout_Click"></asp:LinkButton>
    </div>
  </div>
</div>