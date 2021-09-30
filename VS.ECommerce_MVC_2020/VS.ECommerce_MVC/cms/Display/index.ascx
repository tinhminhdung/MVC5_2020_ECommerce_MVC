<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="index.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.index" %>
<%--onmouseover--%>
<%--<div onmouseover="doSomething('11')">
<a href="http://www.site.com/" >link</a>
</div>--%>
<%--hoặc--%>
<%--<a href="http://www.site.com/" onmouseover="doSomething(this)">test</a>
<script type="text/javascript">
    function doSomething(elem) {
      alert(elem);
    }
</script>--%>

<script type="text/javascript">
    function doSomething(elem) {
        alert(elem);
    }
</script>
<asp:Literal ID="ltlistpro" runat="server"></asp:Literal>
<%--áp dụng để load sau nhé--%>
<%--<div id="dddd"></div>

<script type="text/javascript">
jQuery(document).ready(function ($) {
    $("#dddd")[0].innerHTML = '<%=Bien() %>';
});
</script>--%>

<a class="addshopcart fancybox fancybox.ajax" id="abc" href="/Shopcart.aspx">ưtrg<img src="/Resources/images/muahang.jpg"></a>
     <a id="cart-header" href="/Shopcart.aspx" class=" fancybox fancybox.ajax"></a>
    <%--<div class="rows-home An">
        <div class="slide">
          <div class="flexslider">
            <ul class="slides">
            <%=Advertisings.Ad_vertisings.Advertisings_LI("1") %>
            </ul>
          </div>
        </div>
        <div class="adv-clock"><%=Advertisings.Ad_vertisings.Advertisings("2") %></div>
      </div>--%>
      <div class="rows-home">
        <div class="head_prod">
          <div class="head_1"></div>
          <h3>Sản phẩm mới</h3>
          <div class="head_2"></div>
          <a href="/san-pham-moi.html">Xem tất cả >></a> </div>
        <div class="prod_fr">
             <asp:Repeater ID="rpkhuyenmai"  runat="server">
        <ItemTemplate>11111111111111
          <div class="prod">
         <a  href='<%#Eval("TangName")%>.html'><%#AllQuery.MorePro.ImageDisplay(Eval("ImagesSmall").ToString())%></a>
              <%#Commond.Giamgia(Eval("ipid").ToString())%>
            <h3><a  href='<%#Eval("TangName")%>.html'><%#Eval("Name") %></a></h3>
            <p class="price"><%#AllQuery.MorePro.FormatMoney(Eval("Price").ToString())%> <span><%#AllQuery.MorePro.Detail_Price(Eval("OldPrice").ToString())%></span></p>
          <div class="muahang"><input type="number" max="999" min="0" name='<%#Eval("ipid")%>' id='<%#Eval("ipid")%>' value="1" class="input">
          <a href="javascript:void(0)" rel="popuprel3" class="popup" onmouseover="UpdateOrder(<%#Eval("ipid")%>,'<%#Eval("Name")%>')">onmouseover<img src="/Resources/images/muahang.jpg" /></a></div>
           <a href="javascript:void(0)" rel="popuprel3" class="popup" onclick="UpdateOrder(<%#Eval("ipid")%>,'<%#Eval("Name")%>')"><img src="/Resources/images/muahang.jpg" /></a></div>

                
            </div>
            </ItemTemplate>
    </asp:Repeater>
        </div>


<asp:Repeater ID="rpcates" runat="server">
<ItemTemplate>
<div class="head_prod">
          <div class="head_1"></div>
          <h3><%#Eval("Name") %></h3>
          <div class="head_2"></div>
          <p></p>
          <a href="/<%#Eval("TangName")%>.html">Xem tất cả >></a> </div>
            <div class="prod_fr">
         <asp:Repeater ID="Repeater2" DataSource='<%#NewProductInCate(Eval("id").ToString()) %>' runat="server">
        <ItemTemplate>
          <div class="prod">
            <a  href='<%#Eval("TangName")%>.html'><%#AllQuery.MorePro.ImageDisplay(Eval("ImagesSmall").ToString())%></a>
             <%#Commond.Giamgia(Eval("ipid").ToString())%>
             <h3><a  href='<%#Eval("TangName")%>.html'><%#Eval("Name") %></a></h3>
            <p class="price"><%#AllQuery.MorePro.FormatMoney(Eval("Price").ToString())%> <span><%#AllQuery.MorePro.Detail_Price(Eval("OldPrice").ToString())%></span></p>
     <div class="muahang"><input type="number" max="999" min="0" name='<%#Eval("ipid")%>' id='<%#Eval("ipid")%>' value="1" class="input"> 
    
            <a href="javascript:void(0)" rel="popuprel3" class="popup" onmouseover="UpdateOrder(<%#Eval("ipid")%>,'<%#Eval("Name")%>')">onmouseover<img src="/Resources/images/muahang.jpg" /></a></div>
 <a href="javascript:void(0)" rel="popuprel3" class="popup"  onclick="UpdateOrder(<%#Eval("ipid")%>,'<%#Eval("Name")%>')"><img src="/Resources/images/muahang.jpg" /></a></div>
            <%-- <a class="addshopcart" onclick="showOptions('<%#Eval("ipid") %>')"><img src="/Resources/images/muahang.jpg" /></a>--%>
            </div>
       </ItemTemplate>
    </asp:Repeater>
      </div>
</ItemTemplate>
</asp:Repeater>
      </div>
