<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Nav_conten.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.Nav_conten" %>
<div class="main-breadcrumb">
	<div class="container">
		<div class="row">
			<div class="col-lg-12">
				<ol class="breadcrumb">
					<li><a href="/"><%=label("l_home") %></a></li>
                    <asp:Literal ID="ltrnav" runat="server"></asp:Literal>
				</ol>
			</div>
		</div>
	</div>
</div>