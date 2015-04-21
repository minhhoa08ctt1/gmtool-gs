<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="OAuthPaymentHistory.aspx.cs" Inherits="IDAdmin.Pages.OAuthPaymentHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Giao dịch thanh toán qua OAuth
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(function() {
            $("#<%= txtFromDate.ClientID %>").datepick({ dateFormat: 'dd/mm/yyyy' });
            $("#<%= txtToDate.ClientID %>").datepick({ dateFormat: 'dd/mm/yyyy' });
        });    
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        TRA CỨU THÔNG TIN NẠP THẺ VÀO VÍ GOSU
    </div>
    <div class="page_bar">
        <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>Từ ngày:</td>
                <td>Đến ngày:</td>
                <td>Ký hiệu dịch vụ (ServiceID):</td>                
                <td>Giá trị tìm kiếm</td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" Width="90px" class="textbox" MaxLength="10" />
                </td>
                <td><asp:TextBox ID="txtToDate" runat="server" Width="90px" CssClass="textbox" MaxLength="10" /></td>
                <td>
                    <asp:TextBox ID="txtServiceID" runat="server" Width="250px" CssClass="textbox" />
                </td>                
                <td><asp:TextBox ID="txtSearchValue" runat="server" Width="150px" CssClass="textbox" /></td>
                <td><asp:Button ID="buttonExecute" runat="server" Text=" Hiển thị " /></td>
            </tr>
        </table>
    </div>
    <br />
    <div class="page_content">
        <asp:Panel ID="panelList" runat="server" Width="100%">        
        </asp:Panel>
    </div>
    <div style="text-align:center;padding:5px">
        <asp:HyperLink ID="linkFirst" runat="server" Text="Về đầu" />&nbsp;|&nbsp;
        <asp:HyperLink ID="linkPrev" runat="server" Text="Trang trước" />&nbsp;|&nbsp;
        <asp:HyperLink ID="linkNext" runat="server" Text="Trang sau" />
    </div> 
</asp:Content>
