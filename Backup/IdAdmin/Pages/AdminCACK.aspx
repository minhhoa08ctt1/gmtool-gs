<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" CodeBehind="AdminCACK.aspx.cs" Inherits="IDAdmin.Pages.AdminCACK" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add Item/Add Gold/Add VIP
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function TypeChanged() {
            var selType = "#ctl00_MainContent_ddlType";
            if ($(selType).val()=="AddItem") {
                $("#ctl00_MainContent_lblName").text("Nhập ID vật phẩm/Số lượng");
            }
            else if ($(selType).val() == "AddGold") {
            $("#ctl00_MainContent_lblName").text("Nhập số vàng");
            }
            else {
                $("#ctl00_MainContent_lblName").text("Nhập số ngày");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        Add Item/Add Gold/Add VIP
    </div>    
    <div class="page_content">
        <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>Chọn thao tác:</td>
                <td><asp:DropDownList ID="ddlType" runat="server" Width="200px" onchange="TypeChanged();">                        
                        <asp:ListItem Value="AddItem" Text="Thêm Item" Selected="True" />
                        <asp:ListItem Value="AddGold" Text="Nạp vàng" />
                        <asp:ListItem Value="AddVIP" Text="Nạp danh tuấn"  />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Chọn server:</td>
                <td><asp:DropDownList ID="ddlServer" runat="server" Width="200px">                        
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Tài khoản nhận:</td>
                <td><asp:TextBox ID="txtAccount" runat="server" CssClass="textbox" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblName" runat="server" Text="Nhập ID vật phẩm/Số lượng"></asp:Label></td>
                <td><asp:TextBox ID="txtAmount" runat="server" CssClass="textbox" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Mã bảo mật:</td>
                <td><asp:TextBox ID="txtCaptcha" runat="server" CssClass="textbox" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td><img width="200px" src="Captcha.aspx" alt="" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button ID="buttonAdd" runat="server" CssClass="button" Text="Thêm" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Label ID="labelMessage" runat="server" ForeColor="#FF0000"></asp:Label></td>
            </tr>
        </table>
    </div>
</asp:Content>

