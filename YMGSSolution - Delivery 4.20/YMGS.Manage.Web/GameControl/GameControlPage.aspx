<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="GameControlPage.aspx.cs" Inherits="YMGS.Manage.Web.GameControl.GameControlPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">

<asp:UpdatePanel ID="uplQuery" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<table width=100% class=NoBorderTable>
    <tr>
        <td>
            <fieldset>
                <legend>查询条件</legend>
                    <table class=NoBorderTable style="margin-top:5px;margin-bottom:5px;width:100%">
                        <tr>
                            <td style="width:20px"></td>
                            <td style="width:70px">
                                <span>赛事项目</span>
                            </td>
                            <td style="width:150px">
                                <asp:DropDownList ID=ddlEventItem runat=server AutoPostBack=true Width=152px 
                                    onselectedindexchanged="ddlEventItem_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td style="width:20px">                                
                            </td>
                            <td style="width:90px">
                                <span>赛事区域</span>
                            </td>
                            <td style="width:150px">
                                <asp:DropDownList ID=ddlEventZone runat=server Width=152px AutoPostBack="True" 
                onselectedindexchanged="ddlEventZone_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td style="width:20px">
                            </td>
                            <td style="width:90px">
                                <span>赛事</span>
                            </td>
                            <td style="width:150px">
                                <asp:DropDownList ID=ddlEvent runat=server CssClass="DropdownList" Width=152px></asp:DropDownList>
                            </td>
                            <td align="right">
                            </td>
                        </tr>
                        <tr>
                            <td style="width:20px"></td>
                            <td style="width:70px">
                                <span>比赛名称</span>
                            </td>
                            <td style="width:150px">
                                <asp:TextBox ID=txtMatchName runat=server Width=150px CssClass=TextBox></asp:TextBox>
                            </td>
                            <td style="width:20px">                                
                            </td>
                            <td style="width:90px">
                                <span>比赛开始日期</span>
                            </td>
                            <td style="width:150px">
                                <div class="calendarContainer">
                                    <YMGS:AjaxCalendar ID=startDate runat=server />
                                </div>
                            </td>
                            <td style="width:20px">
                            </td>
                            <td style="width:90px">
                                <span>比赛结束日期</span>
                            </td>
                            <td style="width:150px">
                                <div class="calendarContainer">
                                    <YMGS:AjaxCalendar ID=endDate runat=server />
                                </div>
                            </td>
                            <td align="right">
                                <asp:Button runat=server ID=btnSearch  Width=70px Text="查询" CausesValidation=false CssClass=Button OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
            </fieldset>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>

<asp:Timer ID="tmGameControl" runat="server" OnTick="timer_Tick"></asp:Timer>
<asp:UpdatePanel ID="updPanel" runat="server">
<ContentTemplate>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gridData" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%" DataKeyNames="MATCH_ID"
                OnRowCommand="gridData_RowCommand"   OnRowDataBound="gridData_RowDataBound"                                                   
                EmptyDataText="请输入合适的查询条件查询比赛!"
                >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="MATCH_NAME" HeaderText="比赛名称"/>
                    <%--<asp:BoundField DataField="EVENTTYPE_NAME" HeaderText="赛事类别" />
                    <asp:BoundField DataField="EVENTITEM_NAME" HeaderText="赛事项目" />--%>
                    <asp:TemplateField HeaderText="比赛时间">
                        <ItemTemplate>
                            <asp:Label ID=lblMatchDate  runat=server></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="EVENT_NAME" HeaderText="赛事" />                   
                    <asp:TemplateField HeaderText="比赛开始时间">
                        <ItemTemplate>
                            <asp:Label ID=lblStartDate  runat=server></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="自动封盘时间">
                        <ItemTemplate>
                            <asp:Label ID=lblFreezeDate  runat=server></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态">
                        <ItemTemplate>
                            <asp:Label ID=lblStatus runat=server></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="盘口管理" ItemStyle-HorizontalAlign=Center ItemStyle-Width=50px
                                     ItemStyle-Wrap=false ItemStyle-VerticalAlign=Middle>
                        <ItemTemplate>
                            <asp:LinkButton ID=btnHandicapManange CssClass=LinkButton runat=server Text="盘口管理" CausesValidation=false></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign=Center ItemStyle-Width=150px
                                     ItemStyle-Wrap=false ItemStyle-VerticalAlign=Middle>
                        <ItemTemplate>
                            <asp:LinkButton ID=btnActivated CssClass=LinkButton runat=server Text="激活" CausesValidation=false></asp:LinkButton>
                            <asp:LinkButton ID=btnEditStartDate CssClass=LinkButton runat=server Text="修改时间" CausesValidation=false></asp:LinkButton>
                            <asp:LinkButton ID=btnStartMatch CssClass=LinkButton runat=server Text="开始比赛" CausesValidation=false></asp:LinkButton>
                            <asp:LinkButton ID=btnMatchHalfEnd CssClass=LinkButton runat=server OnClientClick="return confirm('确定半场结束?')" Text="半场结束" CausesValidation=false></asp:LinkButton>
                            <asp:LinkButton ID=btnMatchSecHalfStart CssClass=LinkButton runat=server Text="下半场开始" CausesValidation=false></asp:LinkButton>
                            <asp:LinkButton ID=btnMatchFullEnd CssClass=LinkButton runat=server OnClientClick="return confirm('确定全场结束?')"  Text="全场结束" CausesValidation=false></asp:LinkButton>
                            <asp:LinkButton ID=btnFrezz CssClass=LinkButton runat=server Text="封盘" CausesValidation=false></asp:LinkButton>
                            <asp:LinkButton ID=btnClearMarket CssClass=LinkButton runat=server Text="清理市场" CausesValidation=false></asp:LinkButton>
                            <asp:LinkButton ID=btnRecordScore CssClass=LinkButton runat=server Text="录入比分" CausesValidation=false></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID=pageNavigator runat=server />
        </td>
    </tr>
