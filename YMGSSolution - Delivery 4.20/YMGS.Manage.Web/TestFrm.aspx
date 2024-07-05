<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestFrm.aspx.cs" Inherits="YMGS.Manage.Web.TestFrm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding=0 cellspacing=0 border=0 width=100%>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width:60px">Names</td>
                            <td style="width:10px"></td>
                            <td><asp:TextBox ID=txtNames runat=server Width=100px></asp:TextBox></td>
                            <td style="width:60px">Description</td>
                            <td style="width:10px"></td>
                            <td><asp:TextBox ID=txtDescription runat=server Width=100px></asp:TextBox></td>
                            <td>
                                <asp:Button ID=btnAdd runat=server Text="Add1" onclick="btnAdd_Click" />
                                <asp:Button ID=btnAdd0 runat=server Text="Add2" onclick="btnAdd0_Click" />
                                <asp:Button ID=btnAdd1 runat=server Text="Add Failed with trans" 
                                    onclick="btnAdd1_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnQuery" runat="server" onclick="btnQuery_Click" 
                        Text="Query1" />
                    <asp:Button ID="btnQuery0" runat="server" onclick="btnQuery0_Click" 
                        Text="Query2" />
                                <asp:Button ID=btnAdd2 runat=server Text="Test Business Rule Success" 
                                    onclick="btnAdd2_Click" />
                                <asp:Button ID=btnAdd3 runat=server Text="Test Business Rule Failed" 
                                    onclick="btnAdd3_Click" />
                                <asp:Button ID=btnLog runat=server Text="Test Log" 
                        onclick="btnLog_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
