<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master"
        CodeBehind="ImageUpload.aspx.cs" Inherits="IDAdmin.Pages.ImageUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" Runat="Server">
    Upload ảnh
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="page_title">
       UPLOAD FILE
    </div>
    
    <div class="page_content"> 
        <b>Chú ý:</b>
        <ul>
            <li>Chọn đúng thư mục cần upload file tùy thuộc vào mục đích sử dụng của file để tiện cho việc theo dõi và quản lý</li>
            <li>Nếu tên file cần upload đặt trùng với tên file đã tồn tại, file đã tồn tại sẽ bị ghi đè</li>
            <li>Nếu không đặt tên file, hệ thống sẽ tự động đặt tên</li>
        </ul>
        <table>
            <tr>
                <td>
                    Chọn thư mục upload file:                    
                </td>
                <td>
                    Chọn file cần upload:
                </td>
                <td>
                    Đặt tên file:
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="cmbFolder" runat="server" Width="300px">
                        <asp:ListItem Value="Uploads">Uploads (khuyến cáo sử dụng)</asp:ListItem>
                        <asp:ListItem Value="News">News (dành riêng cho tin khi cần thiết)</asp:ListItem>
                        <asp:ListItem Value="Slide">Slide (Ảnh Slide)</asp:ListItem>
                        <asp:ListItem Value="Invite">Invite (Ảnh dùng cho banner mời bạn)</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:FileUpload ID="fileUploadImage" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="txtFileName" runat="server"></asp:TextBox>                    
                </td>
            </tr>
        </table>
        <asp:Button ID="buttonListFile" runat="server" Text=" Xem danh sách file " OnClick="buttonListFile_Click" />
        <asp:Button ID="buttonUpload" runat="server" Text="  Upload file " OnClick="buttonUpload_Click" />
        <br />
        <asp:Label ID="labelMessage" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Panel ID="panelListFile" runat="server" Width="100%">
            
        </asp:Panel>
    </div>
</asp:Content>
