<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogOn.aspx.cs" Inherits="IDAdmin.Pages.LogOn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Đăng nhập</title>
    <link type="text/css" rel="Stylesheet" href="../css/base.css" />
</head>
<body style="background-color:#FFFFFF">
    <form id="form1" runat="server">
    <p>&nbsp;</p>
    <div class="logon">
        <div class="logon_top">
        </div>
        <div class="logon_mid">
            <div style="text-align:left;font-weight:bold">
                QUẢN TRỊ GOSU                
            </div>
            <div>
                Tên đăng nhập:</div>
            <div>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox" Width="200px" />
                <asp:RequiredFieldValidator ID="requireUserName" runat="server" ControlToValidate="txtUserName"
                    ErrorMessage="*" />
            </div>
            <div>
                Mật khẩu:</div>
            <div>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox" Width="200px" />
                <asp:RequiredFieldValidator ID="requirePassword" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="*" />
            </div>
            <div>
                <asp:Button ID="buttonLogOn" runat="server" Text=" Đăng nhập " CssClass="button" /><br />
                <asp:Label ID="labelMessage" runat="server" Text="" ForeColor="#FF0000" />
            </div>
        </div>
        <div class="logon_bottom">
        </div>
    </div>
    </form>
</body>
</html>
