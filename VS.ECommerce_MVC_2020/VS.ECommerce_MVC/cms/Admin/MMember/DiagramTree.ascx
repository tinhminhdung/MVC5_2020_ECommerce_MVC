<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DiagramTree.ascx.cs" Inherits="VS.ECommerce_MVC.cms.Admin.MMember.DiagramTree" %>
<link rel="stylesheet" href="/Resources/Admins/MTRee/jquery.treemenu.css">
<script src="/Resources/Admins/MTRee/jquery.treemenu.js"></script>
<script>
    $(document).ready(function () {
        $("ul.mytree").treemenu({ openActive: true });
    });
</script>

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
                            <div class="list_item">
                                <div style="color: red; font-size: 15px; text-transform: uppercase"><b>Thành viên:</b>
                                    <asp:Literal ID="ltname" runat="server"></asp:Literal></div>
                                <br />
                                <div style="color: #000; font-size: 13px; text-transform: uppercase"><b>Tổng thành viên:</b>
                                    <asp:Literal ID="ltTongsobanghi" runat="server"></asp:Literal></div>
                                <br />

                                <div style="color: #000; font-size: 13px; text-transform: uppercase"><b>Tổng thành viên toàn hệ thống:</b>
                                    <asp:Literal ID="lttongtvht" runat="server"></asp:Literal></div>
                                <br />

                             

                                <ul class="mytree">
                                       <asp:Literal ID="ltshow" runat="server"></asp:Literal>
                                </ul>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<style>
    .widget-body li {
        line-height: 29px;
    }

    .widget-body code {
        font-size: 15px;
    }
</style>
