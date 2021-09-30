using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;

namespace VS.ECommerce_MVC.cms.Display.Products
{
    public partial class Category : System.Web.UI.UserControl
    {
        private string cid = "-1";
        private string language = Captionlanguage.Language;
        string hp = "";
        int iEmptyIndex = 0;
        string price = "0";
        string produce = "0";
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

            #region RequestLoc
            if (Request["price"] != null && !Request["price"].Equals(""))
            {
                price = Request["price"];
            }
            if (Request["produce"] != null && !Request["produce"].Equals(""))
            {
                produce = Request["produce"];
            }
            #endregion

            if (!IsPostBack)
            {
                // CollectionPager1.FirstText = label("FirstText");
                // CollectionPager1.LastText = label("LastText");

                if (this.cid.Equals(""))
                {
                    this.ltcatename.Text = "Sản phẩm";
                }
                else
                {
                    Menu dt = db.Menus.SingleOrDefault(p => p.ID == int.Parse(More.TangNameicid(hp)));
                    if (dt != null)
                    {
                        ltcatename.Text = dt.Name.ToString();
                        ltcontent.Text = dt.Description;
                    }
                }
                LoadItems();
            }
            // LoadItems();
        }

        //protected void LoadItems()
        //{
        //    #region Boloc
        //    //String chuoi = "";
        //    //String order = "";
        //    //if (produce != "0")
        //    //{
        //    //    chuoi += " and ID_Hang in(" + produce + ")";
        //    //}
        //    //if (price != "0")
        //    //{

        //    // string Gia = " and (";
        //    //string[] strArray = price.ToString().Split(new char[] { ',' });
        //    //for (int i = 0; i < strArray.Length; i++)
        //    //{
        //    //Gia += (i == 0 ? "" : " OR ") + "(Price between (" + Tu(strArray[i].ToString()) + ") and (" + Den(strArray[i].ToString()) + ")) ";
        //    //}
        //    //chuoi += Gia + ")";

        //    //}
        //    //order += " order by Create_Date desc";
        //    //List<Entity.Products> dt = new List<Entity.Products>();
        //    //dt = SProducts.Name_Text("select * from products where  icid in(" + More.Sub_Menu(More.PR, More.TangNameicid(hp)) + ") and lang= '" + language + "' and Status= 1 " + chuoi.ToString() + " and (Create_Date<=getdate() and getdate()<=Modified_Date) " + order + "");
        //    #endregion

        //    List<Entity.Products> dt = new List<Entity.Products>();
        //    dt = SProducts.CategoryDisplay(More.Sub_Menu(More.PR, More.TangNameicid(hp)), language, "1");
        //    if (dt.Count > 0)
        //    {
        //        CollectionPager1.DataSource = dt;
        //        CollectionPager1.MaxPages = 10000;
        //        CollectionPager1.BindToControl = rpcates;
        //        CollectionPager1.PageSize = int.Parse(Commond.Setting("pagepro"));
        //        rpcates.DataSource = CollectionPager1.DataSourcePaged;
        //        rpcates.DataBind();
        //    }
        //    else lterr.Text = "<div style='color:Red; font-weight:bold; text-align:center; margin-bottom:10px; padding-top:10px'>" + this.label("I_dulieuchuadccapnhat") + "</div>";
        //}

        public void LoadItems()
        {
            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(Commond.Setting("pagepro"));

            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.Category_Product> dt = SProducts.CATEGORY_PHANTRANG(Commond.SubMenu(More.PR, More.TangNameicid(hp)), language, "1", (pages - 1), ref Tongsobanghi, Tongsotrang);
            if (dt.Count >= 1)
            {
                ltShow.Text = Commond.LoadProductList(dt);
            }
            else
            {
                ltShow.Text = "<div class='Checkdata'>" + this.label("I_dulieuchuadccapnhat") + "</div>";
            }
            if (Tongsobanghi % Tongsotrang > 0)
            {
                Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
            }
            else
            {
                Tongsobanghi = Tongsobanghi / Tongsotrang;
            }
            ltpage.Text = Commond.Phantrang("/" + hp + ".html", Tongsobanghi, pages);
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}