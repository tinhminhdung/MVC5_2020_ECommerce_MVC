using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;

namespace VS.ECommerce_MVC.cms.Display.Allbums
{
    public partial class Detail : System.Web.UI.UserControl
    {
        #region string
        string cid = "-1";
        string iphoto = "-1";
        private string language = Captionlanguage.Language;
        string hp = "";
        int iEmptyIndex = 0;
        DatalinqDataContext db = new DatalinqDataContext();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Session
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
            if (!IsPostBack)
            {
                LAlbum items = db.LAlbums.SingleOrDefault(p => p.TangName == hp);
                if (items != null)
                {
                    ltcatename.Text = Commond.Name(items.Menu_ID.ToString());
                    iphoto = items.ID.ToString();
                    lttitle.Text = items.Title.ToString();
                    #region views
                    if (MoreAll.MoreAll.GetCookies("views").Equals("") || !MoreAll.MoreAll.GetCookies("views").Equals(this.iphoto))
                    {
                        SAlbum.Name_Text("update Album set Views=Views + 1 where ID=" + iphoto + "");
                        MoreAll.MoreAll.SetCookie("views", this.iphoto);
                    }
                    #endregion
                }
            }
        }
        public string ViewslideMax()
        {
            string bReturn = "";
            LAlbum dbPro = db.LAlbums.SingleOrDefault(p => p.TangName == hp);
            if (dbPro != null)
            {
                string[] strArray = dbPro.Anhnhieu.ToString().Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    // bReturn += "<li class=\"albumli\"><a href=\"" + strArray[i].ToString() + "\" rel=\"prettyPhoto[gallery1]\"><img alt='" + dbPro.Title.ToString() + "'  src=\"" + strArray[i].ToString() + "\"/></li>";
                    bReturn += "<li class=\"albumli\"><a href='" + strArray[i].ToString() + "'><img src='" + strArray[i].ToString() + "' alt='" + dbPro.Title + "" + i + "' /></br></a></li>";
                }
            }
            return bReturn;
        }

        public string ViewslideMin()
        {
            string bReturn = "";
            LAlbum dbPro = db.LAlbums.SingleOrDefault(p => p.TangName == hp);
            if (dbPro != null)
            {
                string[] strArray = dbPro.Anhnhieu.ToString().Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    bReturn += "<li><img alt='" + dbPro.Title.ToString() + "' src=\"" + strArray[i].ToString().Replace("uploads", "uploads/_thumbs") + "\"/></li>";
                }
            }
            return bReturn;
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}