<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="Helper.aspx.cs" Inherits="YMGS.Manage.Web.AssistManage.Helper" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
<asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<table width="100%" class="NoBorderTable">
    <tr>
        <td>
           <asp:Button ID="btnNew" runat="server" Width="70px" Text="新增" CausesValidation=false CssClass="Button" onclick="btnNew_Click" />
        </td>
    </tr>
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gdvMain" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%" 
                OnRowDataBound="gdvMain_RowDataBound" EmptyDataText="无记录!">
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="PITEMID" HeaderText="上级目录名称" />
                    <asp:BoundField DataField="CNITEMNAME" HeaderText="目录名称" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200" HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="hlEdit" Text="编辑" CommandArgument='<%# Eval("ITEMID") %>' runat="server" CausesValidation="false" CssClass="HyperLink" onclick="btnNew_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlDelete" CommandArgument='<%# Eval("ITEMID") %>' CssClass="HyperLink" runat="server" CausesValidation="false" Text="删除" OnClick="btnDelete_Click" OnClientClick="return showConfirm('确定删除?');"></asp:LinkButton>
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
        <span>帮助详细页面</span><asp:HiddenField ID="hfdpid" runat="server" />
    </asp:Panel>
    <table  width="100%">
        <tr>
            <td style="width=100px" >
                <span>上级目录名称</span>
            </td>
            <td >
                <asp:DropDownList ID="ddlpitem" DataTextField="CNITEMNAME" DataValueField="ITEMID" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请选择父结点" ControlToValidate="ddlpitem"  Display="None"></asp:RequiredFieldValidator>
            </td>
             <td  >
            </td>
            <td >
            </td>
        </tr>
        <tr>
            <td >
                <span>中文名</span>
            </td>
            <td >
                <asp:TextBox ID="txtcnitemname" runat="server"  CssClass="TextBox" MaxLength="300"  ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入中文名" ControlToValidate="txtcnitemname" Display="None"></asp:RequiredFieldValidator>
            </td>
               <td style="width:100px">
                <span>英文名</span>
            </td>
            <td >
                <asp:TextBox ID="txtenitemname" runat="server"  CssClass="TextBox" MaxLength="300"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请输入英文名" ControlToValidate="txtenitemname"  Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
           <tr>
            <td >
                <span>中文链接网页</span>
            </td>
            <td >
                <asp:TextBox ID="txtweblink" runat="server"  CssClass="TextBox" MaxLength="200"  ></asp:TextBox>
            </td>
               <td >
                <span>英文链接网页</span>
            </td>
            <td >
                <asp:TextBox ID="txtenweblink" runat="server"  CssClass="TextBox" MaxLength="200" ></asp:TextBox>
            </td>
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
