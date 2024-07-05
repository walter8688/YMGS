<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SimpleMaster.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="YMGS.Trade.Web.Public.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
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
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblpstar" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                                        ID="lblpsw" runat="server" Text="<%$ Resources:GlobalLanguage,Psw %>"></asp:Label>
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
                                    <asp:Label ID="lblscpstar" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                                        ID="lblcpsw" runat="server" Text="<%$ Resources:GlobalLanguage,ConfirmPsw %>"></asp:Label>
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
                                 
                                    <asp:Label ID="lblquestion" runat="server" Text="<%$ Resources:GlobalLanguage,Question %>"></asp:Label>
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
                                <td align="right"><asp:Label ID="lblastar" ForeColor="Red" runat="server" Text="*">   </asp:Label>
                                    <asp:Label ID="lblanswer" runat="server" Text="<%$ Resources:GlobalLanguage,Answer %>"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="SANSWER1" runat="server" Width="100%" MaxLength="100"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                        ControlToValidate="SANSWER1" Display="None" ErrorMessage="<%$ Resources:GlobalLanguage,Answernotnull %>" 
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
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
                                  
                                    <asp:Button ID="btnok" runat="server" Text="<%$ Resources:GlobalLanguage,Ok %>" 
                                        onclick="btnok_Click" />
                                    <asp:Button ID="btnreset" runat="server" 
                                        Text="<%$ Resources:GlobalLanguage,Reset %>" onclick="btnreset_Click" />
                                  
                                </td>
                            </tr>
                        </table>
</asp:Content>
