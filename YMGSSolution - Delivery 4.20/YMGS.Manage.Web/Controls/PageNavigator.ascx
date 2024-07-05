<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageNavigator.ascx.cs" 
    Inherits="YMGS.Manage.Web.Controls.PageNavigator" %>
<table class="NoBorderTable" width=100%>
    <tr class="GridViePageNavigator">
        <td >
          <asp:Label ID="lbls" runat="server" Text="每页显示"></asp:Label>
    <asp:TextBox runat="server" Width="35" MaxLength=5  onkeypress="if((event.keyCode<48 ||event.keyCode>57)) event.returnValue=false;" ID="txtShowPage" Font-Size="9pt"></asp:TextBox>
    <asp:Label runat="server" ID="lblt"  Text="条记录"></asp:Label> 
        </td>
        <td align="right">
            <asp:Label runat="server" Width="150" ID="totalCount"></asp:Label>
        </td>
        <td align="center">
            <asp:Label runat="server" Text="0/0" Width="50" ID="label1"></asp:Label>
        </td>
        <td>
            <asp:LinkButton runat="server" Width="40" Text="首页" ID="FirstButton" CausesValidation="false"
               Font-Underline="true" OnClick="FirstButton_Click"></asp:LinkButton>
        </td>
        <td>
            <asp:LinkButton runat="server" Width="50" Text="上一页" ID="PreviousButton" CausesValidation="false"
                Font-Underline="true" OnClick="PreviousButton_Click"></asp:LinkButton>
        </td>
        <td>
            <asp:LinkButton runat="server" Width="50" Text="下一页" ID="NextButton" CausesValidation="false"
                 Font-Underline="true" OnClick="NextButton_Click"></asp:LinkButton>
        </td>
        <td>
            <asp:LinkButton runat="server" Width="40" Text="末页" ID="LastButton" CausesValidation="false"
                Font-Underline="true" OnClick="LastButton_Click"></asp:LinkButton>
        </td>
    </tr>
</table>
