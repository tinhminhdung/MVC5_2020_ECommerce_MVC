// File javascript để lấy dữ liệu

// Khai báo URL service của bạn ở đây
var baseService = "/PhatTrienAjax";
var countryUrl = baseService + "/GetAllCountries";
var provinceUrl = baseService + "/GetAllProvinceByCountryId";
var districtUrl = baseService + "/GetAllDistrictByProvinceId";
var wardUrl = baseService + "/GetAllWardByDistrictId";
$(document).ready(function () {
    // load danh sách country
    _getCountries();
    $("#Control1_ctl00_ctl00_ddlCountry").on('change', function () {
        var id = $(this).val();
        if (id != undefined && id != '') {
            _getProvince(id);
        }
    });

    $("#Control1_ctl00_ctl00_ddlProvince").on('change', function () {
        var id = $(this).val();
        if (id != undefined && id != '') {
            _getDistrict(id);
        }
    });
    $("#Control1_ctl00_ctl00_ddlDistrict").on('change', function () {
        var id = $(this).val();
        if (id != undefined && id != '') {
            _getWard(id);
        }
    });
    
});
function _getCountries() {
    $.get(countryUrl, function (data) {
        if (data != null && data != undefined && data.length) {
            var html = '';
            html += '<option value="">--Chọn tỉnh thành--</option>';
            $.each(data, function (key, item) {
                html += '<option value=' + item.ID + '>' + item.Name + '</option>';
            });
            $("#Control1_ctl00_ctl00_ddlCountry").html(html);
        }
    });
}
// truyền id của country vào
function _getProvince(id) {
    $.get(provinceUrl + "/" + id, function (data) {
        if (data != null && data != undefined && data.length) {
            var html = '';
            html += '<option value="">--Chọn Quận huyện--</option>';
            $.each(data, function (key, item) {
                html += '<option value=' + item.ID + '>' + item.Name + '</option>';
            });
            $("#Control1_ctl00_ctl00_ddlProvince").html(html);
        }
    });
}
// truyền id của province vào
function _getDistrict(id) {
    $.get(districtUrl + "/" + id, function (data) {
        if (data != null && data != undefined && data.length) {
            var html = '';
            html += '<option value="">--Chọn Phường xã--</option>';
            $.each(data, function (key, item) {
                html += '<option value=' + item.ID + '>' + item.Name + '</option>';
            });
            $("#Control1_ctl00_ctl00_ddlDistrict").html(html);
        }
    });
}