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
using System.IO;
using Services;
using MoreAll;
using System.Text;
using System.Drawing;

namespace VS.ECommerce_MVC.Controllers
{
    public class PhatTrienNangCapController : Controller
    {
        DatalinqDataContext db = new DatalinqDataContext();
        public ActionResult Create_Drop()
        {
            // List<Entity.Menu> dt = SMenu.LOAD_CATESPARENT_ID(More.PR, "VIE", "-1", "1");
            List<Entity.Menu> dt = SMenu.Name_Text("SELECT * FROM [Menu]  where capp='PR'    and Status=1 order by level,Orders asc");
            for (int i = 0; i < dt.Count; i++)
            {
                ViewBag.ddlnhomsp = new SelectList(dt, "Id", "Name");
            }
            // kiểu 2
            ViewBag.ShowDropdowlist = LoadDropdowlist("");// show lên html Dropdowlist

            // Kiểu 3
            ViewBag.ShowDanhMuc = ShowDanhMuc("");// show lên html Dropdowlist

            ViewBag.ShowDanhMucNews = ShowDanhMucNews("");// show lên html Dropdowlist

            return View();
        }

        [HttpPost]
        public ActionResult Create_Drop(string IDNhom)
        {
            return View();
        }

        public ActionResult Edit_Drop(int ID = 794)
        {
            List<Entity.Menu> dt = SMenu.Name_Text("SELECT * FROM [Menu]  where capp='PR'    and Status=1 order by level,Orders asc");
            for (int i = 0; i < dt.Count; i++)
            {
                ViewBag.ddlnhomsp = new SelectList(dt, "Id", "Name", ID);//id là để  active trạng thái

                //var Edit = db.Products.First(m => m.Id == id);
                //var cat = db.GroupProducts.Where(n => n.Active == true).ToList();
                //ViewBag.Cat = new SelectList(cat, "Id", "Name", Edit.IdCategory);// active trạng thái
            }

            // kiểu 2
            ViewBag.ShowDropdowlist = LoadDropdowlist(ID.ToString());// show lên html Dropdowlist

            // Kiểu 3

            ViewBag.ShowDanhMuc = ShowDanhMuc(ID.ToString());// show lên html Dropdowlist
            ViewBag.ShowDanhMucNews = ShowDanhMucNews(ID.ToString());// show lên html Dropdowlist

            return View();
        }

        #region Dropdowlist đa cấp kiểu 2
        protected string LoadDropdowlist(string IDActive)
        {
            string Chuoi = "";
            string str = "";

            str += "<select id=\"ddlcategory\" name=\"ddlcategory\">";
            str += "<option value=\"0\">== Chọn chuyên mục ==</option>";
            List<Entity.Menu> dt = SMenu.LOAD_CATESPARENT_ID(More.PR, "VIE", "-1", "1");
            for (int i = 0; i < dt.Count; i++)
            {
                if (dt[i].Parent_ID.ToString() == "-1")
                {
                    Chuoi = "";
                    for (int j = 1; j < dt[i].Level.Length / 5; j++)
                    {
                        Chuoi = Chuoi + "---";
                    }
                    if (dt[i].ID.ToString() == IDActive)
                        str += "<option  selected=\"selected\"  value=\"" + dt[i].ID.ToString() + "\">" + Chuoi + dt[i].Name.ToString() + "</option>";
                    else
                        str += "<option value=\"" + dt[i].ID.ToString() + "\">" + Chuoi + dt[i].Name.ToString() + "</option>";
                    str += Categories(dt[i].ID.ToString(), IDActive);
                }
            }
            str += "</select>";
            return str;
        }
        protected string Categories(string id, string IDActive)
        {
            string Chuoi = "";
            string str = "";
            List<SelectListItem> cateList = new List<SelectListItem>();
            List<Entity.Menu> dt = SMenu.LOAD_CATESPARENT_ID(More.PR, "VIE", id, "1");
            for (int i = 0; i < dt.Count; i++)
            {
                if (dt[i].Parent_ID.ToString() == id)
                {
                    Chuoi = "";
                    for (int j = 1; j < dt[i].Level.Length / 5; j++)
                    {
                        Chuoi = Chuoi + "---";
                    }
                    if (dt[i].ID.ToString() == IDActive)
                        str += "<option  selected=\"selected\" value=\"" + dt[i].ID.ToString() + "\">" + Chuoi + dt[i].Name.ToString() + "</option>";
                    else
                        str += "<option value=\"" + dt[i].ID.ToString() + "\">" + Chuoi + dt[i].Name.ToString() + "</option>";
                    str += Categories(dt[i].ID.ToString(), IDActive);
                }
            }
            return str.ToString();
        }
        #endregion

