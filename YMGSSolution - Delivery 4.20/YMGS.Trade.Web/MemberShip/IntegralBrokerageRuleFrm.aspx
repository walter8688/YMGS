<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master" AutoEventWireup="true" CodeBehind="IntegralBrokerageRuleFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.IntegralBrokerageRuleFrm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
<table class="NoBorderTable" width="100%">
    <tr>
        <td style="text-align:right;">
            <asp:LinkButton ID="btnMyIntegral" runat="server" Text="<%$ Resources:GlobalLanguage,MyIntegralPage %>" OnClick="BtnMyIntegral_Click" CausesValidation="false" ></asp:LinkButton>
        </td>
    </tr>
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gdvBrokerage" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%" OnRowDataBound="gdvBrokerage_RowDataBind">
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35%" HeaderText="<%$ Resources:GlobalLanguage,Brokerage %>">
                        <ItemTemplate>
                            <asp:Label ID="lblBrokerageRate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="35%" HeaderText="<%$ Resources:GlobalLanguage,IntegralRange %>">
                        <ItemTemplate>
                            <asp:Label ID="lblIntegral" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
</asp:Content>
