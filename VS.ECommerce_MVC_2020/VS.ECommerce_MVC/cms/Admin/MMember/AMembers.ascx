<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AMembers.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Admin.MMember.AMembers" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="/Scripts/ckfinder/ckfinder.js" type="text/javascript"></script>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<div id="cph_Main_ContentPane">
    <div id="">
        <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
            <ul>
                <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
                <li class="Last"><span>Danh sách Thành viên</span></li>
            </ul>
        </div>
        <div style="clear: both;"></div>
        <div class="widget">
              <asp:Panel ID="Panel1" runat="server">
     <div class="row-fluid">
                <div class="span12">
                    <div class="widget">
                        <div class="widget-title">
                            <h4><i class="icon-reorder"></i>Danh sách thư viện</h4>
                        </div>
               <div class="widget-body">
                <div class="row-fluid">
                    <div class="span9">
                        <div id="sample_1_length" class="dataTables_length">
                         <div class="frm_search">
                    <div>
                        <asp:TextBox ID="txtkeyword" runat="server" CssClass="txt_csssearch" Width="400px"></asp:TextBox>
                        <asp:LinkButton ID="lnksearch" runat="server" OnClick="lnksearch_Click" CssClass="vadd toolbar btn btn-info" style="margin-top: -9px;">  <i class="icon-search"></i>&nbsp;Tìm kiếm</asp:LinkButton>
                    </div>
                    <div style="margin-top: 10px;">
                        <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" CssClass="txt"  OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                        </asp:DropDownList>
                         <asp:DropDownList ID="ddlcapdo" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlcapdo_SelectedIndexChanged">
                     <asp:ListItem Selected="True" Value="-1">= Tất cả cấp bậc =</asp:ListItem>
                        <asp:ListItem Value="0">Mới đăng ký - [Lelvel:0]</asp:ListItem>
                        <asp:ListItem Value="1">Đại lý Cấp 1 - [Lelvel:1]</asp:ListItem>
                        <asp:ListItem Value="2">TN Kinh Doanh- [Lelvel:2]</asp:ListItem>
                        <asp:ListItem Value="3">TP Kinh Doanh -[Lelvel:3]</asp:ListItem>
                        <asp:ListItem Value="4">GĐ Kinh Doanh -[Lelvel:4]</asp:ListItem>
                        <asp:ListItem Value="5">GĐ Vùng -[Lelvel:5]</asp:ListItem>
                        <asp:ListItem Value="6">GĐ Miền - [Lelvel:6]</asp:ListItem>
                    </asp:DropDownList>

                        <asp:DropDownList ID="ddlcodong" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlcodong_SelectedIndexChanged">
                     <asp:ListItem Selected="True" Value="-1">= Tất cả  =</asp:ListItem>
                        <asp:ListItem Value="1">Tham gia cổ đông</asp:ListItem>
                        <asp:ListItem Value="0">Không tham gia cổ đông</asp:ListItem>
                    </asp:DropDownList>
                        <asp:DropDownList ID="ddlorderby" runat="server" AutoPostBack="true" CssClass="txt"
                            OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="NgayTao">S.xếp:Ngày cập nhật</asp:ListItem>
                            <asp:ListItem Value="ID">S.xếp:Tăng dần</asp:ListItem>
                            <asp:ListItem Value="HoVaTen">S.xếp:Tên (ABC)</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlordertype" runat="server" AutoPostBack="True" CssClass="txt" OnSelectedIndexChanged="ddlordertype_SelectedIndexChanged">
                            <asp:ListItem Value="desc">Giảm dần</asp:ListItem>
                            <asp:ListItem Value="asc">Tăng dần</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                        </div>
                    </div><div class="span3">
                        <div class="dataTables_filter" id="sample_1_filter">
                       <asp:LinkButton ID="bthienthi" runat="server"  OnClick="btndisplay_Click" CssClass="vadd toolbar btn btn-info"> <i class=" icon-folder-close"></i>&nbsp;Hiện thị</asp:LinkButton>
                        <asp:LinkButton ID="btDeleteall" Visible="false" ToolTip="Xóa những lựa chọn !" OnClientClick=" return confirmDelete(this);" runat="server" OnClick="btxoa_Click"  CssClass="vadd toolbar btn btn-info"><i class="icon-trash"></i>&nbsp;Xóa</asp:LinkButton>
                        </div>
                    </div>
                </div>
