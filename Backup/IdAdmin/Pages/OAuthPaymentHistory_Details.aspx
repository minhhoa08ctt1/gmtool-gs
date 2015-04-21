<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="OAuthPaymentHistory_Details.aspx.cs" Inherits="IDAdmin.Pages.OAuthPaymentHistory_Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Thông tin chi tiết giao dịch
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
        <legend>Thông tin chi tiết giao dịch</legend>    
        <p>
            <span class="span_caption">Mã dịch vụ:</span>
            <span class="span_value">
                <asp:Label ID="labelServiceID" runat="server" Width="80%" Font-Bold="true">11</asp:Label>&nbsp;
            </span>
            <span class="span_caption">Mã đơn hàng:</span>
            <span class="span_value">
                <asp:Label ID="labelOrderID" runat="server" Width="80%" Font-Bold="true">11</asp:Label>&nbsp;
            </span>
            <span class="span_caption">Thời điểm giao dịch:</span>
            <span class="span_value">
                <asp:Label ID="labelCreated" runat="server" Width="80%" Font-Bold="true">11</asp:Label>&nbsp;
            </span>
            <span class="span_caption">Nội dung: </span>
            <span class="span_value">
                <asp:Label ID="labelOrderInfo" runat="server" Width="80%" Font-Bold="true">11</asp:Label>&nbsp;
            </span>
            <span class="span_caption">Số GOSU</span>
            <span class="span_value">
                <asp:Label ID="labelAmount" runat="server" Width="80%" Font-Bold="true">11</asp:Label>&nbsp;
            </span>
            <span class="span_caption">Hình thức thanh toán:</span>
            <span class="span_value">
                <asp:Label ID="labelPayMethod" runat="server" Width="80%" Font-Bold="true">11</asp:Label>&nbsp;
            </span>
            <span class="span_caption">URL nhận kết quả giao dịch:</span>
            <span class="span_value">
                <asp:Label ID="labelRedirectURL" runat="server" Width="80%" Font-Bold="true">11</asp:Label>&nbsp;
            </span>
            <span class="span_caption">Chữ ký bảo mật:</span>
            <span class="span_value">
                <asp:Label ID="labelSignature" runat="server" Width="80%" Font-Bold="true">11</asp:Label>&nbsp;
            </span>
            <span class="span_caption">IP khách hàng:</span>
            <span class="span_value">
                <asp:Label ID="labelIP" runat="server" Width="80%" Font-Bold="true">11</asp:Label>&nbsp;
            </span>
            <span class="span_caption">Tài khoản giao dịch:</span>
            <span class="span_value">
                <asp:Label ID="labelUserName" runat="server" Width="80%" Font-Bold="true">11</asp:Label>&nbsp;
            </span>
            <span class="span_caption">Thời điểm kết thúc giao dịch:</span>
            <span class="span_value">
                <asp:Label ID="labelFinishTime" runat="server" Width="80%" Font-Bold="true">11</asp:Label>&nbsp;
            </span>
            <span class="span_caption">Mã trạng thái (Status):</span>
            <span class="span_value">
                <asp:Label ID="labelStatus" runat="server" Width="80%" Font-Bold="true">11</asp:Label>&nbsp;
            </span>
            <span class="span_caption">Mã kết quả giao dịch (ResponseCode):</span>
            <span class="span_value">
                <asp:Label ID="labelResponseCode" runat="server" Width="80%" Font-Bold="true">11</asp:Label>&nbsp;
            </span>
            <span class="span_caption">Kết quả giao dịch:</span>
            <span class="span_value">
                <asp:Label ID="labelResponseMsg" runat="server" Width="80%" Font-Bold="true">11</asp:Label>&nbsp;
            </span>
        </p>
        </fieldset>
    </div>
</asp:Content>
