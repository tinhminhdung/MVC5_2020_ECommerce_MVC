using MoreAll;
using OfficeOpenXml;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VS.ECommerce_MVC.Models;

namespace VS.ECommerce_MVC.Controllers
{
    public class PhatTrienDangKyController : Controller
    {
        DatalinqDataContext db = new DatalinqDataContext();

        [HttpPost]
        public string Checkmail(string mail)
        {
            string chuoi = "";
            if (mail != "")
            {
                var cat = db.Members.Where(m => m.Email == mail).ToList();
                if (cat.Count > 0)
                {
                    chuoi = "Địa chỉ mail này đã tồn tại!";
                }
            }
            return chuoi;
        }

        [HttpPost]
        public string CheckPhone(string phone)
        {
            string chuoi = "";
            if (phone != "")
            {
                var cat = db.Members.Where(m => m.DienThoai == phone).ToList();
                if (cat.Count > 0)
                {
                    chuoi = "Điện thoại này đã tồn tại!";
                }
            }
            return chuoi;
        }

        public ActionResult FromDangKyThanhVienRequired()
        {
            return View();
        }

        #region[Dang ki]
        [HttpPost]
        public ActionResult FromDangKyThanhVienRequired(FormCollection collect, IEnumerable<HttpPostedFileBase> fileImg)//, IEnumerable<HttpPostedFileBase> fileImg
        {
            try
            {
                string Name = collect["Name"];
                string Password = collect["Pass"];
                string Email = collect["Email"];
                string Tel = collect["Phone"];
                string Address = collect["Address"];
                int Provice = int.Parse(collect["TinhTP"].ToString());
                string Avarta = "";
                double nongthon = 0;
                bool Status = true;
                DateTime SDate = DateTime.Now;
                string images = collect["fileImg"];
                foreach (var file in fileImg)
                {
                    if (file.ContentLength > 0)
                    {
                        //var b = (from k in db.ProImages select k.Id).Max();
                        var ab = Request.Files["fileImg"];
                        String FileExtn = System.IO.Path.GetExtension(file.FileName).ToLower();
                        if (!(FileExtn == ".jpg" || FileExtn == ".png" || FileExtn == ".gif"))
                        {
                            ViewBag.error = "Chỉ cho phép các tệp jpg, gif và png!";
                        }
                        else
                        {
                            var Filename = Path.GetFileName(file.FileName);
                            //String imgPath = String.Format("Uploads/{0}{1}", file.FileName, FileExtn);
                            //file.Save(String.Format("{0}{1}", Server.MapPath("~"), imgPath), Img.RawFormat);
                            var path = Path.Combine(Server.MapPath(Url.Content("/Uploads")), Filename);
                            file.SaveAs(path);
                            Avarta = "/Uploads/" + Filename;
                        }
                    }
                    var fd = file;
                }
                nongthon = (collect["cNongthon"] == "false") ? 0 : 1;
                // data.sp_Customer_Insert(Name, Email, Password, Tel, Address, SDate, Status, "", Provice, 1, false, 1, Avarta, nongthon, 0, true);
                // data.SaveChanges();
                // return Redirect("/Pages/xacnhan");

                ViewBag.err = "OKKKK";
                return View();
            }
            catch
            {
                // ViewBag.TinhTP = new SelectList(Tinhtp(), "value", "text");
                ////ViewBag.Huyen = new SelectList(Quanhuyen(), "value", "text");
                ViewBag.err = "Lỗi nhập dữ liệu đăng ký thành viên!";
                return View();
            }
        }
        #endregion

