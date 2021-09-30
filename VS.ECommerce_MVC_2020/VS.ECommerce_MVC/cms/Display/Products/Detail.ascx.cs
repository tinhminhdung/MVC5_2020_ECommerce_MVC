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
    public partial class Detail : System.Web.UI.UserControl
    {
        string pid = "-1";
        string cid = "-1";
        string hp = "";
        int iEmptyIndex = 0;
        string bReturn = "";
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
            if (!IsPostBack)
            {
                product dt = db.products.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    cid = dt.icid.ToString();
                    ltcatename.Text = Commond.Name(dt.icid.ToString());
                    pid = dt.ipid.ToString();
                    hdipid.Value = dt.ipid.ToString();
                    ltname.Text = dt.Name;
                    ltdesc.Text = dt.Brief;
                    ltdetail.Text = dt.Contents;
                    ltxem.Text = dt.Views.ToString();
                    ltdate.Text = MoreAll.MoreAll.Date(dt.Create_Date.ToString());
                    ltcode.Text = dt.Code.ToString();

                    ltpriceoll.Text = AllQuery.MorePro.FormatMoney_VND(dt.OldPrice.ToString());
                    ltprice.Text =AllQuery.MorePro.FormatMoney_Full_lh(dt.Price.ToString());

                    #region Show ảnh đại diện và nhiều ảnh thum
                    if (dt != null)
                    {
                        bReturn += " <a href=\"" + dt.Images + "\"><img src=\"" + dt.Images + "\" alt=\"" + dt.Name + "\"  /></a>";
                        if (dt.Anh.ToString().Length > 5)
                        {
                            string[] strArray = dt.Anh.ToString().Split(new char[] { ',' });
                            for (int i = 0; i < strArray.Length; i++)
                            {
                                bReturn += "<a href=\"" + strArray[i].ToString() + "\"><img alt='" + dt.Name.ToString() + "'src=\"" + strArray[i].ToString().Replace("uploads", "uploads/_thumbs") + "\"/></a>";
                            }
                        }
                    }
                    ltshowimg.Text = bReturn; 
                    #endregion

                    List<Entity.Category_Product> table = SProducts.Name_Text_Rg("select top " + int.Parse(Commond.Setting("proother")) + " ipid,icid,TangName,Alt,Name,Images,ImagesSmall,Brief,Create_Date,Price,OldPrice,ID_Hang,sanxuat,Code from products where icid= " + cid + " and ipid!= " + pid + "  and lang= '" + language + "'  and Status=1 order by Create_Date desc");
                    if (table.Count >= 1)
                    {
                        ltother.Text += Commond.LoadProductList(table);
                    }
                }
                //#region CookiesPro_sanphamdaxem
                //string ckPro = "";
                //if (MoreAll.MoreAll.GetCookies("CookiesPro").ToString() != null)
                //{
                //    ckPro = MoreAll.MoreAll.GetCookies("CookiesPro").ToString();
                //}
                //if (ckPro != "")
                //{
                //    int[] arrId = ckPro.Split(',').Select(s => int.Parse(s)).ToArray();
                //    if (!arrId.Contains(int.Parse(hdipid.Value.ToString())))
                //    {
                //        ckPro += "," + hdipid.Value.Trim().ToString();
                //    }
                //}
                //else
                //{
                //    ckPro += hdipid.Value.Trim().ToString();
                //}
                //MoreAll.MoreAll.SetCookie("CookiesPro", ckPro, 100);
                //#endregion

            }
            if (MoreAll.MoreAll.GetCookies("views").Equals("") || !MoreAll.MoreAll.GetCookies("views").Equals(this.pid))
            {
                SProducts.UpdateViewTimes(this.pid);
                MoreAll.MoreAll.SetCookie("views", this.pid);
            }
        }

        //public string Viewprodetail()
        //{
        //    string bReturn = "";
        //    product dbPro = db.products.SingleOrDefault(p => p.TangName == hp);
        //    if (dbPro != null)
        //    {
        //        bReturn += " <a href=\"" + dbPro.Images + "\"><img src=\"" + dbPro.Images + "\" alt=\"" + dbPro.Name + "\"  /></a>";
        //        if (dbPro.Anh.ToString().Length > 5)
        //        {
        //            string[] strArray = dbPro.Anh.ToString().Split(new char[] { ',' });
        //            for (int i = 0; i < strArray.Length; i++)
        //            {
        //                bReturn += "<a href=\"" + strArray[i].ToString() + "\"><img alt='" + dbPro.Name.ToString() + "'src=\"" + strArray[i].ToString().Replace("uploads", "uploads/_thumbs") + "\"/></a>";
        //            }
        //        }
        //    }
        //    return bReturn;
        //}

        //public string Viewprodetail()
        //{
        //    string _strProdet = "";
        //    List<Entity.Products> dt = SProducts.GetById(pid);
        //    if (dt.Count > 0)
        //    {
        //        _strProdet += " <a href=\"" + dt.Images + "\"><img src=\"" + dt.Images + "\" alt=\"" + dt.Name + "\"  /></a>";
        //    }
        //    List<Entity.Product_images> lstImage = SProduct_images.GetBy_ipid(pid);
        //    if (lstImage.Count > 0)
        //    {
        //        for (int i = 0; i < lstImage.Count; i++)
        //        {
        //            _strProdet += "<a href=\"" + lstImage[i].Images + "\"><img src=\"" + lstImage[i].Images + "\" alt=\"" + dt.Name + "\"    /></a>";
        //        }
        //    }
        //    return _strProdet;
        //}

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }

        protected void lnkaddtocart_Click(object sender, EventArgs e)
        {
            string Kichco = "0";
            string Mausac = "0";
            try { if (System.Web.HttpContext.Current.Session["Session_Size"].ToString() != null && !System.Web.HttpContext.Current.Session["Session_Size"].ToString().Equals("")) { Kichco = System.Web.HttpContext.Current.Session["Session_Size"].ToString(); } }
            catch (Exception) { }
            try { if (System.Web.HttpContext.Current.Session["Session_MauSac"].ToString() != null && !System.Web.HttpContext.Current.Session["Session_MauSac"].ToString().Equals("")) { Mausac = System.Web.HttpContext.Current.Session["Session_MauSac"].ToString(); } }
            catch (Exception) { }
            SessionCarts.ShoppingCart_AddProduct_Sesion(hdipid.Value, Convert.ToInt32(txtsoluong.Text), Mausac, Kichco);
            System.Web.HttpContext.Current.Session["Session_Size"] = null;
            System.Web.HttpContext.Current.Session["Session_MauSac"] = null;
            System.Web.HttpContext.Current.Session["GSession_MauSac"] = null;
            System.Web.HttpContext.Current.Session["GSession_Size"] = null;
            Response.Redirect("/gio-hang.html");
        }

        protected void lnkaddtocartgh_Click(object sender, EventArgs e)
        {
            SessionCarts.ShoppingCart_AddProduct(hdipid.Value, Convert.ToInt32(txtsoluong.Text));
            Response.Write("<script type=\"text/javascript\">alert('Đã thêm sản phẩm vào giỏ hàng')</script>");
        }
        //v01.vmms.vn
        #region MauSac_KichCo
        public string MauSacFull()// cách lấy ra mầu kiểu code mới
        {
            string show = "0";
            string str = "";
            List<Menu> ListMenu = new List<Menu>();
            var list = db.Trunggians.Where(s => s.Proid == int.Parse(hdipid.Value) && s.Trangthai == 1).OrderBy(s => s.ID).ToList();// tìm trong bảng trung gian có bao nhiêu ID 218
            for (int i = 0; i < list.Count; i++)
            {
                var table = db.Menus.Where(s => s.ID == int.Parse(list[i].Icolor.ToString()) && s.Status == 1 && s.capp == "CO").OrderBy(s => s.Orders).ToList();//so sánh bảng menu và bảng trung gian để lấy ra tên của mầu
                if (table.Count > 0)
                {
                    show = "1";
                    for (int j = 0; j < table.Count; j++)
                    {

                        ListMenu.Add(table[j]);
                    }
                }
            }
            if (show == "1")
            {
                ListMenu = ListMenu.OrderBy(s => s.Orders).ToList();
                str += "<div style=\" padding-top:10px;clear:both\"  class=\"price\">";
                str += "<div style=\"color:#353535\">Mầu sắc</div>";
                str += "<div class=\"Mausac\">";
                int k = 1;
                foreach (var item in ListMenu)
                {
                    if (k == 1)
                    {
                        System.Web.HttpContext.Current.Session["Session_MauSac"] = item.ID;
                        System.Web.HttpContext.Current.Session["GSession_MauSac"] = item.ID;
                    }
                    try
                    {
                        if (item.ID.ToString() == Session["GSession_MauSac"].ToString())
                        {
                            str += "<a href=\"javascript:void(0)\" class=\"Color active\" onclick=\"MauSac(" + item.ID + ",'" + item.Name + "')\"><img  src='" + item.Images + "'/></a>";
                        }
                        else
                        {
                            str += "<a href=\"javascript:void(0)\" class=\"Color\" onclick=\"MauSac(" + item.ID + ",'" + item.Name + "')\"><img  src='" + item.Images + "'/></a>";
                        }
                    }
                    catch (Exception)
                    {
                        str += "<a href=\"javascript:void(0)\" class=\"Color\" onclick=\"MauSac(" + item.ID + ",'" + item.Name + "')\"><img  src='" + item.Images + "'/></a>";
                    }
                    k++;
                }
                str += "</div>";
                str += "</div>";
            }
            return str;
        }
        public string KichCoFull()// cách lấy ra mầu kiểu code mới
        {
            string str = "";
            string show = "0";
            List<Menu> ListMenu = new List<Menu>();
            var list = db.Trunggians.Where(s => s.Proid == int.Parse(hdipid.Value) && s.Trangthai == 2).OrderBy(s => s.ID).ToList();// tìm trong bảng trung gian có bao nhiêu ID 218
            for (int i = 0; i < list.Count; i++)
            {
                var table = db.Menus.Where(s => s.ID == int.Parse(list[i].Icolor.ToString()) && s.Status == 1 && s.capp == "SI").OrderBy(s => s.Orders).ToList();//so sánh bảng menu và bảng trung gian để lấy ra tên của mầu
                if (table.Count > 0)
                {
                    show = "1";
                    for (int j = 0; j < table.Count; j++)
                    {
                        ListMenu.Add(table[j]);
                    }
                }
            }
            if (show == "1")
            {
                ListMenu = ListMenu.OrderBy(s => s.Orders).ToList();
                str += "<div style=\" padding-top:10px;clear:both\"  class=\"price\">";
                str += "<div style=\"color:#353535\">Kích cỡ</div>";
                str += "<div class=\"Kichhuoc\">";
                int k = 1;
                foreach (var item in ListMenu)
                {
                    if (k == 1)
                    {
                        System.Web.HttpContext.Current.Session["Session_Size"] = item.ID;
                        System.Web.HttpContext.Current.Session["GSession_Size"] = item.ID;
                    }
                    try
                    {
                        if (item.ID.ToString() == Session["GSession_Size"].ToString())
                        {
                            str += "<a href=\"javascript:void(0)\" class=\"size active\" onclick=\"KichCo(" + item.ID + ",'" + item.Name + "')\">" + item.Name + "</a>";
                        }
                        else
                        {
                            str += "<a href=\"javascript:void(0)\" class=\"size\" onclick=\"KichCo(" + item.ID + ",'" + item.Name + "')\">" + item.Name + "</a>";
                        }
                    }
                    catch (Exception)
                    {
                        str += "<a href=\"javascript:void(0)\" class=\"size\" onclick=\"KichCo(" + item.ID + ",'" + item.Name + "')\">" + item.Name + "</a>";
                    }
                    k++;
                }
                str += "</div>";
                str += "</div>";
            }
            return str;
        }
        #endregion
    }
}