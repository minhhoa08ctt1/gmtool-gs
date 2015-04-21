<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="WorkDataStatistics.aspx.cs"
    Inherits="IDAdmin.Pages.WorkDataStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Thống kê số liệu vận hành
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">

    <script type="text/javascript">
        $(function() {
            $("#<%= txtFromDate.ClientID %>").datepick({ dateFormat: 'dd/mm/yyyy' });
            $("#<%= txtToDate.ClientID %>").datepick({ dateFormat: 'dd/mm/yyyy' });
        });    
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid">
        <fildset>
       <legend>Phân tích người chơi</legend>
        <div class="row">
            <asp:LinkButton ID="onlinePeopleNumLK" runat="server" OnClick="onlinePeppleNumLK_Click">Số người online(1)</asp:LinkButton>
            <asp:LinkButton ID="loginDataLK" runat="server" OnClick="loginDataLK_Click">Số liệu login(2)</asp:LinkButton>
            <asp:LinkButton ID="loginNewSaveLB" runat="server" 
                OnClick="loginNewSaveLB_Click">Tỉ lệ login mới lưu lại(4)</asp:LinkButton>
        </div>
        </fildset>
        <div class="row">
        <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
        <asp:Button ID="GetData" runat="server" Text="Get online number data" OnClick="GetData_Click" />
        <asp:Button ID="GetLoginData" runat="server" Text="Get login data" onclick="GetLoginData_Click" />
        </div>
        <asp:MultiView ID="container" runat="server">
            <asp:View ID="onlinePeopleNumView" runat="server">
                <div class="row">
                    <asp:DropDownList ID="statisticsTypeDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="statisticsTypeDropDownList_SelectedIndexChanged">
                        <asp:ListItem Value="0">Thực thời: CCU</asp:ListItem>
                        <asp:ListItem Value="1">Bình quân: ACU</asp:ListItem>
                        <asp:ListItem Value="2">Giá trị đỉnh: PCU</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="Count" runat="server" Text="Thống kê" OnClick="Count_Click" />
                    
                </div>
                <div class="row">
                    <asp:MultiView ID="MultiView" runat="server">
                        <asp:View ID="ccuView" runat="server">
                            <asp:Panel ID="onlinePeoplePanelList" runat="server" />
                        </asp:View>
                        <asp:View ID="ccuAverage" runat="server">
                            <asp:Panel ID="ccuAveragePanel" runat="server" />
                        </asp:View>
                        <asp:View ID="ccuMax" runat="server">
                            <asp:Panel ID="ccuMaxPanel" runat="server" />
                        </asp:View>
                    </asp:MultiView>
                </div>
            </asp:View>
            <asp:View ID="loginDataView" runat="server">
                <div class="row">
                    <asp:Button ID="CountLoginData" runat="server" Text="Thống kê" onclick="CountLoginData_Click" />
                </div>
                <div class="row">
                    <asp:Panel ID="loginDataPanel" runat="server">
                    </asp:Panel>
                </div>
            </asp:View>
             <asp:View ID="LogiNewSaveView" runat="server">
                <div class="row">
                    <asp:Button ID="LogiNewSaveButton" runat="server" Text="Thống kê" 
                        onclick="LogiNewSaveButton_Click" />
                </div>
                <div class="row">
                    <asp:Panel ID="LogiNewSavePanel" runat="server">
                    </asp:Panel>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
<asp:Label ID="measureLB" runat="server" Text=""></asp:Label>
</asp:Content>
