﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuPage.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.Page.MenuPage" %>
<%if (Case == "99")
  {%>
<div id="sub-nav">
    <ul>
        <asp:Literal ID="ltmenu" runat="server"></asp:Literal>
    </ul>
</div>
<%} %>