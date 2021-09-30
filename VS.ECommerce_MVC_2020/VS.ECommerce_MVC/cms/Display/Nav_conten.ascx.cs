using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;

namespace VS.ECommerce_MVC.cms.Display
{
    public partial class Nav_conten : System.Web.UI.UserControl
    {
        string hp = "";
        int iEmptyIndex = 0;
        string nav = "";
        string u = "";
        int _cid = -1;
        DatalinqDataContext db = new DatalinqDataContext();
        private string language = Captionlanguage.Language;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["language"] != null)
            {
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["language"] = this.language;
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            #region Requesthp
            if (Request["hp"] != null && !Request["hp"].Equals(""))
            {
                hp = Request["hp"].ToString();
            }
            iEmptyIndex = hp.IndexOf("?");
            if (iEmptyIndex != -1)
            {
                hp = hp.Substring(0, iEmptyIndex);
            }
            #endregion
            try
            {
                if (Request["e"] != null)
                {
                    if (Request["e"].ToString() == "load")
                    {
                        u = Commond.RequestMenu(Request["hp"]);
                    }
                }
            }
            catch
            {
            }
            if (!IsPostBack)
            {
                Nav_Content();
            }
        }

        private void Nav_Content()
        {
            string strReturn = "";
            strReturn += "";
            switch (u)
            {
                case "99":
                    strReturn += LoadNavPage(hp);
                    break;
                case "1":// nhom tin tuc
                    if (int.TryParse(More.TangNameicid(hp), out _cid))
                    {
                        strReturn += "<li><a href=\"/tin-tuc-new.html\">" + label("l_news") + "</a></li>";
                        strReturn += LoadNav(_cid);
                    }
                    break;
                case "3":// nhom chan trang
                    if (int.TryParse(More.TangNameicid(hp), out _cid))
                    {
                        strReturn += LoadNav(_cid);
                    }
                    break;
                case "5":// nhom Album
                    if (int.TryParse(More.TangNameicid(hp), out _cid))
                    {
                        strReturn += "<li><a href=\"/thu-vien-anh.html\">" + label("Thuvienanh") + "</a></li>";
                        strReturn += LoadNav(_cid);
                    }
                    break;
                case "7":// nhom Video
                    if (int.TryParse(More.TangNameicid(hp), out _cid))
                    {
                        strReturn += "<li><a href=\"/thu-vien-video.html\">" + label("Thuvienvideo") + "</a></li>";
                        strReturn += LoadNav(_cid);
                    }
                    break;
                case "20"://// nhom san pham
                    if (int.TryParse(More.TangNameicid(hp), out _cid))
                    {
                        strReturn += "<li><a href=\"/san-pham.html\">" + label("lproducts") + "</a></li>";
                        strReturn += LoadNav(_cid);
                    }
                    break;
                // Chi tiet

                case "2":
                    strReturn += "<li><a href=\"/tin-tuc-new.html\">" + label("l_news") + "</a></li>";
                    strReturn += LoadNavNews();
                    break;
                case "4":
                    strReturn += LoadNavNewsFooter();
                    break;
                case "6":
                    strReturn += "<li><a href=\"/thu-vien-anh.html\">" + label("Thuvienanh") + "</a></li>";
                    strReturn += LoadNavAllbums();
                    break;
                case "8":
                    strReturn += "<li><a href=\"/thu-vien-video.html\">" + label("Thuvienvideo") + "</a></li>";
                    strReturn += LoadNavVideos();
                    break;
                case "21":
                    strReturn += "<li><a href=\"/san-pham.html\">" + label("lproducts") + "</a></li>";
                    strReturn += LoadNavProduts();
                    break;


            }
            if (Request["su"] != null && !Request["su"].Equals(""))
            {
                if (Request["su"].ToString() == "viewcart" || Request["su"].ToString() == "msg" || Request["su"].ToString() == "msg")
                {
                    strReturn += "<li><a href=\"/gio-hang.html\">" + label("lt_cartbox") + "</a></li>";
                }
                if (Request["su"].ToString() == "contact")
                {
                    strReturn += "<li><a href=\"/lien-he.html\">" + label("l_contact") + "</a></li>";
                }
                if (Request["su"].ToString() == "nws")
                {
                    strReturn += "<li><a href=\"/tin-tuc.html\">" + label("l_news") + "</a></li>";
                }
                if (Request["su"].ToString() == "Thuvien")
                {
                    strReturn += "<li><a href=\"/thu-vien-anh.html\">" + label("Thuvienanh") + "</a></li>";
                }
                if (Request["su"].ToString() == "Videos")
                {
                    strReturn += "<li><a href=\"/thu-vien-video.html\">" + label("Thuvienvideo") + "</a></li>";
                }
                if (Request["su"].ToString() == "prd")
                {
                    strReturn += "<li><a href=\"/san-pham.html\">" + label("lproducts") + "</a></li>";
                }
                if (Request["su"].ToString() == "Download")
                {
                    strReturn += "<li><a href=\"/tai-du-lieu.html\">Download</a></li>";
                }
                if (Request["su"].ToString() == "GioiThieu")
                {
                    strReturn += "<li><a href=\"/giai-phap.html\">" + label("giaiphap") + "</a></li>";
                }
                if (Request["su"].ToString() == "Search")
                {
                    strReturn += "<li><a href=\"#\">" + label("l_search") + "</a></li>";
                }
                if (Request["su"].ToString() == "Register")
                {
                    strReturn += "<li><a href=\"/Dang-ky.html\">" + label("Thanhvien") + "</a></li>";
                }
                if (Request["su"].ToString() == "resetpassword")
                {
                    strReturn += "<li><a href=\"/Doi-mat-khau.html\">" + label("lt_changepassword") + "</a></li>";
                }
                if (Request["su"].ToString() == "Infos")
                {
                    strReturn += "<li><a href=\"/thong-tin-thanh-vien.html\">" + label("ttthanhvien") + "</a></li>";
                }
                if (Request["su"].ToString() == "changinfo")
                {
                    strReturn += "<li><a href=\"/xem-thong-tin-thanh-vien.html\">" + label("capthanhvien") + "</a></li>";
                }
                if (Request["su"].ToString() == "changepass")
                {
                    strReturn += "<li><a href=\"/thay-doi-mat-khau.html\">" + label("thaydoimk") + "</a></li>";
                }
                if (Request["su"].ToString() == "Login")
                {
                    strReturn += "<li><a href=\"/Dang-nhap.html\">" + label("l_login") + "</a></li>";
                }
                if (Request["su"].ToString() == "Banchay")
                {
                    strReturn += "<li><a href=\"/san-pham-ban-chay.html\">" + label("l_prdbestsell") + "</a></li>";
                }
                if (Request["su"].ToString() == "sanphamoi")
                {
                    strReturn += "<li><a href=\"/san-pham-moi.html\">" + label("l_prdnews") + "</a></li>";
                }
            }
            ltrnav.Text = strReturn;
        }

