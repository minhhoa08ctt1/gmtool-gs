<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="NormalBroadCast.aspx.cs" Inherits="IDAdmin.Pages.NormalBroadCast" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   THÔNG BÁO THƯỜNG
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
                THÔNG BÁO THƯỜNG
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>THÔNG BÁO THƯỜNG</legend><span class="span_caption">Số hiệu game: </span><span class="span_value">
                        <asp:TextBox ID="txtGameTypeView13" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Số hiệu cụm: </span><span class="span_value">
                        <asp:TextBox ID="txtZoneIdView13" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Nội dung thông báo: </span><span class="span_value">
                        <asp:TextBox ID="txtContentView13" runat="server" CssClass="textbox" Width="300px" Font-Bold="true" Height="100px" TextMode="MultiLine"></asp:TextBox>
                    </span><span class="span_caption">Bao nhiêu phút thì tb một lần: </span><span class="span_value">
                        <asp:TextBox ID="txtIntervalMinute" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Thời điểm kết thúc: </span><span class="span_value">
                        <asp:TextBox ID="txtEndDateTime" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server" Text="Định dạng: dd/mm/yyyy hh:mm:ss"></asp:Label>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:CheckBox ID="checkAcceptView13" runat="server" Text="Đã kiểm tra và chấp nhận thông báo" />
                        <asp:Label ID="labelCheckMessageView13" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAcceptView13" runat="server" CssClass="button" Text=" Thông báo " OnClientClick="return confirm('Bạn đồng ý thông báo không ?')"
                            OnClick="buttonAcceptView13_Click" />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:ScriptManager ID="ScriptManager1" runat="server" />
                        <asp:Timer runat="server" ID="UpdateTimer" OnTick="UpdateTimer_Tick" />
                        <asp:UpdatePanel runat="server" ID="TimedPanel" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="UpdateTimer" EventName="Tick" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="labelMessageView13" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </span>
                </fieldset>
            </div>
</asp:Content>