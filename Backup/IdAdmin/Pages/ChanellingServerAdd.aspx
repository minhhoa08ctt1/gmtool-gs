<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ChanellingServerAdd.aspx.cs" Inherits="IDAdmin.Pages.ChanellingServerAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Bổ sung server dành cho đối tác chanelling
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
        &nbsp;   
    </div>
    <div class="page_bar">
    </div>
    <div class="page_content">        
        <fieldset>            
            <legend>Bổ sung máy chủ game cho đối tác Chanelling</legend>
            <table cellpadding="2" cellspacing="2">
                <tr>
                    <td width="40%">Chọn máy chủ:</td>
                    <td with="60%">
                        <asp:DropDownList ID="cmbGameServer" runat="server" Width="350px">
                        </asp:DropDownList>
                    </td>
                </tr>                
                <tr>
                    <td width="30%">&nbsp;</td>
                    <td with="70%">
                        <asp:Button ID="buttonAdd" runat="server" Text=" Bổ sung " OnClick="buttonAdd_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    
    </div>  
</asp:Content>
