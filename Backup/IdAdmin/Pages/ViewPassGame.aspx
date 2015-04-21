<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" CodeBehind="ViewPassGame.aspx.cs" Inherits="IDAdmin.Pages.ViewPassGame" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Xem password game
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        XEM PASSWORD GAME
    </div>    
    <div class="page_bar">
        <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>Username</td>
                <td><asp:TextBox ID="txtUserName" runat="server" Width="150px" class="textbox" /></td>
                <td><asp:Button ID="buttonExecute" runat="server" CssClass="button" Text="  Xem password  " /></td>
                <td><asp:Label ID="txtPassword" runat="server" Text="" Font-Bold="true" ForeColor="Red" Font-Size="Larger"></asp:Label></td>
            </tr>
        </table>
    </div>
</asp:Content>

