<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="EventManagePage.aspx.cs" Inherits="YMGS.Manage.Web.EventManage.EventManagePage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
<asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <fieldset>
                <legend>查询条件</legend>
                    <table class="NoBorderTable" style="margin-top:5px;margin-bottom:5px; width:100%">
                            <tr>
                                <td style="width:10px"></td>
                                <td style="width:70px">
                                    <span>赛事项目</span>
                                </td>
                                <td style="width:90px">
                                    <asp:DropDownList ID="drpParamItem" runat="server" Width="151px" Height="18px" AutoPostBack="true"
                                        CssClass="DropdownList" 
                                        onselectedindexchanged="drpParamItem_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                <td style="width:10px"></td>
                                <td style="width:70px">
                                    <span>赛事区域</span>
                                </td>
                                <td style="width:90px">
                                     <asp:DropDownList ID="drpEventZone" runat="server" Width="151px" Height="18px" CssClass="DropdownList"></asp:DropDownList> 
                                </td>
                                <td style="width:10px"></td>
                                <td style="width:70px">
                                    <span>赛事名称</span>
                                </td>
                                <td style="width:90px">
                                     <asp:TextBox ID="txtEventName" runat="server"  CssClass="TextBox" MaxLength="100"></asp:TextBox> 
                                </td>
                                <td style="width:10px"></td>
                                <td style="width:80px">
                                    <span>赛事英文名称</span>
                                </td>
                                <td style="width:90px">
                                     <asp:TextBox ID="txtEventNameEn" runat="server"  CssClass="TextBox" MaxLength="100"></asp:TextBox> 
                                </td>
                                
                            </tr>
                            <tr>
                                <td style="width:10px"></td>
                                <td style="width:40px">
                                    <span>赛事状态</span>
                                </td>
                                <td style="width:90px">
                                    <asp:DropDownList ID="drpEventStatus" runat="server" Width="151px" Height="18px" CssClass="DropdownList"></asp:DropDownList> 
                                </td>
                                <td style="width:10px"></td>
                                <td style="width:40px">
                                    <span>开始时间</span>
                                </td>
                                <td style="width:90px">
                                    <div class="calendarContainer">
                                        <YMGS:AjaxCalendar ID="calStartDate" runat="server" NeedCalendarButton="true" />
                                    </div>
                                </td>
                                <td style="width:10px;display:none;"></td>
                                <td style="width:40px;display:none;">
                                    <span>结束时间</span>
                                </td>
                                <td style="width:90px;display:none;">
                                    <div class="calendarContainer">
                                        <YMGS:AjaxCalendar ID="calEndDate" runat="server" NeedCalendarButton="true" />
                                    </div>
                                </td>
                                <td style="width:10px;display:none;"><asp:TextBox ID="txtEventDesc" runat="server"  CssClass="TextBox" MaxLength="100"></asp:TextBox> </td>
                                <td colspan="3" align="right">
                                    <asp:Button ID="btnQueryEvent" Width="70px" CssClass="Button"  runat="server" 
                                        CausesValidation="false" Text="查询" onclick="btnQueryEvent_Click" />
                                    &nbsp;<asp:Button ID="btnAddEvent" runat="server" Width="70px" Text="新增" 
                                        CausesValidation="false" CssClass="Button" onclick="btnAddEvent_Click" />
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
            <asp:GridView ID="gdvEvent" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%" DataKeyNames="EVENT_ID"
                EmptyDataText="请输入合适的查询条件查询赛事!" OnRowDataBound="gdvEvent_RowDataBind" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="EVENTZONE_NAME" HeaderText="赛事区域" />
                    <asp:BoundField DataField="EVENT_NAME" HeaderText="赛事名称" />
                    <asp:BoundField DataField="EVENT_NAME_EN" HeaderText="赛事英文名称" />
                    <asp:BoundField DataField="EVENT_DESC" Visible="false" HeaderText="赛事描述" />
                    <asp:BoundField DataField="START_DATE" HeaderText="开始日期" />
                    <asp:BoundField DataField="STATUSNAME" HeaderText="赛事状态" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250" HeaderText="操作">
                        <ItemTemplate>
                            <asp:Label ID="lblEventItemID" runat="server"  Text='<%#Eval("EventItem_ID") %>' style=" display:none;"></asp:Label>
                            <asp:Label ID="lblEventZoneID" runat="server"  Text='<%#Eval("EVENTZONE_ID") %>' style=" display:none;"></asp:Label>
                            <asp:Label ID="lblEventStatus" runat="server"  Text='<%#Eval("STATUS") %>' style=" display:none;"></asp:Label>
                            <asp:LinkButton ID="hlEdit" Text="编辑" runat="server" CssClass="HyperLink" CausesValidation="false" onclick="btnAddEvent_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlDelete" CssClass="HyperLink" CausesValidation="false" OnClientClick="return showConfirm('确定删除?');" runat="server" Text="删除" onclick="btnDeleteEvent_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlActivity" CssClass="HyperLink" CausesValidation="false" runat="server" Text="激活" OnClick="ActiveEvent_Click" ></asp:LinkButton>
                            <asp:LinkButton ID="hlPause" CssClass="HyperLink"  CausesValidation="false" runat="server" Text="暂停" OnClick="PaurseEvent_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlAbort" CssClass="HyperLink" CausesValidation="false" runat="server" Text="终止" OnClick="AbortEvent_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlEventSaveAs" CssClass="HyperLink" CausesValidation="false" runat="server" Text="赛事另存" onclick="btnAddEvent_Click" CommandName="SaveAs"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
