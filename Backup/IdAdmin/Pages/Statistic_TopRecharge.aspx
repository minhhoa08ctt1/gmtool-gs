<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Statistic_TopRecharge.aspx.cs" Inherits="IDAdmin.Pages.Statistic_TopRecharge" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Thống kê tổng số tiền giao dịch mỗi khách hàng
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
        THỐNG KÊ GIAO DỊCH CỦA KHÁCH HÀNG
    </div>
    <div class="page_bar" style="text-align:right">
        <a href="Statistic_TopRecharge.aspx">[Cộng đồng GOSU]</a>&nbsp;
        <a href="Statistic_TopChanellingRecharge.aspx">[Cộng đồng Chanelling]</a>&nbsp;
    </div>
    <div class="page_bar">
        <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>Từ ngày:</td>
                <td>Đến ngày:</td>
                <td>Game Server:</td>
                <td>Tài khoản (tìm chính xác):</td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" Width="90px" class="textbox" MaxLength="10" />
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" Width="90px" CssClass="textbox" MaxLength="10" />
                </td>
                <td>
                    <asp:DropDownList ID="cmbServer" runat="server" Width="200px">                         
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtAccount" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="buttonExecute" runat="server" CssClass="button" Text="  Thống kê  " />
                </td>
            </tr>
        </table>
    </div>
    <div class="page_content">
        <asp:Panel ID="panelList" runat="server">
        </asp:Panel>
    </div>
    <div style="text-align:center;padding:5px">
        <asp:HyperLink ID="linkPrev" runat="server" Text="Trang trước" />&nbsp;|&nbsp;
        <asp:HyperLink ID="linkNext" runat="server" Text="Trang sau" />
    </div> 
</asp:Content>
