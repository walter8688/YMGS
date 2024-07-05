<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master" AutoEventWireup="true" CodeBehind="SetAgentDeatailFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.SetAgentDeatailFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
<asp:UpdatePanel ID="upl" runat="server">
<ContentTemplate>
<table class="NoBorderTable" width="100%">
    <tr><td style="text-align:left; font-weight:bolder;"><asp:Label ID="lblGrowAgentTitle" runat="server" Text="<%$ Resources:GlobalLanguage,SetAgentDeatailTitle %>"></asp:Label></td></tr>    
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td align="right">
        <asp:LinkButton id="btnGrowMember" runat="server" Text="<%$ Resources:GlobalLanguage,btnGrowMember %>" CausesValidation="false" OnClick="GrowMember_Click"></asp:LinkButton>
        <asp:LinkButton id="btnHandleAgent" runat="server" Text="<%$ Resources:GlobalLanguage,HandleAgent %>" CausesValidation="false" OnClick="HandleAgent_Click"></asp:LinkButton>
        </td>
    </tr>    
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gdvAgent" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%"
                EmptyDataText="<%$ Resources:GlobalLanguage,NoAgentData %>" OnRowDataBound="gdvAgent_RowDataBind" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="ROLE_NAME" HeaderText="<%$ Resources:GlobalLanguage,AgentRole %>" />
                    <asp:BoundField DataField="LOGIN_NAME" HeaderText="<%$ Resources:GlobalLanguage,AgentUserName %>" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="left" ItemStyle-Width="15%" HeaderText="<%$ Resources:GlobalLanguage,BrokerageRate %>">
                        <ItemTemplate>
                            <asp:Label ID="lblBrokerage" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Member_Count" HeaderText="<%$ Resources:GlobalLanguage,GrowMemberNum %>" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250" HeaderText="<%$ Resources:GlobalLanguage,Action %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="hlEdit" Text="<%$ Resources:GlobalLanguage,Edit %>" runat="server" CssClass="HyperLink" CausesValidation="false" OnClick="EditAgent_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
<asp:TextBox ID="hidTxtUserID" runat="server" style="display:none;"></asp:TextBox>
<asp:Button ID="btnFake" runat="server" style="display:none" />
<asp:Panel ID="pnlPopup" runat="server" style="width:300px;height:260px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:GlobalLanguage,AgentDetail %>"></asp:Label>
    </asp:Panel>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
        <tr>
            <td style="width:30px; height:30px; line-height:30px;"></td>
            <td style="width:60px; text-align:right;">
                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:GlobalLanguage,AgentRole %>"></asp:Label>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="txtAgentRole" runat="server" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:30px; height:30px; line-height:30px;"></td>
            <td style="width:60px; text-align:right;">
                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:GlobalLanguage,AgentUserName %>"></asp:Label>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="txtAgentUserName" runat="server" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:30px; height:30px; line-height:30px;"></td>
            <td style="width:60px; text-align:right;">
                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:GlobalLanguage,BrokerageRate %>"></asp:Label>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="txtAgentBrokerage" runat="server" CssClass="TextBox"></asp:TextBox>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                 TargetControlID="txtAgentBrokerage"
                Mask="99.99"
                MessageValidatorTip="true"
                OnFocusCssClass="MaskedEditFocus"
                OnInvalidCssClass="MaskedEditError"
                MaskType="Number"
                AcceptAMPM="False"           
                ErrorTooltipEnabled="True" />
            </td>
        </tr>
        <tr>
            <td style="width:30px; height:30px; line-height:30px;"></td>
            <td style="width:60px; text-align:right;">
                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:GlobalLanguage,GrowMemberNum %>"></asp:Label>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="txtAgentMemberCount" runat="server" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Resources:GlobalLanguage,GrowMemberNumErrMsg %>" ClientValidationFunction="ValidateInteger" style="display:none;"></asp:CustomValidator>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td style="color:Red; text-align:center;">
                    <asp:ValidationSummary runat="server" ShowMessageBox="false" ShowSummary="true" ID="Validationsummary1" />
            </td>
        </tr>
    </table>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:GlobalLanguage,Save %>" Width="70px"  CausesValidation="true" CssClass="Button" OnClick="BtnSave_Click"/>
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
    function ValidateInteger(source, args) {
        var agentMemberCount = $('[id$=txtAgentMemberCount]').val();
        if (!isPositiveInteger(agentMemberCount)) {
            args.IsValid = false;
            return;
        }
        return false;
    }
</script>
</asp:Content>