<div  class="list_item">
     <div style="color: red; font-weight: bold">
                                        Tổng thành viên:
                                        <asp:Literal ID="ltSum" runat="server"></asp:Literal>
                                    </div>
<asp:Repeater id="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
     <HeaderTemplate>
          <table class="table table-striped table-bordered" id="sample_1">
         <tr>
            <%-- <th class="hidden-phone"><input id="chkAll" onclick="javascript: SelectAllCheckboxes(this, 1);" type="checkbox" /></th>--%>
             <th class="hidden-phone">Họ và tên</th>
             <th class="hidden-phone">Ví tiền</th>
             <th class="hidden-phone">Ngày tạo</th>
             <th class="hidden-phone">Trạng thái</th>
             <th class="hidden-phone">Trạng thái</th>
             <th class="hidden-phone">Hiệu chỉnh</th>
          
        </tr>
	</HeaderTemplate>
	<ItemTemplate>
	<tr class="odd gradeX">
           <td style="text-align: center; display:none"><asp:CheckBox ID="chkid" runat="server" onclick="javascript:changeColor(this,'white');"/><asp:HiddenField ID="hiID" Value='<%# Eval("ID") %>' runat="server" /></td>
            <td align=left style=" padding-left:10px; line-height:22px; color:#646465" width=450px>
                Họ và tên:<span style="color:#444444; padding-left:27px;font-weight:bold"><a data-tooltip="sticky<%#Eval("ID") %>" href="#"><%#Eval("HoVaTen") %></a></span><br />
               
                 PasWord:<span style="color: #444444; padding-left: 15px; font-weight: bold"><a target="_blank" href="/Autologon.aspx?ID=<%# Eval("ID") %>&U1=<%# Eval("Key") %>&U2=<%# Eval("PasWord") %>"><%#DataBinder.Eval(Container.DataItem, "PasWord")%></a></span><br />
                 Địa chỉ:<span style="color:#444444; padding-left:40px;font-weight:bold">
                     <%#Commond.ShowTinhthanh(DataBinder.Eval(Container.DataItem, "TinhThanh").ToString())%>/
                     <%#Commond.ShowTinhthanh(DataBinder.Eval(Container.DataItem, "QuanHuyen").ToString())%>/
                     <%#Commond.ShowTinhthanh(DataBinder.Eval(Container.DataItem, "phuongXa").ToString())%>/
                     <%#DataBinder.Eval(Container.DataItem, "DiaChi")%></span><br />
                Điện thoại:<span style="color:#444444; padding-left:22px;font-weight:bold"><%#DataBinder.Eval(Container.DataItem, "DienThoai")%></span><br />
                Email:<span style="color:#444444; padding-left:15px;font-weight:bold"><%#DataBinder.Eval(Container.DataItem, "Email")%></span><br />
                Người giới thiệu:<span style="color: #444444; padding-left: 15px; font-weight: bold"><a target="_blank" href="/admin.aspx?u=Thanhvien&IDThanhVien=<%# Eval("Gioithieu") %>"><%#Commond.ShowThanhVien(DataBinder.Eval(Container.DataItem, "Gioithieu").ToString())%></a></span><br />
                 Cấp bậc:<span style="color: red; padding-left: 15px; font-weight: bold"><%#Commond.ShowCapbac(DataBinder.Eval(Container.DataItem, "CapBac").ToString())%></span><br />
                <div> <%#Commond.ShowAnhDaiDien(DataBinder.Eval(Container.DataItem, "AnhDaiDien").ToString())%></div>
               <a onclick="Showthanhvien(<%#Eval("id") %>)" class="account_a" style=" color: red; font-size: 14px; ">Xem thêm... </a>
                 <div id="<%#Eval("id")%>myDropdown" style=" display:none">
                                     Tên ngân hàng:<span style="color:#444444; padding-left:15px;font-weight:bold"><%#DataBinder.Eval(Container.DataItem, "TenNganHang")%></span><br />
                                     Chủ tài khoản:<span style="color:#444444; padding-left:15px;font-weight:bold"><%#DataBinder.Eval(Container.DataItem, "ChuTaiKhoan")%></span><br />
                                     Số tài khoản:<span style="color:#444444; padding-left:15px;font-weight:bold"><%#DataBinder.Eval(Container.DataItem, "SoTaiKhoan")%></span><br />
                                     Chi nhánh:<span style="color:#444444; padding-left:15px;font-weight:bold"><%#DataBinder.Eval(Container.DataItem, "ChiNhanh")%></span><br />
                <%#Commond.TachMtre(DataBinder.Eval(Container.DataItem, "MTRee").ToString())%> 
                <a target="_blank" href="/cms/Admin/MMember/DiagramTree.aspx?IDTHanhVien=<%#Eval("id") %>">Diagram Tree</a>|
                <br />
                <a style="color: #f50fd9; padding-left: 5px; font-weight: bold" target="_blank" href="admin.aspx?u=CapDuoi&ID=<%#DataBinder.Eval(Container.DataItem, "ID")%>&TV=<%#DataBinder.Eval(Container.DataItem, "ID")%>">>> Danh sách thành viên cấp dưới</a><br />
                <a style="color: #f50fd9; padding-left: 5px; font-weight: bold" target="_blank" href="admin.aspx?u=MTree&IDThanhVien=<%#DataBinder.Eval(Container.DataItem, "ID")%>">>> Danh sách thành viên cấp dưới MTree</a><br />
                </div>
            </td>
         <td style="text-align: left;">
               <div class="ViTien"> Ví rút: <span class="ShowCapbac1"><%#AllQuery.MorePro.Detail_Price(Eval("ViBayMuoiPT").ToString())%></span></div>
               <div class="ViTien"> Ví tạm giữ:  <span class="ShowCapbac1"><%#AllQuery.MorePro.Detail_Price(Eval("ViBaMuoiPT").ToString())%></span></div>
                <div class="ViTien">Đã rút:  <span class="ShowCapbac3"><%#AllQuery.MorePro.Detail_Price(Eval("TongTienDaRut").ToString())%></span></div>
             <%--<div class="ViTien">TrangThaiRuTienBangKhong rút: <%#Eval("TrangThaiRuTienBangKhong").ToString()%></div>--%>
               <hr />
               <div class="ViTien">Tổng lần rút tiền / Tái mua: <span class="ShowCapbac2"><%#Eval("TongSoLanRutTienBangKhong").ToString()%></span></div>
             <div class="ViTien">Trạng thái mua hàng:<%#Commond.TrangThaiMuaHang(Eval("TrangThaiMuaHang").ToString())%></div>
             <hr />
            
         <hr />
            Tổng thành viên F1:<span style="color: #444444; padding-left: 15px; font-weight: bold"><%#Commond.SumThanhVienCapDuoiF1(DataBinder.Eval(Container.DataItem, "ID").ToString())%></span><br />
            Tổng thành viên F1 tháng trước:<span style="color: #444444; padding-left: 15px; font-weight: bold"><%#Commond.SumTongF1ThangTruoc(DataBinder.Eval(Container.DataItem, "ID").ToString())%></span><br />
            Tổng thành viên F1 tháng này:<span style="color: #444444; padding-left: 15px; font-weight: bold"><%#Commond.SumTongF1ThangNay(DataBinder.Eval(Container.DataItem, "ID").ToString())%></span><br />
         <%--   Tổng thành viên cấp dưới tháng này:<span style="color: #444444; padding-left: 15px; font-weight: bold"><%#Commond.SumTongCDuoiThangNay(DataBinder.Eval(Container.DataItem, "ID").ToString())%></span><br />--%>

            Tổng thành viên cấp dưới:<span style="color: #444444; padding-left: 15px; font-weight: bold"><%#Commond.SumThanhVienCapDuoi(DataBinder.Eval(Container.DataItem, "ID").ToString())%></span><br />
            Doanh thu toàn hệ thống:<span style="color: #444444; padding-left: 22px; font-weight: bold"><%#AllQuery.MorePro.FormatMoney_NO(Commond.SumSoTienHopDongToanHeT(Eval("ID").ToString()))%> VNĐ</span><br />
         
         </td>
           <td style="text-align: center;">
             <%#MoreAll.MoreAll.FormatDate(Eval("NgayTao").ToString())%>
           </td>
           <td style="text-align: center;">
               <%#Status(Eval("TrangThai").ToString())%>
           </td>
           <td style="text-align: center;">
            <asp:LinkButton  CssClass="active action-link-button" ID="LinkButton2" runat="server" CommandName="lock"  CommandArgument='<%#Eval("ID")%>'  OnLoad="Lock_Load" Visible='<%#EnableLock(Eval("TrangThai").ToString())%>'><span style="font-size:14px">[Lock]</span></asp:LinkButton>
            <asp:LinkButton CssClass="active action-link-button" ID="LinkButton5" runat="server" CommandName="unlock"  CommandArgument='<%#Eval("ID")%>' Visible='<%#EnableUnLock(Eval("TrangThai").ToString())%>'><span style="font-size:14px">[Unlock]</span></asp:LinkButton>
           </td>
         <td style="text-align: center;">
              <asp:LinkButton CssClass="active action-link-button" ID="LinkButton1" CommandName="EditDetail" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server"><i class="icon-edit"></i></asp:LinkButton>
           <div style=" display:none">  <asp:LinkButton CssClass="active action-link-button" ID="LinkButton6" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="delete" OnLoad="Delete_Load"><i class="icon-trash"></i></asp:LinkButton></div>
         </td>
     </tr>
	</ItemTemplate>
