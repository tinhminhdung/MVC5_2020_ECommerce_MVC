<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.Members.Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <div class="right-home">
      <div class="rows-home">
        <p class="linktag"><a href="/">Trang chủ </a>/ <span>Đăng nhập tài khoản</span></p>
<div class="frm-add">
<div style="width:100%; margin-left:20px">
           <div>
               <div class="labelll"></div>
                <div><asp:Label ID="ltmsg" runat="server" ForeColor=Red></asp:Label></div>
           </div>
               <div>
               <div class="labelll">
                     <%=label("l_username")%>:</div>
                <div>
                    <asp:TextBox ID="txt_Uname" runat="server" class="textarea" ValidationGroup="GInfo"></asp:TextBox> 
                    <span style="font-size: 7pt; color: coral; font-family: Tahoma">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="GInfo" ControlToValidate="txt_Uname" ErrorMessage=" Tên đăng nhập không được để trống !"></asp:RequiredFieldValidator>
                </div>
           </div>
           <div>
               <div class="labelll">
                    <%=label("lt_password")%>:</div>
                <div>
                   <asp:TextBox ID="txt_password" runat="server"  TextMode="password" ValidationGroup="GInfo" class="textarea" ></asp:TextBox>  
                  <span style="font-size: 7pt; color: #ff7f50; font-family: Tahoma">*</span>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="GInfo" ControlToValidate="txt_password" ErrorMessage="Mật khẩu không được để trống !" ></asp:RequiredFieldValidator>
                </div>
           </div>
                 <div><a href="/Dang-ky" style="color:#0296cd; font-size:12px;"><%=label("taotkmoi") %></a> </div>
                 <div> <a href="/Quen-mat-khau" style="color:#0296cd; font-size:12px;"><%=label("Quenmk") %>?</a></div>
        </div>
  </div>
 <div style="padding-left:20px; padding-left:20px; clear:both; padding-top:10px;">
  <asp:Button ID="btnlogin" runat="server" Text="Đăng nhập"  class="btnadd"   ValidationGroup="GInfo" OnClick="btnlogin_Click" CssClass="btnadd" />
</div>
 </div>
</div>
