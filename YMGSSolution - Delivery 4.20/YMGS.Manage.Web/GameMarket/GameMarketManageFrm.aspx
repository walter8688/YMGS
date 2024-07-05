<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true" CodeBehind="GameMarketManageFrm.aspx.cs" Inherits="YMGS.Manage.Web.GameMarket.GameMarketManageFrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="YMGS" %>
<%@ Register Src="~/Controls/AjaxCalendar.ascx" TagName="AjaxCalendar" TagPrefix="YMGS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
<script type="text/javascript">
    function ChangeBetType() {
        var betType = $get("<%= ddlEditBetType.ClientID %>");
        var divCorrectScore = $get("<%= divCorrectScore.ClientID %>");
        var divScore = $get("<%= divScore.ClientID %>");
        var divGoals = $get("<%= divGoals.ClientID %>");

        var selectIndex = betType.selectedIndex;
        if (selectIndex == 0) {
            divCorrectScore.style.display = "none";
            divGoals.style.display = "none";
            divScore.style.display = "none";
        }
        if (selectIndex == 1) {
            divCorrectScore.style.display = "";
            divGoals.style.display = "none";
            divScore.style.display = "none";
        }

        if (selectIndex == 2) {
            divCorrectScore.style.display = "none";
            divGoals.style.display = "";
            divScore.style.display = "none";
        }
        if (selectIndex == 3) {
            divCorrectScore.style.display = "none";
            divGoals.style.display = "none";
            divScore.style.display = "";
        }
    }

    function ValidateCorrectScore(source, args) {
        var betType = $get("<%= ddlEditBetType.ClientID %>");
        if (betType.selectedIndex != 1) {
            args.IsValid = true;
            return;
        }

        var bFlag1 = isDigit($("#" + "<%= txtHomeScore.ClientID %>").val());
        var bFlag2 = isDigit($("#" + "<%= txtAwayScore.ClientID %>").val());

        if (bFlag1 && bFlag2)
            args.IsValid = true;
        else
            args.IsValid = false;
    }

    function ValidateGoals(source, args) {
        var betType = $get("<%= ddlEditBetType.ClientID %>");
        if (betType.selectedIndex != 2) {
            args.IsValid = true;
            return;
        }

        if (betType.selectedIndex == 2) {
            if ($.trim($("#" + "<%= txtGoals.ClientID %>").val()) == "") {
                args.IsValid = false;
                return;
            }
        }

        var bFlag1 = IsNumber($("#" + "<%= txtGoals.ClientID %>").val());

        if (bFlag1)
            args.IsValid = true;
        else
            args.IsValid = false;
    }

    function ValidateScore(source, args) {
        var betType = $get("<%= ddlEditBetType.ClientID %>");
        if (betType.selectedIndex != 3) {
            args.IsValid = true;
            return;
        }

        var bFlag1 = false;
        var scoreAId = "<%= txtScoreA.ClientID %>";
        if ($("#" + scoreAId).val() != "") {

            bFlag1 = IsNumber($("#" + scoreAId).val());
        }
        if ($("#" + scoreAId).val() == "") {
            bFlag1 = true;
        }

        var bFlag2 = false;
        if ($("#" + "<%= txtScoreB.ClientID %>").val() != "") {
            bFlag2 = IsNumber($("#" + "<%= txtScoreB.ClientID %>").val());
        }
        if (bFlag1 && bFlag2)
            args.IsValid = true;
        else
            args.IsValid = false;
    }

    function ValidateMarketType(source, args) {
        var betType = $get("<%= ddlEditBetType.ClientID %>");
        if (betType.selectedIndex != 0) {
            var marketTmpType = $get("<%= ddlMarketTmpType.ClientID %>");
            if (marketTmpType.selectedIndex == 2)
                args.IsValid = false;
            else
                args.IsValid = true;
            return;
        }
        else {
            args.IsValid = true;
        }
    }    
