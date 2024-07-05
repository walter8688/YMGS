<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.ascx.cs" Inherits="YMGS.Manage.Web.Controls.LeftMenu" %>
<ul class="leftcsy" id="xitongguanli">
    <asp:Repeater ID="rptLeftMenu" runat="server" 
        onitemdatabound="rptLeftMenu_ItemDataBound">
        <ItemTemplate>
            <li class="leftfone" runat="server" id="leftmenu1"><a href='<%#Eval("NodeUrl") %>' class="leftfoner"><%#Eval("NodeTitle")%></a> </li>
            <li class="lineli"  runat="server" id="leftmenu2"></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
