using Advertisings;
using MoreAll;
using OfficeOpenXml;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.IO;
using Vietdung.Common;
using System.Runtime.Caching;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Web.Security;

namespace VS.ECommerce_MVC.Controllers
{
    public class HomeController : Controller
    {
        DatalinqDataContext db = new DatalinqDataContext();
        private string language = Captionlanguage.SetLanguage();
        public ActionResult TestMenuMobile()
        {
            return View();
        }
        public ActionResult HeaderCart()
        {
            return View();
        }

        [OutputCache(Duration = int.MaxValue, VaryByParam = "hp", Location = OutputCacheLocation.Server)]
        public ActionResult ShowDemo(string hp)
        {
            string abc = DateTime.Now + "--->" + hp;
            //  var cacheManager = new OutputCacheManager();
            //  cacheManager.RemoveItem("Shared", "Slider");
            ViewBag.show = abc;
            return View();
        }

        #region Index
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();

            //    var news = (from p in db.News where p.lang == language orderby p.Create_Date descending select p).ToList();
            //    var s = (from p in db.products where p.Status == 1 && p.lang == language orderby p.Create_Date descending select p).ToList();
            ViewBag.show = MoreAll.MoreAll.GetCache("Anhnentintuc", System.Web.HttpContext.Current.Cache["Anhnentintuc"] != null ? "" : Ad_vertisings.Anhnentintuc("7"));
            ViewBag.QuangcaoDuoitintuctrangchu = MoreAll.MoreAll.GetCache("QuangcaoDuoitintuctrangchu", System.Web.HttpContext.Current.Cache["QuangcaoDuoitintuctrangchu"] != null ? "" : Ad_vertisings.QuangcaoDuoitintuctrangchu("1"));
            ViewBag.phuongthucvanchuyen = MoreAll.MoreAll.GetCache("PhuongThucVanChuyen", System.Web.HttpContext.Current.Cache["PhuongThucVanChuyen"] != null ? "" : Ad_vertisings.PhuongThucVanChuyen("4"));
            ViewBag.babner = MoreAll.MoreAll.GetCache("Advertisings_A_Images", System.Web.HttpContext.Current.Cache["Advertisings_A_Images"] != null ? "" : Ad_vertisings.Advertisings_A_Images("3"));
            ViewBag.ShowMenuPro = MoreAll.MoreAll.GetCache("ShowMenuPro", System.Web.HttpContext.Current.Cache["ShowMenuPro"] != null ? "" : ShowMenuPro());
            ViewBag.nhomsanpham = MoreAll.MoreAll.GetCache("ShowNhomsanpham", System.Web.HttpContext.Current.Cache["ShowNhomsanpham"] != null ? "" : ShowNhomsanpham());
            ViewBag.News = MoreAll.MoreAll.GetCache("LoadNews", System.Web.HttpContext.Current.Cache["LoadNews"] != null ? "" : LoadNews());
            ViewBag.ShowCanhBanerLoadNews = MoreAll.MoreAll.GetCache("ShowCanhBanerLoadNews", System.Web.HttpContext.Current.Cache["ShowCanhBanerLoadNews"] != null ? "" : ShowCanhBanerLoadNews());
            ViewBag.ShowBanerLoadNews = MoreAll.MoreAll.GetCache("ShowBanerLoadNews", System.Web.HttpContext.Current.Cache["ShowBanerLoadNews"] != null ? "" : ShowBanerLoadNews());
            ViewBag.ShowBanChay = MoreAll.MoreAll.GetCache("ShowBanChay", System.Web.HttpContext.Current.Cache["ShowBanChay"] != null ? "" : ShowBanChay());
            ViewBag.SanPhamMoi = MoreAll.MoreAll.GetCache("SanPhamMoi", System.Web.HttpContext.Current.Cache["SanPhamMoi"] != null ? "" : SanPhamMoi());
            ViewBag.SanPhamXemNhieu = MoreAll.MoreAll.GetCache("SanPhamXemNhieu", System.Web.HttpContext.Current.Cache["SanPhamXemNhieu"] != null ? "" : SanPhamXemNhieu());
            return View();
        }

