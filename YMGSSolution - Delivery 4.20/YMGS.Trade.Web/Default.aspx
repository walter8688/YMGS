<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/MasterPage/HomeMaster.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="YMGS.Trade.Web.Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="Home/HomeRecommandRace.ascx" TagName="HomeRecommandRace" TagPrefix="YMGS" %>
<%@ Register Src="Home/ADPic.ascx" TagName="ADPic" TagPrefix="YMGS" %>
<%@ Register Src="Home/HomeADWords.ascx" TagName="HomeADWords" TagPrefix="YMGS" %>
<%@ Register Src="Home/TopRaceCtrl.ascx" TagName="TopRaceCtrl" TagPrefix="YMGS" %>
<%@ Register Src="Home/OddsCompare.ascx" TagName="OddsCompare" TagPrefix="YMGS" %>
<%@ Register Src="~/Football/Controls/LeftNavigator.ascx" TagName="LeftNavigator" TagPrefix="YMGS"%>
<%@ Register Src="~/Football/Controls/EntNavigator.ascx" TagName="EntNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Football/Controls/FootballMatchList.ascx" TagName="FootballMatchList" TagPrefix="YMGS"%>
<%@ Register Src="~/Football/Controls/FootballInplayMatchList.ascx" TagName="FootballInplayMatchList" TagPrefix="YMGS"%>
<%@ Register Src="~/Football/Controls/CenterMatchMarketTop3.ascx" TagName="MatchMarketTop3" TagPrefix="YMGS"%>
<%@ Register Src="~/Football/Controls/BetPanel.ascx" TagName="BetPanel" TagPrefix="YMGS" %>
<%@ Register Src="~/Football/Controls/CenterMatchMarketTop1.ascx" TagName="MatchMarketTop1" TagPrefix="YMGS"%>
<%@ Register Src="~/Football/Controls/ChampMatchMarketTop3.ascx" TagName="ChampMatchMarketTop3" TagPrefix="YMGS"%>
<%@ Register Src="~/Football/Controls/MatchRealInfo.ascx" TagName ="MatchReal" TagPrefix="YMGS" %>
<%@ Register Src="~/Football/Controls/DefaultInplayList.ascx" TagName="DefaultInplay" TagPrefix="YMGS" %>
<%@ Register Src="~/Football/Controls/AsianMatchMarketTop1.ascx" TagName="AsianTop1" TagPrefix="YMGS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mph" runat="server">
<asp:ScriptManagerProxy ID="smProxy" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/Football/Controls/LeftNavigator.js" />
        <asp:ScriptReference Path="~/Football/Controls/EntNavigator.js" />
        <asp:ScriptReference Path="~/Football/Controls/FootballMatchList.js" />
        <asp:ScriptReference Path="~/Football/Controls/FootballInplayMatchList.js" />
        <asp:ScriptReference Path="~/Football/Controls/CenterMatchMarketTop1.js" />
        <asp:ScriptReference Path="~/Football/Controls/CenterMatchMarketTop3.js" />
        <asp:ScriptReference Path="~/Football/Controls/BetPanel.js" />
        <asp:ScriptReference Path="~/Football/Controls/MatchRealInfo.js" />
        <asp:ScriptReference Path="~/Football/Controls/DefaultInplayList.js" />
        <asp:ScriptReference Path="~/Football/Controls/AsianMatchMarketTop1.js" />
    </Scripts>
    <Services>
        <asp:ServiceReference Path="~/Services/LeftNavigatorService.asmx" />
        <asp:ServiceReference Path="~/Services/FootballMatchListService.asmx" />
        <asp:ServiceReference Path="~/Services/InPlayMatchListService.asmx" />
        <asp:ServiceReference Path="~/Services/MatchMarketService.asmx" />
        <asp:ServiceReference Path="~/Services/BetService.asmx" />
        <asp:ServiceReference Path="~/Services/DefaultInplayService.asmx" />
    </Services>
