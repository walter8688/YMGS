<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="MatchManageFrm.aspx.cs" Inherits="YMGS.Manage.Web.GameMarket.MatchManageFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
<script type="text/javascript">
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
</script>
<asp:UpdatePanel ID=updPanel runat=server UpdateMode=Conditional>
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
                                <asp:DropDownList ID=ddlEvent runat=server CssClass="DropdownList" Width=162px></asp:DropDownList>
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
                                &nbsp;<asp:Button runat=server ID=btnNew Width=70px Text = "新增" CssClass=Button  CausesValidation=false  OnClick="btnNew_Click" />
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
            <asp:GridView ID="gridData" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%" DataKeyNames="MATCH_ID"
                OnRowCommand="gridData_RowCommand"   OnRowDataBound="gridData_RowDataBound"                                                   
                EmptyDataText="请输入合适的查询条件查询比赛!"
                >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="MATCH_NAME" HeaderText="比赛名称"/>
                    <asp:BoundField DataField="MATCH_NAME_EN" HeaderText="比赛英文名称"/>
                    <asp:BoundField DataField="EVENTTYPE_NAME" HeaderText="赛事类别" />
                    <asp:BoundField DataField="EVENTITEM_NAME" HeaderText="赛事项目" />
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
                    <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign=Center ItemStyle-Width=150px
                                     ItemStyle-Wrap=false ItemStyle-VerticalAlign=Middle>
                        <ItemTemplate>
                            <asp:LinkButton ID=btnEdit CssClass=LinkButton runat=server Text="详细" CausesValidation=false></asp:LinkButton>
                            <asp:LinkButton ID=btnActivate  CssClass=LinkButton runat=server Text="激活" CausesValidation=false ></asp:LinkButton>
                            <asp:LinkButton ID=btnSuspend  CssClass=LinkButton runat=server Text="暂停" CausesValidation=false ></asp:LinkButton>
                            <asp:LinkButton ID=btnEditTime  CssClass=LinkButton runat=server Text="修改时间" CausesValidation=false ></asp:LinkButton>
                            <asp:LinkButton ID=btnDelete  CssClass=LinkButton runat=server Text="删除" CausesValidation=false ></asp:LinkButton>
                            <asp:LinkButton ID=btnStop  CssClass=LinkButton runat=server Text="终止" CausesValidation=false ></asp:LinkButton>
                            <asp:LinkButton ID=btnSaveAs CssClass=LinkButton  runat=server Text="另存" CausesValidation=false ></asp:LinkButton>
                            <asp:LinkButton ID=btnRecommend CssClass=LinkButton  runat=server Text="推荐比赛" CausesValidation=false ></asp:LinkButton>
                            <asp:LinkButton ID=btnNotRecommend CssClass=LinkButton  runat=server Text="取消推荐" CausesValidation=false ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID=pageNavigator runat=server />
        </td>
    </tr>
</table>

<asp:Button ID=btnFake runat=server style="display:none" />
<asp:Panel id=pnlPopup runat=server style="width:350px;height:270px;display:none" CssClass="ModalPoup">
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
                <asp:Button ID=btnSave runat=server Text="保存" Width=70px  CausesValidation=true OnClick="btSave_Click" CssClass=Button/>
                <asp:Button ID=btnCancel runat=server Text="取消" Width=70px CausesValidation=false  CssClass=Button/>
            </td>
        </tr>
    </table>  
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" BehaviorID="mdlPopup" 
        TargetControlID="btnFake"
        PopupControlID="pnlPopup"
        BackgroundCssClass="ModalPopupBackground"                         
        CancelControlID="btnCancel" 
        PopupDragHandleControlID="pnlPopupHeader"
    ></ajaxToolkit:ModalPopupExtender>                   
</asp:Panel>     

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
