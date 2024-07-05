<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/MasterPage/HomeMaster.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="YMGS.Trade.Web.Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="Home/HomeRecommandRace.ascx" TagName="HomeRecommandRace" TagPrefix="uc1" %>
<%@ Register Src="Home/ADPic.ascx" TagName="ADPic" TagPrefix="uc1" %>
<%@ Register Src="Home/HomeADWords.ascx" TagName="HomeADWords" TagPrefix="uc1" %>
<%@ Register Src="Home/TopRaceCtrl.ascx" TagName="TopRaceCtrl" TagPrefix="uc1" %>
<%@ Register Src="Home/OddsCompare.ascx" TagName="OddsCompare" TagPrefix="uc1" %>
<%@ Register Src="Home/InPlayGameListCtrl.ascx" TagName="InPlayHomeGameListCtrl" TagPrefix="uc1" %>
<%@ Register Src="Home/GameListCtrl.ascx" TagName="HomeGameListCtrl" TagPrefix="uc1" %>
<%@ Register Src="Home/FootballGameListCtrl.ascx" TagName="FootballGameListCtrl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mph" runat="server">
<link rel="stylesheet" href="Scripts/jqueryui/dev/themes/custom-theme/minified/jquery.ui.tabs.min.css">
<link rel="stylesheet" href="Scripts/jqueryui/dev/themes/custom-theme/minified/jquery.ui.accordion.min.css">
<link rel="stylesheet" href="Scripts/jqueryui/dev/themes/custom-theme/minified/jquery-ui.min.css">
<script src="Scripts/jqueryui/dev/ui/jquery.ui.accordion.js"></script>
<script src="Scripts/jqueryui/dev/ui/jquery.ui.tabs.js"></script>
<script src="Scripts/DefaultJS.js"></script>
<script type="text/javascript" language="javascript">
    function sumProfit() {
        var sumprofit = 0;
        var layprofit = $(".layprofit");
        var backAmount = $(".backclass").find(".backstake");
        backAmount.each(
            function () {
                var temp = parseFloat($(this).val());
                if (temp == "NaN") {
                    temp = 0;
                }
                sumprofit = sumprofit + temp;
            }
        );
        layprofit.each(
            function () {
                var temp = parseFloat($(this).text());
                if (temp == "NaN") {
                    temp = 0;
                }
                sumprofit = sumprofit + temp;
            }
            );
        $(".totalprofit").text($('[id$=hfprofit]').val()+ sumprofit.toFixed(2));
        $("#<%=hfdtotal.ClientID %>").val(sumprofit.toFixed(2));
    }
    $(function () {
        $("#tabInPlay").css("width", $("#tabInPlay").width());
        $('#tabInPlay').tabs();
    })     
