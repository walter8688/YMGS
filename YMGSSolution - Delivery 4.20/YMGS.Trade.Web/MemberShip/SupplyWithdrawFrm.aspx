<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master" AutoEventWireup="true" CodeBehind="SupplyWithdrawFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.SupplyWithdrawFrm" %>
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
            <asp:LinkButton ID="btnFundDetail" runat="server" Text="<%$ Resources:GlobalLanguage,FundDetail %>" OnClick="btnFundDetail_Click"></asp:LinkButton>
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
                        <asp:Button ID="btnWithDraw" Width="70px" CssClass="Button" runat="server" CausesValidation="false" Text="<%$ Resources:GlobalLanguage,WithDraw %>" OnClick="BtnWithDraw_Cilck" />&nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gdvUserWithDraw" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%"
                EmptyDataText="<%$ Resources:GlobalLanguage,NoWithDrawData %>" 
                onrowdatabound="gdvWithDraw_RowDataBound" OnRowCommand="gdvWithDraw_RowCommand" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="TRANS_ID" HeaderText="<%$ Resources:GlobalLanguage,WDTransId %>" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:GlobalLanguage,WDStatus %>">
                        <ItemTemplate>
                            <asp:Label ID="lblWDStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:GlobalLanguage,WDDate %>">
                        <ItemTemplate>
                            <asp:Label ID="lblWDDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="WD_AMOUNT" HeaderText="<%$ Resources:GlobalLanguage,WDAmt %>" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250" HeaderText="<%$ Resources:GlobalLanguage,Action %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnCancleWD" Text="<%$ Resources:GlobalLanguage,Cancel %>" runat="server" CssClass="HyperLink" CausesValidation="false"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
<asp:Button ID="btnFake" runat="server" style="display:none" />
<asp:Panel ID="pnlPopup" runat="server" style="width:300px;height:260px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:GlobalLanguage,WithDraw %>"></asp:Label>
    </asp:Panel>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
        <tr>
            <td style="width:30px; height:30px; line-height:30px;"></td>
            <td style="width:60px; text-align:right;">
                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:GlobalLanguage,WDAmt %>"></asp:Label>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="txtWDAmt" Text="100" runat="server" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSaveWithDraw" runat="server" Text="<%$ Resources:GlobalLanguage,WithDraw %>" Width="70px"  CausesValidation="true" CssClass="Button" OnClick="BtnSaveWithDraw_Click"/>
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:GlobalLanguage,Cancel %>" Width="70px" CausesValidation="false" CssClass="Button" />
            </td>
        </tr>
    </table>
    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Resources:GlobalLanguage,WithDrawErrMsg %>" ClientValidationFunction="ValidateWDMoney" style="display:none;"></asp:CustomValidator>
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
    function ValidateWDMoney(source, args) {
        var wdMoney = $('[id$=txtWDAmt]').val();
        if (!isPositiveInteger(wdMoney)) {
            args.IsValid = false;
            return;
        }
        if (parseInt(wdMoney) < 100) {
            args.IsValid = false;
            return;
        }
        return false;
    }
</script>
</asp:Content>
