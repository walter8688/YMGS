<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="VCardManagePage.aspx.cs" Inherits="YMGS.Manage.Web.SystemSetting.VCardManagePage" %>
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
                    <td style="width:10px"></td>
                    <td style="width:60px">
                        <asp:Label ID="Label1" runat="server" Text="V网卡状态"></asp:Label>
                    </td>
                    <td style="width:90px">
                        <asp:DropDownList ID="DrpVCardStatus" runat="server" CssClass="DropdownList"></asp:DropDownList>
                    </td>
                    <td style="width:10px"></td>
                    <td style="width:60px">
                        <asp:Label ID="Label2" runat="server" Text="V网卡面值"></asp:Label>
                    </td>
                    <td style="width:90px">
                        <asp:DropDownList ID="DrpVCardFaceValue" runat="server" CssClass="DropdownList"></asp:DropDownList>
                    </td>
                    <td style="width:60px">
                        <asp:Label ID="Label6" runat="server" Text="开始日期"></asp:Label>
                    </td>
                    <td style="width:90px">
                        <div class="calendarContainer">
                            <YMGS:AjaxCalendar ID="calStartDate" runat="server" NeedCalendarButton="true" />
                        </div>
                    </td>
                    <td style="width:10px"></td>
                    <td style="width:60px">
                        <asp:Label ID="Label7" runat="server" Text="结束日期"></asp:Label>
                    </td>
                    <td style="width:90px">
                        <div class="calendarContainer">
                            <YMGS:AjaxCalendar ID="calEndDate" runat="server" NeedCalendarButton="true" />
                        </div>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnQuery" Width="70px" CssClass="Button" runat="server" CausesValidation="false" Text="查询" OnClick="BtnQuery_Click" />&nbsp;
                        <asp:Button ID="btnGenerateVCard" Width="70px" CssClass="Button" runat="server" CausesValidation="false" Text="生成V网卡" OnClick="BtnGenerateVCard_Click" />&nbsp;
                        <asp:Button ID="btnExport" runat=server Width="70px" CssClass="Button" CausesValidation="false" Text="导出" OnClick="btnExport_Click" />
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
            <asp:GridView ID="gdvVCard" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%"
                EmptyDataText="无相关V网卡数据" 
                onrowdatabound="gdvVCard_RowDataBound" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="V网卡卡号">
                        <ItemTemplate>
                            <asp:Label ID="lblVCardNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="V网卡激活码">
                        <ItemTemplate>
                            <asp:Label ID="lblVCardActivateNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="VCARD_FACE_VALUE" HeaderText="V网卡面值" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="V网卡状态">
                        <ItemTemplate>
                            <asp:Label ID="lblVCardStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="生成日期">
                        <ItemTemplate>
                            <asp:Label ID="lblVCardGenerateDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LOGIN_NAME" HeaderText="激活人" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="激活日期">
                        <ItemTemplate>
                            <asp:Label ID="lblActivateDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
<asp:Button ID="btnFake" runat="server" style="display:none" />
<asp:Panel ID="pnlPopup" runat="server" style="width:200px;height:200px;display:none" CssClass="ModalPoup">
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
                <asp:Label ID="Label3" runat="server" Text="V网卡面值"></asp:Label>
            </td>
            <td style="width:90px">
                <asp:DropDownList ID="PopDrpVCardFaceValue" runat="server" CssClass="DropdownList"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:30px; height:30px; line-height:30px;"></td>
            <td style="width:60px; text-align:right;">
                <asp:Label ID="Label4" runat="server" Text="生成数量"></asp:Label>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="txtVCardNums" runat="server" CssClass="TextBox" ></asp:TextBox>
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
    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="生成数量必须大于0的正整数" ClientValidationFunction="ValidateVCardNums" style="display:none;"></asp:CustomValidator>
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
<Triggers>
    <asp:PostBackTrigger ControlID= "btnExport"/>
</Triggers>
</asp:UpdatePanel>

<script type="text/javascript">
    function ValidateVCardNums(source, args) {
        var vcardnums = $('[id$=txtVCardNums]').val();
        if (!isPositiveInteger(vcardnums)) {
            args.IsValid = false;
            return;
        }
        if (vcardnums < 1) {
            args.IsValid = false;
            return;
        }
        return false;
    }
</script>
</asp:Content>
