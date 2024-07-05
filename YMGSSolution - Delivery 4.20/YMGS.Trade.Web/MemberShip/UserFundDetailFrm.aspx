<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master" AutoEventWireup="true" CodeBehind="UserFundDetailFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.UserFundDetailFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
<asp:UpdatePanel ID="upl" runat="server">
<ContentTemplate>
<table class="NoBorderTable" width="100%">
    <tr>
        <td style="text-align:right;">
            <asp:LinkButton ID="btnSetBankInfo" runat="server" Text="<%$ Resources:GlobalLanguage,SetBankInfo %>" OnClick="BtnSetBankInfo_Click" CausesValidation="false"></asp:LinkButton>
            <asp:LinkButton ID="btnOnlineCharge" runat="server" Text="<%$ Resources:GlobalLanguage,OnlineCharge %>" OnClick="BtnOnlineCharge_Click" CausesValidation="false"></asp:LinkButton>
            <asp:LinkButton ID="btnSupplyWithdraw" runat="server" Text="<%$ Resources:GlobalLanguage,SupplyWithdraw %>" OnClick="BtnSupplyWithdraw_Click" CausesValidation="false"></asp:LinkButton>
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
                    <td style="width:10px"></td>
                    <td style="width:60px">
                        <asp:Label ID="lblTypeTitle" runat="server" Visible="false" Text="<%$ Resources:GlobalLanguage,Type %>"></asp:Label>
                    </td>
                    <td style="width:90px">
                        <asp:DropDownList ID="ddlType" runat="server" Visible="false"></asp:DropDownList>
                    </td>
                    <td colspan="2" align="right">
                        <asp:Button ID="btnQuery" Width="70px" CssClass="Button" runat="server" CausesValidation="false" Text="<%$ Resources:GlobalLanguage,Query %>" OnClick="BtnQuery_Click" />&nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div><asp:Label ID="Label4" runat="server" Text="<%$ Resources:GlobalLanguage,TotalProfit %>"></asp:Label><asp:Label id="lblFundTotal" runat="server"></asp:Label>
&nbsp;&nbsp;<asp:Label ID="lblReimbursementTitle" Text="<%$ Resources:GlobalLanguage,TotalReimbursement %>" runat=server></asp:Label><asp:Label id="lblReimbursement" runat="server"></asp:Label>
</div>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gdvFundDetail" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%"
                EmptyDataText="<%$ Resources:GlobalLanguage,NoWithDrawData %>" 
                onrowdatabound="gdvFundDetail_RowDataBound" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="TRADE_DATE" HeaderText="<%$ Resources:GlobalLanguage,DateTime %>" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:GlobalLanguage,Type %>">
                        <ItemTemplate>
                            <asp:Label ID="lblTradeType" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TRADE_FUND" HeaderText="<%$ Resources:GlobalLanguage,Fund %>" />
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
