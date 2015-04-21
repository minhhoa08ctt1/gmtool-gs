<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ChanellingGameServer.aspx.cs" Inherits="IDAdmin.Pages.ChanellingGameServer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý máy chủ game dành cho đối tác Chanelling
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        <asp:Label ID="labelPartner" runat="server" Font-Bold="true" Font-Size="16px" ForeColor="#FF0000"></asp:Label>        
    </div>
    <div class="page_bar" style="text-align:right">
        <a href="ChanellingPartner.aspx">[Chọn đối tác khác]</a>
    </div>
    <p>&nbsp;</p>
    <div class="page_bar" style="text-align:left">
        Thiết lập trạng thái cho Server được chọn:
        <select id="status_value" name="status_value">
            <option value="open">Mở chức năng vào game và nạp tiền</option>
            <option value="close">Đóng chức năng vào game và nạp tiền</option>
        </select>
        <button id="set_status" name="button" value="set_status">Thiết lập trạng thái</button> 
                
    </div>
    <div class="page_content">        
        <asp:Panel ID="panelList" runat="server">            
        </asp:Panel>
    </div>    
    <div class="page_bar" style="text-align:right">
        <br />
        <asp:HyperLink ID="linkAddServer" runat="server">[Thêm server cho đối tác]</asp:HyperLink>
    </div>
</asp:Content>
