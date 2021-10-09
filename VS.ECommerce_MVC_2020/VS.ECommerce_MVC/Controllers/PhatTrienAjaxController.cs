using Newtonsoft.Json.Linq;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VS.ECommerce_MVC.Controllers
{
    public class PhatTrienAjaxController : Controller
    {
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
    }
}