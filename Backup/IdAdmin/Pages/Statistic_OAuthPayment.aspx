<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Statistic_OAuthPayment.aspx.cs" Inherits="IDAdmin.Pages.Statistic_OAuthPayment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Thống kê giao dịch qua OAuth Payment Service
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
        <asp:Label ID="labelTitle" runat="server"></asp:Label>
    </div>    
    <div class="page_bar" style="text-align:right">
        <asp:HyperLink ID="linkServiceList" runat="server" Visible="false" NavigateUrl="~/Pages/Statistic_OAuthPayment.aspx">
            [Quay lại danh sách dịch vụ]
        </asp:HyperLink>
    </div>
    <div class="page_bar">
        <asp:Panel ID="panelDateSelect" runat="server">
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
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div class="page_content">
        <asp:Panel ID="panelList" runat="server">
        </asp:Panel>
    </div>
</asp:Content>
