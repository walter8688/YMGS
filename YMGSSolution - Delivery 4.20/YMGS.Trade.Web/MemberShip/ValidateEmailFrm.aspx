<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master" AutoEventWireup="true" CodeBehind="ValidateEmailFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.ValidateEmailFrm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
<table>
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
                    <td align="center" colspan="2">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                            ShowMessageBox="True" ShowSummary="False" />
                    </td>
                </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnsave" runat="server" 
                    Text="<%$ Resources:GlobalLanguage,Validate %>" onclick="btnsave_Click" />
            </td>
        </tr>
        <tr>
            <td>
              
            </td>
        </tr>
    </table>
</asp:Content>
