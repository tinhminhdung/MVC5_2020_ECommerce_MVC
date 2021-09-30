<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Changinfo.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.Members.Changinfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="minimal-page-wrapper">
    <div class="edito-exergue">
        <span class="border-left"></span>
        <div class="wrap ">
     <div style="padding-left:20px; padding-left:140px; clear:both; padding-top:10px;"><asp:Label ID="ltmsg" runat="server" ForeColor=Red></asp:Label></div>
  <div class="frm-add" style=" padding:10px">
     <div class="gachke">
        <div class="tenthanhvien"><%=label("l_name")%> :</div>
        <div><asp:TextBox CssClass='contact3' ID="txtname" runat="server" Width="358px" ></asp:TextBox></div>                    
    </div>
    <div class="gachke">
        <div class="tenthanhvien"><%=label("lt_birthday") %></div>
        <div><asp:TextBox CssClass='contact3' ID="txtbirthday" runat="server" Width="358px" ></asp:TextBox>
            <span style="font-size: 7pt; font-family: Tahoma">(yyyy-mm-dd)</span><cc1:calendarextender id="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtbirthday"></cc1:calendarextender>
        </div> 
       </div>                  
     <div class="gachke">
        <div class="tenthanhvien"><%=label("l_address") %></div>
        <div><asp:TextBox CssClass='contact3' ID="txtaddress" runat="server" Width="358px" ></asp:TextBox></div>                    
    </div>
       <div class="gachke">
        <div class="tenthanhvien">Email</div>
        <div><asp:TextBox CssClass='contact3' ID="txtemail" runat="server" Width="358px" ></asp:TextBox></div>                    
    </div>
     <div class="gachke">
        <div class="tenthanhvien"> <%=label("l_phone")%></div>
        <div><asp:TextBox CssClass='contact3' ID="txtphone" runat="server" Width="358px" ></asp:TextBox></div>                    
    </div>
     <div class="gachke" style=" display:none">
        <div class="tenthanhvien">Ảnh đại diện</div>
        <div>
            <asp:FileUpload ID="flavatar" runat="server" CssClass="contact3" Height="20px" Width="250px" /><br />
            <div  class="adaidien"><asp:Literal ID="ltimg" runat="server"></asp:Literal></div>
        </div>
    </div>
    </div>
    <div style="padding-left:140px; padding-left:140px; clear:both; padding-top:10px;">
            <asp:Button ID="btnsave"  ValidationGroup="GInfo" runat="server" CssClass="btnadd" Text="Lưu thông tin" OnClick="btnsave_Click" />
       </div>
         
            <asp:HiddenField ID="hdid" runat="server" />
            <asp:HiddenField ID="hdimg" runat="server" />
</div>
</div>
</div>
