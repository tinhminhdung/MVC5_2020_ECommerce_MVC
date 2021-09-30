using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.ECommerce_MVC.cms.Display.control
{
    public partial class ControlPro : System.Web.UI.UserControl
    {
        #region string
        #endregion
        string hp = "";
        int iEmptyIndex = 0;
        public string Moldul = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DatalinqDataContext db = new DatalinqDataContext();
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


            #region Request_e
            //try
            // {
            if (Request["e"] != null)
            {
                if (Request["e"].ToString() == "load")
                {
                    string request = Request["hp"] != null ? Request["hp"].ToString() : Request.Path;
                    string t = Request["hp"].ToString() + ".html";
                    if (!request.ToLower().Contains("index.aspx"))
                    {
                        Moldul = Commond.RequestMenu(Request["hp"]);
                        switch (Moldul)
                        {
                            case "23":
                                phcontrol.Controls.Add(LoadControl("~/cms/Display/Products/Hangsanpham.ascx"));
                                break;
                            case "20":
                                phcontrol.Controls.Add(LoadControl("~/cms/Display/Products/Category.ascx"));
                                break;
                            case "21":
                                phcontrol.Controls.Add(LoadControl("~/cms/Display/Products/Detail.ascx"));
                                break;
                        }
                    }
                }

            }
            //}
            //catch (Exception)
            //{
            //    Response.Redirect("/page-404.html");
            //}
            #endregion
            #region Request_su
            switch (Request["su"])
            {
                case "prd":
                    phcontrol.Controls.Add(LoadControl("~/cms/Display/Products/index.ascx"));
                    break;
                case "Search":
                    phcontrol.Controls.Add(LoadControl("~/cms/Display/Products/Search.ascx"));
                    break;
                case "viewcart":
                    phcontrol.Controls.Add(LoadControl("~/cms/Display/Products/Cart.ascx"));
                    break;
                case "msg":
                    phcontrol.Controls.Add(LoadControl("~/cms/Display/Products/Message.ascx"));
                    break;

            }
            #endregion

        }
    }
}