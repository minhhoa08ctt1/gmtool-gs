<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ServerEdit.aspx.cs" Inherits="IDAdmin.Pages.ServerEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý game server
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        Quản lý Server    
    </div>    
    <div class="page_content">
        <fieldset>
            <legend>Thông tin máy chủ game</legend>
            <table style="width:80%; margin:0 auto">
                <tr>
                    <td colspan="2">
                        <b>Thông tin về Server</b>
                    </td>
                </tr>
                <tr>
                    <td style="width:20%">
                        Server ID:
                    </td>
                    <td style="width:80%">
                        <asp:TextBox ID="txtServerID" runat="server" Width="70%" CssClass="textbox" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Server Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtServerName" runat="server" Width="70%" CssClass="textbox" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        FullName:
                    </td>
                    <td>
                        <asp:TextBox ID="txtFullName" runat="server" Width="70%" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Identity Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdentityName" runat="server" Width="70%" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Sort Order:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSortOrder" runat="server" Width="40%" CssClass="textbox"></asp:TextBox>                    
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Status:
                    </td>
                    <td>
                        <asp:TextBox ID="txtStatus" runat="server" Width="40%" CssClass="textbox"></asp:TextBox><br />
                        <b>Lưu ý:</b><br />
                        &nbsp;&nbsp;0: Server bảo trì hoặc không mở cho cộng đồng GOSU<br />
                        &nbsp;&nbsp;1: Server đang hoạt động (đối với cộng đồng GOSU)                        
                    </td>
                </tr>
                <tr>
                    <td>
                        Hot Server?:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsHot" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    <br /><br />
                    <b>DNS và định dạng các URL (tuyệt đối không thay đổi nếu không cần thiết)</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        DNS:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDNS" runat="server" Width="70%" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Gold Transfer Link:
                    </td>
                    <td>
                        <asp:TextBox ID="txtGoldTransferLink" runat="server" Width="70%" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        ID Check Link:
                    </td>
                    <td>
                        <asp:TextBox ID="txtIDCheckLink" runat="server" Width="70%" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Get Character Link:
                    </td>
                    <td>
                        <asp:TextBox ID="txtGetCharacterLink" runat="server" Width="70%" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Login Link:
                    </td>
                    <td>
                        <asp:TextBox ID="txtLoginLink" runat="server" Width="70%" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:CheckBox ID="checkAccept" runat="server" Checked="false" Text="Xác nhận đồng ý thay đổi thông tin server" />
                        <br />
                        <asp:Button ID="buttonSave" runat="server" Text="    Cập nhật   " />
                        <asp:Label ID="labelAddMessage" runat="server" ForeColor="#FF0000"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>
