<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlPro.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.control.ControlPro" %>
<%@ Register Src="~/cms/Display/Nav_conten.ascx" TagPrefix="uc1" TagName="Nav_conten" %>
<%@ Register Src="~/cms/Display/Lefmenu.ascx" TagPrefix="uc1" TagName="Lefmenu" %>
<link href="/Resources/ShopCart/css/Stylecart.css" rel="stylesheet" />
<div class="page_banner">
   <%=Advertisings.Ad_vertisings.Advertisings("6") %>
</div>
<uc1:Nav_conten runat="server" ID="Nav_conten" />

<div class="page-box">
    <div class="container">
        <div class="about_web clearfix row">
            <uc1:Lefmenu runat="server" ID="Lefmenu" />

            <div class="page_right col-sm-9 col-xs-12 clearfix">
                <div class="main">
                    <asp:PlaceHolder ID="phcontrol" runat="server"></asp:PlaceHolder>

                </div>

            </div>
        </div>
    </div>
</div>

