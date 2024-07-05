<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true"
    CodeBehind="HelperNewPage.aspx.cs" Inherits="YMGS.Manage.Web.AssistManage.HelperNewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
    <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divLeft" style="float: left; overflow-y: auto; height: 580px; width: 300px;
                border: 1px solid gray; padding-right: 30px;">
                <asp:TreeView runat="server" ID="tvHelperData" OnSelectedNodeChanged="tvHelperData_SelectedNodeChanged"
                    ExpandDepth="1" ImageSet="BulletedList3" ShowLines="True">
                    <HoverNodeStyle BorderStyle="None" />
                    <NodeStyle CssClass="treeView" />
                    <SelectedNodeStyle CssClass="treeViewSelectd" />
                </asp:TreeView>
            </div>
            <fieldset style="float: inherit; position: relative; left: 3%; width: 60%;">
                <legend>编辑目录</legend>
                <table class="NoBorderTable" width="100%" id="ParamZoneTbl">
                    <tr>
                        <td style="width: 70px; height: 25px; line-height: 25px; text-align: right; margin-left: 10px;
                            padding-right: 5px;">
                            <span>父目录中文名称</span>
                        </td>
                        <td style="width: 90px;">
                            <asp:TextBox runat="server" ID="txtPItemNameCN" Width="100%" CssClass="TextBox"></asp:TextBox>
                            <asp:TextBox ID="txtItemID" Visible="false" runat="server" CssClass="TextBox"></asp:TextBox>
                            <asp:TextBox ID="txtPItemID" Visible="false" runat="server" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td style="width: 70px; height: 25px; line-height: 25px; text-align: right; margin-left: 10px;
                            padding-right: 5px;">
                            <span>父目录英文名称</span>
                        </td>
                        <td style="width: 90px;">
                            <asp:TextBox runat="server" ID="txtPItemNameEN" Width="100%" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td style="width: 20px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 70px; height: 25px; line-height: 25px; text-align: right; margin-left: 10px;
                            padding-right: 5px;">
                            <span style="color: red;">*</span><span>目录中文名称</span>
                        </td>
                        <td style="width: 90px;">
                            <asp:TextBox runat="server" ID="txtItemNameCN" MaxLength="40" Width="100%" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td style="width: 70px; height: 25px; line-height: 25px; text-align: right; margin-left: 10px;
                            padding-right: 5px;">
                            <span style="color: red;">*</span><span>目录英文名称</span>
                        </td>
                        <td style="width: 90px;">
                            <asp:TextBox runat="server" ID="txtItemNameEN" MaxLength="40" Width="100%" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 70px; height: 25px; line-height: 25px; text-align: right; margin-left: 10px;
                            padding-right: 5px;">
                            <span style="color: red;">*</span><span>目录中文链接网页</span>
                        </td>
                        <td style="width: 90px;">
                            <asp:TextBox runat="server" ID="txtItemLinkNameCN" MaxLength="200" Width="100%" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td style="width: 70px; height: 25px; line-height: 25px; text-align: right; margin-left: 10px;
                            padding-right: 5px;">
                            <span style="color: red;">*</span><span>目录英文链接网页</span>
                        </td>
                        <td style="width: 90px;">
                            <asp:TextBox runat="server" ID="txtItemLinkNameEN" MaxLength="200" Width="100%" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 70px; height: 25px; line-height: 25px; text-align: right; margin-left: 10px;
                            padding-right: 5px;">
                            <span>排列序号</span>
                        </td>
                        <td style="width: 90px;">
                            <asp:TextBox runat="server" ID="txtOrderNO" MaxLength="40" Width="100%" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td style="width: 70px; height: 25px; line-height: 25px; text-align: right; margin-left: 10px;
                            padding-right: 5px;">
                            关联Rules
                        </td>
                        <td style="width: 90px;">
                            <asp:DropDownList ID="drpRules" runat="server" CssClass="DropdownList" Width="102%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 70px; height: 25px; line-height: 25px; text-align: right; margin-left: 10px;
                            padding-right: 5px;">
                            <span style="color: red;">*</span><span>子目录中文名称</span>
                        </td>
                        <td style="width: 90px;">
                            <asp:TextBox runat="server" ID="txtChildItemNameCN" MaxLength="40" Width="100%" CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td style="width: 70px; height: 25px; line-height: 25px; text-align: right; margin-left: 10px;
                            padding-right: 5px;">
                            <span style="color: red;">*</span><span>子目录英文名称</span>
                        </td>
                        <td style="width: 90px;">
                            <asp:TextBox runat="server" ID="txtChildItemNameEN" MaxLength="40" Width="100%" CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 70px; height: 25px; line-height: 25px; text-align: right; margin-left: 10px;
                            padding-right: 5px;">
                            <span style="color: red;">*</span><span>子目录链接网页</span>
                        </td>
                        <td style="width: 90px;">
                            <asp:TextBox runat="server" ID="txtChildItemLinkNameCN" MaxLength="40" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td style="width: 70px; height: 25px; line-height: 25px; text-align: right; margin-left: 10px;
                            padding-right: 5px;">
                            <span style="color: red;">*</span><span>子目录链接网页</span>
                        </td>
                        <td style="width: 90px;">
                            <asp:TextBox runat="server" ID="txtChildItemLinkNameEN" MaxLength="40" Width="100%"
                                CssClass="TextBox"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr id="Tr1" align="center">
                        <td colspan="3" style="height: 30px; line-height: 30px; padding-left: 20px;">
                            <asp:TextBox ID="hidActionMode" runat="server" Style="display: none;"></asp:TextBox>
                            <asp:Button ID="btnAddCatalog" Text="新建子目录" Width="70px" CausesValidation="true"
                                CssClass="Button" runat="server" OnClick="btnAddCatalog_Click" OnClientClick="setActionMode('add');" />
                            <asp:Button ID="btnDelCatalog" Text="删除目录" Width="70px" CausesValidation="false"
                                CssClass="Button" runat="server" OnClick="btnDelCatalog_Click" OnClientClick="return showConfirm('确认删除？');" />
                            <asp:Button ID="btnSaveCatalog" Text="保存目录" Width="70px" CausesValidation="true"
                                CssClass="Button" runat="server" OnClick="btnSaveCatalog_Click" OnClientClick="setActionMode('save');" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:CustomValidator ID="valItem" runat="server" Display="None" ErrorMessage="当前目录相关内容不能为空!"
                    ClientValidationFunction="valItemLst"></asp:CustomValidator>
                <asp:CustomValidator ID="valChildItem" runat="server" Display="None" ErrorMessage="当前子目录相关内容不能为空!"
                    ClientValidationFunction="valChildItemLst"></asp:CustomValidator>
                <table class="NoBorderTable" width="100%">
                    <tr>
                        <td style="width: 18px">
                        </td>
                        <td style="color: Red">
                            <asp:ValidationSummary runat="server" ShowMessageBox="false" ShowSummary="true" ID="Validationsummary1" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr id="buttonTr" align="left">
                        <td colspan="3" style="height: 30px; line-height: 30px; padding-left: 20px;">
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function valItemLst(source, args) {
            var flag = document.getElementById("<%=hidActionMode.ClientID %>").value == "save" ? true : false;
            if (chkIsNull('txtItemNameCN') && chkIsNull('txtItemNameEN') && chkIsNull('txtItemLinkNameCN') && chkIsNull('txtItemLinkNameEN') && flag) {
                args.IsValid = false;
                return;
            }
            else {
                args.IsValid = true;
            }
        }
        function valChildItemLst(source, args) {
            var flag = document.getElementById("<%=hidActionMode.ClientID %>").value == "add" ? true : false;
            if (chkIsNull('txtChildItemNameCN') && chkIsNull('txtChildItemNameEN') && chkIsNull('txtChildItemLinkNameCN') && chkIsNull('txtChildItemLinkNameEN') && flag) {
                args.IsValid = false;
                return;
            }
            else {
                args.IsValid = true;
            }
        }

        function setActionMode(val) {
            document.getElementById("<%=hidActionMode.ClientID %>").value = val;
        }
    </script>
</asp:Content>
