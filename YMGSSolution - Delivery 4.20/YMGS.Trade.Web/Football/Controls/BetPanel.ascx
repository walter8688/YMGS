<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BetPanel.ascx.cs" Inherits="YMGS.Trade.Web.Football.Controls.BetPanel" %>
<div id='<%= ClientID %>'>
<div id="betslip" style="height: auto; margin-bottom: 16px;">
<div id="mod-tab-container" class="mod-tab-container-betslip">    
    <%--<h2 class="right-module-header">
        <a href="javascript:void(0);">
            <em><asp:Label runat="server" Text="<%$ Resources:GlobalLanguage,Betslip%>" CssClass="relatedPanelTitle"></asp:Label></em>
            
        </a>
    </h2>--%>
    <div style="background-color: #273A45; height: 25px"><asp:Label ID="Label1" runat="server" Text="<%$ Resources:GlobalLanguage,Betslip%>" CssClass="relatedPanelTitle"></asp:Label></div>
    <div class="mod-betslip">
        <div class="potential-bets-container">
            <div class="bets">
                <div class="back" runat="server" id="backPanle">
                </div>
                <div class="lay" runat="server" id="layPanle">
                </div>
            </div>
            <div class="betfooter" runat="server" id="betFootPanel">
            </div>
        </div>
    </div>
</div>
</div>
</div>