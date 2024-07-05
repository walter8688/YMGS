<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="ChampionGameManagePage.aspx.cs" Inherits="YMGS.Manage.Web.GameMarket.ChampionGameManagePage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>
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
                            <td style="width:100px">
                                <span>冠军赛事类型</span>
                            </td>
                            <td style="width:120px">
                                <asp:DropDownList ID="drpChampEventType" runat="server" Width="155px"></asp:DropDownList>
                            </td>
                            <td style="width:10px">                                
                            </td>
                            <td style="width:100px">
                                <span>冠军赛事名称</span>
                            </td>
                            <td style="width:120px">
                                <asp:TextBox ID="txtChampEventName" runat="server" Width="150px" CssClass="TextBox" MaxLength="100"></asp:TextBox>
                            </td>
                            <td style="width:10px">                                
                            </td>
                            <td style="width:100px">
                                <span>冠军赛事英文名称</span>
                            </td>
                            <td style="width:120px">
                                <asp:TextBox ID="txtChampEventNameEn" runat="server" Width="150px" CssClass="TextBox" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:20px">                                
                            </td>
                            <td style="width:100px">
                                <span>冠军赛事描述</span>
                            </td>
                            <td style="width:120px">
                                <asp:TextBox ID="txtChampEventDesc" runat="server" Width="150px" CssClass="TextBox" MaxLength="100"></asp:TextBox>
                            </td>
                            <td style="width:10px">                                
                            </td>
                            <td style="width:100px">
                                <span>开始时间</span>
                            </td>
                            <td style="width:120px">
                                <div class="calendarContainer">
                                    <YMGS:AjaxCalendar ID="calStartDate" runat="server" NeedCalendarButton="true" />
                                </div>
                            </td>
                            <td style="width:10px">                                
                            </td>
                            <td style="width:100px">
                                <span>结束时间</span>
                            </td>
                            <td style="width:120px">
                                <div class="calendarContainer">
                                    <YMGS:AjaxCalendar ID="calEndDate" runat="server" NeedCalendarButton="true" />
                                </div>
                            </td>
                            <td style="width:10px">
                            </td>
                            <td align="right" colspan="2">
                                <asp:Button runat="server" ID="btnSearch"  Width="70px" Text="查询" CausesValidation="false" CssClass="Button" OnClick="btnSearch_Click" />
                                &nbsp;<asp:Button runat="server" ID="btnNew" Width="70px" Text = "新增" CssClass="Button"  CausesValidation="false"  OnClick="btnNew_Click" />
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
            <asp:GridView ID="gdvChampEvent" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%" DataKeyNames="Champ_Event_ID"
                EmptyDataText="请输入合适的查询条件查询冠军赛事!" OnRowDataBound="gdvChampEvent_RowDataBind" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" HeaderText="冠军赛事类型">
                        <ItemTemplate>
                            <asp:Label ID="lblChampEventTypeName" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Champ_Event_Name" HeaderText="冠军赛事名称" />
                    <asp:BoundField DataField="Champ_Event_Name_En" HeaderText="冠军赛事英文名称" />
                    <asp:BoundField DataField="Champ_Event_Desc" HeaderText="冠军赛事描述" />
                    <asp:BoundField DataField="Champ_Event_StartDate" HeaderText="开始日期" />
                    <asp:BoundField DataField="Champ_Event_EndDate" HeaderText="结束日期" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40" HeaderText="状态">
                        <ItemTemplate>
                            <asp:Label ID="lblChampEventStatusName" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250" HeaderText="操作">
                        <ItemTemplate>
                            <asp:Label ID="lblChampEventTypeID" runat="server"  Text='<%#Eval("Champ_Event_Type") %>' style=" display:none;"></asp:Label>
                            <asp:Label ID="lblEventID" runat="server"  Text='<%#Eval("Event_ID") %>' style=" display:none;"></asp:Label>
                            <asp:Label ID="lblChampEventStatusID" runat="server"  Text='<%#Eval("Champ_Event_Status") %>' style=" display:none;"></asp:Label>
                            <asp:LinkButton ID="hlEdit" Text="编辑" runat="server" CssClass="HyperLink" CausesValidation="false" onclick="btnEdit_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlDelete" Text="删除" runat="server" CssClass="HyperLink" CausesValidation="false" OnClientClick="return showConfirm('确定删除冠军赛事?');"  onclick="btnDeleteEvent_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlActivity" CssClass="HyperLink" CausesValidation="false" runat="server" Text="激活" OnClick="ActiveEvent_Click" ></asp:LinkButton>
                            <asp:LinkButton ID="hlPause" CssClass="HyperLink"  CausesValidation="false" runat="server" Text="暂停" OnClick="PaurseEvent_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlAbort" CssClass="HyperLink" CausesValidation="false" runat="server" Text="终止" OnClick="AbortEvent_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlRecordWinMems" CssClass="HyperLink" CausesValidation="false" runat="server" Text="录入冠军" onclick="btnRecordWinMems_Click"></asp:LinkButton>
                            <asp:LinkButton ID="hlFinish" CssClass="HyperLink" CausesValidation="false" runat="server" Text="结束" OnClick="FinishEvent_Click" OnClientClick="return showConfirm('确定结束冠军赛事?');"></asp:LinkButton>
                            <asp:LinkButton ID="hlSaveAs" CssClass="HyperLink" CausesValidation="false" runat="server" Text="另存" onclick="btnEdit_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
