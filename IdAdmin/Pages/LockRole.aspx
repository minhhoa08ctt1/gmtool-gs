<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="LockRole.aspx.cs" Inherits="IDAdmin.Pages.LockRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
     KHÓA NHÂN VẬT
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <div class="page_title">
                KHÓA NHÂN VẬT
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>KHÓA NHÂN VẬT</legend><span class="span_caption">Số hiệu game: </span><span class="span_value">
                        <asp:TextBox ID="txtGameTypeView3" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Số hiệu cụm: </span><span class="span_value">
                        <asp:TextBox ID="txtZoneIdView3" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Tài khoản chữ số của người chơi: </span><span class="span_value">
                        <asp:TextBox ID="txtAccIdView3" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Timestamp khóa đến kỳ(giây): </span><span class="span_value">
                        <asp:TextBox ID="txtTimeLimitView3" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:CheckBox ID="checkAcceptView3" runat="server" Text="Đã kiểm tra và chấp nhận khóa nhân vật" />
                        <asp:Label ID="labelCheckMessageView3" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAcceptView3" runat="server" CssClass="button" Text=" Khóa " OnClientClick="return confirm('Bạn đồng ý khóa nhân vật không?')"
                            OnClick="buttonAcceptView3_Click" />
                        
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView3" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
</asp:Content>