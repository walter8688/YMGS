<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MemberShipMaster.Master" AutoEventWireup="true" CodeBehind="GameMarketManageFrm.aspx.cs" Inherits="YMGS.Manage.Web.GameMarket.GameMarketManageFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">

<asp:UpdatePanel ID=updPanel runat=server UpdateMode=Conditional>
    <ContentTemplate>
<table width=100% class=NoBorderTable>
    <tr>
        <td>
            <fieldset>
                <legend>查询条件</legend>
                    <table class=NoBorderTable style="margin-top:5px;margin-bottom:5px;width:100%">
                        <tr>
                            <td style="width:20px"></td>
                            <td style="width:70px">
                                <span>交易类型</span>
                            </td>
                            <td style="width:90px">
                                <asp:DropDownList ID=ddlBetType runat=server Width=90px></asp:DropDownList>
                            </td>
                            <td style="width:20px">                                
                            </td>
                            <td style="width:70px">
                                <span>模板名称</span>
                            </td>
                            <td style="width:90px">
                                <asp:TextBox ID=txtTmpName runat=server Width=90px CssClass=TextBox></asp:TextBox>
                            </td>
                            <td style="width:20px">
                            </td>
                            <td align="right">
                                <asp:Button runat=server ID=btnSearch  Width=70px Text="查询" CssClass=Button OnClick="btnSearch_Click" />
                                &nbsp;<asp:Button runat=server ID=btnNew Width=70px Text = "新增" CssClass=Button   OnClick="btnNew_Click" />
                            </td>
                        </tr>
                    </table>
            </fieldset>
        </td>
    </tr>
</table>
<table class="NoBorderTable" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gridData" runat="server" AutoGenerateColumns="false" 
                GridLines="None" CssClass="GridView" Width="100%" DataKeyNames="BET_TYPE_ID"
                OnRowCommand="gridData_RowCommand"   OnRowDataBound="gridData_RowDataBound"                                                   
                EmptyDataText="请输入合适的查询条件查询市场模板!"
                >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="BET_TYPE_ID" HeaderText="请领单号" ItemStyle-Wrap=false />
                    <asp:BoundField DataField="BET_TYPE_NAME" HeaderText=请领人 ItemStyle-Wrap=false />                    
                    <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign=Center ItemStyle-Width=60px>
                        <ItemTemplate>
                            <asp:LinkButton ID=btnEdit runat=server Text="编辑" CommandName="Edit"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign=Center ItemStyle-Width=60px>
                        <ItemTemplate>
                            <asp:LinkButton ID=btnDelete runat=server Text="删除" CommandName="Del"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>

<asp:Button ID=btnFake runat=server style="display:none" />
<asp:Panel id=pnlPopup runat=server style="width:400px;height:300px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
        <span>市场模板详细信息</span>
    </asp:Panel>
    <table class=NoBorderTable width=100%>
        <tr>
            <td>
            this is a test page
            </td>
        </tr>
    </table>
    <br />
    <table class="NoBorderTable" width=100%>
        <tr>
            <td  align=center>
                <asp:Button ID=btnSave runat=server Text="保存" Width=70px  CausesValidation=false CssClass=Button/>
                &nbsp;
                <asp:Button ID=btnCancel runat=server Text="取消" Width=70px CausesValidation=false CssClass=Button />
            </td>
        </tr>
    </table>

                        
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" BehaviorID="mdlPopup" 
        TargetControlID="btnFake"
        PopupControlID="pnlPopup"
        BackgroundCssClass="ModalPopupBackground"                         
        CancelControlID="btnCancel" 
        PopupDragHandleControlID="pnlPopupHeader"
    ></ajaxToolkit:ModalPopupExtender>         
</asp:Panel>          
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
