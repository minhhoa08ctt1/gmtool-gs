<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="WalletSpendingCode.aspx.cs" Inherits="IDAdmin.Pages.WalletSpendingCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Tra cứu lịch sử mua CODE bằng GOSU
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
        TRA CỨU THÔNG TIN MUA CODE KÍCH HOẠT BẰNG VÍ GOSU
    </div>
    <div class="page_bar">
        <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>Từ ngày:</td>
                <td>Đến ngày:</td>
                <td>Giá trị hóa đơn:</td>                
                <td>Kind:</td>
                <td>Tài khoản:</td>
                <td>Trạng thái:</td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" Width="90px" class="textbox" MaxLength="10" />
                </td>
                <td><asp:TextBox ID="txtToDate" runat="server" Width="90px" CssClass="textbox" MaxLength="10" /></td>
                <td>
                    <asp:DropDownList ID="ddlAmount" runat="server" Width="100px">                        
                        <asp:ListItem Value="0" Text="All" Selected="True" />
                        <asp:ListItem Value="1150" Text="1.150 GOSU" />
                        <asp:ListItem Value="1035" Text="1.035 GOSU" />
                        <asp:ListItem Value="920" Text="920 GOSU" />
                        <asp:ListItem Value="5750" Text="5.750 GOSU"  />
                        <asp:ListItem Value="3280" Text="3.280 GOSU"  />
                        <asp:ListItem Value="5175" Text="5.175 GOSU"  />
                    </asp:DropDownList>                    
                </td>
                <td>
                    <asp:DropDownList ID="ddlKind" runat="server" Width="100px">                        
                        <asp:ListItem Value="All" Text="All" Selected="True" />
                        <asp:ListItem Value="FREE" Text="Free" />
                        <asp:ListItem Value="NORMAL" Text="Normal" />
                        <asp:ListItem Value="VIP" Text="Vip"  />
                    </asp:DropDownList>
                </td>
                <td><asp:TextBox ID="txtAccount" runat="server" Width="150px" CssClass="textbox" /></td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="100px">                        
                        <asp:ListItem Value="1" Text="Thành công" Selected=True />
                        <asp:ListItem Value="-1" Text="Lỗi"  />
                        <asp:ListItem Value="0" Text="Tất cả" />
                    </asp:DropDownList>
                </td>
                <td><asp:Button ID="buttonExecute" runat="server" Text=" Hiển thị " /></td>
                <td>
                    &nbsp;&nbsp;GD: <asp:Label ID="txtTotal" runat="server" Text="Label" Font-Bold="true" ForeColor="Red"></asp:Label>
                    &nbsp;&nbsp;Code: <asp:Label ID="txtTotalCode" runat="server" Text="Label" Font-Bold="true" ForeColor="Red"></asp:Label>
                </td>
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
