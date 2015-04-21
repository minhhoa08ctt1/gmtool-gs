<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Statistic_ByCommunity.aspx.cs" Inherits="IDAdmin.Pages.Statistic_ByCommunity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Thống kê giao dịch nạp tiền theo cộng đồng
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
        THỐNG KÊ NẠP TIỀN THEO CỘNG ĐỒNG
    </div>
    <div class="page_bar">
        <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>Từ ngày:</td>
                <td>Đến ngày:</td>
                <td>Game Server:</td>
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
                    <asp:Button ID="buttonExecute" runat="server" CssClass="button" Text="  Thống kê  " />
                    <asp:Button ID="buttonExportToExcel" runat="server" CssClass="button" Text="  Xuất sang Excel  " />
                </td>
            </tr>
        </table>
    </div>
    <div class="page_content">
        <asp:Panel ID="panelList" runat="server">
        </asp:Panel>
    </div>
</asp:Content>
