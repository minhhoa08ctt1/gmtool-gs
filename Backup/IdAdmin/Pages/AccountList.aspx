<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="AccountList.aspx.cs" Inherits="IDAdmin.Pages.AccountList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý tài khoản
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        QUẢN LÝ TÀI KHOẢN
    </div>
    <div class="page_bar">
    <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>Nhóm/vai trò:</td>
                <td>Trạng thái:</td>
                <td>Tìm kiếm:</td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="cmbLevel" runat="server" Width="200px"> 
                        <asp:ListItem Value="-1" Text="Tất cả" />
                        <asp:ListItem Value="0" Text="Tài khoản thường" />
                        <asp:ListItem Value="10" Text="Đối tác" />
                        <asp:ListItem Value="50" Text="Chăm sóc khách hàng" />
                        <asp:ListItem Value="100" Text="Cộng tác viên" />
                        <asp:ListItem Value="1000" Text="Quản trị" />
                        <asp:ListItem Value="10000" Text="Quản trị cao cấp" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="cmbStatus" runat="server" Width="150px">
                        <asp:ListItem Value="" Text="Tất cả" />
                        <asp:ListItem Value="-1" Text="Bị khóa" />
                        <asp:ListItem Value="0" Text="Chưa kích hoạt" />
                        <asp:ListItem Value="1" Text="Đã kích hoạt" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtFindValue" runat="server" Width="250px">
                    </asp:TextBox>
                </td>                
                <td>
                    <asp:Button ID="buttonExecute" runat="server" Text=" Hiển thị " />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="page_content">
        <asp:Panel ID="panelList" runat="server" Width="100%">
        </asp:Panel>
    </div>
    <div class="page_navigatepage">
        <asp:HyperLink ID="linkFirst" runat="server" Text="Về đầu" />&nbsp;|&nbsp;
        <asp:HyperLink ID="linkPrev" runat="server" Text="Trang trước" />&nbsp;|&nbsp;
        <asp:HyperLink ID="linkNext" runat="server" Text="Trang sau" />
    </div>
</asp:Content>