<FooterTemplate>
    </table>
</FooterTemplate>
</asp:Repeater>
 </div>
 <table border="0" cellpadding="0" style="border-collapse: collapse" width="100%">
<tr height="20">
    <td align=right>
        <asp:Literal ID="ltpage" runat="server"></asp:Literal>
        <div class="phantrang" style=" ">
        <cc1:CollectionPager id="CollectionPager1" runat="server"  BackNextDisplay="HyperLinks" BackNextLocation="Split"
            BackText="<<" ShowFirstLast="True" ResultsLocation="Bottom" PagingMode="QueryString" MaxPages="50" FirstText="Trang đầu" HideOnSinglePage="True"  IgnoreQueryString="False" LabelStyle="font-weight: bold;color:red" LabelText="" LastText="Cuối cùng" NextText=">>" PageNumbersDisplay="Numbers" 
            ResultsFormat="Hiển thị từ  {0} Đến {1} (của {2})" ResultsStyle="padding-bottom:5px;padding-top:14px;font-weight: bold;" ShowLabel="False" ShowPageNumbers="True" BackNextStyle="font-weight: bold; margin: 14px;" ControlCssClass="" ControlStyle="" UseSlider="True" PageNumbersSeparator="">
        </cc1:CollectionPager>
        </div>
    </td>
    </tr>
