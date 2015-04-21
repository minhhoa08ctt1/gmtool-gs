<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master"
    CodeBehind="Statistic_CDKey.aspx.cs" Inherits="IDAdmin.Pages.Statistic_CDKey" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    Thống kê tài khoản kích hoạt dựa trên CDKey
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
        $(function() {
            $("#<%= txtFromDate.ClientID %>").datepick({ dateFormat: 'dd/mm/yyyy' });
            $("#<%= txtToDate.ClientID %>").datepick({ dateFormat: 'dd/mm/yyyy' });
        });    
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="page_title">
        THỐNG KÊ TÀI KHOẢN KÍCH HOẠT THEO NGÀY DỰA TRÊN CDKEY
    </div>
    <div class="page_bar">
        <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>Từ ngày:</td>
                <td>Đến ngày:</td>
                <td>Loại thẻ:</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" Width="90px" class="textbox" MaxLength="10" />
                </td>
                <td><asp:TextBox ID="txtToDate" runat="server" Width="90px" CssClass="textbox" MaxLength="10" /></td>
                <td>
                    <asp:DropDownList ID="ddlKind" runat="server" Width="100px">                        
                        <asp:ListItem Value="" Text="All" Selected="True" />
                        <asp:ListItem Value="NORMAL" Text="Normal" />
                        <asp:ListItem Value="VIP" Text="Vip"  />
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
        Tổng số tài khoản đã kích hoạt: 
        <asp:Label ID="labelCountOfUser" runat="server" Font-Bold="true">
        </asp:Label>
        <br /><br />
        <asp:Panel ID="panelList" runat="server">
        </asp:Panel>
    </div>
</asp:Content>