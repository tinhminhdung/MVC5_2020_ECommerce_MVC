using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;
using System.Data;

namespace VS.ECommerce_MVC.cms.Display
{
    public partial class index : System.Web.UI.UserControl
    {
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
            if (!base.IsPostBack)
            {
                // Set Cache
                ltlistpro.Text = MoreAll.MoreAll.GetCache("ShowNhomsanpham", HttpContext.Current.Cache["ShowNhomsanpham"] != null ? "" : ShowNhomsanpham());

                //List<Entity.Category_Product> dt2 = SProducts.S_products_NoiChuoi(" and lang='VIE'");
                //rpkhuyenmai.DataSource = dt2;
                //rpkhuyenmai.DataBind();

                //List<Entity.Category_Product> dt2 = SProducts.Name_Text_Rg("select top 10 Alt,ipid,icid,TangName,Name,Images,ImagesSmall,Brief,Create_Date,Price,OldPrice,ID_Hang,sanxuat,Code from  products where  News=1 and lang='" + language + "' and Status=1  order by Create_Date desc");
                //rpkhuyenmai.DataSource = dt2;
                //rpkhuyenmai.DataBind();

                List<Entity.MenuShow> dt = SMenu.Pages_Home(More.PR, language, "1");
                rpcates.DataSource = dt;
                rpcates.DataBind();

            }
        }

        protected List<Entity.Category_Product> NewProductInCate(string icid)
        {
            return SProducts.GetTopProductInCategory(Commond.HomePage(), icid, More.Sub_Menu(More.PR, icid));
        }
        protected string Bien()
        {
            string str = "";
            str = ("<iframe width=100% height=100% style=\" overflow:hidden\" scrolling=no  frameborder=no src=\"http://dantri.com\" marginheight=0 marginwidth=0></iframe>");
            return str;
        }

        protected string LoadNews()// tin mới
        {
            string str = "";
            List<Entity.Category_News> table = SNews.Name_Text_Rg("SELECT " + Commond.Sql_News() + " FROM [News] WHERE new=1 AND lang='" + language + "'  AND Status=1  order by Create_Date desc");
            if (table.Count >= 1)
            {
                str += Commond.LoadNewsListHome(table, language);
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
                    str += " <section class=\"awe-section-6\">";
                    str += "   <div id=\"product\" class=\"section section-deal products-view-grid\">";
                    str += "     <div class=\"container\">";
                    str += "       <div class=\"section-title a-center\">";
                    str += "         <h2><a href=\"/" + item.TangName + ".html\">" + item.Name + "</a></h2>";
                    str += "       </div>";
                    str += "       <div class=\"section-content\">";
                    str += "         <div class=\"products products-view-grid owl-carousel owl-theme\" data-md-items=\"4\" data-sm-items=\"3\" data-xs-items=\"2\" data-xss-items=\"2\" data-margin=\"30\" data-nav=\"true\">";
                    List<Entity.Category_Product> table = SProducts.GetTopProductInCategory(Commond.HomePage(), item.ID.ToString(), More.Sub_Menu(More.PR, item.ID.ToString()));
                    if (table.Count >= 1)
                    {
                        str += Commond.LoadProductList_Home(table);
                    }
                    str += "         </div>";
                    str += "       </div>";
                    str += "     </div>";
                    str += "   </div>";
                    str += " </section>";
                }
            }
            return str;
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}