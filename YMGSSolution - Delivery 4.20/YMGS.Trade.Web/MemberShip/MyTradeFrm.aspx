<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master"
    AutoEventWireup="true" CodeBehind="MyTradeFrm.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.MyTradeFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <table width="100%" class="NoBorderTable">
                <tr>
                    <td>
                        
                            <table class="NoBorderTable" style="margin-top: 5px; margin-bottom: 5px; width: 100%">
                                <tr>
                                    <td style="width: 70px">
                                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:GlobalLanguage,bettype %>"></asp:Label>
                                    </td>
                                    <td style="width: 120px">
                                        <asp:DropDownList ID="ddlbettype" runat="server">
                                            <asp:ListItem Value="0" Text="<%$ Resources:GlobalLanguage,All %>"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="<%$ Resources:GlobalLanguage,buybet %>"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="<%$ Resources:GlobalLanguage,Salebet %>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 70px">
                                    <asp:Label ID="Label3" runat="server" Text="<%$ Resources:GlobalLanguage,Betstatus %>"></asp:Label>
                                    </td>
                                    <td style="width: 120px">
                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                            <asp:ListItem Value="0" Text="<%$ Resources:GlobalLanguage,All %>"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="<%$ Resources:GlobalLanguage,matching %>"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="<%$ Resources:GlobalLanguage,matched %>"></asp:ListItem>
                                           <%-- <asp:ListItem Value="3" Text="<%$ Resources:GlobalLanguage,settlementedmatched %>"></asp:ListItem>--%>
                                            <asp:ListItem Value="4" Text="<%$ Resources:GlobalLanguage,settlement %>"></asp:ListItem>
                                            <%--<asp:ListItem Value="5" Text="<%$ Resources:GlobalLanguage,Rotaryheader %>"></asp:ListItem>--%>
                                            <asp:ListItem Value="6" Text="<%$ Resources:GlobalLanguage,Canceled %>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 70px">
                                       <asp:Label ID="Label6" runat="server" Text="<%$ Resources:GlobalLanguage,matchname %>"></asp:Label>
                                    </td>
                                    <td style="width: 120px">
                                        <asp:DropDownList ID="ddlMyMatch" runat="server"></asp:DropDownList>
                                    </td>
                                    <td align="right">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                     <asp:Label ID="Label4" runat="server" Text="<%$ Resources:GlobalLanguage,BetStartTime %>"></asp:Label>
                                    </td>
                                    <td style="width: 120px">
                                        <div class="calendarContainer">
                                          <YMGS:AjaxCalendar ID="startDate" runat="server" />
                                        </div>
                                    </td>
                                    <td style="width: 100px">
                                     <asp:Label ID="Label5" runat="server" Text="<%$ Resources:GlobalLanguage,BetEndTime %>"></asp:Label>
                                    </td>
                                    <td style="width: 120px">
                                        <div class="calendarContainer">
                                           <YMGS:AjaxCalendar ID="endDate" runat="server" />
                                        </div>
                                    </td>
                                    <td style="width: 20px">
                                    </td>
                                    <td align="right" colspan="2">
                                        <asp:Button runat="server" ID="Button1" Width="70px" Text="<%$ Resources:GlobalLanguage,Query %>" CausesValidation="false"
                                            CssClass="Button" OnClick="btnSearch_Click" />
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                    </td>
                </tr>
            </table>
            <table class="NoBorderTable" width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="gdvMain" runat="server" AutoGenerateColumns="false" GridLines="None"
                            OnRowDataBound="gridData_RowDataBound" CssClass="GridView" Width="100%" DataKeyNames="BETTYPE,BETID,MATCH_ID"
                            EmptyDataText="<%$ Resources:GlobalLanguage,mytradecondition %>">
                            <EmptyDataRowStyle HorizontalAlign="Left" />
                            <Columns>
                                <asp:BoundField DataField="BETTYPE" HeaderText="<%$ Resources:GlobalLanguage,bettype %>" />
                                <asp:BoundField DataField="MATCH_NAME" HeaderText="<%$ Resources:GlobalLanguage,matchname %>" />
                                <asp:BoundField DataField="MARKET_NAME" HeaderText="<%$ Resources:GlobalLanguage,Selections %>" />
                                <asp:BoundField DataField="BET_TYPE_NAME" HeaderText="<%$ Resources:GlobalLanguage,bettype %>" />
                                <asp:BoundField DataField="Market_Tmp_Type" HeaderText="<%$ Resources:GlobalLanguage,HalfFull %>" />
                                <asp:BoundField DataField="ODDS" HeaderText="<%$ Resources:GlobalLanguage,odds %>" />
                                <asp:BoundField DataField="BET_AMOUNTS" HeaderText="<%$ Resources:GlobalLanguage,Betamount %>" />
                                <asp:BoundField DataField="MATCH_AMOUNTS" HeaderText="<%$ Resources:GlobalLanguage,LeftAmount %>" />
                                <asp:BoundField DataField="TRADE_TIME" HeaderText="<%$ Resources:GlobalLanguage,TradeDate %>" ItemStyle-Width="50px" />
                                <asp:BoundField DataField="TRADE_USER" HeaderText="<%$ Resources:GlobalLanguage,Trader %>" />
                                <asp:BoundField DataField="STATUS" HeaderText="<%$ Resources:GlobalLanguage,Betstatus %>" />
                                <asp:BoundField DataField="TRADE_FUND" HeaderText="<%$ Resources:GlobalLanguage,WinOrLose %>" ItemStyle-Width="60px" />
                                <asp:TemplateField HeaderText="<%$ Resources:GlobalLanguage,BetScore %>" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentScore" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:GlobalLanguage,EventsColumn %>" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:LinkButton OnClick="btnDetail_Click" ID="btnDetail" CommandArgument='<%# Eval("BETTYPE")+","+Eval("BETID")+","+Eval("MATCH_ID")+","+Eval("MATCH_TYPE")+","+Eval("MATCH_NAME")+","+Eval("MARKET_NAME")+","+Eval("BET_AMOUNTS") %>'
                                            runat="server" Text="<%$ Resources:GlobalLanguage,MatchedBets %>" CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="<%$ Resources:GlobalLanguage,Cancel %>" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:LinkButton OnClick="btnCancel_Click" ID="btnCancel" CommandArgument='<%# Eval("BETTYPE")+","+Eval("BETID")+","+Eval("MATCH_ID")+","+Eval("MATCH_TYPE") %>'
                                            runat="server" Text="<%$ Resources:GlobalLanguage,Cancel %>" CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
 </Columns>
                        </asp:GridView>
                        <YMGS:PageNavigator ID="PageNavigator1" PageSize="20" runat="server" OnPageIndexChanged="PageNavigator1_PageIndexChanged" />
                    </td>
                </tr>
            </table>
             <asp:Button ID="btnFake" runat="server" Style="display: none" />
            <asp:Panel ID="pnlPopup" runat="server" Style=" display: none"
                CssClass="ModalPoup">
                <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
                    <span>
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:GlobalLanguage,Detail %>"></asp:Label></span>
                </asp:Panel>
                <table class="NoBorderTable" width="100%">
                <tr>
                        <td style="height: 10px;">
                            <asp:Label ID="lblmatch" runat="server" Text=""></asp:Label>
                            
                              
                        </td>
                        <td> <asp:Label ID="lblmarket" runat="server" Text=""></asp:Label></td>
                         <td><asp:Label ID="lblbettatal" runat="server" Text="0"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 10px;">
                          <asp:GridView ID="gdvsubmain" runat="server" AutoGenerateColumns="false" GridLines="None"
                         CssClass="GridView" Width="100%"  EmptyDataText="<%$ Resources:GlobalLanguage,NoRecord %>">
                            <EmptyDataRowStyle HorizontalAlign="Left" />
                            <Columns>
                                <asp:BoundField DataField="DEAL_TIME" HeaderText="<%$ Resources:GlobalLanguage,TradeDate %>" />
                                <asp:BoundField DataField="ODDS" HeaderText="<%$ Resources:GlobalLanguage,odds %>" />
                                <asp:BoundField DataField="DEAL_AMOUNT" HeaderText="<%$ Resources:GlobalLanguage,TradeAmount %>" />
                            </Columns>
                        </asp:GridView>
                        </td>
                    </tr>
                </table>
              
                <table class="NoBorderTable" width="100%">
                    <tr>
                        <td align="center">
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="OK" Width="70px" CausesValidation="false"
                                CssClass="Button" />
                        </td>
                    </tr>
                </table>
                <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" BehaviorID="mdlPopup"
                    TargetControlID="btnFake" PopupControlID="pnlPopup" BackgroundCssClass="ModalPopupBackground"
                    CancelControlID="btnCancel" PopupDragHandleControlID="pnlPopupHeader">
                </ajaxToolkit:ModalPopupExtender>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
