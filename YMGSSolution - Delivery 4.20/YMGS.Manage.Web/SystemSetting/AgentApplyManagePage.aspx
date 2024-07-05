<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="AgentApplyManagePage.aspx.cs" Inherits="YMGS.Manage.Web.SystemSetting.AgentApplyManagePage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
<asp:UpdatePanel ID="upl" runat="server">
<ContentTemplate>
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
                    <td style="width:50px">
                        <asp:Label ID="Label4" runat="server" Text="用户名"></asp:Label>
                    </td>
                    <td style="width:90px">
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td style="width:10px"></td>
                    <td style="width:60px">
                        <asp:Label ID="Label3" runat="server" Text="总代理等级"></asp:Label>
                    </td>
                    <td style="width:90px">
                        <asp:DropDownList ID="drpRoleType" runat="server" CssClass="DropdownList"></asp:DropDownList>
                    </td>
                    <td style="width:30px">
                        <asp:Label ID="Label5" runat="server" Text="状态"></asp:Label>
                    </td>
                    <td style="width:90px">
                        <asp:DropDownList ID="drpApplyStatus" runat="server" CssClass="DropdownList"></asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnQuery" Width="70px" CssClass="Button" runat="server" CausesValidation="false" Text="查询" OnClick="BtnQuery_Click" />&nbsp;
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
            <asp:GridView ID="gdv" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%"
                EmptyDataText="无相关代理申请数据" 
                onrowdatabound="gdv_RowDataBound" OnRowCommand="gdv_RowCommand" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="Login_Name" HeaderText="用户名" />
                    <asp:BoundField DataField="Role_Name" HeaderText="总代理等级" />
                    <asp:BoundField DataField="User_Telephone" HeaderText="手机号码" />
                    <asp:BoundField DataField="User_Country" HeaderText="所属国籍" />
                    <asp:BoundField DataField="User_Province" HeaderText="所属州省" />
                    <asp:BoundField DataField="User_City" HeaderText="城市" />
                    <asp:BoundField DataField="User_BankAddress" HeaderText="开户行地址" />
                    <asp:BoundField DataField="User_BankNo" HeaderText="银行账号" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="申请日期">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="申请状态">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250" HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnApproveProcess" Text="审批" runat="server" CssClass="HyperLink" CausesValidation="false"></asp:LinkButton>
                            <asp:LinkButton ID="btnConfirm" Text="批准" runat="server" CssClass="HyperLink" CausesValidation="false"></asp:LinkButton>
                            <asp:LinkButton ID="btnReject" Text="拒绝" runat="server" CssClass="HyperLink" CausesValidation="false"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
