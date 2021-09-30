<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="contact.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.contact.contact" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    <div class="right-home">
      <div class="rows-home">
        <p class="linktag"><a href="/">Trang chủ </a>/ <span>Liên hệ</span></p>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<div class='frm-contact'>
<div style="padding-top:10px; line-height:22px"><asp:Literal ID="ltcontactcontent" runat="server"></asp:Literal></div> 
<div Class="Anhmap">
<%=Commond.Setting("txtbando")%>
</div>
<div style=" padding:10px 10px 10px 10px"><asp:Label ID="ltmsg" runat="server" Font-Bold="true" ForeColor="red"></asp:Label></div>
<div style="width:100%">
            <div class="tbinput">
               <div class="labelll">
                      <%=label("l_name")%>:</div>
                <div>
                   <asp:TextBox ID="txtfullname" runat="server" ValidationGroup="GInfo" class="textarea" Width="270px"></asp:TextBox> 
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="GInfo" ControlToValidate="txtfullname" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
           </div>
           <div class="tbinput">
               <div class="labelll">
                    Email:</div>
                <div>
                  <asp:TextBox ID="txtemail" runat="server" ValidationGroup="GInfo" class="textarea" Width="270px"></asp:TextBox> 
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtemail" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"  ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RequiredFieldValidator4" ValidationGroup="GInfo"  runat="server" ControlToValidate="txtemail" ErrorMessage="Vui lòng nhập email hợp lệ." ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  meta:resourcekey="valRegExResource1"></asp:RegularExpressionValidator>
                </div>
           </div>
       <div class="tbinput">
               <div class="labelll">
                    <%=label("l_phone")%>:
                </div>
                <div>
                    <asp:TextBox ID="txtphone" runat="server" ValidationGroup="GInfo" class="textarea" Width="270px"></asp:TextBox> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtphone"  Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"  ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtphone"  Display="Dynamic" ErrorMessage="Số điện thoại phải là số !" SetFocusOnError="True" ValidationExpression="\d*" ValidationGroup="GInfo"></asp:RegularExpressionValidator>
                </div>
           </div>
           <div class="tbinput">
               <div class="labelll">
                    <%=label("l_address")%>:</div>
                <div>
                    <asp:TextBox ID="txtaddress" runat="server" ValidationGroup="GInfo" class="textarea" Width="270px"></asp:TextBox> 
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="GInfo" ControlToValidate="txtaddress" ErrorMessage="*" ></asp:RequiredFieldValidator>
                </div>
           </div>
           <div class="tbinput">
               <div class="labelll">
                    <%=label("l_title")%>:</div>
                <div class="txt_file">
                   <asp:TextBox ID="txttieude" runat="server" ValidationGroup="GInfo" class="textarea" Width="270px"></asp:TextBox>
                </div>
           </div>
             <div class="tbinput">
               <div class="labelll">
                    <%=label("l_content")%>:</div>
                        <div class="txt_file">
                   <asp:TextBox ID="txtcontent" runat="server" Height="111px" TextMode="MultiLine" ValidationGroup="GInfo" class="textarea" Width="270px"></asp:TextBox>
                </div>
           </div>
        </div>
</div>
<div class="khoangcbut">
    <asp:Button ID="btgui" runat="server"  OnClick="btgui_Click" ValidationGroup="GInfo"  Text="Gửi liên hệ" CssClass=btnadd />
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>

