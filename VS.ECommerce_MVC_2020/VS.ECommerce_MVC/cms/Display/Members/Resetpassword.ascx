<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Resetpassword.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.Members.Resetpassword" %>
<%@ Register Src="~/cms/Display/Lefmenu.ascx" TagPrefix="uc1" TagName="Lefmenu" %>
<div class="main-content">
	<div class="container">
		<div class="row">
			<div class="col-md-9">
				<div class="blog-home-title clearfix">
					<h1><%=label("thanhvien1") %></h1>
				</div>
				<div class="blog-article clearfix">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
<asp:View ID="View1" runat="server">
<div class="frm-add">
<div style="width:100%; margin-left:20px">
             <div style="padding-left:20px; padding-left:20px; clear:both; padding-top:10px;"><asp:Label ID="ltmsg" runat="server" ForeColor=Red></asp:Label></div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <img src="/Resources/admin/images/loading.gif" />
            </ProgressTemplate>
            </asp:UpdateProgress>
           <div>
               <div class="labelll">
                    <%=label("thanhvien2") %>:</div>
                <div>
                    <asp:TextBox ID="txtemail" runat="server" ValidationGroup="GInfo"  class="textarea" ></asp:TextBox> 
                    <span style="font-size: 7pt; color: #ff7f50; font-family: Tahoma">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtemail" Display="Dynamic"  SetFocusOnError="True"  ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RequiredFieldValidator4" ValidationGroup="GInfo"  runat="server" ControlToValidate="txtemail"  ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  meta:resourcekey="valRegExResource1"></asp:RegularExpressionValidator>
                </div>
           </div>
        </div>
  </div>
 <div style="padding-left:20px; padding-left:20px; clear:both; padding-top:10px;">
  <asp:Button ID="btnresets" ValidationGroup="GInfo" runat="server" CssClass="btnadd" OnClick="btnregisters_Click"  Text="Lấy lại mật khẩu" />
</div>
    </asp:View>
    <asp:View ID="View2" runat="server">
    <div style=" line-height:22px;padding:10px">
     <div> <asp:Literal ID="ltresult" runat="server"></asp:Literal></div>
     </div>
    </asp:View>
</asp:MultiView>
</ContentTemplate>
 <Triggers>
 <asp:PostBackTrigger ControlID="btnresets" />
</Triggers>
</asp:UpdatePanel>
</div>
			</div>
			<div class="col-md-3">
                <uc1:Lefmenu runat="server" ID="Lefmenu" />
</div>
		</div>
	</div>
</div>

