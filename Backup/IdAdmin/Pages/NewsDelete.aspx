<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master"
        CodeBehind="NewsDelete.aspx.cs" Inherits="IDAdmin.Pages.NewsDelete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    Xóa bài viết
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="panelMessage" runat="server">
        <div class="page_content">
            <fieldset>
                <legend>Thông báo</legend>
                <h2 style="text-align:center; color:#FF0000">
                    Bạn không có quyền xóa bài viết!                     
                </h2>
                <p style="text-align:center">
                    <asp:HyperLink ID="linkBack" runat="server" Text=" Quay về trang trước "></asp:HyperLink>
                </p>
            </fieldset>
        </div>
    </asp:Panel>
</asp:Content>