</script>
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
                            <td style="width:120px">
                                <asp:DropDownList ID=ddlBetType runat=server Width=120px></asp:DropDownList>
                            </td>
                            <td style="width:20px">                                
                            </td>
                            <td style="width:70px">
                                <span>模板名称</span>
                            </td>
                            <td style="width:120px">
                                <asp:TextBox ID=txtTmpName runat=server Width=120px CssClass=TextBox MaxLength=50></asp:TextBox>
                            </td>
                            <td style="width:20px">
                            </td>
                            <td style="width:80px" align=right>
                                <span>模板分类</span>
                            </td>
                            <td>
                                <asp:DropDownList ID=ddlMarketTmpTypeQuery runat=server CssClass="DropdownList" Width=123px></asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:Button runat=server ID=btnSearch  Width=70px Text="查询" CausesValidation=false CssClass=Button OnClick="btnSearch_Click" />
                                &nbsp;<asp:Button runat=server ID=btnNew Width=70px Text = "新增" CssClass=Button  CausesValidation=false  OnClick="btnNew_Click" />
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
                GridLines="None" CssClass="GridView" Width="100%" DataKeyNames="MARKET_TMP_ID"
                OnRowCommand="gridData_RowCommand"   OnRowDataBound="gridData_RowDataBound"                                                   
                EmptyDataText="请输入合适的查询条件查询市场模板!"
                >
                <EmptyDataRowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="MARKET_TMP_NAME" HeaderText="模板名称"/>
                    <asp:BoundField DataField="MARKET_TMP_NAME_EN" HeaderText="模板英文名称"/>
                    <asp:TemplateField HeaderText="交易类型">
                        <ItemTemplate>
                            <asp:Label ID=lblBetTypeName runat=server></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="模板分类">
                        <ItemTemplate>
                            <asp:Label ID=lblMarketTmpType runat=server></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="主队得分(波胆)">
                        <ItemTemplate>
                            <asp:Label ID=lblHomeScore runat=server></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="客队得分(波胆)">
                        <ItemTemplate>
                            <asp:Label ID=lblAwayScore runat=server></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="进球数(大小球)">
                        <ItemTemplate>
                            <asp:Label ID=lblGoals runat=server></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="让球数A(让分盘)">
                        <ItemTemplate>
                            <asp:Label ID=lblScoreA runat=server></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="让球数B(让分盘)">
                        <ItemTemplate>
                            <asp:Label ID=lblScoreB runat=server></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign=Center ItemStyle-Width=60px>
                        <ItemTemplate>
                            <asp:LinkButton ID=btnEdit runat=server Text="编辑" CausesValidation=false
                             ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign=Center ItemStyle-Width=60px>
                        <ItemTemplate>
                            <asp:LinkButton ID=btnDelete runat=server Text="删除" CausesValidation=false ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <YMGS:PageNavigator ID=pageNavigator runat=server />
        </td>
    </tr>
</table>

