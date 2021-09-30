using GHTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using VS.ECommerce_MVC.Models;
using Services;
using System.Web.Script.Serialization;


namespace VS.ECommerce_MVC.Controllers
{
    public class API_ProdutcsController : Controller
    {
        public class ViewProduct
        {
            public int icid { get; set; }
            public string Brief { get; set; }
            public string Code { get; set; }
            public string Create_Date { get; set; }
            public int ID_Hang { get; set; }
            public string Images { get; set; }
            public string ImagesSmall { get; set; }
            public int ipid { get; set; }
            public string Name { get; set; }
            public string Price { get; set; }
            public string Alt { get; set; }
            public string OldPrice { get; set; }
            public string TangName { get; set; }
        }

        public ActionResult All_SanPham_api()
        {
            object input = new
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
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient client = new WebClient();
            var URL = "http://api.phanmembanhang.com/";
            client.Headers["Content-type"] = "application/json";
            var token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJob3NjbyIsImF6cCI6ImFkbWluIiwianRpIjoiNTZkNjkwNDctNmI1Yi00NWJkLTk5M2YtMWRhYWI4MzBmMTFkIiwibmFtZWlkIjoiMSIsInNpZCI6IiIsIm5iZiI6MTYyNjg0OTQ4MSwiZXhwIjoxNjMyMDMzNDgxLCJpc3MiOiJob3Njby52biIsImF1ZCI6Imhvc2NvLnZuIn0.3DxbhbROU-s7izt5-xQy72Y5xIkkRVT1iEagPWtYSSI";
            client.Headers["Authorization"] = token;

            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(URL + "api/Category/ProductList", inputJson);
            var obj = new JavaScriptSerializer().Deserialize<Root>(json);
            //string chuoi = "";
            //foreach (var item in obj.data)
            //{
            //    chuoi += item.Id;
            //}
            //ViewBag.HienThi = chuoi;

            //ViewBag.objs = obj;
            return View(obj);
        }


