<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ServerList.aspx.cs" Inherits="IDAdmin.Pages.ServerList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý game server
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        Quản lý Server    
    </div>    
    <div class="page_bar" style="text-align:right">
        <a href="ChanellingPartner.aspx">[Quản lý server cho đối tác Chanelling]</a>
    </div>
    <div class="page_bar" style="text-align:left">
        Thiết lập cho Server được chọn:
        <select id="status_value" name="status_value">
            <option value="open">Mở server cho cộng đồng GOSU</option>
            <option value="close">Đóng server đối với cộng đồng GOSU</option>
        </select>
        <button id="set_status" name="button" value="set_status">Thiết lập</button>
    </div>    
    <div class="page_content">
        <asp:Panel ID="panelList" runat="server" Width="100%">        
        </asp:Panel>
        <br />
        <br />
        <asp:Panel ID="panelAdd" runat="server" Width="100%">
            <fieldset style="width:98%">
                <legend>Tạo server mới</legend>
                <table width="100%">
                    <tr>
			            <td style="width:10%;">
			                Server ID
			            </td>
			            <td style="width:10%;">
			                Server Name
			            </td>
			            <td style="width:30%;">
			                Full Name
			            </td>
			            <td style="width:10%;">
			                Identiy Name
			            </td>			            
			            <td style="width:10%;">
			               Sort Order
			            </td>
			            <td>
			                <asp:CheckBox ID="checkAccept" runat="server" Checked="false" Text="Xác nhận đồng ý tạo server mới" />
			            </td>
		            </tr>
		            <tr>
		                <td>
			                <asp:TextBox ID="txtServerID" runat="server" CssClass="textbox" Width="100%"></asp:TextBox>
		                </td>
		                <td>
		                    <asp:TextBox ID="txtServerName" runat="server" CssClass="textbox" Width="100%"></asp:TextBox>
		                </td>
		                <td>
		                    <asp:TextBox ID="txtServerFullName" runat="server" CssClass="textbox" Width="100%"></asp:TextBox>
		                </td>
		                <td>
		                    <asp:TextBox ID="txtIdentiyName" runat="server" CssClass="textbox" Width="100%"></asp:TextBox>
		                </td>
		                <td>
		                    <asp:TextBox ID="txtSortOrder" runat="server" CssClass="textbox" Width="100%"></asp:TextBox>
		                </td>
		                <td>
		                    <asp:Button ID="buttonCreate" runat="server" Text=" Tạo Server " />                            
		                </td>
		            </tr>
                </table>
                <asp:Label ID="labelAddMessage" runat="server" ForeColor="#FF0000"></asp:Label>
                <br />
                <ul>
                    <li>
                        Server ID: Mã server, là chuỗi số gồm 5 ký tự và phải theo quy định (Vd: 20001, 20002, 30001, 30002,...)
                    </li>
                    <li>
                        Server Name: Tên server (có dạng S1, S2, S3,.... và không được trùng)
                    </li>
                    <li>
                        Full Name: Tên gọi đầy đủ của Server (Vd: 1. Thiên Long, 2. Địa Long,....)
                    </li>
                    <li>
                        Identity Name: Tên dùng để định danh Server (có dạng S1, S2,... và thường trùng với tên Server)
                    </li>                    
                </ul>
                
            </fieldset>
        </asp:Panel>        
    </div>
    
</asp:Content>
