<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuBar.ascx.cs" Inherits="IDAdmin.Controls.MenuBar" %>
<div id="header">
    <asp:Label ID="labelHeader" runat="server" Text="GOSU"></asp:Label>
</div>
<div id="menu">
    <ul id="menubar">
        <%--<li>
            <a href="#">Hệ thống</a>
            <ul>
                <li><a href="../Pages/ApiGH.aspx">Api giang hồ</a></li>
                <li><a href="../Pages/WorkDataStatistics.aspx">Thống kê số liệu vận hành giang hồ</a></li>

            </ul>
        </li>--%>
        <li>
            <asp:HyperLink ID="linkLogOut" runat="server" NavigateUrl="~/Pages/LogOut.aspx">Thoát</asp:HyperLink>
        </li>
    </ul>
</div>
<div class="clear">
</div>

