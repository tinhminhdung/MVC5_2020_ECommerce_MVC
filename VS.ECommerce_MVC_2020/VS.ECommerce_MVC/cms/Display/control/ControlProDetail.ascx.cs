using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.ECommerce_MVC.cms.Display.control
{
    public partial class ControlProDetail : System.Web.UI.UserControl
    {
        #region string
        #endregion
        string hp = "";
        int iEmptyIndex = 0;
        public string Moldul = "";
        string pid = "-1";
        string cid = "-1";
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
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
            product dt = db.products.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                cid = dt.icid.ToString();
                pid = dt.ipid.ToString();
                List<Entity.Category_Product> table = SProducts.Name_Text_Rg("select top " + int.Parse(Commond.Setting("proother")) + " ipid,icid,TangName,Name,Images,ImagesSmall,Brief,Create_Date,Price,OldPrice,ID_Hang,sanxuat,Code,Noidung1 from products where icid= " + cid + " and ipid!= " + pid + "  and lang= '" + language + "'  and Status=1 order by Create_Date desc");
                if (table.Count >= 1)
                {
                   // ltother.Text += Commond.LoadProductListOther(table);
                }
            }

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
                //case "prd":
                //    phcontrol.Controls.Add(LoadControl("~/cms/Display/Products/index.ascx"));
                //    break;
                //case "Search":
                //    phcontrol.Controls.Add(LoadControl("~/cms/Display/Products/Search.ascx"));
                //    break;
                //case "viewcart":
                //    phcontrol.Controls.Add(LoadControl("Products/Cart.ascx"));
                //    break;
                //case "msg":
                //    phcontrol.Controls.Add(LoadControl("Products/Message.ascx"));
                //    break;

            }
            #endregion

        }
    }
}