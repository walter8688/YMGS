<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="TeamManagePage.aspx.cs" Inherits="YMGS.Manage.Web.EventManage.TeamManagePage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
<asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <fieldset>
                <legend>查询条件</legend>
                    <table class="NoBorderTable" style="margin-top:5px;margin-bottom:5px;width:100%">
                        <tr>
                            <td style="width:20px"></td>
                            <td style="width:90px">
                                <span>赛事项目</span>
                            </td>
                            <td style="width:90px">
                                <asp:DropDownList ID="drpEventItem" runat="server" Width="151px" Height="18px" 
                                    CssClass="DropdownList" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td style="width:20px"></td>
                            <td style="width:90px">
                                <span>参赛成员名称</span>
                            </td>
                            <td style="width:90px">
                                <asp:TextBox ID="txtEventTeamName" runat="server"  CssClass="TextBox" MaxLength="50"></asp:TextBox> 
                            </td>
                            <td style="width:20px"></td>
                            <td style="width:100px">
                                <span>参赛成员英文名称</span>
                            </td>
                            <td style="width:90px">
                                <asp:TextBox ID="txtEventTeamNameEn" runat="server"  CssClass="TextBox" MaxLength="50"></asp:TextBox> 
                            </td>
                            <td style="width:30px"><span>区域</span></td>
                            <td style="width:220px">
                            <asp:TextBox ID="ZoneName" runat="server"  CssClass="TextBox" MaxLength="50" Width="100px"></asp:TextBox>
                            <asp:TextBox ID="ZoneID" runat="server"  CssClass="TextBox" MaxLength="50" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="TxtParamZoneStatus" runat="server"  CssClass="TextBox" MaxLength="50" style="display:none;"></asp:TextBox>
                            <asp:Button ID="BtnChooseZone" runat="server" Text="选择区域" Width="70px"  CausesValidation="false" OnClientClick="SetParamZoneStatus('Query');" CssClass="Button" onclick="btnAddZone_Click"/>
                            </td>
                            
                        </tr>
                        <tr>
                            <td style="width:20px"></td>
                            <td style="width:30px">
                                <span>状态</span>
                            </td>
                            <td style="width:90px">
                                <asp:DropDownList ID="drpParamStatus" runat="server" Width="151px" Height="18px" CssClass="DropdownList"></asp:DropDownList> 
                            </td>
                            <td style="width:20px"></td>
                            <td style="width:90px">
                                <span>类型(国家/职业)</span>
                            </td>
                            <td style="width:90px">
                                    <asp:DropDownList ID="drpParamType1" runat="server" Width="151px" Height="18px" CssClass="DropdownList"></asp:DropDownList>
                            </td>
                            <td style="width:20px"></td>
                            <td style="width:90px">
                                <span>类型(男子/女子)</span>
                            </td>
                            <td style="width:90px">
                                    <asp:DropDownList ID="drpParamType2" runat="server" Width="151px" Height="18px" CssClass="DropdownList"></asp:DropDownList>
                            </td>
                            <td colspan="2" align="left">
                                <asp:Button ID="btnQueryEventTeam" Width="70px" CssClass="Button" 
                                    runat="server" CausesValidation="false" Text="查询" 
                                    onclick="btnQueryEventTeam_Click"  />
                                &nbsp;<asp:Button ID="btnAddEventTeam" runat="server" Width="70px" Text="新增" CausesValidation="false" CssClass="Button" OnClick="btnAddEventTeam_Click"  />
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
            <asp:GridView ID="gdvEventTeam" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%" DataKeyNames="EVENT_TEAM_ID"
                EmptyDataText="请输入合适的查询条件查询参赛成员!" 
                onrowdatabound="gdvEventTeam_RowDataBound" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="EVENTITEM_NAME" HeaderText="赛事项目" />
                    <asp:BoundField DataField="EVENT_TEAM_NAME" HeaderText="赛事成员名称" />
                    <asp:BoundField DataField="EVENT_TEAM_NAME_EN" HeaderText="赛事成员英文名称" />
                    <asp:BoundField DataField="PARAMNAME1" HeaderText="类别(国家/职业)" />
                    <asp:BoundField DataField="PARAMNAME2" HeaderText="类别(男子/女子)" />
                    <asp:BoundField DataField="STATUSNAME" HeaderText="状态" />
                    <asp:BoundField DataField="ZONE_NAME" HeaderText="区域" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200" HeaderText="操作">
                        <ItemTemplate>
                            <asp:Label ID="lblEventItemID" runat="server"  Text='<%#Eval("EVENT_ITEM_ID") %>' style=" display:none;"></asp:Label>
                            <asp:Label ID="lblParamID1" runat="server"  Text='<%#Eval("PARAMID1") %>' style=" display:none;"></asp:Label>
                            <asp:Label ID="lblParamID2" runat="server"  Text='<%#Eval("PARAMID2") %>' style=" display:none;"></asp:Label>
                            <asp:Label ID="lblStatus" runat="server"  Text='<%#Eval("STATUS") %>' style=" display:none;"></asp:Label>
                            <asp:LinkButton ID="hlActivity" CssClass="HyperLink" CausesValidation="false" runat="server" Text="启用" OnClick="btnUpdateEventTeamStatus_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlInActivity" CssClass="HyperLink" CausesValidation="false" runat="server" Text="禁用" OnClick="btnUpdateEventTeamStatus_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlEdit" Text="编辑" runat="server" CssClass="HyperLink" CausesValidation="false" OnClick="btnAddEventTeam_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlDelete" CssClass="HyperLink" CausesValidation="false" runat="server" Text="删除" OnClick="btnDelEventTeam_Click" OnClientClick="return showConfirm('确定删除?');" ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
