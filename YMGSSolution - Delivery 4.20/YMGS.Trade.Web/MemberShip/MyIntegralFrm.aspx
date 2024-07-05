<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master" AutoEventWireup="true" CodeBehind="MyIntegralFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.MyIntegralFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
<asp:UpdatePanel ID="upl" runat="server">
<ContentTemplate><table class="NoBorderTable" width="100%">
    <tr>
        <td style="text-align:right;">
            <asp:LinkButton ID="btnIntegralRule" runat="server" Text="<%$ Resources:GlobalLanguage,ViewIntegralRule %>" OnClick="BtnIntegralRule_Click" CausesValidation="false" ></asp:LinkButton>
        </td>
    </tr>
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:GlobalLanguage,CurIntegral %>"></asp:Label><asp:Label ID="lblCurIntegral" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:GlobalLanguage,CurBrokerage %>"></asp:Label><asp:Label ID="lblCurBrokerage" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <table class="NoBorderTable" style="margin-top:5px;margin-bottom:5px;width:100%">
                <tr>
                    <td style="width:60px">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:GlobalLanguage,BeginDate %>"></asp:Label>
                    </td>
                    <td style="width:90px">
                        <div class="calendarContainer">
                            <YMGS:AjaxCalendar ID="calStartDate" runat="server" NeedCalendarButton="true" />
                        </div>
                    </td>
                    <td style="width:10px"></td>
                    <td style="width:60px">
                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:GlobalLanguage,EndDate %>"></asp:Label>
                    </td>
                    <td style="width:90px">
                        <div class="calendarContainer">
                            <YMGS:AjaxCalendar ID="calEndDate" runat="server" NeedCalendarButton="true" />
                        </div>
                    </td>
                    <td colspan="2" align="right">
                        <asp:Button ID="btnQuery" Width="70px" CssClass="Button" runat="server" CausesValidation="false" Text="<%$ Resources:GlobalLanguage,Query %>" OnClick="BtnQuery_Click" />&nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gdvIntegral" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%"
                EmptyDataText="<%$ Resources:GlobalLanguage,NoMyIntegralData %>" 
                onrowdatabound="gdvIntegral_RowDataBound" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:GlobalLanguage,TradeDate %>">
                        <ItemTemplate>
                            <asp:Label ID="lblTradeDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Dealed_Fund" HeaderText="<%$ Resources:GlobalLanguage,DealFund %>" />
                    <asp:BoundField DataField="Got_Integral" HeaderText="<%$ Resources:GlobalLanguage,GotIntegral %>" />
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
