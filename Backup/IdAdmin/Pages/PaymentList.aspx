<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" 
        CodeBehind="PaymentList.aspx.cs" Inherits="IDAdmin.Pages.PaymentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    Hóa đơn nhập vàng
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="page_title">
        DANH SÁCH HÓA ĐƠN NẠP TIỀN CHƯA ĐƯỢC DUYỆT
    </div>
    <div class="page_content">
        <asp:Panel ID="panelList" runat="server">
        </asp:Panel>
    </div>
</asp:Content>