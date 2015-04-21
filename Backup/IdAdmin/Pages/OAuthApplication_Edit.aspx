<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="OAuthApplication_Edit.aspx.cs" Inherits="IDAdmin.Pages.OAuthApplication_Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý ứng dụng client sử dụng OAuth
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
         Bổ sung/cập nhật thông tin ứng dụng
    </div>
    <div class="page_content">    
        <fieldset>
        <legend>OAuthApplication Information</legend>
        <p>
            <span class="span_caption">Client ID: </span>
            <span class="span_value">
                <asp:TextBox ID="txtClientID" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
            </span>
            <span class="span_caption">Application Name: </span>
            <span class="span_value">
                <asp:TextBox ID="txtApplicationName" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
            </span>
            <span class="span_caption">Secret Key:</span>
            <span class="span_value">
                <asp:TextBox ID="txtSecret" runat="server" CssClass="textbox" Width="80%"></asp:TextBox><br />
                <i><b>Lưu ý:</b>Trong Secret Key chỉ dùng chữ cái, chữ số và dấu gạch chân</i> 
            </span>
            <span class="span_caption">Website: </span>
            <span class="span_value">
                <asp:TextBox ID="txtSite" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
            </span>
            <span class="span_caption">Logo URL: </span>
            <span class="span_value">
                <asp:TextBox ID="txtLogo" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
            </span>
            <span class="span_caption">&nbsp;</span>
            <span class="span_value">
                <asp:CheckBox ID="chkEnabled" runat="server" Text="Enabled Application?"/>
            </span>
            <span class="span_caption">&nbsp;</span>
            <span class="span_value">
                <asp:CheckBox ID="chkAlwaysTrust" runat="server" Text="Always trust this Application?"/>
            </span>
            <span class="span_caption">UserName (Owner): </span>
            <span class="span_value">
                <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
            </span>
            <span class="span_caption">CSS Link: </span>
            <span class="span_value">
                <asp:TextBox ID="txtCssLink" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
            </span>
            <span class="span_caption">Popup Css Link: </span>
            <span class="span_value">
                <asp:TextBox ID="txtPopupCssLink" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
            </span>
            <span class="span_caption">Javascript Link: </span>
            <span class="span_value">
                <asp:TextBox ID="txtJsLink" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
            </span>
            <span class="span_caption">Redirect URIs:<br />(<i>Nhập mỗi URL trên mỗi dòng</i>)</span>
            <span class="span_value">
                <asp:TextBox ID="txtRedirectURIs" runat="server" CssClass="textbox" Width="80%" TextMode="MultiLine" Height="100px"></asp:TextBox>
            </span>            
            <span class="span_caption">RegSourceID (ký hiệu nguồn đăng ký tài khoản):</span>
            <span class="span_value">
                <asp:TextBox ID="txtRegSourceID" runat="server" CssClass="textbox" Width="80%"></asp:TextBox>
            </span>            
            <span class="span_caption">&nbsp;</span>
            <span class="span_value">
                <asp:Button ID="buttonSave" runat="server" Text=" Save Data " />&nbsp;
                <asp:Button ID="buttonCancel" runat="server" Text=" Cancel " />&nbsp;
            </span>            
            <span class="span_caption">&nbsp;</span>
            <span class="span_value">
                <asp:Label ID="labelMessage" runat="server" ForeColor="#FF0000"></asp:Label>
            </span>
       </p>
    </fieldset>
</asp:Content>