<asp:Button ID=btnFake runat=server style="display:none" />
<asp:Panel id=pnlPopup runat=server style="width:450px;height:230px;display:none" CssClass="ModalPoup">
    <asp:Panel ID="pnlPopupHeader" runat="server" CssClass="ModalPoupHeader">
        <span>市场模板详细信息</span>
    </asp:Panel>
    <br />
    <table class=NoBorderTable width=100%>
        <tr>
            <td style="width:1px"></td>
            <td style="width:80px" align=right>
                <font color=red>*</font>
                <span>模板名称</span>
            </td>
            <td style="width:120px">
                <asp:TextBox ID=txtMarketTmpName runat=server CssClass=TextBox MaxLength=50 Width=120px></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMarketTmpName" ErrorMessage="请输入市场模板名称!"
                                            Display="None" ID="vldMarketTypeName" />
            </td>
            <td style="width:2px">
            </td>
            <td style="width:80px" align=right>
                <font color=red>*</font>
                <span>英文名称</span>
            </td>
            <td>
                <asp:TextBox ID=txtMarketTmpNameEn runat=server CssClass=TextBox MaxLength=50 Width=120px></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMarketTmpNameEn" ErrorMessage="请输入市场模板英文名称!"
                                            Display="None" ID="vldMarketTypeNameEn" />
            </td>
        </tr>
        <tr>
            <td style="width:1px"></td>
            <td style="width:80px" align=right>
                <span>交易类型</span>
            </td>
            <td>
                <asp:DropDownList ID=ddlEditBetType runat=server CssClass="DropdownList" onchange="ChangeBetType();" Width=120px></asp:DropDownList>
            </td>
            <td style="width:2px">
            </td>
            <td style="width:80px" align=right>
                <span>模板分类</span>
            </td>
            <td>
                <asp:DropDownList ID=ddlMarketTmpType runat=server CssClass="DropdownList" Width=123px></asp:DropDownList>
            </td>
        </tr>    
    </table>
    <div id="divCorrectScore" style="display:none" runat="server">
        <table class=NoBorderTable width=100%>
            <tr>
                <td style="width:1px"></td>
                <td style="width:80px;" align=right>
                    <font color=red>*</font>
                    <span>主对得分</span>
                </td>
                <td style="width:120px">
                    <asp:TextBox ID=txtHomeScore runat=server Width=120px CssClass=TextBox></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID=txtHomeScoreFilter FilterType=Numbers
                      runat=server TargetControlID="txtHomeScore" FilterMode=ValidChars />
                </td>
                <td style="width:2px"></td>
                <td style="width:80px;" align=right>
                    <font color=red>*</font>
                    <span>客队得分</span>
                </td>
                <td>
                    <asp:TextBox ID=txtAwayScore runat=server Width=120px CssClass=TextBox></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID=FilteredTextBoxExtender1 FilterType=Numbers
                      runat=server TargetControlID="txtAwayScore" FilterMode=ValidChars />
                </td>
            </tr>
        </table>
    </div>
    <div id=divGoals style="display:none" runat=server>
        <table class=NoBorderTable width=100%>
            <tr>
                <td style="width:1px"></td>
                <td style="width:80px;" align=right>
                    <font color=red>*</font>
                    <span>进球数</span>
                </td>
                <td>
                    <asp:TextBox ID=txtGoals runat=server Width=120px CssClass=TextBox></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID=FilteredTextBoxExtender2 FilterType=Numbers,Custom
                      runat=server TargetControlID="txtGoals" FilterMode=ValidChars ValidChars="." />
                </td>
            </tr>
        </table>
    </div>
    <div id=divScore style="display:none" runat=server>
        <table class=NoBorderTable width=100%>
            <tr>
                <td style="width:1px"></td>
                <td style="width:80px;" align=right>
                    <font color=red>*</font>
                    <span>让球数A</span>
                </td>
                <td style="width:120px">
                    <asp:TextBox ID=txtScoreA runat=server Width=120px CssClass=TextBox></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID=FilteredTextBoxExtender3 FilterType=Numbers,Custom
                      runat=server TargetControlID="txtScoreA" FilterMode=ValidChars ValidChars="-." />
                </td>
                <td style="width:2px"></td>
                <td style="width:80px;" align=right>
                    <font color=red>*</font>
                    <span>让球数B</span>
                </td>
                <td>
                    <asp:TextBox ID=txtScoreB runat=server Width=120px CssClass=TextBox></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID=FilteredTextBoxExtender4 FilterType=Numbers,Custom
                      runat=server TargetControlID="txtScoreB" FilterMode=ValidChars ValidChars="-."/>
                </td>
            </tr>
        </table>
    </div>
    <br />
        <asp:CustomValidator ID="vldCorrectScore" runat="server" Display=None ErrorMessage="请按照正确的数字格式输入得分!"
                 ClientValidationFunction="ValidateCorrectScore"></asp:CustomValidator>
        <asp:CustomValidator ID="vldGoals" runat="server" Display=None ErrorMessage="请按照正确的数字格式输入进球数!"
                 ClientValidationFunction="ValidateGoals"></asp:CustomValidator>
        <asp:CustomValidator ID="vldScore" runat="server" Display=None ErrorMessage="请按照正确的数字格式输入让球数!"
                         ClientValidationFunction="ValidateScore"></asp:CustomValidator>
        <asp:CustomValidator ID="vldMarketTmpType" runat="server" Display=None ErrorMessage="当前交易类型不支持半场/全场!"
                         ClientValidationFunction="ValidateMarketType"></asp:CustomValidator>
        <table class=NoBorderTable width=100%>
            <tr>
                <td style="width:20px">
                </td>
                <td style="color:Red">
                        <asp:ValidationSummary runat="server" ShowMessageBox="false" ShowSummary="true" ID="Validationsummary1" />
                </td>
            </tr>
        </table>

    <table class="NoBorderTable" width=100%>
        <tr>
            <td  align=center>
                <asp:Button ID=btnSave runat=server Text="保存" Width=70px  CausesValidation=true CssClass=Button OnClick="btnSave_Click"/>
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
