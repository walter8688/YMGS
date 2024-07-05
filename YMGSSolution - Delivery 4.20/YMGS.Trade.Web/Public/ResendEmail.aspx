<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SimpleMaster.Master" AutoEventWireup="true" CodeBehind="ResendEmail.aspx.cs" Inherits="YMGS.Trade.Web.Public.ResendEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
 <table>
        <tr>
            <td>   
                <asp:Label ID="lblactive" runat="server" Text="<%$ Resources:GlobalLanguage,isresendemail %>"
                    Font-Size="Large" ForeColor="#009933" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnyes" runat="server" 
                    Text="<%$ Resources:GlobalLanguage,yes %>" onclick="btnyes_Click"/>
              
            </td>
        </tr>
    </table>
</asp:Content>