</table>
<asp:Button ID=btnFake runat=server style="display:none" />
<asp:Panel id=pnlPopup runat=server style="width:350px;height:300px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
        <span>修改比赛时间</span>
    </asp:Panel>
    <br />
    <table class=NoBorderTable width=100%>
        <tr>
            <td style="width:1px"></td>
            <td style="width:100px" align=right>
                <font color=red>*</font>
                <span>比赛开始时间</span>
            </td>
            <td style="width:200px">
                <div class="calendarContainer">
                    <YMGS:AjaxCalendar ID=txtBeginDate  runat=server />
                    <asp:TextBox ID=txtBeginTime CssClass=TextBox runat=server Width=40px ></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBeginDate" ErrorMessage="请输入比赛开始日期!"
                                            Display="None" ID="RequiredFieldValidator1" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBeginTime" ErrorMessage="请输入比赛开始时间!"
                                            Display="None" ID="RequiredFieldValidator2" />

                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                    TargetControlID="txtBeginTime" 
                    Mask="99:99"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Time"
                    AcceptAMPM="False"           
                    ErrorTooltipEnabled="True" />
                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server"
                    ControlExtender="MaskedEditExtender3"
                    ControlToValidate="txtBeginTime"
                    IsValidEmpty="True"
                    EmptyValueMessage=""
                    InvalidValueMessage="请按照正确的格式输入开始比赛时间(hh:mm)!"
                    Display="None"
                    TooltipMessage=""
                    EmptyValueBlurredText="*"
                    InvalidValueBlurredMessage="*"/>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width:1px"></td>
            <td style="width:100px" align=right>
                <span>比赛结束时间</span>
            </td>
            <td style="width:200px">
                <div class="calendarContainer">
                    <YMGS:AjaxCalendar ID=txtEndDate runat=server />
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
                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                    ControlExtender="MaskedEditExtender3"
                    ControlToValidate="txtEndTime"
                    IsValidEmpty="True"
                    EmptyValueMessage=""
                    InvalidValueMessage="请按照正确的格式输入结束时间(hh:mm)!"
                    Display="None"
                    TooltipMessage=""
                    EmptyValueBlurredText="*"
                    InvalidValueBlurredMessage="*"/>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width:1px"></td>
            <td style="width:100px" align=right>
                <font color=red>*</font>
                <span>自动封盘时间</span>
            </td>
            <td style="width:200px">
                <div class="calendarContainer">                
                    <YMGS:AjaxCalendar ID=txtFreezeDate runat=server />
                    <asp:TextBox ID=txtFreezeTime CssClass=TextBox runat=server Width=40px ></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFreezeDate" ErrorMessage="请输入自动封盘日期!"
                                            Display="None" ID="RequiredFieldValidator5" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFreezeTime" ErrorMessage="请输入自动封盘时间!"
                                            Display="None" ID="RequiredFieldValidator6" />
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                    TargetControlID="txtFreezeTime" 
                    Mask="99:99"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError"
                    MaskType="Time"
                    AcceptAMPM="False"           
                    ErrorTooltipEnabled="True" />
                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                    ControlExtender="MaskedEditExtender3"
                    ControlToValidate="txtFreezeTime"
                    IsValidEmpty="True"
                    EmptyValueMessage=""
                    InvalidValueMessage="请按照正确的格式输入自动封盘时间(hh:mm)!"
                    Display="None"
                    TooltipMessage=""
                    EmptyValueBlurredText="*"
                    InvalidValueBlurredMessage="*"/>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <asp:CustomValidator ID="vldDateTime" runat="server" Display=None ErrorMessage="比赛开始时间不能大于比赛结束时间，<br/>比赛封盘时间也不能大于比赛结束时间，</br>比赛封盘时间不能大于比赛开始时间!"
                    ClientValidationFunction="ValidateMatchDate"></asp:CustomValidator>
    <table class=NoBorderTable width=100%>
        <tr>
            <td style="width:20px">
            </td>
            <td style="color:Red">
                    <asp:ValidationSummary runat="server" ShowMessageBox="false" ShowSummary="true" ID="Validationsummary1" />
            </td>
        </tr>
    </table>
    <table class="NoBorderTable" width=100%>
        <tr>
            <td  align=center>
                <asp:Button ID=btnSaveMatchDate runat=server Text="保存" Width=70px  CausesValidation=true OnClick="btnSaveMatchDate_Click" CssClass=Button/>
                <asp:Button ID=btnCancel runat=server Text="取消" Width=70px CausesValidation=false  CssClass=Button OnClick="btnCancel_Click"/>
            </td>
        </tr>
    </table>  
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" BehaviorID="mdlPopup" 
        TargetControlID="btnFake"
        PopupControlID="pnlPopup"
        BackgroundCssClass="ModalPopupBackground"                         
        PopupDragHandleControlID="pnlPopupHeader">
    </ajaxToolkit:ModalPopupExtender>                   
