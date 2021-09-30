<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Category.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.Products.Category" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>

<%-- Cách phân trang kiểu trực tiếp ví dụ trang  --> F:\Congty_APhuc\Nam2019\Thang05\VS.Thoitrangslp.com--%>


<div class="rows-home">
<div class="head_prod">
          <div class="head_1"></div>
          <h1><asp:Literal ID="ltcatename" runat="server"></asp:Literal></h1>
          <div class="head_2"></div>
          </div>
       <div class="prod_fr">

<br>
        <div class="News-content">
            <div class="contents">    
                <asp:Literal ID="ltcontent" runat="server"></asp:Literal>
            </div>
        </div>

       <%--  <asp:Repeater ID="rpcates"  runat="server">
        <ItemTemplate>
          <div class="prod">
            <a  href='/<%#Eval("TangName")%>.html' title="<%#Eval("Name")%>"><%#AllQuery.MorePro.Image_Title_Alt(Eval("ImagesSmall").ToString(), Eval("Name").ToString(), Eval("Name").ToString())%></a>
             <%#AllQuery.MorePro.Giamgia(Eval("ipid").ToString())%>
             <h3><a  href='/<%#Eval("TangName")%>.html' title="<%#Eval("Name")%>"><%#AllQuery.MorePro.Substring_Title(Eval("Name").ToString())%></a></h3>
            <p class="price"><%#AllQuery.MorePro.FormatMoney(Eval("Price").ToString())%> <span><%#AllQuery.MorePro.Detail_Price(Eval("OldPrice").ToString())%></span></p>
              <div class="muahang"><input type="number" max="999" min="0" name='<%#Eval("ipid")%>' id='<%#Eval("ipid")%>' value="1" class="input">
           <a href="javascript:void(0)" rel="popuprel3" class="popup" onclick="UpdateOrder(<%#Eval("ipid")%>,'<%#Eval("Name")%>')"><img src="/Resources/images/muahang.jpg" /></a></div>
            </div>
            </ItemTemplate>
    </asp:Repeater>
        <div style=" clear:both"></div>
    <asp:Literal ID="lterr" runat="server"></asp:Literal>
    <div class="pager" style="margin-left:10px; margin-right:10px;color: #999;">
        <cc1:CollectionPager id="CollectionPager1" runat="server"  BackNextDisplay="HyperLinks" BackNextLocation="Split"
        BackText="◄" ShowFirstLast="True" ResultsLocation="None" PagingMode="PostBack" MaxPages="50" FirstText="Trang đầu" HideOnSinglePage="True"  IgnoreQueryString="False" LabelStyle="FONT-WEIGHT: bold;color:red" LabelText="" LastText="Cuối cùng" NextText="►" PageNumbersDisplay="Numbers" 
        ResultsFormat="Hiển thị từ  {0} Đến {1} (của {2})" ResultsStyle="PADDING-BOTTOM:4px;PADDING-TOP:4px;FONT-WEIGHT: bold;" ShowLabel="False" ShowPageNumbers="True" BackNextStyle="FONT-WEIGHT: bold; margin: 14px;" ControlCssClass="" ControlStyle="" UseSlider="True" PageNumbersSeparator="&nbsp;">
        </cc1:CollectionPager>
    </div> 
--%>
            <div style="clear: both;"></div>

        <asp:Literal ID="ltShow" runat="server"></asp:Literal>
        <div style="clear: both;"></div>
        <div class="pager">
                <asp:Literal ID="ltpage" runat="server"></asp:Literal>
        </div>



      </div>

</div>