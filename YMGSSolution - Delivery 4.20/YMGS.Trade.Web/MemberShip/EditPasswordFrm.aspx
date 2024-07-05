<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master"
    AutoEventWireup="true" CodeBehind="EditPasswordFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.EditPasswordFrm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <asp:Panel ID="pnleditpassword" runat="server" Visible="false">
        <table>
            <tr>
                <td align="right">
                    <asp:Label ID="Label1" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                        ID="Label2" runat="server" Text="<%$ Resources:GlobalLanguage,oldPsw %>"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtoldpsw" runat="server" TextMode="Password" Width="100%" MaxLength="200"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtoldpsw"
                        Display="None" ErrorMessage="<%$ Resources:GlobalLanguage,Pswnotnull %>" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label15" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                        ID="Label16" runat="server" Text="<%$ Resources:GlobalLanguage,newPsw %>"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="PASSWORD" runat="server" TextMode="Password" Width="100%" MaxLength="200"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="PASSWORD"
                        Display="None" ErrorMessage="<%$ Resources:GlobalLanguage,Pswnotnull %>" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label17" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                        ID="Label18" runat="server" Text="<%$ Resources:GlobalLanguage,ConfirmnewPsw %>"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtcpassword" runat="server" TextMode="Password" Width="100%" MaxLength="200"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtcpassword"
                        Display="None" ErrorMessage="<%$ Resources:GlobalLanguage,ConfirmPswnotnull %>"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnsave" runat="server" Text="<%$ Resources:GlobalLanguage,Save %>"
                        OnClick="btnsave_Click" />
                    <asp:HiddenField ID="hfdid" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlresult" runat="server" Visible="false">
        <asp:Label ID="lblEditEmailSuccess" runat="server" Text="<%$ Resources:GlobalLanguage,EditPasswordSuccess %>"
            Font-Size="Large" ForeColor="#009933" Visible="False"></asp:Label>
        <asp:Label ID="lbltips" runat="server" Text="<%$ Resources:GlobalLanguage,EditPasswordTips %>"
            Font-Size="Large" ForeColor="#009933" Visible="False"></asp:Label>
    </asp:Panel>
</asp:Content>