</asp:Panel> 
<asp:Button ID=btnFakeMatchScore runat=server style="display:none" />  
<asp:Panel id=pnlRecordScore runat=server style="width:350px;height:400px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlRecordScoreHeader" runat="server" CssClass="ModalPoupHeader">
        <span>录入比分</span>
    </asp:Panel>
    <br />
    <table class=NoBorderTable width=100%>
        <tr>
            <td align="right"><span>主队上半场得分</span></td>
            <td><asp:TextBox ID=txtHomeHalfScore runat=server ></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right"><span>客队上半场得分</span></td>
            <td><asp:TextBox ID=txtGuestHalfScore runat=server CssClass=TextBox ></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right"><span>主队下半场得分</span></td>
            <td><asp:TextBox ID=txtHomeSecHalfScore runat=server CssClass=TextBox></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right"><span>客队下半场得分</span></td>
            <td><asp:TextBox ID=txtGuestSecHalfScore runat=server CssClass=TextBox></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right"><span>主队加时得分</span></td>
            <td><asp:TextBox ID=txtHomeOverTimeScore runat=server CssClass=TextBox></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right"><span>客队加时得分</span></td>
            <td><asp:TextBox ID=txtGuestOverTimeScore runat=server CssClass=TextBox></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right"><span>主队点球得分</span></td>
            <td><asp:TextBox ID=txtHomePointScore runat=server CssClass=TextBox></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right"><span>客队点球得分</span></td>
            <td><asp:TextBox ID=txtGuestPointScore runat=server CssClass=TextBox></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right"><span>主队全场得分</span></td>
            <td><asp:TextBox ID=txtHomeFullScore runat=server CssClass=TextBox></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right"><span>客队全场得分</span></td>
            <td><asp:TextBox ID=txtGuestFullScore runat=server CssClass=TextBox></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" align="center" style="color:Red"><span id="spScoreError" style="display:none;">比赛录入分数须为非负整数!</span></td>
        </tr>
    </table>
    <table class="NoBorderTable" width=100%>
        <tr>
            <td align=center>
                <asp:Button ID=btnSaveScore runat=server Text="保存" Width=70px  CausesValidation=false OnClick="SaveRecordScore_Click" OnClientClick="return ValidateMatchScore();"  CssClass=Button/>
                <asp:Button ID=btnCancleScore runat=server Text="取消" Width=70px CausesValidation=false  CssClass=Button OnClick="btnCancleScore_Click"/>
            </td>
        </tr>
    </table> 
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlScore" BehaviorID="mdlScore" 
        TargetControlID="btnFakeMatchScore"
        PopupControlID="pnlRecordScore"
        BackgroundCssClass="ModalPopupBackground"                         
        PopupDragHandleControlID="pnlRecordScoreHeader">
    </ajaxToolkit:ModalPopupExtender> 
