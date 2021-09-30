<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Lefmenu.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.Lefmenu" %>
<div class="left-home">
    <%=MMenuPro()%>
<div class="gnv-box-header gnv-bg-news" style="margin-top:17px;">TIN TỨC </div>
<ul class="list-news-box">
<asp:Repeater ID="rpcates" runat="server">
<ItemTemplate>
    <li> <a href="<%#Eval("TangName")%>.html"><%#MoreAll.MoreImage.Image_width_height_Title_Alt(Eval("Images").ToString(), "94", "auto", Eval("Title").ToString(), Eval("Title").ToString())%></a><a href="<%#Eval("TangName")%>.html"><%#Eval("Title")%></a></li>
</ItemTemplate>
</asp:Repeater>
</ul>
<div class="gnv-box-header gnv-bg-face">FACEBOOK FANPAGE</div>
<ul class="list-news-box">
    <li><iframe src="https://www.facebook.com/plugins/likebox.php?href=<%=Commond.Setting("Facebook") %>&amp;width=<%=Commond.Setting("txtfbwidth")%>&amp;height= <%=Commond.Setting("txtfbheight")%>&amp;show_faces=true&amp;colorscheme=light&amp;stream=false&amp;show_border=true&amp;header=true" scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:<%=Commond.Setting("txtfbwidth")%>px; height:<%=Commond.Setting("txtfbheight")%>px; margin-bottom:8px;" allowTransparency="true"></iframe></li>
</ul>
<div class="gnv-box-header gnv-bg-tt">QUẢNG CÁO</div>
<ul class="list-news-box">
    <%=Advertisings.Ad_vertisings.Advertisings_LI("5") %>
</ul>
</div>