using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using System.Data;
using Services;
using Framework;
using Entity;

namespace VS.ECommerce_MVC.cms.Display
{
    public partial class Lefmenu : System.Web.UI.UserControl
    {
        #region string
        private string language = Captionlanguage.Language;
        string hp = "";
        int iEmptyIndex = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region #
            if (System.Web.HttpContext.Current.Session["language"] != null)
            {
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["language"] = this.language;
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            #endregion
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
            if (!base.IsPostBack)
            {
                LoadItems();
            }
        }

        #region LoadItems
        void LoadItems()
        {
            List<Entity.News> dt = SNews.Name_Text("SELECT * FROM [News] WHERE new=1  AND Status=1  order by Create_Date desc");
            if (dt.Count > 0)
            {
                rpcates.DataSource = dt;
                rpcates.DataBind();
            }
        }
        #endregion

        #region MyRegion
        protected string MMenuPro()
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.capp_Lang_Parent_ID_Status(More.PR, language, "-1", "1");
            if (dt.Count > 0)
            {
                foreach (Entity.Menu item in dt)
                {
                    str += "<div class=\"gnv-group-menu\">";
                    str += "<div class=\"gnv-menu-mini-header\">";
                    str += "<div class=\"mini-submenu\"></div><a href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a></div>";
                    str += "<div class=\"clearfix\"></div>";
                    str += "<ul class=\"list-group\">";
                    str += MenuPro(item.ID.ToString());
                    str += "</ul>";
                    str += "</div>";
                }
            }
            return str.ToString();
        }

        protected string MenuPro(string id)
        {
            string str = "";

            List<Entity.Menu> dt = SMenu.capp_Lang_Parent_ID_Status(More.PR, language, id, "1");
            if (dt.Count > 0)
            {
                foreach (Entity.Menu item in dt)
                {
                    if (More.Sup_Parent_ID(item.ID.ToString()) == "")
                    {
                        str += "<li><a  href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a></li>";
                    }
                    else
                    {
                        try
                        {
                            if (More.MenuDacap(More.TangNameicid(hp)) == More.MenuDacap(item.ID.ToString()))
                            {
                                str += "<li  class=\"items-product\"><a  href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a>" + Menu_Pro(item.ID.ToString(), "style='display:block'") + "</li>";
                            }
                            else
                            {
                                str += "<li  class=\"items-product\"><a  href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a>" + Menu_Pro(item.ID.ToString(), "style='display:none'") + "</li>";
                            }
                        }
                        catch (Exception)
                        {
                            str += "<li  class=\"items-product\"><a  href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a>" + Menu_Pro(item.ID.ToString(), "style='display:none'") + "</li>";
                        }
                    }
                }
            }
            return str.ToString();
        }

        protected string Menu_Pro(string id, string css)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.capp_Lang_Parent_ID_Status(More.PR, language, id, "1");
            if (dt.Count > 0)
            {
                str += "<ul " + css + ">";
                foreach (Entity.Menu item in dt)
                {
                    str += "<li><a href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a></li>";
                }
                str += "</ul>";
            }
            return str.ToString();
        }
        #endregion

        protected string LocThuongHieu()
        {
            string str = "";
            try
            {
                List<Entity.Menu> dt = SMenu.Name_Text("SELECT * FROM [Menu]  where  capp='" + More.HA + "' and Lang='" + language + "'  and Parent_ID=" + More.MenuDacap(More.TangNameicid(hp)) + " and Status=1 order by Orders asc");
                if (dt.Count > 0)
                {
                    foreach (Entity.Menu item in dt)
                    {
                        str += "<a href=\"javascript:void(0)\"  rel=\"nofollow\" id=\"" + item.ID + "\" title=\"" + item.Name + "\" class=\"sort_list\"  onclick=\"choose_produce(this)\"> " + item.Name.ToString() + " </a>";
                    }
                }
            }
            catch (Exception)
            { }
            return str.ToString();
        }

        protected string LocTheoGia()
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.Name_Text("SELECT * FROM [Menu]  where  capp='" + More.KG + "' and Lang='" + language + "'  and Parent_ID=-1 and Status=1 order by Orders asc");
            if (dt.Count > 0)
            {
                foreach (Entity.Menu item in dt)
                {
                    str += "<a href=\"javascript:void(0)\"  rel=\"nofollow\" name=\"" + item.ID + "\" title=\"" + item.Name + "\" class=\"sort_list\"  onclick=\"choose_price(this)\"> " + item.Name.ToString() + " </a>";
                }
            }
            return str.ToString();
        }

        protected string MenuThuongHieu()
        {
            string str = "";
            try
            {
                List<Entity.Menu> dt = SMenu.Name_Text("SELECT * FROM [Menu]  where  capp='" + More.HA + "' and Lang='" + language + "'  and Parent_ID=" + More.MenuDacap(More.TangNameicid(hp)) + " and Status=1 order by Orders asc");
                if (dt.Count > 0)
                {
                    foreach (Entity.Menu item in dt)
                    {
                        str += "<li><a href=\"" + item.TangName + ".html\"  title=\"" + item.Name + "\"> " + item.Name.ToString() + " </a></li>";
                    }
                }
            }
            catch (Exception)
            { }
            return str.ToString();
        }

        protected string MenuSanpham()
        {
            string str = "";
            try
            {
                List<Entity.Menu> dt = SMenu.Name_Text("SELECT * FROM [Menu]  where  capp='" + More.PR + "' and Lang='" + language + "'  and Parent_ID=" + More.MenuDacap(More.TangNameicid(hp)) + " and Status=1 order by Orders asc");
                if (dt.Count > 0)
                {
                    foreach (Entity.Menu item in dt)
                    {
                        str += "<li><a href=\"" + item.TangName + ".html\"  title=\"" + item.Name + "\"> " + item.Name.ToString() + " </a></li>";
                    }
                }
            }
            catch (Exception)
            { }
            return str.ToString();
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}