<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="OAuthApplication.aspx.cs" Inherits="IDAdmin.Pages.OAuthApplication" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý ứng dụng client sử dụng OAuth
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
         Quản lý ứng dụng client sử dụng OAuth
    </div>
    <div class="page_bar">
        Tìm kiếm:&nbsp;
        <asp:TextBox ID="txtSearchValue" runat="server" Width="250px">
        </asp:TextBox>&nbsp;
        <asp:Button ID="buttonExecute" runat="server" Text=" Hiển thị " />    
    </div>
    <div class="page_bar" style="text-align:right">
        <a href="OAuthApplication_Edit.aspx?action=add">[Bổ sung ứng dụng]</a>
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
