<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="CardLog_CheckError.aspx.cs" Inherits="IDAdmin.Pages.CardLog_CheckError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Kiểm tra lỗi giao dịch thẻ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(function() {
            $("#<%= txtFromDate.ClientID %>").datepick({ dateFormat: 'dd/mm/yyyy' });
            $("#<%= txtToDate.ClientID %>").datepick({ dateFormat: 'dd/mm/yyyy' });
        });    
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server"><div class="page_title">
        TRA CỨU THÔNG TIN GIAO DỊCH THẺ
    </div>
    <div class="page_bar">
        <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>Từ ngày:</td>
                <td>Đến ngày:</td>
                <td>Loại thẻ/hình thức nạp:</td>
                <td>Mã trạng thái:</td>
                <td>Mã lỗi:</td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" Width="90px" class="textbox" MaxLength="10" />
                </td>
                <td><asp:TextBox ID="txtToDate" runat="server" Width="90px" CssClass="textbox" MaxLength="10" /></td>
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
                    </asp:DropDownList>
                </td>
                <td><asp:TextBox ID="txtStatus" runat="server" Width="100px" CssClass="textbox" /></td> 
                <td><asp:TextBox ID="txtErrorCode" runat="server" Width="100px" CssClass="textbox" /></td>        
                <td><asp:Button ID="buttonExecute" runat="server" Text=" Hiển thị " /></td>
            </tr>
        </table>
    </div>
    <br />
    <div class="page_content">
        <asp:Panel ID="panelList" runat="server" Width="100%">        
        </asp:Panel>
    </div>
</asp:Content>
