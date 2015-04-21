<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Redeem.aspx.cs" Inherits="IDAdmin.Pages.Redeem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ĐỀN BÙ CHO NGƯỜI CHƠI
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
                ĐỀN BÙ CHO NGƯỜI CHƠI
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>ĐỀN BÙ CHO NGƯỜI CHƠI</legend>
                    <span class="span_caption">Nội dung đền bù: </span><span class="span_value">
                        <asp:TextBox ID="txtTitleView1" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                     </span><span class="span_caption">Số hiệu game: </span><span class="span_value">
                        <asp:TextBox ID="txtGameTypeView1" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Số hiệu cụm: </span><span class="span_value">
                        <asp:TextBox ID="txtZoneIdView1" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Tài khoản chữ số của người chơi: </span><span class="span_value">
                        <asp:TextBox ID="txtUidView1" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">ID nhân vật: </span><span class="span_value">
                        <asp:TextBox ID="txtCharIdView1" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Tên nhân vật: </span><span class="span_value">
                        <asp:TextBox ID="txtNameView1" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Loại hình đền bù: </span><span class="span_value">
                        <asp:DropDownList ID="DropDownListReTypeView1" runat="server" DataTextField="Title" DataValueField="Value" AutoPostBack="True"
                            OnSelectedIndexChanged="DropDownListReTypeView1_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0" Enabled="False">Ngân lượng</asp:ListItem>
                            <asp:ListItem Value="1" Enabled="False">Vàng</asp:ListItem>
                            <asp:ListItem Value="2" Enabled="False">Lương </asp:ListItem>
                            <asp:ListItem Value="3" Enabled="False">Thời gian </asp:ListItem>
                            <asp:ListItem Value="4" Enabled="False">Ngân lượng khóa</asp:ListItem>
                            <asp:ListItem Value="5" Enabled="False">Ngân phiếu</asp:ListItem>
                            <asp:ListItem Value="6" Enabled="False">Kim phiếu</asp:ListItem>
                            <asp:ListItem Value="7" Selected="True">Đạo cụ</asp:ListItem>
                            <asp:ListItem Value="8" Enabled="False">Thẻ tháng</asp:ListItem>
                        </asp:DropDownList>
                    </span><span class="span_caption">Số lượng đền bù: </span><span class="span_value">
                        <asp:TextBox ID="txtNumView1" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">ID đạo cụ: </span><span class="span_value">
                        <asp:TextBox ID="txtItemIDView1" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Khóa không: </span><span class="span_value">
                        <asp:DropDownList ID="DropDownListIsBindView1" runat="server" DataTextField="Title" DataValueField="Value">
                            <asp:ListItem Selected="True" Value="0">Không khóa</asp:ListItem>
                            <asp:ListItem Value="1">Khóa</asp:ListItem>
                        </asp:DropDownList>
                    </span><span class="span_caption">Cấp đạo cụ:</span> <span class="span_value">
                        <asp:TextBox ID="txtUnitView1" runat="server" CssClass="textbox" Width="300px" Font-Bold="true">
                        </asp:TextBox>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:CheckBox ID="checkAcceptView1" runat="server" Text="Đã kiểm tra và chấp nhận đền bù" />
                        <asp:Label ID="labelCheckMessageView1" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAcceptView1" runat="server" CssClass="button" Text="Yêu cầu đền bù " OnClientClick="return confirm('Bạn đồng ý đền bù không?')"
                            OnClick="buttonAcceptView1_Click" />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView1" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp;
                        <asp:Label ID="labelResultView1" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
</asp:Content>