        #region Dropdowlist đa cấp kiểu 3
        protected string ShowDanhMuc(string IDActive)
        {
            string Chuoi = "";
            string str = "";
            List<Entity.Menu> list = SMenu.Name_Text("SELECT * FROM Menu WHERE capp='PR'  order by level,Orders asc");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Chuoi = "";
                    for (int j = 1; j < list[i].Level.Length / 5; j++)
                    {
                        Chuoi = Chuoi + " ---";
                    }
                    if (list[i].ID.ToString() == IDActive)
                        str += "<option  selected=\"selected\" value=\"" + list[i].ID.ToString() + "\">" + Chuoi + list[i].Name.ToString() + "</option>";
                    else
                        str += "<option value=\"" + list[i].ID.ToString() + "\">" + Chuoi + list[i].Name.ToString() + "</option>";
                }
            }
            list.Clear();
            return str;
        }

        protected string ShowDanhMucNews(string IDActive)
        {
            string Chuoi = "";
            string str = "";
            List<Entity.Menu> list = SMenu.Name_Text("SELECT * FROM Menu WHERE capp='PR'  order by level,Orders asc");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Chuoi = "";
                    for (int j = 1; j < list[i].Level.Length / 5; j++)
                    {
                        Chuoi = Chuoi + " ---";
                    }
                    if (list[i].ID.ToString() == IDActive)
                        str += "<option  selected=\"selected\" data-value=\"" + list[i].ID.ToString() + "\">" + Chuoi + list[i].Name.ToString() + "</option>";
                    else
                        str += "<option data-value=\"" + list[i].ID.ToString() + "\">" + Chuoi + list[i].Name.ToString() + "</option>";
                }
            }
            list.Clear();
            return str;
        }

        #endregion

        public ActionResult UploadFileMau1()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UploadFileMau1(FormCollection collect)
        {
            #region MyRegion
            string UpFileCMT = "";
            HttpPostedFileBase postedFileCMT = Request.Files["FileAttachment1"];
            string path1 = Server.MapPath("~/Uploads/Test/");
            if (!Directory.Exists(path1))
            {
                Directory.CreateDirectory(path1);
            }
            int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB
            if (postedFileCMT.ContentLength > MaxContentLength)
            {
                TempData["Message"] = " File Chứng minh thư không vượt quá 100 MB";
                return View();
            }
            string str65 = "";
            if (postedFileCMT.FileName.Length > 0)
            {
                str65 = Path.GetExtension(postedFileCMT.FileName).ToLower();
                UpFileCMT = DateTime.Now.Ticks.ToString() + 2 + str65;
                postedFileCMT.SaveAs(path1 + UpFileCMT);
            }
            #endregion

            #region UpFileHocBa
            string UpFileHocBa = "";
            HttpPostedFileBase postedFile = Request.Files["FileAttachment2"];
            string path = Server.MapPath("~/Uploads/Test/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            int MaxContentLength1 = 1024 * 1024 * 10; //Size = 10 MB
            if (postedFile.ContentLength > MaxContentLength1)
            {
                TempData["Message"] = " File học bạ không vượt quá 100 MB";
                return View();
            }
            string str66 = "";
            if (postedFile.FileName.Length > 0)
            {
                str66 = Path.GetExtension(postedFile.FileName).ToLower();
                UpFileHocBa = DateTime.Now.Ticks.ToString() + 1 + str66;
                postedFile.SaveAs(path + UpFileHocBa);
            }
            #endregion


            string FileCMCD = "";
            if (UpFileCMT != "")
            {
                FileCMCD = "/Uploads/Test/" + UpFileCMT;
            }
            string FileHocBa = "";
            if (UpFileHocBa != "")
            {
                FileHocBa = "/Uploads/Test/" + UpFileHocBa;
            }

            ViewBag.hienthi += Request.Files["FileAttachment2"] + "====" + Request.Files["FileAttachment1"] + "====" + FileCMCD + "====" + FileHocBa;
            ViewBag.hienthi += "<br />" + postedFile.FileName + "====" + postedFileCMT.FileName;


            return View();
        }
        public ActionResult FileUploading()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload()
        {
            // Learn to use the entire functionality of the dxFileUploader widget.
            // http://js.devexpress.com/Documentation/Guide/UI_Widgets/UI_Widgets_-_Deep_Dive/dxFileUploader/

            var myFile = Request.Files["myFile"];
            var targetLocation = Server.MapPath("~/Uploads/Test/");
            try
            {
                var path = Path.Combine(targetLocation, myFile.FileName);
                //Uncomment to save the file
                //myFile.SaveAs(path);
            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }


        //PhatTrienNangCap/ExportExel

        public void ExportExel()
        {
            string Namefile = "Danhsachdondathang";
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename=" + Namefile + ".xls");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            sb.Append("<table border='1' bgcolor='#ffffff' bordercolor='#dedede' cellspacing='0' cellpadding='0' style='font-size:12px; font-family:Arial; background:white;'>");
            sb.Append("<tr>");
            sb.Append("  <th style=\"width:50px; vertical-align:middle; height: 22px;\">");
            sb.Append("    <b>STT</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Họ và tên</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:400px; vertical-align:middle;\">");
            sb.Append("    <b>Địa chỉ</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Điện thoại</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Email</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:520px; vertical-align:middle;\">");
            sb.Append("    <b>Ghi chú</b>");
            sb.Append("  </th>");
            sb.Append("  </tr>");

            sb.Append("<tr style=\"height: 70px;\">");
            sb.Append("  <td style=\"width:50px; vertical-align:middle; height: 22px;\">");
            sb.Append("    <b>STT</b>");
            sb.Append("  </td>");
            sb.Append("  <td style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Họ và tên</b>");
            sb.Append("  </td>");
            sb.Append("  <td style=\"width:150px; vertical-align:middle;\">");
            sb.Append("<img height=\"32\" src=\"" + ("http://localhost:63137/uploads/Files/636358866065201657.jpg") + "\" v:shapes=\"Picture_x0020_1\" width=\"32\" />");
            sb.Append("  </td>");
            sb.Append("  <td style=\"width:150px; vertical-align:middle;\">");
            sb.Append("<img height=\"32\" src=\"\" v:shapes=\"Picture_x0020_1\" width=\"32\" />");
            sb.Append("  </td>");
            sb.Append("  <td style=\"width:150px; vertical-align:middle;\">");
            sb.Append("<img height=\"32\" src=\"https://linhkienchatluong.vn/Uploads/pic/prods/---Cac-loai-khac/637512287278835418small.png\" v:shapes=\"Picture_x0020_1\" width=\"32\" />");
            sb.Append("  </td>");
            sb.Append("  <td style=\"width:520px; vertical-align:middle;\">");
            sb.Append("    <b><img  style=\"width: 60px; height: 60px; display: inline;\" border=\"0\" src=\"https://linhkienchatluong.vn/Uploads/pic/prods/---Cac-loai-khac/637512287278835418small.png\"></b>");
            sb.Append("  </td>");
            sb.Append("  </tr>");

            sb.Append("</table>");
            Response.Write(sb.ToString());
            Response.Flush();
            Response.End();
            //PhatTrienNangCap/ExportExel

          
        }

       
    }
}