        private string LoadNavNews()
        {
            New dt = db.News.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.icid.ToString()));
                nav = "<li><a href=\"" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }

        private string LoadNavNewsFooter()
        {
            Nfooter dt = db.Nfooters.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.icid.ToString()));
                nav = "<li><a href=\"" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }

        private string LoadNavAllbums()
        {
            //LAlbum dt = db.LAlbums.SingleOrDefault(p => p.TangName == hp);
            //if (dt != null)
            //{
            //    var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.Menu_ID.ToString()));
            //    nav = "<li><a href=\"" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
            //    if (item.Parent_ID != -1)
            //    {
            //        LoadNav(Convert.ToInt32(item.Parent_ID));
            //    }
            //}
            return nav;
        }

        private string LoadNavVideos()
        {
            VideoClip dt = db.VideoClips.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.Menu_ID.ToString()));
                nav = "<li><a href=\"" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }

        private string LoadNavProduts()
        {
            product dt = db.products.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.icid.ToString()));
                nav = "<li><a href=\"" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }

        private string LoadNav(int ID)
        {
            var item = db.Menus.FirstOrDefault(s => s.ID == ID);
            if (item != null)
            {
                nav = "<li><a href=\"" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }
        private string LoadNavPage(string TangName)
        {
            var item = db.Menus.FirstOrDefault(s => s.TangName == TangName & s.capp == More.MN);
            if (item != null)
            {
                nav = "<li><a href=\"" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}