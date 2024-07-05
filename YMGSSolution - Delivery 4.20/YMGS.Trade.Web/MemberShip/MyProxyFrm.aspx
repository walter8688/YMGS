<%@ Page Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master" AutoEventWireup="true"
    CodeBehind="MyProxyFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.MyProxyFrm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <asp:UpdatePanel ID="upl" runat="server">
        <ContentTemplate>
            <table class="NoBorderTable" width="100%">
                <tr>
                    <td>
                        <table class="NoBorderTable" style="margin-top: 5px; margin-bottom: 5px; width: 100%">
                            <tr>
                                <td style="width: 60px">
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:GlobalLanguage,BeginDate %>"></asp:Label>
                                </td>
                                <td style="width: 80px">
                                    <div class="calendarContainer">
                                        <YMGS:AjaxCalendar ID="calStartDate" runat="server" NeedCalendarButton="true" />
                                    </div>
                                </td>
                                <td style="width: 10px">
                                </td>
                                <td style="width: 60px">
                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:GlobalLanguage,EndDate %>"></asp:Label>
                                </td>
                                <td style="width: 80px">
                                    <div class="calendarContainer">
                                        <YMGS:AjaxCalendar ID="calEndDate" runat="server" NeedCalendarButton="true" />
                                    </div>
                                </td>
                                <td style="width: 10px">
                                </td>
                                <td style="width: 40px">
                                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:GlobalLanguage,ActiveStatus %>"></asp:Label>
                                </td>
                                <td style="width: 80px">
                                    <div class="calendarContainer">
                                        <asp:DropDownList ID="ddlApplyStatus" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td colspan="2" align="right">
                                    <asp:Button ID="btnQuery" Width="70px" CssClass="Button" runat="server" CausesValidation="false"
                                        Text="<%$ Resources:GlobalLanguage,Query %>" OnClick="BtnQuery_Click" />&nbsp;
                                    <asp:Button ID="btnAddApplyProxy" Width="70px" CssClass="Button" runat="server" CausesValidation="false"
                                        Text="<%$ Resources:GlobalLanguage,ApplyFor %>" OnClick="btnAddApplyProxy_Click" />&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table class="NoBorderTable" width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="gdvApplyProxy" runat="server" DataKeyNames="Apply_Proxy_ID" AutoGenerateColumns="false"
                            GridLines="None" CssClass="GridView" Width="100%" EmptyDataText="<%$ Resources:GlobalLanguage,NoSuchData %>"
                            OnRowDataBound="gdvApplyProxy_RowDataBound" OnRowCommand="gdvApplyProxy_RowCommand">
                            <EmptyDataRowStyle HorizontalAlign="Left" />
                            <Columns>
                                <asp:BoundField DataField="Apply_Proxy_ID" Visible="false" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" HeaderText="<%$ Resources:GlobalLanguage,AgentLevel %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRoleLevel" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="User_Country" ItemStyle-Width="100" HeaderText="<%$ Resources:GlobalLanguage,BelongCountry %>" />
                                <asp:BoundField DataField="User_Province" ItemStyle-Width="100" HeaderText="<%$ Resources:GlobalLanguage,BelongProvince %>" />
                                <asp:BoundField DataField="User_City" ItemStyle-Width="100" HeaderText="<%$ Resources:GlobalLanguage,BelongCity %>" />
                                <asp:BoundField DataField="User_BankAddress" ItemStyle-Width="100" HeaderText="<%$ Resources:GlobalLanguage,BankAddress %>" />
                                <asp:BoundField DataField="User_BankNO" ItemStyle-Width="100" HeaderText="<%$ Resources:GlobalLanguage,BankNum %>" />
                                <asp:BoundField DataField="User_Telephone" ItemStyle-Width="100" HeaderText="<%$ Resources:GlobalLanguage,TelephoneNo %>" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150" HeaderText="<%$ Resources:GlobalLanguage,ActiveStatus %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200" HeaderText="<%$ Resources:GlobalLanguage,ApplyDate %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApplyDate" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" HeaderText="<%$ Resources:GlobalLanguage,Action %>">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnCancleApply" Text="<%$ Resources:GlobalLanguage,Cancel %>"
                                            runat="server" CssClass="HyperLink" CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <YMGS:PageNavigator ID="pageNavigator" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnFakeFund" runat="server" Style="display: none" />
            <asp:Panel ID="pnlApplyProxyPopup" runat="server" Height="280px" Style="display: none"
                CssClass="ModalPoup">
                <asp:Panel ID="pnlApplyProxyPopupHeader" runat="server" CssClass="ModalPoupHeader">
                    <span>
                        <asp:Label ID="lblApplyProxyDetailsHeader" Text="<%$ Resources:GlobalLanguage,MyApplyProxyManage %>"
                            runat="server"></asp:Label>
                    </span>
                </asp:Panel>
                <table class="NoBorderTable" width="100%">
                    <tr>
                        <td style="height: 10px;">
                            <asp:Label ID="lblChosseProxy" Text="<%$ Resources:GlobalLanguage,AgentLevel %>"
                                runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProxyData" Width="50%" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; height: 10px;">
                            <asp:Label ID="lblUser_Telephone" Text="<%$ Resources:GlobalLanguage,TelephoneNo %>"
                                runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUser_Telephone" Width="98%" MaxLength="50" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; height: 10px;">
                            <asp:Label ID="lblUser_Country" Text="<%$ Resources:GlobalLanguage,BelongCountry %>"
                                runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUser_Country" Width="98%" MaxLength="50" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; height: 10px;">
                            <asp:Label ID="lblUser_Province" Text="<%$ Resources:GlobalLanguage,BelongProvince %>"
                                runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUser_Province" Width="98%" MaxLength="50" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; height: 10px;">
                            <asp:Label ID="lblUser_City" Text="<%$ Resources:GlobalLanguage,BelongCity %>" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUser_City" Width="98%" MaxLength="50" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; height: 10px;">
                            <asp:Label ID="lblUser_BankAddress" Text="<%$ Resources:GlobalLanguage,BankAddress %>"
                                runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUser_BankAddress" Width="98%" MaxLength="50" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; height: 10px;">
                            <asp:Label ID="lblUser_BankNO" Text="<%$ Resources:GlobalLanguage,BankNum %>" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUser_BankNO" Width="98%" MaxLength="50" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:CheckBox ID="chkAgree" AutoPostBack="true" OnCheckedChanged="chkAgree_CheckedChanged"
                                runat="server" Text="<%$ Resources:GlobalLanguage,AgreeBFB %>" />
                            <asp:HyperLink ID="HyperLink1" Text="<%$ Resources:GlobalLanguage,AgentRule %>" runat="server"
                                ForeColor="#3333FF"></asp:HyperLink>
                        </td>
                    </tr>
                </table>
                <table class="NoBorderTable" width="100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnToApplyProxy" runat="server" Text="<%$ Resources:GlobalLanguage,ApplyFor %>"
                                Width="70px" CausesValidation="true" OnClick="btnToApplyProxy_Click" CssClass="Button" />
                            <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:GlobalLanguage,close %>"
                                Width="70px" CausesValidation="false" CssClass="Button" />
                        </td>
                    </tr>
                </table>
                <asp:CustomValidator ID="CVRole" runat="server" ErrorMessage="<%$ Resources:GlobalLanguage,AgentLevelNotNull %>"
                    ClientValidationFunction="ValidateAgentLevel" Style="display: none;"></asp:CustomValidator>
                <asp:CustomValidator ID="CVUser_Telephone" runat="server" ErrorMessage="<%$ Resources:GlobalLanguage,TelephoneNotNull %>"
                    ClientValidationFunction="ValidateUserTel" Style="display: none;"></asp:CustomValidator>
                <table class="NoBorderTable" width="100%">
                    <tr>
                        <td style="color: Red; text-align: center;">
                            <asp:ValidationSummary runat="server" ShowMessageBox="false" ShowSummary="true" ID="ValidationSummary1" />
                        </td>
                    </tr>
                </table>
                <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlApplyProxyPopup" BehaviorID="mdlApplyProxyPopup"
                    TargetControlID="btnFakeFund" PopupControlID="pnlApplyProxyPopup" BackgroundCssClass="ModalPopupBackground"
                    CancelControlID="btnCancel" PopupDragHandleControlID="pnlApplyProxyPopupHeader">
                </ajaxToolkit:ModalPopupExtender>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ValidateAgentLevel(source, args) {
            var proxyData = $('[id$=ddlProxyData]').val();
            if (proxyData == "-1") {
                args.IsValid = false;
                return;
            }
            else {
                args.IsValid = true;
                return;
            }
        }
        function ValidateUserTel(source, args) {
            var name = $('[id$=txtUser_Telephone]').val();
            if (name == "") {
                args.IsValid = false;
                return;
            }
            else {
                args.IsValid = true;
                return;
            }
        }
    </script>
</asp:Content>