</asp:Panel>
<asp:Button ID=btnHandicap runat=server style="display:none" />  
<asp:Panel id=pnlHandicap runat=server style="width:auto;height:auto;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlHandicapHeader" runat="server" CssClass="ModalPoupHeader">
        <span>盘口管理</span>
    </asp:Panel>
    <br />
    <table class="NoBorderTable" width=100%>
        <tr>
            <td style="width:90px" align=right valign=middle>
            <div><span>全选</span><span><input type="checkbox" id="ckbHalfCorrectAll" runat="server" onclick="CheckAllBetType(this,'ckbHalfCorrectScore');" /></span></div>
            <div>波胆</div>
            </td>
            <td style="width:300px">
               <div class="MarketListDiv" style="width:300px;height:180px;">
                    <asp:CheckBoxList ID=ckbHalfCorrectScore runat=server RepeatColumns=2 
                        RepeatDirection=Horizontal CssClass=CheckBoxList Width=100%>
                        <asp:ListItem Value=1 Text="0:0"></asp:ListItem>
                        <asp:ListItem Value=2 Text="0:1"></asp:ListItem>
                        <asp:ListItem Value=3 Text="0:2"></asp:ListItem>
                        <asp:ListItem Value=4 Text="0:3"></asp:ListItem>
                        <asp:ListItem Value=5 Text="0:4"></asp:ListItem>
                        <asp:ListItem Value=6 Text="1:0"></asp:ListItem>
                    </asp:CheckBoxList>
               </div>
            </td>
            <td style="width:95px" align=right>
                <div><span>全选</span><span><input type="checkbox" id="ckcHalfOverUnderAll" runat="server"  onclick="CheckAllBetType(this,'ckbHalfOverUnder');" /></span></div>
                <div>大小球</div>
            </td>
            <td>
               <div class="MarketListDiv" style="width:300px;height:180px;">
                    <asp:CheckBoxList ID=ckbHalfOverUnder runat=server RepeatColumns=2 
                        RepeatDirection=Horizontal CssClass=CheckBoxList Width=100%>
                        <asp:ListItem Value=1 Text="Over/Under0.5"></asp:ListItem>
                        <asp:ListItem Value=2 Text="Over/Under1"></asp:ListItem>
                        <asp:ListItem Value=3 Text="Over/Under2.5"></asp:ListItem>
                        <asp:ListItem Value=4 Text="Over/Under3.5"></asp:ListItem>
                        <asp:ListItem Value=5 Text="Over/Under4.5"></asp:ListItem>
                        <asp:ListItem Value=6 Text="Over/Under5.5"></asp:ListItem>
                    </asp:CheckBoxList>
               </div>
            </td>
            <td style="width:90px" align=right valign=middle>
                <div><span>全选</span><span><input type="checkbox" id="ckbHalfAsianHandicapAll" runat="server" onclick="CheckAllBetType(this,'ckbHalfAsianHandicap');" /></span></div>
                <div>让球盘</div>
            </td>
            <td style="width:300px">
               <div class="MarketListDiv" style="width:300px;height:180px;">
                    <asp:CheckBoxList ID=ckbHalfAsianHandicap runat=server RepeatColumns=2 
                        RepeatDirection=Horizontal CssClass=CheckBoxList Width=100%>
                        <asp:ListItem Value=1 Text="0"></asp:ListItem>
                        <asp:ListItem Value=2 Text="0.5"></asp:ListItem>
                        <asp:ListItem Value=3 Text="1"></asp:ListItem>
                        <asp:ListItem Value=4 Text="1&1.5"></asp:ListItem>
                        <asp:ListItem Value=5 Text="0 & 0.5"></asp:ListItem>
                        <asp:ListItem Value=6 Text="3&4 "></asp:ListItem>
                    </asp:CheckBoxList>
               </div>
            </td>
        </tr>
        <tr>
            <td style="width:90px" align=right valign=middle>
            <div><span>全选</span><span><input type="checkbox" id="ckcCorrectAll" runat="server" onclick="CheckAllBetType(this,'ckbCorrectScore');" /></span></div>
            <div>波胆</div>
            </td>
            <td style="width:300px">
               <div class="MarketListDiv" style="width:300px;height:180px;">
                    <asp:CheckBoxList ID=ckbCorrectScore runat=server RepeatColumns=2 
                        RepeatDirection=Horizontal CssClass=CheckBoxList Width=100%>
                        <asp:ListItem Value=1 Text="0:0"></asp:ListItem>
                        <asp:ListItem Value=2 Text="0:1"></asp:ListItem>
                        <asp:ListItem Value=3 Text="0:2"></asp:ListItem>
                        <asp:ListItem Value=4 Text="0:3"></asp:ListItem>
                        <asp:ListItem Value=5 Text="0:4"></asp:ListItem>
                        <asp:ListItem Value=6 Text="1:0"></asp:ListItem>
                    </asp:CheckBoxList>
               </div>
            </td>
            <td style="width:95px" align=right>
                <div><span>全选</span><span><input type="checkbox" id="ckcOverUnderAll" runat="server"  onclick="CheckAllBetType(this,'ckbOverUnder');" /></span></div>
                <div>大小球</div>
            </td>
            <td>
               <div class="MarketListDiv" style="width:300px;height:180px;">
                    <asp:CheckBoxList ID=ckbOverUnder runat=server RepeatColumns=2 
                        RepeatDirection=Horizontal CssClass=CheckBoxList Width=100%>
                        <asp:ListItem Value=1 Text="Over/Under0.5"></asp:ListItem>
                        <asp:ListItem Value=2 Text="Over/Under1"></asp:ListItem>
                        <asp:ListItem Value=3 Text="Over/Under2.5"></asp:ListItem>
                        <asp:ListItem Value=4 Text="Over/Under3.5"></asp:ListItem>
                        <asp:ListItem Value=5 Text="Over/Under4.5"></asp:ListItem>
                        <asp:ListItem Value=6 Text="Over/Under5.5"></asp:ListItem>
                    </asp:CheckBoxList>
               </div>
            </td>
            <td style="width:90px" align=right valign=middle>
                <div><span>全选</span><span><input type="checkbox" id="ckcAsianHandicapAll" runat="server" onclick="CheckAllBetType(this,'ckbAsianHandicap');" /></span></div>
                <div>让球盘</div>
            </td>
            <td style="width:300px">
               <div class="MarketListDiv" style="width:300px;height:180px;">
                    <asp:CheckBoxList ID=ckbAsianHandicap runat=server RepeatColumns=2 
                        RepeatDirection=Horizontal CssClass=CheckBoxList Width=100%>
                        <asp:ListItem Value=1 Text="0"></asp:ListItem>
                        <asp:ListItem Value=2 Text="0.5"></asp:ListItem>
                        <asp:ListItem Value=3 Text="1"></asp:ListItem>
                        <asp:ListItem Value=4 Text="1&1.5"></asp:ListItem>
                        <asp:ListItem Value=5 Text="0 & 0.5"></asp:ListItem>
                        <asp:ListItem Value=6 Text="3&4 "></asp:ListItem>
                    </asp:CheckBoxList>
               </div>
            </td>
        </tr>
    </table>
    <table class="NoBorderTable" width=100%>
        <tr>
            <td align=center>
                <asp:Button ID=Button2 runat=server Text="确认" Width=70px CausesValidation=false OnClick="Save_Click" CssClass=Button/>
                <asp:Button ID="btnCancelMarket" Text="取消" runat=server Width=70px CausesValidation=false CssClass="Button" />
            </td>
        </tr>
    </table> 
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlHandicap" BehaviorID="mdlHandicap" 
        TargetControlID="btnHandicap"
        PopupControlID="pnlHandicap"
        BackgroundCssClass="ModalPopupBackground"
        CancelControlID = "btnCancelMarket"                         
        PopupDragHandleControlID="pnlHandicapHeader">
    </ajaxToolkit:ModalPopupExtender> 
