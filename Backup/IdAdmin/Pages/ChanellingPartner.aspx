<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ChanellingPartner.aspx.cs" Inherits="IDAdmin.Pages.ChanellingPartner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        CHỌN ĐỐI TÁC CHANELLING CẦN QUẢN LÝ SERVER
    </div>
     <div class="page_bar" style="text-align:right">
        <a href="ServerList.aspx">[Quay lại danh sách Server]</a>
    </div>
    <div class="page_content">   
        <asp:Panel ID="panelList" runat="server" Width="100%">
        </asp:Panel>
    </div>
</asp:Content>
