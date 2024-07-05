<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InPlayGameListCtrl.ascx.cs" Inherits="YMGS.Trade.Web.Home.InPlayGameListCtrl" %>
<div id="accordion" >
    <h3><asp:Label ID="lblTitle" runat="server"></asp:Label>
    <asp:Button runat="server" title="Refresh" class="bf-icon-refresh" id="btnRefresh" OnClick="btnRefresh_Click" OnClientClick="$('#accordion').accordion('disable');" />
    </h3>
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
                <asp:Repeater runat="server" ID="ELRpt" OnItemDataBound="ELRpt_ItemDataBound">
                    <ItemTemplate>
                        <tr class="inplaynow-competition-header">
                            <td colspan="4" scope="col">
                                <a class="ipn-competition-name first" title="View event" href='#'>
                                    <asp:Label ID="EName" runat="server"></asp:Label>
                                </a>
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <asp:Repeater runat="server" ID="MLRpt" OnItemDataBound="MLRpt_ItemDataBound" >
                            <ItemTemplate>
                                <tr class="betContainer">
                                    <td class="name" colspan="4" scope="col">
                                        <div class="starContainer">
                                            <a class='<%#Eval("MatchFavedCalss")%>' accesskey='<%#Eval("MatchID")%>' faved='<%#Eval("IsMatchFaved")%>' href="javascript:void(0);"></a>
                                        </div>
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
                                        <a runat="server" title='<%$ Resources:GlobalLanguage,Viewfullmarket %>' href='<%#Eval("MatchLink")%>'></a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>
                <tr align="right"><td colspan="10"><asp:LinkButton ID="btnViewAll" runat="server" Text="<%$ Resources:GlobalLanguage,ViewAll %>" OnClick="btnViewAll_Click"></asp:LinkButton></td></tr>
            </tbody>
        </table>
    </div>
</div>
