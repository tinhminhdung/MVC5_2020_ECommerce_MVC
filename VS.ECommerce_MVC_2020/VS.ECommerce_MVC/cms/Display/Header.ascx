<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.Header" %>
<%@ Register Src="Products/searchbox.ascx" TagName="searchbox" TagPrefix="uc1" %>
<%@ Register Src="~/cms/Display/Page/MenuTop.ascx" TagPrefix="uc1" TagName="MenuTop" %>
<%@ Register Src="~/cms/Display/AllPage/Box_search.ascx" TagPrefix="uc1" TagName="Box_search" %>

<div id="mySidenav" class="sidenav">
    <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>
   <div class="Danhmucmenu">Danh mục menu  <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a></div>
    <div class="Mobile">
        <nav>
            <ul data-breakpoint="1025" class="flexnav with-js opacity sm-screen flexnav-show">
            <uc1:MenuTop runat="server" ID="MenuTop1" />
            </ul>
        </nav>
    </div>
</div>
<span style="font-size:30px;cursor:pointer" onclick="openNav()">&#9776; Menu Mobile</span>



<div class="Menutop">
    <div class="top">
        <div class="topin">
            <div class="ddiong">Hotline: <%=Commond.Setting("Hotline")%></div>
            <uc1:Box_search runat="server" ID="Box_search" />
        </div>
    </div>
    <div class="Mlogo">
        <div class="logo"><%=AllQuery.Banner.Banners() %></div>
    </div>
</div>
<div style="clear: both"></div>
<div class="Menu">
    <ul>
        <uc1:MenuTop runat="server" ID="MenuTop" />
    </ul>
</div>
<div style="clear: both"></div>
