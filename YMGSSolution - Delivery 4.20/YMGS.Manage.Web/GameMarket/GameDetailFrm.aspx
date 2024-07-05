<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="GameDetailFrm.aspx.cs" Inherits="YMGS.Manage.Web.GameMarket.GameDetailFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
<script type="text/javascript">
    function ValidateEvent(source, args) {
        var eventObj = $("#" + "<%= ddlEvent.ClientID %>");
        if (eventObj.val() == "-1")
            args.IsValid = false;
        else
            args.IsValid = true;
    }

    function ValidateTeam(source, args) {
        var eventObj1 = $("#" + "<%= ddlHomeTeam.ClientID %>");
        if (eventObj1.val() == "-1") {
            args.IsValid = false;
            return;
        }
        else
            args.IsValid = true;

        var eventObj2 = $("#" + "<%= ddlVisitingTeam.ClientID %>");
        if (eventObj2.val() == "-1") {
            args.IsValid = false;
            return;
        }

        if (eventObj1.val() == eventObj2.val()) {
            args.IsValid = false;
            return;
        }
        args.IsValid = true;
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

    function GenerateMatchName() {
        var homeTeamId = $('[id$=ddlHomeTeam]').val();
        var guestTeamId = $('[id$=ddlVisitingTeam]').val();
        if (homeTeamId == -1 || guestTeamId == -1)
            return false;
        var homeTeam = $('[id$=ddlHomeTeam]').find('option:selected').text();
        var guestTeam = $('[id$=ddlVisitingTeam]').find('option:selected').text();

        var url = "GetTeamDetail.ashx?hometeam=" + homeTeamId + "&guestteam=" + guestTeamId;
        $.ajax({
            type: 'post',
            url: url,
            success: function (data) {
                var teamNameEn = data.split('@');
                $('[id$=txtMatchName]').val(homeTeam + ' V ' + guestTeam);
                $('[id$=txtMatchNameEn]').val(teamNameEn[0] + ' V ' + teamNameEn[1]);
            }
        })
    }

    function CheckAllBetType(obj, betTbl) {
        var ckcList = $('[id$=' + betTbl + ']').find('input');
        $(ckcList).each(function () {
            $(this).attr('checked', obj.checked);
        })
    }
</script>
<br />
<asp:UpdatePanel ID=updDetail runat=server UpdateMode=Conditional>
<ContentTemplate>
<table class=NoBorderTable width=100%>
    <tr>
        <td style="width:90px" align=right>
            <font color=red>*</font>
            <span>比赛名称</span>
        </td>
        <td style="width:300px">
            <asp:TextBox ID=txtMatchName runat=server CssClass=TextBox MaxLength=100 Width=300px></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMatchName" ErrorMessage="请输入比赛名称!"
                                        Display="None" ID="vldMatchName" />
        </td>
        <td style="width:95px" align=right>
            <font color=red>*</font>
            <span>比赛英文名称</span>
        </td>
        <td>
            <asp:TextBox ID=txtMatchNameEn runat=server CssClass=TextBox MaxLength=100 Width=300px></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMatchNameEn" ErrorMessage="请输入比赛英文名称!"
                                        Display="None" ID="RequiredFieldValidator7" />
        </td>
    </tr>
    <tr>
        <td style="width:90px" align=right>
            <span>赛事项目</span>
        </td>
        <td style="width:300px">
            <asp:DropDownList ID=ddlEventItem runat=server CssClass="DropdownList" 
                Width=300px AutoPostBack="True" 
                onselectedindexchanged="ddlEventItem_SelectedIndexChanged"></asp:DropDownList>
        </td>
        <td style="width:95px" align=right>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="width:90px" align=right>
            <span>赛事区域</span>
        </td>
        <td style="width:300px">
            <asp:DropDownList ID=ddlEventZone runat=server CssClass="DropdownList" 
                Width=300px AutoPostBack="True" 
                onselectedindexchanged="ddlEventZone_SelectedIndexChanged"></asp:DropDownList>
        </td>
        <td style="width:95px" align=right>
            <font color=red>*</font>
            <span>赛事</span>
        </td>
        <td>
            <asp:DropDownList ID=ddlEvent runat=server CssClass="DropdownList" Width=300px 
                AutoPostBack="True" onselectedindexchanged="ddlEvent_SelectedIndexChanged"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width:90px" align=right>
            <font color=red>*</font>
            <span>主队</span>
        </td>
        <td style="width:300px">
            <asp:DropDownList ID="ddlHomeTeam" runat=server CssClass="DropdownList" onchange="GenerateMatchName();"
                Width=300px ></asp:DropDownList>
        </td>
        <td style="width:95px" align=right>
            <font color=red>*</font>
            <span>客队</span>
        </td>
        <td>
            <asp:DropDownList ID="ddlVisitingTeam" runat=server CssClass="DropdownList" onchange="GenerateMatchName();"
             Width=300px ></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width:90px" align=right>
            <font color=red>*</font>
            <span>开始比赛时间</span>
        </td>
        <td style="width:300px">
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
        <td style="width:95px" align=right>
            <span>结束比赛时间</span>
        </td>
        <td>
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
    </tr>
    <tr>
        <td style="width:90px" align=right>
            <font color=red>*</font>
            <span>自动封盘时间</span>
        </td>
        <td style="width:300px">
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
        <td style="width:95px" align=right>
            <font color=red>*</font>
            <span>是否推荐比赛</span>
        </td>
        <td>
            <asp:RadioButton ID=rbRecommend runat=server Checked=true Text="推荐" GroupName="recommend" />
            <asp:RadioButton ID=rbNotRecommend runat=server Text="不推荐" GroupName="recommend" />
        </td>
    </tr>
    <tr>
        <td style="width:90px" align=right>
            <span>是否走地</span>
        </td>
        <td>
            <asp:RadioButton ID=rbNotZouDi runat=server Checked=true Text="不走地" GroupName="ZouDi" />
            <asp:RadioButton ID=rbZouDi runat=server Text="走地" GroupName="ZouDi" />
        </td>
    </tr>
    <tr runat="server" id="trSocreHalf">
        <td style="width:90px" align=right>
            <span>主队上半场得分</span>
        </td>
        <td style="width:300px">
            <asp:TextBox ID=txtHomeFirHalfScore runat=server Text="" Width=300px CssClass=TextBox ReadOnly=true></asp:TextBox>
        </td>
        <td style="width:95px" align=right>
            <span>客队上半场得分</span>
        </td>
        <td>
            <asp:TextBox ID=txtGuestFirHalfScore runat=server Text="" Width=300px CssClass=TextBox ReadOnly=true></asp:TextBox>
        </td>
    </tr>
    <tr runat="server" id="trSocreSecHalf">
        <td style="width:90px" align=right>
            <span>主队下半场得分</span>
        </td>
        <td style="width:300px">
            <asp:TextBox ID=txtHomeSecHalfScore runat=server  Text="" Width=300px CssClass=TextBox ReadOnly=true></asp:TextBox>
        </td>
        <td style="width:95px" align=right>
            <span>客队下半场得分</span>
        </td>
        <td>
            <asp:TextBox ID=txtGuestSecHalfScore runat=server Text="" Width=300px CssClass=TextBox ReadOnly=true></asp:TextBox>
        </td>
    </tr>
    <tr runat="server" id="trSocreOverTime">
        <td style="width:90px" align=right>
            <span>主队加时赛得分</span>
        </td>
        <td style="width:300px">
            <asp:TextBox ID=txtHomeOvertimeScore runat=server Text="" Width=300px CssClass=TextBox ReadOnly=true></asp:TextBox>
        </td>
        <td style="width:95px" align=right>
            <span>客队加时赛得分</span>
        </td>
        <td>
            <asp:TextBox ID=txtGuestOvertimeScore runat=server Text="" Width=300px CssClass=TextBox ReadOnly=true></asp:TextBox>
        </td>
    </tr>
    <tr runat="server" id="trSocrePoint">
        <td style="width:90px" align=right>
            <span>主队点球得分</span>
        </td>
        <td style="width:300px">
            <asp:TextBox ID=txtHomePointScore runat=server Width=300px Text="" CssClass=TextBox ReadOnly=true></asp:TextBox>
        </td>
        <td style="width:95px" align=right>
            <span>客队点球得分</span>
        </td>
        <td>
            <asp:TextBox ID=txtGuestPointScore runat=server Width=300px Text="" CssClass=TextBox ReadOnly=true></asp:TextBox>
        </td>
    </tr>
</table>
<table class=NoBorderTable width=100%>
    <tr>
        <td style="width:90px" align=right>
            <span>
                标准盘
            </span>
        </td>
        <td>
            <asp:CheckBoxList ID=ckbMatchOdds runat=server RepeatColumns=100 CssClass=CheckBoxList>
                <asp:ListItem Value=1 Text="半场标准盘"></asp:ListItem>
                <asp:ListItem Value=2 Text="全场标准盘"></asp:ListItem>
                <asp:ListItem Value=3 Text="半/全场标准盘"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
</table>
<table class=NoBorderTable width=100%>
    <tr>
        <td style="width:90px" align=right valign=middle>
            <div><span>全选</span><span><input type="checkbox" id="ckcCorrectAll" onclick="CheckAllBetType(this,'ckbCorrectScore');" /></span></div>
            <div>波胆(上半场)</div>
        </td>
        <td style="width:300px">
           <div class="MarketListDiv" style="width:300px;height:120px;">
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
            <div><span>全选</span><span><input type="checkbox" id="ckcCorrectSecHalfAll"  onclick="CheckAllBetType(this,'ckbCorrectScoreSecHalf');" /></span></div>
            <div>波胆(全场)</div>
        </td>
        <td>
           <div class="MarketListDiv" style="width:300px;height:120px;">
                <asp:CheckBoxList ID=ckbCorrectScoreSecHalf runat=server RepeatColumns=2 
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
    </tr>
    <tr>
        <td style="width:90px" align=right valign=middle>
            <div><span>全选</span><span><input type="checkbox" id="ckcOverUnderAll"  onclick="CheckAllBetType(this,'ckbOverUnder');" /></span></div>
            <div>大小球(上半场)</div>
        </td>
        <td style="width:300px">
           <div class="MarketListDiv" style="width:300px;height:120px;">
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
        <td style="width:95px" align=right>
            <div><span>全选</span><span><input type="checkbox" id="ckcOverUnderSecHalfAll"  onclick="CheckAllBetType(this,'ckbOverUnderSecHalf');" /></span></div>
            <div>大小球(全场)</div>
        </td>
        <td>
           <div class="MarketListDiv" style="width:300px;height:120px;">
                <asp:CheckBoxList ID=ckbOverUnderSecHalf runat=server RepeatColumns=2 
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
    </tr>
    <tr>
        <td style="width:90px" align=right valign=middle>
            <div><span>全选</span><span><input type="checkbox" id="ckcAsianHandicapAll" onclick="CheckAllBetType(this,'ckbAsianHandicap');" /></span></div>
            <div>让球盘(上半场)</div>
        </td>
        <td style="width:300px">
           <div class="MarketListDiv" style="width:300px;height:120px;">
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
        <td style="width:95px" align=right>
            <div><span>全选</span><span><input type="checkbox" id="ckcAsianHandicapSecHalfAll"  onclick="CheckAllBetType(this,'ckbAsianHandicapSecHalf');" /></span></div>
            <div>让球盘(全场)</div>
        </td>
        <td>
           <div class="MarketListDiv" style="width:300px;height:120px;">
                <asp:CheckBoxList ID=ckbAsianHandicapSecHalf runat=server RepeatColumns=2 
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
    </tr>
    <tr>
        <td style="width:90px" align="right" valign="middle">
            <div><span>默认盘口</span></div>
            <div>让球盘(上半场)</div>
        </td>
        <td style="width:300px">
           <div class="MarketListDiv" style="width:300px;">
           <asp:DropDownList ID="drpHalfDefault" runat="server" CssClass="DropdownList" Width="300px" ></asp:DropDownList>
           </div>
        </td>
        <td style="width:95px" align="right" valign="middle">
            <div><span>默认盘口</span></div>
            <div>让球盘(全场)</div>
        </td>
        <td>
           <div class="MarketListDiv" style="width:300px;">
           <asp:DropDownList ID="drpFullDefault" runat="server" CssClass="DropdownList" Width="300px" ></asp:DropDownList>
           </div>
        </td>
    </tr>
    <tr>
        <td style="width:90px" align=right valign=middle>
            <span>备注</span>
        </td>
        <td colspan="3">
            <asp:TextBox ID=txtMemo  MaxLength=100 runat=server TextMode=MultiLine CssClass=TextBox Width=300px Height=120px>
            </asp:TextBox>
        </td>
    </tr>
    <tr runat="server" id="trMatchStatus">
        <td style="width:90px" align=right valign=middle>
            <span>比赛状态</span>
        </td>
        <td style="width:300px">
            <asp:TextBox id=txtStatus ReadOnly=true runat=server Width=300px CssClass=TextBox Text="未激活"></asp:TextBox>
        </td>
        <td style="width:95px" align=right>
            <span></span>
        </td>
        <td>
        </td>
    </tr>
</table>
<asp:CustomValidator ID="vldEvent" runat="server" Display=None ErrorMessage="请选择赛事!"
                    ClientValidationFunction="ValidateEvent"></asp:CustomValidator>
<asp:CustomValidator ID="vldTeam" runat="server" Display=None ErrorMessage="请选择比赛的球队,并且主队和客队不能是同一个球队!"
                    ClientValidationFunction="ValidateTeam"></asp:CustomValidator>
<asp:CustomValidator ID="vldDateTime" runat="server" Display=None ErrorMessage="比赛开始时间不能大于比赛结束时间，比赛封盘时间也不能大于比赛结束时间，比赛封盘时间不能大于比赛开始时间!"
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
        <td style="width:90px;"></td>
        <td>
            <asp:Button ID=btnSave runat=server Text="保存" Width=70px  CausesValidation=true CssClass=Button OnClick="btnSave_Click"/>
            <asp:Button ID=btnCancel runat=server Text="返回" Width=70px CausesValidation=false CssClass=Button  OnClick="btnCancel_Click"/>
        </td>
    </tr>
</table>  
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
