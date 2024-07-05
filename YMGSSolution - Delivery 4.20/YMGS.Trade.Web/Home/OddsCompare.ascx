<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OddsCompare.ascx.cs"
    Inherits="YMGS.Trade.Web.Home.OddsCompare" %>
<asp:Panel ID="pnloddscompare" Width="100%" runat="server">
    <table width="100%" >
        <tr style="background-color: #273A45; height: 25px">
            <th colspan="6" align="left">
                <asp:Label ID="lblstartDate" runat="server" Text="<%$ Resources:GlobalLanguage,OddsCompare %>"
                    ForeColor="White"></asp:Label>
            </th>
        </tr>
        <tr>
            <td>
                
            </td>
            <td rowspan="3">
                <asp:Image ID="Image1" runat="server" Height="63px" Width="65px" ImageUrl="~/Images/teamA.jpg" />
            </td>
            <td>
                <asp:Label ID="lblTeamA" runat="server"></asp:Label>
            </td>
            <td>
            </td>
            <td rowspan="3" align="right">
                <asp:Image ID="Image2" runat="server" Height="63px" Width="65px" ImageUrl="~/Images/teamB.jpg" />
            </td>
            <td >
            </td>
        </tr>
        <tr>
            <td>
                <asp:ImageButton ID="ibtpre" runat="server" ImageUrl="~/Images/dotleft.jpg" OnClick="ibtpre_Click" />
            </td>
            <td>
                vs&nbsp;
            </td>
            <td>
            </td>
            <td align="right">
                <asp:ImageButton ID="ibtnext" runat="server" ImageUrl="~/Images/dotright.jpg" OnClick="ibtnext_Click" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="lblTeamB" runat="server"></asp:Label>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="6" class="topline">
                <table width="100%" style="height:100%" class="buttomLine" cellpadding=0 cellspacing=0>
                    <tr>
                        <td style="background-color:#C8DDF6; height:40px">
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:GlobalLanguage,In %>"></asp:Label>
                            <asp:Label ID="lblTeamAA" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:GlobalLanguage,WinIn %>"></asp:Label><br /><br />
                            <asp:Label ID="lblbetfair"  runat="server" Text="" Font-Bold="True" Font-Size="Large"></asp:Label>
                        </td>
                        <td align="right" style="background-color:#C8DDF6">
                           <b> $</b><asp:Label ID="lblprofit" Font-Bold="true" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptoddscompare" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Eval("corp") %>
                                </td>
                                <td align="right">
                                    $<%#Eval("PROFIT") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                 
                </table>
            </td>
        </tr>
   <tr>
                        <td colspan=3>
                            <asp:HyperLink ID="hlktentercompare" runat="server" ForeColor="#333399" Text="<%$ Resources:GlobalLanguage,Enteroddscomparesite %>"></asp:HyperLink>
                        </td>
                        <td align="right" colspan=3>
                            <asp:HyperLink ID="hlktrade" runat="server" Text="<%$ Resources:GlobalLanguage,BetRightNow %>"
                                ForeColor="#333399"></asp:HyperLink>
                        </td>
                    </tr>
    </table>
    <asp:HiddenField ID="hfditem" runat="server" Value="-1" />
    <asp:DropDownList ID="ddlitem" runat="server" Visible="false">
    </asp:DropDownList>
</asp:Panel>
