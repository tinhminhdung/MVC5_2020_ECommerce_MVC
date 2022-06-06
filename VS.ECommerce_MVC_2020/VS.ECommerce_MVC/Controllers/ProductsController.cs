using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace VS.ECommerce_MVC.Controllers
{
    public class ProductsController : Controller
    {
        // GET: /Products/
        DatalinqDataContext db = new DatalinqDataContext();
        private string language = Captionlanguage.SetLanguage();
        public ActionResult Index()
        {
            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(Commond.Setting("pagepro"));
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.Category_Product> dt = SProducts.Product_All(language, "1", (pages - 1), ref Tongsobanghi, Tongsotrang);
            if (dt.Count >= 1)
            {
                ViewBag.ShowList = Commond.LoadProductList(dt);
            }
            else
            {
                ViewBag.ShowList = "<div class='Checkdata'>" + Commond.label("I_dulieuchuadccapnhat") + "</div>";
            }
            if (Tongsobanghi % Tongsotrang > 0)
            {
                Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
            }
            else
            {
                Tongsobanghi = Tongsobanghi / Tongsotrang;
            }
            ViewBag.Phantrang = Commond.Phantrang("/san-pham-news.html", Tongsobanghi, pages);
            return View();
        }

       // [OutputCache(Duration = int.MaxValue, VaryByParam = "hp", Location = OutputCacheLocation.Server)]
        public ActionResult Detail(string hp)
        {
            //Kiểm tra tham số truyền vào có rổng hay không
            if (hp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Nếu không thì truy xuất csdl lấy ra sản phẩm tương ứng
            string bReturn = "";
            product dt = db.products.SingleOrDefault(p => p.TangName == hp);
            if (dt == null)
            {
                //Thông báo nếu như không có sản phẩm đó
                Commond.Link301();
                return HttpNotFound();
            }
            else if (dt != null)
            {
                string cid = dt.icid.ToString();
                string pid = dt.ipid.ToString();

                #region Show ảnh đại diện và nhiều ảnh thum
                if (dt != null)
                {
                    bReturn += "<a href=\"" + dt.Images.Replace("'", "") + "\"><img src=\"" + dt.Images.Replace("'", "") + "\" alt=\"" + dt.Name.Replace("'", "") + "\"  /></a>";
                    if (dt.Anh.ToString().Length > 5)
                    {
                        string[] strArray = dt.Anh.Replace("'", "").ToString().Split(new char[] { ',' });
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            bReturn += "<a href=\"" + strArray[i].ToString().Replace("'", "") + "\"><img alt='" + dt.Name.Replace("'", "") + "'src=\"" + strArray[i].ToString().Replace("uploads", "uploads/_thumbs").Replace("'", "") + "\"/></a>";
                        }
                    }
                }
                ViewBag.Showimages = bReturn;
                #endregion


                #region Other
                List<Entity.Category_Product> table = SProducts.Name_Text_Rg("select top " + int.Parse(Commond.Setting("proother")) + " ipid,icid,TangName,Alt,Name,Images,ImagesSmall,Brief,Create_Date,Price,OldPrice,ID_Hang,sanxuat,Code from products where icid= " + cid + " and ipid!= " + pid + "  and lang= '" + language + "'  and Status=1 order by Create_Date desc");
                if (table.Count >= 1)
                {
                    ViewBag.Other += Commond.LoadProductList(table);
                }

                #endregion

                #region UpdateViewTimes
                if (MoreAll.MoreAll.GetCookies("views").Equals("") || !MoreAll.MoreAll.GetCookies("views").Equals(pid))
                {
                    SProducts.UpdateViewTimes(pid);
                    MoreAll.MoreAll.SetCookie("views", pid);
                }

                #endregion
                return View(dt);
            }
            return View();
        }

       // [OutputCache(Duration = int.MaxValue, VaryByParam = "hp", Location = OutputCacheLocation.Server)]
        public ActionResult Category(string hp)
        {
            if (hp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            #region Menu
            Menu table = db.Menus.SingleOrDefault(p => p.ID == int.Parse(More.TangNameicid(hp)));
            if (table == null)
            {
                Commond.Link301();
            }
            else if (table != null)
            {
                ViewBag.Name = table.Name.ToString();
                ViewBag.ShowConten = table.Description;
            }
            #endregion

            //with CTE_R as
            //(
            //    select value  from  STRING_SPLIT('1,2,3,4,5,6,2,3,23,4,5,46,7,1,2,34,12,45,45,2,3,4,5,6,2,3,23,4,5,46,7,1,2,34,12,45,45,2,3,4,5,6,2,3,23,4,5,46,7,1,2,34,12,45,45,2,3,4,5,6,2,3,23,4,5,46,7,1,2,34,12,45,45,2,3,4,5,6,2,3,23,4,5,46,7,1,2,34,12,45,45,2,3,4,5,6,2,3,23,4,5,46,7,1,2,34,12,45,45,2,3,4,5,6,2,3,23,4,5,46,7,1,2,34,12,45,45,2,3,4,5,6,2,3,23,4,5,46,7,1,2,34,12,45,45,2,3,4,5,6,2,3,23,4,5,46,7,1,2,34,12,45,45,2,3,4,5,6,2,3,23,4,5,46,7,1,2,34,12,45,45',',')
            //)
            //select * from 
            //(
            //    select * from TestA  h
            //    inner join (select distinct value FROM CTE_R ) A on h.ID = a.value
            //) as v



//22222
//truy vấn kiểu in (1,2,3,4,5,6,7)
//@ls_phone =(1,2,3,4,5,6,7,8,9,0);


//WITH tblphone AS (select value AS phone from STRING_SPLIT(@ls_phone,','))
//select A.phone
//from tbl_AppMobile_Customer_Login A with(nolock)
//LEFT JOIN tblphone B ON A.phone = B.phone
//LEFT JOIN tblphone C ON A.accountNo = C.phone
//WHERE status=1
//AND (B.phone IS NOT NULL OR C.phone IS NOT NULL


            #region Category
            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(Commond.Setting("pagepro"));
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.Category_Product> dt = SProducts.CATEGORY_PHANTRANG(Commond.SubMenu(More.PR, More.TangNameicid(hp)), "VIE", "1", (pages - 1), ref Tongsobanghi, Tongsotrang);
            if (dt.Count >= 1)
            {
                ViewBag.ShowList = Commond.LoadProductList(dt);
            }
            else
            {
                ViewBag.ShowList = "<div class='Checkdata'>" + Commond.label("I_dulieuchuadccapnhat") + "</div>";
            }
            if (Tongsobanghi % Tongsotrang > 0)
            {
                Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
            }
            else
            {
                Tongsobanghi = Tongsobanghi / Tongsotrang;
            }
            ViewBag.Phantrang = Commond.Phantrang("/danh-muc/" + hp + ".html", Tongsobanghi, pages);

            #endregion
            return View();
        }

        public ActionResult Search()
        {

            #region Category
            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse(Commond.Setting("pagepro"));
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            string keywords = "";
            if (keywords != null || keywords != "")
            {
                keywords = MoreAll.MoreAll.GetCookies("Search").ToString();
            }
            List<Entity.Category_Product> dt = SProducts.SearchPro(keywords, language, "1", (pages - 1), ref Tongsobanghi, Tongsotrang);
            if (dt.Count >= 1)
            {
                ViewBag.ShowList = Commond.LoadProductList(dt);
            }
            else
            {
                ViewBag.ShowList = "<div class='ttimkiem'><p style='margin-top: 0.33em'> Không tìm thấy <span style='color:Red;'> <b>" + keywords + "</b></span> trong tài liệu nào.</p><p style='margin-top: 1em'>Ðề xuất:</p><ul style='margin: 0px 0px 2em 1.3em'> <li>Xin bạn chắc chắn rằng tất cả các từ đều đúng chính tả. </li><li>Hãy thử những từ khoá khác. </li><li>Hãy thử những từ khoá chung hơn.</li></ul></div>";
            }
            if (Tongsobanghi % Tongsotrang > 0)
            {
                Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
            }
            else
            {
                Tongsobanghi = Tongsobanghi / Tongsotrang;
            }
            ViewBag.Phantrang = Commond.Phantrang("/search.html", Tongsobanghi, pages);
            #endregion
            return View();
        }
    }
}