</asp:Panel>
</ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="tmGameControl" EventName="Tick" />
</Triggers>
</asp:UpdatePanel>
<script type="text/javascript">
    $(function () {
        load();
        EndRequestHandler();
    })

    function EndRequestHandler() {
        CalcMatchScoreListener();
    }

    function CalcMatchScoreListener() {
        var homeScoreTxtArr = ['txtHomeHalfScore', 'txtHomeSecHalfScore', 'txtHomeOverTimeScore', 'txtHomePointScore'];
        var guestScoreTxtArr = ['txtGuestHalfScore', 'txtGuestSecHalfScore', 'txtGuestOverTimeScore', 'txtGuestPointScore'];
        $(homeScoreTxtArr).each(function () {
            $('[id$=' + this + ']').focusout(function () {
                if (isPositiveInteger($(this).val()) || $.trim($(this).val()) == "") {
                    $('#spScoreError').hide();
                    CalcMatchScore('HOME', $('[id$=txtHomeFullScore]'));
                }
                else {
                    $('#spScoreError').show();
                }
            })
        });

        $(guestScoreTxtArr).each(function () {
            $('[id$=' + this + ']').focusout(function () {
                if (isPositiveInteger($(this).val()) || $.trim($(this).val()) == "") {
                    $('#spScoreError').hide();
                    CalcMatchScore('GUEST', $('[id$=txtGuestFullScore]'));
                }
                else {
                    $('#spScoreError').show();
                }
            })
        });
    }

    function CalcMatchScore(ScoreType, FullScoreObj) {
        var ScoreTxtArr = ['txtHomeHalfScore', 'txtHomeSecHalfScore', 'txtHomeOverTimeScore', 'txtHomePointScore'];
        if (ScoreType == 'GUEST') {
            ScoreTxtArr = ['txtGuestHalfScore', 'txtGuestSecHalfScore', 'txtGuestOverTimeScore', 'txtGuestPointScore'];
        }
        
        var isAllEmpty = true;
        for (var j = 0, txt; txt = ScoreTxtArr[j++]; ) {
            if (!chkIsNull(txt)) {
                isAllEmpty = false; 
                break;
            }
        }
        if (isAllEmpty) {
            $(FullScoreObj).val(""); 
            return;
        }
        var FullScore = 0,TempScore;
        for (var i = 0, txt; txt = ScoreTxtArr[i++]; ) {
            if (!chkIsNull(txt)) 
                TempScore = parseInt($('[id$=' + txt + ']').val());
            else 
                TempScore = 0;
            FullScore += TempScore
        }
        $(FullScoreObj).val(FullScore)
    }

    function ValidateMatchDate(source, args) {
        var beginDate = $find("<%= txtBeginDate.ClientID %>").get_value()
        var beginTime = $("#" + "<%= txtBeginTime.ClientID %>");
        var endDate = $find("<%= txtEndDate.ClientID %>").get_value()
        var endTime = $("#" + "<%= txtEndTime.ClientID %>");
        var freezeDate = $find("<%= txtFreezeDate.ClientID %>").get_value()
        var freezeTime = $("#" + "<%= txtFreezeTime.ClientID %>");

        if (!isValidDate(beginDate) || !isValidDate(freezeDate)) {
            args.IsValid = false;
            return;
        }

        var time1 = beginDate + beginTime.val();
        var time2 = endDate + endTime.val();
        var time3 = freezeDate + freezeTime.val();

        if (time3 >= time1)
            args.IsValid = false;
        else
            args.IsValid = true;
    }

    function ValidateMatchScore() {
        var scoreTxtArr = ['txtHomeHalfScore', 'txtGuestHalfScore', 'txtHomeSecHalfScore', 'txtGuestSecHalfScore', 'txtHomeOverTimeScore', 'txtGuestOverTimeScore', 'txtHomePointScore', 'txtGuestPointScore'];
        for (var i = 0, txt; txt = scoreTxtArr[i++]; ) {
            if (!chkIsNull(txt) && !isPositiveInteger($('[id$=' + txt + ']').val())) {
                $('#spScoreError').show();
                return false;
            }
        }
        $('#spScoreError').hide();
        return true;
    }

    function CheckAllBetType(obj, betTbl) {
        var ckcList = $('[id$=' + betTbl + ']').find('input');
        $(ckcList).each(function () {
            $(this).attr('checked', obj.checked);
        })
    }
</script>
</asp:Content>
