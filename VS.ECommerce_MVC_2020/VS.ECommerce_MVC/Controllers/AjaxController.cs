using AutoMapper;
using Entity;
using Framwork;
using MoreAll;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VS.ECommerce_MVC.Models;

namespace VS.ECommerce_MVC.Controllers
{
    public class AjaxController : Controller
    {
        private string language = Captionlanguage.SetLanguage();
        DatalinqDataContext db = new DatalinqDataContext();
        public const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Contact/";
        // link sẽ là http://localhost:63136/Ajax/AddToCart
        /// link -> Ajax/AddToCart


        //   ExportPhatTrien

        #region Xuất exel http://localhost:63136/Ajax/Export
        [HttpGet]
        public ActionResult Export()// http://localhost:63136/Ajax/Export
        {
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=ExcelDemo.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index :D
            return RedirectToAction("Index");
        }
        private List<ProductViewModel> CreateTestItems()
        {
            var resultsList = new List<ProductViewModel>();
            //List<Entity.Products> dt = SProducts.GetByAll("VIE");

            var data = SProducts.GetByAll("VIE");
            var dt = Mapper.Map<List<Entity.Products>, List<ProductViewModel>>(data);

            for (int i = 0; i < dt.Count(); i++)
            {
                var a = new ProductViewModel()
                {
                    ipid = dt[i].ipid,
                    Name = dt[i].Name,
                    Code = dt[i].Code,
                    Brief = dt[i].Brief,
                    //  Contents = dt[i].Contents,
                    Price = dt[i].Price
                };
                resultsList.Add(a);
            }
            return resultsList;
        }
        private Stream CreateExcelFile(Stream stream = null)
        {
            var list = CreateTestItems();
            using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
            {
                // Tạo author cho file Excel
                excelPackage.Workbook.Properties.Author = "XuatExel";
                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = "XuatExel";
                // thêm tí comments vào làm màu 
                excelPackage.Workbook.Properties.Comments = "XuatExel";
                // Add Sheet vào file Excel
                excelPackage.Workbook.Worksheets.Add("Sheet1");
                // Lấy Sheet bạn vừa mới tạo ra để thao tác 
                var workSheet = excelPackage.Workbook.Worksheets[1];
                // Đỗ data vào Excel file
                workSheet.Cells[1, 1].LoadFromCollection(list, true, TableStyles.Dark9);
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<ProductViewModel> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 10;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "Tên sản phẩm";
            worksheet.Cells[1, 3].Value = "Mã sản phẩm";
            worksheet.Cells[1, 4].Value = "Mô tả";
            worksheet.Cells[1, 5].Value = "Nội dung";
            worksheet.Cells[1, 6].Value = "Giá";

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:D1"])
            {
                // Set PatternType
                // range.Style.Fill.PatternType = ExcelFillStyle.DarkGray;
                // Set Màu cho Background
                // range.Style.Fill.BackgroundColor.SetColor(Color.Aqua);
                // Canh giữa cho các text
                // range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 10));
                // Set Border
                //range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                // Set màu ch Border
                // range.Style.Border.Bottom.Color.SetColor(Color.Blue);
            }

            // Đỗ dữ liệu từ list vào 
            for (int i = 0; i < listItems.Count; i++)
            {
                var item = listItems[i];
                worksheet.Cells[i + 2, 1].Value = item.ipid;
                worksheet.Cells[i + 2, 2].Value = item.Name;
                worksheet.Cells[i + 2, 3].Value = item.Code;
                worksheet.Cells[i + 2, 4].Value = item.Brief;
                // worksheet.Cells[i + 2, 5].Value = item.Contents;
                worksheet.Cells[i + 2, 6].Value = item.Price;
            }
        }
        #endregion

        #region Thêm mới với File có sẵn đầu vào D:\ExcelDemo.xlsx
        private DataTable ReadFromExcelfile(string path, string sheetName)
        {
            // Khởi tạo data table
            DataTable dt = new DataTable();
            // Load file excel và các setting ban đầu
            using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
            {
                if (package.Workbook.Worksheets.Count < 1)
                {
                    // Log - Không có sheet nào tồn tại trong file excel của bạn
                    return null;
                }
                // Khởi Lấy Sheet đầu tiện trong file Excel để truy vấn, truyền vào name của Sheet để lấy ra sheet cần, nếu name = null thì lấy sheet đầu tiên
                ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == sheetName) ?? package.Workbook.Worksheets.FirstOrDefault();
                // Đọc tất cả các header
                foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
                {
                    dt.Columns.Add(firstRowCell.Text);
                }
                // Đọc tất cả data bắt đầu từ row thứ 2
                for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
                {
                    // Lấy 1 row trong excel để truy vấn
                    var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                    // tạo 1 row trong data table
                    var newRow = dt.NewRow();
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;

                    }
                    dt.Rows.Add(newRow);
                }
            }
            return dt;
        }


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
        public string CheckPhone(string dienthoai)
        {
            string chuoi = "";
            if (dienthoai != "")
            {
                var cat = db.Members.Where(m => m.DienThoai == dienthoai).ToList();
                if (cat.Count > 0)
                {
                    chuoi = "Số điện thoại này đã tồn tại!";
                }
            }
            return chuoi;
        }

