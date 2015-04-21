<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="TransHistoryDetails.aspx.cs" Inherits="IDAdmin.Pages.TransHistoryDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Thông tin chi tiết giao dịch nạp vàng
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        TRA CỨU, XỬ LÝ GIAO DỊCH NẠP VÀNG
    </div>
    <div class="page_bar" style="text-align:right; width:98%">
        <a href="javascript:history.go(-1)"> <b>[Quay lại trang trước]</b></a>        
    </div> 
    <div class="page_content">
        <table style="width:98%; margin:0 auto" class="table1">
            <tr>
                <td colspan="2" class="cellHeader">
                    Thông tin về giao dịch
                </td>
            </tr>
            <tr>
                <td style="line-height:20px; width:25%;text-align:right" valign="top">
                    Mã giao dịch:<br />
                    Thời điểm giao dịch:<br />
                    Tài khoản nhận tiền:<br />
                    Người nạp tiền:<br />
                    Số tiền:<br />
                    Số vàng trong game:<br />
                    Máy chủ:<br />
                    Hình thức nạp tiền:<br />
                    Serial:<br />
                    Mã PIN:<br />
                    Mã trạng thái (Status):<br />
                    Mã lỗi (Error Code):<br />
                    IP khách hàng:<br />                
                </td>
                <td style="line-height:20px; width:75%;text-align:left" valign="top">
                    <asp:TextBox ID="txtExchangeID" runat="server" Width="60%" ReadOnly="true"></asp:TextBox><br />
                    <asp:TextBox ID="txtCreated" runat="server" CssClass="textbox" Width="60%" ReadOnly="true"></asp:TextBox><br />
                    <asp:TextBox ID="txtAccount" runat="server" CssClass="textbox" Width="60%" ReadOnly="true"></asp:TextBox><br />
                    <asp:TextBox ID="txtCustomerID" runat="server" CssClass="textbox" Width="60%" ReadOnly="true"></asp:TextBox><br />
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox" Width="60%" ReadOnly="true"></asp:TextBox><br />
                    <asp:TextBox ID="txtGold" runat="server" CssClass="textbox" Width="60%" ReadOnly="true"></asp:TextBox><br />
                    <asp:TextBox ID="txtServer" runat="server" CssClass="textbox" Width="60%" ReadOnly="true"></asp:TextBox><br />
                    <asp:TextBox ID="txtType" runat="server" CssClass="textbox" Width="60%" ReadOnly="true"></asp:TextBox><br />
                    <asp:TextBox ID="txtSerial" runat="server" CssClass="textbox" Width="60%" ReadOnly="true"></asp:TextBox><br />
                    <asp:TextBox ID="txtPinNumber" runat="server" CssClass="textbox" Width="60%" ReadOnly="true"></asp:TextBox><br />
                    <asp:TextBox ID="txtStatus" runat="server" CssClass="textbox" Width="60%" ReadOnly="true"></asp:TextBox><br />
                    <asp:TextBox ID="txtErrorCode" runat="server" CssClass="textbox" Width="60%" ReadOnly="true"></asp:TextBox><br />
                    <asp:TextBox ID="txtIP" runat="server" CssClass="textbox" Width="60%" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>    
        </table>
        <br />
         <table style="width:98%; margin:0 auto" class="table1">
            <tr>
                <td colspan="2" class="cellHeader">
                    Thông tin về quá trình xử lý qua cổng nạp vàng của đối tác
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel id="panelGateLog" runat="server" Width="100%">                    
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <br />
        <asp:Panel ID="panelTransEdit" runat="server" Visible="false">
            <table style="width:98%; margin:0 auto" class="table1">
                <tr>
                    <td colspan="2" class="cellHeader">
                        Kết quả truy vấn giao dịch thẻ và xử lý kết quả (sử dụng cho xử lý giao dịch bị lỗi)
                    </td>
                </tr>
                <tr>
                    <td style="line-height:20px; width:25%;text-align:right" valign="top">
                        Kết quả truy vấn giao dịch từ cổng nạp thẻ:
                    </td>
                    <td>
                        <asp:TextBox ID="txtQueryResult" runat="server" CssClass="textbox" Width="60%" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="line-height:20px; width:25%;text-align:right" valign="top">
                        Server:<br />
                        Số tiền:<br />
                        Chọn hình thức xử lý phù hợp:<br />                                                
                    </td>
                    <td style="line-height:20px; width:75%;text-align:left" valign="top">
                        <asp:TextBox ID="txtEditServer" runat="server" CssClass="textbox" Width="60%" ReadOnly="true"></asp:TextBox>
                        <br />
                        <asp:TextBox ID="txtEditAmount" runat="server" CssClass="textbox" Width="60%"></asp:TextBox>
                        <br />
                        <asp:DropDownList ID="cmbEditSatus" runat="server" Width="60%" CssClass="textbox">
                            <asp:ListItem Value = "" Text="-- Chọn hình thức xử lý --"></asp:ListItem>
                            <asp:ListItem Value = "Succeed" Text="Giao dịch thành công"></asp:ListItem>
                            <asp:ListItem Value = "NotSucceed" Text="Giao dịch không thành công (thẻ không hợp lệ)"></asp:ListItem>
                            <asp:ListItem Value = "Pending" Text="Chuyển sang trạng thái chờ nạp vàng vào game (CẨN THẬN)"></asp:ListItem>                            
                        </asp:DropDownList>
                        <br />
                        <asp:CheckBox ID="chkAccept" runat="server" Text="Xác nhận đồng ý sửa log giao dịch" />
                        <br />
                        <asp:Button ID="buttonEditTrans" runat="server" Text="Sửa đổi giao dịch" OnClick="buttonEditTrans_Click" />
                        <br />
                        <asp:Label ID="labelEditMessage" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div class="page_content">
    </div>
</asp:Content>
