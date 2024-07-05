<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SimpleMaster.Master"
    AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="YMGS.Trade.Web.MemberShip.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <table width=500px>
        <tr>
            <td >
                <asp:Label ID="Label1" Font-Bold="True" runat="server" Text="你的个人资料："></asp:Label>
            </td>
            <td align="right">
                <asp:Label ID="Label2" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                    ID="Label3" runat="server" Text="表示必填项"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <table width=350px>
                    <tr>
                        <td align=right width=80px> <asp:Label ID="Label5" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                    ID="Label6" runat="server" Text="姓名"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtName" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                     <tr valign="top">
                        <td align=right><asp:Label
                    ID="Label7" runat="server" Text="出生年月"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlyear" runat="server">
                            </asp:DropDownList><asp:Label
                    ID="Label22" runat="server" Text="年"></asp:Label>
                                <asp:DropDownList ID="ddlmonth" runat="server" Height="16px" Width="63px">
                                    <asp:ListItem>请选择</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                            </asp:DropDownList><asp:Label
                    ID="Label23" runat="server" Text="月"></asp:Label>
                                <asp:DropDownList ID="ddlday" runat="server" Width="61px">
                                    <asp:ListItem>请选择</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>17</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>21</asp:ListItem>
                                    <asp:ListItem>22</asp:ListItem>
                                    <asp:ListItem>23</asp:ListItem>
                                    <asp:ListItem>24</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>26</asp:ListItem>
                                    <asp:ListItem>27</asp:ListItem>
                                    <asp:ListItem>28</asp:ListItem>
                                    <asp:ListItem>29</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>31</asp:ListItem>
                            </asp:DropDownList><asp:Label
                    ID="Label24" runat="server" Text="日"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td align=right><asp:Label ID="Label8" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                    ID="Label9" runat="server" Text="邮箱"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtemail" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td align=right><asp:Label
                    ID="Label10" runat="server" Text="所在地"></asp:Label>
                        </td>
                        <td align="left"><asp:DropDownList ID="ddlshen" runat="server">
                            </asp:DropDownList>
                                <asp:DropDownList ID="ddlshi" runat="server">
                            </asp:DropDownList>
                                <asp:DropDownList ID="ddlhu" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" Width="100%" 
                               Text="详细地址..." Height="48px"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td align="right"><asp:Label
                    ID="Label11" runat="server" Text="邮政编码："></asp:Label>
                        </td>
                        <td align=left>
                            <asp:TextBox ID="txtpostcode" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td align=right><asp:Label
                    ID="Label12" runat="server" Text="联系电话："></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txttel" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label4" Font-Bold="True" runat="server" Text="必发必填信息:"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <table  width=350px>
                    <tr>
                        <td align=right width=80px><asp:Label ID="Label13" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                    ID="Label14" runat="server" Text="用户名"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtName0" runat="server" Width=100%></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align=right><asp:Label ID="Label15" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                    ID="Label16" runat="server" Text="密码"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtName1" runat="server" TextMode="Password" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align=right><asp:Label ID="Label17" ForeColor="Red" runat="server" Text="*"></asp:Label><asp:Label
                    ID="Label18" runat="server" Text="确认密码"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtName2" runat="server" TextMode="Password" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align=right><asp:Label
                    ID="Label20" runat="server" Text="安全提问"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="100%">
                                <asp:ListItem Value="1">你父亲的名字</asp:ListItem>
                                <asp:ListItem Value="2">你母亲的名字</asp:ListItem>
                                <asp:ListItem Value="3">你喜欢的歌手</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align=right><asp:Label
                    ID="Label19" runat="server" Text="答案"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtName3" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align=right><asp:Label
                    ID="Label21" runat="server" Text="验证码"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtvalidatecode" runat="server" Width="65px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                          
                            <asp:CheckBox ID="ckbagree" runat="server" 
                                oncheckedchanged="CheckBox1_CheckedChanged" Text="同意必发必" />
                            <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="#3333FF">用户协议使用条款</asp:HyperLink>
                          
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnRegiester" runat="server" Text="立即注册" />
            </td>
        </tr>
    </table>
</asp:Content>
