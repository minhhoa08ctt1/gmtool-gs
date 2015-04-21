<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_Test.aspx.cs" Inherits="IDAdmin.Pages._Test" EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../script/jquery-1.4.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    Chọn server cần chuyển:<br />
    <asp:DropDownList ID="DropDownList1" runat="server" Width="327px">
        <asp:ListItem Value="S1" Text="Xích Bích"></asp:ListItem>
        <asp:ListItem Value="S2" Text="Hoa Dung"></asp:ListItem>        
    </asp:DropDownList>
    <br />
    <br />
    Hình thức chuyển:<br />
    <asp:RadioButton ID="RadioButton1" runat="server" Text="Giữ nguyên tài khoản" 
        Checked="True" />
    <br />
    <asp:RadioButton ID="RadioButton2" runat="server" Text="Chuyển sang tài khoản mới: " />&nbsp;
    <asp:TextBox ID="TextBox1" runat="server" Width="190px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Đăng ký chuyển server" />
    
        
    </form>
</body>
</html>
