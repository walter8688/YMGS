<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/BetBase.Master" AutoEventWireup="true"
    CodeBehind="Main.aspx.cs" Inherits="YMGS.Manage.Web.testmaster" %>

<%@ Register Src="Controls/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ListPlace" runat="server">
  <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="姓名："></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                    <asp:ListItem Text="a" Value="1"></asp:ListItem>
                    <asp:ListItem  Text="b" Value="2"></asp:ListItem>
                    <asp:ListItem  Text="c" Value="3"></asp:ListItem>
                    <asp:ListItem  Text="d" Value="4"></asp:ListItem>
                    <asp:ListItem  Text="e" Value="5"></asp:ListItem>
                    <asp:ListItem  Text="f" Value="6"></asp:ListItem>
                </asp:CheckBoxList>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                    <asp:ListItem  Text="f" Value="3"></asp:ListItem>
                    <asp:ListItem  Text="g" Value="4"></asp:ListItem>
                    <asp:ListItem  Text="e" Value="5"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>  
    <table> <tr><td align="right">
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></td></tr>
        <tr>
            <td align="right"> <asp:Button ID="btnAdd" runat="server" Text="新建" />
            </td>
        </tr>
        <tr>
            <td> <asp:GridView ID="gdvMain" runat="server" HeaderStyle-Height="25px" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="ROLE_FUNC_MAP_ID" HeaderText="ID" />
            <asp:BoundField DataField="ROLE_ID" HeaderText="角色ID" />
            <asp:BoundField DataField="FUNC_ID" HeaderText="功能点ID" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtupdate" runat="server">编辑</asp:LinkButton>
                    &nbsp;|&nbsp;
                    <asp:LinkButton ID="lbtDel" runat="server">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="bgtd" />
        <RowStyle CssClass="bgtd1" />
    </asp:GridView>
            </td>
            
        </tr>
       
    </table>
     <uc1:PageNavigator ID="PageNavigator1" PageSize="20" runat="server" OnPageIndexChanged="PageNavigator1_PageIndexChanged" />
   
</asp:Content>

