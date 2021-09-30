<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Box_search.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.AllPage.Box_search" %>
<asp:Panel ID="SearchBox" runat="server" DefaultButton="lnksearch">
<asp:TextBox placeholder="Nhập từ khóa tìm kiếm ... " ID="txtkeyword" class="keyword" runat="server"></asp:TextBox>
<asp:Button ID="lnksearch" class="bt-search" runat="server" Text="Tìm kiếm"  onclick="lnksearch_Click" />
</asp:Panel>