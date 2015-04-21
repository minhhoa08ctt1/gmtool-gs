<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="WalletRechargeHistory_Edit.aspx.cs" Inherits="IDAdmin.Pages.WalletRechargeHistory_Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Thông tin giao dịch nạp tiền vào ví GOSU
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">         
    </div>
    <div class="page_bar" style="text-align:right">
        <asp:HyperLink ID="linkBack" runat="server">[Quay lại]</asp:HyperLink>
    </div>
    <div class="page_content">    
        <fieldset>    
        <legend>Thông tin giao dịch nạp tiền vào ví GOSU</legend>    
        <p>
            <span class="span_caption">Mã giao dịch: </span>
            <span class="span_value">
                <asp:Label ID="labelRechargeID" runat="server" Width="80%" Font-Bold="true">11</asp:Label>&nbsp;
            </span>
            <span class="span_caption">Người nạp: </span>
            <span class="span_value">
                <asp:Label ID="labelRechargeUser" runat="server" Width="80%" Font-Bold="true"></asp:Label>&nbsp;
            </span>
            <span class="span_caption">Tài khoản được nạp: </span>
            <span class="span_value">
                <asp:Label ID="labelUserName" runat="server" Width="80%" Font-Bold="true"></asp:Label>&nbsp;
            </span>
            <span class="span_caption">Thời điểm giao dịch: </span>
            <span class="span_value">
                <asp:Label ID="labelCreated" runat="server" Width="80%" Font-Bold="true"></asp:Label>&nbsp;
            </span>
            <span class="span_caption">Hình thức nạp: </span>
            <span class="span_value">
                <asp:Label ID="labelType" runat="server" Width="80%" Font-Bold="true"></asp:Label>&nbsp;
            </span>
            <span class="span_caption">Serial - mã PIN: </span>
            <span class="span_value">
                <asp:Label ID="labelPinNumber" runat="server" Width="80%" Font-Bold="true"></asp:Label>&nbsp;
            </span>
            <span class="span_caption">Số tiền (mệnh giá VNĐ): </span>
            <span class="span_value">
                <asp:Label ID="labelAmount" runat="server" Width="80%" Font-Bold="true"></asp:Label>&nbsp;
            </span>
            <span class="span_caption">Số GOSU nhận được: </span>
            <span class="span_value">
                <asp:Label ID="labelWalletAmount" runat="server" Width="80%" Font-Bold="true"></asp:Label>&nbsp;
            </span>
            <span class="span_caption">Số GOSU được tặng: </span>
            <span class="span_value">
                <asp:Label ID="labelPromotionAmount" runat="server" Width="80%" Font-Bold="true"></asp:Label>&nbsp;
            </span>
            <span class="span_caption">Mã trạng thái giao dịch: </span>
            <span class="span_value">
                <asp:Label ID="labelStatus" runat="server" Width="80%" Font-Bold="true"></asp:Label>&nbsp;
            </span>
            <span class="span_caption">Mã lỗi giao dịch: </span>
            <span class="span_value">
                <asp:Label ID="labelErrorCode" runat="server" Width="80%" Font-Bold="true"></asp:Label>&nbsp;
            </span>
            <span class="span_caption">Kết quả giao dịch: </span>
            <span class="span_value">
                <asp:Label ID="labelMsg" runat="server" Width="80%" Font-Bold="true"></asp:Label>&nbsp;
            </span>   
        </p>
        </fieldset>
    </div>
    
    <asp:Panel ID="panelEdit" runat="server">
        <div class="page_content">    
            <fieldset>    
                <legend>Xử lý giao dịch lỗi</legend>    
                <b>Lưu ý:</b> Sử dụng chức năng này trong trường hợp giao dịch nạp thẻ đã thành công nhưng 
                phía ID GOSU chưa ghi nhận thành công của giao dịch. Tuyệt đối cẩn thận khi sử dụng chức năng này.
                <br />
                <br />
                <span class="span_caption">Số tiền giao dịch (bằng VNĐ): </span>
                <span class="span_value">
                    <asp:TextBox ID="txtEditAmount" runat="server" Width="300px" CssClass="textbox" Text="0"></asp:TextBox>&nbsp;
                </span>
                <span class="span_caption">Mã bảo vệ: </span>
                <span class="span_value">
                    <asp:TextBox ID="txtCaptcha" runat="server" Width="300px" CssClass="textbox"></asp:TextBox>&nbsp;
                    <br />
                    <img src="Captcha.aspx" alt="" />&nbsp;
                </span>
                <span class="span_caption">&nbsp;</span>
                <span class="span_value">
                        <asp:Button ID="buttonUpdateLog" runat="server" Text=" Ghi nhận giao dịch thành công " OnClientClick="return confirm('Bạn có chắc chắn muốn thay đổi giao dịch này sang trạng thái giao dịch thành công?');" OnClick="buttonUpdateLog_Click" />&nbsp;                        
                </span>            
                <span class="span_caption">&nbsp;</span>
                <span class="span_value">
                    <asp:Label ID="labelMessage" runat="server" ForeColor="#FF0000"></asp:Label>
                </span>
            </fieldset>                            
        </div>
    </asp:Panel>        
        
</asp:Content>
