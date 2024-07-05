<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="EventZoneManagePage.aspx.cs" Inherits="YMGS.Manage.Web.EventManage.EventZoneManagePage" %>
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
                            <td style="width:60px">
                                <span>赛事项目</span>
                            </td>
                            <td style="width:90px">
                                <asp:DropDownList ID="drpEventItem" runat="server" Width="151px" Height="18px" CssClass="DropdownList"></asp:DropDownList>
                            </td>
                            <td style="width:10px"></td>
                            <td style="width:90px">
                                <span>赛事区域名称</span>
                            </td>
                            <td style="width:90px">
                                 <asp:TextBox ID="txtEventZoneName" runat="server"  CssClass="TextBox" MaxLength="40"></asp:TextBox> 
                            </td>
                            <td style="width:10px"></td>
                            <td style="width:100px">
                                <span>赛事区域英文名称</span>
                            </td>
                            <td style="width:90px">
                                 <asp:TextBox ID="txtEventZoneNameEn" runat="server"  CssClass="TextBox" MaxLength="40"></asp:TextBox> 
                            </td>
                            <td style="width:30px"><span>区域</span></td>
                            <td style="width:200px">
                            <asp:TextBox ID="ZoneName" runat="server"  CssClass="TextBox" MaxLength="50" Width="100px"></asp:TextBox>
                            <asp:TextBox ID="ZoneID" runat="server"  CssClass="TextBox" MaxLength="50" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="TxtParamZoneStatus" runat="server"  CssClass="TextBox" MaxLength="50" style="display:none;"></asp:TextBox>
                            <asp:Button ID="BtnChooseZone" runat="server" Text="选择区域" Width="70px"  CausesValidation="false" OnClientClick="SetParamZoneStatus('Query');" CssClass="Button" onclick="btnAddZone_Click"/>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnQueryEventZone" Width="70px" CssClass="Button" 
                                    runat="server" Text="查询" CausesValidation="false" onclick="btnQueryEventZone_Click"  />
                                &nbsp;<asp:Button ID="btnAddEventZone" runat="server" CausesValidation="false" Width="70px" Text="新增" 
                                    CssClass="Button" onclick="btnAddEventZone_Click"  />
                            </td>
                            <td style="width:10px"></td>
                            <td style="width:80px;display:none;">
                                <span>赛事区域描述</span>
                            </td>
                            <td style="width:90px;display:none;">
                                 <asp:TextBox ID="txtEventZoneDesc" runat="server"  CssClass="TextBox" MaxLength="100"></asp:TextBox> 
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
            <asp:GridView ID="gdvEventZone" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%" DataKeyNames="EVENTZONE_ID"
                EmptyDataText="请输入合适的查询条件查询赛事区域!" 
                onrowdatabound="gdvEventZone_RowDataBound">
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="EVENTZONE_NAME" HeaderText="赛事区域名称" />
                    <asp:BoundField DataField="EVENTZONE_NAME_EN" HeaderText="赛事区域英文名称" />
                    <asp:BoundField DataField="EVENTZONE_DESC" Visible="false" HeaderText="赛事区域描述" />
                    <asp:BoundField DataField="ZONE_NAME" HeaderText="辅助区域" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120" HeaderText="操作">
                        <ItemTemplate>
                            <asp:Label ID="lblEventItemID" runat="server"  Text='<%#Eval("EVENTITEM_ID") %>' style=" display:none;"></asp:Label>
                            <asp:LinkButton ID="hlEdit" Text="编辑" runat="server" CssClass="HyperLink" CausesValidation="false"  onclick="btnAddEventZone_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlDelete" CssClass="HyperLink" CausesValidation="false" runat="server" Text="删除" onclick="btnDelEventZone_Click" ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
<asp:Button ID="btnFake" runat="server" style="display:none" />
<asp:Panel ID="pnlPopup" runat="server" style="width:350px;height:250px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
        <span>赛事区域详细页面</span>
    </asp:Panel>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
        <tr>
            <td style="width:90px; text-align:right;">
                <span>赛事项目</span>
            </td>
            <td style="width:90px">
                <asp:DropDownList ID="popUpDrpEventItem" runat="server" Width="151px" Height="18px" CssClass="DropdownList"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:90px; text-align:right;">
                <span style="color:Red;">*</span><span>赛事区域名称</span>
            </td>
            <td style="width:90px;">
                <asp:TextBox ID="popUpTxtEventZoneName" runat="server"  CssClass="TextBox" MaxLength="40"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:100px; text-align:right;">
                <span style="color:Red;">*</span><span>赛事区域英文名称</span>
            </td>
            <td style="width:90px;">
                <asp:TextBox ID="popUpTxtEventZoneNameEn" runat="server"  CssClass="TextBox" MaxLength="40"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:90px; text-align:right;"><span style="color:Red;">*</span><span>区域</span></td>
            <td style="width:220px;">
                <asp:TextBox ID="txtZone" runat="server"  CssClass="TextBox" MaxLength="50"></asp:TextBox>
                <asp:TextBox ID="txtZoneID" runat="server"  CssClass="TextBox" MaxLength="50" Visible="false"></asp:TextBox>
                <asp:Button ID="btnAddZone" runat="server" Text="选择区域" Width="70px"  CausesValidation="false" CssClass="Button" OnClientClick="SetParamZoneStatus('Edit');" onclick="btnAddZone_Click"/>
            </td>
        </tr>
        <tr style="display:none;">
            <td style="width:10px; height:30px; line-height:30px;"><asp:TextBox ID="txtHiddenEventZoneID" Visible="false" runat="server"></asp:TextBox></td>
            <td style="width:90px; text-align:right;">
                <span>赛事区域描述</span>
            </td>
            <td style="width:90px">
                <asp:TextBox ID="popUpTxtEventZoneDesc" runat="server"  CssClass="TextBox" MaxLength="100"></asp:TextBox> 
            </td>
        </tr>
    </table>
    <br />
    <asp:CustomValidator ID="valEventZoneName"  runat="server" Display="None" ErrorMessage="赛事区域名称,赛事区域英文名称和区域不能为空!" ClientValidationFunction="validEventZoneName"></asp:CustomValidator>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td style="width:54px">
            </td>
            <td style="color:Red">
                    <asp:ValidationSummary runat="server" ShowMessageBox="false" ShowSummary="true" ID="Validationsummary1" />
            </td>
        </tr>
    </table>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text="保存" Width="70px"  CausesValidation="true" CssClass="Button" OnClick="btnSaveEventZone_Click"/>
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
    function validEventZoneName(source, args) {
        if (chkIsNull('popUpTxtEventZoneName') || chkIsNull('popUpTxtEventZoneNameEn') || chkIsNull('txtZone')) {
            args.IsValid = false;
            return
        }
        args.IsValid = true;
    }

    function SetParamZoneStatus(obj) {
        $('[id$=TxtParamZoneStatus]').val(obj);
    }
</script>
</asp:Content>
