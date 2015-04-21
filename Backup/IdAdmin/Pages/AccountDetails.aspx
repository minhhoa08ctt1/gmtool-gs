<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="AccountDetails.aspx.cs" Inherits="IDAdmin.Pages.AccountDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý tài khoản
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(function() {
            $("#<%= txtBirthday.ClientID %>").datepick({ dateFormat: 'dd/mm/yyyy' });
        });    
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        QUẢN LÝ TÀI KHOẢN
    </div>
    <div class="page_content">
        <asp:Panel ID="panelAccountInfo" runat="server">
            <fieldset>
                <legend>Thông tin tài khoản</legend>
                <p>
                    <span class="span_caption">Mã số tài khoản: </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtCustomerID" runat="server" Width="50%" CssClass="textbox" ReadOnly="true" Font-Bold="true" />
                        <asp:HiddenField ID="hiddenID" runat="server" />
                    </span>
                    <span class="span_caption">Tên đăng nhập: </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtUserName" runat="server" Width="50%" CssClass="textbox" ReadOnly="true" Font-Bold="true" />
                    </span>
                    <span class="span_caption">Thời điểm đăng ký tài khoản: </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtCreated" runat="server" Width="50%" CssClass="textbox" ReadOnly="true" />
                    </span>
                    <span class="span_caption">Thời điểm cập nhật cuối cùng </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtUpdated" runat="server" Width="50%" CssClass="textbox" ReadOnly="true" />
                    </span>
                    <span class="span_caption">Nội dung cập nhật </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtUpdatedContent" runat="server" Width="50%" CssClass="textbox" ReadOnly="true" />
                    </span>
                    <span class="span_caption">Họ và tên: </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtName" runat="server" Width="50%" CssClass="textbox" />
                    </span>
                    <span class="span_caption">Số CMND/Hộ chiếu: </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtNumberID" runat="server" Width="50%" CssClass="textbox" />
                    </span>
                    <span class="span_caption">Vai trò/nhóm: </span>
                    <span class="span_value">
                        <asp:DropDownList ID="cmbLevel" runat = "server" Width="50%" CssClass="textbox">
                            <asp:ListItem Value="0" Text="Thành viên" />
                            <asp:ListItem Value="10" Text="Đại lý" />
                            <asp:ListItem Value="50" Text="Chăm sóc khách hàng" />
                            <asp:ListItem Value="100" Text="Cộng tác viên" />
                            <asp:ListItem Value="1000" Text="Quản trị" />
                            <asp:ListItem Value="10000" Text="Quản trị cao cấp" />
                        </asp:DropDownList>
                        <br />
                        <i><b>Lưu ý:</b> Tài khoản từ mức Chăm sóc khách hàng trở lên là tài khoản có quyền truy cập vào trang Quản trị</i>
                    </span>                    
                    <span class="span_caption">Email: </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtEmail" runat="server" Width="50%" CssClass="textbox" />
                    </span>    
                    <span class="span_caption">Điện thoại: </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtPhoneNumber" runat="server" Width="50%" CssClass="textbox" />
                    </span>
                    <span class="span_caption">Địa chỉ: </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtAddress" runat="server" Width="50%" CssClass="textbox" />
                    </span>
                    <span class="span_caption">Ngày sinh: </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtBirthday" runat="server" Width="155px" CssClass="textbox" /> (Định dạng dd/MM/yyyy)
                    </span>
                    <span class="span_caption">Giới tính: </span>
                    <span class="span_value">
                        <asp:RadioButton ID="optNam" runat="server" Text="Nam" GroupName="groupGender" />
                        <asp:RadioButton ID="optNu" runat="server" Text="Nữ" GroupName="groupGender" />                        
                    </span>
                    <span class="span_caption">Tỉnh/thành phố: </span>
                    <span class="span_value">
                        <asp:DropDownList ID="cmbLocation" runat="server" Width="50%">
                        </asp:DropDownList>
                    </span>
                    <span class="span_caption">Tình trạng: </span>
                    <span class="span_value">
                        <asp:DropDownList ID="cmbStatus" runat="server" Width="50%">
                            <asp:ListItem Value="-1" Text="Bị khóa" />
                            <asp:ListItem Value="0" Text="Chưa kích hoạt tài khoản" />
                            <asp:ListItem Value="1" Text="Đã kích hoạt" />
                        </asp:DropDownList>
                        <br />
                        <i>Lưu ý: Tài khoản bị khóa sẽ không thể vào game. Vẫn được phép đăng nhập trang chủ, trang ID và diễn đàn</i>
                    </span>
                    <span class="span_caption">Câu hỏi bí mật: </span>
                    <span class="span_value">
                        <asp:DropDownList ID="cmbQuestion" runat="server" Width="50%">
                        </asp:DropDownList>
                    </span>
                    <span class="span_caption">Câu trả lời: </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtAnswer" runat="server" Width="50%" CssClass="textbox" />
                    </span>
                    <span class="span_caption">&nbsp;</span>
                    <span class="span_value">
                        <asp:Button ID="buttonSave" runat="server" text=" Cập nhật thông tin " CssClass="button" />
                        <asp:Button ID="buttonBack" runat="server" Text=" Quay lại " CssClass="button" />      
                        <asp:Label ID="labelInfoMessage" runat="server" Text="" ForeColor="#FF0000" />                 
                    </span>
                </p>           
            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="panelAccountPassword" runat="server">
            <fieldset>
                <legend>Thay đổi mật khẩu</legend>
                <p>
                    <span class="span_caption">Mật khẩu hiện tại: </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtPassNow" runat="server" Width="50%" CssClass="textbox" ReadOnly="true" />                        
                    </span>
                    <span class="span_caption">Nhập mật khẩu mới: </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtPassword" TextMode="Password"  runat="server" Width="50%" CssClass="textbox" />                        
                    </span>
                    <span class="span_caption">Xác nhận lại mật khẩu: </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtPassword2" TextMode="Password" runat="server" Width="50%" CssClass="textbox" />                        
                    </span>
                    <span class="span_caption">MD5 Password: </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtMd5Password" TextMode="Password" runat="server" Width="50%" CssClass="textbox" />                        
                    </span>
                    <span class="span_caption">&nbsp;</span>
                    <span class="span_value">
                        <asp:Button ID="buttonChangePass" runat="server" Text=" Đổi mật khẩu " CssClass="button" />     
                        <asp:Label ID="labelPassMessage" runat="server" Text="" ForeColor="#FF0000" />                   
                    </span>                    
                </p>
            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="panelGameAccount" runat="server">
            <fieldset>
                <legend>Tài khoản game</legend>
                <p>
                    <span class="span_caption">Trạng thái: </span>
                    <span class="span_value">
                        <asp:TextBox ID="txtStatus" ReadOnly="true" runat="server" Width="50%" CssClass="textbox" />                        
                    </span>
                    <span class="span_caption">Thiết lập: </span>
                    <span class="span_value">
                        <asp:DropDownList ID="ddlSetting" runat="server" Width="50%">
                            <asp:ListItem Value="0" Text="" Selected="True" />
                            <asp:ListItem Value="1" Text="Khóa tài khoản" />
                            <asp:ListItem Value="2" Text="Mở khóa tài khoản" />
                        </asp:DropDownList>
                    </span>
                    <span class="span_caption">Lý do: </span>
                    <span class="span_value">
                        <asp:DropDownList ID="ddlReason" runat="server" Width="50%">
                            <asp:ListItem Value="0" Text="" Selected="True" />
                            <asp:ListItem Value="1" Text="Hack BC" />
                            <asp:ListItem Value="2" Text="Rao link" />
                            <asp:ListItem Value="3" Text="Tạm thời khóa tranh chấp" />
                            <asp:ListItem Value="4" Text="Tạm khóa kiểm tra Hack" />
                            <asp:ListItem Value="5" Text="Khóa Vĩnh Viễn(Lý Do khác)" />
                        </asp:DropDownList>
                    </span>
                    <span class="span_caption">&nbsp;</span>
                    <span class="span_value">
                        <asp:Button ID="buttonConfirm" runat="server" Text=" Xác nhận " CssClass="button" />     
                        <asp:Label ID="label1" runat="server" Text="" ForeColor="#FF0000" />                   
                    </span>                    
                </p>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