        public ActionResult Index()
        {
            List<Entity.Products> list = new List<Entity.Products>();
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:63136/api/product/GetById/37").Result;
            if (result.IsSuccessStatusCode)
            {
                list = result.Content.ReadAsAsync<List<Entity.Products>>().Result;
            }
            return View(list);
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        #region ThemMoiSanPham
        public class ThemMoiSanPham
        {
            public string Id { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public string Unit { get; set; }
            public int UnitPrice { get; set; }
            public int PurchasePrice { get; set; }
            public int MinInStock { get; set; }
            public int MaxInStock { get; set; }
            public string GroupId { get; set; }
            public string Description { get; set; }
            public string s_NoteImport { get; set; }
            public string s_NoteOrder { get; set; }
            public string Picture { get; set; }
        }

        public ActionResult ThemMoiSanPhamAPIBanHang(ThemMoiSanPham request)
        {
            HttpClientHandler handler = new HttpClientHandler();
            using (var client = new HttpClient(handler, false))
            {
                client.BaseAddress = new Uri("http://api.phanmembanhang.com/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJob3NjbyIsImF6cCI6ImFkbWluIiwianRpIjoiNTZkNjkwNDctNmI1Yi00NWJkLTk5M2YtMWRhYWI4MzBmMTFkIiwibmFtZWlkIjoiMSIsInNpZCI6IiIsIm5iZiI6MTYyNjg0OTQ4MSwiZXhwIjoxNjMyMDMzNDgxLCJpc3MiOiJob3Njby52biIsImF1ZCI6Imhvc2NvLnZuIn0.3DxbhbROU-s7izt5-xQy72Y5xIkkRVT1iEagPWtYSSI");

                //9b1ac48d-e947-4573-a7fe-591ffec0fc0c
                Guid g = Guid.NewGuid();
                // request.Id = g.ToString();
                request.ProductName = "Demo Dũng" + g.ToString();
                request.ProductCode = "abc";
                request.Unit = "";
                request.UnitPrice = 0;
                request.PurchasePrice = 0;
                request.MinInStock = 0;
                request.MaxInStock = 0;
                request.GroupId = "043b3224-c54b-4db0-8934-7325d6b4fea7";// thuốc bổ gan http://api.phanmembanhang.com/api/Category/ProductGroupList
                request.Description = "33";
                request.s_NoteImport = "44";
                request.s_NoteOrder = "55";
                request.Picture = "66";

                var json = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var result = client.PostAsync("/api/Category/AddNewProduct", httpContent).Result;
                string strResponse = result.Content.ReadAsStringAsync().Result;
                if (result.IsSuccessStatusCode)
                {
                    var obj = JsonConvert.DeserializeObject<RootThemMoi>(strResponse);
                    ViewBag.HienThi += "Thêm mới thành công : " + obj.data.Id + "--" + obj.data.Name + "--" + obj.data.ProductCode;
                    ViewBag.HienThi += "<br><br><br><br>: " + obj.data;
                    //  var data = result.Content.ReadAsAsync<List<RootThemMoi>>().Result;
                }
            }
            return View();
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

        public class DataThemMoi
        {
            public DataThemMoi()
            {
                ImageUrls = new List<string>();
                ExtraLabels = new List<string>();
                ExtraValues = new List<string>();
            }
            public string Id { get; set; }
            public string Name { get; set; }
            public string ProductCode { get; set; }
            public string Unit { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Price { get; set; }
            public float InStock { get; set; }
            public float MinInStock { get; set; }
            public float MaxInStock { get; set; }
            public string BranchName { get; set; }
            public string GroupName { get; set; }
            public string GroupId { get; set; }
            public string Description { get; set; }
            public string s_Specification { get; set; }
            public string s_Quantification { get; set; }
            public string s_Color { get; set; }
            public string s_TacDungChinh { get; set; }
            public string s_NoteImport { get; set; }
            public string s_NoteOrder { get; set; }
            public string s_ThanhPhan { get; set; }
            public List<string> ImageUrls { get; set; }
            public List<string> ExtraLabels { get; set; }
            public List<string> ExtraValues { get; set; }
            public string Picture { get; set; }
            public string Unit_ID { get; set; }
            public float f_Convert { get; set; }
        }
        public class DataThemMoisss
        {
            public string Id { get; set; }
            // [JsonProperty(" Name")]
            public string Name { get; set; }
            public string ProductCode { get; set; }
            public string Unit { get; set; }
            public double UnitPrice { get; set; }
            public double Price { get; set; }
            public double InStock { get; set; }
            public double MinInStock { get; set; }
            public double MaxInStock { get; set; }
            public string BranchName { get; set; }
            public string GroupName { get; set; }
            public string GroupId { get; set; }
            public string Description { get; set; }
            public object s_Specification { get; set; }
            public object s_Quantification { get; set; }
            public object s_Color { get; set; }
            public object s_TacDungChinh { get; set; }
            public string s_NoteImport { get; set; }
            public string s_NoteOrder { get; set; }
            public object s_ThanhPhan { get; set; }
            public List<object> ImageUrls { get; set; }
            public List<object> ExtraLabels { get; set; }
            public List<object> ExtraValues { get; set; }
            public object Picture { get; set; }
            public object Unit_ID { get; set; }
            public int f_Convert { get; set; }
        }

        public class MetaThemMoi
        {
            public double status_code { get; set; }
            public string message { get; set; }
        }

        public class RootThemMoi
        {
            public DataThemMoi data { get; set; }
            public MetaThemMoi meta { get; set; }
        }
        #endregion


        public ActionResult All()
        {
            //Có thể Lấy luôn Entity.Products để chạy nếu cùng 1 dự án code
            //Khi đưa sang dự án khác để kết nối API thì phải viết lại Entity cho nó như là: ViewProduct
            //List<Entity.Products> list = new List<Entity.Products>();
            List<ViewProduct> list = new List<ViewProduct>();
            HttpClient client = new HttpClient();
            var result = client.GetAsync("http://localhost:63136/api/product/GetAll").Result;
            if (result.IsSuccessStatusCode)
            {
                //Có thể Lấy luôn Entity.Products để chạy nếu cùng 1 dự án code
                //Khi đưa sang dự án khác để kết nối API thì phải viết lại Entity cho nó như là: ViewProduct
                // list = result.Content.ReadAsAsync<List<Entity.Products>>().Result;
                list = result.Content.ReadAsAsync<List<ViewProduct>>().Result;
            }
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }
        //public ActionResult All_SanPham_api()
        //{
        //    HttpClientHandler handler = new HttpClientHandler();
        //    using (var client = new HttpClient(handler, false))
        //    {
        //        List<Root> list = new List<Root>();
        //        client.BaseAddress = new Uri("http://api.phanmembanhang.com/");
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJob3NjbyIsImF6cCI6ImFkbWluIiwianRpIjoiNTZkNjkwNDctNmI1Yi00NWJkLTk5M2YtMWRhYWI4MzBmMTFkIiwibmFtZWlkIjoiMSIsInNpZCI6IiIsIm5iZiI6MTYyNjg0OTQ4MSwiZXhwIjoxNjMyMDMzNDgxLCJpc3MiOiJob3Njby52biIsImF1ZCI6Imhvc2NvLnZuIn0.3DxbhbROU-s7izt5-xQy72Y5xIkkRVT1iEagPWtYSSI");

        //        var httpContent = new StringContent("{\"productName\":\"\",\"productCode\":\"\",\"description\":\"\",\"visible\":true,\"instock\":0,\"productGroup\":\"\",\"pageSize\":10,\"pageIndex\":10}", Encoding.UTF8, "application/json");

        //        //var json = JsonConvert.SerializeObject(request);
        //        //var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        //        var result = client.PostAsync("/api/Category/ProductList", httpContent).Result;
        //        string strResponse = result.Content.ReadAsStringAsync().Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            List<Root> obj = JsonConvert.DeserializeObject<List<Root>>(strResponse) as List<Root>;
        //            foreach (var item in obj[0].data)
        //            {
        //                ViewBag.HienThi += "Lấy danh sách : <br><br><br><br> " + item.Id + "--" + item.Name + "--" + item.ProductCode;
        //            }
        //            ViewBag.HienThi += "<br><br><br><br>: " + obj;
        //        }
        //    }
        //    return View();
        //}

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Datum
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string ProductCode { get; set; }
            public string Unit { get; set; }
            public double UnitPrice { get; set; }
            public double Price { get; set; }
            public double InStock { get; set; }
            public double MinInStock { get; set; }
            public double MaxInStock { get; set; }
            public string BranchName { get; set; }
            public string GroupName { get; set; }
            public string GroupId { get; set; }
            public string Description { get; set; }
            public string s_Specification { get; set; }
            public string s_Quantification { get; set; }
            public string s_Color { get; set; }
            public string s_TacDungChinh { get; set; }
            public string s_NoteImport { get; set; }
            public string s_NoteOrder { get; set; }
            public string s_ThanhPhan { get; set; }
            public List<string> ImageUrls { get; set; }
            public List<string> ExtraLabels { get; set; }
            public List<string> ExtraValues { get; set; }
            public string Picture { get; set; }
            public string Unit_ID { get; set; }
            public double f_Convert { get; set; }
        }
        public class Paging
        {
            public int TotalPage { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int TotalCount { get; set; }
        }

        public class Meta
        {
            public int status_code { get; set; }
            public string message { get; set; }
        }

        public class Extra
        {
            public int TotalStock { get; set; }
        }

        public class Root
        {
            public List<Datum> data { get; set; }
            public Paging paging { get; set; }
            public Meta meta { get; set; }
            public Extra Extra { get; set; }
        }
    }
}