        #region CacheHtml
        public ActionResult CacheHtml()
        {
            List<Entity.Category_News> table = SNews.Name_Text_Rg("SELECT inid,Alt,TangName,Title,Images,ImagesSmall,Brief,Create_Date FROM [News] WHERE new=1 AND lang='" + language + "'  AND Status=1  order by Create_Date desc");
            if (table.Count >= 1)
            {
                // Cách 2  Ko có  Cache
                ViewBag.Category = table;
            }
            // Cách 2  chạy có Cache Dabase
            ViewBag.data = CacheDatabase.NewsCategory(table, "CacheHtml");
            return View();
        }
        #endregion


        protected string ShowMenuPro()
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.capp_Lang_Parent_ID_Status(More.PR, language, "-1", "1");
            if (dt.Count > 0)
            {
                foreach (Entity.Menu item in dt)
                {
                    str += "<li class=\"nav_item lv1\"><a href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + MenuSupicon(item.ID.ToString()) + "</a>";
                    str += MenuSup(item.ID.ToString());
                    str += "</li>";
                }
            }
            return str.ToString();
        }
        protected string MenuSup(string id)
        {
            string str = "";

            List<Entity.Menu> dt = SMenu.capp_Lang_Parent_ID_Status(More.PR, language, id, "1");
            if (dt.Count > 0)
            {
                str += "<ul class=\"ul_content_right_1\">";
                foreach (Entity.Menu item in dt)
                {
                    str += "<li class=\"nav_item lv2\"><a  href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a>" + MenuSup(item.ID.ToString()) + "</li>";
                }
                str += "</ul>";
            }
            return str.ToString();
        }
        protected string MenuSupicon(string id)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.capp_Lang_Parent_ID_Status(More.PR, language, id, "1");
            if (dt.Count > 0)
            {
                str += "<i class=\"fa fa-angle-right\"></i>";
            }
            return str.ToString();
        }
        protected string LoadNews()// tin mới
        {
            string str = "";
            List<Entity.Category_News> table = SNews.Name_Text_Rg("SELECT inid,Alt,TangName,Title,Images,ImagesSmall,Brief,Create_Date FROM [News] WHERE new=1 AND lang='" + language + "'  AND Status=1  order by Create_Date desc");
            if (table.Count >= 1)
            {
                str += Commond.LoadNewsListHome(table, language);
            }
            return str;
        }
        protected string ShowCanhBanerLoadNews()//Tin tức bên phải banner
        {
            string str = "";
            List<Entity.Category_News> table = SNews.Name_Text_Rg("SELECT top 5 inid,Alt,TangName,Title,Images,ImagesSmall,Brief,Create_Date FROM [News] WHERE CheckBox2=1 AND lang='" + language + "'  AND Status=1  order by Create_Date desc");
            if (table.Count >= 1)
            {
                str += Commond.LoadNewsListHome_CanhBanner(table, language);
            }
            return str;
        }
        protected string ShowBanerLoadNews()//Tin tức bên phải banner
        {
            string str = "";
            List<Entity.Category_News> table = SNews.Name_Text_Rg("SELECT top 1 inid,Alt,TangName,Title,Images,ImagesSmall,Brief,Create_Date FROM [News] WHERE CheckBox1=1 AND lang='" + language + "'  AND Status=1  order by Create_Date desc");
            if (table.Count >= 1)
            {
                str += Commond.LoadNewsListHome_Banner(table, language);
            }
            return str;
        }
        protected string ShowNhomsanpham()
        {
            string str = "";
            List<Entity.MenuShow> dt = SMenu.Pages_Home(More.PR, language, "1");
            if (dt.Count > 0)
            {
                foreach (Entity.MenuShow item in dt)
                {
                    str += "<section class=\"awe-section-2\">";
                    str += "<section class=\"section_product_loopp loop_one\">";
                    str += "<div class=\"container\">";
                    str += "<div class=\"row\">";
                    str += "<div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12 dear_title\">";
                    str += "<h2 class=\"title_head_ border_content\">";
                    str += "<a href=\"/danh-muc/" + item.TangName + ".html\" title=\"" + item.Name + "\">" + item.Name + "</a>";
                    str += "</h2>";
                    str += "</div>";
                    str += "<div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12 wrap_owl_product_ margin-top-30\">";
                    str += "<div class=\"owl_product_ owl-carousel\" data-nav=\"true\" data-lg-items=\"5\" data-md-items=\"4\" data-height=\"false\" data-xs-items=\"2\" data-sm-items=\"3\" data-margin=\"0\">";
                    List<Entity.Category_Product> table = SProducts.GetTopProductInCategory(Commond.HomePage(), item.ID.ToString(), More.Sub_Menu(More.PR, item.ID.ToString()));
                    if (table.Count >= 1)
                    {
                        str += Commond.LoadProductList_Home(table);
                    }
                    str += "</div>";
                    str += "</div>";
                    str += "</div>";
                    str += "</div>";
                    str += "</section>";
                    str += "</section>";
                    str += Ad_vertisings.TheoNhom(item.ID.ToString());

                }
            }
            return str;
        }
        protected string SanPhamMoi()
        {
            string str = "";
            string strb = "";
            List<Entity.Category_Product> table = SProducts.Name_Text_Rg("select Alt,ipid,icid,TangName,Name,Images,ImagesSmall,Brief,Create_Date,Price,OldPrice,ID_Hang,sanxuat,Code from  products where  News=1 and lang='" + language + "' and Status=1  order by Create_Date desc");
            if (table.Count >= 1)
            {
                for (int i = 0; i < table.Count; i++)
                {
                    strb += "  <div class=\"hot_sale_product\">";
                    strb += "    <div class=\"item-img-horizontal\">";
                    strb += " <a class=\"product-image\"  href='/san-pham/" + table[i].TangName + ".html' title=\"" + table[i].Name + "\">" + AllQuery.MorePro.Image_Title_Alts(table[i].ImagesSmall.ToString(), table[i].Name.ToString(), table[i].Alt.ToString()) + "</a>";
                    strb += "    </div>";
                    strb += "    <div class=\"item-info-horizontal\">";

                    strb += "      <h3 class=\"item-name text2line\">";
                    strb += "<a class=\"text2line\" href='/san-pham/" + table[i].TangName + ".html' title=\"" + table[i].Name + "\">" + AllQuery.MorePro.Substring_Title(table[i].Name.ToString()) + "</a>";
                    strb += "      </h3>";
                    strb += "      <div class=\"price-box clearfix\">";
                    strb += "        <span class=\"price product-price\">" + AllQuery.MorePro.FormatMoney(table[i].Price.ToString()) + "</span>";
                    strb += "        <span class=\"price product-price-old\">" + AllQuery.MorePro.Detail_Price(table[i].OldPrice.ToString()) + "</span>";
                    strb += "      </div>";
                    strb += "    </div>";
                    strb += "  </div>";
                    if ((i + 1) % 4 == 0)
                    {
                        strb = "<div class=\"item\">" + strb + "</div>";
                        str += strb.ToString();
                        strb = "";
                    }
                    else if (i == (table.Count - 1))
                    {
                        strb = "<div class=\"item\">" + strb + "</div>";
                        str += strb.ToString();
                    }
                }
            }
            return str;
        }
        protected string ShowBanChay()
        {
            string str = "";
            string strb = "";
            List<Entity.Category_Product> table = SProducts.Name_Text_Rg("select Alt,ipid,icid,TangName,Name,Images,ImagesSmall,Brief,Create_Date,Price,OldPrice,ID_Hang,sanxuat,Code from  products where  Check_01=1 and lang='" + language + "' and Status=1  order by Create_Date desc");
            if (table.Count >= 1)
            {
                for (int i = 0; i < table.Count; i++)
                {
                    strb += "  <div class=\"hot_sale_product\">";
                    strb += "    <div class=\"item-img-horizontal\">";
                    strb += " <a class=\"product-image\"  href='/san-pham/" + table[i].TangName + ".html' title=\"" + table[i].Name + "\">" + AllQuery.MorePro.Image_Title_Alts(table[i].ImagesSmall.ToString(), table[i].Name.ToString(), table[i].Alt.ToString()) + "</a>";
                    strb += "    </div>";
                    strb += "    <div class=\"item-info-horizontal\">";

                    strb += "      <h3 class=\"item-name text2line\">";
                    strb += "<a class=\"text2line\" href='/san-pham/" + table[i].TangName + ".html' title=\"" + table[i].Name + "\">" + AllQuery.MorePro.Substring_Title(table[i].Name.ToString()) + "</a>";
                    strb += "      </h3>";
                    strb += "      <div class=\"price-box clearfix\">";
                    strb += "        <span class=\"price product-price\">" + AllQuery.MorePro.FormatMoney(table[i].Price.ToString()) + "</span>";
                    strb += "        <span class=\"price product-price-old\">" + AllQuery.MorePro.Detail_Price(table[i].OldPrice.ToString()) + "</span>";
                    strb += "      </div>";
                    strb += "    </div>";
                    strb += "  </div>";
                    if ((i + 1) % 4 == 0)
                    {
                        strb = "<div class=\"item\">" + strb + "</div>";
                        str += strb.ToString();
                        strb = "";
                    }
                    else if (i == (table.Count - 1))
                    {
                        strb = "<div class=\"item\">" + strb + "</div>";
                        str += strb.ToString();
                    }
                }
            }
            return str;
        }
        protected string SanPhamXemNhieu()
        {
            string str = "";
            string strb = "";
            List<Entity.Category_Product> table = SProducts.Name_Text_Rg("select Alt,ipid,icid,TangName,Name,Images,ImagesSmall,Brief,Create_Date,Price,OldPrice,ID_Hang,sanxuat,Code from  products where  Check_02=1 and lang='" + language + "' and Status=1  order by Create_Date desc");
            if (table.Count >= 1)
            {
                for (int i = 0; i < table.Count; i++)
                {
                    strb += "  <div class=\"hot_sale_product\">";
                    strb += "    <div class=\"item-img-horizontal\">";
                    strb += " <a class=\"product-image\"  href='/san-pham/" + table[i].TangName + ".html' title=\"" + table[i].Name + "\">" + AllQuery.MorePro.Image_Title_Alts(table[i].ImagesSmall.ToString(), table[i].Name.ToString(), table[i].Alt.ToString()) + "</a>";
                    strb += "    </div>";
                    strb += "    <div class=\"item-info-horizontal\">";
                    strb += "      <h3 class=\"item-name text2line\">";
                    strb += "<a class=\"text2line\" href='/san-pham/" + table[i].TangName + ".html' title=\"" + table[i].Name + "\">" + AllQuery.MorePro.Substring_Title(table[i].Name.ToString()) + "</a>";
                    strb += "      </h3>";
                    strb += "      <div class=\"price-box clearfix\">";
                    strb += "        <span class=\"price product-price\">" + AllQuery.MorePro.FormatMoney(table[i].Price.ToString()) + "</span>";
                    strb += "        <span class=\"price product-price-old\">" + AllQuery.MorePro.Detail_Price(table[i].OldPrice.ToString()) + "</span>";
                    strb += "      </div>";
                    strb += "    </div>";
                    strb += "  </div>";
                    if ((i + 1) % 4 == 0)
                    {
                        strb = "<div class=\"item\">" + strb + "</div>";
                        str += strb.ToString();
                        strb = "";
                    }
                    else if (i == (table.Count - 1))
                    {
                        strb = "<div class=\"item\">" + strb + "</div>";
                        str += strb.ToString();
                    }
                }
            }
            return str;
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
        #endregion

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = DateTime.Now;
            return View();
        }

        //   [HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Contact(FormCollection collect)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        DatalinqDataContext db = new DatalinqDataContext();
        //        Contact obj = new Contact();
        //        obj.vname = collect["txtHoTen"];
        //        obj.vaddress = collect["txtDiaChi"];
        //        db.Contacts.InsertOnSubmit(obj);
        //        db.SubmitChanges();
        //        ViewBag.ThongBao = "Thêm thành công";
        //    }
        //    return View();
        //}


        public ActionResult InfoAction()
        {
            return PartialView("_VoIPSubform1");

        }
        public ActionResult NotesButton()
        {
            return PartialView("_VoIPSubform2");

        }
        public ActionResult OrdersButton()
        {
            return PartialView("_VoIPSubform1");
        }

        public ActionResult DemoRadiobuotom()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetRegisterForm(string RegesterHere)
        {
            if (RegesterHere == "Individual")
            {
                return PartialView("IndividualPartialViewName");
            }
            else
            {
                return PartialView("BusinessPartialViewName");
            }

        }

        [HttpGet]
        public ActionResult XuatExel()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult XuatExel(FormCollection collect)
        {
            GenerateOrder(int.Parse("103"));
            return View();
        }

        #region Export to Excel

        // phải truyền orderId vào nhé, đây chỉ là demo thôi
        private string GenerateOrder(int orderId)
        {
            var folderReport = "/Uploads/Reports/";
            string filePath = System.Web.HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            // Template File
            string templateDocument = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/Templates/OrderTemplate.xlsx");
            string documentName = string.Format("Order-{0}-{1}.xlsx", orderId, DateTime.Now.ToString("yyyyMMddhhmmsss"));
            string fullPath = Path.Combine(filePath, documentName);
            // Results Output
            MemoryStream output = new MemoryStream();
            // Read Template
            using (FileStream templateDocumentStream = System.IO.File.OpenRead(templateDocument))
            {
                // Create Excel EPPlus Package based on template stream
                using (ExcelPackage package = new ExcelPackage(templateDocumentStream))
                {
                    // Grab the sheet with the template, sheet name is "BOL".
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Order"];
                    // Data Acces, load order header data.

                    LCart order = db.LCarts.SingleOrDefault(p => p.ID == orderId);
                    // Insert customer data into template
                    sheet.Cells[4, 1].Value = "Tên khách hàng: " + order.Name;
                    sheet.Cells[5, 1].Value = "Địa chỉ: " + order.Address;
                    sheet.Cells[6, 1].Value = "Điện thoại: " + order.Phone;
                    // Start Row for Detail Rows
                    int rowIndex = 9;

                    // load order details
                    var orderDetails = db.CartDetails.Where(s => s.ID_Cart == int.Parse(order.ID.ToString())).ToList();
                    int count = 1;
                    foreach (var orderDetail in orderDetails)
                    {
                        // Cell 1, Carton Count
                        sheet.Cells[rowIndex, 1].Value = count.ToString();
                        // Cell 2, Order Number (Outline around columns 2-7 make it look like 1 column)
                        sheet.Cells[rowIndex, 2].Value = orderDetail.ipid;
                        // Cell 8, Weight in LBS (convert KG to LBS, and rounding to whole number)
                        sheet.Cells[rowIndex, 3].Value = orderDetail.Quantity.ToString();

                        sheet.Cells[rowIndex, 4].Value = Convert.ToDouble(orderDetail.Price).ToString("N0");
                        sheet.Cells[rowIndex, 5].Value = (Convert.ToDouble(orderDetail.Price) * Convert.ToDouble(orderDetail.Quantity)).ToString("N0");
                        // Increment Row Counter
                        rowIndex++;
                        count++;
                    }
                    double total = (double)(orderDetails.Sum(x => x.Quantity * x.Price));
                    sheet.Cells[24, 5].Value = total.ToString("N0");

                    var numberWord = "Thành tiền (viết bằng chữ): " + NumberHelper.ToString(total);
                    sheet.Cells[26, 1].Value = numberWord;
                    if (order.Create_Date.HasValue)
                    {
                        var date = order.Create_Date.Value;
                        sheet.Cells[28, 3].Value = "Ngày " + date.Day + " tháng " + date.Month + " năm " + date.Year;

                    }
                    package.SaveAs(new FileInfo(fullPath));
                }
                return documentName;
            }

        }
        #endregion


        #region Cache Database
        public ActionResult CacheDatabaseMVC()
        {
            var data = GetDanhMuc();
            return View(data);
        }
        public ActionResult DeleteCache()
        {
            //cách 1
            // MemoryCache.Default.Remove("DanhMuc");
            // cách 2
            Remove("DanhMuc");
            return View();
        }
        public List<Entity.Menu> GetDanhMuc()
        {
            List<Entity.Menu> result = new List<Entity.Menu>();
            var cache = MemoryCache.Default;
            if (cache.Get("DanhMuc") == null)
            {
                var cachePolicty = new CacheItemPolicy();
                cachePolicty.AbsoluteExpiration = DateTime.Now.AddHours(200);
                result = SMenu.GETBYALL("VIE");
                cache.Add("DanhMuc", result, cachePolicty);
            }
            else
            {
                result = (List<Entity.Menu>)cache.Get("DanhMuc");
            }
            return result;
        }
        public void Remove(string cacheKey)
        {
            var lstCaches = MemoryCache.Default.Where(x => x.Key.ToLower().Contains(cacheKey.ToLower())).ToList();
            for (int i = 0; i < lstCaches.Count; i++)
            {
                MemoryCache.Default.Remove(lstCaches[i].Key);
            }
        }
        #endregion


        //***********Phát triển API Gét Vận chuyển *************
        //https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shift/date
        // Token:4924ac11-a01e-11eb-8be2-c21e19fc6803

        public async Task<ActionResult> VanChuyen()
        {
            var data = await BaseApiClient.GetAsync<VanChuyen.Root>("/shiip/public-api/v2/shift/date");
            return View(data);
        }
        //[HttpPost]
        //public async Task<ActionResult> Contact(Contact request)
        //{
        //    if (!ModelState.IsValid)
        //        return View(request);

        //    var result = await CreateContact(request);
        //    if (result)
        //    {
        //        ViewBag.ThongBao = "Thêm mới sản phẩm thành công";
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ThongBao = "Thêm sản phẩm thất bại";
        //    return View(request);
        //}

        // Chạy Ok nhé, nhưng nhớ phải điền hết các trường ko là lỗi ko chạy dc.
        //[HttpPost]
        //public async Task<string> Contact(Contact request)
        //{
        //    if (request.dcreatedate == null)
        //    { }
        //    request.dcreatedate = DateTime.Now;
        //    request.lang = "VIE";
        //    request.istatus = 1;

        //    var json = JsonConvert.SerializeObject(request);
        //    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        //    HttpClientHandler handler = new HttpClientHandler();
        //    using (var client = new HttpClient(handler, false))
        //    {
        //        client.BaseAddress = new Uri("http://localhost:63137/");
        //        var response = await client.PostAsync("/api/Contact/Save", httpContent);
        //        return await response.Content.ReadAsStringAsync();
        //    }
        //}


        // Chạy Ok nhé, nhưng nhớ phải điền hết các trường ko là lỗi ko chạy dc.
        //[HttpPost]
        //public async Task<string> Contact(Contact request)
        //{
        //    if (request.dcreatedate == null)
        //    { }
        //    request.dcreatedate = DateTime.Now;
        //    request.lang = "VIE";
        //    request.istatus = 1;

        //    var json = JsonConvert.SerializeObject(request);
        //    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        //    HttpClientHandler handler = new HttpClientHandler();
        //    using (var client = new HttpClient(handler, false))
        //    {
        //        client.BaseAddress = new Uri("https://hungphong.com.vn");
        //        var response = await client.PostAsync("/api/Contact/Save", httpContent);
        //        return await response.Content.ReadAsStringAsync();
        //    }
        //}

        // Chạy Ok nhé, nhưng nhớ phải điền hết các trường ko là lỗi ko chạy dc.
        //[HttpPost]
        //public async Task<bool> Contact(Contact request)
        //{
        //    System.Web.HttpContext.Current.Session["Token"] = Commond.ConfigHelper.GetByKey("Token");
        //    var sessions = System.Web.HttpContext.Current.Session["Token"].ToString();
        //    HttpClientHandler handler = new HttpClientHandler();
        //    using (var client = new HttpClient(handler, false))
        //    {
        //        client.BaseAddress = new Uri("https://hungphong.com.vn");
        //        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        //        // client.DefaultRequestHeaders.Add("Token", sessions);
        //        request.vtitle = request.vtitle;
        //        request.vname = request.vname;
        //        request.vaddress = request.vaddress;
        //        request.vphone = request.vphone;
        //        request.vemail = "vemail";
        //        request.vcontent = "vcontent";
        //        request.dcreatedate = DateTime.Now;
        //        request.lang = "VIE";
        //        request.istatus = 1;
        //        var json = JsonConvert.SerializeObject(request);
        //        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        //        var response = await client.PostAsync("/api/Contact/Save", httpContent);
        //        return response.IsSuccessStatusCode;
        //    }
        //}

        // Chạy Ok nhé, nhưng nhớ phải điền hết các trường ko là lỗi ko chạy dc.
        //[HttpPost]
        //public async Task<string> Contact(TaoDonHang.Root request)
        //{
        //    System.Web.HttpContext.Current.Session["Token"] = Commond.ConfigHelper.GetByKey("Token");
        //    var sessions = System.Web.HttpContext.Current.Session["Token"].ToString();
        //    HttpClientHandler handler = new HttpClientHandler();
        //    using (var client = new HttpClient(handler, false))
        //    {
        //        client.BaseAddress = new Uri(Commond.ConfigHelper.GetByKey("BaseAddress"));
        //        //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", sessions);
        //        ////  client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
        //        //client.DefaultRequestHeaders.Add("Accept", "application/json");
        //        // // client.DefaultRequestHeaders.Add("Content-Type", "application/json");
        //        //  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
        //        //  client.DefaultRequestHeaders.Add("ShopId", "78703");
        //        // // client.DefaultRequestHeaders.Add("Token", sessions);

        //        // client.DefaultRequestHeaders.Add("Content-Type", "application/json;charset=UTF-8");
        //       // client.DefaultRequestHeaders.Accept.Clear();
        //        // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
        //       // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        //       // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ShopId", "78703");
        //       // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", sessions);

        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Add("ShopId", "78703");
        //        client.DefaultRequestHeaders.Add("Token", sessions);

        //        // client.DefaultRequestHeaders.Add("ShopId", "78703");
        //        // client.DefaultRequestHeaders.Add("Token", sessions);

        //        var checkoutRequest = new TaoDonHang.Category()
        //   {
        //       level1 = "Áo"
        //       // category.level1 = "Áo";
        //   };

        //        var pro = new List<Entity.Products>();
        //        pro = SProducts.Name_Text("select top 1 * from products");
        //        var orderDetails = new List<TaoDonHang.ItemsItem>();
        //        foreach (var item in pro)
        //        {
        //            orderDetails.Add(new TaoDonHang.ItemsItem()
        //            {
        //                name = item.Name,
        //                code = item.Code,
        //                quantity = item.ipid,
        //                price = 23300,
        //                length = 12,
        //                width = 12,
        //                height = 12,
        //                category = checkoutRequest
        //                //category
        //            });
        //        }
        //        request.payment_type_id = 2;
        //        request.note = "Tintest 123";
        //        request.required_note = "KHONGCHOXEMHANG";
        //        request.return_phone = "0332190458";
        //        request.return_address = "39 NTT";
        //        request.return_district_id = null;
        //        request.return_ward_code = "";
        //        request.client_order_code = "";
        //        request.to_name = "TinTest124";
        //        request.to_phone = "0987654321";
        //        request.to_address = "72 Thành Thái; Phường 14; Quận 10; Hồ Chí Minh; Vietnam";
        //        request.to_ward_code = "20308";
        //        request.to_district_id = 1444;
        //        request.cod_amount = 200000;
        //        request.content = "Theo New York Times";
        //        request.weight = 200;
        //        request.length = 1;
        //        request.width = 19;
        //        request.height = 10;
        //        request.pick_station_id = 1444;
        //        request.deliver_station_id = null;
        //        request.insurance_value = 10000000;
        //        request.service_id = 0;
        //        request.service_type_id = 2;
        //        request.order_value = 130000;
        //        request.coupon = null;
        //        request.pick_shift = "[2]";

        //        request.item = orderDetails;

        //        ////   var strContent = JsonConvert.SerializeObject(request,
        //        //                new JsonSerializerSettings
        //        //                {
        //        //                    ContractResolver = new CamelCasePropertyNamesContractResolver()
        //        //                });

        //        var json = JsonConvert.SerializeObject(request);
        //        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        //        var response = await client.PostAsync("/shiip/public-api/v2/shipping-order/create", httpContent);
        //        ViewBag.ThongBao = response.IsSuccessStatusCode;
        //        return "";
        //    }
        //}


        // Test thử đã Ok bên giaohangtietkiem.vn
        [HttpPost]
        public async Task<string> Contact(VanChuyenGiaoHangTK.Root request)
        {
            //System.Web.HttpContext.Current.Session["Token"] = Commond.ConfigHelper.GetByKey("Token");
            //var sessions = System.Web.HttpContext.Current.Session["Token"].ToString();
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
                      id = "123ffg677",
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
                request.products = orderDetails;
                request.order = Orders;

                var json = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                // /Chú ý: sau khi gen đến đây thì lấy dữ liệu của request  rồi lại cho lên trang http://json.parser.online.fr/ fomat
                // sau khi fomat xong thì lại coppy vào posmant để chạy thử xem nó báo lỗi j
                var response = await client.PostAsync("/services/shipment/order/?ver=1.5", httpContent);
                ViewBag.ThongBao = response.IsSuccessStatusCode;
                return "";
            }
        }


        public ActionResult GetProductInfo()
        {
            List<RootGetIDPro> list = new List<RootGetIDPro>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.phanmembanhang.com/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJob3NjbyIsImF6cCI6ImFkbWluIiwianRpIjoiNTZkNjkwNDctNmI1Yi00NWJkLTk5M2YtMWRhYWI4MzBmMTFkIiwibmFtZWlkIjoiMSIsInNpZCI6IiIsIm5iZiI6MTYyNjg0OTQ4MSwiZXhwIjoxNjMyMDMzNDgxLCJpc3MiOiJob3Njby52biIsImF1ZCI6Imhvc2NvLnZuIn0.3DxbhbROU-s7izt5-xQy72Y5xIkkRVT1iEagPWtYSSI");
                var result = client.GetAsync("/api/Category/GetProductInfo/ef0f1f5b-ef0f-415a-8db8-59c297f3b706").Result;
                if (result.IsSuccessStatusCode)
                {
                    //  list = result.Content.ReadAsAsync<List<RootGetIDPro>>().Result;

                    var p = "\"{\\\"data\\\":{\\\"Id\\\":\\\"ef0f1f5b-ef0f-415a-8db8-59c297f3b706\\\",\\\"Name\\\":\\\"loa 2\\\",\\\"ProductCode\\\":\\\"loa2\\\",\\\"Unit\\\":\\\"c\\\",\\\"UnitPrice\\\":2000.0000,\\\"Price\\\":3000.0000,\\\"InStock\\\":4.3,\\\"MinInStock\\\":0.0,\\\"MaxInStock\\\":0.0,\\\"BranchName\\\":\\\"GIADUNG\\\",\\\"GroupName\\\":\\\"AM THANH HLoV\\\",\\\"GroupId\\\":\\\"f5d8084c-9d0b-4b60-b685-03bf801c38da\\\",\\\"Description\\\":\\\"\\\",\\\"s_Specification\\\":\\\"\\\",\\\"s_Quantification\\\":\\\"\\\",\\\"s_Color\\\":\\\"\\\",\\\"s_TacDungChinh\\\":\\\"\\\",\\\"s_NoteImport\\\":\\\"\\\",\\\"s_NoteOrder\\\":\\\"\\\",\\\"s_ThanhPhan\\\":\\\"\\\",\\\"ImageUrls\\\":\\\"\\\",\\\"ExtraLabels\\\":\\\"\\\",\\\"ExtraValues\\\":\\\"\\\",\\\"Picture\\\":null,\\\"Unit_ID\\\":null,\\\"f_Convert\\\":0.0},\\\"meta\\\":{\\\"status_code\\\":0,\\\"message\\\":\\\"Successfully\\\"}}\"";// result.Content.ReadAsStringAsync();
                    RootGetIDPro lists = JsonConvert.DeserializeObject<RootGetIDPro>(p);
                    return View();
                }
            }
            return View();
        }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class DataGetIDPro
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string ProductCode { get; set; }
            public string Unit { get; set; }
            public int UnitPrice { get; set; }
            public int Price { get; set; }
            public double InStock { get; set; }
            public int MinInStock { get; set; }
            public int MaxInStock { get; set; }
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
            public string ImageUrls { get; set; }
            public string ExtraLabels { get; set; }
            public string ExtraValues { get; set; }
            public object Picture { get; set; }
            public object Unit_ID { get; set; }
            public int f_Convert { get; set; }
        }

        public class MetaGetIDPro
        {
            public int status_code { get; set; }
            public string message { get; set; }
        }

        public class RootGetIDPro
        {
            public DataGetIDPro data { get; set; }
            public MetaGetIDPro meta { get; set; }
        }



    }
}