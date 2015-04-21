<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ViewAdminLog.aspx.cs" Inherits="IDAdmin.Pages.ViewAdminLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Lịch sử thao tác của Quản trị
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        THEO DÕI LỊCH SỬ THAO TÁC TRÊN TRANG QUẢN TRỊ
    </div>
    <div class="page_bar" style="text-align:left;font-weight:bold">        
        <asp:Table ID="tableGrantedUser" runat="server" Width="100%" BorderWidth="0">
            <asp:TableRow>
                <asp:TableCell ID="cellGrantedUser" runat="server" Width="100%"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <div class="page_content">
        <asp:Panel ID="panelList" runat="server" Width="100%">            
        </asp:Panel>                
    </div>
    <div style="text-align:center;padding:5px">
        <asp:HyperLink ID="linkPrev" runat="server" Text="Trang trước" />&nbsp;|&nbsp;
        <asp:HyperLink ID="linkNext" runat="server" Text="Trang sau" />;
    </div> 
</asp:Content>
