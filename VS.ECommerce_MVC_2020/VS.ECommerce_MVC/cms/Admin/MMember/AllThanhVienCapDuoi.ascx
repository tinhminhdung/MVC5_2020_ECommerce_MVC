<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AllThanhVienCapDuoi.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Admin.MMember.AllThanhVienCapDuoi" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="/Scripts/ckfinder/ckfinder.js" type="text/javascript"></script>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>

<span></span>
<div id="cph_Main_ContentPane">
    <div id="">
        <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
            <ul>
                <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
                <li class="Last"><span>Danh sách thành viên cấp dưới</span></li>
            </ul>
        </div>
        <div style="clear: both;"></div>
        <div class="widget">
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget">
                        <div class="widget-title">
                            <h4><i class="icon-reorder"></i>Danh sách thành viên cấp dưới</h4>
                        </div>
                        <div class="widget-body">
                            <div class="row-fluid">
                                 <%--<asp:LinkButton ID="lnkxuatExel" runat="server" OnClick="lnkxuatExel_Click" CssClass="vadd toolbar btn btn-info"> Xuất Exel</asp:LinkButton>--%>

                                <div class="span12" style="display: none">
                                    <div id="sample_1_length" class="dataTables_length">
                                        <div class="frm_search">
                                            <div>
                                                <asp:TextBox ID="txtkeyword" runat="server" CssClass="txt_csssearch" Width="400px"></asp:TextBox>
                                                <asp:LinkButton ID="lnksearch" runat="server" OnClick="lnksearch_Click" CssClass="vadd toolbar btn btn-info" Style="margin-top: -9px;">  <i class="icon-search"></i>&nbsp;Tìm kiếm</asp:LinkButton>
                                                <div class="dataTables_filter" id="sample_1_filter">
                                                    <asp:LinkButton ID="bthienthi" runat="server" OnClick="btndisplay_Click" CssClass="vadd toolbar btn btn-info"> <i class=" icon-folder-close"></i>&nbsp;Hiện thị</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div style="margin-top: 10px;">
                                                <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlordertype" runat="server" AutoPostBack="True" CssClass="txt" OnSelectedIndexChanged="ddlordertype_SelectedIndexChanged">
                                                    <asp:ListItem Value="desc">Giảm dần</asp:ListItem>
                                                    <asp:ListItem Value="asc">Tăng dần</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox Style="width: 200px;" ID="txtNgayThangNam" placeholder="Tìm kiếm từ ngày/tháng/năm" AutoPostBack="true" OnTextChanged="txtNgayThangNam_TextChanged" runat="server" CssClass="txt_csssearch" Width="200px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" Format="MM/dd/yyyy" runat="server" TargetControlID="txtNgayThangNam"></cc1:CalendarExtender>
                                                <asp:TextBox Style="width: 200px;" ID="txtDenNgayThangNam" placeholder="Tìm kiếm đến ngày/tháng/năm" AutoPostBack="true" OnTextChanged="txtDenNgayThangNam_TextChanged" runat="server" CssClass="txt_csssearch" Width="200px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" Format="MM/dd/yyyy" runat="server" TargetControlID="txtDenNgayThangNam"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="list_item">
                                 <div style="color: red; font-size:12px"><b>Phân mục</b> : <asp:Literal ID="ltphanmuc" runat="server"></asp:Literal></div>
                                <div style="color: red; font-size:12px"><b>Tổng thành viên</b> : <asp:Literal ID="ltSum" runat="server"></asp:Literal></div>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-striped table-bordered" id="sample_1">
                                            <tr>
                                                <th class="hidden-phone">Họ và tên</th>
                                                <th class="hidden-phone">Ngày tham gia</th>
                                                <th class="hidden-phone">Trạng thái</th>
                                                 <th class="hidden-phone">Tổng TV</th>
                                                 <th class="hidden-phone">Xem thêm</th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td align="left" style="padding-left: 10px; line-height: 22px; color: #646465" width="350px">
                                                
                                                Họ và tên:<span style="color: #444444; padding-left: 27px; font-weight: bold"><a data-tooltip="sticky<%#Eval("ID") %>"  href="/admin.aspx?u=Thanhvien&IDThanhVien=<%#Eval("ID") %>" target="_blank"><%#Eval("HoVaTen") %></a></span><br />
                                                Địa chỉ:<span style="color: #444444; padding-left: 40px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "DiaChi")%></span><br />
                                                Điện thoại:<span style="color: #444444; padding-left: 22px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "DienThoai")%></span><br />
                                                Email:<span style="color: #444444; padding-left: 15px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "Email")%></span><br />
                                            </td>
                                            
                                            <td style="text-align: center;">
                                                <%#MoreAll.MoreAll.FormatDate(Eval("NgayTao").ToString())%>
                                            </td>
                                            <td style="text-align: center;">
                                                <%#Status(Eval("TrangThai").ToString())%>
                                            </td>
                                            <td style="text-align: center;">
                                                <%#Commond.SumThanhVienCapDuoi(Eval("ID").ToString())%>
                                            </td>
                                            <td style="text-align: center">
                                                <a style="color: red" href="/admin.aspx?u=CapDuoi&ID=<%#DataBinder.Eval(Container.DataItem,"ID")%>&TV=<%=IDCD %>">[Chi tiết]</a>
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
                                    <td align="right">
                                        <asp:Literal ID="ltpage" runat="server"></asp:Literal>
                                        <div class="phantrang" style="">
                                            <cc1:CollectionPager ID="CollectionPager1" runat="server" BackNextDisplay="HyperLinks" BackNextLocation="Split"
                                                BackText="<<" ShowFirstLast="True" ResultsLocation="Bottom" PagingMode="QueryString" MaxPages="50" FirstText="Trang đầu" HideOnSinglePage="True" IgnoreQueryString="False" LabelStyle="font-weight: bold;color:red" LabelText="" LastText="Cuối cùng" NextText=">>" PageNumbersDisplay="Numbers"
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


        </div>
