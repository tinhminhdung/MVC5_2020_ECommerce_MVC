<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" CodeBehind="index.aspx.cs" Debug="true" Inherits="VS.ECommerce_MVC.index1" ValidateRequest="false" EnableViewStateMac="false" ViewStateEncryptionMode="Never" MaxPageStateFieldLength="40" EnableEventValidation="false" %>
<%@ Register Src="cms/display/Control.ascx" TagName="Control" TagPrefix="uc1" %>
<%@ Register Src="cms/display/Header.ascx" TagName="Header" TagPrefix="uc2" %>
<%@ Register Src="cms/display/Footer.ascx" TagName="Footer" TagPrefix="uc3" %>
<%@ Register Src="~/cms/Display/AllPage/Box_Hotline.ascx" TagPrefix="uc1" TagName="Box_Hotline" %>

<%--<%@ OutputCache VaryByParam="none" Duration="1"   %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <%--<meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=0">Dùng trường hợp không Zoom được--%>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <html xmlns="http://www.w3.org/1999/xhtml" xml:lang="vi" lang="vi-VN">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <meta name='revisit-after' content='1 days' />
    <meta property="og:type" content="article" />
    <asp:Literal ID="ltFacebook" runat="server"></asp:Literal>
    <meta name="robots" content="index,follow,all" />

    <link href="/Resources/css/Css_All.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="/Resources/css/stylesXOASSSSSSSS.css" />
    <!-- Bootstrap css -->
    <link rel="stylesheet" type="text/css" href="/Resources/dist/css/bootstrap.min.css" />
    <script type="text/javascript" src="/Resources/assets/js/ie-emulation-modes-warning.js"></script>
    <script type="text/javascript" src="/Resources/assets/js/ie10-viewport-bug-workaround.js"></script>
    <link href="/Resources/MenuChinh/css/Menu.css" rel="stylesheet" type="text/css" />
    <%--Screen 2 ben --%>
    <%--<script type="text/javascript" src="/Resources/js/Screen/floating-1.7.js"></script>--%>
    <script type="text/javascript">
        window.onload = function () { CheckScreenWidth() }; function CheckScreenWidth() { var a = document.getElementById("floatdivleftpage"), b = document.getElementById("floatdiv"); if (1024 > window.screen.width || 1024 == window.screen.width) b.style.display = "none", a.style.display = "none" } floatingMenu.add("floatdiv", { targetRight: 0, targetTop: 10, centerY: !0, snap: !0 }); floatingMenu.add("floatdivleftpage", { targetLeft: 0, targetTop: 10, centerY: !0, snap: !0 });
    </script>
    <!-- Zoom Products -->
    <link href="/Resources/css/smoothproducts.css" rel="stylesheet" type="text/css" />
    <!--Ènd Zoom Products -->
    <link href="/Resources/Responsive/css/flexnav.css" media="screen, projection" rel="stylesheet" type="text/css">
    <link href="/Resources/css/Mobile.css" rel="stylesheet" type="text/css" />
    <script src="/Resources/js/jquery-1.7.1.min.js" type="text/javascript"></script>
