<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Admin.MMember.Settings" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="/Scripts/ckfinder/ckfinder.js" type="text/javascript"></script>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<div id="cph_Main_ContentPane">
    <div id="">
        <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
            <ul>
                <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang ch?</a></li>
                <li class="Last"><span>Cấu hình sản phẩm</span></li>
            </ul>
        </div>
        <div style="clear: both;"></div>
        <div class="widget">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="widget-title">
                        <h4><i class="icon-list-alt"></i>&nbsp;Cấu hình hoa hồng</h4>
                    </div>
                    <div class="widget-body">
                        <div class="frm-add">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td style="width: 550px"></td>
                                        <td></td>
                                        <td>
                                            <strong><font color="#ed1f27">
                                            <asp:Literal ID="ltmsg" runat="server"></asp:Literal></font></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 550px">
                                            <strong style="text-transform: uppercase">
                                                <img src="Resources/admin/images/bullet-red.png" border="0" />
                                                Cấu hình hoa hồng đại lý </strong>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Đại lý 1
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="DaiLy1" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Đại lý 2
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="DaiLy2" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Đại lý 3
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="DaiLy3" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Đại lý 4
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="DaiLy4" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 550px">
                                            <strong style="text-transform: uppercase">
                                                <img src="Resources/admin/images/bullet-red.png" border="0" />
                                                Cấu hình hoa hồng Khi mua có số lượng là 4 trở lên (Combo Bình thường) </strong>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Số lượng
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="SoLuong" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            Sau số lượng này sẽ chỉ được hưởng hoa hồng là 200k
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Số tiền 200K
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="SoTien200K" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            VD: 200
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 550px">
                                            <strong style="text-transform: uppercase">
                                                <img src="Resources/admin/images/bullet-red.png" border="0" />
                                                Cấu hình hoa hồng (Combo Cổ đông) </strong>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Trực tiếp
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="TrucTiepCoDong" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            Trực tiếp nhận 18 triệu
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 550px">
                                            <strong style="text-transform: uppercase">
                                                <img src="Resources/admin/images/bullet-red.png" border="0" />
                                                Cấu hình hoa hồng  tái tiêu dùng sau khi rút tiền </strong>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Trực tiếp và 6 tầng
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="TaiTieuDung" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>   200K
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Số lần rút tiền 
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="SoLanRutTien" runat="server" Width="133px" CssClass="txt">0</asp:TextBox> sau 4 lần rút tiền và tái dùng thì F1 đến F6 chỉ dc 200
                                        </td>
                                    </tr>
                                     <tr>
                                        <td style="padding-left: 15px; width: 650px;">Số tiền ví tạm giữ chuyển về ví chính
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="SoTienChuyenVeViChinh" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>Sau mỗi lần tái mua số tiền từ ví tạm giữ sẽ đổ về ví chính là 10 triệu
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 550px">
                                            <strong style="text-transform: uppercase">
                                                <img src="Resources/admin/images/bullet-red.png" border="0" />
                                                Cấu hình hoa hồng 6 tầng (Gián tiếp) </strong>
                                        </td>
                                        <td></td>
                                        <td></td>

                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Đại lý Cấp 2
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="ChietKhau2" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            Ví dụ: 600/ Combo
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Đại lý Cấp 3
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="ChietKhau3" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            Ví dụ: 600/ Combo
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Đại lý Cấp 4
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="ChietKhau4" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            Ví dụ: 300/ Combo
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Đại lý Cấp 5
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="ChietKhau5" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            Ví dụ: 300/ Combo
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Đại lý Cấp 6
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="ChietKhau6" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            Ví dụ: 200/ Combo
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 550px">
                                            <strong style="text-transform: uppercase">
                                                <img src="Resources/admin/images/bullet-red.png" border="0" />
                                                Cấu hình hoa hồng cấp bậc </strong>
                                        </td>
                                        <td></td>
                                        <td></td>

                                    </tr>
                                    <tr style=" display:none">
                                        <td style="padding-left: 15px; width: 650px;">Đại lý Cấp 1
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="Tim6F1" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            Tổng số F1
                                        </td>
                                    </tr>

                                    </tr>
                                       <tr>
                                           <td style="padding-left: 15px; width: 650px;">TN Kinh Doanh
                                           </td>
                                           <td></td>
                                           <td>
                                               <asp:TextBox ID="HHTNKD" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                               Tìm
                                                <asp:TextBox ID="Tim3DaiLyCap1" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                               đại lý cấp 1
                                           </td>
                                       </tr>
                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">TP Kinh Doanh
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="HHTPKD" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            Tìm
                                                <asp:TextBox ID="Tim6DaiLyCap1" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            đại lý cấp 1
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">GĐ Kinh Doanh
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="HHGDKD" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            Tìm
                                                <asp:TextBox ID="Tim12DaiLyCap1" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            đại lý cấp 1
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">GĐ Vùng
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="HHGDVung" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            Tìm
                                                <asp:TextBox ID="Tim2GiamDocKinhDoanh" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            GĐ kinh doanh
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">GĐ Miền
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="HHGDMien" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            Tìm
                                                <asp:TextBox ID="Tim2GiamDocVung" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            GĐ vùng
                                            hoặc  
                                            <asp:TextBox ID="Tim3GiamDocKinhDoanh" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            GĐ Kinh doanh
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 550px">
                                            <strong style="text-transform: uppercase">
                                                <img src="Resources/admin/images/bullet-red.png" border="0" />
                                                Cấu hình hoa hồng đồng hưởng / cuối tháng</strong>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Đồng hưởng giám đốc vùng
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="DongHuongGiamDocVung" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            %
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 15px; width: 650px;">Đồng hưởng giám đốc miền
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="DongHuongGiamDocMien" runat="server" Width="133px" CssClass="txt">0</asp:TextBox>
                                            %
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 550px"></td>
                                        <td></td>
                                        <td>
                                            <br />
                                            <asp:LinkButton ID="btnsetup" runat="server" OnClick="btnsetup_Click" CssClass="toolbar btn btn-info" Style="background: #ed1c24"> <i class="icon-save"></i>  Cập nhật</asp:LinkButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>


<script>

    function fomart_split(input, number) { try { var value = input; if (value != null && !value.length < 1) { return value.split(" - ")[number]; } else { return ""; } } catch (e) { } }
    jQuery(document).ready(function ($) { $(".txt").on('keyup', function () { var n = parseInt($(this).val().replace(/\D/g, ''), 10); $(this).val(n.toLocaleString().replaceAll(".", ",").replaceAll("NaN", "")); }); $(".txt").load('keyup', function () { var n = parseInt($(this).val().replace(/\D/g, ''), 10); if (n.toLocaleString() != 'NaN') { $(this).val(n.toLocaleString().replaceAll(".", ",").replaceAll("NaN", "")); } }); }); $('body').on('keyup', '.txt', function (e) { var n = parseInt($(this).val().replace(/\D/g, ''), 10); if (n.toLocaleString() != 'NaN') { $(this).val(n.toLocaleString().replaceAll(".", ",").replaceAll("NaN", "")); } });
</script>