<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true"
    CodeBehind="OddsCompare.aspx.cs" Inherits="YMGS.Manage.Web.AssistManage.OddsCompare" %>

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
                            <table class="NoBorderTable" style="margin-top: 5px; margin-bottom: 5px; width: 100%">
                                <tr>
                                    <td style="width: 20px">
                                    </td>
                                    <td style="width: 70px">
                                        <span>比赛名称</span>
                                    </td>
                                    <td style="width: 90px">
                                        <asp:TextBox ID="txtmatchName" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 20px">
                                    </td>
                                    <td style="width: 70px">
                                    </td>
                                    <td style="width: 90px">
                                    </td>
                                    <td style="width: 20px">
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnSearch" Width="70px" CssClass="Button" runat="server" CausesValidation="false"
                                            Text="查询" OnClick="btnSearch_Click" />
                                        &nbsp;<asp:Button ID="btnNew" runat="server" Width="70px" Text="新增" CausesValidation="false"
                                            CssClass="Button" OnClick="btnNew_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <table class="NoBorderTable" width="100%">
                <tr>
                    <td class="GridView">
                        <asp:GridView ID="gdvMain" runat="server" AutoGenerateColumns="false" GridLines="None"
                            CssClass="GridView" Width="100%" OnRowDataBound="gdvMain_RowDataBound" EmptyDataText="请输入合适的查询条件查询公告!">
                            <EmptyDataRowStyle HorizontalAlign="Left" />
                            <Columns>
                                <asp:BoundField DataField="MATCHNAME" HeaderText="比赛名称" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="比较项">
                                    <ItemTemplate>
                                        <asp:GridView ID="gdvsubMain" runat="server" AutoGenerateColumns="false" GridLines="None"
                                            CssClass="GridView" Width="100%" OnRowDataBound="gdvsubMain_RowDataBound" EmptyDataText="无记录">
                                            <EmptyDataRowStyle HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="CN_CORP" ItemStyle-Width="200px" HeaderText="比较网站名称" />
                                                <asp:BoundField DataField="PROFIT" ItemStyle-Width="100px" HeaderText="利润" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200" HeaderText="操作">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="hlsubEdit" Text="编辑" CommandArgument='<%#Eval("MATCHID").ToString()+","+Eval("CN_CORP").ToString()%>'
                                                            runat="server" CausesValidation="false" CssClass="HyperLink" OnClick="btnNew_Click"></asp:LinkButton>
                                                        <asp:LinkButton ID="hlsubDelete" CommandArgument='<%#Eval("MATCHID").ToString()+","+Eval("CN_CORP").ToString()%>'
                                                            CssClass="HyperLink" runat="server" CausesValidation="false" Text="删除" OnClick="DelItem_OnClick"
                                                            OnClientClick="return showConfirm('确定删除?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200" HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="hlDelete" CommandArgument='<%# Eval("MATCHID") %>' CssClass="HyperLink"
                                            runat="server" CausesValidation="false" Text="删除比赛比较" OnClick="martchDel_OnClick"
                                            OnClientClick="return showConfirm('确定删除?');"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <YMGS:PageNavigator ID="PageNavigator1" PageSize="20" runat="server" OnPageIndexChanged="PageNavigator1_PageIndexChanged" />
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnFake" runat="server" Style="display: none" />
            <asp:Panel ID="pnlPopup" runat="server" Style="width: 600px; height: 240px; display: none"
                CssClass="ModalPoup">
                <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
                    <span>赔率编辑页面</span><asp:HiddenField ID="hfdpid" runat="server" />
                </asp:Panel>
                <table class="NoBorderTable" width="100%">
                    <tr>
                        <td colspan="4" style="height: 10px; color: Red">
                            提示：每一个比赛比较项需录入本系统默认数据（ 中文赔率比较网站名称："必发必" ,英文赔率比较网站名称"Bestabet",利润自定义）,否则前台无法看到我们公司$10的下注利润
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 10px;">
                            <asp:DropDownList ID="ddlmatch" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请选择比赛"
                                ControlToValidate="ddlmatch" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span>中文赔率比较网站名称</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcncorp" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入中文赔率比较网站名称"
                                ControlToValidate="txtcncorp" Display="None"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <span>英文赔率比较网站名称</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtencorp" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入英文赔率比较网站名称"
                                ControlToValidate="txtencorp" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span>利润</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtprofit" runat="server" CssClass="TextBox" MaxLength="20"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"
                                ControlToValidate="txtprofit" ErrorMessage="利润格式不对" ValidationExpression="^(\d{1,18})(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入利润"
                                ControlToValidate="txtprofit" Display="None"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <table class="NoBorderTable" width="100%">
                    <tr>
                        <td style="width: 55px">
                        </td>
                        <td style="color: Red">
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
</asp:Content>
