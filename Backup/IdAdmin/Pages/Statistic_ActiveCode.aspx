<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" CodeBehind="Statistic_ActiveCode.aspx.cs" Inherits="IDAdmin.Pages.Statistic_ActiveCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý Active Code
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        QUẢN LÝ ACTIVE CODE
    </div>    
    <div class="page_bar">
        <table width="auto" cellpadding="1" cellspacing="1" style="border:0px">
            <tr>
                <td>Serial:</td>
                <td> <asp:TextBox ID="txtSerial" runat="server" Width="150px" CssClass="textbox" value="" /></td>
                <td> &nbsp;&nbsp; Account:</td>
                <td> <asp:TextBox ID="txtAccount" runat="server" Width="150px" CssClass="textbox" value="" /></td>
                <td> <asp:Button ID="buttonExecute" runat="server" CssClass="button" Text="  Xem  " /></td>
            </tr>
        </table>
    </div>
    <div class="page_content">
        <asp:Panel ID="panelList" runat="server">
        </asp:Panel>
    </div>
</asp:Content>

