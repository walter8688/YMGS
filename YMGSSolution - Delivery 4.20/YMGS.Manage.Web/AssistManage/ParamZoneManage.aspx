<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="ParamZoneManage.aspx.cs" Inherits="YMGS.Manage.Web.AssistManage.ParamZoneManage" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ListPlace" runat="server">
<asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<div id="divLeft" style="float:left; overflow-y:auto; height:580px; width:300px; border:1px solid gray; padding-right:30px; ">
    <asp:TreeView runat="server" ID="tvParamZone" 
        onselectednodechanged="tvParamZone_SelectedNodeChanged" ExpandDepth="1" 
        ImageSet="BulletedList3" ShowLines="True">
        <HoverNodeStyle BorderStyle="None" />
        <NodeStyle CssClass="treeView" />
        <SelectedNodeStyle CssClass="treeViewSelectd" />
    </asp:TreeView>
</div>
<fieldset style="float:inherit; position:relative; left:3%; width:25%;">
    <legend>编辑区域</legend>
    <table class="NoBorderTable" id="ParamZoneTbl">
        <tr>
            <td style="width:70px; height:25px; line-height:25px;text-align:right; margin-left:10px; padding-right:5px;">
                <span>父区域名称</span>
            </td>
            <td style="width:90px;">
                <asp:TextBox runat="server" ID="txtParentZoneName" CssClass="TextBox"></asp:TextBox>
                <asp:TextBox ID="txtZnoeID" Visible="false" runat="server" CssClass="TextBox"></asp:TextBox>
                <asp:TextBox ID="txtParentZoneID" Visible="false" runat="server" CssClass="TextBox"></asp:TextBox>
            </td>
            <td style=" width:20px;"></td>
        </tr>
        <tr>    
            <td style="width:70px; height:25px; line-height:25px;text-align:right; margin-left:10px; padding-right:5px;">
                <span style="color:red;">*</span><span>区域名称</span>
            </td>
            <td colspan="2" style="width:90px;">
                <asp:TextBox runat="server" ID="txtZoneName" MaxLength="40" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:70px; height:25px; line-height:25px;text-align:right; margin-left:10px; padding-right:5px;">
                <span style="color:red;">*</span><span>子区域名称</span>
            </td>
            <td colspan="2" style="width:90px;">
                <asp:TextBox runat="server" ID="txtChildZoneName" MaxLength="40" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <asp:CustomValidator ID="valZone"  runat="server" Display="None" ErrorMessage="区域名称不能为空!" ClientValidationFunction="valParamZone"></asp:CustomValidator>
    <asp:CustomValidator ID="valChildZone"  runat="server" Display="None" ErrorMessage="子区域名称不能为空!" ClientValidationFunction="valChildParamZone"></asp:CustomValidator>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td style="width:18px">
            </td>
            <td style="color:Red">
                    <asp:ValidationSummary runat="server" ShowMessageBox="false" ShowSummary="true" ID="Validationsummary1" />
            </td>
        </tr>
    </table>
    <table>
        <tr id="buttonTr" align="left">
            <td colspan="3" style="height:30px; line-height:30px; padding-left:20px;">
                <asp:TextBox ID="hidActionMode" runat="server" style="display:none;"></asp:TextBox>
                <asp:Button ID="btnAddZone" Text="新建子区域" Width="70px" CausesValidation="true" CssClass="Button" runat="server" onclick="btnAddZone_Click"  OnClientClick="setActionMode('add');" />
                <asp:Button ID="btnDelZone" Text="删除区域" Width="70px" CausesValidation="false" CssClass="Button" runat="server" onclick="btnDelZone_Click" OnClientClick="return showConfirm('确认删除？');" />
                <asp:Button ID="btnSaveZone" Text="保存区域" Width="70px" CausesValidation="true" CssClass="Button" runat="server" onclick="btnSaveZone_Click" OnClientClick="setActionMode('save');"  />
            </td>
        </tr>
    </table>
</fieldset>
</ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    function valParamZone(source, args) {
        var flag = document.getElementById("<%=hidActionMode.ClientID %>").value == "save" ? true : false;
        if (chkIsNull('txtZoneName') && flag) {
            args.IsValid = false;
            return;
        }
        else {
            args.IsValid = true;
        }
    }
    function valChildParamZone(source, args) {
        var flag = document.getElementById("<%=hidActionMode.ClientID %>").value == "add" ? true : false;
        if (chkIsNull('txtChildZoneName') && flag) {
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
