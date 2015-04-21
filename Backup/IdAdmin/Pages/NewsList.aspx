<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master"
    CodeBehind="NewsList.aspx.cs" Inherits="IDAdmin.Pages.NewsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
   Quản lý bài viết
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="page_title">
        QUẢN LÝ BÀI VIẾT
    </div>
    <div class="page_bar" style="text-align:right">
        <a href="NewsEdit.aspx">[Viết bài mới]</a>
    </div>
    <div class="page_content">
        <asp:Panel ID="panelList" runat="server">        
        </asp:Panel>
    </div>
    <div class="page_navigatepage">        
        <asp:HyperLink ID="linkPrev" runat="server" Text="Trang trước" />&nbsp;|&nbsp;
        <asp:HyperLink ID="linkNext" runat="server" Text="Trang sau" />
    </div> 
</asp:Content>
