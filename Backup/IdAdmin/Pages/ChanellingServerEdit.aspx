<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ChanellingServerEdit.aspx.cs" Inherits="IDAdmin.Pages.ChanellingServerEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý server dành cho đối tác chanelling
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        QUẢN LÝ MÁY CHỦ GAME DÀNH CHO ĐỐI TÁC CHANELLING
    </div>
    <div class="page_bar">
    </div>
    <div class="page_content">        
        <fieldset>
            <legend>Thông tin máy chủ Chanelling</legend>
            <table cellpadding="2" cellspacing="2">
                <tr>
                    <td width="40%">Đối tác Chanelling:</td>
                    <td with="60%">
                        <asp:Label ID="labelPartnerName" runat="server" Width="400px" BorderStyle="Solid" BorderWidth="1px" BorderColor="#B5D0E7"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="30%">Game:</td>
                    <td with="70%">
                        <asp:Label ID="labelGame" runat="server" Width="400px" BorderStyle="Solid" BorderWidth="1px" BorderColor="#B5D0E7"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="30%">Máy chủ:</td>
                    <td with="70%">
                        <asp:Label ID="labelServer" runat="server" Width="400px" BorderStyle="Solid" BorderWidth="1px" BorderColor="#B5D0E7"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="30%">Chức năng vào game:</td>
                    <td with="70%">
                        <asp:RadioButton ID="optPlayEnabled" runat="server" GroupName="PlayEnabled" Text="Mở" />&nbsp;
                        <asp:RadioButton ID="optPlayDisabled" runat="server" GroupName="PlayEnabled" Text="Đóng" />
                    </td>
                </tr>
                <tr>
                    <td width="30%">Chức năng nạp tiền:</td>
                    <td with="70%">
                        <asp:RadioButton ID="optTopupEnabled" runat="server" GroupName="TopupEnabled" Text="Mở" />&nbsp;
                        <asp:RadioButton ID="optTopupDisabled" runat="server" GroupName="TopupEnabled" Text="Đóng" />
                    </td>
                </tr>
                <tr>
                    <td width="30%">Ghi chú:</td>
                    <td with="70%">
                        <asp:TextBox ID="txtNotes" runat="server" Width="400px" BorderStyle="Solid" BorderWidth="1px" BorderColor="#B5D0E7"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="30%">&nbsp;</td>
                    <td with="70%">
                        <asp:Button ID="buttonUpdate" runat="server" Text=" Lưu thông tin " OnClick="buttonUpdate_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    
    </div>    
</asp:Content>
