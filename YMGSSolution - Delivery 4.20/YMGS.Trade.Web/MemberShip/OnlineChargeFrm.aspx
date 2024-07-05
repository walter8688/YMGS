<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master" AutoEventWireup="true" CodeBehind="OnlineChargeFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.OnlineChargeFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <asp:UpdatePanel ID="upl" runat="server">
<ContentTemplate>
<table class="NoBorderTable" width="100%">
    <tr>
        <td style="text-align:right;">
            <asp:LinkButton ID="btnSetBankInfo" runat="server" Text="<%$ Resources:GlobalLanguage,SetBankInfo %>" OnClick="BtnSetBankInfo_Click" CausesValidation="false" ></asp:LinkButton>
            <asp:LinkButton ID="btnSupplyWithdraw" runat="server" Text="<%$ Resources:GlobalLanguage,SupplyWithdraw %>" OnClick="BtnSupplyWithdraw_Click" CausesValidation="false"></asp:LinkButton>
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
                        <asp:Button ID="btnPay" Width="70px" CssClass="Button" runat="server" CausesValidation="false" Text="<%$ Resources:GlobalLanguage,Pay %>" OnClick="BtnPay_Cikck" />&nbsp;
                        <asp:Button ID="btnVCardActivat" Width="120px" CssClass="Button" runat="server" CausesValidation="false" Text="<%$ Resources:GlobalLanguage,VCardActivated %>" OnClick="BtnVCardActivat_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gdvUserPay" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%"
                EmptyDataText="<%$ Resources:GlobalLanguage,NoPayData %>" 
                onrowdatabound="gdvPay_RowDataBound" OnRowCommand="gdvPay_RowCommand" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:GlobalLanguage,PayType %>">
                        <ItemTemplate>
                            <asp:Label ID="lblPayType" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Order_Id" HeaderText="<%$ Resources:GlobalLanguage,PayOrder %>" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:GlobalLanguage,PayDate %>">
                        <ItemTemplate>
                            <asp:Label ID="lblTransDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Tran_Amount" HeaderText="<%$ Resources:GlobalLanguage,PayAmt %>" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:GlobalLanguage,PayStatus %>">
                        <ItemTemplate>
                            <asp:Label ID="lblTransStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250" HeaderText="<%$ Resources:GlobalLanguage,Pay %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnRePay" Text="<%$ Resources:GlobalLanguage,Pay %>" runat="server" CssClass="HyperLink" CausesValidation="false"></asp:LinkButton>
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
        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:GlobalLanguage,Pay %>"></asp:Label>
    </asp:Panel>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
        <tr>
            <td style="width:30px; height:30px; line-height:30px;"></td>
            <td style="width:60px; text-align:right;">
                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:GlobalLanguage,PayAmt %>"></asp:Label>
            </td>
            <td style="width:90px">
                <asp:DropDownList ID="DrpVCardFaceValue" runat="server" CssClass="DropdownList"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnPayMoney" runat="server" Text="<%$ Resources:GlobalLanguage,Pay %>" Width="70px"  CausesValidation="true" CssClass="Button" OnClick="BtnPayMoney_Click"/>
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:GlobalLanguage,Cancel %>" Width="70px" CausesValidation="false" CssClass="Button" />
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
<asp:Button ID="btnFakeActivateVCard" runat="server" style="display:none" />
<asp:Panel ID="pnlActivateVCard" runat="server" style="width:300px;height:220px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlActivateVCardHeader" runat="server" CssClass="ModalPoupHeader">
        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:GlobalLanguage,VCardActivate %>"></asp:Label>
    </asp:Panel>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
        <tr>
            <td style="width:30px; height:30px; line-height:30px;"></td>
            <td style="width:60px; text-align:right;">
                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:GlobalLanguage,VCardNo %>"></asp:Label>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="txtVCardNo" runat="server" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:30px; height:30px; line-height:30px;"></td>
            <td style="width:60px; text-align:right;">
                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:GlobalLanguage,VCardActivateNo %>"></asp:Label>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="txtVCardActivateNo" runat="server" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnActivate" runat="server" Text="<%$ Resources:GlobalLanguage,Activate %>" Width="70px"  CausesValidation="false" CssClass="Button" OnClick="BtnActivate_Click" OnClientClick="return CheckVCardActivate();"/>
                &nbsp;
                <asp:Button ID="btnCancleVCardActivate" runat="server" Text="<%$ Resources:GlobalLanguage,Cancel %>" Width="70px" CausesValidation="false" CssClass="Button" />
            </td>
        </tr>
    </table>
    <div style="display:none; text-align:center; color:Red;" id="divVCardActivateErrMsg"><asp:Label ID="Label8" runat="server" Text="<%$ Resources:GlobalLanguage,VCardActivateErrMsg %>"></asp:Label></div>
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlActivateVCard" BehaviorID="mdlActivateVCard" 
    TargetControlID="btnFakeActivateVCard"
    PopupControlID="pnlActivateVCard"
    BackgroundCssClass="ModalPopupBackground"                         
    CancelControlID="btnCancleVCardActivate" 
    PopupDragHandleControlID="pnlActivateVCardHeader">
    </ajaxToolkit:ModalPopupExtender> 
</asp:Panel>
<asp:Button ID="btnFakeMsg" runat="server" style="display:none;" />
<asp:Panel ID="pnlMsg" runat="server" style="width:200px;height:160px;display:none" CssClass="ModalPoup">
    <div id="divMsg" runat="server" style="text-align:center; font-weight:bolder; line-height:20px;">
        
    </div>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnCancleMsg" runat="server" Text="<%$ Resources:GlobalLanguage,Confirm %>" Width="70px" CausesValidation="false" CssClass="Button" />
            </td>
        </tr>
    </table>
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlMsg" BehaviorID="mdlMsg" 
    TargetControlID="btnFakeMsg"
    PopupControlID="pnlMsg"
    BackgroundCssClass="ModalPopupBackground"                         
    CancelControlID="btnCancleMsg">
    </ajaxToolkit:ModalPopupExtender> 
</asp:Panel>
<asp:Button ID="btnFakePayMsg" runat="server" style="display:none;" />
<asp:Panel ID="pnlPayMsg" runat="server" style="width:200px;height:220px;display:none" CssClass="ModalPoup">
    <div id="divPayMsg" runat="server" style="text-align:left; font-weight:bolder; line-height:20px;">
        
    </div>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnCanclePayMsg" runat="server" Text="<%$ Resources:GlobalLanguage,Confirm %>" Width="70px" CausesValidation="false" CssClass="Button" />
            </td>
        </tr>
    </table>
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPayMsg" BehaviorID="mdlPayMsg" 
    TargetControlID="btnFakePayMsg"
    PopupControlID="pnlPayMsg"
    BackgroundCssClass="ModalPopupBackground"                         
    CancelControlID="btnCanclePayMsg">
    </ajaxToolkit:ModalPopupExtender> 
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    function CheckVCardActivate() {
        if (chkIsNull('txtVCardNo') || chkIsNull('txtVCardActivateNo')) {
            $('#divVCardActivateErrMsg').show();
            return false;
        }
        else {
            $('#divVCardActivateErrMsg').hide();
            return true;
        }
    }
</script>
</asp:Content>
