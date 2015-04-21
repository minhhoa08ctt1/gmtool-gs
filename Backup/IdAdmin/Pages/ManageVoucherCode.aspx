<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" CodeBehind="ManageVoucherCode.aspx.cs" Inherits="IDAdmin.Pages.ManageVoucherCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý Voucher Code
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        QUẢN LÝ VOUCHER CODE
    </div>    
    <div class="page_bar">
        <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>PinCode</td>
                <td>Rate</td>
                <td>Status</td>
                <td>UsedAccount</td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="txtPinCode" runat="server" Width="200px" class="textbox" /></td>
                <td>
                    <asp:DropDownList ID="ddlRate" runat="server" Width="100px">                        
                        <asp:ListItem Value="All" Text="All" Selected="True" />
                        <asp:ListItem Value="0.05" Text="0.05" />
                        <asp:ListItem Value="0.1" Text="0.1"  />
                        <asp:ListItem Value="0.15" Text="0.15"  />
                        <asp:ListItem Value="0.2" Text="0.2"  />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="100px">                        
                        <asp:ListItem Value="All" Text="All" Selected="True" />
                        <asp:ListItem Value="1" Text="Chưa sử dụng" />
                        <asp:ListItem Value="2" Text="Đã sử dụng" />
                    </asp:DropDownList>
                </td>
                <td><asp:TextBox ID="txtUsedAccount" runat="server" Width="200px" CssClass="textbox" /></td>
                <td>
                    <asp:Button ID="buttonExecute" runat="server" CssClass="button" Text="  Thống kê  " />
                </td>
                <td>
                    &nbsp;&nbsp;<asp:Label ID="txtTotal" runat="server" Text="Label" Font-Bold="true" ForeColor="Red" Font-Size="Larger"></asp:Label>
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

