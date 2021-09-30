using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VS.ECommerce_MVC.Controllers
{
    public class My_PartialviewController : Controller
    {
        //
        // GET: /My_Partialview/
        string hp = "";
        string nav = "";
        string u = "";
        int _cid = -1;
        DatalinqDataContext db = new DatalinqDataContext();
        private string language = Captionlanguage.SetLanguage();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RefreshHeaderCart()
        {
            return PartialView();
        }

        public ActionResult AutoRefreshXemNhanh()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AutoRefreshXemNhanh(string id)
        {
            product dt = db.products.SingleOrDefault(p => p.ipid == int.Parse(id));
            if (dt != null)
            {
                string cid = dt.icid.ToString();
                string pid = dt.ipid.ToString();
                ViewBag.Hang = More.Name(dt.ID_Hang.ToString());


                return PartialView(dt);
            }
            return PartialView();
        }

        public ActionResult AutoRefreshMuaThemNhieu()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AutoRefreshMuaThemNhieu(string id, string sl)
        {
            if (id != "")
            {
                List<Entity.Products> dt = SProducts.GetById(id);
                if (dt.Count > 0)
                {
                    SessionCarts.ShoppingCart_AddProduct_Sesion(id.ToString(), Convert.ToInt32(sl), "0", "0");
                }
            }

            string str = "";
            if (Session["cart"] != null)
            {
                System.Data.DataTable dtcart = (System.Data.DataTable)Session["cart"];
                if (dtcart.Rows.Count > 0)
                {
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        if (dtcart.Rows[i]["PID"].ToString() == id)
                        {
                            str += " <div class=\"modal-body\">";
                            str += "<div class=\"media\">";
                            str += " <div class=\"media-left\">";
                            str += "<div class=\"thumb-1x1\">";
                            str += " <img src=\"" + dtcart.Rows[i]["vimg"].ToString() + "\" alt=\"" + dtcart.Rows[i]["Name"].ToString() + "\">";
                            str += " </div>";
                            str += " </div>";
                            str += " <div class=\"media-body\">";
                            str += "<div class=\"product-title\">";
                            str += " <a href=\"#\" title=\"" + dtcart.Rows[i]["Name"].ToString() + "\">" + dtcart.Rows[i]["Name"].ToString() + "</a>";
                            str += " </div>";
                            str += "<div class=\"qty\">";
                            str += "  Số lượng: <span>" + dtcart.Rows[i]["Quantity"].ToString() + "</span>";
                            str += "   </div>";
                            str += "<div class=\"product-new-price\">";
                            str += "  Giá: <span>" + dtcart.Rows[i]["Price"].ToString() + "₫</span>";
                            str += " </div>";
                            str += " </div>";
                            str += " </div>";
                            str += " </div>";
                        }
                    }
                }
            }
            ViewBag.ThongTin = str;
            return PartialView();
        }
         public ActionResult AutoRefreshMuaThem()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AutoRefreshMuaThem(string id)
        {
            if (id != "")
            {
                List<Entity.Products> dt = SProducts.GetById(id);
                if (dt.Count > 0)
                {
                    SessionCarts.ShoppingCart_AddProduct_Sesion(id.ToString(), Convert.ToInt32("1"), "0", "0");
                }
            }

            string str = "";
            if (Session["cart"] != null)
            {
                System.Data.DataTable dtcart = (System.Data.DataTable)Session["cart"];
                if (dtcart.Rows.Count > 0)
                {
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        if (dtcart.Rows[i]["PID"].ToString() == id)
                        {
                            str += " <div class=\"modal-body\">";
                            str += "<div class=\"media\">";
                            str += " <div class=\"media-left\">";
                            str += "<div class=\"thumb-1x1\">";
                            str += " <img src=\"" + dtcart.Rows[i]["vimg"].ToString() + "\" alt=\"" + dtcart.Rows[i]["Name"].ToString() + "\">";
                            str += " </div>";
                            str += " </div>";
                            str += " <div class=\"media-body\">";
                            str += "<div class=\"product-title\">";
                            str += " <a href=\"/products/ong-nhua-dung-ly\" title=\"" + dtcart.Rows[i]["Name"].ToString() + "\">" + dtcart.Rows[i]["Name"].ToString() + "</a>";
                            str += " </div>";
                            str += "<div class=\"qty\">";
                            str += "  Số lượng: <span>" + dtcart.Rows[i]["Quantity"].ToString() + "</span>";
                            str += "   </div>";
                            str += "<div class=\"product-new-price\">";
                            str += "  Giá: <span>" + dtcart.Rows[i]["Price"].ToString() + "₫</span>";
                            str += " </div>";
                            str += " </div>";
                            str += " </div>";
                            str += " </div>";
                        }
                    }
                }
            }
            ViewBag.ThongTin = str;
            return PartialView();
        }



        public ActionResult Header()
        {
            return PartialView();
        }
        public ActionResult Footer()
        {
            return PartialView();
        }
        public ActionResult Lefmenu()
        {
            ViewBag.ThuongHieu = ShowMenuTuongHieu();
            ViewBag.ShowMenuSP = ShowMenuSP();
            ViewBag.SanPhamBanChay = SanPhamBanChay();
            return PartialView();
        }

        public ActionResult LefmenuNews()
        {
            ViewBag.ThuongHieu = ShowMenuTuongHieu();
            ViewBag.ShowMNTintuc = Tintuc();
            ViewBag.SanPhamBanChay = SanPhamBanChay();
            ViewBag.LoadNewsNoiBat = LoadNewsNoiBat();
            return PartialView();
        }

        protected string LoadNewsNoiBat()// tin mới
        {
            string str = "";
            List<Entity.Category_News> table = SNews.Name_Text_Rg("SELECT inid,Alt,TangName,Title,Images,ImagesSmall,Brief,Create_Date FROM [News] WHERE new=1 AND lang='" + language + "'  AND Status=1  order by Create_Date desc");
            if (table.Count >= 1)
            {
                str += Commond.LoadNewsNoiBat(table, language);
            }
            return str;
        }

        protected string Tintuc()
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.capp_Lang_Parent_ID_Status(More.NS, language, "-1", "1");
            if (dt.Count > 0)
            {
                foreach (Entity.Menu item in dt)
                {
                    str += "<li class=\"nav-item\"> <i class=\"fa fa-arrow-circle-right\" aria-hidden=\"true\"></i> <a class=\"nav-link\" href=\"/danh-muc-tin/" + item.TangName.ToString() + ".html\">" + item.Name.ToString() + "</a> </li>";
                }
            }
            return str.ToString();
        }
        protected string ShowMenuSP()
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.capp_Lang_Parent_ID_Status(More.PR, language, "-1", "1");
            if (dt.Count > 0)
            {
                foreach (Entity.Menu item in dt)
                {
                    str += "<li class=\"nav-item\"> <i class=\"fa fa-arrow-circle-right\" aria-hidden=\"true\"></i> <a class=\"nav-link\" href=\"/danh-muc/" + item.TangName.ToString() + ".html\">" + item.Name.ToString() + "</a> </li>";
                }
            }
            return str.ToString();
        }

        protected string ShowMenuTuongHieu()
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.capp_Lang_Parent_ID_Status(More.HA, language, "-1", "1");
            if (dt.Count > 0)
            {
                foreach (Entity.Menu item in dt)
                {
                    str += "<li class=\"nav-item\"> <i class=\"fa fa-arrow-circle-right\" aria-hidden=\"true\"></i> <a class=\"nav-link\" href=\"/thuong-hieu/" + item.TangName.ToString() + ".html\">" + item.Name.ToString() + "</a> </li>";
                }
            }
            return str.ToString();
        }

        protected string SanPhamBanChay()
        {
            string str = "";
            string strb = "";
            List<Entity.Category_Product> table = SProducts.Name_Text_Rg("select top 5  Alt,ipid,icid,TangName,Name,Images,ImagesSmall,Brief,Create_Date,Price,OldPrice,ID_Hang,sanxuat,Code from  products where  News=1 and lang='" + language + "' and Status=1  order by Create_Date desc");
            if (table.Count >= 1)
            {
                for (int i = 0; i < table.Count; i++)
                {
                    str += "<div class=\"row row-noGutter\">";
                    str += "<div class=\"col-sm-12\">";
                    str += "<div class=\"product-mini-item clearfix  \">";
                    str += "<div class=\"product-img relative\">";

                    str += " <a class=\"product-banchay\"  href='/san-pham/" + table[i].TangName + ".html' title=\"" + table[i].Name + "\">" + MoreAll.MoreImage.Image_width_height_Title_Alt(table[i].ImagesSmall.ToString(), "87", "87", table[i].Name.ToString(), table[i].Alt.ToString()) + "</a>";

                    str += "</div>";
                    str += "<div class=\"product-info\">";
                    str += "<h3>";
                    str += "<a class=\"product-name\" href='/san-pham/" + table[i].TangName + ".html' title=\"" + table[i].Name + "\">" + AllQuery.MorePro.Substring_Title(table[i].Name.ToString()) + "</a>";
                    str += "</h3>";
                    str += "<div class=\"price-box\">";
                    str += "<div class=\"special-price\">";
                    str += "    <span class=\"price product-price\">" + AllQuery.MorePro.FormatMoney(table[i].Price.ToString()) + "</span>";
                    str += "</div>";

                    str += "</div>";
                    str += "</div>";
                    str += "</div>";
                    str += "</div>";
                    str += "</div>";

                }
            }
            return str;
        }


        #region Control Nav_conten
        public ActionResult Nav_conten()
        {
            ViewBag.ltrnav = ShowLoadNav_conten();
            return PartialView();
        }

        private string ShowLoadNav_conten()
        {
            string strReturn = "";
            strReturn += "";
            var curURL = Request.RawUrl;
            u = curURL.Substring(curURL.LastIndexOf("/") + 1);
            hp = u.Replace(".html", "");
            hp = Removelink.RemoveUrl(hp);

            #region Nhóm danh mục
            // nhóm
            if (Request.RawUrl.Contains("/page/"))
            {
                strReturn += LoadNavPage("page", hp);
            }
            if (Request.RawUrl.Contains("/danh-muc-tin/"))
            {
                if (int.TryParse(More.TangNameicid(hp), out _cid))
                {
                    strReturn += "<li><a href=\"/tin-tuc-new.html\">" + Commond.label("l_news") + "</a><span> <i class=\"fa fa-angle-right\"></i> </span> </li>";
                    strReturn += LoadNav("danh-muc-tin", _cid);
                }
            }
            //case "3":// nhom chan trang
            //    if (int.TryParse(More.TangNameicid(hp), out _cid))
            //    {
            //        strReturn += LoadNav(_cid);
            //    }
            //    break;
            if (Request.RawUrl.Contains("/danh-muc-anh/"))
            {
                if (int.TryParse(More.TangNameicid(hp), out _cid))
                {
                    strReturn += "<li><a href=\"/thu-vien-anh.html\">" + Commond.label("Thuvienanh") + "</a><span> <i class=\"fa fa-angle-right\"></i> </span> </li>";
                    strReturn += LoadNav("danh-muc-anh", _cid);
                }
            }
            if (Request.RawUrl.Contains("/danh-muc-video/"))
            {
                if (int.TryParse(More.TangNameicid(hp), out _cid))
                {
                    strReturn += "<li><a href=\"/thu-vien-video.html\">" + Commond.label("Thuvienvideo") + "</a><span> <i class=\"fa fa-angle-right\"></i> </span> </li>";
                    strReturn += LoadNav("danh-muc-video", _cid);
                }
            }
            if (Request.RawUrl.Contains("/danh-muc/"))
            {
                if (int.TryParse(More.TangNameicid(hp), out _cid))
                {
                    strReturn += "<li><a href=\"/san-pham-news.html\">" + Commond.label("lproducts") + "</a><span> <i class=\"fa fa-angle-right\"></i> </span> </li>";
                    strReturn += LoadNav("danh-muc", _cid);
                }
            }
            #endregion

            //// Chi tiet
            #region Chi tiet
            if (Request.RawUrl.Contains("/tin-tuc/"))
            {
                strReturn += "<li><a href=\"/tin-tuc-new.html\">" + Commond.label("l_news") + "</a><span> <i class=\"fa fa-angle-right\"></i> </span> </li>";
                strReturn += LoadNavNews("danh-muc-tin", hp);
            }
            //case "4":
            //    strReturn += LoadNavNewsFooter(hp);
            //    break;
            if (Request.RawUrl.Contains("/album/"))
            {
                strReturn += "<li><a href=\"/thu-vien-anh.html\">" + Commond.label("Thuvienanh") + "</a><span> <i class=\"fa fa-angle-right\"></i> </span> </li>";
                strReturn += LoadNavAllbums("danh-muc-anh", hp);
            }
            if (Request.RawUrl.Contains("/video/"))
            {
                strReturn += "<li><a href=\"/thu-vien-video.html\">" + Commond.label("Thuvienvideo") + "</a><span> <i class=\"fa fa-angle-right\"></i> </span> </li>";
                strReturn += LoadNavVideos("danh-muc-video", hp);
            }
            if (Request.RawUrl.Contains("/san-pham/"))
            {
                strReturn += "<li><a href=\"/san-pham.html\">" + Commond.label("lproducts") + "</a><span> <i class=\"fa fa-angle-right\"></i> </span> </li>";
                strReturn += LoadNavProduts("danh-muc", hp);
            }
            #endregion

            if (Request.RawUrl.Contains("gio-hang.html"))
            {
                strReturn += "<li><a href=\"/gio-hang.html\">" + Commond.label("lt_cartbox") + "</a></li>";
            }
            if (Request.RawUrl.Contains("dat-hang.html"))
            {
                strReturn += "<li><a href=\"/dat-hang.html\">" + Commond.label("lt_cartbox") + "</a></li>";
            }
            if (Request.RawUrl.Contains("Message-removecart.html"))
            {
                strReturn += "<li><a href=\"/Message-removecart.html\">" + Commond.label("lt_cartbox") + "</a></li>";
            }
            if (Request.RawUrl.Contains("Message-Ordersucess.html"))
            {
                strReturn += "<li><a href=\"/Message-Ordersucess.html\">" + Commond.label("lt_cartbox") + "</a></li>";
            }
            if (Request.RawUrl.Contains("lien-he.html"))
            {
                strReturn += "<li><a href=\"/lien-he.html\">" + Commond.label("l_contact") + "</a></li>";
            }
            if (Request.RawUrl.Contains("tin-tuc-new.html"))
            {
                strReturn += "<li><a href=\"/tin-tuc-new.html\">" + Commond.label("l_news") + "</a></li>";
            }
            if (Request.RawUrl.Contains("thu-vien-anh.html"))
            {
                strReturn += "<li><a href=\"/thu-vien-anh.html\">" + Commond.label("Thuvienanh") + "</a></li>";
            }
            if (Request.RawUrl.Contains("thu-vien-video.html"))
            {
                strReturn += "<li><a href=\"/thu-vien-video.html\">" + Commond.label("Thuvienvideo") + "</a></li>";
            }
            if (Request.RawUrl.Contains("san-pham-news.html"))
            {
                strReturn += "<li><a href=\"/san-pham-news.html\">" + Commond.label("lproducts") + "</a></li>";
            }
            if (Request.RawUrl.Contains("Search.html"))
            {
                strReturn += "<li><a href=\"#\">" + Commond.label("l_search") + "</a></li>";
            }
            if (Request.RawUrl.Contains("Dang-ky.html"))
            {
                strReturn += "<li><a href=\"/Dang-ky.html\">" + Commond.label("Thanhvien") + "</a></li>";
            }
            if (Request.RawUrl.Contains("Dang-nhap.html"))
            {
                strReturn += "<li><a href=\"/Dang-nhap.html\">" + Commond.label("l_login") + "</a></li>";
            }
            if (Request.RawUrl.Contains("doi-mat-khau.html"))
            {
                strReturn += "<li><a href=\"/doi-mat-khau.html\">" + Commond.label("lt_changepassword") + "</a></li>";
            }
            if (Request.RawUrl.Contains("ho-so-thanh-vien.html"))
            {
                strReturn += "<li><a href=\"/ho-so-thanh-vien.html\">" + Commond.label("ttthanhvien") + "</a></li>";
            }
            if (Request.RawUrl.Contains("Quen-mat-khau.html"))
            {
                strReturn += "<li><a href=\"/Quen-mat-khau.html\">" + Commond.label("thaydoimk") + "</a></li>";
            }
            if (Request.RawUrl.Contains("lich-su-mua-hang.html"))
            {
                strReturn += "<li><a href=\"/lich-su-mua-hang.html\">Lịch sử mua hàng</a></li>";
            }
            //if (Request["su"].ToString() == "Banchay")
            //{
            //    strReturn += "<li><a href=\"/san-pham-ban-chay.html\">" + Commond.label("l_prdbestsell") + "</a></li>";
            //}
            //if (Request["su"].ToString() == "sanphamoi")
            //{
            //    strReturn += "<li><a href=\"/san-pham-moi.html\">" + Commond.label("l_prdnews") + "</a></li>";
            //}
            return strReturn;
        }
        private string LoadNavNews(string url, string hp)
        {
            New dt = db.News.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.icid.ToString()));
                nav = "<li><a href=\"/" + url + "/" + item.TangName.ToString() + ".html\">" + item.Name + "</a><span> <i class=\"fa fa-angle-right\"></i> </span> </li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(url, Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }

        private string LoadNavNewsFooter(string url, string hp)
        {
            Nfooter dt = db.Nfooters.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.icid.ToString()));
                nav = "<li><a href=\"/" + url + "/" + item.TangName.ToString() + ".html\">" + item.Name + "</a><span> <i class=\"fa fa-angle-right\"></i> </span> </li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(url, Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }

        private string LoadNavAllbums(string url, string hp)
        {
            LAlbum dt = db.LAlbums.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.Menu_ID.ToString()));
                nav = "<li><a href=\"/" + url + "/" + item.TangName.ToString() + ".html\">" + item.Name + "</a><span> <i class=\"fa fa-angle-right\"></i> </span> </li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(url, Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }

        private string LoadNavVideos(string url, string hp)
        {
            VideoClip dt = db.VideoClips.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.Menu_ID.ToString()));
                nav = "<li><a href=\"/" + url + "/" + item.TangName.ToString() + ".html\">" + item.Name + "</a><span> <i class=\"fa fa-angle-right\"></i> </span> </li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(url, Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }

        private string LoadNavProduts(string url, string hp)
        {
            product dt = db.products.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.icid.ToString()));
                nav = "<li><a href=\"/" + url + "/" + item.TangName.ToString() + ".html\">" + item.Name + "</a><span> <i class=\"fa fa-angle-right\"></i> </span> </li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(url, Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }

        private string LoadNav(string url, int ID)
        {
            var item = db.Menus.FirstOrDefault(s => s.ID == ID);
            if (item != null)
            {
                nav = " <li><a href=\"/" + url + "/" + item.TangName.ToString() + ".html\">" + item.Name + "</a><span> <i class=\"fa fa-angle-right\"></i> </span> </li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(url, Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }
        private string LoadNavPage(string url, string TangName)
        {
            var item = db.Menus.FirstOrDefault(s => s.TangName == TangName & s.capp == More.MN);
            if (item != null)
            {
                nav = "<li><a href=\"/page/" + item.TangName.ToString() + ".html\">" + item.Name + "</a><span> <i class=\"fa fa-angle-right\"></i> </span> </li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(url, Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }
        #endregion

    }
}