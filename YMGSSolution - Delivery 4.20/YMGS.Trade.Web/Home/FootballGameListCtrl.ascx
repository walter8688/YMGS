<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FootballGameListCtrl.ascx.cs" Inherits="YMGS.Trade.Web.Home.FootballGameListCtrl" %>

<asp:HiddenField ID="hdfFbCal" runat="server" Value="1" />
<div id="tabFbCalendar">
    <ul>
        <asp:Repeater ID="rptCalendar" runat="server">
        <ItemTemplate>
            <li calid='<%#Eval("CalendarDateId")%>'><a href='<%#Eval("CalendarHref")%>'><span><%#Eval("WeekCalendarName")%></span></a></li>
        </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div id="ft-1" style="display:none;"></div>
    <div id="ft-2" style="display:none;"></div>
    <div id="ft-3" style="display:none;"></div>
    <div id="ft-4" style="display:none;"></div>
    <div id="ft-5" style="display:none;"></div>
    <div id="ft-6" style="display:none;"></div>
    <div id="ft-7" style="display:none;"></div>
</div>
<asp:UpdatePanel runat="server" ID="uplFbCal" UpdateMode="Conditional" ChildrenAsTriggers="false" >
<ContentTemplate>
<div id="divGameList" runat="server" class="fbCalander">
    <asp:Repeater runat="server" ID="ELRpt" OnItemDataBound="ELRpt_ItemDataBound">
    <ItemTemplate>
    <div id='<%#Eval("EventId")%>' class="ftAccordion">
        <h3><asp:Label ID="EName" runat="server"></asp:Label></h3>
	    <div class="mod-coupon">
            <table border="0" cellspacing="0" cellpadding="0">
                <thead>
                    <tr class="home-away-headings">
                        <th class="header" colSpan="4" scope="col">
                        </th>
                        <asp:Repeater runat="server" ID="rptMLFlag">
                            <ItemTemplate>
                                <th align="center" style="width:10%;" colSpan="2" scope="col">
                                    <span><%#Eval("MarketFlag")%></span>
                                </th>
                            </ItemTemplate>
                        </asp:Repeater>
                        <th class="other-markets" scope="col">
                            <span class="hide-offscreen"></span>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="MLRpt" OnItemDataBound="MLRpt_ItemDataBound" >
                        <ItemTemplate>
                            <tr class="betContainer">
                                <td class="name" colspan="4" scope="col">
                                    <div class="matchContainer">
                                        <asp:Label ID="MID" runat="server" style="display:none;" Text='<%#Eval("MatchID")%>'></asp:Label>
                                        <a class="description" title="View match" href='<%#Eval("MatchLink")%>'>
                                            <span class="home-team"><asp:Label ID="HTName" runat="server"></asp:Label></span>
                                            <span class="inplaynow-score"><%#Eval("CurrentScore")%></span>
                                            <span class="away-team"><asp:Label ID="GTName" runat="server"></asp:Label></span>
                                        </a>
                                        <span class="dtstart"><asp:Label ID="lblCusParam" runat="server"></asp:Label></span>
                                    </div>
                                </td>
                                <asp:Repeater runat="server" ID="MKRpt" OnItemDataBound="MKRpt_ItemDataBound">
                                    <ItemTemplate>
                                        <td class="cta"><asp:Button  ID="btnBack" class="cta cta-back" runat="server" OnClick="btnBack_OnClick" Text='<%#Eval("LayMatchAmouts").ToString()=="0.00"?@"&#13;&#10;&#13;&#10;":Eval("LayOdds")+@"&#13;&#10;$"+Eval("LayMatchAmouts")%>' CommandArgument='<%#Eval("MarketId")%>' CausesValidation="False"/></td>
                                        <td class="cta"><asp:Button  ID="btnLay" class="cta cta-lay" runat="server" OnClick="btnLay_OnClick" Text='<%#Eval("BackMatchAmouts").ToString()=="0.00"?@"&#13;&#10;&#13;&#10;":Eval("BackOdds")+@"&#13;&#10;$"+Eval("BackMatchAmouts")%>' CommandArgument='<%#Eval("MarketId")%>' CausesValidation="False" /></td>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <td>
                                    <div class='<%#Eval("MatchStatusClass")%>' style="width: 263px; margin-left: -265px; height:2.3em;">
                                        <span><asp:Label ID="lblBetStatus" runat="server"></asp:Label></span>
                                    </div>
                                </td>
                                <td class="other-markets">
                                    <a runat="server" title="<%$ Resources:GlobalLanguage,Viewfullmarket %>" href='<%#Eval("MatchLink")%>'></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
    </ItemTemplate>
    </asp:Repeater>
</div>
<div id="divNotice" class="iphp-notice" runat="server" visible="false">
    <asp:Label runat="server" ID="lblNotice"></asp:Label>
</div>
<asp:Button ID="btnReBind" OnClick="btnReBind_Click" runat="server" style="display:none;" />
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="FbCalanderTimer" EventName="Tick" />
</Triggers>
</asp:UpdatePanel>
<asp:Timer ID="FbCalanderTimer" runat="server" Interval="60000" OnTick="FbCalanderTimer_OnClick" Enabled="false"></asp:Timer>
