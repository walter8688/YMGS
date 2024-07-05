<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="YMGS.Manage.Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录</title>
    <link href="css/logincss.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="login">
            <table>
                <tr>
                    <td>
                        <span class="sp1">用户名：</span> <span class="sp2">
                            <asp:TextBox ID="txtUserId" runat="server" class="textlogin" MaxLength="20"></asp:TextBox>
                        </span>
                    </td>
                    <td  valign=bottom>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="" 
                            ControlToValidate="txtUserId" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="sp1">密码：</span> <span class="sp2">

                            <asp:TextBox ID="txtPsw" runat="server" class="textlogin" MaxLength="200" TextMode="Password"></asp:TextBox>
                        </span>
                    </td>
                    <td valign="bottom">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage=""
                            ControlToValidate="txtPsw" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
           
            
            <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
           
            
            <div class="loginc">
                <asp:ImageButton ID="btnLogin" runat="server"
                    ImageUrl="Images/loginbtn.png" onclick="btnLogin_Click" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
