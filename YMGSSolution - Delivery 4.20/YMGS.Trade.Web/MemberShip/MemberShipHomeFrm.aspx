<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master"
    AutoEventWireup="true" CodeBehind="MemberShipHomeFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.MemberShipHomeFrm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <table width="600px" class="Registerstyle">
        <tr>
            <td>

            </td>
            <td align="right">
                <asp:Label ID="Label2" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                    ID="Label3" runat="server" Text="<%$ Resources:GlobalLanguage,MustInput %>"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <table width="450px">
                    <tr>
                        <td align="right" width="120px">
                            <asp:Label ID="Label5" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                                ID="Label6" runat="server" Text="<%$ Resources:GlobalLanguage,UserName %>"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="USER_NAME" runat="server" Width="100%" MaxLength="40"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="USER_NAME"
                                Display="None" ErrorMessage="<%$ Resources:GlobalLanguage,UserNameNotNull %>"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td align="right">
                            <asp:Label ID="Label7" runat="server" Text="<%$ Resources:GlobalLanguage,BirthIn %>"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="BORN_YEAR" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="BORN_MONTH" runat="server">
                                <asp:ListItem Text="Month" Value="0"></asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="BORN_DAY" runat="server" Width="61px">
                                <asp:ListItem Text="Day" Value="0"></asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label12" runat="server" Text="<%$ Resources:GlobalLanguage,Tel %>"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="PHONE_NUMBER" runat="server" MaxLength="20" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label8" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                                ID="Label9" runat="server" Text="<%$ Resources:GlobalLanguage,Email %>"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="EMAIL_ADDRESS" runat="server" Width="75%" MaxLength="50" 
                                Enabled="False"></asp:TextBox>
                            <asp:LinkButton ID="lbtEditEmail" runat="server" 
                                Text="<%$ Resources:GlobalLanguage,EditEmail %>" onclick="lbtEditEmail_Click"></asp:LinkButton>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="EMAIL_ADDRESS"
                                Display="None" ErrorMessage="<%$ Resources:GlobalLanguage,emailnotnull %>" SetFocusOnError="True"></asp:RequiredFieldValidator>
                          
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:GlobalLanguage,Address %>"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="COUNTRY" runat="server">
                            </asp:DropDownList>
                            <asp:TextBox ID="CITY" runat="server" MaxLength="50">city</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="ADDRESS" runat="server" TextMode="MultiLine" Width="100%" Text="Address"
                                Height="48px" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label11" runat="server" Text="<%$ Resources:GlobalLanguage,Postcode %>"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="ZIP_CODE" runat="server" Width="100%" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label23" runat="server" Text="<%$ Resources:GlobalLanguage,Currency %>"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="CURRENCY_ID" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label24" runat="server" Text="<%$ Resources:GlobalLanguage,Timezone %>"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="TIMEZONE_ID" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
       <tr>
            <td align="center" colspan="2">
               <table><tr><td>  <asp:LinkButton ID="lbtEditPassword" runat="server" 
                                 onclick="lbtEditPassword_Click" Text="<%$ Resources:GlobalLanguage,EditPsw %>"></asp:LinkButton>
                        </td> <td>  
                       <asp:LinkButton ID="lbtEditSecuretyQuestion" runat="server" onclick="lbtEditSecuretyQuestion_Click" 
                             Text="<%$ Resources:GlobalLanguage,EditSecuretyQ %>"    ></asp:LinkButton>
                        </td></tr></table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <table>
                    <tr style="height: 20px">
                        <td>
                        </td> <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:GlobalLanguage,Save %>"
                                OnClick="btnRegiester_Click" />
                        </td>
                         <td>
                           

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
