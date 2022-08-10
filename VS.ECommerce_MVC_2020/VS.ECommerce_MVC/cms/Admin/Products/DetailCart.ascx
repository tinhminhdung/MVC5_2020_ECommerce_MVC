<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailCart.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Admin.Products.DetailCart" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="/Scripts/ckfinder/ckfinder.js" type="text/javascript"></script>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>

<div id="cph_Main_ContentPane">
    <div id="">
        <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
            <ul>
                <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
                <li class="Last"><span>Quản lý giỏ hàng</span></li>
            </ul>
        </div>
        <div style="clear: both;"></div>
        <div class="widget">
            <div id="cph_Main_ContentPane">
                <div class="widget">
                    <div class="widget-title">
                        <h4><i class="icon-reorder"></i>&nbsp;Thông tin giỏ hàng</h4>
                        <div class="ui-widget-content ui-corner-top ui-corner-bottom">
                            <div id="toolbox">
                                <div style="float: right;" class="toolbox-content">
                                    <table class="toolbar">
                                        <tbody>
                                            <tr>
                                                <td align="center">
                                                    <a id="" class="toolbar btn btn-info" href="javascript:{}" onclick="window.print()"><i class="fa fa-print"></i>Print </a></td>
                                                <td align="center">
                                                    <a id="" class="toolbar btn btn-info" href="/admin.aspx?u=pro&su=carts"><i class="icon-chevron-left"></i>Quay lại</a></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="widget-body">
                        <table style="width: 100%;">
                            <tbody>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Mã đơn hàng:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <b style="color: red">#<asp:Literal ID="ltmadonhang" runat="server"></asp:Literal></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%; text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Tên khách hàng:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <asp:Literal ID="ltname" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Địa chỉ:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <asp:Literal ID="ltdiachi" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Điện thoại:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <asp:Literal ID="ltdienthoai" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Email:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <asp:Literal ID="ltemail" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Hình thức thanh toán:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <asp:Literal ID="ltthanhtoan" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Phương thức giao hàng:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <asp:Literal ID="lthinhthucgiaohang" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Nội dung yêu cầu:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <asp:Literal ID="ltghichu" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Tổng số tiền đơn hàng:</span>
                                    </td>
                                    <td style="font-weight: bold; color: red">
                                        <asp:Literal ID="lttong" runat="server"></asp:Literal>
                                        <%=MoreAll.Other.Giatri("Dongiapro") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Tổng số tiền bằng chữ:</span>
                                    </td>
                                    <td style="font-weight: bold; color: red">
                                        <asp:Literal ID="ltvietchu" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <%if (DuyetDonHang == "1")
                                  {%>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Duyệt:</span>
                                    </td>
                                    <td style="font-weight: bold; color: red">
                                        <asp:HiddenField ID="hdid" runat="server" />
                                        <asp:Button ID="btduyet" Visible="false" CssClass="btnadd" runat="server" OnClientClick="SendTuVan()" OnClick="btduyet_Click" Text="Duyệt Đơn Hàng - Giao hàng" />
                                        <asp:Label ID="ltmess" BorderColor="Red" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <%}
                                  else
                                  {%>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Duyệt:</span>
                                    </td>
                                    <td style="font-weight: bold; color: red; font-size: 20px">
                                        <div>Bạn không có quyền duyệt đơn hàng</div>
                                    </td>
                                </tr>
                                <%} %>
                            </tbody>
                        </table>
                        <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                            <tbody>
                                <tr class="trHeader" style="height: 40px">
                                    <td style="width: 4%" class="contentadmin">STT</td>
                                    <td class="contentadmin">Ảnh</td>
                                    <td class="contentadmin">Mã sản phẩm</td>
                                    <td class="contentadmin">Tên sản phẩm</td>
                                    <td style="width: 10%; text-align: center !important" class="contentadmin">Giá</td>
                                    <td style="width: 7%; text-align: center !important" class="contentadmin">Số lượng</td>
                                    <td style="width: 15%; text-align: center !important" class="contentadmin">Số tiền</td>
                                </tr>
                                <asp:Repeater ID="rpcartdetail" runat="server">
                                    <ItemTemplate>
                                        <tr style="height: 20px" class="tr_while" onmouseover="this.className='tr_while_over'" onmouseout="this.className='tr_while'">
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%=i++ %></td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin">
                                                <img src="<%#Anh(Eval("ipid").ToString())%>" style="width: 50px; height: 50px;" /></td>
                                            <td style="padding-left: 5px; text-align: left !important" class="cartadmin"><%#Codes(Eval("ipid").ToString())%></td>
                                            <td style="padding-left: 5px; text-align: left !important" class="cartadmin"><%#DataBinder.Eval(Container.DataItem,"Name")%></td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#AllQuery.MorePro.Detail_Price(Eval("Price").ToString())%> <%=MoreAll.Other.Giatri("Dongiapro") %></td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#DataBinder.Eval(Container.DataItem,"Quantity")%></td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#AllQuery.MorePro.Detail_Price(Eval("Money").ToString())%> <%=MoreAll.Other.Giatri("Dongiapro") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr style="height: 20px; background: #fafafa" class="tr_while" onmouseover="this.className='tr_while_over'" onmouseout="this.className='tr_while'">
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%=i++ %></td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin">
                                                <img src="<%#Anh(Eval("ipid").ToString())%>" style="width: 50px; height: 50px;" /></td>
                                            <td style="padding-left: 5px; text-align: left !important" class="cartadmin"><%#Codes(Eval("ipid").ToString())%></td>
                                            <td style="padding-left: 5px; text-align: left !important" class="cartadmin"><%#DataBinder.Eval(Container.DataItem,"Name")%></td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#AllQuery.MorePro.Detail_Price(Eval("Price").ToString())%> <%=MoreAll.Other.Giatri("Dongiapro") %> </td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#DataBinder.Eval(Container.DataItem,"Quantity")%></td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#AllQuery.MorePro.Detail_Price(Eval("Money").ToString())%> <%=MoreAll.Other.Giatri("Dongiapro") %></td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <br />
                        <br />
                        <br />

                    </div>

                </div>
            </div>
        </div>
    </div>
</div>
<div style="clear: both;"></div>
<asp:HiddenField ID="hdIDGiohang" Value="0" runat="server" />
<asp:HiddenField ID="hdtongtien" Value="0" runat="server" />
<asp:HiddenField ID="hdIDThanhVien" Value="0" runat="server" />

<asp:HiddenField ID="hdtongtienchienluoc" Value="0" runat="server" />
<asp:HiddenField ID="hdTienLoiNhuan" Value="0" runat="server" />



<style>
    .btnadd {
    border: 1px solid #ed1f27 !important;
    background: #fff;
    color: #ed1f27 !important;
    margin-bottom: 6px;
    margin-top: 5px;
    padding-left: 17px;
    padding-right: 17px;
    height: 38px!important;
    font-size: 13px!important;
    font-weight: 700;
    margin-right: 11px;
    width: auto!important;
    font-family: arial;
    border-radius: 3px;
}
</style>

 <script type="text/javascript">
     var submit1 = 0;
     function SendTuVan() {
         if (++submit1 > 1) {
             alert('Hệ thống đang xử lý. Vui lòng đợi, Không kích nhiều lần');
             return false;
         }
     };
    
    </script>

<script>
    $('form').submit(function () {
        $(':submit', this).click(function () {
            return false;
        });
    });
</script>