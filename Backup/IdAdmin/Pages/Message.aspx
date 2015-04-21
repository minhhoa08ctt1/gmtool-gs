<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master"
    CodeBehind="Message.aspx.cs" Inherits="IDAdmin.Pages.Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    Thông báo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="page_content">
            <fieldset>
                <legend>Thông báo</legend>
                <h2 style="text-align:center; color:#FF0000">
                    Bạn không có quyền sử dụng chức năng này!                     
                </h2>                
            </fieldset>
        </div>
</asp:Content>
