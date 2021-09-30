using MoreAll;
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using VS.ECommerce_MVC;

public class VanchuyenGiaoHangTietKiem
{

    // Cách chạy không thông qua MVC mà chạy theo dạng code webfrom load sự kiện
    public static string TaoDonHang()
    {
        HttpClientHandler handler = new HttpClientHandler();
        using (var client = new HttpClient(handler, false))
        {

            client.BaseAddress = new Uri("https://services.giaohangtietkiem.vn");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Token", "89ceA31EC615B829085cDe3f6f155bFcFc8987Af");

            var pro = new List<Entity.Products>();
            pro = SProducts.Name_Text("select top 5 * from products");
            var orderDetails = new List<VanChuyenGiaoHangTK.ProductsItem>();
            foreach (var item in pro)
            {
                orderDetails.Add(new VanChuyenGiaoHangTK.ProductsItem()
                {
                    name = item.Name,
                    weight = 0.1,
                    quantity = item.ipid,
                    product_code = item.ipid + item.ipid
                });
            }

            var Orders = new VanChuyenGiaoHangTK.Order()
            {
                id = "123ffg990",
                pick_name = "HCM-nội thành",
                pick_address = "590 CMT8 P.11",
                pick_province = "TP. Hồ Chí Minh",
                pick_district = "Quận 3",
                pick_ward = "Phường 1",
                pick_tel = "0911222333",
                tel = "0911222333",
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
                pick_session = 2
            };
            var request = new List<VanChuyenGiaoHangTK.Root>();
            request.Add(new VanChuyenGiaoHangTK.Root()
            {
                products = orderDetails,
                order = Orders
            });
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            // /Chú ý: sau khi gen đến đây thì lấy dữ liệu của request  rồi lại cho lên trang http://json.parser.online.fr/ fomat
            // sau khi fomat xong thì lại coppy vào posmant để chạy thử xem nó báo lỗi j
            var response = client.PostAsync("/services/shipment/order/?ver=1.5", httpContent);
            return response.ToString();
        }
    }

    public static string LayDanhSachSanPham_API_BanHang()
    {
        HttpClientHandler handler = new HttpClientHandler();
        using (var client = new HttpClient(handler, false))
        {
            client.BaseAddress = new Uri("http://api.phanmembanhang.com/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJob3NjbyIsImF6cCI6ImFkbWluIiwianRpIjoiMmI5ZDVlNzYtZGY2OC00NWU4LWE2ZjYtOTU4NWEyYmI2ZjFmIiwibmFtZWlkIjoiMSIsInNpZCI6IiIsIm5iZiI6MTYyNjgzOTgxNywiZXhwIjoxNjMyMDIzODE3LCJpc3MiOiJob3Njby52biIsImF1ZCI6Imhvc2NvLnZuIn0.xRc63u0527TShRFFTTCc4WGempL8ZVSoCjp1jSIYX2");
            var request = new LayToanBoDanhSachSanPham()
            {
                productName = "",
                productCode = "",
                description = "",
                visible = true,
                instock = 0,
                productGroup = "",
                pageSize = 10,
                pageIndex = 10
            };

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            // /Chú ý: sau khi gen đến đây thì lấy dữ liệu của request  rồi lại cho lên trang http://json.parser.online.fr/ fomat
            // sau khi fomat xong thì lại coppy vào posmant để chạy thử xem nó báo lỗi j
            var response = client.PostAsync("/api/Category/ProductList", httpContent);

            return response.ToString();
        }

    }

    public class LayToanBoDanhSachSanPham
    {
        public String productName;
        public String productCode;
        public String description;
        public Boolean visible;
        public int instock;
        public String productGroup;
        public int pageSize;
        public int pageIndex;
    }
}