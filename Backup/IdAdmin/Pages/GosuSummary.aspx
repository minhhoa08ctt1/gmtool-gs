<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="GosuSummary.aspx.cs" Inherits="IDAdmin.Pages.GosuSummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Thông tin tổng hợp ví gosu
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
        THỐNG KÊ TỔNG HỢP VÍ GOSU
    </div>
    <div class="page_bar">
    <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>Từ ngày:</td>
                <td>Đến ngày:</td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" Width="90px" class="textbox" MaxLength="10" />
                </td>
                <td><asp:TextBox ID="txtToDate" runat="server" Width="90px" CssClass="textbox" MaxLength="10" /></td>                
                <td>
                    <asp:Button ID="buttonExecute" runat="server" CssClass="button" Text="  Thống kê  " />
                    <asp:Button ID="buttonExportToExcel" runat="server" CssClass="button" Text="  Xuất sang Excel  " />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="page_content">
        Tổng số Gosu hiện có trong ví: 
        <asp:Label ID="labelCountOfGosu" runat="server" Font-Bold="true">
        </asp:Label>
        <br /><br />
        <asp:Panel ID="panelList" runat="server" Width="100%">
        </asp:Panel>
    </div>
</asp:Content>
