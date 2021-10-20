using MoreAll;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace VS.ECommerce_MVC.Controllers
{
    public class PhatTrienAjaxController : Controller
    {
        DatalinqDataContext db = new DatalinqDataContext();

        public ActionResult DemoJson()
        {
            //Khóa học trên tedu https://www.youtube.com/watch?v=vALwIgRTFP8 


            //1:  Object db to string json
            List<Entity.Contacts> list = SContacts.Name_Text("select * from Contacts  order by dcreatedate desc");
            var Json = JsonConvert.SerializeObject(list);// Chuyển đổi từ db lên json cho Ajax dùng
            ViewBag.Hienthi = Json;


            // 2: Json to Object
            // Chuyển đổi từ json trên Ajax về để thành dạng Obj Của C#
            string input = "[{\"ino\":331,\"vtitle\":\"1\",\"vname\":\"3\",\"vaddress\":\"4\",\"vphone\":\"5\",\"vemail\":\"5\",\"vcontent\":\"PostMuitilRequest\",\"dcreatedate\":\"2021-10-09T09:23:17.39\",\"lang\":\"VIE \",\"istatus\":0},{\"ino\":329,\"vtitle\":\"A\",\"vname\":\"Nguyễn\",\"vaddress\":\"11\",\"vphone\":\"0987654322\",\"vemail\":\"435345\",\"vcontent\":\"PostMuitilRequest\",\"dcreatedate\":\"2021-10-09T09:23:17.387\",\"lang\":\"VIE \",\"istatus\":0},{\"ino\":330,\"vtitle\":\"333\",\"vname\":\"Dthaung\",\"vaddress\":\"33\",\"vphone\":\"098765454\",\"vemail\":\"324\",\"vcontent\":\"PostMuitilRequest\",\"dcreatedate\":\"2021-10-09T09:23:17.387\",\"lang\":\"VIE \",\"istatus\":0}]";
            List<Entity.Contacts> obj = new JavaScriptSerializer().Deserialize<List<Entity.Contacts>>(input);
            // Hoặc
            // List<Entity.Contacts> obj = new JavaScriptSerializer().Deserialize<List<Entity.Contacts>>(Json);
            string chuoi = "";
            foreach (var item in obj)
            {
                chuoi += item.vname + "<br>";
            }
            ViewBag.Hienthi2 = chuoi;



            //3: Object to string json
            // Chuyển đổi từ json  lên json cho Ajax dùng
            ViewBag.Hienthi3 = JsonConvert.SerializeObject(obj);

            return View();
        }

        #region AjaxJson 1
        public ActionResult PostAjaxJson()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PostAjaxJson(Entity.Contacts request)
        {
            var result = "Lỗi";
            Entity.Contacts obj = new Entity.Contacts();
            obj.vtitle = "";
            obj.vname = request.vname;
            obj.vaddress = request.vaddress;
            obj.vphone = request.vphone;
            obj.vemail = "";
            obj.vcontent = "PostAjaxJson";
            obj.dcreatedate = DateTime.Now;
            obj.lang = "VIE";
            obj.istatus = 0;

            if (SContacts.INSERT(obj) == true)
            {
                result = "Liên hệ thành công";
            }

            return Json(new
            {
                msg = result
            });
        }

        public ActionResult CreateMulti()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostMuitilRequest(ObjMuitiRequest obj)
        {
            var msg = "";
            // có thể kết hợp cùng 1 lúc nhiều bảng được nhé, ví dụ thêm liên hệ, thêm menu , thêm nhiều bảng db cùng 1 sự kiện
            try
            {
                //var itemsLeaderTime = JArray.Parse(obj.SourceLeaderTime);
                var itemsLienHe = JArray.Parse(obj.SourceLienHe);
                if (itemsLienHe.Count > 0)
                {
                    // Xóa các bảng cũ trong slq trước khi thêm mới nếu cần nhé
                    //var result2 = DataAccess.DataAccess.SP_TRADTERM_DELETE_UPDATE(
                    //      itemsContract[0]["ContractNo"].ToString()
                    //);

                    // có thể thêm nhiều bảng khác nhau cùng 1 lúc 
                    //if (itemsLeaderTime.Count() > 0)
                    //{
                    //    foreach (var itemLeaderTime in itemsLeaderTime)
                    //    {
                    //        DataAccess.DataAccess.SP_TRADTERM_INSERT_LEADTIME(
                    //             itemsContract[0]["ContractNo"].ToString()
                    //           , itemsContract[0]["VendorNo"].ToString()
                    //           , itemLeaderTime["Leadtimesx"].ToString()
                    //           , itemLeaderTime["ThoiGianKyHD"].ToString()
                    //           , itemLeaderTime["ThanhToan"].ToString()
                    //           , itemLeaderTime["DongCongVaHQXuat"].ToString()
                    //           , itemLeaderTime["DiBien"].ToString()
                    //           , itemLeaderTime["ThuTucHQDauNhap"].ToString()
                    //           , itemLeaderTime["TongLeadtime"].ToString()
                    //           , Session["uid"].ToString()
                    //           , itemLeaderTime["typeTab"].ToString()
                    //        );
                    //    }
                    //}
                    if (itemsLienHe.Count() > 0)
                    {
                        foreach (var item in itemsLienHe)
                        {
                            Entity.Contacts obk = new Entity.Contacts();
                            obk.vtitle = item["vtitle"].ToString();
                            obk.vname = item["vname"].ToString();
                            obk.vaddress = item["vaddress"].ToString();
                            obk.vphone = item["vphone"].ToString();
                            obk.vemail = item["vemail"].ToString();
                            obk.vcontent = "PostMuitilRequest";
                            obk.dcreatedate = DateTime.Now;
                            obk.lang = "VIE";
                            obk.istatus = 0;
                            if (SContacts.INSERT(obk) == true)
                            {
                                msg = "Liên hệ thành công";
                            }
                        }
                    }

                }
                return Json(msg);
            }
            catch (Exception ex)
            {
                msg = "Có lỗi. Vui lòng kiểm tra lại" + ex;
            }
            return Json(msg);
        }

        public class ObjMuitiRequest
        {
            //// Cần phải khai báo thêm nếu cần thêm nhiều bảng nhé
            public string SourceLienHe { get; set; }
            public string SourceLeadTime { get; set; }
        }

        public ActionResult EditMulti()
        {
            List<Entity.Contacts> list_LienHe = SContacts.Name_Text("select * from Contacts  order by dcreatedate desc");
            ViewBag.list_LienHe = list_LienHe;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateMuitilRequest(ObjMuitiRequest obj)
        {
            var msg = "";
            // có thể kết hợp cùng 1 lúc nhiều bảng được nhé, ví dụ thêm liên hệ, thêm menu , thêm nhiều bảng db cùng 1 sự kiện
            try
            {
                //var itemsLeaderTime = JArray.Parse(obj.SourceLeaderTime);
                var itemsLienHe = JArray.Parse(obj.SourceLienHe);
                if (itemsLienHe.Count > 0)
                {
                    // Xóa các bảng cũ trong slq trước khi thêm mới nếu cần nhé
                    //var result2 = DataAccess.DataAccess.SP_TRADTERM_DELETE_UPDATE(
                    //      itemsContract[0]["ContractNo"].ToString()
                    //);



                    // có thể thêm nhiều bảng khác nhau cùng 1 lúc 
                    //if (itemsLeaderTime.Count() > 0)
                    //{
                    //    foreach (var itemLeaderTime in itemsLeaderTime)
                    //    {
                    //        DataAccess.DataAccess.SP_TRADTERM_INSERT_LEADTIME(
                    //             itemsContract[0]["ContractNo"].ToString()
                    //           , itemsContract[0]["VendorNo"].ToString()
                    //           , itemLeaderTime["Leadtimesx"].ToString()
                    //           , itemLeaderTime["ThoiGianKyHD"].ToString()
                    //           , itemLeaderTime["ThanhToan"].ToString()
                    //           , itemLeaderTime["DongCongVaHQXuat"].ToString()
                    //           , itemLeaderTime["DiBien"].ToString()
                    //           , itemLeaderTime["ThuTucHQDauNhap"].ToString()
                    //           , itemLeaderTime["TongLeadtime"].ToString()
                    //           , Session["uid"].ToString()
                    //           , itemLeaderTime["typeTab"].ToString()
                    //        );
                    //    }
                    //}
                    if (itemsLienHe.Count() > 0)
                    {

                        // xóa bản ghi cũ trước khi cập nhật

                        SContacts.Name_Text("delete from Contacts");
                        foreach (var item in itemsLienHe)
                        {
                            Entity.Contacts obk = new Entity.Contacts();
                            obk.vtitle = item["vtitle"].ToString();
                            obk.vname = item["vname"].ToString();
                            obk.vaddress = item["vaddress"].ToString();
                            obk.vphone = item["vphone"].ToString();
                            obk.vemail = item["vemail"].ToString();
                            obk.vcontent = "PostMuitilRequest";
                            obk.dcreatedate = DateTime.Now;
                            obk.lang = "VIE";
                            obk.istatus = 0;
                            if (SContacts.INSERT(obk) == true)
                            {
                                msg = "Liên hệ thành công";
                            }
                        }
                    }

                }
                return Json(msg);
            }
            catch (Exception ex)
            {
                msg = "Có lỗi. Vui lòng kiểm tra lại" + ex;
            }
            return Json(msg);
        }

        #endregion

        #region Multi_News

        public ActionResult CreateMulti_News()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostMuitilRequest_News(ObjMuitiNewsRequest request)
        {
            var msg = "";
            // có thể kết hợp cùng 1 lúc nhiều bảng được nhé, ví dụ thêm liên hệ, thêm menu , thêm nhiều bảng db cùng 1 sự kiện
            try
            {
                //var itemsLeaderTime = JArray.Parse(obj.SourceLeaderTime);
                var tr = JArray.Parse(request.SourceNew);// convert kiểu json sang Objeck
                if (tr.Count > 0)
                {
                    // Xóa các bảng cũ trong slq trước khi thêm mới nếu cần nhé
                    //var result2 = DataAccess.DataAccess.SP_TRADTERM_DELETE_UPDATE(
                    //      itemsContract[0]["ContractNo"].ToString()
                    //);

                    // có thể thêm nhiều bảng khác nhau cùng 1 lúc 
                    //if (itemsLeaderTime.Count() > 0)
                    //{
                    //    foreach (var itemLeaderTime in itemsLeaderTime)
                    //    {
                    //        DataAccess.DataAccess.SP_TRADTERM_INSERT_LEADTIME(
                    //             itemsContract[0]["ContractNo"].ToString()
                    //           , itemsContract[0]["VendorNo"].ToString()
                    //           , itemLeaderTime["Leadtimesx"].ToString()
                    //           , itemLeaderTime["ThoiGianKyHD"].ToString()
                    //           , itemLeaderTime["ThanhToan"].ToString()
                    //           , itemLeaderTime["DongCongVaHQXuat"].ToString()
                    //           , itemLeaderTime["DiBien"].ToString()
                    //           , itemLeaderTime["ThuTucHQDauNhap"].ToString()
                    //           , itemLeaderTime["TongLeadtime"].ToString()
                    //           , Session["uid"].ToString()
                    //           , itemLeaderTime["typeTab"].ToString()
                    //        );
                    //    }
                    //}
                    if (tr.Count() > 0)
                    {
                        foreach (var item in tr)
                        {

                            //"[{\"IDNhom\":\"794\",\"IDNhomCung\":\"Hàng mới\",\"IDNhomInput\":[\"794\",\"Nội thất phòng ngủ\"],\"IDNhomCungInput\":\"Khai trương\",\"Name\":\"nn\",\"Images\":\"ii\",\"NgayTao\":\"2021-10-16\",\"KieuSo\":\"11\",\"Status\":0}]"
                            TestMuiltiInPut obj = new TestMuiltiInPut();
                            obj.IDNhom = int.Parse(item["IDNhom"].ToString());
                            obj.IDNhomCung = int.Parse(item["IDNhomCung"].ToString());
                            obj.IDNhomInput = item["IDNhomInput"].ToString();
                            obj.IDNhomCungInput = item["IDNhomCungInput"].ToString();
                            obj.Status = int.Parse(item["Status"].ToString());
                            obj.Name = item["Name"].ToString();
                            obj.Images = item["Images"].ToString();
                            obj.NgayTao = Convert.ToDateTime(item["NgayTao"].ToString());
                            obj.KieuSo = int.Parse(item["KieuSo"].ToString());
                            db.TestMuiltiInPuts.InsertOnSubmit(obj);
                            db.SubmitChanges();
                            if (obj.ID > 0)
                            {
                                msg = "Thành công !!!!!!!!!.";
                            }
                        }
                    }

                }
                return Json(msg);
            }
            catch (Exception ex)
            {
                msg = "Có lỗi. Vui lòng kiểm tra lại" + ex;
            }
            return Json(msg);
        }

        public class ObjMuitiNewsRequest
        {
            //// Cần phải khai báo thêm nếu cần thêm nhiều bảng nhé
            public string SourceNew { get; set; }
            //  public string SourceLeadTime { get; set; }
        }

        [HttpPost]
        public ActionResult GetListMenu()
        {
            try
            {
                List<Entity.Menu> bn = SMenu.LOAD_CATESPARENT_ID(More.PR, "VIE", "-1", "1");
                if (bn.Count > 0)
                {
                    return Json(JsonConvert.SerializeObject(bn));
                }
                else
                {
                    return Json(null);
                }
            }
            catch (Exception ex)
            {
                LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "GetListMenu");
                return Json(null);
            }
        }


        public ActionResult EditMulti_News()
        {
            List<TestMuiltiInPut> ListNews = db.ExecuteQuery<TestMuiltiInPut>(@"SELECT * FROM TestMuiltiInPut order by ID asc").ToList();
            ViewBag.ListNews = ListNews;
            return View();
        }
        [HttpPost]
        public ActionResult UpdateMuitilRequest_News(ObjMuitiNewsRequest request)
        {
            var msg = "";
            // có thể kết hợp cùng 1 lúc nhiều bảng được nhé, ví dụ thêm liên hệ, thêm menu , thêm nhiều bảng db cùng 1 sự kiện
            try
            {
                //var itemsLeaderTime = JArray.Parse(obj.SourceLeaderTime);
                var tr = JArray.Parse(request.SourceNew);// convert kiểu json sang Objeck
                if (tr.Count > 0)
                {
                    // Xóa các bảng cũ trong slq trước khi thêm mới nếu cần nhé
                    //var result2 = DataAccess.DataAccess.SP_TRADTERM_DELETE_UPDATE(
                    //      itemsContract[0]["ContractNo"].ToString()
                    //);

                    // có thể thêm nhiều bảng khác nhau cùng 1 lúc 
                    //if (itemsLeaderTime.Count() > 0)
                    //{
                    //    foreach (var itemLeaderTime in itemsLeaderTime)
                    //    {
                    //        DataAccess.DataAccess.SP_TRADTERM_INSERT_LEADTIME(
                    //             itemsContract[0]["ContractNo"].ToString()
                    //           , itemsContract[0]["VendorNo"].ToString()
                    //           , itemLeaderTime["Leadtimesx"].ToString()
                    //           , itemLeaderTime["ThoiGianKyHD"].ToString()
                    //           , itemLeaderTime["ThanhToan"].ToString()
                    //           , itemLeaderTime["DongCongVaHQXuat"].ToString()
                    //           , itemLeaderTime["DiBien"].ToString()
                    //           , itemLeaderTime["ThuTucHQDauNhap"].ToString()
                    //           , itemLeaderTime["TongLeadtime"].ToString()
                    //           , Session["uid"].ToString()
                    //           , itemLeaderTime["typeTab"].ToString()
                    //        );
                    //    }
                    //}
                    if (tr.Count() > 0)
                    {
                        SContacts.Name_Text("delete from TestMuiltiInPut");
                        foreach (var item in tr)
                        {
                            //"[{\"IDNhom\":\"794\",\"IDNhomCung\":\"Hàng mới\",\"IDNhomInput\":[\"794\",\"Nội thất phòng ngủ\"],\"IDNhomCungInput\":\"Khai trương\",\"Name\":\"nn\",\"Images\":\"ii\",\"NgayTao\":\"2021-10-16\",\"KieuSo\":\"11\",\"Status\":0}]"
                            TestMuiltiInPut obj = new TestMuiltiInPut();
                            obj.IDNhom = int.Parse(item["IDNhom"].ToString());
                            obj.IDNhomCung = int.Parse(item["IDNhomCung"].ToString());
                            obj.IDNhomInput = item["IDNhomInput"].ToString();
                            obj.IDNhomCungInput = item["IDNhomCungInput"].ToString();
                            obj.Status = int.Parse(item["Status"].ToString());
                            obj.Name = item["Name"].ToString();
                            obj.Images = item["Images"].ToString();
                            obj.NgayTao = Convert.ToDateTime(item["NgayTao"].ToString());
                            obj.KieuSo = int.Parse(item["KieuSo"].ToString());
                            db.TestMuiltiInPuts.InsertOnSubmit(obj);
                            db.SubmitChanges();
                            if (obj.ID > 0)
                            {
                                msg = "Thành công !!!!!!!!!.";
                            }
                        }
                    }
                }
                return Json(msg);
            }
            catch (Exception ex)
            {
                msg = "Có lỗi. Vui lòng kiểm tra lại" + ex;
            }
            return Json(msg);
        }


        //CREATE TABLE [dbo].[TestMuiltiInPut](
        //    [ID] [int] IDENTITY(1,1) NOT NULL,
        //    [IDNhom] [int] NULL,
        //    [IDNhomCung] [int] NULL,
        //    [IDNhomInput] [nvarchar](500) NULL,
        //    [IDNhomCungInput] [nvarchar](500) NULL,
        //    [Status] [int] NULL,
        //    [Name] [ntext] NULL,
        //    [Images] [ntext] NULL,
        //    [NgayTao] [datetime] NULL,
        //    [KieuSo] [int] NULL,
        // CONSTRAINT [PK_TestMuiltiInPut] PRIMARY KEY CLUSTERED 
        //(
        //    [ID] ASC
        //)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        //) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

        //GO 
        #endregion

        #region Tỉnh thành
        public ActionResult TinhThanh()
        {
            return View();
        }
     
        /// Hàm lấy danh sách Country
        public JsonResult GetAllCountries()
        {
            List<Tinhthanh> data = db.ExecuteQuery<Tinhthanh>(@"SELECT * FROM Tinhthanh where Parent_ID=-1 and capp='TT' order by Create_Date desc").ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// Hàm lấy danh sách tỉnh thành theo CountryId. 
        /// Id = 237 là của Việt Nam. Do database mình quy định vậy
        /// <param name="id">Id của country</param>
        public JsonResult GetAllProvinceByCountryId(int? id = 237)
        {
            var data = db.Tinhthanhs.Where(x => x.Parent_ID == id).OrderBy(x => x.Name).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// Hàm lấy tất cả danh sách quận huyện
        /// Id = 1 là Hà Nội, do database mình quy định vậy
        /// <param name="id">Id = ProvinceId</param>
        
        public JsonResult GetAllDistrictByProvinceId(int? id = 1)
        {
            var data = db.Tinhthanhs.Where(x => x.Parent_ID == id).OrderBy(x => x.Name).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// Hàm lấy danh sách xã phường theo quận huyện
        /// Id= 1 là Ba Đình. Do database quy định
        /// <param name="id"></param>
        public JsonResult GetAllWardByDistrictId(int? id = 1)
        {
            var data = db.Tinhthanhs.Where(x => x.Parent_ID == id).OrderBy(x => x.Name).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}