        [HttpPost]
        // [ValidateInput(false)]
        public ActionResult LienHe(FormCollection collect)
        {
            // string ddllist = collect["ddlcategory"];// lấy ra ID của Dropdowlist
            if (ModelState.IsValid)
            {
                #region Contacts
                Entity.Contacts obj = new Entity.Contacts();
                obj.vtitle = "";
                obj.vname = collect["HoVaTen"];
                obj.vaddress = "";
                obj.vphone = collect["DienThoai"];
                obj.vemail = collect["Email"];
                obj.vcontent = collect["NoiDung"];
                obj.dcreatedate = DateTime.Now;
                obj.lang = "VIE";
                obj.istatus = 0;
                if (SContacts.INSERT(obj) == true)
                {
                    TempData["ThongBao"] = "Gửi liên hệ thành công";

                    #region Senmail
                    if (!Commond.Setting("Emailden").Equals(""))
                        Senmail(collect["HoVaTen"], "", collect["DienThoai"], collect["Email"], "", collect["NoiDung"]);
                    #endregion

                    return Content("<html><head><script type=\"text/javascript\">alert('Gửi liên hệ thành công');window.location.href='/'; </script></head></html>");
                }
                #endregion
            }
            return RedirectToAction("Index", "Home");
        }
        void Senmail(string txtHoTen, string txtaddress, string txtphone, string txtemail, string txttieude, string txtcontent)
        {
            try
            {
                string title = "";
                System.Text.StringBuilder strb = new System.Text.StringBuilder();
                strb.AppendLine("<div style=\"width:100%; padding:10px; line-height:22px;\"> ");
                strb.AppendLine("<div style=\"font-size:18px; font-weight:bold; text-align:center; color:#F00; text-decoration:none;text-transform:uppercase;\">Thông tin liên hệ của khách hàng</div> ");
                strb.AppendLine("<div style=\"font-weight:bold; color:#666; padding-top:10px; text-align:center;text-decoration:none;\"> Website: " + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "/</div>");
                strb.AppendLine("<div style=\" color:#666; padding-top:10px\"> ");
                strb.AppendLine("<div style=\"font-size:14px;font-weight:bold; padding-bottom:5px;text-transform:uppercase; text-decoration:underline;color:#fe0505\">Thông tin khách hàng</div> ");
                strb.AppendLine(" <table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\"> ");
                strb.AppendLine(" <tr> ");
                strb.AppendLine(" <td style=\"border-bottom:dotted 1px #d6d6d6; height:22px; width:20%\">Họ và tên:</td> ");
                strb.AppendLine(" <td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;color:#fe0505\">" + txtHoTen + "</td> ");
                strb.AppendLine("</tr> ");
                //strb.AppendLine("<tr> ");
                //strb.AppendLine(" <td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">Địa chỉ:</td>");
                //strb.AppendLine(" <td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">" + txtaddress + "</td>");
                //strb.AppendLine("</tr>");
                strb.AppendLine("<tr>");
                strb.AppendLine(" <td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">Điện thoại:</td>");
                strb.AppendLine("<td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">" + txtphone + "</td>");
                strb.AppendLine("</tr>");
                strb.AppendLine(" <tr>");
                strb.AppendLine("  <td style=\"border-bottom:dotted 1px #d6d6d6;height:22px;\">Email:</td>");
                strb.AppendLine(" <td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">" + txtemail + "</td>");
                strb.AppendLine("</tr>");
                //strb.AppendLine("<tr>");
                //strb.AppendLine("<td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">Tiêu đề:</td>");
                //strb.AppendLine("<td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\"> " + txttieude + "</td>");
                //strb.AppendLine("</tr>");
                strb.AppendLine("<tr>");
                strb.AppendLine("<td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">Ngày Gửi:</td>");
                strb.AppendLine("<td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\"> " + DateTime.Now.AddYears(3).ToString("MM/dd/yyyy HH:mm:ss") + "</td>");
                strb.AppendLine("</tr>");
                strb.AppendLine("<tr>");
                strb.AppendLine("<td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\">Nội dung:</td>");
                strb.AppendLine("<td style=\"border-bottom:dotted 1px #d6d6d6; height:22px;\"> " + txtcontent + "</td>");
                strb.AppendLine("</tr>");
                strb.AppendLine("</table>");
                strb.AppendLine("</div>");
                strb.AppendLine("</div>");
                string email = Email.email();
                string password = Email.password();
                int port = Convert.ToInt32(Email.port());
                string host = Email.host();
                MailUtilities.SendMail("Liên hệ từ " + txtHoTen + "", email, password, Commond.Setting("Emailden"), host, port, "Thông tin liên hệ của khách hàng từ Website: " + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "", strb.ToString());
                //MailUtilities.SendMail("Liên hệ từ " + txtHoTen.Text.Trim() + "", email, password, this.txtemail.Text.Trim(), host, port, "Thông tin liên hệ của khách hàng từ Website: " + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "", strb.ToString());
            }
            catch (Exception)
            { return; }
        }



        [HttpGet]
        public ActionResult ReadFromExcel()
        {
            var data = ReadFromExcelfile(@"D:\ExcelDemo.xlsx", "Sheet1");
            return View(data);
        }
        #endregion

        
        [HttpPost]
        public ActionResult UploadFile_BIBO()
        {
            try
            {
                var file = System.Web.HttpContext.Current.Request.Files["MyImages"];
                string filename = Regex.Replace(Path.GetFileName(Server.MapPath(file.FileName)), @"[^0-9a-zA-Z:,.-]+", "");
                string str_dir = HttpContext.Server.MapPath("~/Uploads/Test/") + DateTime.Now.ToString("yyyy") + @"\" + DateTime.Now.ToString("MM");
                if (!Directory.Exists(str_dir))
                {
                    Directory.CreateDirectory(str_dir);
                }

                #region save raw
                string link = str_dir + @"/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_imp_" + filename;
                file.SaveAs(link);
                #endregion

                var matP = Regex.Split(link.Replace("\\", "/"), "Uploads");
                // return Json(new { code = 1, link = "/Images/" + matP[1] });
                return Json(new { code = 1, link = "/Uploads/Test/" + matP[1], showfie = "/image/iconpdf.png" });
            }
            catch (Exception ex)
            {
                return Json(@"{""code"":""0"",""link"":""""}");
            }
        }
        [HttpPost]
        public ActionResult UploadFiles_FileNang()
        {
            try
            {
                //Fetch the File.
                HttpPostedFile postedFile = System.Web.HttpContext.Current.Request.Files[0];
                //Create the Directory.
                string path = HttpContext.Server.MapPath("~/Uploads/Test/") + DateTime.Now.ToString("yyyy") + @"\" + DateTime.Now.ToString("MM");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //Fetch the File Name.
                string fileName = postedFile.FileName;
                string link = path + @"/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_imp_" + fileName;
                //Save the File.
                postedFile.SaveAs(link);
                var matP = Regex.Split(link.Replace("\\", "/"), "Uploads");
                //Send OK Response to Client.
                return Json(new { code = 1, link = "/Uploads/Test/" + matP[1] });
            }
            catch (Exception ex)
            {
                return Json(@"{""code"":""0"",""link"":""""}");
            }

        }

        [HttpPost]
        async public Task<ActionResult> UploadFile_Multiple(string foldername)
        {
            string path = "";
            string Link = "";
            string UrlImages = "";
            HttpFileCollectionBase files = Request.Files;
            for (var i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];

                string filename = Regex.Replace(Path.GetFileName(Server.MapPath(file.FileName)), @"[^0-9a-zA-Z:,.-]+", "");
                string str_dir = HttpContext.Server.MapPath("~/Uploads/Test/") + DateTime.Now.ToString("yyyy") + @"\" + DateTime.Now.ToString("MM");
                if (!Directory.Exists(str_dir))
                {
                    Directory.CreateDirectory(str_dir);
                }

                #region save raw
                path = str_dir + @"/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_imp_" + filename;
                file.SaveAs(path);
                var matP = Regex.Split(path.Replace("\\", "/"), "Uploads");
                #endregion

                if (files.Count == 1)
                {
                    UrlImages += "/Uploads" + matP[1];
                    Link += "<img src=\"/Uploads" + matP[1] + "\" class='imgFull' />";
                }
                else
                {
                    UrlImages += "/Uploads" + matP[1] + ",";
                    Link += "<img src=\"/Uploads" + matP[1] + "\" style=\" width:200px\" />";
                }
            }
            ViewBag.ShowImg = path;
            return Json(new { code = 1, Images = Link, ImagesUrl = UrlImages.TrimEnd(',') });
        }



        [HttpPost]
        public ActionResult UploadFile_SQL()
        {
            try
            {
                var file = System.Web.HttpContext.Current.Request.Files["MyImages"];
                string filename = Regex.Replace(Path.GetFileName(Server.MapPath(file.FileName)), @"[^0-9a-zA-Z:,.-]+", "");
                string str_dir = HttpContext.Server.MapPath("~/Uploads/sql/");
                if (!Directory.Exists(str_dir))
                {
                    Directory.CreateDirectory(str_dir);
                }
                #region save raw
                string c = filename;
                string link = str_dir + @"" + c.Replace(c.ToString(), "SQLQuery.sql");
                System.IO.File.Delete(link);
                file.SaveAs(link);
                #endregion
                var matP = Regex.Split(link.Replace("\\", "/"), "Uploads");
                string sql = "";
                string lblAlert = "";
                try
                {
                    sql = MoreAll.MoreAll.RunScriptFile("SQLQuery.sql", false);
                    if (sql == "ERROR")
                    {
                        sql = "Không tồn tại file script hoặc thư mục sql";
                    }

                    lblAlert = "<b style=\"color:red;\">Cập nhật CSDL Thành công</b>OK<br/>" + sql;
                }
                catch (Exception ex)
                {
                    if (sql == "ERROR")
                    {
                        sql = "Không tồn tại file script hoặc thư mục sql ";
                    }
                    lblAlert = "Lỗi khi Cập nhật CSDL <br/>" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace + "<br/>" + sql;
                }


                return Json(new { code = lblAlert, link = "/Uploads/sql/" + matP[1] });
            }
            catch (Exception ex)
            {
                return Json(@"{""code"":""0"",""link"":""""}");
            }
        }

        [HttpGet]
        public ActionResult AddToCart(string ipid)
        {
            if (ipid != "")
            {
                List<Entity.Products> dt = SProducts.GetById(ipid);
                if (dt.Count > 0)
                {
                    SessionCarts.ShoppingCart_AddProduct_Sesion(ipid.ToString(), Convert.ToInt32("1"), "0", "0");
                }
                Response.Redirect("/gio-hang.html");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Box_TimKiem(FormCollection collect)
        {
            string txtkeyword = collect["txtkeyword"];
            MoreAll.MoreAll.SetCookie("Search", txtkeyword, 5000);
            Response.Redirect("/Search.html");
            return View();
        }

        [HttpPost]
        public ActionResult MuaHangTheoSoLuong(string Quantity, string ID)
        {
            if (ID != "")
            {
                List<Entity.Products> dt = SProducts.GetById(ID);
                if (dt.Count > 0)
                {
                    SessionCarts.ShoppingCart_AddProduct_Sesion(ID.ToString(), Convert.ToInt32(Quantity), "0", "0");
                }
                Response.Redirect("/gio-hang.html");
            }
            return View();
        }

        //Đặt hàng nhanh ở trang chi tiết
        [HttpPost]
        public ActionResult DatHangNhanh(FormCollection collect)
        {
            string txtQuantitys = collect["txtQuantitys"];
            string hdipid = collect["hdipid"];
            List<Entity.Products> dt = SProducts.GetById(hdipid);
            if (dt.Count > 0)
            {
                SessionCarts.ShoppingCart_AddProduct_Sesion(hdipid.ToString(), Convert.ToInt32(txtQuantitys), "0", "0");
            }
            Response.Redirect("/gio-hang.html");

            return View();
        }


        [HttpGet]
        public ActionResult NgonNgu(string language)
        {
            string value = Request["language"].ToString();
            System.Web.HttpContext.Current.Session["language"] = value;
            System.Web.HttpContext.Current.Response.Redirect("/");
            return View();
        }

        [HttpPost]
        public ActionResult CapNhatSoLuong(string productId, string quantity)
        {
            DataTable dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
            dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
            SessionCarts.Cart_Updatequantity(ref dtcart, productId, quantity);
            System.Web.HttpContext.Current.Session["cart"] = dtcart;
            Response.Redirect("/gio-hang.html");
            return View();
        }

        public ActionResult LoadAjaxActionLink()
        {
            return Content("Hello Ajax");
        }

        #region Gio hang PopUp


        [HttpGet]
        public string ShowCart()
        {
            string str = "";
            string tongien = "0";
            string sosp = "0";
            string inumofproducts = "0";
            string totalvnd = "0";
            if (System.Web.HttpContext.Current.Session["cart"] != null)
            {
                DataTable dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                if (dtcart.Rows.Count > 0)
                {
                    str += "<div class=\"shop_cart ajax\">";
                    str += "<div id=\"Loadingshop\">";
                    str += "<div class=\"inner\"><img src=\"/Resources/ShopCart/images/ajax-loader_2.gif\"><p>Đang xử lý...</p></div>";
                    str += "</div>";
                    str += "<div class=\"title\">";
                    str += "<div class=\"tl txt_b\">Giỏ hàng của bạn (<span class=\"shopping_cart_item\">" + Services.SessionCarts.LoadCart() + "</span> sản phẩm)</div>";
                    str += "<input id=\"temp_total\" value=\"0\" type=\"hidden\">";
                    str += "</div>";


                    str += " <div id=\"popupCart\" class=\"clearfix\">";
                    str += "  <div class=\"content-popup-cart\">";
                    str += "    <div class=\"thead-popup\">";
                    str += "      <div style=\"width: 49.75%;\" class=\"text-left\">Sản phẩm</div>";
                    str += "      <div style=\"width: 15%;\" class=\"text-center\">Giá</div>";
                    str += "      <div style=\"width: 20%;\" class=\"text-center\">Số lượng</div>";
                    str += "      <div style=\"width: 15%;\" class=\"text-right\">Tổng tiền</div>";
                    str += "    </div>";

                    str += "    <div class=\"tbody-popup scrollbar-dynamic\">";

                    if (dtcart.Rows.Count > 0)
                    {
                        double num = 0.0;
                        int num2 = 0;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                            num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                        }
                        totalvnd = num.ToString();
                        inumofproducts = num2.ToString();
                    }
                    tongien = AllQuery.MorePro.FormatMoney_Cart_Total(totalvnd.ToString());
                    sosp = inumofproducts;
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        str += "      <div class=\"item-popup \">";
                        str += "        <div style=\"width: 15%;\" class=\"border height image_ text-left\">";
                        str += "          <div class=\"item-image\">";
                        str += "<img src=\"" + dtcart.Rows[i]["Vimg"].ToString() + "\" class='anhdaidienpop'>";
                        str += "          </div>";
                        str += "        </div>";
                        str += "        <div style=\"width:35%;\" class=\"height text-left\">";
                        str += "          <div class=\"item-info\">";
                        str += "            <p class=\"item-name\">";
                        str += "<a class=\"\" target=_blank href=\"" + AllQuery.MorePro.LoadLink(dtcart.Rows[i]["PID"].ToString()) + "\"><b>" + dtcart.Rows[i]["Name"].ToString() + "</b></a>";
                        str += "            </p>";
                        str += "<a class=\"i-del shows\" onclick=\"AJdeleteShoppingCartItem(" + dtcart.Rows[i]["PID"].ToString() + ",'" + dtcart.Rows[i]["Name"].ToString() + "')\"><img src=\"/Resources/ShopCart/images/xoa.png\" /> Bỏ sản phẩm</a>";
                        str += "          </div>";
                        str += "        </div>";
                        str += "        <div style=\"width: 15%;\" class=\"border height text-center\">";
                        str += "<div id=\"sell_price_pro_17876\">" + AllQuery.MorePro.FormatMoneyCart(dtcart.Rows[i]["Price"].ToString()) + "</div>";
                        str += "<div class=\"txt_d\">" + ShopGiacu(dtcart.Rows[i]["PID"].ToString()) + "</div>";
                        //  str += "<div class=\"txt_pink\">" + AllQuery.MorePro.Tietkiem(dtcart.Rows[i]["PID"].ToString()) + "</div>";
                        str += "        </div>";
                        str += "        <div style=\"width: 20%;\" class=\"border height text-center\">";
                        str += "          <div class=\"qty_thuongdq check_\">";
                        str += "<input type=\"number\" max=\"999\" min=\"0\" style=\" width:50px\"  value=\"" + dtcart.Rows[i]["Quantity"].ToString() + "\" onchange=\"AddShoppingCartItem(" + dtcart.Rows[i]["PID"].ToString() + ",'" + dtcart.Rows[i]["Name"].ToString() + "',$(this))\" class=\"txt_center cor3px shows\">";
                        str += "          </div>";
                        str += "        </div>";
                        str += "        <div style=\"width: 15%;\" class=\"border height text-right\">";
                        str += "          <span class=\"cart-price\">";
                        str += "            <span class=\"price\">" + AllQuery.MorePro.FormatMoneyCart(dtcart.Rows[i]["Money"].ToString()) + "</span>";
                        str += "          </span>";
                        str += "        </div>";

                        str += "        </div>";

                    }
                    str += "      </div>";
                    str += "    </div>";
                    str += "  </div>";

                    str += "<table class=\"tbl_cart\" style=\"\" cellpadding=\"5\">";
                    str += "<tbody>";
                    str += "<tr>";
                    str += "<td colspan=\"5\" class=\"txt_right\">";
                    str += "<div style=\"line-height: 26px;\">";
                    str += "Tổng cộng : <span class=\"sub1 txt_18 txt_pink total_value_step txt_b\" id=\"total_value\" data-value=\"" + tongien + "\">" + tongien + "</span><br>";
                    str += "<span id=\"other-discount\">Tổng số sản phẩm: <span data-discount=\"0\" id=\"price-discount\" class=\"txt_pink\">" + sosp + "</span></span><br>";
                    str += "<span>Thanh toán: <span id=\"total_shopping_price\" class=\"txt_pink txt_b total_value_step\">" + tongien + "</span></span>";
                    str += "<br>Giá đã bao gồm VAT";
                    str += "</div>";
                    str += "</td>";
                    str += "</tr>";
                    str += "<tr>";
                    str += "<td colspan=\"4\" class=\"txt_right\">";
                    str += "<a href=\"/\" class=\"txt_pink txt_18 txt_b\" style=\"float:left;\"><i class=\"fa fa-angle-left\"></i> Tiếp tục mua hàng</a>";
                    str += "<div style=\"float:right;\">";
                    //str += "<a class=\"btn bg_pink txt_center txt_20 txt_u\" href=\"/gio-hang.html\" style=\"padding:5px 50px;\">";
                    //str += "MUA ONLINE<br> <span class=\"txt_12\" style=\"text-transform: none;\">(giao hàng tận nơi)</span>";
                    //str += "</a>";
                    str += "<a class=\"adrbutton tienhanh\" href=\"/gio-hang.html\" >Tiến hành đặt hàng</a>";
                    str += "</div>";
                    str += "</td>";
                    str += "</tr>";
                    str += "</tbody>";
                    str += "</table>";
                    str += "</div> ";

                }
                else
                {
                    str += "<div class=\"shop_cart ajax\">";
                    str += "  <div class=\"num0\">";
                    str += " <div class=\"modalbodys cart_body\">";
                    str += "<i class=\"icon_cart\"></i>";
                    str += "<h2>Giỏ hàng của bạn hiện đang trống</h2>";
                    str += "<p>Hãy nhanh tay sở hữu những sản phẩm yêu thích của bạn</p>";
                    str += "<a class=\"adrbutton\" href=\"/\">Tiếp tục mua sắm</a>";
                    str += " </div>";
                    str += "  </div>";
                    str += "</div> ";
                }
            }
            else
            {
                str += "<div class=\"shop_cart ajax\">";
                str += "  <div class=\"num0\">";
                str += " <div class=\"modalbodys cart_body\">";
                str += "<i class=\"icon_cart\"></i>";
                str += "<h2>Giỏ hàng của bạn hiện đang trống</h2>";
                str += "<p>Hãy nhanh tay sở hữu những sản phẩm yêu thích của bạn</p>";
                str += "<a class=\"adrbutton\" href=\"/\">Tiếp tục mua sắm</a>";
                str += " </div>";
                str += "  </div>";
                str += "</div> ";
            }
            return (str.ToString());
        }

        [HttpGet]
        public string ShowTongTien()
        {
            string str = "";
            string tongien = "0";
            string totalvnd = "0";
            if (System.Web.HttpContext.Current.Session["cart"] != null)
            {
                DataTable dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                if (dtcart.Rows.Count > 0)
                {
                    if (dtcart.Rows.Count > 0)
                    {
                        double num = 0.0;
                        int num2 = 0;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                            num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                        }
                        totalvnd = num.ToString();
                    }
                    tongien = AllQuery.MorePro.FormatMoney_Cart_Total(totalvnd.ToString());

                    str += "<div class=\"cart-footer\">";
                    str += "    <hr>";
                    str += "    <div class=\"clearfix\">";
                    str += "        <div class=\"cart__subtotal\">";
                    str += "            <div class=\"cart__col-6\">Tổng tiền:</div>";
                    str += "            <div class=\"text-right cart__totle\">";
                    str += "                <span class=\"total-price\">" + tongien + "</span>";
                    str += "            </div>";
                    str += "        </div>";
                    str += "    </div>";
                    str += "    <div class=\"cart__btn-proceed-checkout-dt\">";
                    str += " <button onclick=\"location.href='/gio-hang.html'\" type=\"button\" class=\"button btn btn-default cart__btn-proceed-checkout\" id=\"btn-proceed-checkout\" title=\"Thanh toán\">Thanh toán</button>";
                    str += "    </div>";
                    str += "</div>";
                }
            }
            return (str.ToString());
        }

        [HttpGet]
        public string Cart_sidebar()
        {
            string str = "";
            string tongien = "0";
            string sosp = "0";
            string inumofproducts = "0";
            string totalvnd = "0";

            if (System.Web.HttpContext.Current.Session["cart"] != null)
            {
                DataTable dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                if (dtcart.Rows.Count > 0)
                {
                    str += "<div class=\"cart_body\">";
                    if (dtcart.Rows.Count > 0)
                    {
                        double num = 0.0;
                        int num2 = 0;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                            num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                        }
                        totalvnd = num.ToString();
                        inumofproducts = num2.ToString();
                    }
                    tongien = AllQuery.MorePro.FormatMoney_Cart_Total(totalvnd.ToString());
                    sosp = inumofproducts;


                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        str += "<div class=\"clearfix cart_product productid-" + dtcart.Rows[i]["PID"].ToString() + "\">";
                        str += "<a class=\"cart_image\" href=\"" + AllQuery.MorePro.LoadLink(dtcart.Rows[i]["PID"].ToString()) + "\" title=\"" + dtcart.Rows[i]["Name"].ToString() + "\">";
                        str += "<img src=\"" + dtcart.Rows[i]["Vimg"].ToString() + "\" alt=\"" + dtcart.Rows[i]["Name"].ToString() + "\">";
                        str += "</a>";
                        str += "<div class=\"cart_info\">";
                        str += "<div class=\"cart_name\">";
                        str += "<a href=\"" + AllQuery.MorePro.LoadLink(dtcart.Rows[i]["PID"].ToString()) + "\" title=\"" + dtcart.Rows[i]["Name"].ToString() + "\">" + dtcart.Rows[i]["Name"].ToString() + "</a>";
                        str += "</div>";
                        str += "<div class=\"row-cart-left\">";
                        str += "<div class=\"cart_item_name\">";
                        str += "<div>";

                        str += "<label class=\"cart_quantity\">Số lượng</label>";
                        str += "<div class=\"cart_select\">";
                        str += "<div class=\"input-group-btn\">";

                        str += "<input class=\"variantID\" type=\"hidden\" name=\"variantId\" value=\"" + dtcart.Rows[i]["PID"].ToString() + "\">";
                        str += "<button onclick=\"var result = document.getElementById('qtyItem" + dtcart.Rows[i]["PID"].ToString() + "'); var qtyItem" + dtcart.Rows[i]["PID"].ToString() + " = result.value; if( !isNaN( qtyItem" + dtcart.Rows[i]["PID"].ToString() + " ) &amp;&amp; qtyItem" + dtcart.Rows[i]["PID"].ToString() + " > 1 ) result.value--;return false;\" class=\"reduced items-count btn-minus btn btn-default\" type=\"button\">–</button>";
                        str += "<input type=\"text\" maxlength=\"3\" min=\"0\" class=\"input-text number-sidebar qtyItem" + dtcart.Rows[i]["PID"].ToString() + "\" id=\"qtyItem" + dtcart.Rows[i]["PID"].ToString() + "\" name=\"" + dtcart.Rows[i]["Name"].ToString() + "\" size=\"1\" value=\"" + dtcart.Rows[i]["Quantity"].ToString() + "\">";
                        str += "<button onclick=\"var result = document.getElementById('qtyItem" + dtcart.Rows[i]["PID"].ToString() + "'); var qtyItem" + dtcart.Rows[i]["PID"].ToString() + " = result.value; if( !isNaN( qtyItem" + dtcart.Rows[i]["PID"].ToString() + " )) result.value++;return false;\" class=\"increase items-count btn-plus btn btn-default\" type=\"button\">+</button>";

                        str += "</div>";
                        str += "</div>";
                        str += "</div>";
                        str += "</div>";
                        str += "<div class=\"text-right cart_prices\">";
                        str += "<div class=\"cart__price\">";
                        str += "<span class=\"cart__sale-price\">" + AllQuery.MorePro.FormatMoneyCart(dtcart.Rows[i]["Price"].ToString()) + "</span>";
                        str += "</div>";
                        str += "<a class=\"cart__btn-remove remove-item-cart\" href=\"javascript:void(0)\"   onclick=\"AJdeleteShoppingCartItem(" + dtcart.Rows[i]["PID"].ToString() + ",'" + dtcart.Rows[i]["Name"].ToString() + "')\" title=\"Bỏ sản phẩm\">Bỏ sản phẩm</a>";
                        str += "</div>";
                        str += "</div>";
                        str += "</div>";
                        str += "</div>";
                    }
                    str += "</div>";


                }
                else
                {
                    str += "<div class=\"cart_body\">";
                    str += "<div class=\"shop_cartNews\">";
                    str += "  <div class=\"num0\">";
                    str += " <div class=\"modalbodys cart_body\">";
                    str += "<i class=\"icon_cart\"></i>";
                    str += "<h2>Giỏ hàng của bạn hiện đang trống</h2>";
                    str += "<p>Hãy nhanh tay sở hữu những sản phẩm yêu thích của bạn</p>";
                    str += "<a class=\"adrbutton\" href=\"/\">Tiếp tục mua sắm</a>";
                    str += " </div>";
                    str += "  </div>";
                    str += "</div> ";
                    str += "</div> ";
                }
            }
            else
            {
                str += "<div class=\"cart_body\">";
                str += "<div class=\"shop_cartNews \">";
                str += "  <div class=\"num0\">";
                str += " <div class=\"modalbodys cart_body\">";
                str += "<i class=\"icon_cart\"></i>";
                str += "<h2>Giỏ hàng của bạn hiện đang trống</h2>";
                str += "<p>Hãy nhanh tay sở hữu những sản phẩm yêu thích của bạn</p>";
                str += "<a class=\"adrbutton\" href=\"/\">Tiếp tục mua sắm</a>";
                str += " </div>";
                str += "  </div>";
                str += "</div> ";
                str += "</div> ";
            }
            return (str.ToString());
        }


        //[HttpGet]
        //public string ShowCart()
        //{
        //    string str = "";
        //    string tongien = "0";
        //    string sosp = "0";
        //    string inumofproducts = "0";
        //    string totalvnd = "0";
        //    if (System.Web.HttpContext.Current.Session["cart"] != null)
        //    {
        //        DataTable dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
        //        if (dtcart.Rows.Count > 0)
        //        {
        //            str += "<div class=\"shop_cart ajax\">";
        //            str += "<div id=\"Loadingshop\">";
        //            str += "<div class=\"inner\"><img src=\"/Resources/ShopCart/images/ajax-loader_2.gif\"><p>Đang xử lý...</p></div>";
        //            str += "</div>";
        //            str += "<div class=\"title\">";
        //            str += "<div class=\"tl txt_b\">Giỏ hàng của bạn (<span class=\"shopping_cart_item\">" + Services.SessionCarts.LoadCart() + "</span> sản phẩm)</div>";
        //            str += "<input id=\"temp_total\" value=\"0\" type=\"hidden\">";
        //            str += "</div>";


        //            str += "<table class=\"tbl_cart\" style=\"\" cellpadding=\"5\">";
        //            str += "<tbody>";
        //            str += "<tr id=\"shopping-cart-first-row\" class=\"txt_u txt_14 txt_b\">";
        //            str += "<td  style=\"width: 367px;\">Sản phẩm</td>";
        //            str += "<td style=\"width: 174px;\" class=\"shopping-cart-price-col\">Đơn giá</td>";
        //            str += "<td style='width: 128px;' class=\"shopping-cart-quantity-col center\">Số lượng</td>";
        //            str += "<td style=\"text-align: right;width: 190px;\" class=\"shopping-cart-sum-col\">Thành tiền</td>";
        //            str += "</tr>";

        //            if (dtcart.Rows.Count > 0)
        //            {
        //                double num = 0.0;
        //                int num2 = 0;
        //                for (int i = 0; i < dtcart.Rows.Count; i++)
        //                {
        //                    num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
        //                    num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
        //                }
        //                totalvnd = num.ToString();
        //                inumofproducts = num2.ToString();
        //            }
        //            tongien = AllQuery.MorePro.FormatMoney_Cart_Total(totalvnd.ToString());
        //            sosp = inumofproducts;
        //            for (int i = 0; i < dtcart.Rows.Count; i++)
        //            {
        //                str += "<tr id=\"itm17876\">";
        //                str += "<td style=\"text-align: left;\">";
        //                str += "<div class=\"cartInfo-img fl\">";
        //                str += "<img src=\"" + dtcart.Rows[i]["Vimg"].ToString() + "\" style=\"vertical-align: middle; margin-right: 10px;width:60px;\">";
        //                str += "</div>";
        //                str += "<div class=\"sum\">";
        //                str += "<div class=\"cartInfo-name\">";
        //                str += "<a class=\"\" target=_blank href=\"" + AllQuery.MorePro.LoadLink(dtcart.Rows[i]["PID"].ToString()) + "\"><b>" + dtcart.Rows[i]["Name"].ToString() + "</b></a>";
        //                str += "<br>";
        //                str += "</div>";
        //                str += "<a class=\"i-del shows\" onclick=\"AJdeleteShoppingCartItem(" + dtcart.Rows[i]["PID"].ToString() + ",'" + dtcart.Rows[i]["Name"].ToString() + "')\"><img src=\"/Resources/ShopCart/images/xoa.png\" /> Bỏ sản phẩm</a>";
        //                str += "</div>";
        //                str += "</td>";
        //                str += "<td style=\"\">";
        //                str += "<span id=\"sell_price_pro_17876\">" + AllQuery.MorePro.FormatMoneyCart(dtcart.Rows[i]["Price"].ToString()) + "</span>";
        //                str += "<br><span class=\"txt_d\">" + ShopGiacu(dtcart.Rows[i]["PID"].ToString()) + "</span>";
        //                str += "<br><span class=\"txt_pink\">" + AllQuery.MorePro.Tietkiem(dtcart.Rows[i]["PID"].ToString()) + "</span>";
        //                str += "</td>";
        //                str += "<td class=\"center\">";
        //                //data-abc='123'
        //                str += "<input type=\"number\" max=\"999\" min=\"0\" style=\" width:50px\"  value=\"" + dtcart.Rows[i]["Quantity"].ToString() + "\" onchange=\"AddShoppingCartItem(" + dtcart.Rows[i]["PID"].ToString() + ",'" + dtcart.Rows[i]["Name"].ToString() + "',$(this))\" class=\"txt_center cor3px shows\">";
        //                str += "</td>";
        //                str += "<td style=\"text-align: right;\">";
        //                str += "<span id=\"price_pro_17876\">" + AllQuery.MorePro.FormatMoneyCart(dtcart.Rows[i]["Money"].ToString()) + "</span>";
        //                str += "</td>";
        //                str += "</tr>";
        //            }

        //            str += "<tr>";
        //            str += "<td colspan=\"5\" class=\"txt_right\">";
        //            str += "<div style=\"line-height: 26px;\">";
        //            str += "Tổng cộng : <span class=\"sub1 txt_18 txt_pink total_value_step txt_b\" id=\"total_value\" data-value=\"" + tongien + "\">" + tongien + "</span><br>";
        //            str += "<span id=\"other-discount\">Tổng số sản phẩm: <span data-discount=\"0\" id=\"price-discount\" class=\"txt_pink\">" + sosp + "</span></span><br>";
        //            str += "<span>Thanh toán: <span id=\"total_shopping_price\" class=\"txt_pink txt_b total_value_step\">" + tongien + "</span></span>";
        //            str += "<br>Giá đã bao gồm VAT";
        //            str += "</div>";
        //            str += "</td>";
        //            str += "</tr>";
        //            str += "<tr>";
        //            str += "<td colspan=\"4\" class=\"txt_right\">";
        //            str += "<a href=\"/\" class=\"txt_pink txt_18 txt_b\" style=\"float:left;\"><i class=\"fa fa-angle-left\"></i> Tiếp tục mua hàng</a>";
        //            str += "<div style=\"float:right;\">";
        //            //str += "<a class=\"btn bg_pink txt_center txt_20 txt_u\" href=\"/gio-hang.html\" style=\"padding:5px 50px;\">";
        //            //str += "MUA ONLINE<br> <span class=\"txt_12\" style=\"text-transform: none;\">(giao hàng tận nơi)</span>";
        //            //str += "</a>";
        //            str += "<a class=\"adrbutton tienhanh\" href=\"/gio-hang.html\" >Tiến hành đặt hàng</a>";
        //            str += "</div>";
        //            str += "</td>";
        //            str += "</tr>";
        //            str += "</tbody>";
        //            str += "</table>";
        //            str += "</div> ";

        //        }
        //        else
        //        {
        //            str += "<div class=\"shop_cart ajax\">";
        //            str += "  <div class=\"num0\">";
        //            str += " <div class=\"modalbodys cart_body\">";
        //            str += "<i class=\"icon_cart\"></i>";
        //            str += "<h2>Giỏ hàng của bạn hiện đang trống</h2>";
        //            str += "<p>Hãy nhanh tay sở hữu những sản phẩm yêu thích của bạn</p>";
        //            str += "<a class=\"adrbutton\" href=\"/\">Tiếp tục mua sắm</a>";
        //            str += " </div>";
        //            str += "  </div>";
        //            str += "</div> ";
        //        }
        //    }
        //    else
        //    {
        //        str += "<div class=\"shop_cart ajax\">";
        //        str += "  <div class=\"num0\">";
        //        str += " <div class=\"modalbodys cart_body\">";
        //        str += "<i class=\"icon_cart\"></i>";
        //        str += "<h2>Giỏ hàng của bạn hiện đang trống</h2>";
        //        str += "<p>Hãy nhanh tay sở hữu những sản phẩm yêu thích của bạn</p>";
        //        str += "<a class=\"adrbutton\" href=\"/\">Tiếp tục mua sắm</a>";
        //        str += " </div>";
        //        str += "  </div>";
        //        str += "</div> ";
        //    }
        //    return (str.ToString());
        //}

        [HttpPost]
        public string Up_Order(string id, string quantity)
        {
            return ShoppingCart_AddProduct(id.ToString(), int.Parse(quantity));
        }

        public string ShoppingCart_AddProduct(string pid, int quantity)
        {
            if (System.Web.HttpContext.Current.Session["cart"] == null)
            {
                // create session cart.
                SessionCarts.ShoppingCreateCart();
                ShoppingCart_AddProduct(pid, quantity);
            }
            else
            {
                List<Products> dt = new List<Products>();
                // lay chi tiet san pham.
                dt = SProducts.GetById(pid);
                if (dt.Count > 0)
                {
                    string vimg = dt[0].Images.ToString();
                    string name = dt[0].Name.ToString();
                    string Mausac = "0";
                    string Kichco = "0";
                    try
                    {
                        if (System.Web.HttpContext.Current.Session["Session_Size"].ToString() != null && !System.Web.HttpContext.Current.Session["Session_Size"].ToString().Equals(""))
                        {
                            Kichco = System.Web.HttpContext.Current.Session["Session_Size"].ToString();
                        }
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        if (System.Web.HttpContext.Current.Session["Session_MauSac"].ToString() != null && !System.Web.HttpContext.Current.Session["Session_MauSac"].ToString().Equals(""))
                        {
                            Mausac = System.Web.HttpContext.Current.Session["Session_MauSac"].ToString();
                        }
                    }
                    catch (Exception)
                    { }
                    if (!dt[0].Price.ToString().Equals(""))
                    {
                        float prices = 0;
                        float pricesi = 0;
                        if (dt[0].Price.Length > 0)
                        {
                            prices = Convert.ToSingle(dt[0].Price);
                        }
                        float money = prices * quantity;
                        DataTable dtcart = new DataTable();
                        dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                        bool hasincart = false;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                            {
                                if (dtcart.Rows[i]["Price"].ToString().Length > 0)
                                {
                                    pricesi = Convert.ToSingle(dtcart.Rows[i]["Price"]);
                                }
                                hasincart = true;
                                // cap nhat thong tin cua cart.
                                quantity += Convert.ToInt32(dtcart.Rows[i]["Quantity"]);
                                dtcart.Rows[i]["Quantity"] = quantity;
                                dtcart.Rows[i]["Money"] = quantity * Convert.ToSingle(pricesi);
                                dtcart.Rows[i]["Mausac"] = Mausac;
                                dtcart.Rows[i]["Kichco"] = Kichco;
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                                break;
                            }
                        }
                        if (hasincart == false)
                        {
                            if (dtcart != null)
                            {
                                DataRow dr = dtcart.NewRow();
                                dr["PID"] = pid;
                                dr["Vimg"] = vimg;
                                dr["Name"] = name;
                                dr["Price"] = prices;
                                dr["Quantity"] = quantity;
                                dr["Money"] = money;
                                dr["Mausac"] = Mausac;
                                dr["Kichco"] = Kichco;
                                dtcart.Rows.Add(dr);
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                            }
                        }
                    }
                    else
                    {
                        float prices = 0; float pricesi = 0;
                        if (dt[0].Price.Length > 0)
                        {
                            prices = Convert.ToSingle(dt[0].Price);
                        }
                        float money = prices * quantity;
                        DataTable dtcart = new DataTable();
                        dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                        bool hasincart = false;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                            {
                                if (dtcart.Rows[i]["Price"].ToString().Length > 0)
                                {
                                    pricesi = Convert.ToSingle(dtcart.Rows[i]["Price"]);
                                }
                                hasincart = true;
                                // cap nhat thong tin cua cart.
                                quantity += Convert.ToInt32(dtcart.Rows[i]["Quantity"]);
                                dtcart.Rows[i]["Quantity"] = quantity;
                                dtcart.Rows[i]["Money"] = quantity * Convert.ToSingle(pricesi);
                                dtcart.Rows[i]["Mausac"] = Mausac;
                                dtcart.Rows[i]["Kichco"] = Kichco;
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                                break;
                            }
                        }
                        if (hasincart == false)
                        {
                            if (dtcart != null)
                            {
                                DataRow dr = dtcart.NewRow();
                                dr["PID"] = pid;
                                dr["Vimg"] = vimg;
                                dr["Name"] = name;
                                dr["Price"] = prices;
                                dr["Quantity"] = quantity;
                                dr["Money"] = money;
                                dr["Mausac"] = Mausac;
                                dr["Kichco"] = Kichco;
                                dtcart.Rows.Add(dr);
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                            }
                        }
                    }
                }
            }
            System.Web.HttpContext.Current.Session["Session_Size"] = null;
            System.Web.HttpContext.Current.Session["Session_MauSac"] = null;
            System.Web.HttpContext.Current.Session["GSession_MauSac"] = null;
            System.Web.HttpContext.Current.Session["GSession_Size"] = null;
            return "";
        }

        [HttpPost]
        public string Updatequantity(string id, string quantity)
        {
            if (System.Web.HttpContext.Current.Session["cart"] != null)
            {
                DataTable dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                return Cart_Updatequantity(ref dtcart, id, quantity);
            }
            return "";
        }
        protected static string Cart_Updatequantity(ref DataTable dtcart, string pid, string quantity)
        {
            if (dtcart.Rows.Count > 0)
            {
                for (int i = 0; i < dtcart.Rows.Count; i++)
                {
                    if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                    {
                        dtcart.Rows[i]["quantity"] = quantity;
                        dtcart.Rows[i]["Money"] = Convert.ToInt32(quantity) * Convert.ToDouble(dtcart.Rows[i]["Price"].ToString());
                        // return "";
                    }
                }
            }
            return "";
        }

        [HttpPost]
        public string DeleteShopCart(string id, string quantity)
        {
            return ShoppingCart_RemoveProduct(id.ToString());
        }

        protected static string ShoppingCart_RemoveProduct(string pid)
        {
            DataTable dtcart = new DataTable();
            dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];

            for (int i = 0; i < dtcart.Rows.Count; i++)
            {
                if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                {
                    dtcart.Rows.RemoveAt(i);
                    break;
                }
            }
            System.Web.HttpContext.Current.Session["cart"] = dtcart;
            return "";
        }

        [HttpPost]
        public string Up_KichCo(string id, string quantity)
        {
            return Session_Size(id.ToString());
        }

        protected static string Session_Size(string pid)
        {
            System.Web.HttpContext.Current.Session["Session_Size"] = pid.ToString();
            System.Web.HttpContext.Current.Session["GSession_Size"] = pid.ToString();
            return "";
        }

        [HttpPost]
        public string Up_MauSac(string id, string quantity)
        {
            return Session_MauSac(id.ToString());
        }

        protected static string Session_MauSac(string pid)
        {
            System.Web.HttpContext.Current.Session["Session_MauSac"] = pid.ToString();
            System.Web.HttpContext.Current.Session["GSession_MauSac"] = pid.ToString();
            return "";
        }

        [HttpGet]
        public string Loadinfo()
        {
            string str = "";
            List<Entity.Products> dt = SProducts.Name_Text("Select top 1 * from products ORDER BY NEWID()");
            if (dt.Count > 0)
            {
                str += " <div class=\"wpfomo-product-thumb-container\">";
                str += " <img src=\"" + dt[0].Images.ToString() + "\" class=\"wpfomo-product-thumb\"></div>";
                str += "<div class=\"wpfomo-content-wrapper\">";
                str += " <p><span class=\"wpfomo-buyer-name\">Chị Tuyết ở Cầu Giấy, Hn</span></p>";
                str += " <a href=\"\" target=\"_blank\" class=\"wpfomo-product-name\">Mua Thành Công " + dt[0].Name.ToString() + "</a><div class=\"time\">";
                str += " <span class=\"wpfomo-secondary-text\">Đã mua " + dt[0].Price.ToString() + " phút trước</span>";
                str += "</div>";
                str += "</div>";
            }
            return (str.ToString());
        }
        [HttpGet]
        public string LoadCart()
        {
            if (System.Web.HttpContext.Current.Session["cart"] != null)
            {
                DataTable cartdetail = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                if (cartdetail.Rows.Count > 0)
                {
                    string inumofproducts = "";
                    string totalvnd = "";
                    if (cartdetail.Rows.Count > 0)
                    {
                        double num = 0.0;
                        int num2 = 0;
                        for (int i = 0; i < cartdetail.Rows.Count; i++)
                        {
                            num += Convert.ToDouble(cartdetail.Rows[i]["Money"].ToString());
                            num2 += Convert.ToInt32(cartdetail.Rows[i]["Quantity"].ToString());
                        }
                        totalvnd = num.ToString();
                        inumofproducts = num2.ToString();
                    }
                    return inumofproducts;
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }
        public static string ShopGiacu(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                if (dt[0].OldPrice.ToString().Length > 0)
                {
                    str = AllQuery.MorePro.Detail_Price(dt[0].OldPrice.ToString()) + " đ";
                }
            }
            return str.ToString();
        }
        #endregion

        // ví dụ của code thầy bên myclass
        //public ActionResult DemoAjax()
        //{
        //    DonDatHang ddh = db.DonDatHangs.First();

        //    return View();
        //}
        ////Xử lý ajax action link
        //public ActionResult LoadAjaxActionLink()
        //{
        //    System.Threading.Thread.Sleep(2000);
        //    return Content("Hello Ajax");
        //}
        ////Xử lý ajax BeginForm
        //public ActionResult LoadAjaxBeginForm(FormCollection f)
        //{
        //    System.Threading.Thread.Sleep(2000);
        //    string KQ = f["txt1"].ToString();
        //    return Content(KQ);

        //}
        ////Xử lý Ajax Jquery
        //public ActionResult LoadAjaxJQuery(int a, int b)
        //{
        //    System.Threading.Thread.Sleep(2000);
        //    return Content((a + b).ToString());
        //}

        ////Sử dụng load ajax trả về kết quả 1 partialview

        ////Tạo partialview
        //public ActionResult LoadSanPhamAjax()
        //{
        //    var lstSanPham = db.SanPhams;
        //    return PartialView("LoadSanPhamAjax",lstSanPham);
        //}

        [HttpPost]
        public ActionResult add_danh_gia(string hdsipid, string IDGiaoVien, string IDThanhVien, string rating_value, string comment)
        {
            DatalinqDataContext db = new DatalinqDataContext();
            LDanhGiaSao obj = new LDanhGiaSao();
            obj.IDThanhVien = Convert.ToInt32(IDThanhVien);
            obj.IDSanPham = Convert.ToInt32(hdsipid);
            obj.IDGiaoVien = Convert.ToInt32(IDGiaoVien);
            if (rating_value == "1")
            {
                obj.MotSao = 1;
            }
            if (rating_value == "2")
            {
                obj.HaiSao = 1;
            }
            if (rating_value == "3")
            {
                obj.BaSao = 1;
            }
            if (rating_value == "4")
            {
                obj.BonSao = 1;
            }
            if (rating_value == "5")
            {
                obj.NamSao = 1;
            }
            obj.NoiDung = comment;
            obj.Create_Date = DateTime.Now;
            db.LDanhGiaSaos.InsertOnSubmit(obj);
            db.SubmitChanges();
            string ketqua = "Gửi đánh giá khóa học thành công.";
            return Content(ketqua);
        }

    }
}