<asp:Button ID="btnFake" runat="server" style="display:none" />
<asp:Panel ID="pnlPopup" runat="server" style="width:1000px;height:560px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
        <span>赛事详细页面</span>
    </asp:Panel>
    <fieldset>
    <legend>赛事详细信息</legend>
        <table class="NoBorderTable" width="100%" id="popEventTeamTbl">
            <tr>
                <td colspan="5" style="height:10px;"></td>
            </tr>
            <tr>
                <td style="width:5px;"></td>
                <td style="width:50px; text-align:right;">
                    <span>赛事项目</span>
                </td>
                <td style="width:90px">
                    <asp:DropDownList ID="popDrpEventItem" runat="server" Width="151px" Height="18px"  AutoPostBack="true"
                        CssClass="DropdownList" 
                        onselectedindexchanged="popDrpEventItem_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td style="width:50px; text-align:right;">
                    <span>赛事区域</span>
                </td>
                <td style="width:90px">
                    <asp:DropDownList ID="popDrpEventZone" runat="server" Width="151px" Height="18px" CssClass="DropdownList"></asp:DropDownList> 
                </td>
                <td style="width:50px; text-align:right;">
                    <span style="color:Red;">*</span><span>赛事名称</span>
                </td>
                <td style="width:90px">
                    <asp:TextBox ID="popTxtEventName" runat="server"  CssClass="TextBox" MaxLength="100"></asp:TextBox> 
                </td>
                <td style="width:80px; text-align:right;">
                    <span style="color:Red;">*</span><span>赛事英文名称</span>
                </td>
                <td style="width:90px">
                    <asp:TextBox ID="popTxtEventNameEn" runat="server"  CssClass="TextBox" MaxLength="100"></asp:TextBox> 
                </td>
            </tr>
            <tr>
                <td style="width:5px;"></td>
                <td style="width:50px;display:none;">
                    <span>赛事描述</span>
                </td>
                <td style="width:90px;display:none;">
                    <asp:TextBox ID="popTxtEventDesc" runat="server"  CssClass="TextBox" MaxLength="100"></asp:TextBox> 
                </td>
                <td style="width:50px; text-align:right;">
                    <span style="color:Red;">*</span><span>开始时间</span>
                </td>
                <td style="width:90px">
                    <div class="calendarContainer">
                        <YMGS:AjaxCalendar ID="popCalStartDate" runat="server" NeedCalendarButton="true" />
                    </div>
                </td>
                <td style="width:50px; text-align:right;">
                    <span></span>
                </td>
                <td style="width:90px">
                    <asp:DropDownList ID="popDrpEventStatus" runat="server" Width="151px" Height="18px" CssClass="DropdownList"></asp:DropDownList> 
                </td>
            </tr>
        </table>
    </fieldset>
    <table>
        <tr>
            <td colspan="3" style="height:2px;"></td>
        </tr>
    </table>
    <fieldset>
    <legend>赛事成员</legend>
    <div style=" margin-left:10%;">
        <div style=" width:88%; float:left; border:1px solid gray;">
            <table class="NoBorderTable" width="100%">
                <tr>
                    <td colspan="5" style="height:2px;"></td>
                </tr>
                <tr>
                    <td style="width:5px;"></td>
                    <td style="width:90px; text-align:right;">
                    <span>赛事项目</span>
                    </td>
                    <td style="width:90px">
                        <asp:DropDownList ID="popDrpEventItemTeam" runat="server" Width="151px" Height="18px"  AutoPostBack="true"
                        CssClass="DropdownList" ></asp:DropDownList>
                    </td>
                    <td style="width:90px; text-align:right;">
                        <span>赛事成员名称</span>
                    </td>
                    <td style="width:90px">
                        <asp:TextBox ID="popTxtEventTeamName" runat="server"  CssClass="TextBox" MaxLength="50"></asp:TextBox> 
                    </td>
                    <td>
                        <span>区域</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtZone" runat="server"  CssClass="TextBox" MaxLength="50" Width="100px"></asp:TextBox>
                        <asp:TextBox ID="txtZoneID" runat="server"  CssClass="TextBox" MaxLength="50" Visible="false"></asp:TextBox>
                        <asp:Button ID="btnAddZone" runat="server" Text="选择区域" Width="70px"  CausesValidation="false" CssClass="Button" onclick="btnAddZone_Click"/>
                    </td>
                </tr>
                <tr>
                    <td style="width:5px;"></td>
                    <td style="width:90px; text-align:right;">
                        <span>类型(国家/职业)</span>
                    </td>
                    <td style="width:90px">
                        <asp:DropDownList ID="popDrpParamType1" runat="server" Width="151px" Height="18px" CssClass="DropdownList"></asp:DropDownList>
                    </td>
                    <td style="width:50px; text-align:right;">
                        <span>类型(男子/女子)</span>
                    </td>
                    <td style="width:90px">
                        <asp:DropDownList ID="popDrpParamType2" runat="server" Width="151px" Height="18px" CssClass="DropdownList"></asp:DropDownList>
                    </td>
                    <td colspan="2" style="width:90px"><asp:Button ID="popBtnQuery" runat="server" Text="查询" Width="70px"  CausesValidation="false" CssClass="Button" OnClick="popBtnQuery_Click"/></td>
                </tr>
            </table>
            <div style="height:270px; width:100%;border:1px solid gray; ">
                <div style=" width:89%;">
                    <div style="float:left; width:40%; margin:10px -12px 10px 50px;">
                        <asp:ListBox ID="popCkcEventTeamList" runat="server" Height="250" Width="250" SelectionMode="Multiple"></asp:ListBox>
                    </div>
                    <div class="inline" style=" margin-top:95px;" >
                        <div>
                            <asp:Button ID="btnSelectEventTeam" CausesValidation="false" runat="server" Text="选择>>" OnClick="selectEventTeam_Click" />
                        </div>
                        <div style="padding-top:10px;">
                            <asp:Button ID="btnRemoveEventTeam" CausesValidation="false" runat="server" Text="<<移除" OnClick="removeEventTeam_Click" />
                        </div>
                    </div>
                    <div class="inline" style="width:40%; margin-left:10px;">
                        <asp:ListBox ID="popCkcSelectedEventTeamList" runat="server" Height="250" Width="250" SelectionMode="Multiple"></asp:ListBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </fieldset>
    <br />
    <asp:CustomValidator ID="valEventName"  runat="server" Display="None" ErrorMessage="赛事名称和赛事英文名称不能为空!" ClientValidationFunction="validEventName"></asp:CustomValidator>
    <asp:CustomValidator ID="valStartDate"  runat="server" Display="None" ErrorMessage="赛事开始时间不能为空!" ClientValidationFunction="validEventStartDate"></asp:CustomValidator>
    <%--<asp:CustomValidator ID="valEndDate"  runat="server" Display="None" ErrorMessage="赛事结束时间不能为空!" ClientValidationFunction="validEventEndDate"></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator1"  runat="server" Display="None" ErrorMessage="赛事结束时间不能早于赛事开始时间" ClientValidationFunction="validEndDate"></asp:CustomValidator>--%>
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
                <asp:Button ID="btnSave" runat="server" Text="保存" Width="70px"  CausesValidation="true" CssClass="Button" onclick="btnSaveEvent_Click"/>
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
    CancelControlID="btnCancel" 
    PopupDragHandleControlID="pnlPopupZoneHeader">
    </ajaxToolkit:ModalPopupExtender>         
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    function validEventName(source, args) {
        if (chkIsNull('popTxtEventName') || chkIsNull('popTxtEventNameEn')) {
            args.IsValid = false;
            return;
        }
        args.IsValid = true;
    }

    function validEventStartDate(source, args) {
        if (chkIsNull('popCalStartDate_txtDateHolder')) {
            args.IsValid = false;
            return
        }
        args.IsValid = true;
    }
</script>
</asp:Content>
