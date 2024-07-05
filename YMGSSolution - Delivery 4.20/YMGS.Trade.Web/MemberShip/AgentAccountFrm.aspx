<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master" AutoEventWireup="true" CodeBehind="AgentAccountFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.AgentAccountFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
<table class="NoBorderTable" width="100%">
    <tr><td style="text-align:left; font-weight:bolder;"><asp:Label ID="lblGrowAgentTitle" runat="server" Text="<%$ Resources:GlobalLanguage,GrowAgentTitle %>"></asp:Label></td></tr>    
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td align="right">
        <asp:LinkButton id="btnHandleAgent" runat="server" Text="<%$ Resources:GlobalLanguage,HandleAgent %>" CausesValidation="false" OnClick="HandleAgent_Click"></asp:LinkButton>
        <asp:LinkButton id="btnSetAgentDeatail" runat="server" Text="<%$ Resources:GlobalLanguage,SetAgentDeatail %>" CausesValidation="false" OnClick="SetAgentDeatail_Click"></asp:LinkButton>
        </td>
    </tr>    
</table>
<table class="NoBorderTable" width="65%" style=" text-align:left;">
    <tr>
        <td align="right" width="90px"><span style="color:Red;">*</span><asp:Label ID="Label1" runat="server" Text="<%$ Resources:GlobalLanguage,GrowMemberAccount %>"></asp:Label></td>
        <td><asp:TextBox ID="txtGrowMemberAccout" runat="server" Width="239px" MaxLength="20" class="TextBox" ></asp:TextBox></td>
        <td style="width:200px" class="ValidateTd"><asp:Label ID="spanAccountNull" runat="server" Text="<%$ Resources:GlobalLanguage,AccountNull %>" style="display:none;"></asp:Label>
        <asp:Label ID="spanAccount" runat="server" Text="<%$ Resources:GlobalLanguage,AccountExists %>" style="display:none;"></asp:Label>
        <asp:Label ID="lblAccountRule" runat="server" Text="<%$ Resources:GlobalLanguage,LoginNameRule %>" style="display:none;"></asp:Label></td>
    </tr>
    <tr>
        <td align="right" width="90px"><span style="color:Red;">*</span><asp:Label ID="Label2" runat="server" Text="<%$ Resources:GlobalLanguage,GrowMemberEmail %>"></asp:Label></td>
        <td width="239px"><asp:TextBox ID="txtGrowMemberEmail" runat="server" Width="239px" class="TextBox"></asp:TextBox></td>
        <td class="ValidateTd">
        <asp:Label ID="emailSpanNull" runat="server" Text="<%$ Resources:GlobalLanguage,EmailNull %>" style="display:none;"></asp:Label>
        <asp:Label ID="emailSpan" runat="server" Text="<%$ Resources:GlobalLanguage,EmailError %>" style="display:none;"></asp:Label>
        <asp:Label ID="spanEmailExists" runat="server" Text="<%$ Resources:GlobalLanguage,EmailExists %>" style="display:none;"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="right"><asp:Button ID="btnGrowMember" runat="server" Text="<%$ Resources:GlobalLanguage,btnGrowMember %>"  CausesValidation="true" OnClick="GrowMember_Click" OnClientClick="return CheckGrowMemberAccout();"/></td>
        <td></td>
    </tr>
    <tr>
        <td colspan="3" style="color:Red; font-size:large;">
        <asp:Label ID="lblGrowSuccess" runat="server" Text="<%$ Resources:GlobalLanguage,GrowSuccess %>" Visible="false"></asp:Label>
        <asp:Label ID="lblGrowFail" runat="server" Text="<%$ Resources:GlobalLanguage,GrowFail %>" Visible="false"></asp:Label>
        </td>
    </tr>
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gdvGrowMember" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%"
                EmptyDataText="<%$ Resources:GlobalLanguage,NoGrowMember %>" 
                onrowdatabound="gdvGrowMember_RowDataBound" >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="LOGIN_NAME" HeaderText="<%$ Resources:GlobalLanguage,GrowMemberAccount %>" />
                    <asp:BoundField DataField="EMAIL_ADDRESS" HeaderText="<%$ Resources:GlobalLanguage,GrowMemberEmail %>" />
                    <asp:BoundField DataField="ACCOUNT_STATUS" HeaderText="<%$ Resources:GlobalLanguage,ActiveStatus %>" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250" HeaderText="<%$ Resources:GlobalLanguage,Action %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="hlResendMail" Text="<%$ Resources:GlobalLanguage,ResendMail %>" runat="server" CssClass="HyperLink" CausesValidation="false" OnClick="ResendMail_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID="pageNavigator" runat="server" />
        </td>
    </tr>
</table>
<script type="text/javascript">
    function CheckGrowMemberAccout() {
        var accountName = $('[id$=txtGrowMemberAccout]');
        var email = $('[id$=txtGrowMemberEmail]');
        var validaters = ["spanAccount", "spanAccountNull", "emailSpanNull", "emailSpan", "spanEmailExists", "lblGrowSuccess", "lblGrowFail", "lblAccountRule"];
        for (var i = 0, v; v = validaters[i++]; ) {
            //console.log(v);
            $('[id$=' + v + ']').hide();
        }
        
        if ($.trim(accountName.val()) == "") {
            $('[id$=spanAccountNull]').show()
            accountName.focus();
            return false;
        }
        else {
            $('[id$=spanAccountNull]').hide();
        }

        if (!validateLoginName('txtGrowMemberAccout')) {
            $('[id$=lblAccountRule]').show()
            accountName.focus();
            return false;
        }
        else {
            $('[id$=lblAccountRule]').hide()
        }

        if ($.trim(email.val()) == "") {
            $('[id$=emailSpanNull]').show();
            email.focus();
            return false;
        }
        else {
            $('[id$=emailSpanNull]').hide();
        }

        if (!isEmail(email.val())) {
            $('[id$=emailSpan]').show();
            email.focus();
            return false;
        }
        else {
            $('[id$=emailSpan]').hide();
        }
        var result = false;
        var requestURL = "AjaxResponseFrm.aspx?Key=CheckAccountName&Value=" + accountName.val();
        $.ajax({
            type: "POST",
            url: requestURL,
            async: false,
            success: function (code) {
                if (code == "True") {
                    $('[id$=spanAccount]').show();
                    accountName.focus();
                }
                else {
                    $('[id$=spanAccount]').hide();
                    result = true;
                }
            },
            error: function () {
                alert('error');
            }
        });

        if (!result)
            return result;

        requestURL = "AjaxResponseFrm.aspx?Key=CheckEmail&Value=" + email.val();
        $.ajax({
            type: "POST",
            url: requestURL,
            async: false,
            success: function (code) {
                if (code == "True") {
                    $('[id$=spanEmailExists]').show();
                    email.focus();
                    result = false;
                }
                else {
                    $('[id$=spanEmailExists]').hide();
                    result = true;
                }
            },
            error: function () {
                alert('error');
            }
        });
        return result;

    }

</script>
</asp:Content>
