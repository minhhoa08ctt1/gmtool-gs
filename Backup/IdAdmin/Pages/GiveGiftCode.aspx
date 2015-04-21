<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="GiveGiftCode.aspx.cs" Inherits="IDAdmin.Pages.GiveGiftCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Cấp phát GiftCode cho User
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Thông báo</legend>
        <h2 style="text-align:center; color:#FF0000">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>                     
        </h2>   
        <h2 style="text-align:center">
            <asp:HyperLink id="linkBack" runat="server" Text="[Quay lại]"></asp:HyperLink>
        </h2>     
    </fieldset>
</asp:Content>
