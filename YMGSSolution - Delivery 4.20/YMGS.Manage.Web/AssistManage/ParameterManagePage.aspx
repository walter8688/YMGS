<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/BetBase.Master" CodeBehind="ParameterManagePage.aspx.cs" Inherits="YMGS.Manage.Web.AssistManage.ParameterManagePage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">  
<asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<table width="100%" class="NoBorderTable">
    <tr>
        <td>
            <fieldset>
                <legend>查询条件</legend>
                    <table class="NoBorderTable" style="margin-top:5px;margin-bottom:5px;width:100%">
                        <tr>
                            <td style="width:20px"></td>
                            <td style="width:70px">
                                <span>参数类别</span>
                            </td>
                            <td style="width:90px">
                                <asp:DropDownList ID="drpParamType" runat="server" Width="151px" Height="18px" CssClass="DropdownList"></asp:DropDownList>
                            </td>
                            <td style="width:20px"></td>
                            <td style="width:70px">
                                <span>参数名称</span>
                            </td>
                            <td style="width:90px">
                                 <asp:TextBox ID="txtParamName" runat="server"  CssClass="TextBox" MaxLength="100"></asp:TextBox> 
                            </td>
                            <td style="width:20px">
                            </td>
                            <td align="right">
                                <asp:Button ID="btnQueryParam" Width="70px" CssClass="Button" runat="server" CausesValidation=false Text="查询" onclick="btnQueryParam_Click" />
                                &nbsp;<asp:Button ID="btnAddParam" runat="server" Width="70px" Text="新增" CausesValidation=false CssClass="Button" onclick="btnAddParam_Click" />
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
            <asp:GridView ID="gdvMain" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%" DataKeyNames="PARAM_ID"
                OnRowDataBound="gdvMain_RowDataBound" EmptyDataText="请输入合适的查询条件查询参数类型!">
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="PARAM_TYPE_NAME" HeaderText="参数类型" />
                    <asp:BoundField DataField="PARAM_NAME" HeaderText="参数名称" />
                    <asp:BoundField DataField="IS_USE" HeaderText="是否使用" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200" HeaderText="操作">
                        <ItemTemplate>
                            <asp:Label ID="lblParamType" runat="server" Text='<%#Eval("PARAM_TYPE") %>' style="display:none;"></asp:Label>
                            <asp:LinkButton ID="hlUp" runat="server" CausesValidation="false" Text="上移" CssClass="HyperLink"  OnClick="btnParamOrderUp_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlDown" runat="server" CausesValidation="false" Text="下移" CssClass="HyperLink"  OnClick="btnParamOrderDown_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlEdit" Text="编辑" runat="server" CausesValidation="false" CssClass="HyperLink" onclick="btnAddParam_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlDelete" CssClass="HyperLink" runat="server" CausesValidation="false" Text="删除" OnClick="btnParamDelete_Click" OnClientClick="return showConfirm('确定删除?');"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
<asp:Button ID="btnFake" runat="server" style="display:none" />
<asp:Panel ID="pnlPopup" runat="server" style="width:300px;height:200px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
        <span>参数管理详细页面</span>
    </asp:Panel>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
        <tr>
            <td style="width:40px; height:30px; line-height:30px;"></td>
            <td style="width:50px">
                <span>参数类别</span>
            </td>
            <td style="width:90px">
                <asp:DropDownList ID="popupDrpParamType" Width="151px" Height="18px" runat="server" CssClass="DropdownList"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:40px; height:30px; line-height:30px;"></td>
            <td style="width:50px;">
                <span>参数名称</span>
            </td>
            <td style="width:90px;">
                <asp:TextBox ID="popupTxtParamName" runat="server"  CssClass="TextBox" MaxLength="100"></asp:TextBox><span>*</span> 
            </td>
        </tr>
        <tr>
            <td style="width:40px; height:30px; line-height:30px;"><asp:TextBox ID="txtHiddenParamID" Visible="false" runat="server"></asp:TextBox></td>
            <td style="width:50px">
                <span>是否使用</span>
            </td>
            <td style="width:90px">
                <asp:CheckBox ID="popupCkcInUse" runat="server" Text="是否使用" Checked="true"/>
            </td>
        </tr>
    </table>
    <asp:CustomValidator ID="valParamName" runat="server" Display=None ErrorMessage="参数名称不能为空!"
                 ClientValidationFunction="ValidateParamName"></asp:CustomValidator>
    <table class=NoBorderTable width=100%>
        <tr>
            <td style="width:55px">
            </td>
            <td style="color:Red">
                    <asp:ValidationSummary runat="server" ShowMessageBox="false" ShowSummary="true" ID="Validationsummary1" />
            </td>
        </tr>
    </table>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text="保存" Width="70px"  CausesValidation="true" CssClass="Button"  OnClick="btnSave_Click"/>
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
    function ValidateParamName(source, args) {
        if (chkIsNull('popupTxtParamName')) {
            args.IsValid = false;
            return;
        }
        else {
            args.IsValid = true;
        }
    }
</script>
</asp:Content>
