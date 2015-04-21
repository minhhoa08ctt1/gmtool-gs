<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="UnlockRole.aspx.cs" Inherits="IDAdmin.Pages.UnlockRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    MỞ KHÓA NHÂN VẬT
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <div class="page_title">
                MỞ KHÓA NHÂN VẬT
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>MỞ KHÓA NHÂN VẬT</legend><span class="span_caption">Số hiệu game: </span><span class="span_value">
                        <asp:TextBox ID="txtGameTypeView4" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Số hiệu cụm: </span><span class="span_value">
                        <asp:TextBox ID="txtZoneIdView4" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Tài khoản chữ số của người chơi: </span><span class="span_value">
                        <asp:TextBox ID="txtAccIdView4" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:CheckBox ID="checkAcceptView4" runat="server" Text="Đã kiểm tra và chấp nhận mở khóa nhân vật" />
                        <asp:Label ID="labelCheckMessageView4" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAcceptView4" runat="server" CssClass="button" Text=" Mở khóa " OnClientClick="return confirm('Bạn đồng ý mở khóa nhân vật không?')"
                            OnClick="buttonAcceptView4_Click" />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView4" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
</asp:Content>
