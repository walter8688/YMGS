<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="CommissionManagePage.aspx.cs" Inherits="YMGS.Manage.Web.GameSettle.CommissionManagePage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
<asp:UpdatePanel ID="upl" runat="server">
<ContentTemplate>
<table class="NoBorderTable" width="100%">
        <tr>
            <td align="right">
                <%--<asp:Button ID="btnQueryEvent" CssClass="Button" Width="70px"  runat="server"  CausesValidation="false" Text="查询" onclick="btnQuery_Click" />--%>
                <asp:Button ID="btnNew" runat="server" CssClass="Button" Width="70px" CausesValidation="false" Text="新增" OnClick="btnNew_Click" />
            </td>
        </tr>
    </table>
<table class="NoBorderTable" width="100%">
        <tr>
            <td>
                <asp:GridView ID="gdvBrokerage" runat="server" AutoGenerateColumns="false" 
                    GridLines="None" CssClass="GridView" Width="100%" DataKeyNames="Brokerage_Rate_ID"
                    EmptyDataText="请输入合适的查询条件查询佣金率!" OnRowDataBound="gdvBrokerage_RowDataBind" >
                    <EmptyDataRowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35%" HeaderText="佣金比率">
                            <ItemTemplate>
                                <asp:Label ID="lblBrokerageRate" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35%" HeaderText="积分累计">
                            <ItemTemplate>
                                <asp:Label ID="lblIntegral" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%" HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="hlEdit" Text="编辑" runat="server" CssClass="HyperLink" CausesValidation="false" onclick="btnEidt_Click"></asp:LinkButton>
                                <asp:LinkButton ID="hlDelete" CssClass="HyperLink" CausesValidation="false" OnClientClick="return showConfirm('确定删除?');" runat="server" Text="删除" onclick="btnDelete_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <YMGS:PageNavigator ID="pageNavigator" runat="server" />
            </td>
        </tr>
    </table>
<asp:TextBox ID="hidTxtBrokerageID" runat="server" style="display:none;"></asp:TextBox>
<asp:Button ID="btnFake" runat="server" style="display:none" />
<asp:Panel ID="pnlPopup" runat="server" style="width:200px;height:250px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
        <span>佣金管理详细页面</span>
    </asp:Panel>
    <table class="NoBorderTable" width="100%" id="popEventTeamTbl">
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
        <tr>
            <td style="width:20px"></td>
            <td style="width:70px; text-align:right;">
                <span style="color:Red;">*</span><span>佣金率(百分比)</span>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="txtBrokerage" runat="server"  CssClass="TextBox" MaxLength="50"></asp:TextBox> 
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                 TargetControlID="txtBrokerage"
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
            <td style="width:20px"></td>
            <td style="width:70px; text-align:right;">
                <span style="color:Red;">*</span><span>积分下限</span>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="txtIntegralAbove" runat="server"  CssClass="TextBox" MaxLength="50"></asp:TextBox> 
            </td>
        </tr>
        <tr>
            <td style="width:20px"></td>
            <td style="width:70px; text-align:right;">
                <span style="color:Red;">*</span><span>积分上限</span>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="txtIntegralBelow" runat="server"  CssClass="TextBox" MaxLength="50"></asp:TextBox> 
            </td>
        </tr>
    </table>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBrokerage" ErrorMessage="佣金率不能为空!" style="display:none;"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIntegralAbove" ErrorMessage="积分下限不能为空!" style="display:none;"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtIntegralBelow" ErrorMessage="积分上限不能为空!" style="display:none;"></asp:RequiredFieldValidator>
    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="积分必须为非负整数" ClientValidationFunction="ValidateInteger" style="display:none;"></asp:CustomValidator>
    
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
                <asp:Button ID="btnSave" runat="server" Text="保存" Width="70px"  CausesValidation="true" CssClass="Button" onclick="btnSave_Click"/>
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="取消" Width="70px" CausesValidation="false" CssClass="Button" />
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
        var txtMinIntegral = $('[id$=txtIntegralAbove]').val();
        var txtMaxIntegral = $('[id$=txtIntegralBelow]').val();
        if (!isPositiveInteger(txtMinIntegral) || !isPositiveInteger(txtMaxIntegral)) {
            args.IsValid = false;
            return;
        }
        return false;
    }
</script>
</asp:Content>
