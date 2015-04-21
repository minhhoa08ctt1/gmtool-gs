<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="EditAccount.aspx.cs" Inherits="IDAdmin.Pages.EditAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CHỈNH SỬA TÀI KHOẢN
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        CHỈNH SỬA TÀI KHOẢN
    </div>
    <div class="page_content">
        <fieldset>
            <legend>CHỈNH SỬA TÀI KHOẢN</legend>
            <span class="span_caption">Mật khẩu: </span>
            <span class="span_value">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" Width="300px" Font-Bold="true" TextMode="Password"></asp:TextBox>
            </span>
            <span class="span_caption">Nhập lại mật khẩu: </span><span class="span_value">
                <asp:TextBox ID="txtReTypePassword" runat="server" CssClass="textbox" Width="300px" Font-Bold="true" TextMode="Password"></asp:TextBox>
            </span>
            <span class="span_caption">Email: </span><span class="span_value">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
            </span>
            <span class="span_caption">&nbsp;</span>
            <span class="span_value">
                <asp:CheckBox ID="checkAccept" runat="server" Text="Đã kiểm tra và chấp nhận chỉnh sửa" />
                <asp:Label ID="labelCheckMessage" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
            </span>
            <span class="span_caption">&nbsp;</span>
            <span class="span_value">
                <asp:Button ID="buttonSave" runat="server" Text="Lưu" OnClick="buttonSave_Click" />
            </span>
            <span class="span_caption">&nbsp;</span>
            <span class="span_value">
                <asp:Label ID="messageLabel" runat="server" ForeColor="Red"></asp:Label>
            </span>
        </fieldset>
    </div>
</asp:Content>
