<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cart.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Display.Products.Cart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="/cms/Display/Products/Resources/Giohang.css" rel="stylesheet" type="text/css" />
<link href="/cms/display/contact/Resources/css/StyleSheet.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="right-home">
            <div class="rows-home">
                <div class="head_prod">
                    <div class="head_1"></div>
                    <h3>Giỏ hàng</h3>
                    <div class="head_2"></div>
                    <p></p>
                </div>
                <div style="clear: both"></div>
                <%--        <div style=" text-align:left; padding-left:0px; padding-bottom:10px;"><%=label("Thank_you_roducts")%></div>--%>
                <asp:Panel ID="pncart" runat="server">
                    <div class='frm_cart '>
                        <div class="bgContent">
                            <div class="borderBlock_ListPro scollgiohang">
                                <table cellpadding="0" cellspacing="0" class="dcart scoll" width="100%" bgcolor="#FFFFFF" bordercolor="#C3C3C3">
                                    <tr>
                                        <td height="30" colspan="8" class="TitleItem" style="text-align: left"><%=label("l_have")%> <span style="color: #F00; font-weight: bold;">
                                            <asp:Literal ID="ltnumberofproducts" runat="server"></asp:Literal></span> <%=label("lproductsincart")%></td>
                                    </tr>
                                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="ItemDataBound_RP">
                                        <HeaderTemplate>
                                            <tr class="procart">
                                                <td align="left"><strong>STT</strong></td>
                                                <td align="left"><strong><%=label("l_images") %></strong></td>
                                                <td align="center"><strong><%=label("lt_firstname") %></strong></td>
                                                <td align="center"><strong><%=label("lprice") %></strong></td>
                                                <td align="center"><strong><%=label("lquantity") %></strong></td>
                                                <td align="center"><strong><%=label("l_tomoney") %></strong></td>
                                                <td align="right"><strong>[<%=label("ldelete")%>]</strong></td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr align="center">
                                                <td height="30" align="center" class="TitleItem">
                                                    <asp:Label ID="lb_tt" runat="server"></asp:Label></td>
                                                <td align="left" class="TitleItem">
                                                    <img src="<%#Eval("Vimg")%>" style="width: 77px; height: auto; border: solid 1px #d7d7d7" />
                                                    <%#Mausac(Eval("Mausac").ToString())%>
                                                    <%#Kichthuoc(Eval("Kichco").ToString())%>
                                                </td>
                                                <td align="center" class="TitleItem"><strong><%#Eval("Name")%></strong></td>
                                                <td align="center" class="TitleItem"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Price").ToString())%> </td>
                                                <td align="center" class="TitleItem">
                                                    <asp:HiddenField ID="hiID" Value='<%# Eval("PID") %>' runat="server" />
                                                    <asp:TextBox ID="txtxQuantity" Style="border: 1px solid #d7d7d7" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity")%>' CssClass="txt_css" Width="40px" runat="server" OnTextChanged="txtxQuantity_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                                <td align="center" class="TitleItem"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Money").ToString())%> </td>
                                                <td align="center">
                                                    <asp:LinkButton ID="LinkButton4" CommandName="delete" OnLoad="Delete_Load" CssClass="lnk" CommandArgument='<%#Eval("PID")%>' runat="server"><span class="cmdxoa"></span></asp:LinkButton></td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr style="background: #fff5ee;">
                                                <td height="30" align="center" class="TitleItem">
                                                    <asp:Label ID="lb_tt" runat="server"></asp:Label></td>
                                                <td align="left" class="TitleItem">
                                                    <img src="<%#Eval("Vimg")%>" style="width: 77px; height: auto; border: solid 1px #d7d7d7" />
                                                    <%#Mausac(Eval("Mausac").ToString())%>
                                                    <%#Kichthuoc(Eval("Kichco").ToString())%>
                                                </td>
                                                <td align="center" class="TitleItem"><strong><%#Eval("Name")%></strong></td>
                                                <td align="center" class="TitleItem"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Price").ToString())%> </td>
                                                <td align="center" class="TitleItem">
                                                    <asp:HiddenField ID="hiID" Value='<%# Eval("PID") %>' runat="server" />
                                                    <asp:TextBox ID="txtxQuantity" Style="border: 1px solid #d7d7d7" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity")%>' CssClass="txt_css" Width="40px" runat="server" OnTextChanged="txtxQuantity_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                <td align="center" class="TitleItem"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Money").ToString())%> </td>
                                                <td align="center">
                                                    <asp:LinkButton ID="LinkButton4" CommandName="delete" OnLoad="Delete_Load" CssClass="lnk" CommandArgument='<%#Eval("PID")%>' runat="server"><span class="cmdxoa"></span></asp:LinkButton></td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td height="30" colspan="5" align="right" class="TitleItem"><strong style="float: right; font-weight: bold; padding-right: 10px;"><%=label("ltotal")%>:</strong></td>
                                        <td colspan="3" align="center" class="TitleItem"><strong>
                                            <asp:Literal ID="lttotalvnd" runat="server"></asp:Literal>
                                        </strong></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
               
                                <div class="Buttondathang">
                                    <asp:Button ID="btnorder" CssClass="btnadd" runat="server" Text="Đặt hàng" OnClick="lnkorder_Click"></asp:Button>
                                    <asp:Button ID="btndelete" CssClass="btnadd" runat="server" OnLoad="Empty_Load" Text="Hủy bỏ giỏ hàng" OnClick="lnkdeletecart_Click"></asp:Button>
                                    <asp:Button ID="btnext" CssClass="btnadd" runat="server" Text="Tiếp tục mua hàng" OnClick="btnext_Click"></asp:Button>
                                </div>
                       


                </asp:Panel>
                <asp:Panel ID="pnmessage" runat="server">
                    <div class="modalbodys cart_body">
                        <i class="icon_cart"></i>
                        <h2><%=label("giohang1")%></h2>
                        <p><%=label("giohang2")%></p>
                        <a class="adrbutton" href="/"><%=label("giohang3")%></a>

                    </div>
                </asp:Panel>

                <asp:Panel ID="Panel1" runat="server" Visible="false">
                    <div class="modalbodys cart_body">
                        <i class="icon_cart"></i>
                        <asp:Literal ID="lblKQ" runat="server"></asp:Literal>
                        <br />
                        <h2><%=label("giohang1")%></h2>
                        <p><%=label("giohang2")%></p>
                        <a class="adrbutton" href="/"><%=label("giohang3")%></a>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnOrder" runat="server" Visible="false">
                    <div class='frm_cart scollgiohang'>
                        <table cellpadding="0" cellspacing="0" class="dcart scoll" width="100%" bgcolor="#FFFFFF" bordercolor="#C3C3C3">
                            <tr>
                                <td height="30" colspan="8" class="TitleItem" style="text-align: left"><%=label("l_have")%> <span style="color: #F00; font-weight: bold;">
                                    <asp:Literal ID="ltProdinCart" runat="server"></asp:Literal></span> <%=label("lproductsincart")%></td>
                            </tr>
                            <asp:Repeater ID="Repeater2" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="ItemDataBound_RP">
                                <HeaderTemplate>
                                    <tr class="procart">
                                        <td align="left"><strong>STT</strong></td>
                                        <td align="left"><strong><%=label("l_images") %></strong></td>
                                        <td align="center"><strong><%=label("lt_firstname") %></strong></td>
                                        <td align="center"><strong><%=label("lprice") %></strong></td>
                                        <td align="center"><strong><%=label("lquantity") %></strong></td>
                                        <td align="center"><strong><%=label("l_tomoney") %></strong></td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td height="30" align="center" class="TitleItem">
                                            <asp:Label ID="lb_tt" runat="server"></asp:Label></td>
                                        <td align="left" class="TitleItem">
                                            <img src="<%#Eval("Vimg")%>" style="width: 77px; height: auto; border: solid 1px #d7d7d7" />
                                            <%#Mausac(Eval("Mausac").ToString())%>
                                            <%#Kichthuoc(Eval("Kichco").ToString())%>
                                        </td>
                                        <td align="left" class="TitleItem"><strong><%#Eval("Name")%></strong></td>
                                        <td align="center" class="TitleItem"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Price").ToString())%> </td>
                                        <td align="center" class="TitleItem"><b><%#Eval("Quantity")%></b></td>
                                        <td align="center" class="TitleItem"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Money").ToString())%> </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr style="background: #fff5ee;">
                                        <td height="30" align="center" class="TitleItem">
                                            <asp:Label ID="lb_tt" runat="server"></asp:Label></td>
                                        <td align="left" class="TitleItem">
                                            <img src="<%#Eval("Vimg")%>" style="width: 77px; height: auto; border: solid 1px #d7d7d7" />
                                            <%#Mausac(Eval("Mausac").ToString())%>
                                            <%#Kichthuoc(Eval("Kichco").ToString())%>
                                        </td>
                                        <td align="center" class="TitleItem"><strong><%#Eval("Name")%></strong></td>
                                        <td align="center" class="TitleItem"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Price").ToString())%> </td>
                                        <td align="center" class="TitleItem"><b><%#Eval("Quantity")%></b></td>
                                        <td align="center" class="TitleItem"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Money").ToString())%> </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td height="30" colspan="5" align="right" class="TitleItem"><strong style="float: right; font-weight: bold; padding-right: 10px;"><%=label("ltotal")%>:</strong></td>
                                <td colspan="3" align="center" class="TitleItem"><strong>
                                    <asp:Literal ID="ltTotalOrder" runat="server"></asp:Literal>
                                </strong></td>
                            </tr>
                        </table>
                    </div>
                    <div style="white-space: 100%">
                        <div class="bacoc">
                            <span class="order-header"><%=label("giohang4") %></span>
                            <div class="maunen">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                                <div class='frm-contact'>
                                    <div style="width: 100%">
                                        <div style="float: left; width: 100%">
                                            <div class="labelll">
                                                <%=label("lt_fullname")%>:
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtName" CssClass="CSTextBox" ValidationGroup="cart" runat="server" Width="222px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="cart" ControlToValidate="txtName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%">
                                            <div class="labelll">
                                                <%=label("l_address")%>:
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtAddress" CssClass="CSTextBox" ValidationGroup="cart" runat="server" Width="222px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="cart" ControlToValidate="txtAddress" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%">
                                            <div class="labelll">
                                                <%=label("l_phone")%>:
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtPhone" CssClass="CSTextBox" ValidationGroup="cart" runat="server" Width="124px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="cart" ControlToValidate="txtPhone" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtPhone">
                                                </cc1:FilteredTextBoxExtender>
                                            </div>
                                        </div>

                                        <div style="float: left; width: 100%">
                                            <div class="labelll">
                                                <%=label("l_email")%>:
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtEmail" CssClass="CSTextBox" ValidationGroup="cart" runat="server" Width="222px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%">
                                            <div class="labelll">
                                                <%=label("l_content")%>:
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtnoidung" CssClass="CSTextBox" runat="server" ValidationGroup="cart" Width="264px" Height="87px" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="bacoc annhe" style="display: none">
                            <span class="order-header">Phương thức thanh toán </span>
                            <div class="maunen">
                                <asp:RadioButton ID="rdotructiep" GroupName="group" Text="Thanh toán tại cửa hàng" Checked="true" runat="server" OnCheckedChanged="rdotructiep_CheckedChanged" AutoPostBack="true" />
                                <div style="clear: both; width: 100%"></div>
                                <asp:Panel ID="Pltructiep" runat="server">
                                    <%=Commond.Setting("ttcuahang")%>
                                </asp:Panel>
                                <asp:RadioButton ID="rdoATM" GroupName="group" Text="Thanh toán chuyển khoản ATM" runat="server" OnCheckedChanged="rdoATM_CheckedChanged" AutoPostBack="true" />
                                <div style="clear: both; width: 100%"></div>
                                <asp:Panel ID="platm" runat="server" Visible="false">
                                    <%=Commond.Setting("ttnganhang")%>
                                </asp:Panel>
                                <div style="clear: both; width: 100%"></div>
                                <asp:RadioButton ID="rdoOnepay" GroupName="group" Text="Thanh toán Online qua thẻ ATM nội địa Onepay" runat="server" OnCheckedChanged="rdoOnepay_CheckedChanged" AutoPostBack="true" />
                                <asp:Panel ID="plOnepay" runat="server" Visible="false">
                                    <%=Commond.Setting("txtttatm")%>
                                </asp:Panel>
                                <%--<asp:RadioButton ID="rdoNganluong" GroupName="group" Text="Thanh toán bằng Ví điện tử NgânLượng.vn" runat="server" OnCheckedChanged="rdoNganluong_CheckedChanged" AutoPostBack="true" />
        <asp:Panel ID="plNganluong" runat="server" Visible="false">
           <%=Commond.Setting("txtttnganluong")%>
        </asp:Panel>--%>
                                <asp:RadioButton ID="rdoPayPal" GroupName="group" Text="Thanh toán bằng Ví điện tử PayPal" runat="server" OnCheckedChanged="rdoPayPal_CheckedChanged" AutoPostBack="true" />
                                <div style="clear: both; width: 100%"></div>
                                <asp:Panel ID="plPayPal" runat="server" Visible="false">
                                    <%=Commond.Setting("txtttPayPal")%>
                                </asp:Panel>
                                <div style="clear: both; width: 100%"></div>
                                <div id="TQNganLuong" runat="server">
                                    <asp:RadioButton ID="NL" runat="server" Text=" Thanh toán bằng Ví điện tử NgânLượng" GroupName="group" OnCheckedChanged="NL_CheckedChanged" AutoPostBack="true" />
                                    <div class="boxContent" runat="server" id="CNL">
                                        <%=Commond.Setting("txtttnganluong")%>
                                    </div>
                                    <div style="clear: both; width: 100%"></div>
                                    <asp:RadioButton ID="ATM_ONLINE" runat="server" Text="Thanh toán online bằng thẻ ngân hàng nội địa - NgânLượng.vn" GroupName="group" OnCheckedChanged="ATM_ONLINE_CheckedChanged" AutoPostBack="true" />
                                    <div class="boxContent" runat="server" id="CATM_ONLINE" style="display: none">
                                        <p>
                                            <i>
                                                <span style="color: #ff5a00; font-weight: bold; text-decoration: underline;">Lưu ý</span>: Bạn cần đăng ký Internet-Banking hoặc dịch vụ thanh toán trực tuyến tại ngân hàng trước khi thực hiện.</i>
                                        </p>

                                        <ul class="cardList clearfix">

                                            <li class="bank-online-methods ">
                                                <label for="vcb_ck_on">
                                                    <i class="VCB" title="Ngân hàng TMCP Ngoại Thương Việt Nam"></i>
                                                    <asp:RadioButton ID="VCB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="vnbc_ck_on">
                                                    <i class="DAB" title="Ngân hàng Đông Á"></i>
                                                    <asp:RadioButton ID="DAB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="tcb_ck_on">
                                                    <i class="TCB" title="Ngân hàng Kỹ Thương"></i>
                                                    <asp:RadioButton ID="TCB" runat="server" GroupName="bankcode" />
                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="sml_atm_mb_ck_on">
                                                    <i class="MB" title="Ngân hàng Quân Đội"></i>
                                                    <asp:RadioButton ID="MB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="shb_ck_on">
                                                    <i class="SHB" title="Ngân hàng Sài Gòn - Hà Nội"></i>
                                                    <asp:RadioButton ID="SHB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="sml_atm_vib_ck_on">
                                                    <i class="VIB" title="Ngân hàng Quốc tế"></i>
                                                    <asp:RadioButton ID="VIB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="sml_atm_vtb_ck_on">
                                                    <i class="ICB" title="Ngân hàng Công Thương Việt Nam"></i>
                                                    <asp:RadioButton ID="ICB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="sml_atm_exb_ck_on">
                                                    <i class="EXB" title="Ngân hàng Xuất Nhập Khẩu"></i>
                                                    <asp:RadioButton ID="EXB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="sml_atm_acb_ck_on">
                                                    <i class="ACB" title="Ngân hàng Á Châu"></i>
                                                    <asp:RadioButton ID="ACB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="sml_atm_hdb_ck_on">
                                                    <i class="HDB" title="Ngân hàng Phát triển Nhà TPHCM"></i>
                                                    <asp:RadioButton ID="HDB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="sml_atm_msb_ck_on">
                                                    <i class="MSB" title="Ngân hàng Hàng Hải"></i>
                                                    <asp:RadioButton ID="MSB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="sml_atm_nvb_ck_on">
                                                    <i class="NVB" title="Ngân hàng Nam Việt"></i>
                                                    <asp:RadioButton ID="NVB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="sml_atm_vab_ck_on">
                                                    <i class="VAB" title="Ngân hàng Việt Á"></i>
                                                    <asp:RadioButton ID="VAB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="sml_atm_vpb_ck_on">
                                                    <i class="VPB" title="Ngân Hàng Việt Nam Thịnh Vượng"></i>
                                                    <asp:RadioButton ID="VPB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="sml_atm_scb_ck_on">
                                                    <i class="SCB" title="Ngân hàng Sài Gòn Thương tín"></i>
                                                    <asp:RadioButton ID="SCB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="ojb_ck_on">
                                                    <i class="OJB" title="Ngân hàng Đại Dương"></i>
                                                    <asp:RadioButton ID="OJB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="bnt_atm_pgb_ck_on">
                                                    <i class="PGB" title="Ngân hàng Xăng dầu Petrolimex"></i>
                                                    <asp:RadioButton ID="PGB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="bnt_atm_gpb_ck_on">
                                                    <i class="GPB" title="Ngân hàng TMCP Dầu khí Toàn Cầu"></i>
                                                    <asp:RadioButton ID="GPB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="bnt_atm_agb_ck_on">
                                                    <i class="AGB" title="Ngân hàng Nông nghiệp &amp; Phát triển nông thôn"></i>
                                                    <asp:RadioButton ID="AGB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="bnt_atm_sgb_ck_on">
                                                    <i class="SGB" title="Ngân hàng Sài Gòn Công Thương"></i>
                                                    <asp:RadioButton ID="SGB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="bnt_atm_nab_ck_on">
                                                    <i class="NAB" title="Ngân hàng Nam Á"></i>
                                                    <asp:RadioButton ID="NAB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                            <li class="bank-online-methods ">
                                                <label for="sml_atm_bab_ck_on">
                                                    <i class="BAB" title="Ngân hàng Bắc Á"></i>
                                                    <asp:RadioButton ID="BAB" runat="server" GroupName="bankcode" />

                                                </label>
                                            </li>

                                        </ul>

                                    </div>
                                    <div style="clear: both; width: 100%"></div>
                                    <asp:RadioButton ID="VISA" Text="Thanh toán bằng thẻ Visa hoặc MasterCard - NgânLượng.vn" runat="server" GroupName="group" OnCheckedChanged="VISA_CheckedChanged" AutoPostBack="true" />
                                    <div class="boxContent" runat="server" id="CVISA" style="display: none">
                                        <%=Commond.Setting("txtVisa")%>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="bacoc annhe" style="display: none">
                            <span class="order-header">Hình thức vận chuyển</span>
                            <div class="maunen">
                                <div class="cus-ship clearfix">
                                    <p class="clearfix">
                                        <label class="info-vc">
                                            <input type="radio" checked="" runat="server" value="Giao hàng tại cửa hàng" name="choices" id="giaohangtannoi">Giao hàng tại cửa hàng
                                        </label>
                                        &nbsp;&nbsp;&nbsp;<label style="padding-left: 15px;" class="box-info-vc"><%=Commond.Setting("ttgiaodich")%></label>
                                    </p>
                                    <p class="clearfix">
                                        <label class="info-vc">
                                            <input type="radio" value="Chuyển phát nhanh" runat="server" name="choices" id="Chuyenphatnhanh">Chuyển phát nhanh<br />
                                        </label>
                                        &nbsp;&nbsp;&nbsp;<label style="padding-left: 15px; width: 100%" class="box-info-vc"><%=Commond.Setting("ttchuyenphatnhanh")%></label>
                                    </p>
                                </div>
                            </div>
                        </div>

                        <div style="width: 100%; float: left">
                            <asp:Button ID="btnSendOrder" ValidationGroup="cart" CssClass="btnadd" runat="server" Text="Đặt hàng" OnClick="btnSendOrder_Click" />
                            <asp:Button ID="btnEditCart" CssClass="btnadd" runat="server" Text="Sửa lại giỏ hàng" OnClick="btnEditCart_Click" />
                            <asp:Button ID="_btctnew" CssClass="btnadd" runat="server" Text="Mua thêm" OnClick="_btctnew_Click" />
                            <asp:Button ID="btnCancelOrder" CssClass="btnadd" runat="server" Text="Hủy đặt hàng" OnClick="btnCancelOrder_Click" />
                        </div>

                        <div style="color: #333; float: left; font: 400 14px/22px arial; padding-top: 10px; width: 98%;">
                            <asp:Literal ID="ltgiohang" runat="server"></asp:Literal>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
<div style="clear: both"></div>
