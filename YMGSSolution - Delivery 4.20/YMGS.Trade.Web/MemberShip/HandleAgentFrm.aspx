<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master" AutoEventWireup="true" CodeBehind="HandleAgentFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.HandleAgentFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
<table class="NoBorderTable" width="100%">
    <tr><td style="text-align:left; font-weight:bolder;"><asp:Label ID="lblGrowAgentTitle" runat="server" Text="<%$ Resources:GlobalLanguage,HandleAgentTitle %>"></asp:Label></td></tr>    
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td align="right">
        <asp:LinkButton id="btnGrowMember" runat="server" Text="<%$ Resources:GlobalLanguage,btnGrowMember %>" CausesValidation="false" OnClick="GrowMember_Click"></asp:LinkButton>
        <asp:LinkButton id="btnSetAgentDeatail" runat="server" Text="<%$ Resources:GlobalLanguage,SetAgentDeatail %>" CausesValidation="false" OnClick="SetAgentDeatail_Click"></asp:LinkButton>
        </td>
    </tr>    
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td style="text-align:left; line-height:25px;">
            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:GlobalLanguage,HandleRoleTitle %>"></asp:Label>
            <asp:DropDownList ID="drpHandleType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpHandleType_SelectedIndexChanged" style="margin-bottom:3px;"></asp:DropDownList>
        </td>
    </tr> 
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gdvAgent" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%" DataKeyNames="USER_ID"
                EmptyDataText="<%$ Resources:GlobalLanguage,NoSuchData %>" OnRowDataBound="gdv_RowDataBind" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250" HeaderText="<%$ Resources:GlobalLanguage,HandleRole %>">
                        <ItemTemplate>
                            <asp:Label ID="lblRoleName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LOGIN_NAME" HeaderText="<%$ Resources:GlobalLanguage,HandleUserName %>" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250" HeaderText="<%$ Resources:GlobalLanguage,Action %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="hlEdit" runat="server" CssClass="HyperLink" CausesValidation="false" OnClick="Edit_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
</asp:Content>