<asp:Button ID="btnFake" runat="server" style="display:none" />
<asp:Panel ID="pnlPopup" runat="server" style="width:570px;height:250px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
        <span>赛事成员详细页面</span>
    </asp:Panel>
    <table class="NoBorderTable" width="100%" id="popEventTeamTbl">
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
        <tr>
            <td style="width:10px"></td>
            <td style="width:110px; text-align:right;">
                <span style="color:Red;">*</span><span>赛事成员中文名称</span>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="popTxtEventTeamName" runat="server"  CssClass="TextBox" MaxLength="50"></asp:TextBox> 
            </td>
            <td style="width:10px"></td>
            <td style="width:110px; text-align:right;">
                <span style="color:Red;">*</span><span>赛事成员英文名称</span>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="popTxtEventTeamNameEn" runat="server"  CssClass="TextBox" MaxLength="50"></asp:TextBox> 
            </td>
        </tr>
        <tr>
            <td style="width:10px"></td>
            <td style="width:110px; text-align:right;">
                <span>赛事项目</span>
            </td>
            <td style="width:90px">
                <asp:DropDownList ID="popDrpEventItem" runat="server" Width="151px" Height="18px" CssClass="DropdownList" AutoPostBack="true"></asp:DropDownList>
            </td>
            <td style="width:10px"></td>
            <td style="width:110px; text-align:right;">
                <span>状态</span>
            </td>
            <td style="width:90px">
                <asp:DropDownList ID="popDrpParamStatus" runat="server" Width="151px" Height="18px" CssClass="DropdownList"></asp:DropDownList> 
            </td>
        </tr>
        <tr>
            <td style="width:10px"></td>
            <td style="width:110px; text-align:right;">
                <span>类型(国家/职业)</span>
            </td>
            <td style="width:90px">
                <asp:DropDownList ID="popDrpParamType1" runat="server" Width="151px" Height="18px" CssClass="DropdownList"></asp:DropDownList>
            </td>
            <td style="width:10px"></td>
            <td style="width:110px; text-align:right;">
                <span>类型(男子/女子)</span>
            </td>
            <td style="width:90px">
                <asp:DropDownList ID="popDrpParamType2" runat="server" Width="151px" Height="18px" CssClass="DropdownList"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:10px"></td>
            <td style="width:110px; text-align:right;">
                <span>区域</span>
            </td>
            <td colspan="4">
                <asp:TextBox ID="txtZone" runat="server"  CssClass="TextBox" MaxLength="50"></asp:TextBox>
                <asp:TextBox ID="txtZoneID" runat="server"  CssClass="TextBox" MaxLength="50" Visible="false"></asp:TextBox>
                <asp:Button ID="btnAddZone" runat="server" Text="选择区域" Width="70px"  CausesValidation="false" CssClass="Button" OnClientClick="SetParamZoneStatus('Edit');" onclick="btnAddZone_Click"/>
            </td>
        </tr>
    </table>
    <br />
    <asp:CustomValidator ID="valEventTeamName"  runat="server" Display="None" ErrorMessage="赛事成员名称和赛事成员英文不能为空!" ClientValidationFunction="validEventTeamName"></asp:CustomValidator>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td style="width:20px">
            </td>
            <td style="color:Red">
                    <asp:ValidationSummary runat="server" ShowMessageBox="false" ShowSummary="true" ID="Validationsummary1" />
            </td>
        </tr>
    </table>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text="保存" Width="70px"  CausesValidation="true" CssClass="Button" onclick="btnSaveEventTeam_Click"/>
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
<asp:Button ID="btnFakeZone" runat="server" style="display:none" />
<asp:Panel ID="pnlPopupZone" runat="server" style="display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlPopupZoneHeader" runat="server" CssClass="ModalPoupHeader">
        <span>区域</span>
    </asp:Panel>
    <table class="NoBorderTable" width="100%">
    <tr>
        <td colspan="3">
            <div style="width:350px; height:480px; overflow:auto; ">
            <asp:TreeView runat="server" ID="tvParamZone"
            ExpandDepth="1" onselectednodechanged="tvParamZone_SelectedNodeChanged"
            ImageSet="BulletedList3" ShowLines="True">
            <HoverNodeStyle BorderStyle="None" />
            <NodeStyle CssClass="treeView" />
            <SelectedNodeStyle CssClass="treeViewSelectd" />
            </asp:TreeView>
            </div>
        </td>
    </tr>
    <tr align="center">
        <td><asp:Button ID="Button1" runat="server" Text="取消" Width="70px" CausesValidation="false" CssClass="Button" /></td>
    </tr>
    </table>
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopupZone" BehaviorID="mdlPopupZone" 
    TargetControlID="btnFakeZone"
    PopupControlID="pnlPopupZone"
    BackgroundCssClass="ModalPopupBackground"                         
    CancelControlID="Button1" 
    PopupDragHandleControlID="pnlPopupZoneHeader">
    </ajaxToolkit:ModalPopupExtender>         
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    function validEventTeamName(source, args) {
        if (chkIsNull('popTxtEventTeamName')) {
            args.IsValid = false;
            return;
        }
        args.IsValid = true;
    }

    function SetParamZoneStatus(obj) {
        $('[id$=TxtParamZoneStatus]').val(obj);
    }
</script>
</asp:Content>
