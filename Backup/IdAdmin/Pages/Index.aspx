<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" Inherits="IDAdmin.Pages.Index" Codebehind="Index.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    Quản trị GOSU - Trang chủ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <p style="text-align:center; font-size:24px; font-weight:bolder">
        &nbsp;
    </p>
    <fieldset style="margin:0 auto; width:350px;padding:10px;font-size:12px;font-weight:normal; text-align:center">       
        <legend>Chọn dịch vụ/game cần quản trị</legend>
        <br />        
        <br />
        <asp:DropDownList ID="cmbGame" runat="server" Width="300px">            
        </asp:DropDownList>
        <br /><br />
        <asp:Button ID="buttonOK" runat="server" CssClass="button" Text="   Chấp nhận   " OnClick="buttonOK_Click" />
        <br /><br />
    </fieldset>    
</asp:Content>

