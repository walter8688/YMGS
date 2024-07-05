<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master" AutoEventWireup="true" CodeBehind="FinancialAccountFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.FinancialAccountFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
<asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<table class="NoBorderTable" width="100%">
    <tr>
        <td style="text-align:right;">
            <asp:LinkButton ID="btnSetBankInfo" runat="server" Text="<%$ Resources:GlobalLanguage,SetBankInfo %>" OnClick="showPopUp_Click"></asp:LinkButton>
            <asp:LinkButton ID="btnOnlineCharge" runat="server" Text="<%$ Resources:GlobalLanguage,OnlineCharge %>" OnClick="btnOnlineCharge_Click"></asp:LinkButton>
            <asp:LinkButton ID="btnSupplyWithdraw" runat="server" Text="<%$ Resources:GlobalLanguage,SupplyWithdraw %>" OnClick="BtnSupplyWithdraw_Click"></asp:LinkButton>
            <asp:LinkButton ID="btnFundDetail" runat="server" Text="<%$ Resources:GlobalLanguage,FundDetail %>" OnClick="btnFundDetail_Click"></asp:LinkButton>
        </td>
    </tr>
</table>
<table class="UserFundStyle">
    <tr>
        <td style="text-align:right;"><asp:Label ID="lblBankName" runat="server" Text="<%$ Resources:GlobalLanguage,BankName %>"></asp:Label></td>
        <td><asp:Label ID="txtBankName" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="text-align:right;"><asp:Label ID="lblOpenBankName" runat="server" Text="<%$ Resources:GlobalLanguage,OpenBankName %>"></asp:Label></td>
        <td><asp:Label ID="txtOpenBankName" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="text-align:right;"><asp:Label ID="lblBankCardNo" runat="server" Text="<%$ Resources:GlobalLanguage,BankCardNo %>"></asp:Label></td>
        <td><asp:Label ID="txtBankCardNo" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="text-align:right;"><asp:Label ID="lblAccountHolder" runat="server" Text="<%$ Resources:GlobalLanguage,AccountHolder %>"></asp:Label></td>
        <td><asp:Label ID="txtAccountHolder" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="text-align:right;"><asp:Label ID="lblCurrentFund" runat="server" Text="<%$ Resources:GlobalLanguage,CurrentFund %>"></asp:Label></td>
        <td><asp:Label ID="txtCurrentFund" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="text-align:right;"><asp:Label ID="lblFreezedFund" runat="server" Text="<%$ Resources:GlobalLanguage,FreezedFund %>"></asp:Label></td>
        <td><asp:Label ID="txtFreezedFund" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="text-align:right;"><asp:Label ID="lblCurrentIntegral" runat="server" Text="<%$ Resources:GlobalLanguage,CurrentIntegral %>"></asp:Label></td>
        <td><asp:Label ID="txtCurrentIntegral" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="text-align:right;"><asp:Label ID="lblFundAccountStatus" runat="server" Text="<%$ Resources:GlobalLanguage,FundAccountStatus %>"></asp:Label></td>
        <td><asp:Label ID="txtFundAccountStatus" runat="server"></asp:Label></td>
    </tr>
</table>

<asp:Button ID="btnFake" runat="server" style="display:none" />
<asp:Panel ID="pnlPopup" runat="server" style="width:200px;height:250px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
        <span><label id="lblPopPanelTitle" runat="server"></label></span>
    </asp:Panel>
    <table>
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
    </table>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td style="text-align:right;"><asp:Label ID="Label1" runat="server" Text="<%$ Resources:GlobalLanguage,BankName %>"></asp:Label></td>
            <td><asp:TextBox ID="popTxtBankName" runat="server" CssClass="TextBox"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align:right;"><asp:Label ID="Label3" runat="server" Text="<%$ Resources:GlobalLanguage,OpenBankName %>"></asp:Label></td>
            <td><asp:TextBox ID="popTxtOpenBankName" runat="server" CssClass="TextBox"></asp:TextBox></td></td>
        </tr>
        <tr>
            <td style="text-align:right;"><asp:Label ID="Label5" runat="server" Text="<%$ Resources:GlobalLanguage,BankCardNo %>"></asp:Label></td>
            <td><asp:TextBox ID="popTxtBankCardNo" runat="server" CssClass="TextBox"></asp:TextBox></td></td>
        </tr>
        <tr>
            <td style="text-align:right;"><asp:Label ID="Label7" runat="server" Text="<%$ Resources:GlobalLanguage,AccountHolder %>"></asp:Label></td>
            <td><asp:TextBox ID="popTxtAccountHolder" runat="server" CssClass="TextBox"></asp:TextBox></td></td>
        </tr>
    </table>
    <br />
    <table class="NoBorderTable" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:GlobalLanguage,Save %>" Width="70px"  CausesValidation="true" CssClass="Button" onclick="btnSave_Click"/>
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
</ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    $(function () {
        load();
        LoadPageStyle();
    })

    function EndRequestHandler() {
        LoadPageStyle();
    }

    function LoadPageStyle() {
        $('.UserFundStyle tr td').each(function () {
            $(this).css('text-align', 'right');
        })
    }
</script>
</asp:Content>
