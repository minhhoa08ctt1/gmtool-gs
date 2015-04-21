<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ApiGH.aspx.cs" Inherits="IDAdmin.Pages.ApiGH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    API giang hồ game
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:DropDownList ID="DropDownListFunction" runat="server" AutoPostBack="True" DataTextField="Name" DataValueField="Value"
            OnSelectedIndexChanged="DropDownListFunction_SelectedIndexChanged">
            <asp:ListItem Value="0">Đền bù cho người chơi</asp:ListItem>
            <asp:ListItem Value="1">Đá người chơi khỏi mạng</asp:ListItem>
            <asp:ListItem Value="2">Khóa nhân vật</asp:ListItem>
            <asp:ListItem Value="3">Mở khóa nhân vật</asp:ListItem>
            <asp:ListItem Value="4" Enabled="False">Kiểm tra cụm đơn</asp:ListItem>
            <asp:ListItem Value="5" Enabled="False">Kiểm tra hết các cụm</asp:ListItem>
            <asp:ListItem Value="6" Enabled="False">Nhận danh sách server game</asp:ListItem>
            <asp:ListItem Value="7" Enabled="False">Kiểm tra thông tin</asp:ListItem>
            <asp:ListItem Value="8" Enabled="False">Sửa thông tin thông dụng</asp:ListItem>
            <asp:ListItem Value="9" Enabled="False">Đếm số người đăng nhập ngày nào đó</asp:ListItem>
            <asp:ListItem Value="10">Thông Báo Dừng Server</asp:ListItem>
            <asp:ListItem Value="11">Thông Báo Quan Trọng</asp:ListItem>
            <asp:ListItem Value="12">Thông Báo Thường</asp:ListItem>
        </asp:DropDownList>
    </div>
    <asp:MultiView ID="MultiViewApiGH" runat="server">
        <asp:View ID="redeemView" runat="server">
            <div class="page_title">
                ĐỀN BÙ CHO NGƯỜI CHƠI
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>ĐỀN BÙ CHO NGƯỜI CHƠI</legend><span class="span_caption">Số hiệu game: </span><span class="span_value">
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
                        <asp:Button ID="buttonAcceptView1" runat="server" CssClass="button" Text=" Đền bù " OnClientClick="return confirm('Bạn đồng ý đền bù không?')"
                            OnClick="buttonAcceptView1_Click" />
                        <asp:Button ID="buttonCancelView1" runat="server" CssClass="button" Text=" Quay lại " OnClick="buttonCancelView1_Click" />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView1" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp;
                        <asp:Label ID="labelResultView1" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
        </asp:View>
        <asp:View ID="punishView" runat="server">
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
                        <asp:Button ID="buttonAcceptView2" runat="server" CssClass="button" Text=" Đá ngừoi chơi " OnClientClick="return confirm('Bạn đồng ý đền bù không?')"
                            OnClick="buttonAcceptView2_Click" />
                        <asp:Button ID="buttonCancelView2" runat="server" CssClass="button" Text=" Quay lại " OnClick="buttonCancelView2_Click" />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView2" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
        </asp:View>
        <asp:View ID="lockRoleView" runat="server">
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
                    </span><span class="span_caption">Timestamp khóa đến kỳ: </span><span class="span_value">
                        <asp:TextBox ID="txtTimeLimitView3" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:CheckBox ID="checkAcceptView3" runat="server" Text="Đã kiểm tra và chấp nhận khóa nhân vật" />
                        <asp:Label ID="labelCheckMessageView3" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAcceptView3" runat="server" CssClass="button" Text=" Khóa " OnClientClick="return confirm('Bạn đồng ý đền bù không?')"
                            OnClick="buttonAcceptView3_Click" />
                        <asp:Button ID="buttonCancelView3" runat="server" CssClass="button" Text=" Quay lại " OnClick="buttonCancelView3_Click" />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView3" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
        </asp:View>
        <asp:View ID="unLockRoleView" runat="server">
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
                        <asp:Button ID="buttonAcceptView4" runat="server" CssClass="button" Text=" Mở khóa " OnClientClick="return confirm('Bạn đồng ý đền bù không?')"
                            OnClick="buttonAcceptView4_Click" />
                        <asp:Button ID="buttonCancelView4" runat="server" CssClass="button" Text=" Quay lại " OnClick="buttonCancelView4_Click" />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView4" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
        </asp:View>
        <asp:View ID="getOneView" runat="server">
            <div class="page_title">
                KIỂM TRA CỤM ĐƠN
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>KIỂM TRA CỤM ĐƠN</legend><span class="span_caption">Số hiệu cụm: </span><span class="span_value">
                        <asp:TextBox ID="txtZoneIdView5" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAccepView5t" runat="server" CssClass="button" Text=" Kiểm tra " OnClick="buttonAcceptView5_Click" />
                        <asp:Button ID="buttonCancelView5" runat="server" CssClass="button" Text=" Quay lại " />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView5" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
        </asp:View>
        <asp:View ID="getAllView" runat="server">
            <div class="page_title">
                KIỂM TRA CỤM ĐƠN
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>KIỂM TRA HẾT CÁC CỤM</legend><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAcceptView6" runat="server" CssClass="button" Text=" Kiểm tra " OnClick="buttonAcceptView6_Click" />
                        <asp:Button ID="buttonCancelView6" runat="server" CssClass="button" Text=" Quay lại " />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView6" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
        </asp:View>
        <asp:View ID="getServerListView" runat="server">
            <div class="page_title">
                NHẬN DANH SÁCH SERVER GAME
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>NHẬN DANH SÁCH SERVER GAME</legend><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAcceptView7" runat="server" CssClass="button" Text=" Nhận danh sách " OnClick="buttonAcceptView7_Click" />
                        <asp:Button ID="buttonCancelView7" runat="server" CssClass="button" Text=" Quay lại " />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView7" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp;
                        <asp:Label ID="txtServerList" runat="server" ForeColor="#FF0000"></asp:Label>
                    </span>
                </fieldset>
            </div>
        </asp:View>
        <asp:View ID="queryCharInfoView" runat="server">
            <div class="page_title">
                Kiểm tra thông tin
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>Kiểm tra thông tin</legend><span class="span_caption">Số hiệu game: </span><span class="span_value">
                        <asp:TextBox ID="txtGameTypeView8" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Số hiệu cụm: </span><span class="span_value">
                        <asp:TextBox ID="txtZoneIdView8" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Loại thông tin: </span><span class="span_value">
                        <asp:DropDownList ID="DropDownListTypeView8" runat="server" DataTextField="Title" DataValueField="Value">
                            <asp:ListItem Value="0">Xem thông tin cơ bản Theo tài khoản </asp:ListItem>
                            <asp:ListItem Value="1">Xem thông tin cơ bản theo nickname  </asp:ListItem>
                            <asp:ListItem Value="2">Xem thông tin trang bị theo nickname </asp:ListItem>
                            <asp:ListItem Value="3">Xem thông tin túi theo nickname </asp:ListItem>
                            <asp:ListItem Value="4">Xem thông tin tọa kỵ theo nickname </asp:ListItem>
                            <asp:ListItem Value="5">Xem thông tin võ tướng theo nickname </asp:ListItem>
                            <asp:ListItem Value="6">Xem thông tin bang theo tên bang </asp:ListItem>
                            <asp:ListItem Value="7">Xem thông tin chi tiết bang theo tên bang </asp:ListItem>
                            <asp:ListItem Value="8">Xem số lượng tạo nhân vật theo ngày tháng </asp:ListItem>
                            <asp:ListItem Value="9">Xem số lượng nghề theo ngày tháng </asp:ListItem>
                            <asp:ListItem Value="10">Xem số lượng quốc gia theo ngày tháng </asp:ListItem>
                            <asp:ListItem Value="11">Thông tin nạp thẻ </asp:ListItem>
                            <asp:ListItem Value="12">Nhận số người dùng sôi nổi </asp:ListItem>
                        </asp:DropDownList>
                    </span><span class="span_caption">Giá trị: </span><span class="span_value">
                        <asp:TextBox ID="txtKeyView8" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAcceptView8" runat="server" CssClass="button" Text=" Kiểm tra " OnClick="buttonAcceptView8_Click" />
                        <asp:Button ID="buttonCancelView8" runat="server" CssClass="button" Text=" Quay lại " />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView8" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
        </asp:View>
        <asp:View ID="commonWebCommandView" runat="server">
            <div class="page_title">
                SỬA THÔNG TIN THÔNG DỤNG
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>SỬA THÔNG TIN THÔNG DỤNG</legend><span class="span_caption">Số hiệu game: </span><span class="span_value">
                        <asp:TextBox ID="txtGameTypeView9" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Số hiệu cụm: </span><span class="span_value">
                        <asp:TextBox ID="txtZoneIdView9" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Tên nhân vật: </span><span class="span_value">
                        <asp:TextBox ID="txtNameView9" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Chọn câu lệnh: </span><span class="span_value">
                        <asp:DropDownList ID="DropDownListCommandView9" runat="server" AutoPostBack="True" DataTextField="Title" DataValueField="Value"
                            OnSelectedIndexChanged="DropDownListCommandView9_SelectedIndexChanged">
                            <asp:ListItem Value="zyz act=addobj typeID={0} num={1}" Selected="True">Thêm Đạo cụ</asp:ListItem>
                            <asp:ListItem Value="zyz act=addMoney num={0}">Thêm vàng</asp:ListItem>
                            <asp:ListItem Value="zyz act=addBindMoney num={0}">Thêm đồng</asp:ListItem>
                            <asp:ListItem Value="levelup num={0}">Tăng cấp</asp:ListItem>
                            <asp:ListItem Value="kick name={0}">Đá khỏi mạng</asp:ListItem>
                            <asp:ListItem Value="donttalk name={0} time={1}">Cấm người chơi nói</asp:ListItem>
                        </asp:DropDownList>
                    </span><span id="spanLeft" runat="server" class="span_caption">
                        <asp:PlaceHolder ID="panelLeftView9" runat="server"></asp:PlaceHolder>
                    </span><span id="spanRight" runat="server" class="span_value">
                        <asp:PlaceHolder ID="panelRightView9" runat="server"></asp:PlaceHolder>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:CheckBox ID="checkAcceptView9" runat="server" Text="Đã kiểm tra và chấp nhận thực hiện câu lệnh" />
                        <asp:Label ID="labelCheckMessageView9" runat="server" ForeColor="Red"></asp:Label>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAcceptView9" runat="server" CssClass="button" Text=" Sửa " OnClientClick="return confirm('Bạn đồng ý thực hiện câu lệnh')"
                            OnClick="buttonAcceptView9_Click" />
                        <asp:Button ID="buttonCancelView9" runat="server" CssClass="button" Text=" Quay lại " />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView9" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
        </asp:View>
        <asp:View ID="dayLoginCountView" runat="server">
            <div class="page_title">
                ĐẾM SỐ NGƯỜI ĐĂNG NHẬP CỦA NGÀY
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>ĐẾM SỐ NGƯỜI ĐĂNG NHẬP CỦA NGÀY</legend><span class="span_caption">Số hiệu game: </span><span class="span_value">
                        <asp:TextBox ID="txtGameTypeView10" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Số hiệu cụm: </span><span class="span_value">
                        <asp:TextBox ID="txtZoneIdView10" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Ngày bắt đầu: </span><span class="span_value">
                        <asp:TextBox ID="txtDateView10" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Ngày kết thúc: </span><span class="span_value">
                        <asp:TextBox ID="txtEndDateView10" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAcceptView10" runat="server" CssClass="button" Text=" Đếm " OnClick="buttonAcceptView10_Click" />
                        <asp:Button ID="buttonCancelView10" runat="server" CssClass="button" Text=" Quay lại " />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView10" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp;<br />
                        <asp:Label ID="dateLogicCount" runat="server" ForeColor="#FF0000"></asp:Label>
                    </span>
                </fieldset>
            </div>
        </asp:View>
        <asp:View ID="shutdownBroadcastView" runat="server">
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
                        <asp:Button ID="buttonCancelView11" runat="server" CssClass="button" Text=" Quay lại " OnClick="buttonCancelView11_Click" />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView11" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
        </asp:View>
        <asp:View ID="importanceBroadcastView" runat="server">
            <div class="page_title">
                THÔNG BÁO QUAN TRỌNG
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>THÔNG BÁO QUAN TRỌNG</legend><span class="span_caption">Số hiệu game: </span><span class="span_value">
                        <asp:TextBox ID="txtGameTypeView12" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Số hiệu cụm: </span><span class="span_value">
                        <asp:TextBox ID="txtZoneIdView12" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Nội dung thông báo: </span><span class="span_value">
                        <asp:TextBox ID="txtContentView12" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Thời gian hiển thị thông báo(phút): </span><span class="span_value">
                        <asp:TextBox ID="txtEffectTimeView12" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:CheckBox ID="checkAcceptView12" runat="server" Text="Đã kiểm tra và chấp nhận thông báo" />
                        <asp:Label ID="labelCheckMessageView12" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAcceptView12" runat="server" CssClass="button" Text=" Thông báo " OnClientClick="return confirm('Bạn đồng ý thông báo không ?')"
                            OnClick="buttonAcceptView12_Click" />
                        <asp:Button ID="buttonCancelView12" runat="server" CssClass="button" Text=" Quay lại " OnClick="buttonCancelView12_Click" />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView12" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
        </asp:View>
        <asp:View ID="normalBroadcastView" runat="server">
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
                    </span><span class="span_caption">Bao nhiêu phút thì thông báo một lần: </span><span class="span_value">
                        <asp:TextBox ID="txtIntervalMinute" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Thời điểm kết thúc: </span><span class="span_value">
                        <asp:TextBox ID="txtEndDateTime" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:CheckBox ID="checkAcceptView13" runat="server" Text="Đã kiểm tra và chấp nhận thông báo" />
                        <asp:Label ID="labelCheckMessageView13" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAcceptView13" runat="server" CssClass="button" Text=" Thông báo " OnClientClick="return confirm('Bạn đồng ý thông báo không ?')"
                            OnClick="buttonAcceptView13_Click" />
                        <asp:Button ID="buttonCancelView13" runat="server" CssClass="button" Text=" Quay lại " OnClick="buttonCancelView13_Click" />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:ScriptManager ID="ScriptManager1" runat="server" />
                        <asp:Timer runat="server" ID="UpdateTimer" ontick="UpdateTimer_Tick" />
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
        </asp:View>
        <asp:View ID="redeemGiftPacketView" runat="server">
            <div class="page_title">
                BÁN ĐẠO CỤ QUÀ
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>BÁN ĐẠO CỤ QUÀ</legend><span class="span_caption">Số hiệu game: </span><span class="span_value">
                        <asp:TextBox ID="txtGameTypeView14" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Số hiệu cụm: </span><span class="span_value">
                        <asp:TextBox ID="txtZoneIdView14" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Tài khoản chữ số của người chơi: </span><span class="span_value">
                        <asp:TextBox ID="txtUidView14" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">ID nhân vật: </span><span class="span_value">
                        <asp:TextBox ID="txtCharIdView14" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Tên nhân vật: </span><span class="span_value">
                        <asp:TextBox ID="txtNameView14" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Loại hình đền bù: </span><span class="span_value">
                        <asp:DropDownList ID="DropDownListReTypeView14" runat="server" DataTextField="Title" DataValueField="Value" AutoPostBack="True"
                            OnSelectedIndexChanged="DropDownListReTypeView14_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0">Ngân lượng</asp:ListItem>
                            <asp:ListItem Value="1">Vàng</asp:ListItem>
                            <asp:ListItem Value="2">Lương </asp:ListItem>
                            <asp:ListItem Value="3">Thời gian </asp:ListItem>
                            <asp:ListItem Value="4">Ngân lượng khóa</asp:ListItem>
                            <asp:ListItem Value="5">Ngân phiếu</asp:ListItem>
                            <asp:ListItem Value="6">Kim phiếu</asp:ListItem>
                            <asp:ListItem Value="7">Đạo cụ</asp:ListItem>
                            <asp:ListItem Value="8">Thẻ tháng</asp:ListItem>
                        </asp:DropDownList>
                    </span><span class="span_caption">Số lượng đền bù: </span><span class="span_value">
                        <asp:TextBox ID="txtNumView14" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">ID đạo cụ: </span><span class="span_value">
                        <asp:TextBox ID="txtItemIdView14" runat="server" CssClass="textbox" Width="300px" Font-Bold="true"></asp:TextBox>
                    </span><span class="span_caption">Khóa không: </span><span class="span_value">
                        <asp:DropDownList ID="DropDownListIsBindView14" runat="server" DataTextField="Title" DataValueField="Value">
                            <asp:ListItem Selected="True" Value="0">Không khóa</asp:ListItem>
                            <asp:ListItem Value="1">Khóa</asp:ListItem>
                        </asp:DropDownList>
                    </span><span class="span_caption">Cấp đạo cụ:</span> <span class="span_value">
                        <asp:TextBox ID="txtLevelView14" runat="server" CssClass="textbox" Width="300px" Font-Bold="true">
                        </asp:TextBox>
                    </span></span><span class="span_caption">Phiếu điểm tiêu tốn:</span> <span class="span_value">
                        <asp:TextBox ID="txtPointView14" runat="server" CssClass="textbox" Width="300px" Font-Bold="true">
                        </asp:TextBox>
                    </span></span><span class="span_caption">Mà Thương Thành(giống mã game):</span> <span class="span_value">
                        <asp:TextBox ID="txtSourceView14" runat="server" CssClass="textbox" Width="300px" Font-Bold="true">
                        </asp:TextBox>
                    </span></span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:CheckBox ID="checkAcceptView14" runat="server" Text="Đã kiểm tra và chấp nhận đền bù" />
                        <asp:Label ID="labelCheckMessageView14" runat="server" Text="" ForeColor="#FF0000"></asp:Label>
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Button ID="buttonAcceptView14" runat="server" CssClass="button" Text=" Bán " OnClientClick="return confirm('Bạn đồng ý đền bù không?')"
                            OnClick="buttonAcceptView14_Click" />
                        <asp:Button ID="buttonCancelView14" runat="server" CssClass="button" Text=" Quay lại " />
                    </span><span class="span_caption">&nbsp;</span> <span class="span_value">
                        <asp:Label ID="labelMessageView14" runat="server" ForeColor="#FF0000"></asp:Label>&nbsp; </span>
                </fieldset>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
