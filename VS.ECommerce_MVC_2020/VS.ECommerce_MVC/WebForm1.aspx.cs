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
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace VS.ECommerce_MVC
{
    public partial class WebForm1 : System.Web.UI.Page
    {
  
        DatalinqDataContext db = new DatalinqDataContext();
        //public static string getOrderTracesByJson(string requestData, string urlStr, string method)
        //{
        //    string result = null;
        //    if (method.Equals("post"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/trackings/post";
        //        string RelUrl = ReqURL + urlStr;
        //        result = sendPost(ReqURL, requestData, "POST");

        //    }
        //    else if (method.Equals("get"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/trackings/get";
        //        string RelUrl = ReqURL + urlStr;
        //        //Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "GET");

        //    }
        //    else if (method.Equals("batch"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/trackings/batch";
        //        string RelUrl = ReqURL + urlStr;
        //        //Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "POST");

        //    }
        //    else if (method.Equals("codeNumberGet"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/trackings";
        //        string RelUrl = ReqURL + urlStr;
        //        //Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "GET");

        //    }
        //    else if (method.Equals("codeNumberPut"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/trackings";
        //        string RelUrl = ReqURL + urlStr;
        //        //Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "PUT");

        //    }
        //    else if (method.Equals("codeNumberDel"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/trackings";
        //        string RelUrl = ReqURL + urlStr;
        //        //Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "DELETE");

        //    }
        //    else if (method.Equals("realtime"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/trackings/realtime";
        //        string RelUrl = ReqURL + urlStr;
        //        //Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "POST");

        //    }
        //    else if (method.Equals("carriers"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/carriers";
        //        string RelUrl = ReqURL + urlStr;
        //        //Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "GET");

        //    }
        //    else if (method.Equals("carriers/detect"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/carriers/detect";
        //        string RelUrl = ReqURL + urlStr;
        //        //Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "POST");

        //    }
        //    else if (method.Equals("update"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/trackings/update";
        //        string RelUrl = ReqURL + urlStr;
        //        //Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "POST");

        //    }
        //    else if (method.Equals("getuserinfo"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/trackings/getuserinfo";
        //        string RelUrl = ReqURL + urlStr;
        //        //Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "GET");

        //    }
        //    else if (method.Equals("getstatusnumber"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/trackings/getstatusnumber";
        //        string RelUrl = ReqURL + urlStr;
        //        //Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "GET");

        //    }
        //    else if (method.Equals("notupdate"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/trackings/notupdate";
        //        string RelUrl = ReqURL + urlStr;
        //        //Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "POST");

        //    }
        //    else if (method.Equals("remote"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/trackings/remote";
        //        string RelUrl = ReqURL + urlStr;
        //        Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "POST");

        //    }
        //    else if (method.Equals("costtime"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/trackings/costtime";
        //        string RelUrl = ReqURL + urlStr;
        //        //Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "POST");

        //    }
        //    else if (method.Equals("delete"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/trackings/delete";
        //        string RelUrl = ReqURL + urlStr;
        //        //Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "POST");

        //    }
        //    else if (method.Equals("updatemore"))
        //    {

        //        string ReqURL = "http://api.tracktry.com/v1/trackings/updatemore";
        //        string RelUrl = ReqURL + urlStr;
        //        //Console.WriteLine("RelUrl:" + RelUrl);
        //        result = sendPost(RelUrl, requestData, "POST");

        //    }
        //    return result;

        //}

        private string sendPost(string url, string requestData, string method)
        {
            string result = "";
            byte[] byteData = null;
            if (requestData != null)
            {
                byteData = Encoding.GetEncoding("UTF-8").GetBytes(requestData.ToString());
            }
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Timeout = 30 * 1000;
                request.Method = method;
                request.Headers["Token"] = "89ceA31EC615B829085cDe3f6f155bFcFc8987Af";
                if (byteData != null)
                {
                    Stream stream = request.GetRequestStream();
                    stream.Write(byteData, 0, byteData.Length);
                    stream.Flush();
                    stream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream backStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(backStream, Encoding.GetEncoding("UTF-8"));
                result = sr.ReadToEnd();
                sr.Close();
                backStream.Close();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        private string sendPostPhanMem(string url, string requestData, string method)
        {
            string result = "";
            byte[] byteData = null;
            if (requestData != null)
            {
                byteData = Encoding.GetEncoding("UTF-8").GetBytes(requestData.ToString());
            }
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Timeout = 30 * 1000;
                request.Method = method;
                request.Headers["Authorization"] = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJob3NjbyIsImF6cCI6ImFkbWluIiwianRpIjoiNTZkNjkwNDctNmI1Yi00NWJkLTk5M2YtMWRhYWI4MzBmMTFkIiwibmFtZWlkIjoiMSIsInNpZCI6IiIsIm5iZiI6MTYyNjg0OTQ4MSwiZXhwIjoxNjMyMDMzNDgxLCJpc3MiOiJob3Njby52biIsImF1ZCI6Imhvc2NvLnZuIn0.3DxbhbROU-s7izt5-xQy72Y5xIkkRVT1iEagPWtYSSI";
                if (byteData != null)
                {
                    Stream stream = request.GetRequestStream();
                    stream.Write(byteData, 0, byteData.Length);
                    stream.Flush();
                    stream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream backStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(backStream, Encoding.GetEncoding("UTF-8"));
                result = sr.ReadToEnd();
                sr.Close();
                backStream.Close();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public class Tes1
        {
            public Tes1()
            {
                this.Tes2 = new List<Tes2>();
            }

            public int ID;
            public string Ten;

            // dbo.Tes2.IDTes1 -> dbo.Tes1.ID (FK_Tes2_Tes1)
            public List<Tes2> Tes2;
        }

        public class Tes2
        {
            public int ID;
            public int? IDTes1;
            public string Name;

            // dbo.Tes2.IDTes1 -> dbo.Tes1.ID (FK_Tes2_Tes1)
            public Tes1 Tes1;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var query = from p in db.products
                        join pt in db.Menus on p.icid equals pt.ID 
                        where p.Status == 1
                        select new { p, pt };
            query = query.Where(p => p.pt.ID == 75);




            AddNewProductModel.ThemMoiSanPham request = new AddNewProductModel.ThemMoiSanPham();
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

                    Response.Write("Thêm mới thành công : " + obj.data.Id + "###" + obj.data.Name + "###" + obj.data.ProductCode);
                    Response.Write("<br><br><br><br>: " + obj.data);
                    //  var data = result.Content.ReadAsAsync<List<RootThemMoi>>().Result;
                }
            }


            // Cách 1 thông qua dạng chưa fomat json
            // Học các của trang https://www.tracktry.com/api-csharp.html
            //string requestData = "{ \"products\":[ { \"name\":\"Bàn kệ tivi 7777777777777\", \"weight\":0.1, \"quantity\":37, \"product_code\":74 } ], \"order\":{ \"id\":\"8888s\", \"pick_name\":\"HCM-nội thành\", \"pick_address\":\"590 CMT8 P.11\", \"pick_province\":\"TP. Hồ Chí Minh\", \"pick_district\":\"Quận 3\", \"pick_ward\":\"Phường 1\", \"pick_tel\":\"0911222333\", \"tel\":\"0911222333\", \"name\":\"GHTK - HCM - Noi Thanh\", \"address\":\"123 nguyễn chí thanh\", \"province\":\"TP. Hồ Chí Minh\", \"district\":\"Quận 1\", \"ward\":\"Phường Bến Nghé\", \"hamlet\":\"Khác\", \"is_freeship\":\"1\", \"pick_date\":\"2016-09-30\", \"pick_money\":47000, \"note\":\"Khối lượng tính cước tối đa: 1.00 kg\", \"value\":3000000, \"transport\":\"fly\", \"pick_option\":\"cod\", \"deliver_option\":\"xteam\", \"pick_session\":2 } }";
            //string ReqURL = "https://services.giaohangtietkiem.vn/services/shipment/order/?ver=1.5";
            //sendPost(ReqURL, requestData, "POST");
            ////  sau khi có kết quả thì lại gen ra dạng   var httpContent = new StringContent(json, Encoding.UTF8, "application/json"); để đọc kết quả nhé


            // string result = null;

            // string   requestData = "{ \"Id\":\"d7cd43ae-535a-40cf-8e7e-81484a0b874d\", \"OrderCode\":\"Nguyễn Dũng\", \"EmployeeId\":\"817e0714-71be-4210-9a4f-2e58859a425d\", \"CustomerId\":\"b272f4e9-3468-42fd-be0e-2c925a39117d\", \"OrderDate\":\"2021-07-24T10:35:30.2359476+07:00\", \"Status\":0, \"BillingAddress\":\"string\", \"ShippingAddress\":\"string\", \"OrderTotal\":0.0, \"f_Vat\":0.0, \"m_Vat\":0.0, \"OrderTotalDiscount\":0.0, \"f_Discount\":0.0, \"m_Discount\":0.0, \"m_TotalMoney\":0.0, \"Discription\":\"string\", \"OrderDetail\":[ { \"Id\":\"edd5c647-09ca-4e49-b2b0-53dcbbcc0173\", \"OrderId\":\"d7cd43ae-535a-40cf-8e7e-81484a0b874d\", \"ProductId\":\"9b12153a-c98d-4238-86bc-245d4f2604ac\", \"ProductName\":\"Bàn kệ tivi BK1111\", \"ProductCode\":\"pk\", \"Unit\":\"40000\", \"Price\":23300.0, \"Qty\":37.0, \"f_Discount\":0.0, \"m_Discount\":0.0, \"Description\":\"pk\", \"f_Convert\":0.0, \"StoreId\":\"11638865-b070-44a9-8f13-11fa05eb0ffc\" } ], \"Description\":\"Thư Demo\", \"CreatedBy\":\"DungABC\", \"ModifiedBy\":\"12\", \"CreatedDate\":\"2021-07-24T10:35:33.9389746+07:00\", \"ModifiedDate\":\"2021-07-24T10:35:34.2358653+07:00\", \"Location_Id\":\"CN01\" }";
            //// string requestData = "{ \"Id\":\"d7cd43ae-535a-40cf-8e7e-81484a0b874d\",\"OrderCode\":\"Nguyễn Dũng\",\"EmployeeId\":\"817e0714-71be-4210-9a4f-2e58859a425d\",\"CustomerId\":\"b272f4e9-3468-42fd-be0e-2c925a39117d\",\"OrderDate\":\"2021-07-24T10:35:30.2359476+07:00\",\"Status\":0,\"BillingAddress\":\"string\",\"ShippingAddress\":\"string\",\"OrderTotal\":0.0,\"f_Vat\":0.0,\"m_Vat\":0.0,\"OrderTotalDiscount\":0.0,\"f_Discount\":0.0,\"m_Discount\":0.0,\"m_TotalMoney\":0.0,\"Discription\":\"string\",\"OrderDetail\":[{\"Id\":\"edd5c647-09ca-4e49-b2b0-53dcbbcc0173\",\"OrderId\":\"d7cd43ae-535a-40cf-8e7e-81484a0b874d\",\"ProductId\":\"9b12153a-c98d-4238-86bc-245d4f2604ac\",\"ProductName\":\"Bàn kệ tivi BK1111\",\"ProductCode\":\"pk\",\"Unit\":\"40000\",\"Price\":23300.0,\"Qty\":37.0,\"f_Discount\":0.0,\"m_Discount\":0.0,\"Description\":\"pk\",\"f_Convert\":0.0,\"StoreId\":\"11638865-b070-44a9-8f13-11fa05eb0ffc\"}],\"Description\":\"Thư Demo\",\"CreatedBy\":\"DungABC\",\"ModifiedBy\":\"12\",\"CreatedDate\":\"2021-07-24T10:35:33.9389746+07:00\",\"ModifiedDate\":\"2021-07-24T10:35:34.2358653+07:00\",\"Location_Id\":\"CN01\"}";
            // string ReqURL = "http://api.phanmembanhang.com/api/Customer/AddNewPurchaseOrder";
            // result = sendPostPhanMem(ReqURL, requestData, "POST");
            // //  sau khi có kết quả thì lại gen ra dạng   var httpContent = new StringContent(json, Encoding.UTF8, "application/json"); để đọc kết quả nhé

            // string chuoi = result;

            //// Cách 2 TaoDonHang
            //var request = GiaoHangTietKiemWebFrom.TaoDonHangAPIBanHang();
            ////"{\"data\":[{\"Id\":\"6a315962-bf33-4149-ac43-388495f4d003\",\"Name\":\"HUYLM TEST 05\",\"ProductCode\":\"HUYLM_05\",\"Unit\":\"C\",\"UnitPrice\":0.0000,\"Price\":0.0000,\"InStock\":121.0,\"MinInStock\":0.0,\"MaxInStock\":0.0,\"BranchName\":\"ANGLE\",\"GroupName\":\"001 BUT \",\"GroupId\":null,\"Description\":\"\",\"s_Specification\":\"\",\"s_Quantification\":\"\",\"s_Color\":\"\",\"s_TacDungChinh\":\"\",\"s_NoteImport\":\"\",\"s_NoteOrder\":\"\",\"s_ThanhPhan\":\"\",\"ImageUrls\":[],\"ExtraLabels\":[],\"ExtraValues\":[],\"Picture\":\"\",\"Unit_ID\":null,\"f_Convert\":0.0},{\"Id\":\"db1173c8-973c-408c-bf7e-65d47252ab66\",\"Name\":\"HUYLM_THUNG\",\"ProductCode\":\"HUYLM_THUNG\",\"Unit\":\"THUNG\",\"UnitPrice\":68000.0000,\"Price\":68000.0000,\"InStock\":2.0,\"MinInStock\":0.0,\"MaxInStock\":0.0,\"BranchName\":\"ELECTROLUX\",\"GroupName\":\"DH MIDEA\",\"GroupId\":null,\"Description\":\"\",\"s_Specification\":\"\",\"s_Quantification\":\"\",\"s_Color\":\"\",\"s_TacDungChinh\":\"\",\"s_NoteImport\":\"\",\"s_NoteOrder\":\"\",\"s_ThanhPhan\":\"\",\"ImageUrls\":[],\"ExtraLabels\":[],\"ExtraValues\":[],\"Picture\":\"\",\"Unit_ID\":null,\"f_Convert\":0.0},{\"Id\":\"2eb8488e-1812-45e8-b32f-99a633cfa479\",\"Name\":\"Kem dưỡng gạo Nhật\",\"ProductCode\":\"TNL_0001\",\"Unit\":\"Hộp\",\"UnitPrice\":100000.0000,\"Price\":190000.0000,\"InStock\":100.0,\"MinInStock\":0.0,\"MaxInStock\":0.0,\"BranchName\":\"ANGLE\",\"GroupName\":\"DMP_TNL\",\"GroupId\":null,\"Description\":\"\",\"s_Specification\":\"\",\"s_Quantification\":\"\",\"s_Color\":\"\",\"s_TacDungChinh\":\"\",\"s_NoteImport\":\"\",\"s_NoteOrder\":\"\",\"s_ThanhPhan\":\"\",\"ImageUrls\":[],\"ExtraLabels\":[],\"ExtraValues\":[],\"Picture\":\"\",\"Unit_ID\":null,\"f_Convert\":0.0},{\"Id\":\"00213e42-24db-4161-a579-7625f65bbdfd\",\"Name\":\"kem ngăn ngừa rạn da hộp 125gr  palmers\",\"ProductCode\":\"017920                        \",\"Unit\":\"HOP\",\"UnitPrice\":332000.0000,\"Price\":332000.0000,\"InStock\":146.0,\"MinInStock\":0.0,\"MaxInStock\":0.0,\"BranchName\":\"Dược phẩm\",\"GroupName\":\"THUỐC\",\"GroupId\":null,\"Description\":\"\",\"s_Specification\":\"\",\"s_Quantification\":\"\",\"s_Color\":\"\",\"s_TacDungChinh\":\"\",\"s_NoteImport\":\"\",\"s_NoteOrder\":\"\",\"s_ThanhPhan\":\"\",\"ImageUrls\":[],\"ExtraLabels\":[],\"ExtraValues\":[],\"Picture\":\"\",\"Unit_ID\":null,\"f_Convert\":0.0},{\"Id\":\"6b2375da-046d-4e7a-ad11-02bf031cd9ed\",\"Name\":\"khăn giấy AB\",\"ProductCode\":\"test9999\",\"Unit\":\"gói\",\"UnitPrice\":5000.0000,\"Price\":10000.0000,\"InStock\":1.0,\"MinInStock\":0.0,\"MaxInStock\":0.0,\"BranchName\":\"ANGLE\",\"GroupName\":\"001 BUT \",\"GroupId\":null,\"Description\":\"\",\"s_Specification\":\"\",\"s_Quantification\":\"\",\"s_Color\":\"\",\"s_TacDungChinh\":\"\",\"s_NoteImport\":\"\",\"s_NoteOrder\":\"\",\"s_ThanhPhan\":\"\",\"ImageUrls\":[],\"ExtraLabels\":[],\"ExtraValues\":[],\"Picture\":\"\",\"Unit_ID\":null,\"f_Convert\":0.0},{\"Id\":\"b436b87c-e86c-47f0-a9e8-a948d3b29432\",\"Name\":\"L6025 60x60\",\"ProductCode\":\"L6025\",\"Unit\":\"Viên\",\"UnitPrice\":53000.0000,\"Price\":56000.0000,\"InStock\":19.0,\"MinInStock\":0.0,\"MaxInStock\":0.0,\"BranchName\":\"ANGLE\",\"GroupName\":\"001 BUT \",\"GroupId\":null,\"Description\":\"\",\"s_Specification\":\"60x60\",\"s_Quantification\":\"\",\"s_Color\":\"\",\"s_TacDungChinh\":\"\",\"s_NoteImport\":\"\",\"s_NoteOrder\":\"\",\"s_ThanhPhan\":\"\",\"ImageUrls\":[],\"ExtraLabels\":[],\"ExtraValues\":[],\"Picture\":\"\",\"Unit_ID\":null,\"f_Convert\":0.0},{\"Id\":\"f69ad6fc-8d74-4280-8c46-4ae5ae9dd6b3\",\"Name\":\"Lạc Đà Màu Đen\",\"ProductCode\":\"LACDA\",\"Unit\":\"cây\",\"UnitPrice\":0.0000,\"Price\":0.0000,\"InStock\":1.0,\"MinInStock\":0.0,\"MaxInStock\":0.0,\"BranchName\":\"ANGLE\",\"GroupName\":\"Thuốc lá\",\"GroupId\":null,\"Description\":\"\",\"s_Specification\":\"\",\"s_Quantification\":\"\",\"s_Color\":\"\",\"s_TacDungChinh\":\"\",\"s_NoteImport\":\"\",\"s_NoteOrder\":\"\",\"s_ThanhPhan\":\"\",\"ImageUrls\":[],\"ExtraLabels\":[],\"ExtraValues\":[],\"Picture\":\"\",\"Unit_ID\":null,\"f_Convert\":0.0},{\"Id\":\"ddea69fd-3377-40bb-9085-6c70d4c9f76c\",\"Name\":\"Lam Husq/lam 70/42 mắt H42\",\"ProductCode\":\"501 95 80-84\",\"Unit\":\"Cái\",\"UnitPrice\":0.0000,\"Price\":0.0000,\"InStock\":4.0,\"MinInStock\":0.0,\"MaxInStock\":0.0,\"BranchName\":\"ANGLE\",\"GroupName\":\"001 BUT \",\"GroupId\":null,\"Description\":\"\",\"s_Specification\":\"\",\"s_Quantification\":\"\",\"s_Color\":\"\",\"s_TacDungChinh\":\"\",\"s_NoteImport\":\"Hàng KM ko thu tiền\",\"s_NoteOrder\":\"\",\"s_ThanhPhan\":\"\",\"ImageUrls\":[],\"ExtraLabels\":[],\"ExtraValues\":[],\"Picture\":\"\",\"Unit_ID\":null,\"f_Convert\":0.0},{\"Id\":\"156998b7-7d55-45ca-9ee9-bb1ebd083517\",\"Name\":\"Lam máy cưa xích 25 inch\",\"ProductCode\":\"LAM25TIHL\",\"Unit\":\"Cái\",\"UnitPrice\":928000.0000,\"Price\":1450000.0000,\"InStock\":5.0,\"MinInStock\":0.0,\"MaxInStock\":0.0,\"BranchName\":\"Máy cắt\",\"GroupName\":\"Máy cắt cỏ\",\"GroupId\":null,\"Description\":\"\",\"s_Specification\":\"\",\"s_Quantification\":\"\",\"s_Color\":\"\",\"s_TacDungChinh\":\"\",\"s_NoteImport\":\"\",\"s_NoteOrder\":\"\",\"s_ThanhPhan\":\"\",\"ImageUrls\":[],\"ExtraLabels\":[],\"ExtraValues\":[],\"Picture\":\"\",\"Unit_ID\":null,\"f_Convert\":0.0},{\"Id\":\"42148cb5-a70c-4997-b267-0289d3bb8363\",\"Name\":\"Lam máy cưa xích 30 inch\",\"ProductCode\":\"LAM30STIHL\",\"Unit\":\"Cái\",\"UnitPrice\":1050000.0000,\"Price\":1550000.0000,\"InStock\":10.0,\"MinInStock\":0.0,\"MaxInStock\":0.0,\"BranchName\":\"Máy cắt\",\"GroupName\":\"Máy cắt cỏ\",\"GroupId\":null,\"Description\":\"\",\"s_Specification\":\"\",\"s_Quantification\":\"\",\"s_Color\":\"\",\"s_TacDungChinh\":\"\",\"s_NoteImport\":\"\",\"s_NoteOrder\":\"\",\"s_ThanhPhan\":\"\",\"ImageUrls\":[],\"ExtraLabels\":[],\"ExtraValues\":[],\"Picture\":\"\",\"Unit_ID\":null,\"f_Convert\":0.0}],\"paging\":{\"TotalPage\":22,\"PageIndex\":10,\"PageSize\":10,\"TotalCount\":214},\"meta\":{\"status_code\":0,\"message\":\"Successfully\"},\"Extra\":{\"TotalStock\":10908336}}"

            //var json = JsonConvert.SerializeObject(request);
            //var httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            //var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            //var result = client.PostAsync("/api/Customer/AddNewPurchaseOrder", httpContent).Result;
            //string strResponse = result.Content.ReadAsStringAsync().Result;
            //if (result.IsSuccessStatusCode)
            //{
            //    var obj = JsonConvert.DeserializeObject<DeleteProductModel.Root>(strResponse);
            //    // ViewBag.HienThi += "Thêm mới thành công : " + obj.data.Id + "--" + obj.data.Name + "--" + obj.data.ProductCode;
            //    //  ViewBag.HienThi += "<br><br><br><br>: " + obj.data;
            //    //  var data = result.Content.ReadAsAsync<List<RootThemMoi>>().Result;
            //}


            // Hủy đơn hàng
            // Mã đơn hàng của hệ thống GHTK
            //  GiaoHangTietKiemWebFrom.HuyDonHang("S18474582.SGA8.A25.733862956");

        }
    }
}