</table>
                   </div>
                        </div>
                    </div>
         </div>
    <div style="color: red; font-weight: bold">Chú ý: Thành viên đăng ký rồi ko được xóa vì sẽ hỏng hết các F con</div>
            </asp:Panel>
            <asp:Panel ID="Panel2" Visible="false" runat="server">
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget">
                            <div class="widget-title">
                                <h4><i class="icon-reorder"></i>Thêm mới thành viên</h4>
                            </div>
                            <div class="widget-body">
                                <div class="row-fluid">
                                    <div class="span9">
                                        <div id="sample_1_length" class="dataTables_length">
                                            <div class="frm_search">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span3">
                                        <div class="dataTables_filter" id="sample_1_filter">
                                            <a class="vadd toolbar btn btn-info" href="admin.aspx?u=Thanhvien"><i class=" icon-folder-close"></i>&nbsp; Quay về trang thành viên</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="widget-body">
                                    <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    <table style="width: 100%;">
                                        <tbody>
                                            <tr>
                                                <td style="width: 14%; text-align: right; padding-right: 15px">
                                                    <span id="" class="lbleft">Tên thành viên <span style="color: red">(*)</span>:</span>
                                                </td>
                                                <td style="font-weight: bold">
                                                    <asp:TextBox ID="HoVaTen" ValidationGroup="GInfo" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="GInfo" ControlToValidate="HoVaTen" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; padding-right: 15px">
                                                    <span id="" class="lbleft">Điện thoại <span style="color: red">(*)</span>:</span>
                                                </td>
                                                <td style="font-weight: bold">
                                                    <asp:TextBox ID="DienThoai" ValidationGroup="GInfo" placeholder="Điền số điện thoại _ Số hợp đồng" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="GInfo" ControlToValidate="DienThoai" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="text-align: right; padding-right: 15px">
                                                    <span id="" class="lbleft">Tỉnh thành <span style="color: red">(*)</span>:</span>
                                                </td>
                                                <td style="font-weight: bold">
                                           <asp:DropDownList ID="ddlCountry" class="form-control " runat="server">
                                                <asp:ListItem Value="" Selected="True">-- chọn Tỉnh thành --</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdddlCountry" runat="server" />
                                                </td>
                                            </tr>

                                                <tr>
                                                <td style="text-align: right; padding-right: 15px">
                                                    <span id="" class="lbleft">Quận huyện <span style="color: red">(*)</span>:</span>
                                                </td>
                                                <td style="font-weight: bold">
                                                          <asp:DropDownList ID="ddlProvince" class="form-control " runat="server" >
                                                    <asp:ListItem Value="" Selected="True">-- chọn Quận huyện --</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdddlProvince" runat="server" />

                                                </td>
                                            </tr>

                                                 <tr>
                                                <td style="text-align: right; padding-right: 15px">
                                                    <span id="" class="lbleft">Phường xã <span style="color: red">(*)</span>:</span>
                                                </td>
                                                <td style="font-weight: bold">
                                <asp:DropDownList ID="ddlDistrict" class="form-control " runat="server">
                                    <asp:ListItem Value="" Selected="True">-- chọn Phường xã --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdddlDistrict" runat="server" />

                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="text-align: right; padding-right: 15px">
                                                    <span id="" class="lbleft">Địa chỉ <span style="color: red">(*)</span>:</span>
                                                </td>
                                                <td style="font-weight: bold">
                                                    <asp:TextBox ID="DiaChi" ValidationGroup="GInfo" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="GInfo" ControlToValidate="DiaChi" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>

                                            


                                         <%--   <tr>
                                                <td style="text-align: right; padding-right: 15px">
                                                    <span id="" class="lbleft">Tỉnh thành <span style="color: red">(*)</span>:</span>
                                                </td>
                                                <td style="font-weight: bold">
                                                    <asp:DropDownList ID="ddTinhThanhAdd" ValidationGroup="GInfo" runat="server" CssClass="txt">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="GInfo" ControlToValidate="ddTinhThanhAdd" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>

                                           
                                       <%--     <tr>
                                                <td style="text-align: right; padding-right: 15px">
                                                    <span id="" class="lbleft">Người giới thiệu <span style="color: red">(*)</span>:</span>
                                                </td>
                                                <td style="font-weight: bold">
                                                    <asp:TextBox ID="NguoiGioiThieu" ValidationGroup="GInfo" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="GInfo" ControlToValidate="NguoiGioiThieu" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>

                                          
                                            <tr>
                                                <td style="text-align: right; padding-right: 15px">
                                                    <span id="" class="lbleft">Tên ngân hàng<span style="color: red">(*)</span>:</span>
                                                </td>
                                                <td style="font-weight: bold">
                                                    <asp:TextBox ID="TenNganHang" ValidationGroup="GInfo" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="GInfo" ControlToValidate="TenNganHang" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; padding-right: 15px">
                                                    <span id="" class="lbleft">Chủ tài khoản <span style="color: red">(*)</span>:</span>
                                                </td>
                                                <td style="font-weight: bold">
                                                    <asp:TextBox ID="ChuTaiKhoan" CssClass="OrderValue" ValidationGroup="GInfo" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="GInfo" ControlToValidate="ChuTaiKhoan" ErrorMessage="*"></asp:RequiredFieldValidator>

                                                </td>
                                            </tr>
                                           
                                           
                                            <tr>
                                                <td style="text-align: right; padding-right: 15px">
                                                    <span id="" class="lbleft">Số tài khoản <span style="color: red">(*)</span>:</span>
                                                </td>
                                                <td style="font-weight: bold">
                                                    <asp:TextBox ID="SoTaiKHoan"  CssClass="" ValidationGroup="GInfo" runat="server"></asp:TextBox>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="GInfo" ControlToValidate="SoTaiKHoan" ErrorMessage="*"></asp:RequiredFieldValidator>

                                                </td>
                                            </tr>
                                           
                                                <tr>
                                                <td style="text-align: right; padding-right: 15px">
                                                    <span id="" class="lbleft">Ảnh đại diện:</span>
                                                </td>
                                                <td style="font-weight: bold">
                                                     <asp:TextBox ID="txtImage" runat="server" CssClass="text image"></asp:TextBox>
                                        <input id="btnImage" type="button" onclick="BrowseServer('<%=txtImage.ClientID %>','News');" value="Chọn ảnh" class="toolbar btns btn-info" />
                                        <asp:Literal ID="ltimgs" runat="server"></asp:Literal>
                                        <asp:HiddenField ID="hdimgnews" runat="server" />
                                                </td>
                                            </tr>
                                           
                                          

                                            <tr>
                                                <td align="left"></td>
                                                <td>
                                                    <asp:LinkButton ID="btn_InsertUpdate" ValidationGroup="GInfo" runat="server" OnClick="btn_InsertUpdate_Click" CssClass="toolbar btn btn-info"> <i class="icon-ok"></i>Cập nhật </asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="toolbar btn btn-info"> <i class="icon-chevron-left"></i>Hủy</asp:LinkButton>
                                                    <br />
                                                    <br />

                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>


                                    <br />
                                </div>
                            </div>
                        </div>


                    </div>
            </asp:Panel>
    </div>

            <input id="hd_insertupdate" type="hidden" size="1" name="Hidden1" runat="server">
        <input id="hd_id" type="hidden" size="1" name="Hidden2" runat="server">
        <input id="hd_page_edit_id" type="hidden" size="1" name="Hidden2" runat="server">
        <input id="hd_imgpath" type="hidden" size="1" name="Hidden2" runat="server">
        <input id="hd_rootpic" type="hidden" size="1" runat="server">
        <input id="hd_par_id" type="hidden" size="1" name="Hidden2" runat="server">
        <asp:HiddenField ID="hidLevel" runat="server" />

        <script type="text/javascript" charset="utf-8">
            function Showthanhvien(id) {
                var name = id + 'myDropdown';
                var hidden = document.getElementById(name);
                if (hidden.style.display == 'none') {
                    hidden.style.display = 'block';
                }
                else {
                    hidden.style.display = 'none';
                }
            }
