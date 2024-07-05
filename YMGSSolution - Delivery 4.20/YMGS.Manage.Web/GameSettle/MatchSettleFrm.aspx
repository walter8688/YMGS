<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="MatchSettleFrm.aspx.cs" Inherits="YMGS.Manage.Web.GameSettle.MatchSettleFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
<table width=100% class=NoBorderTable>
    <tr>
        <td>
            <fieldset>
                <legend>查询条件</legend>
                    <table class=NoBorderTable style="margin-top:5px;margin-bottom:5px;width:100%">
                        <tr>
                            <td style="width:20px"></td>
                            <td style="width:70px">
                                <span>赛事项目</span>
                            </td>
                            <td style="width:150px">
                                <asp:DropDownList ID=ddlEventItem runat=server AutoPostBack=true Width=152px 
                                    onselectedindexchanged="ddlEventItem_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td style="width:20px">                                
                            </td>
                            <td style="width:90px">
                                <span>赛事区域</span>
                            </td>
                            <td style="width:150px">
                                <asp:DropDownList ID=ddlEventZone runat=server Width=152px AutoPostBack="True" 
                onselectedindexchanged="ddlEventZone_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td style="width:20px">
                            </td>
                            <td style="width:90px">
                                <span>赛事</span>
                            </td>
                            <td style="width:150px">
                                <asp:DropDownList ID=ddlEvent runat=server CssClass="DropdownList" Width=152px></asp:DropDownList>
                            </td>
                            <td align="right">
                            </td>
                        </tr>
                        <tr>
                            <td style="width:20px"></td>
                            <td style="width:70px">
                                <span>比赛名称</span>
                            </td>
                            <td style="width:150px">
                                <asp:TextBox ID=txtMatchName runat=server Width=150px CssClass=TextBox></asp:TextBox>
                            </td>
                            <td style="width:20px">                                
                            </td>
                            <td style="width:90px">
                                <span>比赛开始日期</span>
                            </td>
                            <td style="width:150px">
                                <div class="calendarContainer">
                                    <YMGS:AjaxCalendar ID=startDate runat=server />
                                </div>
                            </td>
                            <td style="width:20px">
                            </td>
                            <td style="width:90px">
                                <span>比赛结束日期</span>
                            </td>
                            <td style="width:150px">
                                <div class="calendarContainer">
                                    <YMGS:AjaxCalendar ID=endDate runat=server />
                                </div>
                            </td>
                            <td align="right">
                                <asp:Button runat=server ID=btnSearch  Width=70px Text="查询" CausesValidation=false CssClass=Button OnClick="btnSearch_Click" />
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
            <asp:GridView ID="gridData" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%" DataKeyNames="MATCH_ID"
                OnRowCommand="gridData_RowCommand"   OnRowDataBound="gridData_RowDataBound"                                                   
                EmptyDataText="请输入合适的查询条件查询比赛!"
                >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="MATCH_NAME" HeaderText="比赛名称"/>
                    <asp:BoundField DataField="EVENTTYPE_NAME" HeaderText="赛事类别" />
                    <asp:BoundField DataField="EVENTITEM_NAME" HeaderText="赛事项目" />
                    <asp:BoundField DataField="EVENT_NAME" HeaderText="赛事" />                   
                    <asp:TemplateField HeaderText="比赛开始时间">
                        <ItemTemplate>
                            <asp:Label ID=lblStartDate  runat=server></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="自动封盘时间">
                        <ItemTemplate>
                            <asp:Label ID=lblFreezeDate  runat=server></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态">
                        <ItemTemplate>
                            <asp:Label ID=lblStatus runat=server></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign=Center ItemStyle-Width=150px
                                     ItemStyle-Wrap=false ItemStyle-VerticalAlign=Middle>
                        <ItemTemplate>
                            <asp:LinkButton ID=btnFirstHalfCalc CssClass=LinkButton runat=server Text="半场结算" CausesValidation=false></asp:LinkButton>
                            <asp:LinkButton ID=btnFullCalc  CssClass=LinkButton runat=server Text="全场结算" CausesValidation=false ></asp:LinkButton>
                            <asp:LinkButton ID=btnReHalfCalc  CssClass=LinkButton runat=server Text="半场重新结算" CausesValidation=false ></asp:LinkButton>
                            <asp:LinkButton ID=btnReFullCalc  CssClass=LinkButton runat=server Text="重新结算" CausesValidation=false ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID=pageNavigator runat=server />
        </td>
    </tr>
</table>
</asp:Content>