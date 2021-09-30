<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlNews.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.control.ControlNews" %>
<%@ Register Src="~/cms/Display/Nav_conten.ascx" TagPrefix="uc1" TagName="Nav_conten" %>
<div class="page_banner">
<%if (Moldul == "99")
  { %>
<%=Advertisings.Ad_vertisings.Advertisings("7") %>
<%} %>
<%if (Request["su"] == "contact" || Request["su"] == "Page")
  { %>
<%=Advertisings.Ad_vertisings.Advertisings("8") %>
<%} %>
<%if (Moldul == "1" || Moldul == "2" || Request["su"] == "nws")
  { %>
<%=Advertisings.Ad_vertisings.Advertisings("5") %>
<%} %>
</div>
<uc1:Nav_conten runat="server" ID="Nav_conten" />

<div class="page-box">
    <div class="container">
        <div class="about_web clearfix row">
            s
            <div class="page_right col-sm-9 col-xs-12 clearfix">
                <div class="main">
                    <asp:PlaceHolder ID="phcontrol" runat="server"></asp:PlaceHolder>

                </div>

            </div>
        </div>
    </div>
</div>

