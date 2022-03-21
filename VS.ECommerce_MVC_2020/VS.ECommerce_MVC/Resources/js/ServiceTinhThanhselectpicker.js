﻿// File javascript để lấy dữ liệu

// Khai báo URL service của bạn ở đây
var baseService = "/PhatTrienAjax";
var countryUrl = baseService + "/GetAllCountries";
var provinceUrl = baseService + "/GetAllProvinceByCountryId";
var districtUrl = baseService + "/GetAllDistrictByProvinceId";
var wardUrl = baseService + "/GetAllWardByDistrictId";
$(document).ready(function () {
    // load danh sách country
    _getCountries();
    $("#ddlCountry").on('change', function () {
        var id = $(this).val();
        if (id != undefined && id != '') {
            _getProvince(id);
        }
    });

    $("#ddlProvince").on('change', function () {
        var id = $(this).val();
        if (id != undefined && id != '') {
            _getDistrict(id);
        }
    });
    $("#ddlDistrict").on('change', function () {
        var id = $(this).val();
        if (id != undefined && id != '') {
            _getWard(id);
        }
    });
    $("#ddlWard").on('change', function () {
        var countryText = $("#ddlCountry option:selected").text();
        var provinceText = $("#ddlProvince option:selected").text();
        var districtText = $("#ddlDistrict option:selected").text();
        var wardText = $("#ddlWard option:selected").text();
        var html = "Quốc gia: " + countryText + " Tỉnh thành: " + provinceText + " " + "Quận huyện: " + districtText + " " + "Xã phường: " + wardText;
        html += "</br>Quê bạn thật là đẹp. Chúc mừng bạn!!!";
        $("#divResult").html(html);
    });
});
function _getCountries() {
    $.get(countryUrl, function (data) {
        if (data != null && data != undefined && data.length) {
            var html = '';
            html += '<option value="">--Không chọn--</option>';
            $.each(data, function (key, item) {
                html += '<option value="' + item.ID + ' - ' + item.Name + '">' + item.ID + ' - ' + item.Name + '</option>';
            });
            $("#ddlCountry").html(html);
        }
        $('#ddlCountry').selectpicker('refresh');
    });
}
// truyền id của country vào
function _getProvince(id) {
    $.get(provinceUrl + "/" + fomart_split(id, 0), function (data) {
        if (data != null && data != undefined && data.length) {
            var html = '';
            html += '<option value="">--Không chọn--</option>';
            $.each(data, function (key, item) {
                html += '<option value="' + item.ID + ' - ' + item.Name + '">' + item.ID + ' - ' + item.Name + '</option>';
            });
            $("#ddlProvince").html(html);
        }
        $('#ddlProvince').selectpicker('refresh');
    });
}
// truyền id của province vào
function _getDistrict(id) {
    $.get(districtUrl + "/" + fomart_split(id, 0), function (data) {
        if (data != null && data != undefined && data.length) {
            var html = '';
            html += '<option value="">--Không chọn--</option>';
            $.each(data, function (key, item) {
                html += '<option value="' + item.ID + ' - ' + item.Name + '">' + item.ID + ' - ' + item.Name + '</option>';
            });
            $("#ddlDistrict").html(html);
        }
        $('#ddlDistrict').selectpicker('refresh');
    });
}
// truyền id của district vào
function _getWard(id) {
    $.get(wardUrl + "/" + fomart_split(id, 0), function (data) {
        if (data != null && data != undefined && data.length) {
            var html = '';
            html += '<option value="">--Không chọn--</option>';
            $.each(data, function (key, item) {
                html += '<option value="' + item.ID + ' - ' + item.Name + '">' + item.ID + ' - ' + item.Name + '</option>';
            });
            $("#ddlWard").html(html);
        }
        $('#ddlWard').selectpicker('refresh');
    });
}

function fomart_split(input, number) {
    var value = input;
    if (value != null && !value.length < 1) {
        return value.split(" - ")[number];
    } else {
        return "";
    }
}