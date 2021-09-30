<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Detail.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.Products.Detail" %>
<link href="/cms/Display/Products/Resources/Style.css" rel="stylesheet" type="text/css" />
<script src="../../../Resources/js/jquery-1.7.1.min.js" type="text/javascript"></script>
<script type="text/javascript">
    var r = { 'special': /[\W]/g, 'quotes': /[^0-9^]/g, 'notnumbers': /[^a-zA]/g }
    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
        if (o.value == '') {
            o.value = '1';
        }
    }
</script>
 <div class="right-home">
<div class="rows-home">
   <p class="linktag"><a href="/">Trang chủ </a>/ <span><asp:Literal ID="ltcatename" runat="server"></asp:Literal></span></p>
<div class="tongctiet">
<div class="lefctiet">
<div class="sp-loading"><img src="/Resources/images/sp-loading.gif" alt=""><br>LOADING IMAGES</div>
<div class="sp-wrap">
    <asp:Literal ID="ltshowimg" runat="server"></asp:Literal>
</div>
</div>
<div class="rightct">
<div class="prodetail">
<div class="ptitle"><h1><asp:Literal ID="ltname" runat="server"></asp:Literal></h1></div>
<div class="dptime">
<div class="dptime">
  <span>Cập nhật cuối lúc: <b><asp:Literal ID="ltdate" runat="server"></asp:Literal></b></span> ,
    <span style="margin-left: 12px;">Đã xem: <b><asp:Literal ID="ltxem" runat="server"></asp:Literal></b> lượt</span>
</div>
    
<div class="clear"></div>
<div class="pdid">Mã sản phẩm: <span class="prdid"><asp:Literal ID="ltcode" runat="server"></asp:Literal></span></div>
<div class="pdid" style=" display:none">Thương hiệu: <span class="prdid"><asp:Literal ID="ltthuonghieu" runat="server"></asp:Literal></span></div>
<p class="price_old" style=" display:none">Giá thị trường: <span><asp:Literal ID="ltpriceoll" runat="server"></asp:Literal> đ</span></p>
   <div class="price">
                                    <span class="newprice"><span>Giá:</span><asp:Literal ID="ltprice" runat="server"></asp:Literal></span>
                                </div>
<div class="clear"></div>

<div class="pdid"><asp:Literal ID="ltdesc" runat="server"></asp:Literal></div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%=MauSacFull()%>
            <%=KichCoFull()%>
        <div class="clear"></div>
        <asp:HiddenField ID="HiddenField1"  runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>


	
<div class="tonnng">
<p class="prod_text">Số lượng:
     <asp:TextBox ID="txtsoluong" class="input-text qty" Text="1" runat="server">1</asp:TextBox>
</p>
<div class="frmbuy">
<asp:LinkButton  CssClass="btn-style-buynow" ID="lnkaddtocart" runat="server"  OnClick="lnkaddtocart_Click">Đặt hàng ngay</asp:LinkButton>
<asp:LinkButton CssClass="btn-style-add" ID="lnkaddtocartgh" runat="server"   OnClick="lnkaddtocartgh_Click">Thêm vào giỏ hàng</asp:LinkButton>
    </div>
</div>



    <div class="clear" style=" clear:both; height:5px"></div>

</div>
</div>
</div>
</div>
<div class="clear" style=" clear:both; height:5px"></div>
 <asp:Literal ID="ltshares" runat="server"></asp:Literal>
<div class="clear" style=" clear:both; height:5px"></div>

<div class="Chitietsp">Chi tiết sản phẩm</div>
<div class="dangky">
<div class="trai"></div>
<div class="phai"></div>
</div>
<div class="News-content"><asp:Literal ID="ltdetail" runat="server"></asp:Literal></div>
<%if (Commond.Setting("Commentsp") == "1"){ %>
<div class="shares">
    <div class="fb-comments" data-href="<%=MoreAll.MoreAll.RequestUrl(Request.Url.ToString()) %>" data-width="100%" data-numposts="5" data-colorscheme="light"></div>
</div>
<%} %>
</div>
<div class="clear"></div>

<div class="head_prod">
          <div class="head_1"></div>
          <h3>Sản phẩm khác</h3>
          <div class="head_2"></div>
          </div>
       <div class="prod_fr">

             <asp:Literal ID="ltother" runat="server"></asp:Literal>

    </div>
    </div>
    </div>
   

<asp:HiddenField ID="hdCurAmount" Value="1" runat="server" />
<asp:HiddenField ID="hdipid" Value="1" runat="server" />
 
<script type="text/javascript">
    function DetailUpdateOrder(id, name, active) {
        //bắt đầu
         var body = active.children(".adricon");
         $.ajax({
             type: "POST",
             url: "/index.aspx/Up_Order",
             data: "{id:'" + id.toString() + "',quantity:'" + $("#<%=hdCurAmount.ClientID %>").val() + "'}",
             contentType: "application/json; charset=utf-8",
             datatype: "json",
             async: "true",
             success: function (response) {
                 ShowCart();
                 LoadCart();
             },
             error: function (response) {
             },
             beforeSend: function () {
                 body.addClass('adricons');
             },
             complete: function () {
                 setTimeout(function () { body.removeClass("adricons"); }, 1000);
             }
         });
//        var soluong = $("#<%=hdCurAmount.ClientID %>").val();
//        var total = parseInt(soluong) + parseInt($("#Cart")[0].innerHTML);
//        $("#Cart")[0].innerHTML = total.toString();
    }
</script>



<script type="text/javascript">
    $(".size").on("click", function () {
        var count = $(this).parent().find("a").length;
        for (i = 0; i < count; i++) {
            $(this).parent().find("a")[i].className = "size";
        }
        this.className = "size active";
    })
    $(".Color").on("click", function () {
        var count = $(this).parent().find("a").length;
        for (i = 0; i < count; i++) {
            $(this).parent().find("a")[i].className = "Color";
        }
        this.className = "Color active";
    })
</script>
<script type="text/javascript">
    function KichCo(id, name) {
        //bắt đầu
        $.ajax({
            type: "POST",
            url: "/index.aspx/Up_KichCo",
            data: "{id:'" + id.toString() + "',quantity:'1'}",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: "true",
            success: function (response) {
            },
            error: function (response) {
            }
        });
    }
</script>
<script type="text/javascript">
    function MauSac(id, name) {
        //bắt đầu
        $.ajax({
            type: "POST",
            url: "/index.aspx/Up_MauSac",
            data: "{id:'" + id.toString() + "',quantity:'1'}",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: "true",
            success: function (response) {
            },
            error: function (response) {
            }
        });
    }
</script>

<%--// Mầu sắc , kích thước .,thangs4/2017.theanh/ web thoi trang
//Hoặc lọc như trang http://thietbianninh.com
//web mới code mới, lọc và phân trang kiểu trực tiếp sql a phúc F:\Congty_APhuc\Nam2019\Thang05\VS.Thoitrangslp.com--%>
<%-- Cách phân trang kiểu trực tiếp ví dụ trang  --> F:\Congty_APhuc\Nam2019\Thang05\VS.Thoitrangslp.com--%>
