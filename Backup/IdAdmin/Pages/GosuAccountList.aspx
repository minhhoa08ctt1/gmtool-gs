<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="GosuAccountList.aspx.cs" Inherits="IDAdmin.Pages.GosuAccountList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý tài khoản nạp gosu
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        QUẢN LÝ TÀI KHOẢN NẠP GOSU
    </div>
    <div class="page_bar">
    <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>Tìm kiếm:</td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtFindValue" runat="server" Width="250px">
                    </asp:TextBox>
                </td>                
                <td>
                    <asp:Button ID="buttonExecute" runat="server" Text=" Tìm " />
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
