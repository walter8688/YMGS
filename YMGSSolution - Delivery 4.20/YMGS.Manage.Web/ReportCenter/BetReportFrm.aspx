<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="BetReportFrm.aspx.cs" Inherits="YMGS.Manage.Web.ReportCenter.BetReportFrm" %>
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
                    <td style="width:70px">
                        <span>赛事项目</span>
                    </td>
                    <td style="width:150px">
                        <asp:DropDownList ID=ddlEventItem runat=server AutoPostBack=true Width=152px 
                            onselectedindexchanged="ddlEventItem_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td style="width:10px"></td>
                    <td style="width:70px">
                        <span>赛事区域</span>
                    </td>
                    <td style="width:150px">
                        <asp:DropDownList ID=ddlEventZone runat=server Width=152px AutoPostBack="True" 
        onselectedindexchanged="ddlEventZone_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td style="width:10px"></td>
                    <td style="width:70px">
                        <span>赛事</span>
                    </td>
                    <td style="width:150px">
                        <asp:DropDownList ID=ddlEvent runat=server CssClass="DropdownList" Width=152px></asp:DropDownList>
                    </td>
                    <td align="right">
                    </td>
                </tr>
                <tr>
                    <td style="width:70px">
                        <asp:Label ID="Label1" runat="server" Text="开始日期"></asp:Label>
                    </td>
                    <td style="width:150px">
                        <div class="calendarContainer">
                            <YMGS:AjaxCalendar ID="calStartDate" runat="server" NeedCalendarButton="true" />
                        </div>
                    </td>
                    <td style="width:10px"></td>
                    <td style="width:70px">
                        <asp:Label ID="Label2" runat="server" Text="结束日期"></asp:Label>
                    </td>
                    <td style="width:150px">
                        <div class="calendarContainer">
                            <YMGS:AjaxCalendar ID="calEndDate" runat="server" NeedCalendarButton="true" />
                        </div>
                    </td>
                    <td style="width:10px"></td>
                    <td style="width:70px">
                        <asp:Label ID="Label3" runat="server" Text="下注类型"></asp:Label>
                    </td>
                    <td style="width:150px">
                        <asp:DropDownList ID="drpExchangeType" runat="server" CssClass="DropdownList"></asp:DropDownList>
                    </td>
                    <td style="width:10px"></td>
                    <td style="width:30px">
                        <asp:Label ID="Label4" runat="server" Text="玩法"></asp:Label>
                    </td>
                    <td style="width:90px">
                        <asp:DropDownList ID="drpBetType" runat="server" CssClass="DropdownList"></asp:DropDownList>
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
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gdvExchange" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%"
                EmptyDataText="无相关下注数据">
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="ExchangeType" HeaderText="下注类型" />
                    <asp:BoundField DataField="EventName" HeaderText="赛事名称" />
                    <asp:BoundField DataField="MatchName" HeaderText="比赛名称" />
                    <asp:BoundField DataField="BET_NAME" HeaderText="玩法" />
                    <asp:BoundField DataField="Market_Tmp_Type" HeaderText="半全场" />
                    <asp:BoundField DataField="MARKET_NAME" HeaderText="市场名称" />
                    <asp:BoundField DataField="ODDS" HeaderText="赔率" />
                    <asp:BoundField DataField="BET_AMOUNTS" HeaderText="下注金额" />
                    <asp:BoundField DataField="MATCH_AMOUNTS" HeaderText="撮合剩余金额" />
                    <asp:BoundField DataField="TRADE_TIME" HeaderText="交易时间" />
                    <asp:BoundField DataField="MatchScore" HeaderText="下注时比分" />
                    <asp:BoundField DataField="LOGIN_NAME" HeaderText="下注人" />
                    <asp:BoundField DataField="Exchange_Status" HeaderText="下注状态" />
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
</asp:Content>