</asp:ScriptManagerProxy>
<div class="default-container">
    <!--右边投注推荐广告 Begin -->
    <div id="related">
        <div><YMGS:BetPanel ID="bet" runat="server" /></div>
        <div><YMGS:OddsCompare ID="oc" runat="server" /></div>
        <div><YMGS:HomeADWords ID="adwords" runat="server" /></div>
        <div><YMGS:HomeRecommandRace ID="RR" runat="server" /></div>
        <%--<div><YMGS:ADPic ID="AD" runat="server" /></div>--%>
    </div>
    <!--右边投注推荐广告 End -->
    <!--左边导航 Begin -->
    <div id="nav">
        <div class="mod-nav"><YMGS:LeftNavigator ID="leftNav" runat="server" /></div>
        <div class="mod-nav"><YMGS:EntNavigator ID="entNav" runat="server" /></div>
    </div>
    <!--左边导航 End -->
    <!--中间市场 Begin -->
    <div id="main-wrapper">

        <input type="hidden" id="languagemark" value="" runat="server" />
        <input type="hidden" id="hdfPageId" value="0" runat="server" />
        <div><YMGS:TopRaceCtrl ID="TR" runat="server" /></div>
        <div><YMGS:DefaultInplay ID="DefaultIn" runat="server" /></div>
        <div><YMGS:MatchReal ID="MatchRealInfo" runat="server" /></div>
        <div><YMGS:FootballInplayMatchList ID="inPlayCtrl" runat="server" /></div>
        <div><YMGS:FootballMatchList ID="fbCtrl" runat="server" /></div>
        <!--中间市场 top1 让球盘(全场所有) -->
<%--        <div><YMGS:MatchMarketTop1 ID="MMtop1HandicapFullAll" runat="server" /></div>--%>
        <div><YMGS:AsianTop1 ID="AsianTop1FullAll" runat="server" /></div>
        <!--中间市场 top1 让球盘(半场所有) -->
<%--        <div><YMGS:MatchMarketTop1 ID="MMtop1HandicapHalfAll" runat="server" /></div>--%>
        <div><YMGS:AsianTop1 ID="AsianTop1HalfAll" runat="server" /></div>
        <!--中间市场 top1 大小球（半场0.5、1.5）-->
        <div><YMGS:MatchMarketTop1 ID="MMtop1SoccerHalfSpecial" runat="server" /></div>
        <!--中间市场 top1 大小球（全场1.5、2.5）-->
        <div><YMGS:MatchMarketTop1 ID="MMtop1SoccerFullSpecial" runat="server" /></div>
        <!--中间市场 top1 波胆（全场所有）-->
        <div><YMGS:MatchMarketTop1 ID="MMtop1CorrectFull" runat="server" /></div>
        <!--中间市场 top1 波胆（半场所有）-->
        <div><YMGS:MatchMarketTop1 ID="MMtop1CorrectHalf" runat="server" /></div>
        <!--中间市场 top1 标准盘（全场）-->
        <div><YMGS:MatchMarketTop1 ID="MMtop1StandardFull" runat="server" /></div>
        <!--中间市场 top1 标准盘（半场）-->
        <div><YMGS:MatchMarketTop1 ID="MMtop1StandardHalf" runat="server" /></div>
        <!--中间市场 top1 标准盘（半/全场）-->
        <div><YMGS:MatchMarketTop1 ID="MMtop1StandardHalfAndFull" runat="server" /></div>
        <!--中间市场 top1 大小球（半场剩余的）-->
        <div><YMGS:MatchMarketTop1 ID="MMtop1SoccerHalfOthers" runat="server" /></div>
        <!--中间市场 top1 大小球（全场剩余的）-->
        <div><YMGS:MatchMarketTop1 ID="MMtop1SoccerFullOthers" runat="server" /></div>
        <!--中间市场 top3 -->
        <div><YMGS:MatchMarketTop3 ID="MMtop3" runat="server" /></div>
        <!--冠军比赛 top3 -->
        <div><YMGS:ChampMatchMarketTop3 ID="ChampTop3" runat="server" /></div>
    </div>
    <!--中间市场 Begin -->
    <input type="hidden" id="hdBetSenderId" />
</div>
<script type="text/javascript">
    //下注成功后的委托
    function BetEventHandler(sender, args) {
        var betSenderIds = $('[id$=hdBetSenderId]').val();
        $('[id$=hdBetSenderId]').val(betSenderIds + args.betSender + ',');
        var bet = $find('<%=bet.ClientID %>');
        if (bet != null)
            bet.buildBetPanel();
    }
    //投注成功后的委托
    function BetSuccessEventHandler(sender, args) {
        //更新用户资金
        var userLoginCtl = $find('homeLogin');
        if (userLoginCtl != null) {
            userLoginCtl.updateUserFund();
        }
        //更新市场
        var betSenderIds = $('[id$=hdBetSenderId]').val().split(',');
        if (typeof betSenderIds === 'undefined')
            return false;
        if (betSenderIds == null)
            return false;
        if (betSenderIds.length < 1)
            return false;
        for (var i = 0, arr; arr = betSenderIds[i++]; ) {
            var betSender = $find(arr);
            if (betSender != null) {
                betSender.refresh();
            }
        }
        $('[id$=hdBetSenderId]').val('');
    }
</script>
</asp:Content>