
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace VS.ECommerce_MVC.Controllers
{
    public class GiaoHangTietKiemController : Controller
    {
        // GET: 
        // Tính phí vận chuyển
        public ActionResult TInhPhiVanChuyen()
        {
            object input = new
            {
                pick_province = "Hà Nội",
                pick_district = "Quận Hai Bà Trưng",
                province = "Hà nội",
                district = "Quận Cầu Giấy",
                address = "P.503 tòa nhà Auu Việt, số 1 Lê Đức Thọ",
                weight = 1000,
                value = 3000000,
                transport = "fly",
                deliver_option = "xteam",
                tags = ""
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            var URL = "https://services.giaohangtietkiem.vn";
            client.Headers["Content-type"] = "application/json";
            var token = "89ceA31EC615B829085cDe3f6f155bFcFc8987Af";
            client.Headers["Token"] = token;

            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(URL + "/services/shipment/fee", inputJson);
            var obj = new JavaScriptSerializer().Deserialize<TinhPhi.Root>(json);
            ViewBag.HienThi = " Cước vận chuyển tính theo VNĐ:" + obj.fee.fee;

            return View();
        }
        public ActionResult DatHang()
        {
            var pro = new List<Entity.Products>();
            pro = SProducts.Name_Text("select top 1 * from products");
            var orderDetails = new List<DauVaoDatHang.Product>();
            foreach (var item in pro)
            {
                orderDetails.Add(new DauVaoDatHang.Product()
                {
                    name = item.Name,
                    weight = 0.1,
                    quantity = item.ipid,
                    product_code = item.ipid + item.ipid
                });
            }
            object[] array = new object[2] { 1, 7 };
            var Orders = new DauVaoDatHang.Order()
            {
                id = "123f33fg990",
                pick_name = "HCM-nội thành",
                pick_address = "590 CMT8 P.11",
                pick_province = "TP. Hồ Chí Minh",
                pick_district = "Quận 3",
                pick_ward = "Phường 1",
                pick_tel = "0978456122",
                tel = "0987774587",
                name = "GHTK - HCM - Noi Thanh",
                address = "123 nguyễn chí thanh",
                province = "TP. Hồ Chí Minh",
                district = "Quận 1",
                ward = "Phường Bến Nghé",
                hamlet = "Khác",
                is_freeship = "1",
                pick_date = "2016-09-30",
                pick_money = 47000,
                note = "Khối lượng tính cước tối đa: 1.00 kg",
                value = 3000000,
                transport = "fly",
                pick_option = "cod",
                deliver_option = "xteam",
                pick_session = 2,
                tags = array.ToArray()
            };
            var request = new DauVaoDatHang.Root()
           {
               products = orderDetails,
               order = Orders
           };

            string inputJson = (new JavaScriptSerializer()).Serialize(request);
            WebClient client = new WebClient();
            var URL = "https://services.giaohangtietkiem.vn";
            client.Headers["Content-type"] = "application/json";
            var token = "89ceA31EC615B829085cDe3f6f155bFcFc8987Af";
            client.Headers["Token"] = token;

            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(URL + "/services/shipment/order/?ver=1.5", inputJson);
            var obj = new JavaScriptSerializer().Deserialize<DatHang.Root>(json);

            ViewBag.HienThi += inputJson + "<br />";
            ViewBag.HienThi += json + "<br />";

            if (obj.success == true)
            {
                ViewBag.HienThi += " Thông báo : " + obj.message + "<br />";
                ViewBag.HienThi += " nhãn đơn GHTK, tương ứng với id của bạn" + obj.order.label + "<br />";
                ViewBag.HienThi += " mã đơn hàng của đối tác Ghtk - id trong request đẩy sang của bạn" + obj.order.partner_id + "<br />";
                ViewBag.HienThi += "trạng thái đơn hàng hiện tại trên hệ thống GHTK, tra bảng mã trạng thái đơn trong phần webhook   :" + obj.order.status_id + "<br />";
            }
            else
            {
                ViewBag.HienThi += " Thông báo : " + obj.message;
            }
            return View();
        }

    }
}