<asp:Button ID="btnFake" runat="server" style="display:none" />
<asp:TextBox ID="txtChampEventType" runat="server" Text="1" style="display:none"></asp:TextBox>
<asp:TextBox ID="txtChampEventId" runat="server" style="display:none"></asp:TextBox>
<asp:Panel ID="pnlPopup" runat="server" style="width:1000px;height:650px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
        <span>冠军赛事详细页面</span>
    </asp:Panel>
    <table>
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
    </table>
    <div style="vertical-align:middle; line-height:20px;">
        <asp:Label ID="poplblChampEvent" runat="server" Text="体育" Width="100px" CssClass="SelTab"></asp:Label>
        <asp:Label ID="poplblChampEntertainment" runat="server" Text="娱乐" Width="100px" CssClass="UnSelTab"></asp:Label>
    </div>
    <fieldset>
        <legend><span id="spnChampTypeName">体育</span></legend>
        <div id="divSports">
        <table class="NoBorderTable" width="100%">
            <tr>
                <td colspan="9" style="height:10px;"></td>
            </tr>
            <tr>
                <td style="width:5px;"></td>
                <td style="width:80px;text-align:right;">
                    <font color=red>*</font><span>冠军赛事名称</span>
                </td>
                <td style="width:90px">
                    <asp:TextBox ID="popTxtEventName" runat="server"  CssClass="TextBox" MaxLength="100" Width="150px"></asp:TextBox> 
                </td>
                <td style="width:110px;text-align:right;">
                    <font color=red>*</font><span>冠军赛事英文名称</span>
                </td>
                <td style="width:90px">
                    <asp:TextBox ID="popTxtEventNameEn" runat="server"  CssClass="TextBox" MaxLength="100" Width="150px"></asp:TextBox> 
                </td>
                <td style="width:60px;text-align:right;">
                    <font color=red>*</font><span>开始时间</span>
                </td>
                <td style="width:90px">
                    <div class="calendarContainer">
                        <YMGS:AjaxCalendar ID="popCalStartDate" runat="server" NeedCalendarButton="true" />
                        <asp:TextBox ID=txtBeginTime CssClass=TextBox runat=server Width=40px ></asp:TextBox>
                    </div>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                    TargetControlID="txtBeginTime" 
                    Mask="99:99"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Time"
                    AcceptAMPM="False"           
                    ErrorTooltipEnabled="True" />
                </td>
                <td style="width:60px;text-align:right;">
                    <font color=red>*</font><span>结束时间</span>
                </td>
                <td style="width:90px">
                    <div class="calendarContainer">
                        <YMGS:AjaxCalendar ID="popCalEndDate" runat="server" NeedCalendarButton="true" />
                        <asp:TextBox ID=txtEndTime CssClass=TextBox runat=server Width=40px ></asp:TextBox>
                    </div>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                    TargetControlID="txtEndTime" 
                    Mask="99:99"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Time"
                    AcceptAMPM="False"           
                    ErrorTooltipEnabled="True" />
                </td>
            </tr>
            <tr>
                <td style="width:5px;"></td>
                <td style="width:80px;text-align:right; ">
                    <span>冠军赛事描述</span>
                </td>
                <td style="width:90px">
                    <asp:TextBox ID="popTxtEventDesc" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>   
                <td style="width:110px; text-align:right;">
                    <span>赛事项目</span>
                </td>
                <td style="width:90px">
                    <asp:DropDownList ID="popDrpEventItem" runat="server" Width="150px" Height="18px"  AutoPostBack="true"
                        CssClass="DropdownList" OnSelectedIndexChanged="popDrpEventItem_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td style="width:60px;text-align:right;">
                    <span>赛事区域</span>
                </td>
                <td style="width:90px">
                    <asp:DropDownList ID="popDrpEventZone" runat="server" Width="150px" Height="18px" AutoPostBack="true" CssClass="DropdownList" OnSelectedIndexChanged="popDrpEventZone_SelectedIndexChanged"></asp:DropDownList> 
                </td>
                <td style="width:60px;text-align:right;">
                    <span>赛事</span>
                </td>
                <td style="width:90px">
                    <asp:DropDownList ID="popDrpEvent" runat="server" Width="150px" Height="18px" AutoPostBack="true" CssClass="DropdownList" OnSelectedIndexChanged="popDrpEvent_SelectedIndexChanged"></asp:DropDownList> 
                </td>
                
            </tr>
        </table>
        <div style="height:350px; width:100%;border:0px solid gray; ">
            <div style=" width:88%;">
                <div style="float:left; width:30%; margin:20px -10px 10px 50px;">
                    <div>赛事成员列表</div>
                    <div>
                        <asp:ListBox ID="lstEventTeam" runat="server" Height="300" Width="250" SelectionMode="Multiple"></asp:ListBox>
                    </div>
                </div>
                <div class="inline" style="margin-top:120px;">
                    <div>
                        <asp:Button ID="btnSelectEventTeam" CausesValidation="false" runat="server" Text="选择>>" OnClick="btnSelectedEventMember"  />
                    </div>
                    <div style="padding-top:10px;">
                        <asp:Button ID="btnRemoveEventTeam" CausesValidation="false" runat="server" Text="<<移除" OnClick="btnRemoveEventMember" />
                    </div>
                </div>
                <div class="inline" style="margin:20px 10px 10px 0px;">
                    <div>冠军赛事成员列表</div>
                    <div>
                        <asp:ListBox ID="lstEventSelectedTeam" runat="server" Height="300" Width="250" SelectionMode="Multiple"></asp:ListBox>   
                    </div>
                </div>
                <div class="inline" style="width:20%; margin:20px 10px 10px 20px;">
                    <div>获胜成员列表</div>
                    <div>
                        <asp:ListBox ID="lsbEventWinMemList" runat="server" Height="300" Width="250" SelectionMode="Multiple"></asp:ListBox>   
                    </div>
                </div>
            </div>
        </div>
        </div>
        <div id="divEntertainment" style="display:none;">
        <table class="NoBorderTable" width="100%">
            <tr>
                <td colspan="5" style="height:10px;"></td>
            </tr>
            <tr>
                <td style="width:15px;"></td>
                <td style="width:70px;">
                    <font color=red>*</font><span>冠军赛事名称</span>
                </td>
                <td style="width:90px">
                    <asp:TextBox ID="popTxtEntChampName" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>
                <td style="width:80px;">
                    <font color=red>*</font><span>冠军英文赛事名称</span>
                </td>
                <td style="width:90px">
                    <asp:TextBox ID="popTxtEntChampNameEn" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>
                <td style="width:70px;">
                    <span>冠军赛事描述</span>
                </td>
                <td style="width:90px">
                    <asp:TextBox ID="popTxtEntChampDesc" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td> 
            </tr>
            <tr>
                <td style="width:15px;"></td>            
                <td style="width:70px;display:none;">
                    <font color=red>*</font><span>开始时间</span>
                </td>
                <td style="width:90px;display:none;">
                    <div class="calendarContainer">
                        <YMGS:AjaxCalendar ID="popEntCalStartDate" runat="server" NeedCalendarButton="true" />
                        <asp:TextBox ID=txtEntBeignTime CssClass=TextBox runat=server Width=40px ></asp:TextBox>
                    </div>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                    TargetControlID="txtEntBeignTime" 
                    Mask="99:99"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Time"
                    AcceptAMPM="False"           
                    ErrorTooltipEnabled="True" />
                </td>
                <td style="width:70px;">
                    <font color=red>*</font><span>结束时间</span>
                </td>
                <td style="width:90px">
                    <div class="calendarContainer">
                        <YMGS:AjaxCalendar ID="popEntCalEndDate" runat="server" NeedCalendarButton="true" />
                        <asp:TextBox ID=txtEntEndTime CssClass=TextBox runat=server Width=40px ></asp:TextBox>
                    </div>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                    TargetControlID="txtEntEndTime" 
                    Mask="99:99"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Time"
                    AcceptAMPM="False"           
                    ErrorTooltipEnabled="True" />
                </td>
                <td colspan="2"></td>
            </tr>
        </table>
        <div>
            <div style="float:left; width:250px; margin:20px 10px 10px 24px;">
                <div>冠军赛事成员列表</div>
                <div>
                    <asp:ListBox ID="lsbEntEventTeam" runat="server" Height="300" Width="250" SelectionMode="Multiple"></asp:ListBox>
                </div>
            </div>
            <div class="inline" style="margin-top:80px; width:160px;">
                <div style="margin-bottom:2px;"><span>冠军赛事成员名称:</span></div>
                <div><asp:TextBox ID="txtAddChampEventName" runat="server" CssClass="TextBox" Width="160px"></asp:TextBox></div>
                <div style="margin-bottom:2px;"><span>冠军赛事成员英文名称:</span></div>
                <div><asp:TextBox ID="txtAddChampEventNameEn" runat="server" CssClass="TextBox" Width="160px"></asp:TextBox></div>
                <div><asp:Button ID="btnAddChampEventName" runat="server" Text="<<新增" Width="70px"  CausesValidation="false" CssClass="Button" OnClick="btnAddMember_Click" OnClientClick="return CheckEntMember();"/></div>
                <div><asp:Button ID="btnRemoveChampEventName" runat="server" Text=">>移除" Width="70px"  CausesValidation="false" CssClass="Button" OnClick="btnRemoveMember_Click"/></div>
                <div id="ChampEventNameMsg" style="color:Red; display:none;"><span>冠军赛事成员名称和冠军赛事成员英文名称不能为空</span></div>
            </div>
            <div class="inline" style="width:20%; margin:20px 0px 10px 20px;">
                <div>获胜成员列表</div>
                <div>
                    <asp:ListBox ID="lsbEntWimMemList" runat="server" Height="300" Width="250" SelectionMode="Multiple"></asp:ListBox>
                </div>
            </div>
        </div>
        </div>
    </fieldset>
    <table>
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
    </table>
    <br />
    <asp:CustomValidator ID="valEventName"  runat="server" Display="None" ErrorMessage="冠军赛事名称和冠军赛事英文名称不能为空!" ClientValidationFunction="validChampEventName"></asp:CustomValidator>
    <asp:CustomValidator ID="valStartDate"  runat="server" Display="None" ErrorMessage="赛事开始时间不能为空!" ClientValidationFunction="validChampEventStartDate"></asp:CustomValidator>
    <asp:CustomValidator ID="valEndDate"  runat="server" Display="None" ErrorMessage="赛事结束时间不能为空;赛事结束时间不能早于赛事开始时间!" ClientValidationFunction="validChampEventEndDate"></asp:CustomValidator>
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
                <asp:Button ID="btnSave" runat="server" Text="保存" Width="70px"  CausesValidation="true" CssClass="Button" onclick="btnSave_Click"/>
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
<asp:Button ID="btnFakeRecordWinMembers" runat="server" style="display:none" />
<asp:Panel ID="pnlRecordWinMembers" runat="server" style="width:650px;height:550px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlRecordWinMembersHeader" runat="server" CssClass="ModalPoupHeader">
        <span>维护获胜成员</span>
    </asp:Panel>
    <table>
        <tr>
            <td colspan="3" style="height:10px;"></td>
        </tr>
    </table>
    <div>
        <div style="float:left; width:250px; margin:20px 10px 10px 24px;">
            <div>冠军赛事成员列表</div>
            <div>
                <asp:ListBox ID="lstEventMembers" runat="server" Height="300" Width="250" SelectionMode="Multiple"></asp:ListBox>
            </div>
        </div>
        <div class="inline" style="margin-top:80px;">
            <div><asp:Button ID="Button1" runat="server" Text="新增>>" Width="70px"  CausesValidation="false" CssClass="Button" OnClick="btnAddWinMember_Click"/></div>
            <div><asp:Button ID="Button2" runat="server" Text="<<移除" Width="70px"  CausesValidation="false" CssClass="Button" OnClick="btnRemoveWinMember_Click"/></div>
        </div>
        <div class="inline" style="width:20%; margin:20px 0px 10px 0px;">
            <div>获胜成员列表</div>
            <div>
                <asp:ListBox ID="lstWinMembers" runat="server" Height="300" Width="250" SelectionMode="Multiple"></asp:ListBox>
            </div>
        </div>
    </div>
    <table class="NoBorderTable" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnRecordWinMembers" runat="server" Text="保存" Width="70px"  CausesValidation="false" CssClass="Button" onclick="btnRecordWinMembers_Click"/>
                &nbsp;
                <asp:Button ID="btnCancleRecordWinMembers" runat="server" Text="取消" Width="70px" CausesValidation="false" CssClass="Button" />
            </td>
        </tr>
    </table>
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlRecordWinMembers" BehaviorID="mdlRecordWinMembers" 
    TargetControlID="btnFakeRecordWinMembers"
    PopupControlID="pnlRecordWinMembers"
    BackgroundCssClass="ModalPopupBackground"                         
    CancelControlID="btnCancleRecordWinMembers"
    PopupDragHandleControlID="pnlRecordWinMembersHeader">
    </ajaxToolkit:ModalPopupExtender>    
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    $(function () {
        load();
        EndRequestHandler();

        $('[id$=poplblChampEntertainment]').live('click', function () {
            $(this).removeClass('UnSelTab').addClass('SelTab');
            $('[id$=poplblChampEvent]').removeClass('SelTab').addClass('UnSelTab');
            $('#divSports').hide();
            $('#divEntertainment').show();
            $('[id$=txtChampEventType]').val('2'); //Entertainment
            $('#spnChampTypeName').html('娱乐');
        });

        $('[id$=poplblChampEvent]').live('click', function () {
            $(this).removeClass('UnSelTab').addClass('SelTab');
            $('[id$=poplblChampEntertainment]').removeClass('SelTab').addClass('UnSelTab');
            $('#divEntertainment').hide();
            $('#divSports').show();
            $('[id$=txtChampEventType]').val('1'); //Sports
            $('#spnChampTypeName').html('体育');
        });

        $('[id$=btnNew]').live('click', function () {
            var champTypeId = $('[id$=txtChampEventType]').val();
            if (champTypeId == "1") {
                $('#divSports').show();
                $('#divEntertainment').hide;
            }
            else {
                $('#divSports').hide();
                $('#divEntertainment').show;
            }
        });

        $('[id$=btnAddChampEventName]').live('click', function () {
            if (chkIsNull('txtAddChampEventName')) {
                return;
            }
        })
    })

    function InitPageStype() {
        $('[id$=poplblChampEvent]').css('cursor', 'pointer');
        $('[id$=poplblChampEntertainment]').css('cursor', 'pointer');
        //$find('<%= popCalStartDate.ClientID%>').set_disabled(true);
        //$find('<%= popCalEndDate.ClientID%>').set_disabled(true)
        var champTypeId = $('[id$=txtChampEventType]').val();
        if (champTypeId == "1") {
            $('#divSports').css('display', '');
            $('#divEntertainment').css('display', 'none');
            $('#spnChampTypeName').html('体育');
            $('[id$=poplblChampEvent]').removeClass('UnSelTab').addClass('SelTab');
            $('[id$=poplblChampEntertainment]').removeClass('SelTab').addClass('UnSelTab');
        }
        else {
            $('#divSports').css('display', 'none');
            $('#divEntertainment').css('display', '');
            $('#spnChampTypeName').html('娱乐');
            $('[id$=poplblChampEntertainment]').removeClass('UnSelTab').addClass('SelTab');
            $('[id$=poplblChampEvent]').removeClass('SelTab').addClass('UnSelTab');
        }
    }

    function EndRequestHandler() {
        InitPageStype();
    }

    function validChampEventName(source, args) {
        var champType = $('[id$=txtChampEventType]').val();
        if (champType == "1") {
            if (chkIsNull('popTxtEventName') || chkIsNull('popTxtEventNameEn')) {
                args.IsValid = false;
                return;
            }
        }
        else {
            if (chkIsNull('popTxtEntChampName') || chkIsNull('popTxtEntChampNameEn')) {
                args.IsValid = false;
                return;
            }
        }
        args.IsValid = true;
    }

    function validChampEventStartDate(source, args) {
        var calStartDate = $find('<%= popCalStartDate.ClientID %>').get_value();
        var calEntStartDate = $find('<%= popEntCalStartDate.ClientID %>').get_value();
        var champType = $('[id$=txtChampEventType]').val();
        if (champType == "1") {
            if (chkIsNull('popCalStartDate_txtDateHolder')) {
                args.IsValid = false;
                return;
            }
            if (!isValidDate(calStartDate)) {
                args.IsValid = false;
                return;
            }
        }
        else {
//            if (chkIsNull('popEntCalStartDate_txtDateHolder')) {
//                args.IsValid = false;
//                return;
            //            }
            if ($('[id$=txtChampEventType]').val() == "1") {
                args.IsValid = true;
                return;
            }
        }
        args.IsValid = true;
    }

    function validChampEventEndDate(source, args) {
        var calStartDate = $find('<%= popCalStartDate.ClientID %>').get_value();
        var beginTime = $("#" + "<%= txtBeginTime.ClientID %>");
        var calEntStartDate = $find('<%= popEntCalStartDate.ClientID %>').get_value();
        var entBeginTime = $("#" + "<%= txtEntBeignTime.ClientID %>");
        var calEndDate = $find('<%= popCalEndDate.ClientID %>').get_value();
        var endTime = $("#" + "<%= txtEndTime.ClientID %>");
        var calEntEndDate = $find('<%= popEntCalEndDate.ClientID %>').get_value();
        var entEndTime = $("#" + "<%= txtEntEndTime.ClientID %>");
        var champType = $('[id$=txtChampEventType]').val();
        var sportBeginTime = calStartDate + beginTime;
        var sportEndTime = calEndDate + endTime;
        var entBeginTime = calEntStartDate + entBeginTime;
        var entEndTime = calEntEndDate + entEndTime;
        if (champType == "1") {
            if (chkIsNull('popCalEndDate_txtDateHolder')) {
                args.IsValid = false;
                return;
            }
            if (!isValidDate(calEndDate)) {
                args.IsValid = false;
                return;
            }
            if (sportEndTime <= sportBeginTime) {
                args.IsValid = false;
                return;
            }
        }
        else {
            if (chkIsNull('popEntCalEndDate_txtDateHolder')) {
                args.IsValid = false;
                return;
            }
            if ($('[id$=txtChampEventType]').val() == "1") {
                args.IsValid = true;
                return;
            }
//            if (entEndTime <= entBeginTime) {
//                args.IsValid = false;
//                return;
//            }
        }
        args.IsValid = true;
    }

    function CheckEntMember() {
        if (chkIsNull('txtAddChampEventName') || chkIsNull('txtAddChampEventNameEn')) {
            $('#ChampEventNameMsg').show();
            return false;
        }
        $('#ChampEventNameMsg').hide();
        return true;
    }
</script>
</asp:Content>
