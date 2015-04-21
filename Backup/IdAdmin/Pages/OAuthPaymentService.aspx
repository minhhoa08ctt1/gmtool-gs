<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="OAuthPaymentService.aspx.cs" Inherits="IDAdmin.Pages.OAuthPaymentService" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        OAuth Payment Service Manager
    </div>        
    <div class="page_bar" style="text-align:right">
        <asp:HyperLink ID="linkAddNew" runat="server" NavigateUrl="">[Add New]</asp:HyperLink>&nbsp;
        <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="">[Go Back]</asp:HyperLink>&nbsp;
    </div>
    <br />
    <div class="page_content">
        <span class="span_caption">Client ID: </span>
        <span class="span_value">
            <asp:Label ID="labelClientID" runat="server" Font-Bold="true"></asp:Label>&nbsp;
        </span>
        <span class="span_caption">Application Name: </span>
        <span class="span_value">
            <asp:Label ID="labelApplicationName" runat="server" Font-Bold="true"></asp:Label>&nbsp;
        </span>
        <span class="span_caption">UserName (Owner):</span>
        <span class="span_value">
            <asp:Label ID="labelUserName" runat="server" Font-Bold="true"></asp:Label>&nbsp;
        </span>
    </div>
    <div class="page_content">
        <asp:Panel ID="panelList" runat="server" Width="100%">
        </asp:Panel>
    </div>    
    <div class="page_content">
        <fieldset>
            <legend><asp:Label ID="labelEditTitle" runat="server" Text=""></asp:Label></legend>
            <p>
                <span class="span_caption">Service ID: </span>
                <span class="span_value">
                    <asp:TextBox ID="txtServiceID" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
                </span>
                <span class="span_caption">Service Name: </span>
                <span class="span_value">
                    <asp:TextBox ID="txtServiceName" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
                </span>
                <span class="span_caption">Service Key: </span>
                <span class="span_value">
                    <asp:TextBox ID="txtServiceKey" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
                </span>
                <span class="span_caption">Description: </span>
                <span class="span_value">
                    <asp:TextBox ID="txtServiceDesc" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
                </span>
                <span class="span_caption">Allowed Gosu Transfer Type: </span>
                <span class="span_value">
                    <asp:DropDownList ID="cmbGosuTransferType" runat="server" CssClass="textbox" Width="80%">
                        <asp:ListItem Value="none" Text="Không được phép thanh toán"></asp:ListItem>
                        <asp:ListItem Value="gosu" Text="GOSU"></asp:ListItem>
                        <asp:ListItem Value="promotion" Text="GOSU Tặng"></asp:ListItem>
                        <asp:ListItem Value="duo" Text="DUO"></asp:ListItem>
                        <asp:ListItem Value="both" Text="Cả hai hình thức"></asp:ListItem>
                    </asp:DropDownList>
                </span>                
                <span class="span_caption">&nbsp;</span>
                <span class="span_value">
                    <asp:Button ID="buttonSave" runat="server" Text=" Save Data " />&nbsp;                    
                </span>            
                <span class="span_caption">&nbsp;</span>
                <span class="span_value">
                    <asp:Label ID="labelMessage" runat="server" ForeColor="#FF0000"></asp:Label>
                </span>
           </p>
        </fieldset>
    </div>
</asp:Content>
