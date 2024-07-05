<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeADWords.ascx.cs"
    Inherits="YMGS.Trade.Web.Home.HomeADWords" %>
<table width="100%" cellspacing="0" cellpadding="0">
    <asp:Repeater ID="rptADWords" runat="server">
        <ItemTemplate>
            <tr>
                <td style="border: 1px solid #C0C0C0">
                    <table>
                        <tr style="height: 25px">
                            <th colspan="2" align="left">
                                <asp:Label ID="lbltitle" runat="server" Text='<%#Eval("title") %>'
                                    ></asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:HyperLink ID="hlkdesc" runat="server" ForeColor="Silver" NavigateUrl='<%#Eval("weblink") %>'><%#Eval("desc") %></asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
