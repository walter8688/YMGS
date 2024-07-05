<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenu.ascx.cs" Inherits="YMGS.Manage.Web.Controls.TopMenu" %>
<asp:Repeater ID="rptTopMenu" runat="server" 
    onitemdatabound="rptTopMenu_ItemDataBound">
    <ItemTemplate>
        <div class="caidan" runat="server" id="divTopMenu">
            <asp:LinkButton ID="lbtTopMenu"  CausesValidation="false"  runat="server" class="fony" Text='<%#Eval("NodeTitle") %>'  CommandArgument='<%#Eval("NodeId") %>' PostBackUrl='<%#Eval("NodeUrl") %>'></asp:LinkButton>
        </div>
    </ItemTemplate>
</asp:Repeater>
