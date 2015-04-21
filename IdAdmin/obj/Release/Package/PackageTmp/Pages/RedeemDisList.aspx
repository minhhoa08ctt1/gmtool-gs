
<%@ Page Language="C#" MasterPageFile="~/Shared/Site.Master" AutoEventWireup="true" CodeBehind="RedeemDisList.aspx.cs" Inherits="IDAdmin.Pages.RedeemDisList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Danh sách đền bù bị từ chối
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page_title">
               ĐỀN BÙ BỊ TỜ CHỐI
            </div>
            <div class="page_content">
                <fieldset>
                    <legend> ĐỀN BÙ BỊ TỜ CHỐI</legend>     
                  
                         <asp:Panel ID="dataPanel" runat="server"></asp:Panel>
                        <center style="padding:5px;">
                        </center>                   
                </fieldset>
            </div>
</asp:Content>
