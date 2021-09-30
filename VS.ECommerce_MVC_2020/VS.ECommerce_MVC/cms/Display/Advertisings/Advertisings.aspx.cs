using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;

namespace VS.ECommerce_MVC.cms.Display.Advertisings
{
    public partial class Advertisings : System.Web.UI.Page
    {
        #region string
        string images = "-1";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region MyRegion
            if (Request["images"] != null && !Request["images"].Equals(""))
            {
                images = Request["images"];
            }
            List<Entity.Advertisings> dt = SAdvertisings.GETBYID(images);
            if (dt.Count > 0)
            {
                if (!dt[0].Path.ToString().Equals(""))
                {
                    SAdvertisings.UPDATE_VIEW(images);
                    base.Response.Redirect((dt[0].Path.ToString()));
                }
                else
                {
                    base.Response.Redirect("/index.aspx");
                }
            }
            #endregion
        }

        private static string ResolveLink(string link)
        {
            if (link.IndexOf("@") > -1)
            {
                string[] strArray = link.Split(new char[] { Convert.ToChar("@") });
                if (strArray.Length == 2)
                {
                    return strArray[0];
                }
            }
            return link;
        }
    }
}