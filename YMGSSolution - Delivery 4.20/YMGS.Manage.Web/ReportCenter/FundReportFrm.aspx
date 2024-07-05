<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="FundReportFrm.aspx.cs" Inherits="YMGS.Manage.Web.ReportCenter.FundReportFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
<%--<asp:UpdatePanel ID="upl" runat="server">
<ContentTemplate>--%>
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
<div><span>总金额:</span><asp:Label ID="lblFundTotal" runat="server"></asp:Label></div>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gdvFund" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%"
                EmptyDataText="无相关资金数据" 
                onrowdatabound="gdvFund_RowDataBound" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="类型">
                        <ItemTemplate>
                            <asp:Label ID="lblFundType" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="时间">
                        <ItemTemplate>
                            <asp:Label ID="lblFundDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="金额">
                        <ItemTemplate>
                            <asp:Label ID="lblFundAmt" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="会员名">
                        <ItemTemplate>
                            <asp:Label ID="lblFundUserName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状态">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>
