<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Command.aspx.cs" Inherits="IDAdmin.Pages.Command" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <fieldset>
            <legend>Đóng/mở hình thức nạp thẻ:</legend>             
            Mã hình thức nạp thẻ: <asp:TextBox ID="txtPaymentType" runat="server"></asp:TextBox> 
            Trạng thái (0: đóng/1: mở): <asp:TextBox ID="txtStatus" runat="server"></asp:TextBox>
            <asp:Button ID="buttonPaymentType" runat="server" Text=" Thay đổi " OnClick="buttonPaymentType_Click" />
        </fieldset>
        <br />
    </div>
    </form>
</body>
</html>
