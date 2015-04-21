<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master"
    CodeBehind="PaymentAdd.aspx.cs" Inherits="IDAdmin.Pages.PaymentAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Lập hóa đơn nạp tiền vào game
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page_title">
        LẬP HÓA ĐƠN NẠP TIỀN TRỰC TIẾP VÀO GAME
    </div>
    <div class="page_content">
        <fieldset>
            <legend>Thông tin hóa đơn nạp tiền</legend>
                <span class="span_caption">Tài khoản nhận tiền:</span> 
                <span class="span_value">
                    <asp:TextBox ID="txtAccount" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requireAccount" runat="server" ControlToValidate="txtAccount" ErrorMessage="*"></asp:RequiredFieldValidator>
                </span>
                <span class="span_caption">Server nhận tiền:</span> 
                <span class="span_value">
                    <asp:DropDownList ID="cmbServer" runat="server" Width="300px">
                    </asp:DropDownList>                    
                </span>
                <span class="span_caption">Số tiền khách hàng thanh toán:</span> 
                <span class="span_value">
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validAmount" runat="server"
                                ErrorMessage = "Số tiền không hợp lệ"
                                ControlToValidate = "txtAmount"
                                ValidationExpression = "^\d+$"></asp:RegularExpressionValidator>
                </span>
                <span class="span_caption">Số tiền được nạp vào game (đã cộng thêm %):</span> 
                <span class="span_value">
                    <asp:TextBox ID="txtCardLogAmount" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validCardLogAmout" runat="server"
                                ErrorMessage = "Số tiền không hợp lệ"
                                ControlToValidate = "txtCardLogAmount"
                                ValidationExpression = "^\d+$"></asp:RegularExpressionValidator>
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
                    <asp:Button ID="buttonAccept" runat="server" CssClass="button" Text="  Chấp nhận " />
                    <asp:Button ID="buttonCancel" runat="server" CssClass="button" Text="  Hủy bỏ " />                    
                </span>
                <span class="span_caption">&nbsp;</span> 
                <span class="span_value">
                    <asp:Label ID="labelMessage" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp;                    
                </span>
        </fieldset>
    </div>
</asp:Content>
