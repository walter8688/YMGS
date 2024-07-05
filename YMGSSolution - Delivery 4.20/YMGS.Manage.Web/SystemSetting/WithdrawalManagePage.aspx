<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="WithdrawalManagePage.aspx.cs" Inherits="YMGS.Manage.Web.SystemSetting.WithdrawalManagePage" %>
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
                    <td style="width:30px">
                        <asp:Label ID="Label3" runat="server" Text="状态"></asp:Label>
                    </td>
                    <td style="width:90px">
                        <asp:DropDownList ID="drpWDStatus" runat="server" CssClass="DropdownList"></asp:DropDownList>
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
            <asp:GridView ID="gdvUserWithDraw" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%"
                EmptyDataText="无相关提现数据" 
                onrowdatabound="gdvWithDraw_RowDataBound" OnRowCommand="gdvWithDraw_RowCommand" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="LOGIN_NAME" HeaderText="用户名" />
                    <asp:BoundField DataField="TRANS_ID" HeaderText="交易号" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="提现状态">
                        <ItemTemplate>
                            <asp:Label ID="lblWDStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="提现日期">
                        <ItemTemplate>
                            <asp:Label ID="lblWDDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="WD_AMOUNT" HeaderText="提现金额" />
                    <asp:BoundField DataField="WD_BANK_NAME" HeaderText="转账银行" />
                    <asp:BoundField DataField="WD_CARD_NO" HeaderText="转账卡号" />
                    <asp:BoundField DataField="WD_ACCOUNT_HOLDER" HeaderText="转账人" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250" HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnConfirmWD" Text="批准" runat="server" CssClass="HyperLink" CausesValidation="false"></asp:LinkButton>
                            <asp:LinkButton ID="btnRejectWD" Text="拒绝" runat="server" CssClass="HyperLink" CausesValidation="false"></asp:LinkButton>
                            <asp:LinkButton ID="btnTransferWD" Text="转账" runat="server" CssClass="HyperLink" CausesValidation="false"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
<asp:Button ID="btnFake" runat="server" style="display:none" />
<asp:Panel ID="pnlPopup" runat="server" style="width:200px;height:160px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
        <asp:Label ID="Label5" runat="server" Text="转账"></asp:Label>
    </asp:Panel>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
        <tr>
            <td style="width:30px; height:30px; line-height:30px;"></td>
            <td style="width:60px; text-align:right;">
                <asp:Label ID="Label6" runat="server" Text="交易号"></asp:Label>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="txtTransId" runat="server" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSaveTransId" runat="server" Text="保存" Width="70px"  CausesValidation="true" CssClass="Button" OnClick="BtnSave_Click"/>
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="取消" Width="70px" CausesValidation="false" CssClass="Button" />
            </td>
        </tr>
    </table>
    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="交易号不能为空" ClientValidationFunction="ValidateTransId" style="display:none;"></asp:CustomValidator>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td style="color:Red; text-align:center;">
                    <asp:ValidationSummary runat="server" ShowMessageBox="false" ShowSummary="true" ID="ValidationSummary1" />
            </td>
        </tr>
    </table>
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" BehaviorID="mdlPopup" 
    TargetControlID="btnFake"
    PopupControlID="pnlPopup"
    BackgroundCssClass="ModalPopupBackground"                         
    CancelControlID="btnCancel" 
    PopupDragHandleControlID="pnlPopupHeader">
    </ajaxToolkit:ModalPopupExtender> 
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    function ValidateTransId(source, args) {
        if (chkIsNull('txtTransId')) {
            args.IsValid = false;
            return;
        }
        return false;
    }
</script>
</asp:Content>
