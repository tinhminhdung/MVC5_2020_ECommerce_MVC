<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Index.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.Products.Index" %>
<%--  <div class="rows-home">
<asp:Repeater ID="rpcates" runat="server">
<ItemTemplate>
<div class="head_prod">
          <div class="head_1"></div>
          <h3><%#AllQuery.MorePro.Substring_Title(Eval("Name").ToString())%></h3>
          <div class="head_2"></div>
          <p></p>
          <a href="/<%#Eval("TangName")%>.html">Xem tất cả >></a> </div>
            <div class="prod_fr">
         <asp:Repeater ID="Repeater2" DataSource='<%#NewProductInCate(Eval("id").ToString()) %>' runat="server">
        <ItemTemplate>
        <div class="prod">
            <a  href='/<%#Eval("TangName")%>.html' title="<%#Eval("Name")%>"><%#AllQuery.MorePro.Image_Title_Alt(Eval("ImagesSmall").ToString(), Eval("Name").ToString(), Eval("Name").ToString())%></a>
             <%#AllQuery.MorePro.Giamgia(Eval("ipid").ToString())%>
             <h3><a  href='/<%#Eval("TangName")%>.html' title="<%#Eval("Name")%>"><%#AllQuery.MorePro.Substring_Title(Eval("Name").ToString())%></a></h3>
            <p class="price"><%#AllQuery.MorePro.FormatMoney(Eval("Price").ToString())%> <span><%#AllQuery.MorePro.Detail_Price(Eval("OldPrice").ToString())%></span></p>
            <div class="muahang"><input type="number" max="999" min="0" name='<%#Eval("ipid")%>' id='<%#Eval("ipid")%>' value="1" class="input"> <a href="javascript:void(0)"  onclick="UpdateOrder(<%#Eval("ipid")%>,'<%#Eval("Name")%>')"><img src="/Resources/images/muahang.jpg" /></a></div>
            </div>
            </ItemTemplate>
    </asp:Repeater>
      </div>
</ItemTemplate>
</asp:Repeater>
</div>--%>


      <div style="clear: both;"></div>

        <asp:Literal ID="ltShow" runat="server"></asp:Literal>
        <div style="clear: both;"></div>
        <div class="pager">
                <asp:Literal ID="ltpage" runat="server"></asp:Literal>
        </div>

