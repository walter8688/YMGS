<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeRecommandRace.ascx.cs"
    Inherits="YMGS.Trade.Web.Home.HomeRecommandRace" %>
<table width="100%" cellspacing=0 cellpadding=0>
    <tr style="background-color:#273A45;height:25px ">
        <th colspan="2" align="left">
            <asp:Label ID="lblstartDate" runat="server" Text="<%$ Resources:GlobalLanguage,RecommandRace %>" ForeColor="White"></asp:Label>
        </th>
    </tr>
    <asp:Repeater ID="rptRecRace" runat="server" OnItemDataBound="rptRecRace_ItemDataBound">
        <ItemTemplate>
            <tr  style="background-color:#72BBEF">
                <th align="center">
                    <asp:HiddenField ID="hfdeventid" runat="server" Value=' <%#Eval("EVENT_ID") %>' />
                    <%#Eval("EVENT_NAME")%>
                </th>
            </tr>
            <tr>
                <th align="left">
                    <asp:Label ID="lblstartDate" runat="server" Text='<%#Eval("StartDate") %>'></asp:Label>
                </th>
            </tr>
            <asp:Repeater ID="rptsubRecRace" runat="server" OnItemDataBound="rptsubRecRace_ItemDataBound">
                <ItemTemplate>
                    <tr >
                        <td align="left"  >
                            <asp:HiddenField ID="hfdparam" runat="server" Value=' <%#Eval("param") %>' />
                            <asp:HyperLink ID="hlkmatch" runat="server"><%#Eval("MATCH_NAME") %></asp:HyperLink>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </ItemTemplate>
    </asp:Repeater>
</table>
