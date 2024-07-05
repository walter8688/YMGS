<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true"
    CodeBehind="ADRegion.aspx.cs" Inherits="YMGS.Manage.Web.SystemSetting.ADRegion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
    <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hfdaccessable" runat="server" />
            <asp:Button runat="server"  ID="btnNew" Width="70px" Text="新增" CssClass="Button"
                CausesValidation="false" OnClick="btnEdit_Click" />
            <asp:GridView ID="gdvMain" runat="server" AutoGenerateColumns="false" GridLines="None"
                CssClass="GridView" Width="100%"  EmptyDataText="无记录" 
                onrowdatabound="gdvMain_RowDataBound">
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="AD_WORDS_ID" HeaderText="编号" />
                    <asp:BoundField DataField="TITLE" HeaderText="标题" />
                    <asp:BoundField DataField="DESC" HeaderText="描述" />
                    <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:LinkButton OnClick="btnEdit_Click" ID="btnEdit" CommandArgument='<%# Eval("AD_WORDS_ID") %>'
                                runat="server" Text="编辑" CausesValidation="false"></asp:LinkButton>
                            <asp:LinkButton OnClick="btnDel_Click" ID="btndel" CommandArgument='<%# Eval("AD_WORDS_ID") %>'
                                runat="server" Text="删除" CausesValidation="false" OnClientClick="return confirm('Are you sure to delete this item?');"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnFake" runat="server" Style="display: none" />
            <asp:Panel ID="pnlPopup" runat="server" Style="display: none" CssClass="ModalPoup">
                <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
                    <span>文字广告区域</span>
                </asp:Panel>
                <table class="NoBorderTable" width="100%">
                    <tr>
                        <td colspan="5">
                        提示：维护该区域时，必需要存在 标题：赔率比较网站 英文标题：Odds Compare Website 的记录，否则前台页面中链接：进入赔率比较网站 无对应链接
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="标题"></asp:Label><asp:HiddenField ID="hfdid"
                                runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="txttitle" runat="server" Width="200px" MaxLength="30"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                ErrorMessage="请输入中文标题" SetFocusOnError="True" ControlToValidate="txttitle"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp; &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="英文标题"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txttitleen" runat="server" Width="200px" MaxLength="30"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                ErrorMessage="请输入英文文标题" SetFocusOnError="True" ControlToValidate="txttitleen"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="描述"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdesc" runat="server" Width="200px" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                                ErrorMessage="请输入中文描述" ControlToValidate="txtdesc"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;&nbsp; &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="英文描述"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdescen" runat="server" Width="200px" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                                ErrorMessage="请输入英文描述" SetFocusOnError="True" ControlToValidate="txtdescen"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="链接地址"></asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtweblink" runat="server" Width="478px" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
                                ErrorMessage="请输入链接地址" SetFocusOnError="True" ControlToValidate="txtweblink"></asp:RequiredFieldValidator>
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
    <table>
        <tr>
            <td colspan="5">
                <p style="background-color: #CCCCCC; height: 1px;">
                </p>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="Label6" runat="server" Text="中文广告栏图片上传"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:FileUpload ID="fileuploadcn" runat="server" Width="400px" />
                &nbsp;&nbsp;
                </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="Label7" runat="server" Text="英文广告栏图片上传"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:FileUpload ID="fileuploaden" runat="server" Width="400px" />
                &nbsp;&nbsp;<asp:Button ID="btnUploadcn" runat="server" Text="上传" CausesValidation="False" 
                    onclick="btnUploadcn_Click" />
            &nbsp;</td>
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
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="5">
                <p style="background-color: #CCCCCC; height: 1px;">
                </p>
            </td>
        </tr>
        <tr>
            <th colspan="5">
              置顶比赛设置
            </th>
        </tr>
         <tr>
            <td colspan="5" align=left style="color:Red">
              提示:置顶比赛图片每次保存时需上传图片;
            </td>
        </tr>
          <tr>
            <td>
            比赛中文名称
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlmatchmarket" runat="server">
                </asp:DropDownList>
                <asp:HiddenField ID="hfdurl" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="ddlmatchmarket" Display="None" ErrorMessage="请选择比赛" 
                    ValidationGroup="toprace"></asp:RequiredFieldValidator>
            </td>
            <td>
                </td>
        </tr>
         <tr>
            <td>
            比赛中文图片
            </td>
            <td>
                <asp:FileUpload ID="fulcn" runat="server" />
               
            </td>
            <td>比赛英文图片
            </td>
            <td> <asp:FileUpload ID="fulen" runat="server" />
           
            </td>
            <td>
               </td>
        </tr>
          <tr>
            <td>中文标题
            </td>
            <td>
                <asp:TextBox ID="txtcnTitle" runat="server" MaxLength="100" Width="240px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="txtcnTitle" Display="None" ErrorMessage="请输入中文标题" 
                    ValidationGroup="toprace"></asp:RequiredFieldValidator>
            </td>
            <td>英文标题
            </td>
            <td><asp:TextBox ID="txtenTitle" runat="server" MaxLength="100" Width="240px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                    ControlToValidate="txtenTitle" Display="None" ErrorMessage="请输入英文标题" 
                    ValidationGroup="toprace"></asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
           <tr>
            <td>中文内容
            </td>
            <td>  <asp:TextBox ID="txtcnContext" runat="server" Height="50px" MaxLength="300" 
                    TextMode="MultiLine" Width="240px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                    ControlToValidate="txtcnContext" Display="None" ErrorMessage="请输入中文内容" 
                    ValidationGroup="toprace"></asp:RequiredFieldValidator>
            </td>
            <td>英文内容
            </td>
            <td>  <asp:TextBox ID="txtenContent" runat="server" Height="50px" MaxLength="300" 
                    TextMode="MultiLine" Width="240px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                    ControlToValidate="txtenContent" Display="None" ErrorMessage="请输入英文内容" 
                    ValidationGroup="toprace"></asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" 
                    ValidationGroup="toprace" />
            </td>
            <td>
            </td>
            <td>
                <asp:Button ID="btnSaveTopRace" runat="server" Text="保存置顶比赛" 
                    onclick="btnSaveTopRace_Click" ValidationGroup="toprace" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
