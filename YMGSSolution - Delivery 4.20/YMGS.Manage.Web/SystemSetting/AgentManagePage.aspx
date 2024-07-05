<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="AgentManagePage.aspx.cs" Inherits="YMGS.Manage.Web.SystemSetting.AgentManagePage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
<asp:UpdatePanel ID="upl" runat="server">
<ContentTemplate>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <fieldset>
                <legend>查询条件</legend>
                    <table class="NoBorderTable" style="margin-top:5px;margin-bottom:5px; width:100%">
                        <tr>
                            <td style="width:10px"></td>
                            <td style="width:60px">
                                <span>代理类型</span>
                            </td>
                            <td style="width:90px">
                                <asp:DropDownList ID="drpAgentType" runat="server" Width="150px" CssClass="DropdownList" ></asp:DropDownList>
                            </td>
                            <td style="width:10px"></td>
                            <td style="width:60px">
                                <span>用户名</span>
                            </td>
                            <td style="width:90px">
                                <asp:TextBox ID="txtUserName" runat="server"  CssClass="TextBox"></asp:TextBox>
                            </td>
                            <td style="width:10px"></td>
                            <td style="width:60px">
                                <span>所属代理人</span>
                            </td>
                            <td style="width:90px">
                                <asp:TextBox ID="txtAgentName" runat="server"  CssClass="TextBox"></asp:TextBox>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnQueryEvent" runat="server" Width="70px" CssClass="Button" CausesValidation="false" Text="查询" onclick="btnQuery_Click" />
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
            <asp:GridView ID="gdvAgent" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%"
                EmptyDataText="请输入合适的查询条件代理数据!" OnRowDataBound="gdvAgent_RowDataBind" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="ROLE_NAME" HeaderText="代理角色" />
                    <asp:BoundField DataField="USER_NAME" HeaderText="姓名" />
                    <asp:BoundField DataField="LOGIN_NAME" HeaderText="用户名" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="left" ItemStyle-Width="15%" HeaderText="返点率">
                        <ItemTemplate>
                            <asp:Label ID="lblBrokerage" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Member_Count" HeaderText="可发展会员人数" />
                    <asp:BoundField DataField="agentName" HeaderText="所属代理" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120" HeaderText="下属会员">
                        <ItemTemplate>
                            <asp:LinkButton ID="hlBelongMember" Text="查看" runat="server" CssClass="HyperLink" CausesValidation="false" OnClick="ViewMember_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250" HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="hlEdit" Text="编辑" runat="server" CssClass="HyperLink" CausesValidation="false" OnClick="EditAgent_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlCancleAgent" Text="取消代理" runat="server" CssClass="HyperLink" CausesValidation="false" OnClick="CancleAgent_Click"></asp:LinkButton>
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
        <span>代理管理详细页面</span>
    </asp:Panel>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
        <tr>
            <td style="width:30px; height:30px; line-height:30px;"></td>
            <td style="width:60px; text-align:right;">
                <span>代理角色</span>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="txtAgentRole" runat="server" Disabled="true" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:30px; height:30px; line-height:30px;"></td>
            <td style="width:60px; text-align:right;">
                <span>姓名</span>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="txtAgentUserName" runat="server" Disabled="true" CssClass="TextBox" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:30px; height:30px; line-height:30px;"></td>
            <td style="width:60px; text-align:right;">
                <span>返点率</span>
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
                <span>可发展会员人数</span>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="txtAgentMemberCount" runat="server" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="可发展会员人数必须为非负整数" ClientValidationFunction="ValidateInteger" style="display:none;"></asp:CustomValidator>
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
                <asp:Button ID="btnSave" runat="server" Text="保存" Width="70px"  CausesValidation="true" CssClass="Button" OnClick="BtnSave_Click"/>
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
<asp:Button ID="btnFakeAgent" runat="server" style="display:none" />
<asp:Panel ID="pnlMember" runat="server" style="width:600px;height:auto;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlMemberHeader" runat="server" CssClass="ModalPoupHeader">
        <span>代理详细信息</span>
    </asp:Panel>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
    </table>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td>
                <asp:GridView ID="gdvMember" runat="server" AutoGenerateColumns="false" 
                    GridLines="None" CssClass="GridView" Width="100%"
                    EmptyDataText="没有下属会员信息">
                    <EmptyDataRowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:BoundField DataField="USER_NAME" HeaderText="用户名" />
                        <asp:BoundField DataField="EMAIL_ADDRESS" HeaderText="邮件地址" />
                        <asp:BoundField DataField="RoleName" HeaderText="角色" />
                        <asp:BoundField DataField="ACCOUNT_STATUS" HeaderText="状态" />
                    </Columns>
                </asp:GridView>
                <YMGS:PageNavigator ID="pageNavigatorMember" runat="server" />
            </td>
        </tr>
    </table>
    <table class="NoBorderTable" width="100%">
    <tr>
        <td align="center">
            <asp:Button ID="btnBack" runat="server" Text="返回" Width="70px" CausesValidation="false" CssClass="Button" />
        </td>
    </tr>
    </table>
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopupMember" BehaviorID="mdlPopupMember" 
    TargetControlID="btnFakeAgent"
    PopupControlID="pnlMember"
    BackgroundCssClass="ModalPopupBackground"                         
    CancelControlID="btnBack" 
    PopupDragHandleControlID="pnlMemberHeader">
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
