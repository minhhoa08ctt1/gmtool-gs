<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="RedeemList.aspx.cs" Inherits="IDAdmin.Pages.RedeemList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ĐỀN BÙ CHO NGƯỜI CHƠI
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
               DUYỆT ĐỀN BÙ
            </div>
            <div class="page_content">
                <fieldset>
                    <legend>DUYỆT ĐỀN BÙ</legend>     
                  
                         <asp:Panel ID="dataPanel" runat="server"></asp:Panel>
                        <center style="padding:5px;">
                            <asp:Button ID="acceptCom" runat="server" Text="Duyệt thông qua" OnClick="acceptCom_Click" />
                        <asp:Button ID="dismissCom" runat="server" Text="Từ chối thông qua" OnClick="dismissCom_Click" />
                        </center>                   
                </fieldset>
            </div>
</asp:Content>
