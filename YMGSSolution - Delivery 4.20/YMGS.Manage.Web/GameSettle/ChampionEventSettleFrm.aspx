<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="ChampionEventSettleFrm.aspx.cs" Inherits="YMGS.Manage.Web.GameSettle.ChampionEventSettleFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
<table width="100%" class="NoBorderTable">
    <tr>
        <td>
            <fieldset>
                <legend>查询条件</legend>
                    <table class="NoBorderTable" style="margin-top:5px;margin-bottom:5px;width:100%">
                        <tr>
                            <td style="width:20px"></td>
                            <td style="width:70px">
                                <span>冠军赛事类型</span>
                            </td>
                            <td style="width:120px">
                                <asp:DropDownList ID="drpChampEventType" runat="server" Width="150px"></asp:DropDownList>
                            </td>
                            <td style="width:10px">                                
                            </td>
                            <td style="width:70px">
                                <span>冠军赛事名称</span>
                            </td>
                            <td style="width:120px">
                                <asp:TextBox ID="txtChampEventName" runat="server" Width="150px" CssClass="TextBox" MaxLength="100"></asp:TextBox>
                            </td>
                            <td style="width:10px">                                
                            </td>
                            <td style="width:70px">
                                <span>冠军赛事描述</span>
                            </td>
                            <td style="width:120px">
                                <asp:TextBox ID="txtChampEventDesc" runat="server" Width="150px" CssClass="TextBox" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:20px">                                
                            </td>
                            <td style="width:70px">
                                <span>开始时间</span>
                            </td>
                            <td style="width:120px">
                                <div class="calendarContainer">
                                    <YMGS:AjaxCalendar ID="calStartDate" runat="server" NeedCalendarButton="true" />
                                </div>
                            </td>
                            <td style="width:10px">                                
                            </td>
                            <td style="width:70px">
                                <span>结束时间</span>
                            </td>
                            <td style="width:120px">
                                <div class="calendarContainer">
                                    <YMGS:AjaxCalendar ID="calEndDate" runat="server" NeedCalendarButton="true" />
                                </div>
                            </td>
                            <td style="width:10px">
                            </td>
                            <td align="right" colspan="2">
                                <asp:Button runat="server" ID="btnSearch"  Width="70px" Text="查询" CausesValidation="false" CssClass="Button" OnClick="btnSearch_Click" />                                
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
            <asp:GridView ID="gdvChampEvent" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%" DataKeyNames="Champ_Event_ID"
                EmptyDataText="请输入合适的查询条件查询冠军赛事!" OnRowDataBound="gdvChampEvent_RowDataBind" 
                OnRowCommand="gridData_RowCommand" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField  ItemStyle-Width="100" HeaderText="冠军赛事类型">
                        <ItemTemplate>
                            <asp:Label ID="lblChampEventTypeName" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Champ_Event_Name" HeaderText="冠军赛事名称" />
                    <asp:BoundField DataField="Champ_Event_Desc" HeaderText="冠军赛事描述" />
                    <asp:BoundField DataField="Champ_Event_StartDate" HeaderText="开始日期" />
                    <asp:BoundField DataField="Champ_Event_EndDate" HeaderText="结束日期" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40" HeaderText="状态">
                        <ItemTemplate>
                            <asp:Label ID="lblChampEventStatusName" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80" HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID=btnCalcChampEvent CssClass=LinkButton runat=server Text="结算" CausesValidation=false></asp:LinkButton>
                            <asp:LinkButton ID=btnReCalcChampEvent CssClass=LinkButton runat=server Text="重新结算" CausesValidation=false></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
</asp:Content>