</head>
<body>
    <%--End Screen 2 ben --%>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Literal ID="ltShowbody" runat="server"></asp:Literal>
        <uc2:Header ID="Header1" runat="server" />
        <%--loadings Load ảnh và iframe sau khi load web xong--%>
        <div class="loadings">
            <uc1:Control ID="Control1" runat="server" />
            <uc3:Footer ID="Footer1" runat="server" />
        </div>
        <%=Commond.Setting("Livechat")%>
        <%if (Request["su"] == null && Modul == "")
          {%>
        <h1 style="display: none"><%=Commond.Setting("webname")%></h1>
        <%} %>
        <div class="scroll-top-inner" id="toTop"><img src="/Resources/images/top.png" /></div>
    </form>



    <%--PopUp--%>
    <link rel="stylesheet" href="/Resources/js/popUp/general.css" type="text/css" media="screen" />
    <script src="/Resources/js/popUp/popup.js" type="text/javascript"></script>
    <%--End PopUp--%>

    <script type="text/javascript" src="/Resources/dist/js/bootstrap.min.js"></script>
    <!-- Syntax Highlighter -->
    <script src="/Resources/Responsive/js/jquery.flexnav.js" type="text/javascript"></script>
   <script type="text/javascript">
       jQuery(document).ready(function ($) {
           $(".flexnav").flexNav();
       });
       function openNav() { document.getElementById("mySidenav").style.width = "250px"; }
       function closeNav() { document.getElementById("mySidenav").style.width = "0"; }
    </script>
    
    <!-- ShopCart -->
    <link href="/Resources/ShopCart/css/popup.css" rel="stylesheet" type="text/css" />
    <script src="/Resources/ShopCart/js/popup.js" type="text/javascript"></script>
    <link href="/Resources/ShopCart/css/Stylecart.css" rel="stylesheet" type="text/css" />
    <!-- ShopCart -->

    <script type="text/javascript">
        $('.tabs li a').click(function () {
            $('.list-news-hover').show(); //ẩn hết nội dung
            var activeTab = $(this).attr('href'); //gán giá trị href cho activeTab
            //activeTab = #news// activeTab =#random
            $(activeTab).fadeIn();
            return false; //chỉ hiện thị, thay đổi nội dung - không refesh trang
        });
    </script>
    <!-- Zoom Products -->
    <script type="text/javascript" src="/Resources/js/smoothproducts.min.js"></script>
    <script type="text/javascript">
        $('.sp-wrap').smoothproducts();
    </script>
    <!-- End Zoom Products -->
    <%-- Allbums--%><%--
    <script src="/cms/Display/Allbums/FlexSlider/js/flexslider.js" type="text/javascript"></script>
   <script type="text/javascript">
       $(function () {
           SyntaxHighlighter.all();
       });
           $('#carouselp').flexslider({
               animation: "slide",
               controlNav: false,
               animationLoop: false,
               slideshow: false,
               itemWidth: 120,
               itemMargin: 6,
               asNavFor: '#slidert'
           });
           $('#slidert').flexslider({
               animation: "slide",
               controlNav: false,
               animationLoop: false,
               slideshow: false,
               sync: "#carouselp",
               start: function (slidert) {
                   $('body').removeClass('loading');
               }
           });
    </script>--%>
    <!-- Popup Shopcart Products -->
    <div id="jName" style="margin-bottom: 25px; text-align: center; background: #fff8d1 none repeat scroll 0 0; border: 1px solid #fcab0f; border-radius: 3px; bottom: 15px; color: #000; display: none; padding: 10px; position: fixed; right: 0; width: 300px; z-index: 9999;"></div>
    <%-- Ajaxloading--%>
    <div id="Ajaxloading">
        <div class="inner">
            <img src="/Resources/ShopCart/images/ajax-loader_2.gif"><p>Đang xử lý...</p>
        </div>
    </div>
    <%-- Ajaxloading--%>
    <%-- popupbox--%>
    <div class="popupbox3" id="popuprel3">
        <div class="closes">
            <img src="/Resources/ShopCart/images/b_close.png" /></div>
        <div id="intabdiv3">
            <div id="scollssss">
			  <%-- <div id="scoll">--%>
                <div id="cartContent"></div>
            </div>
        </div>
    </div>
    <div style="display: none;" id="fade"></div>
    <%-- popupbox--%>




 <%--lazyload_News Load ảnh và iframe sau khi load web xong -- và js auto height--%>
	<script src="/Resources/js/equalheight.js"></script>
	<script type="text/javascript" charset="utf-8">
		$(window).load(function () {
			equalheight('.products-cat-frame .item');
		});
		$(window).resize(function () {
			equalheight('.products-cat-frame .item');
		});
	</script>
    <script type="text/javascript" src="/Resources/js/lazyload_News.js"></script>
    <uc1:Box_Hotline runat="server" ID="Box_Hotline" />
</body>
</html>