        #region checklist
        //https://www.c-sharpcorner.com/UploadFile/sourabh_mishra1/checkboxlist-in-Asp-Net-mvc/
        public class StudentModel
        {
            public IList<SelectListItem> StudentNames { get; set; }
        }
        public ActionResult checklist()
        {

            // code vẫn chạy dc nhé


            //List<Entity.Menu> dt = SMenu.LOAD_CATESPARENT_ID(More.PR, "VIE", "-1", "1");
            //for (int i = 0; i < dt.Count; i++)
            //{
            //    if (dt[i].Parent_ID.ToString() == "-1")
            //    {
            //        var checkBoxId = "chk" + dt[i].ID;
            //        var tdId = "td" + dt[i].ID;
            //        ViewBag.CheckLit += "<tr> <td width=\"20px\"> <input type=\"checkbox\" id=\"" + checkBoxId + "\" class=\"chkclass\" value=\"" + dt[i].ID + "\"> </td> <td id=\"" + tdId + "\" width=\"100px\"> " + dt[i].Name + " </td> </tr>";
            //    }
            //}

            //string Javascrips = "";
            //#region Tag
            //string[] strArray = table.IDDanhMucNhomSanPham.ToString().Split(new char[] { ',' });
            //for (int k = 0; k < strArray.Length; k++)
            //{
            //    Javascrips += "<script>  document.getElementById(\"chk" + strArray[k].ToString() + "\").checked = true;</script>";
            //}
            //#endregion
            //ViewBag.Javascrip = Javascrips;
            // }
            return View();
        }
        [HttpPost]
        public ActionResult Studentl(string[] Name)
        {
            //string chuoi = "0";
            //foreach (var item in Name)
            //{
            //    chuoi += "," + item;
            //}
            //SQuanLyGiaoVien.Name_Text("update QuanLyGiaoVien set IDDanhMucNhomSanPham = '" + chuoi + "' where ID =" + MoreAll.MoreAll.GetCookies("TeacherID").ToString() + "");
            //F:\Congty_APhuc\Nam2021\Thang08\VS.CongDong_MVC_DaiHocDongDO\VS.E_Commerce\Controllers/tech giáo viên
            return Json(new { success = Name });
        }
        #endregion

        public ActionResult RankDanhGiaSao()
        {
            ViewBag.IDThanhViens = "46";
            ViewBag.IDGiaoVien = "1";
            ViewBag.hdipid = "14";

            if (MoreAll.MoreAll.GetCookies("MembersID") != "")
            {
                //  Response.Write("select * from DanhGiaSao where IDThanhVien=" + MoreAll.MoreAll.GetCookies("MembersID") + "  and  IDGiaoVien=" + ViewBag.IDGiaoVien + "  and  IDSanPham=" + ViewBag.hdipid + " ");
                List<Entity.DanhGiaSao> iitem = SDanhGiaSao.Name_Text(@"select * from DanhGiaSao where IDThanhVien=" + MoreAll.MoreAll.GetCookies("MembersID") + "  and  IDGiaoVien=" + ViewBag.IDGiaoVien + "  and  IDSanPham=" + ViewBag.hdipid + " ");
                if (iitem.Count() > 0)
                {
                    ViewBag.Annn = "display:none";
                }
            }
            int Rank1 = 0;
            int Rank2 = 0;
            int Rank3 = 0;
            int Rank4 = 0;
            int Rank5 = 0;
            List<Entity.DanhGiaSao> iitem2 = SDanhGiaSao.Name_Text(@"select * from DanhGiaSao ");//where   and  IDSanPham=" + ViewBag.hdipid + " 
            if (iitem2.Count() > 0)
            {
                string tong = iitem2.Count().ToString();
                ViewBag.Annn = "display:none";
                ViewBag.VDanhGiaSao = iitem2;
                ViewBag.CountDanhGiaSao = tong;

                foreach (var item in iitem2)
                {
                    Rank1 += Convert.ToInt32(item.MotSao);
                    Rank2 += Convert.ToInt32(item.HaiSao);
                    Rank3 += Convert.ToInt32(item.BaSao);
                    Rank4 += Convert.ToInt32(item.BonSao);
                    Rank5 += Convert.ToInt32(item.NamSao);
                }

                try
                {
                    ViewBag.Rank11 = Commond.ShowSumRank(Rank1.ToString(), tong);
                    ViewBag.Rank12 = Commond.ShowSumRank(Rank2.ToString(), tong);
                    ViewBag.Rank13 = Commond.ShowSumRank(Rank3.ToString(), tong);
                    ViewBag.Rank14 = Commond.ShowSumRank(Rank4.ToString(), tong);
                    ViewBag.Rank15 = Commond.ShowSumRank(Rank5.ToString(), tong);
                }
                catch (Exception)
                {
                    ViewBag.Rank11 = "0";
                    ViewBag.Rank12 = "0";
                    ViewBag.Rank13 = "0";
                    ViewBag.Rank14 = "0";
                    ViewBag.Rank15 = "0";
                    ViewBag.CountDanhGiaSao = "0";
                }
            }
            else
            {

                ViewBag.Rank11 = "0";
                ViewBag.Rank12 = "0";
                ViewBag.Rank13 = "0";
                ViewBag.Rank14 = "0";
                ViewBag.Rank15 = "0";
                ViewBag.CountDanhGiaSao = "0";
            }

            return View();
        }


    }
}