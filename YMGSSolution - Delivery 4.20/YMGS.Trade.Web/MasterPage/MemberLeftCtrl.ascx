<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberLeftCtrl.ascx.cs" Inherits="YMGS.Trade.Web.MasterPage.MemberLeftCtrl" %>
<div class="hyheadleft">
    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:GlobalLanguage,MemberShipCenter %>"></asp:Label></div>
<div class="leftmain"> 
    <asp:Repeater runat="server" ID="repMenuItems">
        <ItemTemplate>
            <a class="<%# ((bool)DataBinder.Eval(Container.DataItem,"Selected"))?"leftmenusel":"" %>"
                href="<%# DataBinder.Eval(Container.DataItem, "Url") %>"
                target="<%# DataBinder.Eval(Container.DataItem, "UrlTarget") %>"
                ><%# DataBinder.Eval(Container.DataItem, "DisplayText")%></a>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div class="botlefcss"></div>