using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class PhanMemBanHangController : Controller
    {

        ///api/Category/DeleteProduct
        public ActionResult DeleteProduct()
        {
            using (var client = new HttpClient())
            {
                object input = new
                {
                    ObjectId = "c0e3084f-6ebe-4fa0-9b9f-6e8c66b07d6b"
                };
                // "d1873bc5-48f8-4b4a-be7b-9dd58b593bbd"
                var json = JsonConvert.SerializeObject(input);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri("http://api.phanmembanhang.com/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJob3NjbyIsImF6cCI6ImFkbWluIiwianRpIjoiNTZkNjkwNDctNmI1Yi00NWJkLTk5M2YtMWRhYWI4MzBmMTFkIiwibmFtZWlkIjoiMSIsInNpZCI6IiIsIm5iZiI6MTYyNjg0OTQ4MSwiZXhwIjoxNjMyMDMzNDgxLCJpc3MiOiJob3Njby52biIsImF1ZCI6Imhvc2NvLnZuIn0.3DxbhbROU-s7izt5-xQy72Y5xIkkRVT1iEagPWtYSSI");
                var result = client.PostAsync("/api/Category/DeleteProduct", httpContent).Result;
                string strResponse = result.Content.ReadAsStringAsync().Result;
                if (result.IsSuccessStatusCode)
                {
                    var list = new JavaScriptSerializer().Deserialize<DeleteProductModel.Root>(strResponse);
                    //string chuoi = "";
                    //chuoi += list.data.Name;
                    //chuoi += list.data.ProductCode;
                    if (list.meta.message != "1")//Có dữ liệu liên quan, không được xóa
                    {
                        ViewBag.Category = list.meta.message;
                    }
                    else
                    {
                        ViewBag.Category = "Không tìm thấy ID";
                    }
                    //status_code

                }
            }
            return View();
        }

        //api/Category/GetProductInfo
        public ActionResult GetProductInfo()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.phanmembanhang.com/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJob3NjbyIsImF6cCI6ImFkbWluIiwianRpIjoiNTZkNjkwNDctNmI1Yi00NWJkLTk5M2YtMWRhYWI4MzBmMTFkIiwibmFtZWlkIjoiMSIsInNpZCI6IiIsIm5iZiI6MTYyNjg0OTQ4MSwiZXhwIjoxNjMyMDMzNDgxLCJpc3MiOiJob3Njby52biIsImF1ZCI6Imhvc2NvLnZuIn0.3DxbhbROU-s7izt5-xQy72Y5xIkkRVT1iEagPWtYSSI");
                var result = client.GetAsync("/api/Category/GetProductInfo/a66c4605-2339-4bd1-ab89-a35adf1bff15").Result;
                string strResponse = result.Content.ReadAsStringAsync().Result;
                if (result.IsSuccessStatusCode)
                {
                    var list = new JavaScriptSerializer().Deserialize<GetProductInfo_Model.Root>(strResponse);
                    //string chuoi = "";
                    //chuoi += list.data.Name;
                    //chuoi += list.data.ProductCode;
                    ViewBag.Category = list;
                }
            }
            return View();
        }

        //api/Category/ProductList
        public ActionResult ProductList()
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
            var obj = new JavaScriptSerializer().Deserialize<ProductListModel.Root>(json);
            //string chuoi = "";
            //foreach (var item in obj.data)
            //{
            //    chuoi += item.Id;
            //}
            //ViewBag.HienThi = chuoi;
            //ViewBag.objs = obj;
            return View(obj);
        }

        //api/Category/ProductList
        public ActionResult ProductListPhanTrang(string keyword = "")
        {
            #region Request
            Int16 pages = 1;
            int Tongsobanghi = 0;
            int Tongsotrang = int.Parse("100");

            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            if ((Request.QueryString["keyword"] != null) && (Request.QueryString["keyword"] != ""))
            {
                keyword = Request.QueryString["keyword"].Trim();
            }
            #endregion

            #region api
            WebClient client = new WebClient();
            var URL = "http://api.phanmembanhang.com/";
            client.Headers["Content-type"] = "application/json";
            var token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJob3NjbyIsImF6cCI6ImFkbWluIiwianRpIjoiNTZkNjkwNDctNmI1Yi00NWJkLTk5M2YtMWRhYWI4MzBmMTFkIiwibmFtZWlkIjoiMSIsInNpZCI6IiIsIm5iZiI6MTYyNjg0OTQ4MSwiZXhwIjoxNjMyMDMzNDgxLCJpc3MiOiJob3Njby52biIsImF1ZCI6Imhvc2NvLnZuIn0.3DxbhbROU-s7izt5-xQy72Y5xIkkRVT1iEagPWtYSSI";
            client.Headers["Authorization"] = token;
            client.Encoding = Encoding.UTF8;
            object input = new
            {
                productName = keyword.Replace("%20", " "),
                productCode = "",
                description = "",
                visible = true,
                instock = 0,
                productGroup = "",
                pageSize = Tongsotrang,
                pageIndex = (pages - 1)
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            string json = client.UploadString(URL + "api/Category/ProductList", inputJson);
            var obj = new JavaScriptSerializer().Deserialize<ProductListModel.Root>(json);

            #endregion

            #region Phantrang
            Tongsobanghi = obj.paging.TotalCount;
            if (Tongsobanghi % Tongsotrang > 0)
            {
                Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
            }
            else
            {
                Tongsobanghi = Tongsobanghi / Tongsotrang;
            }
            ViewBag.Phantrang = Commond.Phantrang("/PhanMemBanHang/ProductListPhanTrang", Tongsobanghi, pages);
            #endregion

            return View(obj);
        }

        //api/Category/AddNewProduct
        public ActionResult AddNewProduct(AddNewProductModel.ThemMoiSanPham request)
        {
            HttpClientHandler handler = new HttpClientHandler();
            using (var client = new HttpClient(handler, false))
            {
                client.BaseAddress = new Uri("http://api.phanmembanhang.com/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJob3NjbyIsImF6cCI6ImFkbWluIiwianRpIjoiNTZkNjkwNDctNmI1Yi00NWJkLTk5M2YtMWRhYWI4MzBmMTFkIiwibmFtZWlkIjoiMSIsInNpZCI6IiIsIm5iZiI6MTYyNjg0OTQ4MSwiZXhwIjoxNjMyMDMzNDgxLCJpc3MiOiJob3Njby52biIsImF1ZCI6Imhvc2NvLnZuIn0.3DxbhbROU-s7izt5-xQy72Y5xIkkRVT1iEagPWtYSSI");

                //9b1ac48d-e947-4573-a7fe-591ffec0fc0c
                #region request Obj
                Guid g = Guid.NewGuid();
                // request.Id = g.ToString();
                request.ProductName = "Demo Dũng News" + DateTime.Now.ToString();
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
                #endregion

                var json = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var result = client.PostAsync("/api/Category/AddNewProduct", httpContent).Result;
                string strResponse = result.Content.ReadAsStringAsync().Result;
                if (result.IsSuccessStatusCode)
                {
                    var obj = JsonConvert.DeserializeObject<AddNewProductModel.RootThemMoi>(strResponse);
                    ViewBag.HienThi += "Thêm mới thành công : " + obj.data.Id + "###" + obj.data.Name + "###" + obj.data.ProductCode;
                    ViewBag.HienThi += "<br><br><br><br>: " + obj.data;
                    //  var data = result.Content.ReadAsAsync<List<RootThemMoi>>().Result;
                }
            }
            return View();
        }

        // api/Category/UpdateProduct
        public ActionResult UpdateProduct(AddNewProductModel.ThemMoiSanPham request)
        {
            HttpClientHandler handler = new HttpClientHandler();
            using (var client = new HttpClient(handler, false))
            {
                client.BaseAddress = new Uri("http://api.phanmembanhang.com/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJob3NjbyIsImF6cCI6ImFkbWluIiwianRpIjoiNTZkNjkwNDctNmI1Yi00NWJkLTk5M2YtMWRhYWI4MzBmMTFkIiwibmFtZWlkIjoiMSIsInNpZCI6IiIsIm5iZiI6MTYyNjg0OTQ4MSwiZXhwIjoxNjMyMDMzNDgxLCJpc3MiOiJob3Njby52biIsImF1ZCI6Imhvc2NvLnZuIn0.3DxbhbROU-s7izt5-xQy72Y5xIkkRVT1iEagPWtYSSI");

                //a66c4605-2339-4bd1-ab89-a35adf1bff15
                request.Id = "cae2027b-e175-4a70-b0ff-df9bbfdda232";
                request.ProductName = "Tịnh Minh Dũng";
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
                var result = client.PostAsync("/api/Category/UpdateProduct", httpContent).Result;
                string strResponse = result.Content.ReadAsStringAsync().Result;
                if (result.IsSuccessStatusCode)
                {
                    var obj = JsonConvert.DeserializeObject<AddNewProductModel.RootThemMoi>(strResponse);
                    ViewBag.HienThi += "Sửa thành công : " + obj.data.Id + "--" + obj.data.Name + "--" + obj.data.ProductCode;
                    ViewBag.HienThi += "<br><br><br><br>: " + obj.data;
                    //  var data = result.Content.ReadAsAsync<List<RootThemMoi>>().Result;
                }
            }
            return View();
        }

        //  POST /api/Customer/AddNewPurchaseOrder
        public ActionResult AddNewPurchaseOrder(AddNewPurchaseOrderModel.Root request)
        {
           
            HttpClientHandler handler = new HttpClientHandler();
            using (var client = new HttpClient(handler, false))
            {
                client.BaseAddress = new Uri("http://api.phanmembanhang.com/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJob3NjbyIsImF6cCI6ImFkbWluIiwianRpIjoiNTZkNjkwNDctNmI1Yi00NWJkLTk5M2YtMWRhYWI4MzBmMTFkIiwibmFtZWlkIjoiMSIsInNpZCI6IiIsIm5iZiI6MTYyNjg0OTQ4MSwiZXhwIjoxNjMyMDMzNDgxLCJpc3MiOiJob3Njby52biIsImF1ZCI6Imhvc2NvLnZuIn0.3DxbhbROU-s7izt5-xQy72Y5xIkkRVT1iEagPWtYSSI");
                Guid g = Guid.NewGuid();
                Guid odrr = Guid.NewGuid();


                var pro = new List<Entity.Products>();
                pro = SProducts.Name_Text("select top 1 * from products");

                var orderDetails = new List<AddNewPurchaseOrderModel.OrderDetail>();
                foreach (var item in pro)
                {
                    orderDetails.Add(new AddNewPurchaseOrderModel.OrderDetail()
                    {
                        Id = odrr.ToString(),// phải lấy ID từ sản phẩm đã thêm vào
                        OrderId = g.ToString(),
                        ProductId = "9b12153a-c98d-4238-86bc-245d4f2604ac",
                        ProductName = item.Name,
                        ProductCode = item.Code,
                        Unit = "40000",
                        Price = 23300,
                        Qty = item.ipid,
                        f_Discount = 0,
                        m_Discount = 0,
                        Description = item.Code,
                        f_Convert = 0,
                        StoreId = "11638865-b070-44a9-8f13-11fa05eb0ffc"//http://api.phanmembanhang.com/api/Category/StoreList
                    });
                }


                request.Id = g.ToString();
                request.OrderCode = "Nguyễn Dũng";
                request.EmployeeId = "817e0714-71be-4210-9a4f-2e58859a425d";//http://api.phanmembanhang.com/api/Category/EmployeeList  // lấy danh sách nhân viên trên api
                request.CustomerId = "b272f4e9-3468-42fd-be0e-2c925a39117d";//http://api.phanmembanhang.com/api/Category/GetCustomerInfo/b272f4e9-3468-42fd-be0e-2c925a39117d// phải tạo thành viên trên api
                request.OrderDate = DateTime.Now;
                request.Status = 0;
                request.BillingAddress = "string";
                request.ShippingAddress = "string";
                request.OrderTotal = 0;
                request.f_Vat = 0;
                request.m_Vat = 0;
                request.OrderTotalDiscount = 0;
                request.f_Discount = 0;
                request.m_Discount = 0;
                request.m_TotalMoney = 0;
                request.Discription = "string";

                request.OrderDetail = orderDetails;

                request.Description = "Thư Demo";
                request.CreatedBy = "DungABC";
                request.ModifiedBy = "12";
                request.CreatedDate = DateTime.Now;
                request.ModifiedDate = DateTime.Now;
                request.Location_Id = "CN01";//http://api.phanmembanhang.com/api/Category/LocationList

                var json = JsonConvert.SerializeObject(request);
                // xem lại kết quả json, ko hiểu sao cứ fomat bỏ cái dấu " ở đầu {} thì nó lại dc, xong thì cho vào swagger thì lại dc
                // sau khi có jsson thì coppy và cho vào đây fomat xem có lỗi ko https://jsonformatter.curiousconcept.com/#
                // sau đó coppy dán vào trang http://api.phanmembanhang.com/swagger/#!/Customer/ApiCustomerAddNewPurchaseOrderPost xem có lỗi ko , nếu có lỗi hỏi bên phần mềm API
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var result = client.PostAsync("/api/Customer/AddNewPurchaseOrder", httpContent).Result;
                string strResponse = result.Content.ReadAsStringAsync().Result;
                if (result.IsSuccessStatusCode)
                {
                    var obj = JsonConvert.DeserializeObject<DeleteProductModel.Root>(strResponse);
                    // ViewBag.HienThi += "Thêm mới thành công : " + obj.data.Id + "--" + obj.data.Name + "--" + obj.data.ProductCode;
                    //  ViewBag.HienThi += "<br><br><br><br>: " + obj.data;
                    //  var data = result.Content.ReadAsAsync<List<RootThemMoi>>().Result;
                }
            }
            return View();
        }

        //Parameters đã đẩy lên thành công với "Id":"c1cfd428-476e-4862-8074-22622a01da97",  http://api.phanmembanhang.com/api/PurchaseOrder/GetPurchaseOrderDetail/c1cfd428-476e-4862-8074-22622a01da97
        //        {
        //   "Id":"c1cfd428-476e-4862-8074-22622a01da97",// xem chi tiết đơn http://api.phanmembanhang.com/api/PurchaseOrder/GetPurchaseOrderDetail/c1cfd428-476e-4862-8074-22622a01da97
        //   "OrderCode":"Nguyễn Dũng",
        //   "EmployeeId":"817e0714-71be-4210-9a4f-2e58859a425d",
        //   "CustomerId":"b272f4e9-3468-42fd-be0e-2c925a39117d",
        //   "OrderDate":"2021-07-22T17:26:59.1319737+07:00",
        //   "Status":0,
        //   "BillingAddress":"string",
        //   "ShippingAddress":"string",
        //   "OrderTotal":0.0,
        //   "f_Vat":0.0,
        //   "m_Vat":0.0,
        //   "OrderTotalDiscount":0.0,
        //   "f_Discount":0.0,
        //   "m_Discount":0.0,
        //   "m_TotalMoney":0.0,
        //   "Discription":"string",
        //   "OrderDetail":[
        //      {
        //         "Id":"9b12153a-c98d-4238-86bc-245d4f2604ac",
        //         "OrderId":"c1cfd428-476e-4862-8074-22622a01da97",
        //         "ProductId":"9b12153a-c98d-4238-86bc-245d4f2604ac",
        //         "ProductName":"Bàn kệ tivi BK1111",
        //         "ProductCode":"pk",
        //         "Unit":"40000",
        //         "Price":23300.0,
        //         "Qty":37.0,
        //         "f_Discount":0.0,
        //         "m_Discount":0.0,
        //         "Description":"pk",
        //         "f_Convert":0.0,
        //         "StoreId":"11638865-b070-44a9-8f13-11fa05eb0ffc"
        //      }
        //   ],
        //   "Description":"Thư Demo",
        //   "CreatedBy":"DungABC",
        //   "ModifiedBy":"12",
        //   "CreatedDate":"2021-07-22T17:27:01.7671956+07:00",
        //   "ModifiedDate":"2021-07-22T17:27:01.8911176+07:00",
        //   "Location_Id":"CN01"
        //}



        //        {
        //   "Id":"d7cd43ae-535a-40cf-8e7e-81484a0b874d",
        //   "OrderCode":"Nguyễn Dũng",
        //   "EmployeeId":"817e0714-71be-4210-9a4f-2e58859a425d",
        //   "CustomerId":"b272f4e9-3468-42fd-be0e-2c925a39117d",
        //   "OrderDate":"2021-07-24T10:35:30.2359476+07:00",
        //   "Status":0,
        //   "BillingAddress":"string",
        //   "ShippingAddress":"string",
        //   "OrderTotal":0.0,
        //   "f_Vat":0.0,
        //   "m_Vat":0.0,
        //   "OrderTotalDiscount":0.0,
        //   "f_Discount":0.0,
        //   "m_Discount":0.0,
        //   "m_TotalMoney":0.0,
        //   "Discription":"string",
        //   "OrderDetail":[
        //      {
        //         "Id":"edd5c647-09ca-4e49-b2b0-53dcbbcc0173",
        //         "OrderId":"d7cd43ae-535a-40cf-8e7e-81484a0b874d",
        //         "ProductId":"9b12153a-c98d-4238-86bc-245d4f2604ac",
        //         "ProductName":"Bàn kệ tivi BK1111",
        //         "ProductCode":"pk",
        //         "Unit":"40000",
        //         "Price":23300.0,
        //         "Qty":37.0,
        //         "f_Discount":0.0,
        //         "m_Discount":0.0,
        //         "Description":"pk",
        //         "f_Convert":0.0,
        //         "StoreId":"11638865-b070-44a9-8f13-11fa05eb0ffc"
        //      }
        //   ],
        //   "Description":"Thư Demo",
        //   "CreatedBy":"DungABC",
        //   "ModifiedBy":"12",
        //   "CreatedDate":"2021-07-24T10:35:33.9389746+07:00",
        //   "ModifiedDate":"2021-07-24T10:35:34.2358653+07:00",
        //   "Location_Id":"CN01"
        //}
    }
}