<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="IntegralReportFrm.aspx.cs" Inherits="YMGS.Manage.Web.ReportCenter.IntegralReportFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <fieldset>
            <legend>查询条件</legend>
            <table class="NoBorderTable" style="margin-top:5px;margin-bottom:5px;width:100%">
                <tr>
                    <td style="width:60px">
                        <asp:Label ID="Label1" runat="server" Text="开始日期"></asp:Label>
                    </td>
                    <td style="width:90px">
                        <div class="calendarContainer">
                            <YMGS:AjaxCalendar ID="calStartDate" runat="server" NeedCalendarButton="true" />
                        </div>
                    </td>
                    <td style="width:10px"></td>
                    <td style="width:60px">
                        <asp:Label ID="Label2" runat="server" Text="结束日期"></asp:Label>
                    </td>
                    <td style="width:90px">
                        <div class="calendarContainer">
                            <YMGS:AjaxCalendar ID="calEndDate" runat="server" NeedCalendarButton="true" />
                        </div>
                    </td>
                    <td style="width:10px"></td>
                    <td style="width:30px">
                        <asp:Label ID="Label3" runat="server" Text="类型"></asp:Label>
                    </td>
                    <td style="width:90px">
                        <asp:DropDownList ID="drpFundType" runat="server" CssClass="DropdownList"></asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnQuery" Width="70px" CssClass="Button" runat="server" CausesValidation="false" Text="查询" OnClick="BtnQuery_Click" />&nbsp;
                        <asp:Button ID="btnExport" Width="70px" CssClass="Button" runat="server" CausesValidation="false" Text="导出" OnClick="BtnExport_Click" />
                    </td>
                </tr>
            </table>
            </fieldset>
        </td>
    </tr>
</table>
<div id="divBrk" runat="server"><span>佣金总金额:</span><asp:Label ID="lblBrkFundTotal" runat="server"></asp:Label></div>
<div id="divAgent" runat="server"><span>代理返点金额:</span><asp:Label ID="lblAgentFundTotal" runat="server"></asp:Label> | <span>总代理返点金额:</span><asp:Label ID="lblGAFundTotal" runat="server"></asp:Label></div>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gdvBrokerage" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%"
                EmptyDataText="无相关佣金数据" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="reportType" HeaderText="类型" />
                    <asp:BoundField DataField="SETTLE_TIME" HeaderText="结算时间" />
                    <asp:BoundField DataField="EVENT_NAME" HeaderText="赛事名称" />
                    <asp:BoundField DataField="MATCH_NAME" HeaderText="比赛名称" />
                    <asp:BoundField DataField="BET_TYPE_NAME" HeaderText="玩法" />
                    <asp:BoundField DataField="MARKET_NAME" HeaderText="市场名称" />
                    <asp:BoundField DataField="ODDS" HeaderText="成交赔率" />
                    <asp:BoundField DataField="DEAL_AMOUNT" HeaderText="成交金额" />
                    <asp:BoundField DataField="DEAL_TIME" HeaderText="交易时间" />
                    <asp:BoundField DataField="winuser" HeaderText="赢家" />
                    <asp:BoundField DataField="loseuser" HeaderText="输家" />
                    <asp:BoundField DataField="BROKERAGE_RATE" HeaderText="佣金率" />
                    <asp:BoundField DataField="BROKERAGE" HeaderText="佣金额" />
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gdvCommission" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%"
                EmptyDataText="无相关返点数据" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="reportType" HeaderText="类型" />
                    <asp:BoundField DataField="SETTLE_TIME" HeaderText="结算时间" />
                    <asp:BoundField DataField="EVENT_NAME" HeaderText="赛事名称" />
                    <asp:BoundField DataField="MATCH_NAME" HeaderText="比赛名称" />
                    <asp:BoundField DataField="BET_TYPE_NAME" HeaderText="玩法" />
                    <asp:BoundField DataField="MARKET_NAME" HeaderText="市场名称" />
                    <asp:BoundField DataField="ODDS" HeaderText="成交赔率" />
                    <asp:BoundField DataField="DEAL_AMOUNT" HeaderText="成交金额" />
                    <asp:BoundField DataField="DEAL_TIME" HeaderText="交易时间" />
                    <asp:BoundField DataField="winuser" HeaderText="赢家" />
                    <asp:BoundField DataField="loseuser" HeaderText="输家" />
                    <asp:BoundField DataField="AGENT_COMMISSION_RATE" HeaderText="代理返点率" />
                    <asp:BoundField DataField="AGENT_COMMISSION_TRADE_FUND" HeaderText="代理返点金额" />
                    <asp:BoundField DataField="MAIN_AGENT_COMMISSION_RATE" HeaderText="总代理返点率" />
                    <asp:BoundField DataField="MAIN_AGENT_COMMISSION_TRADE_FUND" HeaderText="总代理返点金额" />
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigatorCommission" runat="server" />
        </td>
    </tr>
</table>
</asp:Content>
