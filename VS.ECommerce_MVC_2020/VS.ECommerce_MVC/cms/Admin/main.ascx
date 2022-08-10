<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="main.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Admin.main" %>

<div id="container" class="row-fluid">
    <div class="boder_menu">
        <div id="menu">
            <ul>
                <li class="content1"  id="Quantri" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-cogs"></i>Quản trị</span>
                    </a>
                    <ul>
                        <li id="set" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=set"><span class="SubMenuText">Cấu hình</span></a>
                        </li>
                         <li>
                            <a class="LinkButton" href="/admin.aspx?u=301"><span class="SubMenuText">Chuyển trang 301 </span></a>
                        </li>
                        <li id="Marketing" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=Marketing&su=Marketing"><span class="SubMenuText">Email nhận tin khuyến mại</span></a>
                        </li>
                        <li id="Contacts" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=Contacts"><span class="SubMenuText">Liên hệ, phản hồi</span></a>
                        </li>
                    </ul>
                </li>
                <li class="content2"  id="User" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-user"></i>Thành viên</span>
                    </a>
                    <ul>
                        <li id="AdminUser" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=AdminUser"><span class="SubMenuText">Thành viên quản trị</span></a>
                        </li>
                          <li id="Thanhvien" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=Thanhvien"><span class="SubMenuText">Thành viên đăng ký</span></a>
                        </li>

                         <li id="Danhsachloinhuan" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=Danhsachloinhuan"><span class="SubMenuText">Danh sách lợi nhuận</span></a>
                        </li>
                          <li id="Li2" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LichSuViGiaoDich"><span class="SubMenuText">Danh sách LS ví tiền</span></a>
                        </li>
                        <li id="Danhsachhoahong" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=Danhsachhoahong"><span class="SubMenuText">Danh sách hoa hồng</span></a>
                        </li>
                        <li id="LichSuThanhToan" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LichSuThanhToan"><span class="SubMenuText">Danh sách thanh toán</span></a>
                        </li>
                           <li id="CauHinhHH" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=CauHinhHH"><span class="SubMenuText">Cấu hình hoa hồng</span></a>
                        </li>
                    </ul>
                </li>
                <li class="content3"  id="Danhmuc" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-th-large"></i>Danh mục</span>
                    </a>
                    <ul>
                        <li id="Page" runat="server"><a class="LinkButton" href="/admin.aspx?u=Page"><span class="SubMenuText">Quản trị menu</span></a></li>
                        <li id="Advertisings" runat="server"><a class="LinkButton" href="/admin.aspx?u=Advertisings"><span class="SubMenuText">Quản trị quảng cáo</span></a></li>
                        <%--
						<li><a  class="LinkButton" href="/admin.aspx?u=Advertisings&su=DMAdvertising"><span class="SubMenuText">Quảng cáo theo nhóm sản phẩm</span></a></li>
                        <li><a  class="LinkButton" href="/admin.aspx?u=Advertisings&su=DMAdvertisingNews"><span class="SubMenuText">Quảng cáo theo nhóm tin</span></a></li>
						--%>
                        <li id="Tienich" runat="server"><a class="LinkButton" href="/admin.aspx?u=Tienich"><span class="SubMenuText">Tư vấn online</span></a></li>
                        <%-- <li id="faq" runat="server"><a class="LinkButton" href="/admin.aspx?u=faq"><span class="SubMenuText">Hỏi đáp</span></a></li>
                        <li><a  class="LinkButton" href="/admin.aspx?u=info"><span class="SubMenuText">thông tin chân trang</span></a></li>
                        <li><a  class="LinkButton" href="/admin.aspx?u=Gioithieu"><span class="SubMenuText">Gioithieu</span></a></li>
                        <li><a  class="LinkButton" href="/admin.aspx?u=Dichvu"><span class="SubMenuText">Dichvu</span></a></li>
                        <li><a  class="LinkButton" href="/admin.aspx?u=Download&su=Download"><span class="SubMenuText">Thư viện tải file</span></a></li>
                        --%>
                    </ul>
                </li>
                <li class="content4" id="News" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-globe"></i>Tin tức</span>
                    </a>
                    <ul>
                        <li><a class='linkmainmenu' href='?u=news&su=news'>Danh mục tin tức</a></li>
                        <li><a class='linkmainmenu' href='?u=news&su=Tintuc'>Danh sách tin</a></li>
                        <li><a class='linkmainmenu' href='?u=news&su=nset'>Cấu hình</a></li>
                    </ul>
                </li>
                <li class="content5" id="Products" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-tags"></i>Sản phẩm</span>
                    </a>
                    <ul>
                        <li><a class='linkmainmenu' href='?u=pro&su=pro'>Danh mục sản phẩm</a></li>
<%--                    <li><a class='linkmainmenu'  href='?u=pro&su=Manufacturer'>Thương hiệu</a></li>
                        <li><a class='linkmainmenu' href='?u=pro&su=ManagementPrice'>Khoảng giá</a></li>
                        <li><a class='linkmainmenu' href='?u=pro&su=Size'>Kích thước</a></li>
                        <li><a class='linkmainmenu'  href='?u=pro&su=Color'>Mầu sắc</a></li>--%>
                        <li><a class='linkmainmenu'  href='?u=pro&su=items'>Danh sách sản phẩm</a></li>
                        <li><a class='linkmainmenu' href='?u=pro&su=set'>Cấu hình</a></li>
                    </ul>
                </li>
                <li class="content6" id="Album" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-picture"></i>Thư viện</span>
                    </a>
                    <ul>
                        <li><a class='linkmainmenu' href='?u=Album&su=Album'>Danh mục thư viện</a></li>
                        <li><a class='linkmainmenu' href='?u=Album&su=items'>Danh sách thư viện</a></li>
                        <li><a class='linkmainmenu' href='?u=Album&su=set'>Cấu hình</a></li>
                    </ul>
                </li>
                <li class="content7" id="Video" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="fa fa-youtube-play"></i>Thư viện video</span>
                    </a>
                    <ul>
                        <li><a class='linkmainmenu' href='?u=Video&su=Video'>Danh mục video</a></li>
                        <li><a class='linkmainmenu' href='?u=Video&su=items'>Danh sách video</a></li>
                        <li><a class='linkmainmenu' href='?u=Video&su=set'>Cấu hình</a></li>
                    </ul>
                </li>
                <li class="ctgiohang" id="carts" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-shopping-cart"></i>Giỏ hàng</span>
                    </a>
                    <ul>
                        <li><a class='linkmainmenu' href='?u=carts'>Quản lý đơn đặt hàng</a></li>
                    </ul>
                </li>
                <li class="giohangs" id="giohang" runat="server"><span class="badges"><%=TCarts() %></span></li>
            </ul>
        </div>
    </div>
</div>
<div id="main">
    <div id="main-content">
        <div class="container-fluid">
            <div style="width: 100%; margin: 0 auto;">
                <asp:PlaceHolder ID="phcontrol" runat="server"></asp:PlaceHolder>
            </div>
        </div>
    </div>
</div>