</script>
<script src="/Resources/js/ServiceTinhThanhselectpicker.js"></script>





        <script>
            $("#<%=ddlCountry.ClientID%>").on('change', function () {
                var countryText = $("#<%=ddlCountry.ClientID%> option:selected").val();
                $("#<%=hdddlCountry.ClientID%>").val(countryText);
                });

            $("#<%=ddlProvince.ClientID%>").on('change', function () {
                var countryText = $("#<%=ddlProvince.ClientID%> option:selected").val();
                    $("#<%=hdddlProvince.ClientID%>").val(countryText);
                });
            $("#<%=ddlDistrict.ClientID%>").on('change', function () {
                    var countryText = $("#<%=ddlDistrict.ClientID%> option:selected").val();
                    $("#<%=hdddlDistrict.ClientID%>").val(countryText);
                });
              
                /// load lại các control
            var Tinhthanh = $("#<%=hdddlCountry.ClientID%>").val();
                if (Tinhthanh.length > 0) {
                    _getProvince(Tinhthanh);
                }
                var Province = $("#<%=hdddlProvince.ClientID%>").val();
                if (Province.length > 0) {
                    _getDistrict(Province);
                }
                var District = $("#<%=hdddlDistrict.ClientID%>").val();
                if (District.length > 0) {
                    _getWard(District);
                }

                // Active
                setTimeout(function () {
                    var Tinhthanh = $("#<%=hdddlCountry.ClientID%>").val();
                    if (Tinhthanh.length > 0) {
                        // _getProvince(Tinhthanh);
                        $("#<%=ddlCountry.ClientID%>").val(Tinhthanh);
                    }
                    var Province = $("#<%=hdddlProvince.ClientID%>").val();
                    if (Province.length > 0) {
                        // _getDistrict(Province);
                        $("#<%=ddlProvince.ClientID%>").val(Province);
                    }

                    var District = $("#<%=hdddlDistrict.ClientID%>").val();
                    if (District.length > 0) {
                        // _getWard(District);
                        $("#<%=ddlDistrict.ClientID%>").val(District);
                    }
                }, 1000);
            
            </script>