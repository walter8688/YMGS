<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SimpleMaster.Master"
    AutoEventWireup="true" CodeBehind="UserRegisterFrm.aspx.cs" Inherits="YMGS.Trade.Web.Public.UserRegisterFrm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
   
            <table width="600px"  class="Registerstyle" >
                <tr><td colspan="2" style="color:Red;"><asp:Label ID="lblCompleteYourDetail" Font-Bold="True" runat="server" Text="<%$ Resources:GlobalLanguage,GrowMemberRegisterSubject %>"></asp:Label></td></tr>
                <tr>
                    <td >
                        <asp:Label ID="Label1" Font-Bold="True" runat="server" Text="<%$ Resources:GlobalLanguage,YourDetail %>"></asp:Label>
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="USER_NAME" Display="None" ErrorMessage="<%$ Resources:GlobalLanguage,UserNameNotNull %>" 
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
                                    <asp:Label ID="Label8" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                                        ID="Label9" runat="server" Text="<%$ Resources:GlobalLanguage,Email %>"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="EMAIL_ADDRESS" runat="server" Width="100%" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                        ControlToValidate="EMAIL_ADDRESS" Display="None" ErrorMessage="<%$ Resources:GlobalLanguage,emailnotnull %>" 
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                        ControlToValidate="EMAIL_ADDRESS" ErrorMessage="<%$ Resources:GlobalLanguage,isnotemailstyle %>" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                        Display="None" SetFocusOnError="True"></asp:RegularExpressionValidator>
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
                                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:GlobalLanguage,Tel %>"></asp:Label>
                                </td>
                                <td align="left">
                                   <asp:TextBox ID="PHONE_NUMBER" runat="server"  MaxLength="20" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label23" runat="server" Text="<%$ Resources:GlobalLanguage,Currency %>"></asp:Label>
                                </td>
                                <td align="left">
                                   <asp:DropDownList ID="drpCurrency" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label24" runat="server" Text="<%$ Resources:GlobalLanguage,Timezone %>"></asp:Label>
                                </td>
                                <td align="left">
                                   <asp:DropDownList ID="drpTimeZone" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label4" Font-Bold="True" runat="server" Text="<%$ Resources:GlobalLanguage,BFBAccountInfo %>"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <table width="450px">
                            <tr>
                                <td align="right" width="120px">
                                    <asp:Label ID="Label13" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                                        ID="Label14" runat="server" Text="<%$ Resources:GlobalLanguage,LoginName %>"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="LOGIN_NAME" runat="server" Width="100%" MaxLength="20"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="LOGIN_NAME" Display="None" ErrorMessage="<%$ Resources:GlobalLanguage,LoginNamenotnull %>" 
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="valloginname"  runat="server" Display="None" ErrorMessage="<%$ Resources:GlobalLanguage,LoginNameRule %>" ClientValidationFunction="valloginname"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label15" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                                        ID="Label16" runat="server" Text="<%$ Resources:GlobalLanguage,Psw %>"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="PASSWORD" runat="server" TextMode="Password" Width="100%" 
                                        MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="PASSWORD" Display="None" ErrorMessage="<%$ Resources:GlobalLanguage,Pswnotnull %>" 
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="CustomValidator1"  runat="server" Display="None" ErrorMessage="<%$ Resources:GlobalLanguage,PasswordRule %>" ClientValidationFunction="valpassword"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label17" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                                        ID="Label18" runat="server" Text="<%$ Resources:GlobalLanguage,ConfirmPsw %>"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtcpassword" runat="server" TextMode="Password" Width="100%" 
                                        MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                        ControlToValidate="txtcpassword" Display="None" ErrorMessage="<%$ Resources:GlobalLanguage,ConfirmPswnotnull %>" 
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label20" runat="server" Text="<%$ Resources:GlobalLanguage,Question %>"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="SQUESTION1" runat="server" Width="100%">
                                        <asp:ListItem Value="1" Text="<%$ Resources:GlobalLanguage,fathername %>"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="<%$ Resources:GlobalLanguage,mathername %>"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="<%$ Resources:GlobalLanguage,likesinger %>"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label27" ForeColor="Red" runat="server" Text="*"></asp:Label>
                                    <asp:Label ID="Label19" runat="server" Text="<%$ Resources:GlobalLanguage,Answer %>"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="SANSWER1" runat="server" Width="100%" MaxLength="100"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="SANSWER1" Display="None" ErrorMessage="<%$ Resources:GlobalLanguage,Answernotnull %>" 
                                        SetFocusOnError="True"></asp:RequiredFieldValidator> 
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label26" ForeColor="Red" runat="server" Text="*"></asp:Label>
                                    <asp:Label ID="Label21" runat="server" Text="<%$ Resources:GlobalLanguage,ValidateCode %>"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtvalidatecode" runat="server" Width="65px"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                                        ControlToValidate="txtvalidatecode" Display="None" ErrorMessage="<%$ Resources:GlobalLanguage,ValidateCodenotnull %>" 
                                        SetFocusOnError="True"></asp:RequiredFieldValidator> 
                                 <asp:Image ID="imgValidate" runat="server"  />
                                    <a id="safecode" href='javascript: reloadcode("<%= imgValidate.ClientID %>");'><asp:Label ID="Label25" runat="server" Text="<%$ Resources:GlobalLanguage,ChangePic %>"></asp:Label></a>
                                </td>
                            </tr>
                            <tr style="height:10px">
                                <td> 
                                </td>   <td> 
                                </td>
                                </tr>
                            <tr>
                                <td> 
                                </td>
                                <td align="left">
                                    <input id="Checkbox2" checked type="checkbox" onclick="javascript:checkbutton('<%= btnRegiester.ClientID %>',this);" />
                                    <%--<asp:Label ID="Label22" runat="server" Text="<%$ Resources:GlobalLanguage,AgreeBFB %>"></asp:Label>--%>
                                    <asp:Label ID="Label22" runat="server" Text="<%$ Resources:GlobalLanguage,Agreement1 %>"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td> 
                                </td>
                                <td align="left">
                                    <asp:HyperLink ID="hlkhelper" runat="server" ForeColor="#3333FF" Text="<%$ Resources:GlobalLanguage,Agreement2 %>"></asp:HyperLink>
                                    <asp:Label ID="Label28" runat="server" Text="<%$ Resources:GlobalLanguage,Agreement3 %>"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                            ShowMessageBox="True" ShowSummary="False" />
                    </td>
                </tr>
                <tr >
                    <td colspan="2" align="center">
                        <table>
                            <tr style="height:20px">
                                <td> 
                                     </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnRegiester" runat="server" 
                                        Text="<%$ Resources:GlobalLanguage,Register %>" onclick="btnRegiester_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
<script type="text/ecmascript">
    function valloginname(source, args) {
        if (!validateLoginName('LOGIN_NAME')) {
            args.IsValid = false;
            return;
        }
        args.IsValid = true;
    }

    function valpassword(source, args) {
        if (!validatePassword('PASSWORD', 'LOGIN_NAME')) {
            args.IsValid = false;
            return;
        }
        args.IsValid = true;
    }
</script>      
</asp:Content>
