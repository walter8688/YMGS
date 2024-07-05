<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="YMGS.Manage.Web.SystemSetting.UserManage" %>
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
                                        <span>姓名</span>
                                    </td>
                                    <td style="width: 120px">
                                        <asp:TextBox CssClass="TextBox" ID="txtUserName" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 20px">
                                    </td>
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
                                    <td align="right">
                                        <asp:Button runat="server" ID="btnSearch" Width="70px" Text="查询" CausesValidation="false"
                                            CssClass="Button" OnClick="btnSearch_Click" />
                                        &nbsp;<asp:Button runat="server" Visible=false ID="btnNew" Width="70px" Text="新增" CssClass="Button"
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
                         OnRowDataBound="gdvMain_RowDataBound"   CssClass="GridView" Width="100%" DataKeyNames="ROLE_ID" EmptyDataText="请输入合适的查询条件查询会员账户信息!">
                            <EmptyDataRowStyle HorizontalAlign="Left" />
                            <Columns>
                                <asp:BoundField DataField="USER_ID" HeaderText="用户ID" />
                                <asp:BoundField DataField="USER_NAME" HeaderText="姓名" />
                                <asp:BoundField DataField="LOGIN_NAME" HeaderText="用户名" />
                                <asp:BoundField DataField="ROLE_ID" HeaderText="角色ID" Visible="false" />
                                <asp:BoundField DataField="ROLE_NAME" HeaderText="角色" />
                                <asp:BoundField DataField="ACCOUNT_STATUS" Visible="false" HeaderText="状态" />
                                <asp:BoundField DataField="ACCOUNT_STATUS" HeaderText="状态" />
                                <asp:BoundField DataField="CREATE_DATE" HeaderText="创建时间" />
                                <asp:BoundField DataField="CUR_FUND" HeaderText="当前资金" />
                                <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:LinkButton OnClick="btnNew_Click" ID="btnEdit" CommandArgument='<%# Eval("USER_ID") %>' runat="server" Text="编辑" CausesValidation="false"></asp:LinkButton>
                                        <asp:LinkButton OnClick="btnEditFund_Click" ID="btnEditFund" CommandArgument='<%# Eval("USER_ID") %>' CommandName='<%# Eval("USER_NAME") %>' runat="server" Text="修改资金" CausesValidation="false"></asp:LinkButton>
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
                    <span>用户管理详细页面</span>
                </asp:Panel>
                <table class="NoBorderTable" width="100%">
                    <tr>
                        <td colspan="3" style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40px; height: 30px; line-height: 30px;">
                        </td>
                        <td style="width: 50px">
                            <span>用户名</span>
                        </td>
                        <td style="width: 90px">
                            <asp:TextBox ID="User_NAME" Width="145px" Enabled=false runat="server" MaxLength=40></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 40px; height: 30px; line-height: 30px;">
                        </td>
                        <td style="width: 50px">
                            <span>
                                <asp:Label ID="lblstatus" runat="server" Text="状态"></asp:Label></span>
                        </td>
                        <td style="width: 90px">
                            <asp:DropDownList ID="ddlstatus"  Width="150px" runat="server">
                           
                            <asp:ListItem Text="活动" Value="1"></asp:ListItem>
                               <asp:ListItem Text="锁定" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40px; height: 30px; line-height: 30px;">
                        </td>
                        <td style="width: 50px;">
                            <span>角色</span>
                        </td>
                        <td style="width: 90px;">
                            <asp:DropDownList ID="ddlRole"  Width="150px" runat="server" >
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                ControlToValidate="ddlRole" ErrorMessage="请选择角色" Display="None" 
                                SetFocusOnError="True" ClientValidationFunction="Validate"></asp:CustomValidator>
                        </td>
                    </tr>  <tr>
                        <td style="width: 55px">
                        </td>
                        <td colspan=2 style="color: Red">
                            <asp:ValidationSummary runat="server" ShowMessageBox="false" ShowSummary="true" ID="Validationsummary1" />
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 40px; height: 30px; line-height: 30px;">
                            <asp:TextBox ID="txtuserID" Visible="false" runat="server"></asp:TextBox>

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
            <asp:Button ID="btnFakeFund" runat="server" Style="display: none" />
            <asp:Panel ID="pnlFundPopup" runat="server" Height="160px" Style=" display: none"
                CssClass="ModalPoup">
                <asp:Panel ID="pnlFundPopupHeader" runat="server" CssClass="ModalPoupHeader">
                    <span>用户资金详细页面</span>
                </asp:Panel>
                <table class="NoBorderTable" width="100%">
                    <tr>
                        <td colspan="3" style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40px; height: 30px; line-height: 30px;">
                        </td>
                        <td style="width: 40px">
                            <span>用户名</span>
                        </td>
                        <td style="width: 90px">
                            <asp:TextBox ID="txtCurUserName" Width="145px" Enabled=false runat="server" MaxLength=40></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40px; height: 30px; line-height: 30px;">
                        </td>
                        <td style="width: 40px">
                            <span>资金</span>
                        </td>
                        <td style="width: 90px">
                            <asp:TextBox ID="txtCurUserFund" Width="145px" runat="server" MaxLength=40></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center"><div id="errMsg" style="display:none;color:Red;">资金必须为非负数值</div></td>
                    </tr>                   
                </table>
                <table class="NoBorderTable" width="100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="BtnFundSave" runat="server" Text="保存" Width="70px" CausesValidation="false"
                                CssClass="Button" OnClick="BtnFundSave_Click" OnClientClick="return ValidateFund();" />
                            &nbsp;
                            <asp:Button ID="BtnFundCancle" runat="server" Text="取消" Width="70px" CausesValidation="false"
                                CssClass="Button" />
                        </td>
                    </tr>
                </table>
                <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlFundPopup" BehaviorID="mdlFundPopup"
                    TargetControlID="btnFakeFund" PopupControlID="pnlFundPopup" BackgroundCssClass="ModalPopupBackground"
                    CancelControlID="BtnFundCancle" PopupDragHandleControlID="pnlFundPopupHeader">
                </ajaxToolkit:ModalPopupExtender>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">

        function Validate(source, args) {
           // alert(document.getElementById("<%=ddlRole.ClientID %>")[0].selected);
            if (document.getElementById("<%=ddlRole.ClientID %>")[0].selected ==true) {

                args.IsValid = false;
            }
        }

        function ValidateFund() {
            var curFund = $('[id$=txtCurUserFund]').val();
            if (!checkUserFund(curFund)) {
                $('#errMsg').show();
                return false;
            }
            $('#errMsg').hide();
            return true;
        }

</script>
</asp:Content>