</script>

    <table class="defaultTable" style="width: 100%">
        <tr>
            <td valign="top">
                <asp:Label ID="lbllan" runat="server" CssClass="languagemark" Text="0"></asp:Label>
                <asp:HiddenField ID="hfprofit" Value="<%$ Resources:GlobalLanguage,Liability %>" runat="server" />
                <asp:HiddenField ID="hfdlan" Value="0" runat="server" />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hfdeventitemid" Value="0" runat="server" />
                        <asp:HiddenField ID="hfdeventzoneid" Value="0" runat="server" />
                        <asp:HiddenField ID="hfdeventid" Value="0" runat="server" />
                        <asp:HiddenField ID="hfditemdate" Value="0" runat="server" />
                        <asp:HiddenField ID="hfdmatchid" Value="0" runat="server" />
                        <asp:HiddenField ID="hfdmarkettempid" Value="0" runat="server" />
                        <asp:HiddenField ID="hfdIsChampian" Value="0" runat="server" />
                        <asp:HiddenField ID="hfdEntChampian" Value="0" runat="server" />
                        <asp:HiddenField ID="hfdChampionID" Value="0" runat="server" />
                        <asp:HiddenField ID="hfdChampeventid" Value="0" runat="server" />
                        <asp:HiddenField ID="hfdIsInPlay" Value="0" runat="server" />
                        <asp:HiddenField ID="hdfIsFtCalander" Value="0" runat="server" />
                        <table class="defaultTable">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="leftmenuWidth">
                                        <asp:Repeater ID="rptleftmenu" runat="server" OnItemDataBound="rptleftmenu_ItemDataBound">
                                            <ItemTemplate>
                                                <tr class="eventitem" runat="server" id="eventitemid">
                                                    <td>
                                                        <asp:Button ID="itemname" runat="server" BorderStyle="None" Width="100%" Height="100%"
                                                            OnClick="eventitem_OnClick" CommandArgument='<%#Eval("EVENTITEM_IDs")%>' Text='<%#Eval("EventItem_Names")%>'
                                                            CausesValidation="False" />
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="eventitem">
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <asp:Repeater ID="rpteventzone" runat="server" OnItemDataBound="rpteventzone_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr class="eventzone" runat="server" id="eventzoneid">
                                                                        <td>
                                                                            <asp:Button ID="itemname" runat="server" BorderStyle="None" Width="100%" Height="100%"
                                                                                OnClick="eventzone_OnClick" CommandArgument='<%#Eval("EVENTZONE_ID")%>' Text='<%#Eval("EVENTZONE_NAME")%>'
                                                                                CausesValidation="False" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="eventzone">
                                                                        <td>
                                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                                <asp:Repeater ID="rptevent" runat="server" OnItemDataBound="rptevent_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr class="event" runat="server" id="eventid">
                                                                                            <td>
                                                                                                <asp:Button ID="itemname" runat="server" BorderStyle="None" Width="100%" Height="100%"
                                                                                                    OnClick="event_OnClick" CommandArgument='<%#Eval("EVENT_ID")%>' Text='<%#Eval("EVENT_NAME")%>'
                                                                                                    CausesValidation="False" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr runat="server" id="event">
                                                                                            <td>
                                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                                    <asp:Repeater ID="rpteventdate" runat="server" OnItemDataBound="rpteventdate_ItemDataBound">
                                                                                                        <ItemTemplate>
                                                                                                            <tr class="itemdate" runat="server" id="itemdateid">
                                                                                                                <td>
                                                                                                                    <asp:HiddenField ID="hfdSTARTDATEID" runat="server" Value='<%#Eval("MARK")%>' />
                                                                                                                    <asp:Button ID="itemname" runat="server" BorderStyle="None" Width="100%" Height="100%"
                                                                                                                        OnClick="eventDate_OnClick" CommandArgument='<%#Eval("ID")%>' Text='<%#Eval("STARTDATE")%>'
                                                                                                                        CausesValidation="False" />
                                                                                                                    <asp:LinkButton ID="lbtitemname" runat="server" BorderStyle="None" Width="100%" Height="100%"
                                                                                                                        OnClick="eventDate_OnClick" CommandArgument='<%#Eval("ID")%>' Text='<%#Eval("STARTDATE")%>'
                                                                                                                        CausesValidation="False"></asp:LinkButton>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr runat="server" id="itemdate">
                                                                                                                <td>
                                                                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                                                                        <asp:Repeater ID="rptmatch" runat="server" OnItemDataBound="rptmatch_ItemDataBound">
                                                                                                                            <ItemTemplate>
                                                                                                                                <tr class="match" runat="server" id="matchid">
                                                                                                                                    <td>
                                                                                                                                        <asp:LinkButton ID="itemname" CssClass="leftmenueslinkbutton" Width="100%" Height="100%"
                                                                                                                                            OnClick="martch_OnClick" runat="server" Text='<%#Eval("MATCH_NAME")%>' CommandArgument='<%#Eval("MATCH_ID")%>'
                                                                                                                                            CausesValidation="False"></asp:LinkButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr runat="server" id="match">
                                                                                                                                    <td>
                                                                                                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                                                                                                            <asp:Repeater ID="rptMatchMarket" runat="server">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <tr class="match" runat="server" id="matchid">
                                                                                                                                                        <td>
                                                                                                                                                            <asp:LinkButton ID="itemname" Width="100%" Height="100%" OnClick="martchMarket_OnClick"
                                                                                                                                                                runat="server" CssClass="leftmenueslinkbutton" Text='<%#Eval("MARKET_TMP_NAME")%>'
                                                                                                                                                                CommandArgument='<%#Eval("BET_TYPE_ID").ToString()+","+Eval("MARKET_TMP_TYPE").ToString()%>'
                                                                                                                                                                CausesValidation="False"></asp:LinkButton>
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:Repeater>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:Repeater>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:Repeater>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Repeater ID="rptEnt" runat="server">
                                            <ItemTemplate>
                                                <tr class="match">
                                                    <td>
                                                        <asp:LinkButton ID="lbtChampMartch" Width="100%" Height="100%" OnClick="ChampMartch_OnClick"
                                                            runat="server" Text='<%#Eval("Champ_Event_Name")%>' CommandArgument='<%#Eval("Champ_Event_ID")%>'
                                                            CausesValidation="False"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="width: 3px">
            </td>
            <td valign="top">
                <table class="BlankTable midarea">
                    <tr style="display:none">
                        <td>
                            <marquee onmouseover="this.stop()" scrollamount="2" onmouseout="this.start()" class="topmarquee">
                                <table><tr>
                                <asp:Repeater ID="rptNotice" runat="server">
                                <ItemTemplate>
                                <td style="white-space: nowrap">
                                <%#Eval("TITLE").ToString() + ":" + Eval("CONTENT").ToString()%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                </ItemTemplate>
                                </asp:Repeater>
                                </tr></table></marquee>
                        </td>
                    </tr>
                    <tr>
                        <td class="realscore">
                            <asp:Panel ID="pnlScore" runat="server">
                            <asp:UpdatePanel ID=updScore runat=server UpdateMode=Conditional ChildrenAsTriggers=false>
                                <ContentTemplate>
                                <table class=NoBorderTable>
                                    <td>                
                                        <asp:Label id=lblTeamA runat=server></asp:Label>&nbsp;
                                    </td>
                                    <td align=center>
                                        <asp:Label ID=lblScore runat=server></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;<asp:Label id=lblTeamB runat=server></asp:Label>
                                    </td>
                                </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            </asp:Panel>                            
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Panel ID="pnlMidTop" Visible="false" runat="server" Width="100%" Style="border: 1px solid #CCCCCC;">
                                <uc1:TopRaceCtrl ID="TopRace" runat="server" />
                                <asp:HiddenField ID="hfdroprace" runat="server" />
                                <asp:UpdatePanel ID="pnlTopRace" runat="server" UpdateMode=Conditional ChildrenAsTriggers=false>
                                    <ContentTemplate>
                                        <table width="100%">
                                            <asp:Repeater ID="rptTopRecommandMain" runat="server" OnItemDataBound="rptmain_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr><td width="100%">
                                                            <table width="100%" cellpadding="0" cellspacing="0" class="centerbody">
                                                                <tr class="btr">
                                                                    <td class="btr_td1"><span><%#Eval("MATCH_NAME")%></span>&nbsp;-&nbsp;<asp:Label ID="lblname" runat="server" Text='<%#Eval("MARKET_TMP_NAME")%>'></asp:Label>&nbsp;&nbsp;&nbsp;
                                                                        <span><%#DateTime.Parse(Eval("STARTDATE").ToString()).ToString("yyyy-MM-dd HH:mm")%></span>&nbsp;&nbsp;<span style="color:Red; font-weight:bold"><%# Eval("MATCH_STATUS") %></span></td>
                                                                    <td  class="btr_td1"><asp:Button ID="btnRefTopRac" CssClass="reflink" runat=server Text="<%$ Resources:GlobalLanguage,Refresh %>" OnClick="btnRefTopRace_Click"></asp:Button></td>
                                                                    <td class="btr_td2" align=right><asp:ImageButton ID="ibtchartrpt" CausesValidation="false" runat="server" ImageUrl="Images/h.jpg"  CommandArgument='<%#Eval("MATCH_ID").ToString()+","+Eval("MARKET_TMP_NAME").ToString()+","+Eval("MATCH_NAME").ToString()%>' OnClick="btnrpt_OnClick" /></td>
                                                                </tr><asp:Repeater ID="rsm" runat="server" OnItemDataBound="rptsubmain_ItemDataBound"><HeaderTemplate><tr height="20">
                                                                            <td class="backlayTdCss"></td>
                                                                            <td class="backlaytitlecss"><asp:Label ID="l1" runat=server Text="<%$ Resources:GlobalLanguage,Back %>"></asp:Label></td>
                                                                            <td class="backlaytitlecss"><asp:Label ID="Label1" runat=server Text="<%$ Resources:GlobalLanguage,Salebet %>"></asp:Label></td>
                                                                        </tr></HeaderTemplate><ItemTemplate><tr>
                                                                            <td class="bTdCss"><span><%#Eval("MARKET_NAME")%></span></td>
                                                                            <td class="backTdCss"><asp:Button  ID="btnBack" runat="server" OnClick="btnBack_OnClick" Text='<%#Eval("layMATCH_AMOUNTS").ToString()=="0.00"?@"&#13;&#10;&#13;&#10;":Eval("layodds")+@"&#13;&#10;$"+Eval("layMATCH_AMOUNTS")%>' CommandArgument='<%#Eval("MARKET_ID")%>' CausesValidation="False"  /><input id="txtMatchId" type="hidden" runat="server" value='<%#Eval("MATCH_ID")%>' /></td>
                                                                            <td  class="layTdCss"><asp:Button  ID="btnLay" runat="server" OnClick="btnLay_OnClick" Text='<%#Eval("backMATCH_AMOUNTS").ToString()=="0.00"?@"&#13;&#10;&#13;&#10;":Eval("backodds")+@"&#13;&#10;$"+Eval("backMATCH_AMOUNTS")%>' CommandArgument='<%#Eval("MARKET_ID")%>' CausesValidation="False"  /></td>
                                                                        </tr></ItemTemplate></asp:Repeater>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                        <asp:Button ID="btnFake" runat="server" Style="display: none" />
                                        <asp:Panel ID="pnlPopup" runat="server" Style="display: none" CssClass="ModalPoup">
                                            <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
                                                <span>
                                                    <asp:Label ID="lblmatchname" runat="server" Text="Label"></asp:Label></span>
                                            </asp:Panel>
                                            <table class="NoBorderTable" width="100%" >
                                                <tr>
                                                    <td colspan="3" style="height: 10px;">
                                                        <asp:HiddenField ID="hfdrptmatchid" runat="server" />
                                                        <asp:DropDownList ID="ddlmarket" runat="server" OnTextChanged="ddlmarket_OnTextChanged"
                                                            AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 10px;">
                                                    &nbsp;    <asp:Label ID="Label16" runat="server" Text="<%$ Resources:GlobalLanguage,curracesum %>"></asp:Label>
                                                        <asp:Label ID="lblcurracesum" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <table class="aroundline" cellpadding=0 cellspacing=0>
                                                            <asp:Repeater ID="rptmatchchart" runat="server">
                                                                <HeaderTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:GlobalLanguage,odds%>"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label11" runat="server" Text="<%$ Resources:GlobalLanguage,buy%>"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label12" runat="server" Text="<%$ Resources:GlobalLanguage,Sale%>"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label13" runat="server" Text="<%$ Resources:GlobalLanguage,dealed%>"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <%#Eval("ODDS")%>
                                                                        </td>
                                                                        <td>
                                                                            <%#Eval("backAmount")%>
                                                                        </td>
                                                                        <td>
                                                                            <%#Eval("layAmount")%>
                                                                        </td>
                                                                        <td>
                                                                            <%#Eval("dealAmount")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table class="NoBorderTable" width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:LinkButton ID="lkbclose" runat="server" Text="<%$ Resources:GlobalLanguage,close%>"
                                                            OnClick="btnCancel_OnClick" CausesValidation="False"></asp:LinkButton>
                                                        &nbsp;
                                                        <asp:Button ID="btnCancel" runat="server" Text="取消" Width="70px" CausesValidation="false"
                                                            CssClass="unvisiable" OnClick="btnCancel_OnClick" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" BehaviorID="mdlPopup"
                                                TargetControlID="btnFake" PopupControlID="pnlPopup" BackgroundCssClass="ModalPopupBackground"
                                                CancelControlID="btnCancel" PopupDragHandleControlID="pnlPopupHeader">
                                            </ajaxToolkit:ModalPopupExtender>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ToprecommandTimer" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:Timer ID="ToprecommandTimer" runat="server" Interval="60000" OnTick="Toprecommand_OnClick">
                                </asp:Timer>
                            </asp:Panel>
                            <asp:Panel ID="pnlInPlay" Width="100%" runat="server">
                                <div id="tabInPlay">
                                    <ul>
                                        <li><a href="#tabs-1" onclick="javascript:$('[id$=btnInPlay]').click();"><asp:Label ID="Label2" runat="server" Text="<%$ Resources:GlobalLanguage,InPlay %>"></asp:Label></a></li>
                                        <li><a href="#tabs-2" onclick="javascript:$('[id$=btnGameToday]').click();"><asp:Label ID="Label7" runat="server" Text="<%$ Resources:GlobalLanguage,Today %>"></asp:Label></a></li>
                                        <li><a href="#tabs-3" onclick="javascript:$('[id$=btnGameTomorrow]').click();"><asp:Label ID="Label8" runat="server" Text="<%$ Resources:GlobalLanguage,Tomorrow %>"></asp:Label></a></li>
                                        <li><a href="#tabs-4" onclick="javascript:$('[id$=btnYourInPlay]').click();"><asp:Label ID="Label9" runat="server" Text="<%$ Resources:GlobalLanguage,YourInPlay %>"></asp:Label></a></li>
                                    </ul>
                                    <div id="tabs-1">
                                    <asp:UpdatePanel ID="uplInPlay" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <uc1:InPlayHomeGameListCtrl ID="homeGameListCtrl" runat="server" />
                                        <asp:Button runat="server" ID="btnInPlay" OnClick="LoadInPlay_Click" style="display:none;" />
                                    </ContentTemplate>
                                    <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="InPlayTimer" EventName="Tick" />
                                    </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:Timer ID="InPlayTimer" runat="server" Interval="60000" OnTick="InPlayTimer_OnClick" Enabled="false"></asp:Timer>
                                    </div>
                                    <div id="tabs-2">
                                    <asp:UpdatePanel ID="uplGameToday" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <uc1:HomeGameListCtrl ID="GameListToday" runat="server" />
                                        <asp:Button runat="server" ID="btnGameToday" OnClick="LoadGameToday_Click" style="display:none;" />
                                    </ContentTemplate>
                                    <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="GameTodayTimer" EventName="Tick" />
                                    </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:Timer ID="GameTodayTimer" runat="server" Interval="60000" OnTick="GameTodayTimer_OnClick" Enabled="false"></asp:Timer>
                                    </div>
                                    <div id="tabs-3">
                                    <asp:UpdatePanel ID="uplGameTomorrow" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <uc1:HomeGameListCtrl ID="GameListTomorrow" runat="server" />
                                        <asp:Button runat="server" ID="btnGameTomorrow" OnClick="LoadGameTomorrow_Click" style="display:none;" />
                                    </ContentTemplate>
                                    <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="GameTomorrowTimer" EventName="Tick" />
                                    </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:Timer ID="GameTomorrowTimer" runat="server" Interval="60000" OnTick="GameTomorrowTimer_OnClick" Enabled="false"></asp:Timer>
                                    </div>
                                    <div id="tabs-4">
                                    <asp:UpdatePanel ID="uplYourGames" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <uc1:HomeGameListCtrl ID="GameListYourInPlay" runat="server" />
                                        <asp:Button runat="server" ID="btnYourInPlay" OnClick="LoadYourInPlay_Click" style="display:none;" />
                                    </ContentTemplate>
                                    <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="GameYourInPlayTimer" EventName="Tick" />
                                    </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:Timer ID="GameYourInPlayTimer" runat="server" Interval="60000" OnTick="GameYourInPlayTimer_OnClick" Enabled="false"></asp:Timer>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlFootball" Width="100%" runat="server">
                                <uc1:FootballGameListCtrl ID="footballGameListCtrl" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="pnlMidM" runat="server" UpdateMode="Conditional" ChildrenAsTriggers=false>
                                <ContentTemplate>                                    
                                    <table width="100%"><asp:Repeater ID="rm" runat="server" OnItemDataBound="rptmain_ItemDataBound"><ItemTemplate><tr>
                                                    <td width="100%">
                                                        <table width="100%" cellpadding="0" cellspacing="0" class="centerbody">
                                                            <tr class="btr">
                                                                <td  align="left"><span><%#Eval("MATCH_NAME")%></span>&nbsp;-&nbsp;<asp:Label ID="lblname" runat="server" Text='<%#Eval("MARKET_TMP_NAME")%>'></asp:Label>&nbsp;&nbsp;&nbsp;
                                                                    <span><%#DateTime.Parse(Eval("STARTDATE").ToString()).ToString("yyyy-MM-dd HH:mm")%></span>&nbsp;&nbsp;<span style="color:Red; font-weight:bold"><%# Eval("MATCH_STATUS") %></span></td>
                                                                <td class="btr_td1" align="left"><asp:Button ID=btnRefRac CssClass="reflink" runat=server Text="<%$ Resources:GlobalLanguage,Refresh %>" OnClick="btnRefRac_Click"></asp:Button></td>
                                                                <td class="btr_td2" align="right"><asp:ImageButton ID="ibtchartrpt" CausesValidation="false" ImageUrl="~/Images/h.jpg" runat="server" CommandArgument='<%#Eval("MATCH_ID").ToString()+","+Eval("MARKET_TMP_NAME").ToString()+","+Eval("MATCH_NAME").ToString()%>' OnClick="btnrptM_OnClick" /></td>
                                                            </tr><asp:Repeater ID="rsm" runat="server" OnItemDataBound="rptsubmain_ItemDataBound"><HeaderTemplate><tr height="20">
                                                                        <td  class="backlayTdCss">&nbsp;</td>
                                                                        <td class="backlaytitlecss"><asp:Label ID=l1 runat=server Text="<%$ Resources:GlobalLanguage,Back %>"></asp:Label></td>
                                                                        <td class="backlaytitlecss"><asp:Label ID=l2 runat=server Text="<%$ Resources:GlobalLanguage,Salebet %>"></asp:Label></td>
                                                                </tr></HeaderTemplate><ItemTemplate><tr><td class="bTdCss"><span><%#Eval("MARKET_NAME")%></span></td>
                                                                        <td class="backTdCss"><asp:Button ID="btnBack" runat="server" OnClick="btnBack_OnClick" Text='<%#Eval("layMATCH_AMOUNTS").ToString()=="0.00"?@"&#13;&#10;&#13;&#10;":Eval("layodds")+@"&#13;&#10;$"+Eval("layMATCH_AMOUNTS")%>' CommandArgument='<%#Eval("MARKET_ID")%>' CausesValidation="False"  /><input id="txtMatchId" type="hidden" runat="server" value='<%#Eval("MATCH_ID")%>' /></td>
                                                                        <td class="layTdCss"><asp:Button  ID="btnLay" runat="server" OnClick="btnLay_OnClick" Text='<%#Eval("backMATCH_AMOUNTS").ToString()=="0.00"?@"&#13;&#10;&#13;&#10;":Eval("backodds")+@"&#13;&#10;$"+Eval("backMATCH_AMOUNTS")%>' CommandArgument='<%#Eval("MARKET_ID")%>' CausesValidation="False"  /></td>
                                                                    </tr></ItemTemplate></asp:Repeater>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <asp:Button ID="btnFakeM" runat="server" Style="display: none" />
                                    <asp:Panel ID="pnlPopupM" runat="server" Style="display: none" CssClass="ModalPoup">
                                        <asp:Panel ID="pnlPopupHeaderM" runat="server" CssClass="ModalPoupHeader">
                                            <span>
                                                <asp:Label ID="lblmatchnameM" runat="server" Text="Label"></asp:Label></span>
                                        </asp:Panel>
                                        <table class="NoBorderTable" width="100%">
                                            <tr>
                                                <td colspan="3" style="height: 10px;">
                                                    <asp:HiddenField ID="hfdrptmatchidM" runat="server" />
                                                    <asp:DropDownList ID="ddlmarketM" runat="server" OnTextChanged="ddlmarketM_OnTextChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 10px;">
                                                    <asp:Label ID="Label17" runat="server" Text="<%$ Resources:GlobalLanguage,curracesum %>"></asp:Label>
                                                    <asp:Label ID="lblrptsum" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <table class="aroundline">
                                                        <asp:Repeater ID="rptmatchchartM" runat="server">
                                                            <HeaderTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label10" runat="server" Text="<%$ Resources:GlobalLanguage,odds%>"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label11" runat="server" Text="<%$ Resources:GlobalLanguage,buy%>"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label12" runat="server" Text="<%$ Resources:GlobalLanguage,Sale%>"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label13" runat="server" Text="<%$ Resources:GlobalLanguage,dealed%>"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <%#Eval("ODDS")%>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("backAmount")%>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("layAmount")%>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("dealAmount")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table class="NoBorderTable" width="100%">
                                            <tr>
                                                <td align="center">
                                                    <asp:LinkButton ID="lkbcloseM" runat="server" Text="<%$ Resources:GlobalLanguage,close%>"
                                                        OnClick="btnCancelM_OnClick" CausesValidation="False"></asp:LinkButton>
                                                    &nbsp;
                                                    <asp:Button ID="btnCancelM" runat="server" Text="取消" Width="70px" CausesValidation="false"
                                                        CssClass="unvisiable" OnClick="btnCancel_OnClick" />
                                                </td>
                                            </tr>
                                        </table>
                                        <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopupM" BehaviorID="mdlPopupM"
                                            TargetControlID="btnFakeM" PopupControlID="pnlPopupM" BackgroundCssClass="ModalPopupBackground"
                                            CancelControlID="btnCancelM" PopupDragHandleControlID="pnlPopupHeaderM">
                                        </ajaxToolkit:ModalPopupExtender>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="MarketTimer" EventName="Tick" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:Timer ID="MarketTimer" runat="server" Interval="60000" OnTick="itemname_OnClick">
                            </asp:Timer>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 3px">
            </td>
            <td valign="top" class="rightarea">
                <asp:UpdatePanel ID="pnlbook" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlsubbook" runat="server">
                            <table width="100%" class="BlankTable">
                                <tr>
                                    <td style="border-style: solid; border-width: 1px; color: #FF0000">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" BorderStyle="None"
                                            DisplayMode="List" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="backtab" width="100%">
                                            <tr>
                                                <td colspan="4" class="backclass">
                                                    <asp:Panel ID="pnlback" runat="server" Width="100%">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <asp:Repeater ID="rptback" runat="server" OnItemDataBound="rptback_ItemDataBound">
                                                                <HeaderTemplate>
                                                                    <tr class="backHeader">
                                                                        <th style="width: 45%">
                                                                            <asp:Label ID="l1" runat="server" Text="<%$ Resources:GlobalLanguage,buy %>"></asp:Label>
                                                                        </th>
                                                                        <td align="center" style="width: 30%">
                                                                            <asp:Label ID="l2" runat="server" Text="<%$ Resources:GlobalLanguage,odds %>"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 20%">
                                                                            <asp:Label ID="l3" runat="server" Text="<%$ Resources:GlobalLanguage,amount %>"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 5%; text-align: right;">
                                                                            <asp:Label ID="l4" runat="server" Text="<%$ Resources:GlobalLanguage,profit %>"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <th colspan="4" align="left">
                                                                            <asp:HiddenField ID="hfdMATCH_ID" runat="server" Value='<%#Eval("MATCH_ID")%>' />
                                                                            <asp:HiddenField ID="hfdMATCHTYPE" runat="server" Value='<%#Eval("MATCHTYPE")%>' />
                                                                            <asp:Label ID="lblmatchName" runat="server" class="MATCH_NAME" Text='<%#Eval("MATCH_NAME")%>'></asp:Label>
                                                                        </th>
                                                                    </tr>
                                                                    <tr style="background-color: #72BBEF">
                                                                        <td colspan="4">
                                                                            <table width="100%" class="backclass" style="width: 100%; padding: 0px; margin: 0px;"
                                                                                cellpadding="0" cellspacing="0">
                                                                                <asp:Repeater ID="rptbackitem" runat="server" OnItemDataBound="rptbackitem_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: White;
                                                                                                padding-top: 3px; padding-bottom: 3px;">
                                                                                                <span class="MATCHTYPE" style="display: none">
                                                                                                    <%#Eval("MATCHTYPE")%></span> <span class="MARKET_ID" style="display: none">
                                                                                                        <%#Eval("MARKET_ID")%></span>
                                                                                                <asp:HiddenField ID="hfdMATCH_ID" runat="server" Value='<%#Eval("MATCH_ID")%>' />
                                                                                                <asp:HiddenField ID="hfdMARKET_TMP_ID" runat="server" Value='<%#Eval("MARKET_TMP_ID")%>' />
                                                                                                <asp:HiddenField ID="hfdMARKET_ID" runat="server" Value='<%#Eval("MARKET_ID")%>' />
                                                                                                <asp:HiddenField ID="hfdMATCHTYPE" runat="server" Value='<%#Eval("MATCHTYPE")%>' />
                                                                                                <asp:HiddenField ID="hfdMARKET_TMP_NAME" runat="server" Value='<%#Eval("MARKET_TMP_NAME")%>' />
                                                                                                <asp:HiddenField ID="hfdMARKET_NAME" runat="server" Value='<%#Eval("MARKET_NAME")%>' />
                                                                                                <asp:HiddenField ID="hfdMATCH_NAME" runat="server" Value='<%#Eval("MATCH_NAME")%>' />
                                                                                                <asp:ImageButton ID="imbdel" runat="server" OnClick="imbdelback_OnClick" CommandArgument='<%#Eval("MARKET_ID").ToString()+","+Eval("MATCHTYPE").ToString()%>'
                                                                                                    ImageUrl="~/Images/close.png" CausesValidation="False" />
                                                                                            </td>
                                                                                            <td style="width: 160px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: White;
                                                                                                padding-top: 3px; padding-bottom: 3px;">
                                                                                                <%#Eval("MARKET_NAME")%>
                                                                                                <br />
                                                                                                <span style="color: #808080">
                                                                                                    <%#Eval("MARKET_TMP_NAME")%></span>
                                                                                            </td>
                                                                                            <td style="width: 70px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: White;
                                                                                                padding-top: 3px; padding-bottom: 3px; vertical-align: middle;" class="oddstd">
                                                                                                <asp:TextBox ID="txtodds" CssClass="TextBox odds" onchange="Achangeprofit(this);"
                                                                                                    runat="server" Text='<%#Eval("odds")%>'></asp:TextBox>
                                                                                                <ajaxToolkit:NumericUpDownExtender ID="NumericUpDownExtender1" Minimum="1.01" Maximum="3000"
                                                                                                    TargetControlID="txtodds" runat="server" Width="55">
                                                                                                </ajaxToolkit:NumericUpDownExtender>
                                                                                            </td>
                                                                                            <td style="width: 60px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: White;
                                                                                                padding-top: 3px; padding-bottom: 3px;">
                                                                                                <asp:TextBox ID="txtstake" MaxLength="7" CssClass="TextBox backstake" onchange="Achangestakeprofit(this);"
                                                                                                    Width="60px" runat="server" Text='<%#Eval("AMOUNTS")%>'></asp:TextBox>
                                                                                                <asp:CustomValidator ID="cvdstake" runat="server" ClientValidationFunction="Checkbackstake"
                                                                                                    ErrorMessage='<%#Eval("MARKET_NAME").ToString()+"["+Eval("MARKET_TMP_NAME").ToString()+"]: "+NNN%>'
                                                                                                    ControlToValidate="txtstake" Display="None" SetFocusOnError="True"></asp:CustomValidator>
                                                                                            </td>
                                                                                            <td style="width: 50px; text-align: right; border-bottom-style: solid; border-bottom-width: 1px;
                                                                                                border-bottom-color: White; padding-top: 3px; padding-bottom: 3px;">
                                                                                                <asp:Label ID="lblprofit" CssClass="backprofit" runat="server" Text="1"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="laytab" width="100%">
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Panel ID="pnllay" runat="server">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <asp:Repeater ID="rptlay" runat="server" OnItemDataBound="rptlay_ItemDataBound">
                                                                <HeaderTemplate>
                                                                    <tr class="layHeader">
                                                                        <th style="width: 45%">
                                                                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:GlobalLanguage,Sale %>"></asp:Label>
                                                                        </th>
                                                                        <td align="center" style="width: 30%">
                                                                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:GlobalLanguage,buyodds %>"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 20%">
                                                                            <asp:Label ID="Label5" runat="server" Text="<%$ Resources:GlobalLanguage,buyamount %>"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 5%; text-align: right;">
                                                                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:GlobalLanguage,stage %>"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <th colspan="4" align="left">
                                                                            <asp:HiddenField ID="hfdMATCH_ID" runat="server" Value='<%#Eval("MATCH_ID")%>' />
                                                                            <asp:HiddenField ID="hfdMATCHTYPE" runat="server" Value='<%#Eval("MATCHTYPE")%>' />
                                                                            <asp:Label ID="lblmatchName" runat="server" class="MATCH_NAME" Text='<%#Eval("MATCH_NAME")%>'></asp:Label>
                                                                        </th>
                                                                    </tr>
                                                                    <tr style="background-color: #FAA9BA;">
                                                                        <td colspan="4">
                                                                            <table class="whiteline" cellpadding="0" cellspacing="0">
                                                                                <asp:Repeater ID="rptlayitem" runat="server" OnItemDataBound="rptlayitem_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <span class="MATCHTYPE" style="display: none">
                                                                                                    <%#Eval("MATCHTYPE")%></span>
                                                                                                <asp:HiddenField ID="hfdMARKET_TMP_ID" runat="server" Value='<%#Eval("MARKET_TMP_ID")%>' />
                                                                                                <asp:HiddenField ID="hfdMARKET_ID" runat="server" Value='<%#Eval("MARKET_ID")%>' />
                                                                                                <asp:HiddenField ID="hfdMATCHTYPE" runat="server" Value='<%#Eval("MATCHTYPE")%>' />
                                                                                                <asp:HiddenField ID="hfdMARKET_TMP_NAME" runat="server" Value='<%#Eval("MARKET_TMP_NAME")%>' />
                                                                                                <asp:HiddenField ID="hfdMARKET_NAME" runat="server" Value='<%#Eval("MARKET_NAME")%>' />
                                                                                                <asp:HiddenField ID="hfdMATCH_ID" runat="server" Value='<%#Eval("MATCH_ID")%>' />
                                                                                                <asp:HiddenField ID="hfdMATCH_NAME" runat="server" Value='<%#Eval("MATCH_NAME")%>' />
                                                                                                <span class="MARKET_ID" style="display: none">
                                                                                                    <%#Eval("MARKET_ID")%></span>
                                                                                                <asp:ImageButton ID="imbdel" runat="server" OnClick="imbdellay_OnClick" CommandArgument='<%#Eval("MARKET_ID").ToString()+","+Eval("MATCHTYPE").ToString()%>'
                                                                                                    ImageUrl="~/Images/close.png" CausesValidation="False" />
                                                                                            </td>
                                                                                            <td style="width: 160px">
                                                                                                <%#Eval("MARKET_NAME")%>
                                                                                                <br />
                                                                                                <span style="color: #808080">
                                                                                                    <%#Eval("MARKET_TMP_NAME")%></span>
                                                                                            </td>
                                                                                            <td style="width: 70px; vertical-align: middle;" class="oddstd">
                                                                                                <asp:TextBox ID="txtodds" onchange="changeprofit(this);" CssClass="TextBox odds"
                                                                                                    runat="server" Text='<%#Eval("odds")%>'></asp:TextBox>
                                                                                                <ajaxToolkit:NumericUpDownExtender ID="NumericUpDownExtender1" Minimum="1.01" Maximum="3000"
                                                                                                    TargetControlID="txtodds" runat="server" Width="55">
                                                                                                </ajaxToolkit:NumericUpDownExtender>
                                                                                            </td>
                                                                                            <td style="width: 60px">
                                                                                                <asp:TextBox ID="txtstake" onchange="changestakeprofit(this);" CssClass="TextBox stake"
                                                                                                    MaxLength="7" Width="60px" runat="server" Text='<%#Eval("AMOUNTS")%>'></asp:TextBox>
                                                                                                <asp:CustomValidator ID="cvdstake" runat="server" ClientValidationFunction="Checkstake"
                                                                                                    ErrorMessage='<%#Eval("MARKET_NAME").ToString()+"["+Eval("MARKET_TMP_NAME").ToString()+"]: "+NNN %>'
                                                                                                    ControlToValidate="txtstake" Display="None" SetFocusOnError="True"></asp:CustomValidator>
                                                                                            </td>
                                                                                            <td style="width: 50px; text-align: right;">
                                                                                                <asp:Label ID="lblprofit" CssClass="layprofit" runat="server" Text="1"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbltotal" CssClass="totalprofit" runat="server" Text="0"></asp:Label>
                                        <asp:HiddenField ID="hfdtotal" runat="server" Value="0" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table class=NoBorderTable width=100%>
                                <tr>
                                    <td align=left><asp:Button ID="btnCancelSelection"  runat="server" Text="<%$ Resources:GlobalLanguage,CancelAllSelection %>"  OnClick="btnCancelSelection_Click" OnClientClick="this.disabled=true" UseSubmitBehavior=false CausesValidation=false /></td>
                                    <td align=right><asp:Button ID="btnpbets" CssClass="betbutton" runat="server" Text="<%$ Resources:GlobalLanguage,PlaceBets %>"  OnClick="btnpbets_Click" OnClientClick="this.disabled=true" UseSubmitBehavior=false /></td>
                                </tr>
                            </table>
                            
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="pnlTJ" runat="server" Width="100%">
                            <table width="100%" class="BlankTable">
                                <tr>
                                    <td valign="top" style="border: 1px solid #CCCCCC;">
                                        <uc1:OddsCompare ID="oc" runat="server" />
                                    </td>
                                </tr>
                                <tr style="height: 7px">
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <uc1:HomeADWords ID="adwords" runat="server" />
                                    </td>
                                </tr>
                                <tr style="height: 7px">
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="border: 1px solid #CCCCCC;">
                                        <uc1:HomeRecommandRace ID="RR" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlAD" runat="server">
                            <table width="100%">
                                <tr>
                                    <td height="100" valign="top">
                                        <uc1:ADPic ID="Pic" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
