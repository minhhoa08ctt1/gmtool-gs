<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" CodeBehind="ManageActiveCode.aspx.cs" Inherits="IDAdmin.Pages.ManageActiveCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý Active Code
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        QUẢN LÝ ACTIVE CODE
    </div>    
    <div class="page_bar">
        <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>PinCode</td>
                <td>Serial</td>
                <td>Type</td>
                <td>Kind</td>
                <td>Turn</td>
                <td>Status</td>
                <td>Account</td>
                <td>UsedAccount</td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="txtPinCode" runat="server" Width="100px" class="textbox" /></td>
                <td><asp:TextBox ID="txtSerial" runat="server" Width="100px" CssClass="textbox" /></td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server" Width="100px">                        
                        <asp:ListItem Value="All" Text="All" Selected="True" />
                        <asp:ListItem Value="online" Text="Online" />
                        <asp:ListItem Value="vegiay" Text="Vé giấy"  />
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
                <td>
                    <asp:DropDownList ID="ddlTurn" runat="server" Width="100px">                        
                        <asp:ListItem Value="All" Text="All" Selected="True" />
                        <asp:ListItem Value="1" Text="1" />
                        <asp:ListItem Value="2" Text="2" />
                        <asp:ListItem Value="3" Text="3"  />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="100px">                        
                        <asp:ListItem Value="All" Text="All" Selected="True" />
                        <asp:ListItem Value="1" Text="Chưa bán" />
                        <asp:ListItem Value="2" Text="Đã bán" />
                        <asp:ListItem Value="3" Text="Đã sử dụng" />
                    </asp:DropDownList>
                </td>
                <td><asp:TextBox ID="txtAccount" runat="server" Width="100px" class="textbox" /></td>
                <td><asp:TextBox ID="txtUsedAccount" runat="server" Width="100px" CssClass="textbox" /></td>
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

