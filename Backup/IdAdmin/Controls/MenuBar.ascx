<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuBar.ascx.cs" Inherits="IDAdmin.Controls.MenuBar" %>
<div id="header">
   <asp:Label ID="labelHeader" runat="server" Text="GOSU"></asp:Label>
</div>
<div id="menu">            
    <ul id="menubar">
        <li>
            <a href="Index.aspx">Trang chủ</a>         
        </li>
        <li>
            <a href="#">Quản lý Nội dung</a>
            <ul>
                <li><a href="NewsList.aspx">Danh sách bài viết</a></li>
                <li><a href="NewsEdit.aspx?action=add">Viết bài mới</a></li>
                <li><a href="ImageUpload.aspx">Upload file</a></li>
            </ul>
        </li>
        <li>
            <a href="#">Quản lý tài khoản</a>
            <ul>
                <li><a href="AccountList.aspx">Tài khoản GOSU</a></li>
                <li><a href="ChanellingUsers.aspx">Tài khoản Chanelling</a></li>
                <li><a href="ViewPassGame.aspx">Xem password game</a></li>
            </ul>
        </li>
        <li>
            <a href="#">Quản lý giao dịch</a>
            <ul>
                <li><a href="TransHistory.aspx">Giao dịch nạp vàng vào game</a></li>
                <li><a href="PaymentAdd.aspx">Lập hóa đơn nạp tiền game trực tiếp</a></li>
                <li><a href="PaymentList.aspx">Xử lý hóa đơn nạp tiền game</a></li>
                <li><a href="WalletRechargeHistory.aspx">Giao dịch nạp tiền vào ví GOSU</a></li>    
                <li><a href="WalletSpendingCode.aspx">Giao dịch mua CODE từ ví GOSU</a></li>    
                <li><a href="OAuthPaymentHistory.aspx">Giao dịch thanh toán qua OAuth</a></li>    
                            
            </ul>
        </li>
        <li>
            <a href="#">Thống kê</a>
            <ul>
                <li><a href="Statistic_Sumary.aspx">Doanh thu hàng ngày</a></li>      
                <li><a href="Statistic_ByCommunity.aspx">TK nạp tiền theo cộng đồng</a></li>      
                <li><a href="Statistic_User.aspx">Đăng ký tài khoản hàng ngày</a></li>
                <li><a href="Statistic_TopRecharge.aspx">TK giao dịch của khách hàng</a></li>
                <li><a href="Statistic_SumaryByGameAndType.aspx">TK tổng hợp tiền nạp vào game</a></li>
                <li><a href="Statistic_SumaryWalletRecharge.aspx">TK nạp tiền vào ví GOSU</a></li>
                <li><a href="Statistic_OAuthPayment.aspx">TK giao dịch qua OAuth Payment Service</a></li>
                <li><a href="Statistic_ActivatedAccount.aspx">TK kích hoạt theo ngày</a></li>
                <li><a href="Statistic_CDKey.aspx">CDKey sử dụng theo ngày</a></li>
                <li><a href="Statistic_ActiveCode.aspx">Code kích hoạt</a></li>
            </ul>
        </li>
        <li>
            <a href="#">Ví gosu</a>
            <ul>
                <li><a href="GosuAccountList.aspx">Tài khoản nạp gosu</a></li>
                <li><a href="GosuSummary.aspx">Thông tin tổng hợp ví gosu</a></li>      
            </ul>
        </li>
        <li>
            <a href="#">Hệ thống</a>
            <ul>
                <li><a href="ServerList.aspx">Quản lý game server</a></li>
                <li><a href="ManageActiveCode.aspx">Quản lý Active Code</a></li>
                <li><a href="ManageVoucherCode.aspx">Quản lý Voucher Code</a></li>
                <li><a href="AdminCACK.aspx">Add Item/Add Gold/Add VIP</a></li>
                <li><a href="ViewAdminLog.aspx">Xem lịch sử thao tác của Admin</a></li>
                <li><a href="CardLog_CheckError.aspx">Kiểm tra lỗi giao dịch thẻ</a></li>
                <li><a href="ManageUserRights.aspx">Cấp phát quyền quản trị</a></li>
                <li><a href="OAuthApplication.aspx">OAuth &amp; Payment Service</a></li>
                
            </ul>
        </li>
        <li>
            <asp:HyperLink ID="linkLogOut" runat="server" NavigateUrl="~/Pages/LogOut.aspx">Thoát</asp:HyperLink>
        </li>     
    </ul>
</div>
<div class="clear">
</div>

