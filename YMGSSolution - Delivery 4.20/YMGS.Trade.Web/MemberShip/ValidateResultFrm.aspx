<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master" AutoEventWireup="true" CodeBehind="ValidateResultFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.ValidateResultFrm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
<table><tr><td><asp:Label ID="lblSendEditEmail" runat="server" Text="<%$ Resources:GlobalLanguage,SendEditEmail %>"
                    Font-Size="Large" ForeColor="#009933" Visible="False"></asp:Label>
                    <asp:Label ID="lblEditEmailSuccess" runat="server" Text="<%$ Resources:GlobalLanguage,EditEmailSuccess %>"
                    Font-Size="Large" ForeColor="#009933" Visible="False"></asp:Label>
                    </td></tr></table>
</asp:Content>
