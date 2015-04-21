<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ChanellingUsers.aspx.cs" Inherits="IDAdmin.Pages.ChanellingUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Tài khoản phía đối tác Chanelling
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        DANH SÁCH TÀI KHOẢN CỦA ĐỐI TÁC CHANELLING
    </div>
    <div class="page_bar">
        <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>Đối tác Chanelling:</td>
                <td>Tài khoản cần tìm</td>
                <td></td>             
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="cmbCommunity" runat="server" Width="150px"> 
                        <asp:ListItem Value="" Text="--- Tất cả ---" />                        
                        <asp:ListItem Value="soha" Text="Soha (Ming ID)" />
                        <asp:ListItem Value="vtc" Text="VTC - GO" />
                        <asp:ListItem Value="zing" Text="Zing" />
                        <asp:ListItem Value="fpt" Text="FPT" />
                        <asp:ListItem Value="fb_" Text="Facebook" />
                    </asp:DropDownList>
                </td>
                <td><asp:TextBox ID="txtUserName" runat="server" Width="200px" CssClass="textbox"></asp:TextBox></td>                
                <td>
                    <asp:Button ID="buttonExecute" runat="server" CssClass="button" Text="  Xem danh sách  " />
                </td>
            </tr>
        </table>
    </div>
    <div class="page_content">
        <asp:Panel ID="panelList" runat="server">
        </asp:Panel>
    </div>
    <div style="text-align:center;padding:5px">
        <asp:HyperLink ID="linkPrev" runat="server" Text="Trang trước" />&nbsp;|&nbsp;
        <asp:HyperLink ID="linkNext" runat="server" Text="Trang sau" />
    </div> 
</asp:Content>
