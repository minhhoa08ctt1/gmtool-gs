<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="GetCharInfo.aspx.cs" Inherits="IDAdmin.Pages.GetCharInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    LẤY THÔNG TIN NHÂN VẬT
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        LẤY THÔNG TIN NHÂN VẬT
    </div>
    <div class="page_content">
        <fieldset>
            <legend>Lấy thông tin nhân vật</legend>
            <span class="span_caption">Tài khoản gosu: </span>
            <span class="span_value">
                <asp:TextBox ID="gosuAccountTextBoxView15" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
            </span>
            <span class="span_caption">Số hiệu game: </span>
            <span class="span_value">
                <asp:TextBox ID="gameTypeTextBoxView15" runat="server" CssClass="textbox" Width="300px" Font-Bold="true">
                </asp:TextBox>
            </span>
            <span class="span_caption">Số hiệu cụm: </span>
            <span class="span_value">
                <asp:TextBox ID="gameZoneTextBoxView15" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
            </span>
            <span class="span_caption">&nbsp;</span>
            <span class="span_value">
                <asp:Button ID="getCharIdButtonView15" runat="server" Text="Lấy thông tin" OnClick="getCharIdButtonView15_Click" />
            </span>
            <span class="span_caption">&nbsp;</span>
            <span class="span_value">
                <asp:Label ID="messageLabelView15" runat="server" ForeColor="Red"></asp:Label>
            </span>
        </fieldset>
    </div>
</asp:Content>
