<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TinhThanh_Aspx.aspx.cs" ValidateRequest="false" EnableEventValidation="false" ViewStateEncryptionMode="Never" EnableViewStateMac="false" Inherits="VS.ECommerce_MVC.cms.PhatTrien.TinhThanh_Aspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.10.0/js/bootstrap-select.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.10.0/css/bootstrap-select.min.css" rel="stylesheet" />
    
</head>
<body>
    đã làm ở trang MMember
    F:\CongtyGooDeign\Nam2022\Thang07\ThaiSonVietNam.vn\VS.ECommerce_MVC\cms\Admin\MMember


    <form id="form1" runat="server">
        <div>
            <div class="jumbotron">
                <h1>Tỉnh thành </h1>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label for="ddlCountry">Quốc gia</label>
                                <asp:DropDownList ID="ddlCountry" class="form-control " runat="server" >
                                    <asp:ListItem Value="" Selected="True">-- chọn Quốc gia --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdddlCountry" runat="server" />
                            </div>
                            <div class="form-group">
                                <label for="ddlProvince">Tỉnh thành</label>
                                <asp:DropDownList ID="ddlProvince" class="form-control " runat="server" >
                                    <asp:ListItem Value="" Selected="True">-- chọn Tỉnh thành --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdddlProvince" runat="server" />
                            </div>
                            <div class="form-group">
                                <label for="ddlDistrict">Quận huyện</label>
                                <asp:DropDownList ID="ddlDistrict" class="form-control " runat="server" >
                                    <asp:ListItem Value="" Selected="True">-- chọn quận huyện --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdddlDistrict" runat="server" />
                            </div>
                            <div class="form-group">
                                <label for="ddlWard">Xã phường</label>
                                <asp:DropDownList ID="ddlWard" class="form-control " runat="server" >
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdddlWard" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <asp:Button ID="Button2" runat="server" Text="Hiên th111111ị" />
                        <asp:Button ID="Button1" runat="server" Text="Hiên thị" OnClick="Button1_Click" />
                        <br />
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        <h1>OK, chúng ta đã có giao diện rồi. Giờ code thôi</h1>
                        <div style="color: green; font-size: 22px">
                            Bạn đang ở khu vực:
                            <div id="divResult"></div>
                        </div>
                    </div>
                </div>
            </div>
        <%--    <script src="/Resources/js/ServiceTinhThanhAspx.js"></script>--%> chạy khi code asp sinh ra dạng Control1_ctl00_ctl00_ddlCountry
            <script src="../../Resources/js/ServiceTinhThanhselectpicker.js"></script> chạy dạng ko bị sinh ra Control1_ctl00_ctl00_ddlCountry


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
        </div>
    </form>
</body>
</html>
