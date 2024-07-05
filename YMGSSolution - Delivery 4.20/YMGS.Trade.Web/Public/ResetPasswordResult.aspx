<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SimpleMaster.Master" AutoEventWireup="true" CodeBehind="ResetPasswordResult.aspx.cs" Inherits="YMGS.Trade.Web.Public.ResetPasswordResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
 <table>
        <tr>
            <td>  
                <asp:Label ID="lblresult" runat="server" Text="<%$ Resources:GlobalLanguage,ResetSuccess %>"
                    Font-Size="Large" ForeColor="#009933" Visible="true"></asp:Label>
             
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblnote" runat="server" Text="<%$ Resources:GlobalLanguage,Relogin %>"
                    Visible="true"></asp:Label>
            </td>
        </tr>
      
    </table>
</asp:Content>
