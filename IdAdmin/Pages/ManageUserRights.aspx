<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" 
    CodeBehind="ManageUserRights.aspx.cs" Inherits="IDAdmin.Pages.ManageUserRights" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    QUẢN LÝ, CẤP PHÁT QUYỀN CHO NGƯỜI DÙNG
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="page_title">
        QUẢN LÝ, CẤP PHÁT QUYỀN CHO NGƯỜI DÙNG
    </div>
    <div class="page_bar">
        Tài khoản:
        <asp:TextBox ID="txtUserName" runat="server" Width="300px" CssClass="textbox"></asp:TextBox>
        <asp:Button ID="buttonExecute" runat="server" Text = " Hiển thị " CssClass="button" />        
    </div>
    <div class="page_bar" style="text-align:left;font-weight:bold">
        <asp:Table ID="tableGrantedUser" runat="server" Width="100%" BorderWidth="0">
            <asp:TableRow>
                <asp:TableCell ID="cellGrantedUser" runat="server" Width="100%" Wrap="true"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <br />
    <div class="page_content">
        <asp:Panel ID="panelList" runat="server" Width="100%">            
        </asp:Panel>                
    </div>
</asp:Content>
