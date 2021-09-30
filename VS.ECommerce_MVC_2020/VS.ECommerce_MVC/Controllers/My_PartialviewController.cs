using Advertisings;
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
        public ActionResult AutoRefreshCart()
        {
            return PartialView();
        }
        public ActionResult AutoRefreshHeaderCart()
        {
            return PartialView();
        }
        public ActionResult Header()
        {
            return PartialView();
        }
        public ActionResult Footer()
        {
            //ViewBag.GioLamViec = MoreAll.MoreAll.GetCache("GioLamViec", System.Web.HttpContext.Current.Cache["GioLamViec"] != null ? "" : Ad_vertisings.GioLamViec("8"));

            return PartialView();
        }
        public ActionResult Lefmenu()
        {
            //ViewBag.ShowMenuSP = ShowMenuHeader();

            return PartialView();
        }
        protected string ShowMenuHeader()
        {
            string str = "";
            List<Entity.Menu_OK> table = SMenu.Name_Text_Rg("SELECT capp,Create_Date,Description,Equals,ID,Images,Lang,Level,Link,Module,Name,Orders,Parent_ID,ShowID,Styleshow,TangName,Type,Url_Name,Views FROM Menu where capp='" + More.MN + "' and lang='" + language + "' and Views=3 and status=1 order by level,Orders asc");//Views là vị trí menu top
            table = table.Where(s => s.Level.Length == 5).ToList();
            if (table.Count > 0)
            {
                for (int i = 0; i < table.Count; i++)
                {
                    string Link = "";
                    if (table[i].ShowID == 2)// dạng nội dung=2
                    {
                        Link = "/page/" + table[i].TangName + ".html";
                    }
                    else if (table[i].ShowID == 3)// dạng link=3
                    {
                        Link = table[i].Link;
                    }
                    else//Trang liên kết =1
                    {
                        if (table[i].Link == "/")
                        {
                            Link = table[i].Link;
                        }
                        else
                        {
                            Link = table[i].Link;
                        }
                    }
                    str += "<li  class=\"nav-item  lv1\">";
                    str += " <a class=\"nav-link\" style=\"margin-right:15px\" href='" + Link + "'>" + table[i].Name.ToString() + "</a> " + Menu_Pro(table[i].Level.ToString()) + "";
                    str += "</li>";
                }
            }
            return (str);
        }

        protected string Menu_Pro(string id)
        {
            string str = "";
            List<Entity.Menu_OK> dt = SMenu.Name_Text_Rg("SELECT capp,Create_Date,Description,Equals,ID,Images,Lang,Level,Link,Module,Name,Orders,Parent_ID,ShowID,Styleshow,TangName,Type,Url_Name,Views FROM Menu where capp='" + More.MN + "' and lang='" + language + "' and Views=3 and status=1 and  len([Level]) >= 10 and [Level] like '" + id + "%'  order by level,Orders asc");//Views là vị trí menu top
            if (dt.Count > 0)
            {
                str += "<i class=\"fa fa-caret-down\"></i><ul class=\"dropdown-menu\">";
                foreach (Entity.Menu_OK item in dt)
                {
                    string Link = "";
                    if (item.ShowID == 2)// dạng nội dung=2
                    {
                        Link = "/page/" + item.TangName + ".html";
                    }
                    else if (item.ShowID == 3)// dạng link=3
                    {
                        Link = item.Link;
                    }
                    else//Trang liên kết =1
                    {
                        if (item.Link == "/")
                        {
                            Link = item.Link;
                        }
                        else
                        {
                            Link = item.Link;
                        }
                    }

                    str += "<li><a href='" + Link + "'>" + item.Name.ToString() + "</a>" + Menu_Pro(item.ID.ToString()) + "</li>";
                }
                str += "</ul>";
            }
            return str.ToString();
        }
        public ActionResult LefmenuNews()
        {
            return PartialView();
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
					#region _cid
                    try
                    {
                        int iEmptyIndex = 0;
                        if (_cid == 0)
                        {
                            iEmptyIndex = hp.IndexOf("?");
                            if (iEmptyIndex != -1)
                            {
                                hp = hp.Substring(0, iEmptyIndex);
                                _cid = Convert.ToInt32(More.TangNameicid(hp));
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    #endregion
                    strReturn += "<li><a href=\"/tin-tuc-new.html\">" + Commond.label("l_news") + "</a></li>";
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
					#region _cid
                    try
                    {
                        int iEmptyIndex = 0;
                        if (_cid == 0)
                        {
                            iEmptyIndex = hp.IndexOf("?");
                            if (iEmptyIndex != -1)
                            {
                                hp = hp.Substring(0, iEmptyIndex);
                                _cid = Convert.ToInt32(More.TangNameicid(hp));
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    #endregion
                    strReturn += "<li><a href=\"/thu-vien-anh.html\">" + Commond.label("Thuvienanh") + "</a></li>";
                    strReturn += LoadNav("danh-muc-anh", _cid);
                }
            }
            if (Request.RawUrl.Contains("/danh-muc-video/"))
            {
                if (int.TryParse(More.TangNameicid(hp), out _cid))
                {
					#region _cid
                    try
                    {
                        int iEmptyIndex = 0;
                        if (_cid == 0)
                        {
                            iEmptyIndex = hp.IndexOf("?");
                            if (iEmptyIndex != -1)
                            {
                                hp = hp.Substring(0, iEmptyIndex);
                                _cid = Convert.ToInt32(More.TangNameicid(hp));
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    #endregion
                    strReturn += "<li><a href=\"/thu-vien-video.html\">" + Commond.label("Thuvienvideo") + "</a></li>";
                    strReturn += LoadNav("danh-muc-video", _cid);
                }
            }
            if (Request.RawUrl.Contains("/danh-muc/"))
            {
                if (int.TryParse(More.TangNameicid(hp), out _cid))
                {
					#region _cid
                    try
                    {
                        int iEmptyIndex = 0;
                        if (_cid == 0)
                        {
                            iEmptyIndex = hp.IndexOf("?");
                            if (iEmptyIndex != -1)
                            {
                                hp = hp.Substring(0, iEmptyIndex);
                                _cid = Convert.ToInt32(More.TangNameicid(hp));
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    #endregion
                    strReturn += "<li><a href=\"/san-pham-news.html\">" + Commond.label("lproducts") + "</a></li>";
                    strReturn += LoadNav("danh-muc", _cid);
                }
            }
            #endregion

            //// Chi tiet
            #region Chi tiet
            if (Request.RawUrl.Contains("/tin-tuc/"))
            {
                strReturn += "<li><a href=\"/tin-tuc-new.html\">" + Commond.label("l_news") + "</a></li>";
                strReturn += LoadNavNews("danh-muc-tin", hp);
            }
            //case "4":
            //    strReturn += LoadNavNewsFooter(hp);
            //    break;
            if (Request.RawUrl.Contains("/album/"))
            {
                strReturn += "<li><a href=\"/thu-vien-anh.html\">" + Commond.label("Thuvienanh") + "</a></li>";
                strReturn += LoadNavAllbums("danh-muc-anh", hp);
            }
            if (Request.RawUrl.Contains("/video/"))
            {
                strReturn += "<li><a href=\"/thu-vien-video.html\">" + Commond.label("Thuvienvideo") + "</a></li>";
                strReturn += LoadNavVideos("danh-muc-video", hp);
            }
            if (Request.RawUrl.Contains("/san-pham/"))
            {
                 strReturn += "<a href=\"/san-pham-news.html\">" + Commond.label("lproducts") + "</a>";
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
                nav = "<span class=\"mr_lr\">&nbsp;/&nbsp;</span><li><a href=\"/" + url + "/" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
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
                nav = "<span class=\"mr_lr\">&nbsp;/&nbsp;</span><li><a href=\"/" + url + "/" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
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
                nav = "<span class=\"mr_lr\">&nbsp;/&nbsp;</span><li><a href=\"/" + url + "/" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
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
                nav = "<span class=\"mr_lr\">&nbsp;/&nbsp;</span><li><a href=\"/" + url + "/" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
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
                nav = "<span class=\"mr_lr\">&nbsp;/&nbsp;</span><li><a href=\"/" + url + "/" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
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
                nav = "<span class=\"mr_lr\">&nbsp;/&nbsp;</span><li><a href=\"/" + url + "/" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
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
                nav = "<span class=\"mr_lr\">&nbsp;/&nbsp;</span><li><a href=\"/page/" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
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