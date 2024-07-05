<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master" AutoEventWireup="true" CodeBehind="EditEmailFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.EditEmailFrm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
<table>
 <tr>
            <td>    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:GlobalLanguage,OldEmail %>"
                    Font-Size="Large" ForeColor="#009933" ></asp:Label>
                <asp:Label ID="lbloldemail" runat="server" Text=""
                    Font-Size="Large" ForeColor="#009933"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>    <asp:Label ID="lblfail" runat="server" Text="<%$ Resources:GlobalLanguage,NewEmail %>"
                    Font-Size="Large" ForeColor="#009933"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                        ControlToValidate="txtEmail" ErrorMessage="<%$ Resources:GlobalLanguage,isnotemailstyle %>" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                        Display="None" SetFocusOnError="True"></asp:RegularExpressionValidator>

            </td>
        </tr>
           <tr>
                <td align="center" >
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                </td>
            </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnsave" runat="server" Visible="false" 
                    Text="<%$ Resources:GlobalLanguage,Save %>" onclick="btnsave_Click" />
                <asp:HiddenField ID="hfdid" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
              
            </td>
        </tr>
    </table>
</asp:Content>
