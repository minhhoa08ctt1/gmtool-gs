<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true"
    CodeBehind="Statistic_Sumary.aspx.cs" Inherits="IDAdmin.Pages.Statistic_Sumary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Thống kê doanh thu hàng ngày
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
        THỐNG KÊ DOANH THU
    </div>    
    <div class="page_bar">
        <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>Từ ngày:</td>
                <td>Đến ngày:</td>
                <td>Mốc thời gian</td>
                <td>Loại thẻ/hình thức nạp:</td>
                <td>Game Server:</td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" Width="90px" class="textbox" MaxLength="10" />
                </td>
                <td><asp:TextBox ID="txtToDate" runat="server" Width="90px" CssClass="textbox" MaxLength="10" /></td>
                <td>
                    <asp:DropDownList ID="cmbTimeLine" runat="server" Width="90px">                        
                        <asp:ListItem Value="1" Text="1 giờ" />
                        <asp:ListItem Value="2" Text="2 giờ" />
                        <asp:ListItem Value="3" Text="3 giờ" />
                        <asp:ListItem Value="4" Text="4 giờ" />
                        <asp:ListItem Value="5" Text="5 giờ" />
                        <asp:ListItem Value="6" Text="6 giờ" />
                        <asp:ListItem Value="7" Text="7 giờ" />
                        <asp:ListItem Value="8" Text="8 giờ" />
                        <asp:ListItem Value="9" Text="9 giờ" />
                        <asp:ListItem Value="10" Text="10 giờ" />
                        <asp:ListItem Value="11" Text="11 giờ" />
                        <asp:ListItem Value="12" Text="12 giờ" />
                        <asp:ListItem Value="13" Text="13 giờ" />
                        <asp:ListItem Value="14" Text="14 giờ" />
                        <asp:ListItem Value="15" Text="15 giờ" />
                        <asp:ListItem Value="16" Text="16 giờ" />
                        <asp:ListItem Value="17" Text="17 giờ" />
                        <asp:ListItem Value="18" Text="18 giờ" />
                        <asp:ListItem Value="19" Text="19 giờ" />
                        <asp:ListItem Value="20" Text="20 giờ" />
                        <asp:ListItem Value="21" Text="21 giờ" />
                        <asp:ListItem Value="22" Text="22 giờ" />
                        <asp:ListItem Value="23" Text="23 giờ" />
                        <asp:ListItem Value="0" Text="Cuối ngày" Selected="True" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="cmbType" runat="server" Width="150px"> 
                        <asp:ListItem Value="" Text="--- Tất cả ---" />                        
                        <asp:ListItem Value="vtc" Text="VTC" />
                        <asp:ListItem Value="fpt" Text="GATE FPT" />
                        <asp:ListItem Value="vina" Text="VINAPHONE" />
                        <asp:ListItem Value="mobi" Text="MOBIFONE" />
                        <asp:ListItem Value="viettel" Text="VIETTEL" />
                        <asp:ListItem Value="xgo" Text="XGO" />
                        <asp:ListItem Value="chm" Text="PHATTAI" />
                        <asp:ListItem Value="soha" Text="Soha Chanelling" />
                        <asp:ListItem Value="go-vtc" Text="VTC (Go) Chanelling" />
                        <asp:ListItem Value="TIENMAT" Text="Tiền mặt" />
                        <asp:ListItem Value="ONEPAY" Text="Chuyển khoản bằng ATM" />
                    </asp:DropDownList>
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
