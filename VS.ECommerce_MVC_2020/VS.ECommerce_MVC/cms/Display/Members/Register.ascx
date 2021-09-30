<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Register.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.Members.Register" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <div class="right-home">
      <div class="rows-home">
        <p class="linktag"><a href="/">Trang chủ </a>/ <span>Tạo tài khoản</span></p>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
<asp:View ID="View1" runat="server">
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
                    <asp:TextBox ID="txtusername" runat="server" class="textarea" ValidationGroup="GInfo"></asp:TextBox> 
                    <span style="font-size: 7pt; color: coral; font-family: Tahoma">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="GInfo" ControlToValidate="txtusername" ErrorMessage=" Tên đăng nhập không được để trống !"></asp:RequiredFieldValidator>
                </div>
           </div>
           <div>
               <div class="labelll">
                    <%=label("lt_password")%>:</div>
                <div>
                   <asp:TextBox ID="txtpassword" runat="server"  TextMode="Password" ValidationGroup="GInfo" class="textarea" ></asp:TextBox>  
                  <span style="font-size: 7pt; color: #ff7f50; font-family: Tahoma">*</span>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="GInfo" ControlToValidate="txtpassword" ErrorMessage="Mật khẩu không được để trống !" ></asp:RequiredFieldValidator>
                </div>
           </div>
            <div>
               <div class="labelll">
                    <%=label("l_name")%></div>
                <div>
                   <asp:TextBox ID="txtlastname" runat="server"  ValidationGroup="GInfo"  class="textarea" ></asp:TextBox> 
                    <span style="font-size: 7pt; color: #ff7f50; font-family: Tahoma">*</span>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="GInfo" ControlToValidate="txtlastname" ErrorMessage="Họ và tên không được để trống !" ></asp:RequiredFieldValidator>
                </div>
           </div>
           <div>
               <div class="labelll">
                    Email</div>
                <div>
                    <asp:TextBox ID="txtemail" runat="server" ValidationGroup="GInfo"  class="textarea" ></asp:TextBox> 
                    <span style="font-size: 7pt; color: #ff7f50; font-family: Tahoma">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtemail" Display="Dynamic" ErrorMessage="Email không được để trống !" SetFocusOnError="True"  ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RequiredFieldValidator4" ValidationGroup="GInfo"  runat="server" ControlToValidate="txtemail" ErrorMessage="Vui lòng nhập một địa chỉ email hợp lệ." ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  meta:resourcekey="valRegExResource1"></asp:RegularExpressionValidator>
                </div>
           </div>
           <div>
               <div class="labelll">
                     <%=label("l_phone")%></div>
                <div>
                    <asp:TextBox ID="txt_phone" runat="server" class="textarea" ValidationGroup="GInfo" MaxLength="11"></asp:TextBox> 
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_phone"  Display="Dynamic" ErrorMessage="Số di động không được để trống !" SetFocusOnError="True"  ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_phone"  Display="Dynamic" ErrorMessage="Số điện thoại phải là số !" SetFocusOnError="True" ValidationExpression="\d*" ValidationGroup="GInfo"></asp:RegularExpressionValidator>
                <span style="font-size: 7pt; color: #ff7f50; font-family: Tahoma">*</span>
                </div>
           </div>
            <div>
               <div class="labelll">
                     <%=label("l_address")%></div>
                <div>
                   <asp:TextBox ID="txt_add" runat="server" class="textarea"  ValidationGroup="GInfo"></asp:TextBox> 
                    <span style="font-size: 7pt; color: #ff7f50; font-family: Tahoma">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="GInfo" ControlToValidate="txt_add" ErrorMessage="Địa chỉ không được để trống !" ></asp:RequiredFieldValidator>
                </div>
           </div>
        </div>
  </div>
 <div style="padding-left:20px; padding-left:20px; clear:both; padding-top:10px;">
        <asp:Button ID="btnregister"   ValidationGroup="GInfo" runat="server" Text="Đăng ký" OnClick="Button1_Click" class="btnadd" />
        <asp:Button ID="btncancel" runat="server" Text="Làm lại" class="btnadd" OnClick="btncancel_Click" />
</div>
    </asp:View>
    <asp:View ID="View2" runat="server">
    <div style=" line-height:22px; padding:10px">
    <div><%=label("login3") %>.</div>
     <div><%=label("login4") %></div>
    </div>
    </asp:View>
</asp:MultiView>
</ContentTemplate>
</asp:UpdatePanel>
   </div>
</div>
