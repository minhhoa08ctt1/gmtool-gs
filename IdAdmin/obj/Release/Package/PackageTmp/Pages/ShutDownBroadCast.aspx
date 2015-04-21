<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ShutDownBroadCast.aspx.cs" Inherits="IDAdmin.Pages.ShutDownBroadCast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   THÔNG BÁO DỪNG SERVER
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <div class="page_title">
                THÔNG BÁO DỪNG SERVER
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>THÔNG BÁO DỪNG SERVER</legend><span class="span_caption">Số hiệu game: </span><span class="span_value">
                        <asp:TextBox ID="txtGameTypeView11" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Số hiệu cụm: </span><span class="span_value">
                        <asp:TextBox ID="txtZoneIdView11" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Nội dung thông báo: </span><span class="span_value">
                        <asp:TextBox ID="txtContentView11" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Thời gian dừng server: </span><span class="span_value">
                        <asp:TextBox ID="txtShutDownTimeView11" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:CheckBox ID="checkAcceptView11" runat="server" Text="Đã kiểm tra và chấp nhận thông báo" />
                        <asp:Label ID="labelCheckMessageView11" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAcceptView11" runat="server" CssClass="button" Text=" Thông báo " OnClientClick="return confirm('Bạn đồng ý thông báo không ?')"
                            OnClick="buttonAcceptView11_Click" />
                        
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView11" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
</asp:Content>