<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" 
    CodeBehind="PaymentAccept.aspx.cs" Inherits="IDAdmin.Pages.PaymentAccept" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    Duyệt nạp tiền
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="page_title">
        XỬ LÝ HÓA ĐƠN NẠP TIỀN
    </div>
    <div class="page_content">
        <fieldset>
            <legend>Hóa đơn nạp tiền vào game</legend>
            <span class="span_caption">Số hóa đơn: </span>
            <span class="span_value">
                <asp:TextBox ID="txtBillID" runat="server" CssClass="textbox" Width="300px" ReadOnly="true" Font-Bold="true"></asp:TextBox>
            </span>
            <span class="span_caption">Thời điểm lập hóa đơn: </span>
            <span class="span_value">
                <asp:TextBox ID="txtCreatedTime" runat="server" CssClass="textbox" Width="300px" ReadOnly="true" Font-Bold="true"></asp:TextBox>
            </span>
            <span class="span_caption">Người lập hóa đơn: </span>
            <span class="span_value">
                <asp:TextBox ID="txtCreatedUserID" runat="server" CssClass="textbox" Width="300px" ReadOnly="true" Font-Bold="true"></asp:TextBox>
            </span>
            <span class="span_caption">Tài khoản được nạp tiền: </span>
            <span class="span_value">
                <asp:TextBox ID="txtAccount" runat="server" CssClass="textbox" Width="300px" ReadOnly="true" Font-Bold="true"></asp:TextBox>
            </span>
            <span class="span_caption">Game: </span>
            <span class="span_value">
                <asp:TextBox ID="txtGameName" runat="server" CssClass="textbox" Width="300px" ReadOnly="true" Font-Bold="true"></asp:TextBox>
            </span>
            <span class="span_caption">Server nạp tiền: </span>
            <span class="span_value">
                <asp:TextBox ID="txtGameServer" runat="server" CssClass="textbox" Width="300px" ReadOnly="true" Font-Bold="true"></asp:TextBox>
            </span>
            <span class="span_caption">&nbsp;</span>
            <span class="span_value">
                &nbsp;
            </span>
            <span class="span_caption">Số tiền (khách hàng nộp): </span>
            <span class="span_value">
                <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox" Width="300px" ReadOnly="true" Font-Bold="true"></asp:TextBox>
            </span>
            <span class="span_caption">&nbsp;</span>
            <span class="span_value">
                <asp:Label ID="labelAmount" runat="server" Font-Italic="true"></asp:Label>
            </span>
            <span class="span_caption">Số tiền (chuyển vào game sau khi cộng %): </span>
            <span class="span_value">
                <asp:TextBox ID="txtCardLogAmount" runat="server" CssClass="textbox" Width="300px" ReadOnly="true" Font-Bold="true"></asp:TextBox>
            </span>
            <span class="span_caption">&nbsp;</span>
            <span class="span_value">
                <asp:Label ID="labelCardLogAmount" runat="server" Font-Italic="true"></asp:Label>
            </span>
            
            <span class="span_caption">&nbsp;</span>
            <span class="span_value">
                <asp:CheckBox ID="checkAccept" runat="server" Text="Đã kiểm tra và chấp nhận hóa đơn nạp tiền này" />
                <asp:Label ID="labelMessage" runat="server" Text="" ForeColor="#FF0000"></asp:Label>                
            </span>
            <span class="span_caption">&nbsp;</span>
            <span class="span_value">
                <asp:Button ID = "buttonAccept" runat="server" CssClass="button" Text= " Chấp nhận hóa đơn " OnClientClick="return confirm('Bạn đồng ý duyệt và chấp nhận hóa đơn nạp tiền này?')" />
                <asp:Button ID = "buttonCancel" runat="server" CssClass="button" Text= " Quay lại " />
            </span>
        </fieldset>        
    </div>
    
</asp:Content>