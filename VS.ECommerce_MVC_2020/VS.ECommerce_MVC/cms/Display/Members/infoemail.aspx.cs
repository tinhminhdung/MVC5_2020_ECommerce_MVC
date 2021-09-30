using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Framework;
using System.Data;

namespace VS.ECommerce_MVC.cms.Display.Members
{
    public partial class infoemail : System.Web.UI.Page
    {
        string key = "";
        private string language = Captionlanguage.Language;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Fusers item = new Fusers();
            //if (System.Web.HttpContext.Current.Session["language"] != null)
            //{
            //    this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            //}
            //else
            //{
            //    System.Web.HttpContext.Current.Session["language"] = this.language;
            //    this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            //}
            //if (Request["key"] != null && !Request["key"].Equals(""))
            //{
            //    key = Request["key"];
            //}
            //if (item.vvalidatekey(key.ToLower()).Rows.Count > 0)
            //{
            //    DataTable table = item.vvalidatekey(key);
            //    if (table.Rows.Count > 0)
            //    {
            //        string newpassword = DateTime.Now.Ticks.ToString();
            //        item.Active_vvalidatekey(table.Rows[0]["vuserun"].ToString(), newpassword);
            //        ltthongbao.Text = label("senemail1") + "<br />" + label("senemail2") + "<br />" + label("senemail3");
            //    }
            //}
            //else
            //{
            //    ltthongbao.Text = label("senemail4") + "<br />" + label("senemail5") + "<br />" + label("senemail3");
            //}
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}