<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Punish.aspx.cs" Inherits="IDAdmin.Pages.Punish" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ĐÁ NGƯỜI CHƠI RA KHỎI MẠNG
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
                ĐÁ NGƯỜI CHƠI RA KHỎI MẠNG
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>ĐÁ NGƯỜI CHƠI RA KHỎI MẠNG</legend><span class="span_caption">Số hiệu game: </span><span class="span_value">
                        <asp:TextBox ID="txtGameTypeView2" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Số hiệu cụm: </span><span class="span_value">
                        <asp:TextBox ID="txtZoneIdView2" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Tài khoản chữ số của người chơi: </span><span class="span_value">
                        <asp:TextBox ID="txtAccIdView2" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:CheckBox ID="checkAcceptView2" runat="server" Text="Đã kiểm tra và chấp nhận đá người chơi ra khỏi mạng" />
                        <asp:Label ID="labelCheckMessageView2" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAcceptView2" runat="server" CssClass="button" Text=" Đá người chơi " OnClientClick="return confirm('Bạn đồng ý đền bù không?')"
                            OnClick="buttonAcceptView2_Click" />
                        
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView2" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
</asp:Content>
