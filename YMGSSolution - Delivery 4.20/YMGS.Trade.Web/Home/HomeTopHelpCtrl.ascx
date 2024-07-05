<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeTopHelpCtrl.ascx.cs" Inherits="YMGS.Trade.Web.Home.HomeTopHelpCtrl" %>
<div class="hometophelpleft">
    <asp:Repeater ID="rpthelper" runat="server">
    <ItemTemplate>
    <asp:HyperLink ID="hyAbout" runat="server" CssClass="hometophelplink"  Target=_blank Text='<%#Eval("name")%>' NavigateUrl='<%#"~/Public/HelperCenter.aspx?node="+Eval("ITEMID").ToString()%>'></asp:HyperLink>
    <span class="jiange"></span>
    </ItemTemplate>
    </asp:Repeater>
  
    <asp:LinkButton ID=lbLanguage runat=server onclick="lbLanguage_Click"></asp:LinkButton>
</div>

