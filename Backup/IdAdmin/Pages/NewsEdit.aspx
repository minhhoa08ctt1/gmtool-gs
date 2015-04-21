<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" ValidateRequest="false" 
        CodeBehind="NewsEdit.aspx.cs" Inherits="IDAdmin.Pages.NewsEdit" %>
        
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    Quản lý bài viết
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server"> 
    <script src="../script/tiny_mce/tiny_mce.js" type="text/javascript"></script>
    <script type="text/javascript">
        tinyMCE.init({
            // General options
            mode: "textareas",
            theme: "advanced", 
            // Theme options
            theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,cut,copy,paste,pastetext,pasteword,|,justifyleft,justifycenter,justifyright,justifyfull,|,formatselect,fontselect,fontsizeselect",
            theme_advanced_buttons2: "bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,code,|,forecolor,backcolor,hr,removeformat,visualaid,sub,sup",              
            theme_advanced_buttons3: "",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left"
    });
    </script>   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="page_title">
        SOẠN BÀI VIẾT
    </div>
    <div class="page_content">        
        Tiêu đề: <asp:Label ID="labelTitleMsg" runat="server" ForeColor="#FF0000" Text="" />
        <br />
        <asp:TextBox ID="txtTitle" runat="server" Width="100%" CssClass="textbox" /><br />
        Nội dung: <asp:Label ID="labelContentMsg" runat="server" ForeColor="#FF0000" Text="" />
        <br />
        <asp:TextBox ID="txtContent" runat="server" Width="100%" Height="450px" TextMode="MultiLine" />
        <br />
        Phân loại bài viết:
        <asp:DropDownList ID="cmbCategory" runat="server" Width="250px">
            <asp:ListItem Value="0" Text="Không phân loại" />
            <asp:ListItem Value="1" Text="Tin tức" />
            <asp:ListItem Value="2" Text="Hướng dẫn" />
        </asp:DropDownList>
        Bài viết dành cho Game: <asp:DropDownList ID="cmbGame" runat="server" Width="300px">
        </asp:DropDownList>
        <asp:Button ID="buttonSave" runat="server" Text="   Lưu bài viết   " CssClass="button" />             
    </div>
</asp:Content>
