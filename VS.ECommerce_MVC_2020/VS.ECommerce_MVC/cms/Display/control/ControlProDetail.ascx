<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlProDetail.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.control.ControlProDetail" %>
<%@ Register Src="~/cms/Display/Nav_conten.ascx" TagPrefix="uc1" TagName="Nav_conten" %>
<%@ Register Src="~/cms/Display/Lefmenu.ascx" TagPrefix="uc1" TagName="Lefmenu" %>
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
            <div style="clear:both"></div>

            <div class="page_related">
                <div class="container">
                    <div class="in_hot_pro clearfix">
                        <div class="page_title"><em>Sản phẩm cùng loại</em></div>
                        <div class="nbs-flexisel-container">
                            <div class="nbs-flexisel-inner">
                                <ul id="flexiselDemo2" class="clearfix nbs-flexisel-ul" style="left: -417.105px; display: block;">
                                    <asp:Literal ID="ltother" runat="server"></asp:Literal>
                                </ul>
                                <div class="nbs-flexisel-nav-left"></div>
                                <div class="nbs-flexisel-nav-right"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</div>

