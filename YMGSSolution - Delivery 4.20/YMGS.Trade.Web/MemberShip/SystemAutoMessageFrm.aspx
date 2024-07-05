<%@ Page Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master" AutoEventWireup="true"
    CodeBehind="SystemAutoMessageFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.SystemAutoMessageFrm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <asp:UpdatePanel ID="upl" runat="server">
        <ContentTemplate>
            <table class="NoBorderTable" width="100%">
                <tr>
                    <td>
                        <table class="NoBorderTable" style="margin-top: 5px; margin-bottom: 5px; width: 100%">
                            <tr>
                                <td style="width: 60px">
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:GlobalLanguage,BeginDate %>"></asp:Label>
                                </td>
                                <td style="width: 90px">
                                    <div class="calendarContainer">
                                        <YMGS:AjaxCalendar ID="calStartDate" runat="server" NeedCalendarButton="true" />
                                    </div>
                                </td>
                                <td style="width: 10px">
                                </td>
                                <td style="width: 60px">
                                    <asp:Label ID="Label2" runat="server" Text="<%$ Resources:GlobalLanguage,EndDate %>"></asp:Label>
                                </td>
                                <td style="width: 90px">
                                    <div class="calendarContainer">
                                        <YMGS:AjaxCalendar ID="calEndDate" runat="server" NeedCalendarButton="true" />
                                    </div>
                                </td>
                                <td colspan="2" align="right">
                                    <asp:Button ID="btnQuery" Width="70px" CssClass="Button" runat="server" CausesValidation="false"
                                        Text="<%$ Resources:GlobalLanguage,Query %>" OnClick="BtnQuery_Click" />&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table class="NoBorderTable" width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="gdvSystemAutoMessage" runat="server" AutoGenerateColumns="false"
                            GridLines="None" CssClass="GridView" Width="100%" EmptyDataText="<%$ Resources:GlobalLanguage,NoSystemMessage %>"
                            OnRowDataBound="gdvSystemAutoMessage_RowDataBound" OnRowCommand="gdvSystemAutoMessage_RowCommand">
                            <EmptyDataRowStyle HorizontalAlign="Left" />
                            <Columns>
                                <asp:BoundField DataField="MESSAGEID" Visible="false" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="500" HeaderText="<%$ Resources:GlobalLanguage,MessageContent %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMsgContent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150" HeaderText="<%$ Resources:GlobalLanguage,DateTime %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMsgDate" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100" HeaderText="<%$ Resources:GlobalLanguage,Action %>">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnView" Text="<%$ Resources:GlobalLanguage,View %>" runat="server"
                                            CssClass="HyperLink" CausesValidation="false"></asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" Text="<%$ Resources:GlobalLanguage,Delete %>" runat="server"
                                            CssClass="HyperLink" CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <YMGS:PageNavigator ID="pageNavigator" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnFakeFund" runat="server" Style="display: none" />
            <asp:Panel ID="pnlSysAutoMsgPopup" runat="server" Height="160px" Style="display: none"
                CssClass="ModalPoup">
                <asp:Panel ID="pnlSysAutoMsgPopupHeader" runat="server" CssClass="ModalPoupHeader">
                    <span>
                        <asp:Label ID="lblSysAutoMsgDetailsHeader" Text="<%$ Resources:GlobalLanguage,MessageContent %>"
                            runat="server"></asp:Label>
                    </span>
                </asp:Panel>
                <table class="NoBorderTable" width="100%">
                    <tr>
                        <td colspan="3" style="height: 10px;">
                            <asp:Label ID="lblMsgContentDetails" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="NoBorderTable" width="100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="BtnOK" runat="server" Text="<%$ Resources:GlobalLanguage,close %>"
                                Width="70px" CausesValidation="false" CssClass="Button" />
                        </td>
                    </tr>
                </table>
                <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlSysAutoMsgPopup" BehaviorID="mdlSysAutoMsgPopup"
                    TargetControlID="btnFakeFund" PopupControlID="pnlSysAutoMsgPopup" BackgroundCssClass="ModalPopupBackground"
                    CancelControlID="BtnOK" PopupDragHandleControlID="pnlSysAutoMsgPopupHeader">
                </ajaxToolkit:ModalPopupExtender>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
