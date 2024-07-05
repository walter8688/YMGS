<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopRaceCtrl.ascx.cs"
    Inherits="YMGS.Trade.Web.Home.TopRaceCtrl" %>
<table width="100%">
    <tr>
        <td rowspan="3" style="width:60px">
            <asp:Image ID="imgADPic" runat="server" Height="176px"  Width=177px />
            </td>
         <th colspan="3">
             <asp:Label ID="lblmatchname" runat="server" Text="" Font-Bold="True" 
                 Font-Names="Franklin Gothic Demi" Font-Size="XX-Large" ForeColor="#FF9900"></asp:Label>
        </th>
    </tr>
     <tr>
         <td style="width:50%">
             <asp:Label ID="lbltitle" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
        </td>
         <td rowspan="2" style="width:1px"><p style=" background-color:Gray; width:1px; height:50px"></p>
        </td>
         <td rowspan="2" style="width:100px">
             <asp:HyperLink ID="hlkbetNow" runat="server" Text="<%$ Resources:GlobalLanguage,BetNow %>">></asp:HyperLink>
        </td>
    </tr>
     <tr>
         <td>
             <asp:Label ID="lblcontent" runat="server" Font-Size="Small"></asp:Label>
             <asp:HiddenField ID="hfdmark" runat="server" />
              <asp:HiddenField ID="hfdmarchid" runat="server" Value="0" />
        </td>
    </tr>
     </table>
