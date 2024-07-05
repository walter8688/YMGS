<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="HotLine.aspx.cs" Inherits="YMGS.Manage.Web.AssistManage.HotLine" %>
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
                                <span>标题</span>
                            </td>
                            <td style="width:90px">
                                <asp:TextBox ID="txttitles" runat="server"></asp:TextBox>
                            </td>
                            <td style="width:20px"></td>
                            <td style="width:70px">
                                
                            </td>
                            <td style="width:90px">
                                 <asp:CheckBox ID="ckbIsValiabe" runat="server" Text="是否有效" />
                            </td>
                            <td style="width:20px">
                            </td>
                            <td align="right">
                                <asp:Button ID="btnSearch" Width="70px" CssClass="Button" runat="server" CausesValidation=false Text="查询" onclick="btnSearch_Click" />
                                &nbsp;<asp:Button ID="btnNew" runat="server" Width="70px" Text="新增" CausesValidation=false CssClass="Button" onclick="btnNew_Click" />
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
                GridLines="None" CssClass="GridView" Width="100%" 
                OnRowDataBound="gdvMain_RowDataBound" EmptyDataText="请输入合适的查询条件查询公告!">
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="title" HeaderText="标题" />
                    <asp:BoundField DataField="content" HeaderText="内容" />
                    <asp:BoundField DataField="isv" HeaderText="是否有效" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200" HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="hlEdit" Text="编辑" CommandArgument='<%# Eval("pid") %>' runat="server" CausesValidation="false" CssClass="HyperLink" onclick="btnNew_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlDelete" CommandArgument='<%# Eval("pid") %>' CssClass="HyperLink" runat="server" CausesValidation="false" Text="删除" OnClick="btnDelete_Click" OnClientClick="return showConfirm('确定删除?');"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="PageNavigator1" PageSize="20" runat="server" OnPageIndexChanged="PageNavigator1_PageIndexChanged" />
        </td>
    </tr>
</table>
<asp:Button ID="btnFake" runat="server" style="display:none" />
<asp:Panel ID="pnlPopup" runat="server" style="width:600px;height:240px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
        <span>公告详细页面</span><asp:HiddenField ID="hfdpid" runat="server" />
    </asp:Panel>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
        <tr>
            <td >
                <span>中文标题</span>
            </td>
            <td >
                <asp:TextBox ID="txttitle" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入中文标题" ControlToValidate="txttitle"  Display="None"></asp:RequiredFieldValidator>
            </td>
             <td >
                <span>英文标题</span>
            </td>
            <td >
                <asp:TextBox ID="txtentitle" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入英文标题" ControlToValidate="txtentitle"  Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td >
                <span>中文内容</span>
            </td>
            <td >
                <asp:TextBox ID="txtcontent" runat="server"  CssClass="TextBox" MaxLength="300" TextMode="MultiLine" Height="50px" Width="100%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入中文内容" ControlToValidate="txtcontent" Display="None"></asp:RequiredFieldValidator>
            </td>
               <td >
                <span>英文内容</span>
            </td>
            <td >
                <asp:TextBox ID="txtencontent" runat="server"  CssClass="TextBox" MaxLength="300" TextMode="MultiLine" Height="50px" Width="100%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请输入英文内容" ControlToValidate="txtencontent"  Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td >
                <span>是否有效</span>
            </td>
            <td>
                <asp:CheckBox ID="ckbisv" runat="server" Text="有效" Checked="true"/>
            </td>
            <td></td>  <td></td>
        </tr>
    </table>
   
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
</asp:Content>
