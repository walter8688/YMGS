<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeFooterCtrl.ascx.cs"
    Inherits="YMGS.Trade.Web.MasterPage.HomeFooterCtrl" %>
<div class="foot-background"></div>
<div class="mod-footer-general">
    <ul id="footer_Links">
    <li class="start"><a href="javascript:void(0);"><asp:Label ID="Label1" runat="server" Text="<%$ Resources:GlobalLanguage,TermConditions %>"></asp:Label></a></li>
    <li><a href="javascript:void(0);"><asp:Label ID="Label2" runat="server" Text="<%$ Resources:GlobalLanguage,CookiePolicy %>"></asp:Label></a></li>    
    <li><a href="javascript:void(0);"><asp:Label ID="Label3" runat="server" Text="<%$ Resources:GlobalLanguage,DisputeResolution %>"></asp:Label></a></li>
    <li><a href="javascript:void(0);"><asp:Label ID="Label4" runat="server" Text="<%$ Resources:GlobalLanguage,PrivacyPolicy %>"></asp:Label></a></li>
    <li><a href="javascript:void(0);"><asp:Label ID="Label8" runat="server" Text="<%$ Resources:GlobalLanguage,PaymentMethods %>"></asp:Label></a></li>
    <li><a href="javascript:void(0);"><asp:Label ID="Label9" runat="server" Text="<%$ Resources:GlobalLanguage,SecurityFAQS %>"></asp:Label></a></li>
    <li><a href="javascript:void(0);"><asp:Label ID="Label10" runat="server" Text="<%$ Resources:GlobalLanguage,MarketingAffiliates %>"></asp:Label></a></li>
    <li><a href="javascript:void(0);"><asp:Label ID="Label11" runat="server" Text="<%$ Resources:GlobalLanguage,Careers %>"></asp:Label></a></li>
    <li><a href="javascript:void(0);"><asp:Label ID="Label12" runat="server" Text="<%$ Resources:GlobalLanguage,SiteFeedback %>"></asp:Label></a></li>
    <li><a href="javascript:void(0);">﻿<asp:Label ID="Label13" runat="server" Text="<%$ Resources:GlobalLanguage,CustomerCommitment %>"></asp:Label></a></li>
    <li class="last"><a href="/help/必发必网站教程.pptx"><asp:Label ID="Label14" runat="server" Text="<%$ Resources:GlobalLanguage,BestabetCourse %>"></asp:Label></a></li>
    <li class="last"><a href="/help/必发必下注教程.pptx"><asp:Label ID="Label15" runat="server" Text="<%$ Resources:GlobalLanguage,BestabetFlow %>"></asp:Label></a></li>
    </ul>
</div>
<p class=SeperatorOne></p>
<div class="PartnerPic">
    <asp:Label ID="Label5" Width="200px" runat="server" Text="<%$ Resources:GlobalLanguage,OfficialPartner %>"></asp:Label>
    <asp:Label ID="Label6" Width="200px" runat="server" Text="<%$ Resources:GlobalLanguage,MobileBettingApps %>"></asp:Label>
    <asp:Label ID="Label7" Width="200px" runat="server" Text="<%$ Resources:GlobalLanguage,ApprovedBettingPartners%>"></asp:Label>
</div>
<div><img src="Images/botpic1.jpg" /></div>
<p class=SeperatorTwo></p>
<div><img src="Images/botpic2.jpg" /></div>
<div><asp:Label ID="lblFooter" runat="server" Text="<%$ Resources:GlobalLanguage,footnote %>"></asp:Label></div>

<script type="text/javascript">
    $(function () {
        if ($.trim($('.last a span').html()) === "") {
            $('.last').each(function () {
                $(this).removeClass('last').addClass('start');
            })
        }
    })
</script>
<script type="text/javascript">    var cnzz_protocol = (("https:" == document.location.protocol) ? " https://" : " http://"); document.write(unescape("%3Cdiv id='cnzz_stat_icon_1000032347'%3E%3C/div%3E%3Cscript src='" + cnzz_protocol + "w.cnzz.com/q_stat.php%3Fid%3D1000032347%26l%3D2' type='text/javascript'%3E%3C/script%3E"));</script>