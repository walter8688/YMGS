<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageNavigator.ascx.cs" 
    Inherits="YMGS.Manage.Web.Controls.PageNavigator" %>
<table class="NoBorderTable" width=100%>
    <tr class="GridViePageNavigator">
        <td >
          <asp:Label ID="lbls" runat="server" Text="<%$ Resources:GlobalLanguage,displayonepage %>"></asp:Label>
    <asp:TextBox runat="server" Width="35" MaxLength=5  onkeypress="if((event.keyCode<48 ||event.keyCode>57)) event.returnValue=false;" ID="txtShowPage" Font-Size="9pt"></asp:TextBox>
    <asp:Label runat="server" ID="lblt"  Text="<%$ Resources:GlobalLanguage,tiaorecord %>"></asp:Label> 
        </td>
        <td align="right">
            <asp:Label runat="server" Width="150" ID="totalCount"></asp:Label>
        </td>
        <td align="center">
            <asp:Label runat="server" Text="0/0" Width="50" ID="label1"></asp:Label>
        </td>
        <td>
            <asp:LinkButton runat="server" Width="40" Text="<%$ Resources:GlobalLanguage,firstpage %>" ID="FirstButton" CausesValidation="false"
               Font-Underline="true" OnClick="FirstButton_Click"></asp:LinkButton>
        </td>
        <td>
            <asp:LinkButton runat="server" Width="50" Text="<%$ Resources:GlobalLanguage,prepage %>" ID="PreviousButton" CausesValidation="false"
                Font-Underline="true" OnClick="PreviousButton_Click"></asp:LinkButton>
        </td>
        <td>
            <asp:LinkButton runat="server" Width="50" Text="<%$ Resources:GlobalLanguage,nextpage %>" ID="NextButton" CausesValidation="false"
                 Font-Underline="true" OnClick="NextButton_Click"></asp:LinkButton>
        </td>
        <td>
            <asp:LinkButton runat="server" Width="40" Text="<%$ Resources:GlobalLanguage,lastpage %>" ID="LastButton" CausesValidation="false"
                Font-Underline="true" OnClick="LastButton_Click"></asp:LinkButton>
        </td>
    </tr>
</table>
