<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SimpleMaster.Master"
    AutoEventWireup="true" CodeBehind="RegisterResult.aspx.cs" Inherits="YMGS.Trade.Web.Public.RegisterResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <table>
        <tr>
            <td>    <asp:Label ID="lblfail" runat="server" Text="<%$ Resources:GlobalLanguage,RegesterFail %>"
                    Font-Size="Large" ForeColor="#009933" Visible="False"></asp:Label>
                <asp:Label ID="lblresult" runat="server" Text="<%$ Resources:GlobalLanguage,RegesterSuccess %>"
                    Font-Size="Large" ForeColor="#009933" Visible="False"></asp:Label>
                <asp:Label ID="lblactive" runat="server" Text="<%$ Resources:GlobalLanguage,ActiveSuccess %>"
                    Font-Size="Large" ForeColor="#009933" Visible="False"></asp:Label>
                         <asp:Label ID="lblresendemail" runat="server" Text="<%$ Resources:GlobalLanguage,resemail %>"
                    Font-Size="Large" ForeColor="#009933" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblnote" runat="server" Text="<%$ Resources:GlobalLanguage,registerresult %>"
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblrelogin" runat="server" Text="<%$ Resources:GlobalLanguage,Relogin %>"
                    Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
