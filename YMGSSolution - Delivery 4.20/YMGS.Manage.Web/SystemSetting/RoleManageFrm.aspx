<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true"
    CodeBehind="RoleManageFrm.aspx.cs" Inherits="YMGS.Manage.Web.SystemSetting.RoleManageFrm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
    <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" class="NoBorderTable">
                <tr>
                    <td>
                        <fieldset>
                            <legend>查询条件</legend>
                            <table class="NoBorderTable" style="margin-top: 5px; margin-bottom: 5px; width: 100%">
                                <tr>
                                    <td style="width: 70px">  
                                        <span>角色名称</span>
                                    </td>
                                    <td style="width: 120px">
                                        <asp:TextBox CssClass="TextBox" ID="txtRoleName" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 20px">
                                    </td>
                                    <td align="right">
                                        <asp:Button runat="server" ID="btnSearch" Width="70px" Text="查询" CausesValidation="false"
                                            CssClass="Button" OnClick="btnSearch_Click" />
                                        &nbsp;<asp:Button runat="server" ID="btnNew" Width="70px" Text="新增" CssClass="Button"
                                            CausesValidation="false" OnClick="btnNew_Click" />
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
                        <asp:GridView ID="gdvMain" runat="server" AutoGenerateColumns="false" GridLines="None"
                         OnRowDataBound="gdvMain_RowDataBound"   CssClass="GridView" Width="100%" DataKeyNames="ROLE_ID" EmptyDataText="请输入合适的查询条件查询市场模板!">
                            <EmptyDataRowStyle HorizontalAlign="Left" />
                            <Columns>
                                <asp:BoundField DataField="ROLE_ID" HeaderText="角色ID" />
                                <asp:BoundField DataField="ROLE_NAME" HeaderText="角色名称" />
                                <asp:BoundField DataField="ROLE_DESC" HeaderText="角色描述" />
                                <asp:BoundField DataField="createrName" HeaderText="创建者" />
                                <asp:BoundField DataField="CREATE_TIME" HeaderText="创建日期" />
                                <asp:BoundField DataField="laster" HeaderText="更新人" />
                                <asp:BoundField DataField="LAST_UPDATE_TIME" HeaderText="更新日期" />
                                <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:LinkButton OnClick="btnNew_Click" ID="btnEdit" CommandArgument='<%# Eval("ROLE_ID") %>' runat="server" Text="编辑" CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="删除"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:LinkButton OnClick="btnNew_Click" ID="btnDelete" CommandName="del" CommandArgument='<%# Eval("ROLE_ID") %>' runat="server" Text="删除" CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <YMGS:PageNavigator ID="PageNavigator1" PageSize="20" runat="server" OnPageIndexChanged="PageNavigator1_PageIndexChanged" />
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnFake" runat="server" Style="display: none" />
            <asp:Panel ID="pnlPopup" runat="server" Style=" display: none"
                CssClass="ModalPoup">
                <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
                    <span>角色管理详细页面</span>
                </asp:Panel>
                <table class="NoBorderTable" width="100%">
                    <tr>
                        <td colspan="3" style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40px; height: 30px; line-height: 30px;">
                        </td>
                        <td style="width: 60px">
                            <span style="color:Red;">*</span><span>角色名称</span>
                        </td>
                        <td style="width: 90px">
                            <asp:TextBox ID="ROLE_NAME" runat="server" MaxLength=40></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入角色名称" ControlToValidate="ROLE_NAME" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 40px; height: 30px; line-height: 30px;">
                        </td>
                        <td style="width: 60px">
                            <span style="color:Red;">*</span><span>角色描述</span>
                        </td>
                        <td style="width: 90px">
                            <asp:TextBox ID="roledesc" runat="server" MaxLength=100></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入角色描述" ControlToValidate="roledesc" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40px; height: 30px; line-height: 30px;">
                        </td>
                        <td style="width: 60px; text-align:right;">
                            <span>角色权限</span>
                        </td>
                        <td style="width: 90px;">
                        <div style="width:230px; height:220px; overflow:auto;">
                            <asp:TreeView ID="tvFunc" runat="server" 
                                ShowCheckBoxes="All" 
                                ExpandDepth="0" onclick="OnTreeNodeChecked()"
                               >
                            </asp:TreeView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40px; height: 30px; line-height: 30px;">
                            <asp:TextBox ID="txtroleID" Visible="false" runat="server"></asp:TextBox>
                        </td>
                      
                    </tr>
                     <tr>
                        <td style="width: 55px">
                        </td>
                        <td colspan=2 style="color: Red">
                            <asp:ValidationSummary runat="server" ShowMessageBox="false" ShowSummary="true" ID="Validationsummary1" />
                        </td>

                    </tr>
                </table>
              
                <table class="NoBorderTable" width="100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" Text="保存" Width="70px" CausesValidation="true"
                                CssClass="Button" OnClick="btnSave_Click" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="取消" Width="70px" CausesValidation="false"
                                CssClass="Button" />
                        </td>
                    </tr>
                </table>
                <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" BehaviorID="mdlPopup"
                    TargetControlID="btnFake" PopupControlID="pnlPopup" BackgroundCssClass="ModalPopupBackground"
                    CancelControlID="btnCancel" PopupDragHandleControlID="pnlPopupHeader">
                </ajaxToolkit:ModalPopupExtender>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function OnTreeNodeChecked() {
            var eleChecked = false;
            var ele = event.srcElement;
            if (ele.type == 'checkbox') {
                eleChecked = ele.checked;    
                //获取div的id
                var eleDivId = ele.id.replace('CheckBox', 'Nodes');
                var eleDiv = document.getElementById(eleDivId);
                if (eleDiv == null || typeof (eleDiv) == "undefined") return;
             
                //循环下面子CheckBox
                var eleCheckBoxs = eleDiv.getElementsByTagName('input');
                for (var i = 0; i < eleCheckBoxs.length; i++) {
                    if (eleCheckBoxs[i].type == 'checkbox') {
                        eleCheckBoxs[i].checked = eleChecked;
                    }
                }
            }
        }
    </script>
</asp:Content>
