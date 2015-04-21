<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="AddGosu.aspx.cs" Inherits="IDAdmin.Pages.AddGosu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        NẠP GOSU TRỰC TIẾP CHO TÀI KHOẢN KHÁCH HÀNG
    </div>
    <div class="page_content">
        <fieldset>
            <legend>Nạp GOSU</legend>
            <span class="span_caption">Tài khoản nhận tiền:</span> 
            <span class="span_value">
                <asp:TextBox ID="txtAccount" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>                    
            </span>
            <span class="span_caption">Số tiền (tính theo VNĐ):</span> 
            <span class="span_value">
                <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>                    
            </span>
            <span class="span_caption">Lý do nạp tiền:</span> 
            <span class="span_value">
                <asp:TextBox ID="txtMsg" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>                    
            </span>
            <span class="span_caption">Hình thức nạp tiền:</span> 
            <span class="span_value">
                <asp:DropDownList ID="cmbType" runat="server" CssClass="textbox" Width="300px">
                    <asp:ListItem Value="">--- Chọn hình thức nạp GOSU ---</asp:ListItem>
                    <asp:ListItem Value="TIENMAT">Tiền mặt (nhận GOSU)</asp:ListItem>
                    <asp:ListItem Value="TANGGOSU">Tặng GOSU (nhận GOSU tặng)</asp:ListItem>
                </asp:DropDownList>
            </span>
            <span class="span_caption">Mã bảo mật:</span> 
                <span class="span_value">
                    <asp:TextBox ID="txtCaptcha" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
                </span>
                <span class="span_caption">&nbsp;</span> 
                <span class="span_value">
                    <img src="Captcha.aspx" alt="" />&nbsp;
                </span>
                <span class="span_caption">&nbsp;</span> 
                <span class="span_value">
                    <asp:Button ID="buttonAccept" runat="server" CssClass="button" Text="  Chấp nhận " />&nbsp;
                    <asp:Button ID="buttonCancel" runat="server" CssClass="button" Text="  Hủy bỏ " />&nbsp;
                    <asp:Button ID="buttonRechargeList" runat="server" CssClass="button" Text="  Nạp GOSU từ list " />                    
                </span>
                <span class="span_caption">&nbsp;</span> 
                <span class="span_value">
                    <asp:Label ID="labelMessage" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp;                    
                </span>
        </fieldset>
    </div>
</asp:Content>
