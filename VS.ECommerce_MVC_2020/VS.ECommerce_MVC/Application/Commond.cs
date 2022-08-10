using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using VS.ECommerce_MVC;

public class Commond
{
    //string s1 = "abcd";
    //string s2 = "";
    //string s3 = null;
    //if (!String.IsNullOrEmpty(s1))
    //{
    //    Response.Write(s1 + "<br>");
    //}
    //else
    //{
    //    Response.Write("Rỗng 1<br>");
    //}

    public const string SessionCart = "SessionCart";

    #region Thành viên
    public static string ShowAnhDaiDien(string status)
    {
        if (status.Length > 3)
        {
            return "<img src=\"" + status + "\" width=\"105px\" />";
        }
        return "";
    }

    public static string ShowCapbac(string status)
    {
        if (status.Equals("1"))
        {
            return "<span class='ShowCapbac1'>Đại lý Cấp 1</span>";
        }
        else if (status.Equals("2"))
        {
            return "<span class='ShowCapbac2'>TN Kinh Doanh</span>";
        }
        else if (status.Equals("3"))
        {
            return "<span class='ShowCapbac3'>TP Kinh Doanh</span>";
        }
        else if (status.Equals("4"))
        {
            return "<span class='ShowCapbac4'>GĐ Kinh Doanh</span>";
        }
        else if (status.Equals("5"))
        {
            return "<span class='ShowCapbac5'>GĐ Vùng</span>";
        }
        else if (status.Equals("6"))
        {
            return "<span class='ShowCapbac6'>GĐ Miền</span>";
        }
        return "<span class='ShowCapbac0'>Thành viên</span>";
    }
    public static string TrangThaiThamGiaCoDong(string status)
    {
        if (status.Equals("1"))
        {
            return "<span class='ShowCapbac3'>Tham gia gói cổ đông</span>";
        }
        return "<span class='ShowCapbac0'>Chưa tham gia</span>";
    }
    public static string TrangThaiMuaHang(string status)
    {
        if (status.Equals("1"))
        {
            return "<span class='ShowCapbac3'>Đã mua hàng</span>";
        }
        return "<span class='ShowCapbac0'>Chưa mua hàng</span>";
    }

    public static string TrangThaiThamGiaCoDongMTRe(string status)
    {
        if (status.Equals("1"))
        {
            return "<span class='ShowCapbac3'>Tham gia gói cổ đông</span>";
        }
        return "";
    }
    public static string ShowTinhthanh(string id)
    {
        try
        {
            DatalinqDataContext db = new DatalinqDataContext();
            List<Tinhthanh> data = db.ExecuteQuery<Tinhthanh>(@"SELECT * FROM Tinhthanh where id=" + id + " order by ID asc").ToList();
            if (data.Count > 0)
            {
                return data[0].Name;
            }
        }
        catch (Exception)
        { }
        return "0";
    }

    public static string SumTongF1ThangTruoc(string id)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        string str = "0";
        try
        {
            List<Entity.Member> dt = SMember.Name_Text("SELECT * FROM [dbo].[Members] WHERE GioiThieu =" + id + "    and month(NgayTao)=  MONTH(dateadd (month,-1, GETDATE()))  and year(NgayTao)= YEAR(dateadd (YEAR,0, GETDATE()-30 )) ");
            return dt.Count.ToString();
        }
        catch (Exception)
        { }
        return "0";
    }
    public static string SumTongF1ThangNay(string id)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        string str = "0";
        try
        {
            List<Entity.Member> dt = SMember.Name_Text("SELECT * FROM [dbo].[Members] WHERE GioiThieu=" + id + "   and  month(NgayTao)=  MONTH(dateadd (month,0, GETDATE()))  and year(NgayTao)= YEAR(dateadd (YEAR,0, GETDATE())) ");
            return dt.Count.ToString();
        }
        catch (Exception)
        { }
        return "0";
    }
    public static string SumTongCDuoiThangNay(string id)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        string str = "0";
        try
        {
            List<Entity.Member> dt = SMember.Name_Text("SELECT * FROM [dbo].[Members] WHERE  (([MTree] LIKE N'%|" + id + "|%')) and ID!=" + id + "   and  month(NgayTao)=  MONTH(dateadd (month,0, GETDATE()))  and year(NgayTao)= YEAR(dateadd (YEAR,0, GETDATE())) ");
            return dt.Count.ToString();
        }
        catch (Exception)
        { }
        return "0";
    }
    public static string SumTongTVHeThong(string id)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        string str = "0";
        try
        {
            List<Entity.Member> dt = SMember.Name_Text("SELECT * FROM [dbo].[Members] WHERE  (([MTree] LIKE N'%|" + id + "|%')) and ID!=" + id + "  ");
            return dt.Count.ToString();
        }
        catch (Exception)
        { }
        return "0";
    }
    public static string SumThanhVienCapDuoiF1(string id)
    {
        try
        {
            List<Entity.Member> dt = SMember.Name_Text("select * from [Members]  where GioiThieu =" + id + "");
            return dt.Count().ToString();
        }
        catch (Exception)
        {
        }
        return "0";
    }
    public static string SumThanhVienCapDuoi(string id)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        string str = "0";
        try
        {
            List<Entity.Member> dt = SMember.Name_Text("SELECT * FROM [dbo].[Members] WHERE(([MTree] LIKE N'%|" + id + "|%')) and ID!=" + id + "  and TrangThai=1");
            return dt.Count.ToString();
        }
        catch (Exception)
        { }
        return "0";
    }
    public static string SumSoTienHopDongToanHeT(string id)
    {
        try
        {
            DatalinqDataContext db = new DatalinqDataContext();
            var dt = db.Show_TongDoanhSo(id).ToList();
            return dt[0].TongTien.ToString();
        }
        catch (Exception)
        {
        }
        return "0";
    }

    public static string FormatDate_IDQR(object date)
    {
        return (Convert.ToDateTime(date).ToString("MMddyyhhmmss"));
    }
    public static bool Check(object String)
    {
        return ((String != null) && (String.ToString().Trim().Length > 0));
    }
    public static DateTime ConvertStringToDate(string Date, string FromFormat)
    {
        return DateTime.ParseExact(Date, FromFormat, null);
    }
    public static string FormatDate(object date)
    {
        if (date != null)
        {
            if (date.ToString().Trim().Length > 0 && date != null)
            {
                if (DateTime.Parse(date.ToString()).Year != 1900)
                {
                    DateTime dNgay = Convert.ToDateTime(date.ToString());
                    return ((DateTime)dNgay).ToString("yyyy-MM-dd");
                }
            }

        }
        return "";
    }
    public static string TachMtre(string MTRee)
    {
        return MTRee.Replace("|", " |");
    }
    public static string ShowThanhVien_Display(string id)
    {
        string str = "";
        List<Entity.Member> dt = SMember.GET_BY_ID(id);
        if (dt.Count >= 1)
        {
            str += dt[0].HoVaTen + " - " + dt[0].DienThoai;
        }
        return str;
    }
    public static string ShowThanhVienEXel(string id)
    {
        string str = "";
        List<Entity.Member> dt = SMember.GET_BY_ID(id);
        if (dt.Count >= 1)
        {
            str += dt[0].HoVaTen + " - " + dt[0].DienThoai;
        }
        else
        {
            str = "Không tìm thấy thành viên";
        }
        return str;
    }
    public static string ShowMTree(string id)
    {
        string str = "";
        try
        {
            if (id != "0" || id != "")
            {
                List<Entity.Member> dt = SMember.GET_BY_ID(id);
                if (dt.Count >= 1)
                {
                    str = dt[0].MTRee;
                }
            }
        }
        catch (Exception)
        { }
        return str;
    }
    public static string SearchThanhVien(string keyword)
    {
        string str = "0";
        List<Entity.Member> dt = SMember.Name_Text("select * from Members where (HoVaTen like N'%" + keyword + "%' or DienThoai like N'%" + keyword + "%')");
        if (dt.Count >= 1)
        {
            for (int i = 0; i < dt.Count; i++)
            {
                str = str + "," + dt[i].ID.ToString();
            }
        }
        return str.Replace("0,", "");
    }

    public static string ShowThanhVien(string id)
    {
        string str = "";
        List<Entity.Member> dt = SMember.GET_BY_ID(id);
        if (dt.Count >= 1)
        {
            str += "<span id=" + dt[0].ID.ToString() + " style=\" color:red\">";
            if (dt[0].HoVaTen.ToString().Length > 0)
            {
                str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].ID.ToString() + "\"><span style='color:red'>" + dt[0].HoVaTen + " [Level " + dt[0].CapBac + "]</span></a>";
            }
            str += "</span> - ";
            if (dt[0].DienThoai.ToString().Length > 0)
            {
                str += dt[0].DienThoai;
            }
        }
        else
        {
            str = "Không tìm thấy thành viên";
        }
        return str;
    }

    #endregion

    #region Modul Product
    public static string ShowNamePro(string ID)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        product str = db.products.SingleOrDefault(p => p.ipid == int.Parse(ID));
        if (str != null)
        {
            return str.Name.ToString();
        }
        return "";
    }
    public static string Sql_Product()
    {
        return "Alt,ipid,icid,TangName,Name,Images,ImagesSmall,Brief,Create_Date,Price,OldPrice,ID_Hang,sanxuat,Code";
    }

    #region Hiển thị bao nhiêu sản phẩm ở nhóm trang chủ
    public static string HomePage()
    {
        return Commond.Setting("HomePage");
    }
    #endregion

    public static string GiamgiaNews(string Price, string OldPrice)
    {
        string Width = "";
        if (Price.ToString() != "" && OldPrice.ToString() != "")
        {
            if (Convert.ToDouble(OldPrice.ToString()) > Convert.ToDouble(Price.ToString()))
            {
                double cu = Convert.ToDouble(OldPrice.ToString());
                double hientai = Convert.ToDouble(Price.ToString());
                double Tong = (((cu - hientai) / cu) * 100);
                Tong = System.Math.Round(Tong, 0);
                if (Tong != 0)
                {
                    Width += "    <div class=\"sale-flash\">";
                    Width += "      <div class=\"before\"></div> - " + Tong.ToString() + "%";
                    Width += "    </div>";
                }
            }
        }
        return Width.ToString();
    }
    public static string Giamgia(string ID)
    {
        string Width = "";
        DatalinqDataContext db = new DatalinqDataContext();
        product str = db.products.SingleOrDefault(p => p.ipid == int.Parse(ID));
        if (str != null)
        {
            if (str.Price.ToString() == "" || str.OldPrice.ToString() == "")
            {
            }
            else if (Convert.ToDouble(str.OldPrice.ToString()) > Convert.ToDouble(str.Price.ToString()))
            {
                double cu = Convert.ToDouble(str.OldPrice.ToString());
                double hientai = Convert.ToDouble(str.Price.ToString());
                double Tong = (((cu - hientai) / cu) * 100);
                Tong = System.Math.Round(Tong, 0);
                if (Tong != 0)
                {
                    Width += "    <div class=\"sale-flash\">";
                    Width += "      <div class=\"before\"></div> - " + Tong.ToString() + "%";
                    Width += "    </div>";
                }
            }
        }
        return Width.ToString();
    }
    public static string LoadProductList_Home(IEnumerable<Entity.Category_Product> product)
    {
        string str = "";
        foreach (Entity.Category_Product item in product)
        {
            str += "  <div class=\"product-box\">";
            str += "  <div class=\"product-thumbnail flexbox-grid\">";
            str += " <a  href='/san-pham/" + item.TangName + ".html' title=\"" + item.Name + "\">" + AllQuery.MorePro.Image_Title_Alts(item.ImagesSmall.ToString(), item.Name.ToString(), item.Alt.ToString()) + "</a>";
            str += Giamgia(item.ipid.ToString());
            str += "  </div>";
            str += "  <div class=\"product-info a-center\">";
            str += "    <h3 class=\"product-name\">";
            str += "      <a  href='/san-pham/" + item.TangName + ".html' title=\"" + item.Name + "\">" + AllQuery.MorePro.Substring_Title(item.Name.ToString()) + "</a>";
            str += "    </h3>";
            str += "    <div class=\"bizweb-product-reviews-badge\" ></div>";
            str += "    <div class=\"price-box clearfix\">";
            str += "      <div class=\"special-price\">";
            str += "        <span class=\"price product-price\">" + AllQuery.MorePro.FormatMoney(item.Price.ToString()) + "</span>";
            str += "      </div>";
            str += "      <div class=\"old-price\">";
            str += "        <span class=\"price product-price-old\">" + AllQuery.MorePro.Detail_Price(item.OldPrice.ToString()) + "</span>";
            str += "      </div>";
            str += "    </div>";

            str += "<a href=\"javascript:void(0)\"   onclick=\"UpdateOrder('" + item.ipid.ToString() + "','" + item.Name + "')\" class=\" cart_button_style btn-cart left-to add_to_cart evo-header-cart\" title=\"Cho vào giỏ hàng\"><span><span class=\"ti-bag\"></span></span>cart_sidebar  Giỏ hàng</a>";

            str += "<a href=\"javascript:void(0)\" rel=\"popuprel3\" onclick=\"UpdateOrder('" + item.ipid.ToString() + "','" + item.Name + "')\" class=\" cart_button_style btn-cart left-to add_to_cart popup\" title=\"Cho vào giỏ hàng\"><span><span class=\"ti-bag\"></span></span> Giỏ hàng</a>";
            str += "  </div>";
            str += "</div>";
        }
        return str;
    }
    public static string LoadProductList(IEnumerable<Entity.Category_Product> product)
    {
        string str = "";
        foreach (Entity.Category_Product item in product)
        {
            str += "<div class=\"col-xs-6 col-sm-12 col-md-3 col-lg-3 product-col oother\">";
            str += "<div class=\"product-box\">";
            str += "<div class=\"product-thumbnail\">";
            // str += Giamgia(item.ipid.ToString());
            str += " <a class=\"image_link display_flex\"  href='/san-pham/" + item.TangName + ".html' title=\"" + item.Name + "\">" + AllQuery.MorePro.Image_Title_Alts(item.ImagesSmall.ToString(), item.Name.ToString(), item.Alt.ToString()) + "</a>";
            str += "</div>";
            str += "<div class=\"product-info a-center\">";
            str += "<h3 class=\"product-name\"><a class=\"text2line\" href='/san-pham/" + item.TangName + ".html' title=\"" + item.Name + "\">" + AllQuery.MorePro.Substring_Title(item.Name.ToString()) + "</a></h3>";
            str += "<div class=\"price-box clearfix\">";
            str += "<span class=\"price product-price\">" + AllQuery.MorePro.FormatMoney(item.Price.ToString()) + "</span>";
            str += "<span class=\"price product-price-old\">" + AllQuery.MorePro.Detail_Price(item.OldPrice.ToString()) + "</span>";
            str += "</div>";
            str += "<div class=\"action__\">";
            str += "<div  class=\"variants form-nut-grid\" >";
            str += "<div>";
            str += "<a href=\"javascript:void(0)\"   onclick=\"UpdateOrder('" + item.ipid.ToString() + "','" + item.Name + "')\" class=\" cart_button_style btn-cart left-to add_to_cart evo-header-cart\" title=\"Cho vào giỏ hàng\"><span><span class=\"ti-bag\"></span></span>cart_sidebar  Giỏ hàng</a>";

            str += "<a href=\"javascript:void(0)\" rel=\"popuprel3\" onclick=\"UpdateOrder('" + item.ipid.ToString() + "','" + item.Name + "')\" class=\" cart_button_style btn-cart left-to add_to_cart popup\" title=\"Cho vào giỏ hàng\"><span><span class=\"ti-bag\"></span></span> Giỏ hàng</a>";
            str += "</div>";
            str += "</div>";
            str += "</div>";
            str += "</div>";
            str += "</div>";
            str += "</div>";
        }
        return str;
    }
    #endregion

    #region Modul News
    public static string Sql_News()
    {
        return "inid,Alt,TangName,Title,Images,ImagesSmall,Brief,Create_Date";
    }
    public static string LoadNewsListHome(IEnumerable<Entity.Category_News> news, string language)
    {
        string str = "";
        foreach (Entity.Category_News item in news)
        {
            str += "<div class=\"item_blog_owl\">";
            str += " <article class=\"blog-item blog-item-grid row\">";
            str += "  <div class=\"wrap_blg\">";
            str += "   <div class=\"blog-item-thumbnail img1 col-lg-12 col-md-12 col-sm-12 col-xs-12\">";
            str += "<a href=\"/tin-tuc/" + item.TangName + ".html\" title=\"" + item.Title + "\">" + AllQuery.MoreNews.Image_Title_Alt(item.ImagesSmall.ToString(), item.Title.ToString(), item.Alt.ToString()) + "</a>";
            str += "   </div>";
            str += "   <div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12 content_ar blog_large_item\">";
            // str += "       <span class=\"blog_name\">Tin tức</span>";
            str += "       <h3 class=\"blog-item-name large_name\">";
            str += "<a class=\"text1line\"  href=\"/tin-tuc/" + item.TangName + ".html\"  title=\"" + item.Title + "\">" + AllQuery.MoreNews.Substring_Title(item.Title.ToString()) + "</a>";
            str += "</h3>";
            str += " <div class=\"summary_ text3line\">";
            str += AllQuery.MoreNews.Substring_Mota(item.Brief.ToString());
            str += "</div>";
            str += "   </div>";
            str += "  </div>";
            str += " </article>";
            str += "  </div>";
        }
        return str;
    }
    public static string LoadNewsListHome_CanhBanner(IEnumerable<Entity.Category_News> news, string language)
    {
        string str = "";
        foreach (Entity.Category_News item in news)
        {
            str += " <article class=\"blog-item blog-item-list \">";
            str += "  <div class=\"blog-item-thumbnail thumb_mini img1\" >";
            str += "<a href=\"/tin-tuc/" + item.TangName + ".html\" title=\"" + item.Title + "\">" + AllQuery.MoreNews.Image_Title_Alt_NWH(item.ImagesSmall.ToString(), item.Title.ToString(), item.Alt.ToString()) + "</a>";
            str += "  </div>";
            str += "  <div class=\"ct_list_item content_mini\">";
            str += "    <h3 class=\"blog-item-name\">";
            str += "<a class=\"text2line\"  href=\"/tin-tuc/" + item.TangName + ".html\"  title=\"" + item.Title + "\">" + AllQuery.MoreNews.Substring_Title(item.Title.ToString()) + "</a>";
            str += "    </h3>";
            str += "    <span class=\"post_\">";
            str += "      <span>";
            str += "        <span class=\"ti-time\"></span>&nbsp; " + MoreAll.FormatDateTime.FormatDate_Brithday(item.Create_Date.ToString());
            str += "      </span>";
            str += "    </span>";
            str += "  </div>";
            str += "</article>";
        }
        return str;
    }
    public static string LoadNewsListHome_Banner(IEnumerable<Entity.Category_News> news, string language)
    {
        string str = "";
        foreach (Entity.Category_News item in news)
        {
            str += "<article class=\"blog-item blog-item-grid row\">";
            str += "<div class=\"wrap_blg\">";
            str += "<div class=\"blog-item-thumbnail img1 col-lg-12 col-md-12 col-sm-12 col-xs-12 ccccbbb\" >";
            str += "<a href=\"/tin-tuc/" + item.TangName + ".html\" title=\"" + item.Title + "\">" + AllQuery.MoreNews.Image_Title_Alt_NWH(item.Images.ToString(), item.Title.ToString(), item.Alt.ToString()) + "</a>";
            str += "</div>";
            str += "<div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12 content_ar blog_large_item\">";
            //str += "<span class=\"blog_name\">Tin tức</span>";
            str += "<h3 class=\"blog-item-name large_name\">";
            str += "<a class=\"text2line\"  href=\"/tin-tuc/" + item.TangName + ".html\"  title=\"" + item.Title + "\">" + item.Title.ToString() + "</a>";
            str += "</h3>";
            str += "<span class=\"post_\">";
            str += "<span>";
            str += "<span class=\"ti ti-user\"></span>&nbsp; Admin";
            str += "</span>";
            str += "<span class=\"margin-left-15\">";
            str += "<span class=\"ti ti-time\"></span>&nbsp; " + MoreAll.FormatDateTime.FormatDate_Brithday(item.Create_Date.ToString());
            str += "</span>";
            str += "</span>";
            str += "<div class=\"summary_ text3line\">" + AllQuery.MoreNews.Substring_Mota(item.Brief.ToString()) + "</div>";
            str += "</div>";
            str += "</div>";
            str += "</article>";
        }
        return str;
    }

    //public static string LoadNewsList(IEnumerable<Entity.Category_News> news, string language)
    //{
    //    string str = "";
    //    foreach (Entity.Category_News item in news)
    //    {
    //        str += "<div class=\"item-news\">";
    //        str += "<div class=\"img\"><a href=\"/" + item.TangName + ".html\" title=\"" + item.Title + "\">" + AllQuery.MoreNews.Image_Title_Alt(item.ImagesSmall.ToString(), item.Title.ToString(), item.Alt.ToString()) + "</a></div>";
    //        str += " <div class=\"title-news\"><a href=\"/" + item.TangName + ".html\"  title=\"" + item.Title + "\">" + AllQuery.MoreNews.Substring_Title(item.Title.ToString()) + "</a></div>";
    //        str += "<div class=\"des-news\">" + AllQuery.MoreNews.Substring_Mota(item.Brief.ToString()) + "</div>";
    //        str += " <div class=\"chitiet\"><a href=\"/" + item.TangName + ".html\"  title=\"" + item.Title + "\">" + Captionlanguage.GetLabel(("l_viewdetail"), language) + "</a></div>";
    //        str += "</div>";
    //    }
    //    return str;
    //}
    public static string LoadNewsList(IEnumerable<Entity.Category_News> news, string language)
    {
        string str = "";
        foreach (Entity.Category_News item in news)
        {
            str += "<div class=\"col-lg-4 col-md-4 col-sm-6 col-xs-12\">";
            str += "<div class=\"item_blog_owl\">";
            str += "<article class=\"blog-item blog-item-grid row\">";
            str += "<div class=\"wrap_blg\">";
            str += "<div class=\"blog-item-thumbnail img1 col-lg-12 col-md-12 col-sm-12 col-xs-12\" >";
            str += "<a href=\"/tin-tuc/" + item.TangName + ".html\" title=\"" + item.Title + "\">" + AllQuery.MoreNews.Image_Title_Alt(item.ImagesSmall.ToString(), item.Title.ToString(), item.Alt.ToString()) + "</a>";
            str += "</div>";
            str += "<div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12 content_ar blog_large_item\">";
            //str += "<span class=\"blog_name\">Tin tức</span>";
            str += "<h3 class=\"blog-item-name\"><a class=\"text2line\"  href=\"/tin-tuc/" + item.TangName + ".html\"  title=\"" + item.Title + "\">" + AllQuery.MoreNews.Substring_Title(item.Title.ToString()) + "</a></h3>";
            str += "<div class=\"summary_ sum text4line\">";
            str += AllQuery.MoreNews.Substring_Mota(item.Brief.ToString());
            str += "</div>";
            str += "</div>";
            str += "</div>";
            str += "</article>";
            str += "</div>";
            str += "</div>";
        }
        return str;
    }
    #endregion

    #region ALbum
    public static string LoadALbum_Home(IEnumerable<Entity.Album_RutGon> product)
    {
        string str = "";
        foreach (Entity.Album_RutGon item in product)
        {
            str += "        <li class=\" abcolmd\">";
            str += "            <div class=\"abitem\">";
            str += "                <div class=\"img\">";
            str += "                    <a title=\"" + item.Title + "\" href='/album/" + item.TangName + ".html'>" + MoreAll.MoreImage.Image_width_height_Title_Alt(item.ImagesSmall.ToString(), "195", "146", item.Title.ToString(), item.Alt.ToString()) + " <div class=\"imghover\"></div>";
            str += "                    </a>";
            str += "                </div>";
            str += "                <div class=\"tiemtitle\">";
            str += "                    <h2><a title=\"" + item.Title + "\" href='/album//item.TangName.html'>" + item.Title + "</a></h2>";
            str += "                </div>";
            str += "            </div>";
            str += "        </li>";
        }
        return str;
    }

    #endregion

    #region Video
    public static string LoadVideo_Home(IEnumerable<Entity.VideoClip_RutGon> product)
    {
        string str = "";
        foreach (Entity.VideoClip_RutGon item in product)
        {

            str += "<div class=\"col-xs-12 col-md-6 col-sm-6\">";
            str += "<div class=\"artitle-item \">";
            str += "<div class=\"video-container\">";
            str += "<iframe width=\"100%\" class='Videoyoutube' src=\"https://www.youtube.com/embed/" + item.Youtube + "\" frameborder=\"0\" allowfullscreen=\"\"></iframe>";
            str += "</div>";
            str += "<div class=\"article-info-box\">";
            str += "<a title=\"" + item.Title + "\" href=\"/video/" + item.TangName + ".html\" class='title'><h2>" + item.Title + "</h2></a>";

            str += "<div class=\"description\">" + item.Brief + "</div>";
            str += "<a title=\"" + item.Title + "\" href=\"/video/" + item.TangName + ".html\" class=\"viewmore\"> Xem thêm <i class=\"fa fa-angle-right\"></i>";
            str += "</a>";
            str += "</div>";
            str += "</div>";
            str += "</div>";

            //str += "<div class=\"vd-item\">";
            //str += "<div class=\"img\">";
            //str += "    <a title=\"" + item.Title + "\" href=\"/video/" + item.TangName + ".html\">" + MoreAll.MoreImage.Image_width_height_Title_Alt(item.ImagesSmall.ToString(), "195", "146", item.Title.ToString(), item.Alt.ToString()) + "</a>";
            //str += "    <div class=\"pl\"><a title=\"" + item.Title + "\" href=\"/video/" + item.TangName + ".html\">";
            //str += "        <img src=\"/Resources/images/play.png\" /></a></div>";
            //str += "</div>";
            //str += "<span><a title=\"" + item.Title + "\" href=\"/video/" + item.TangName + ".html\">" + item.Title + "</a></span>";
            //str += "</div>";
        }
        return str;
    }
    #endregion

    #region All More
    public static string Name(string ID) //// Tăng và giảm thứ tự trong ô txtOrders
    {
        DatalinqDataContext db = new DatalinqDataContext();
        Menu table = db.Menus.SingleOrDefault(p => p.ID == int.Parse(ID));
        if (table != null)
        {
            return table.Name.ToString();
        }
        return "";
    }

    public static string RequestMenu(string Request)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        string Modul = "";
        ModulControl str = db.ModulControls.SingleOrDefault(p => p.TangName == Request);
        if (str != null)
        {
            Modul = str.Module.ToString();
        }
        return Modul;
    }
    public static string Setting(string giatri)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        string item = "";
        Setting str = db.Settings.SingleOrDefault(p => p.Properties == giatri && p.Lang == MoreAll.MoreAll.Language);
        if (str != null)
        {
            item = str.Value;
        }
        return item.ToString();
    }
    public static string SubMenu(string Capp, string cid)
    {
        string submn = cid;
        List<Entity.MenuID> dt = SMenu.Menu_ID(cid, Capp);
        for (int i = 0; i < dt.Count; i++)
        {
            submn = submn + "," + SubMenu(Capp, dt[i].ID.ToString());
        }
        return submn;
    }

    #endregion

    #region Phân trang
    public static string Phantrang_loc(string Url, string UrlLoc, int Tongsobanghi, Int16 pages)
    {
        // Bổ xung id vào để phục vụ phần lọc js,  theo mầu , kích thước, giá, ... ko bị lỗi
        string str = "<div class='Phantrang'>";
        if (Tongsobanghi > 1)
        {
            str += "<a id=\"1\" href='" + Url + "?page=1" + UrlLoc + "'><< Trang đầu</a>";
            for (int i = 1; i <= Tongsobanghi; i++)
            {
                if (i == pages)
                {
                    str += "<a class='pageactive' id=\"" + i + "\" href=\"" + Url + "?page=" + i + "" + UrlLoc + "\">" + i + "</a>";
                }
                else
                {
                    str += "<a class='page' id=\"" + i + "\" href=\"" + Url + "?page=" + i + "" + UrlLoc + "\">" + i + "</a>";
                }
            }
            str += "<a id=\"" + Tongsobanghi + "\" href='" + Url + "?page=" + Tongsobanghi + "" + UrlLoc + "'>Cuối cùng >></a>";
        }
        str += "</div>";
        return str;
    }
    //Old
    //public static string Phantrang(string Url, int Tongsobanghi, Int16 pages)
    //{
    //    string str = "<div class='Phantrang'>";
    //    if (Tongsobanghi > 1)
    //    {
    //        str += "<a href='" + Url + "?page=1'><< Trang đầu</a>";
    //        for (int i = 1; i <= Tongsobanghi; i++)
    //        {
    //            if (i == pages)
    //            {
    //                str += "<a class='pageactive' href=\"" + Url + "?page=" + i + "\">" + i + "</a>";
    //            }
    //            else
    //            {
    //                str += "<a class='page' href=\"" + Url + "?page=" + i + "\">" + i + "</a>";
    //            }
    //        }
    //        str += "<a href='" + Url + "?page=" + Tongsobanghi + "'>Cuối cùng >></a>";
    //    }
    //    str += "</div>";
    //    return str;
    //}
    //News
    public static string PhantrangAdmin(string Url, int Tongsobanghi, int pages)
    {
        string str = "<div class='PhantrangAD'>";
        if (Tongsobanghi > 1)
        {
            str += "<a href='" + Url + "&page=1'><< Trang đầu</a>";
            int startPage;
            if (Tongsobanghi <= 7)
                startPage = 1;
            else if (pages <= 4)
                startPage = 1;
            else if ((Tongsobanghi - pages) >= 3)
                startPage = pages - 3;
            else startPage = Tongsobanghi - 6;
            if (startPage > 1)
                str += string.Format("<a class=\"aso\">...</a>");
            for (int i = startPage; i <= (Tongsobanghi <= 7 ? Tongsobanghi : startPage + 6); i++)
            {
                if (i == pages)
                {
                    str += "<a class='pageactive' href=\"" + Url + "&page=" + i + "\">" + i + "</a>";
                }
                else
                {
                    str += "<a class='page' href=\"" + Url + "&page=" + i + "\">" + i + "</a>";
                }
            }
            if ((Tongsobanghi - pages > 3) && (Tongsobanghi > 7))
                str += string.Format("<a class=\"aso\">...</a>");
            str += "<a href='" + Url + "&page=" + Tongsobanghi + "'>Cuối cùng >></a>";
        }
        str += "</div>";
        return str;
    }
    public static string Phantrang(string Url, int Tongsobanghi, int pages)
    {
        string str = "<div class='Phantrang'>";
        if (Tongsobanghi > 1)
        {
            str += "<a href='" + Url + "?page=1'><< Trang đầu</a>";
            int startPage;
            if (Tongsobanghi <= 7)
                startPage = 1;
            else if (pages <= 4)
                startPage = 1;
            else if ((Tongsobanghi - pages) >= 3)
                startPage = pages - 3;
            else startPage = Tongsobanghi - 6;
            if (startPage > 1)
                str += string.Format("<a class=\"aso\">...</a>");
            for (int i = startPage; i <= (Tongsobanghi <= 7 ? Tongsobanghi : startPage + 6); i++)
            {
                if (i == pages)
                {
                    str += "<a class='pageactive' href=\"" + Url + "?page=" + i + "\">" + i + "</a>";
                }
                else
                {
                    str += "<a class='page' href=\"" + Url + "?page=" + i + "\">" + i + "</a>";
                }
            }
            if ((Tongsobanghi - pages > 3) && (Tongsobanghi > 7))
                str += string.Format("<a class=\"aso\">...</a>");
            str += "<a href='" + Url + "?page=" + Tongsobanghi + "'>Cuối cùng >></a>";
        }
        str += "</div>";
        return str;
    }
    #endregion

    #region Truy Vấn Linq
    public static IEnumerable<Menu> Name_Text_Menu(string sql)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        return db.ExecuteQuery<Menu>(@"" + sql + "");
    }
    public static IEnumerable<product> Name_Text_Pro(string sql)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        return db.ExecuteQuery<product>(@"" + sql + "");
    }
    public static IEnumerable<New> Name_Text_News(string sql)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        return db.ExecuteQuery<New>(@"" + sql + "");
    }

    #endregion

    #region All Tổng hợp
    public static string ShowActive(string id)
    {
        List<Entity.Menu> dt = SMenu.Detail(id);
        if (dt.Count > 0)
        {
            return dt[0].ID + " - " + dt[0].Name;
        }
        return "";
    }

    public static string DateTime_All(object date)
    {
        return (Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
    }
    public static string ShowSumRank(string proRateSum, string proRateCount)
    {
        float result = 0;
        if (proRateSum != "0")
            result = Convert.ToInt32(proRateCount) / Convert.ToInt32(proRateSum);
        if (result > 100)
        {
            return "100";
        }
        if (result <= 0)
        {
            return "0";
        }
        return result.ToString();
    }
    public static string ShowRankThanhVien(string rank1, string rank2, string rank3, string rank4, string rank5)
    {
        if (rank1 == ("1"))
        {
            return "<i class=\"fa fa-star co-or\"></i>  <i class=\"fa fa-star-o co-or\"></i><i class=\"fa fa-star-o co-or\"></i><i class=\"fa fa-star-o co-or\"></i><i class=\"fa fa-star-o co-or\"></i>";
        }
        if (rank2 == ("1"))
        {
            return "<i class=\"fa fa-star co-or\"></i>  <i class=\"fa fa-star co-or\"></i><i class=\"fa fa-star-o co-or\"></i><i class=\"fa fa-star-o co-or\"></i><i class=\"fa fa-star-o co-or\"></i>";
        }
        if (rank3 == ("1"))
        {
            return "<i class=\"fa fa-star co-or\"></i>  <i class=\"fa fa-star co-or\"></i><i class=\"fa fa-star co-or\"></i><i class=\"fa fa-star-o co-or\"></i><i class=\"fa fa-star-o co-or\"></i>";
        }
        if (rank4 == ("1"))
        {
            return "<i class=\"fa fa-star co-or\"></i>  <i class=\"fa fa-star co-or\"></i><i class=\"fa fa-star co-or\"></i><i class=\"fa fa-star co-or\"></i><i class=\"fa fa-star-o co-or\"></i>";
        }
        if (rank5 == ("1"))
        {
            return "<i class=\"fa fa-star co-or\"></i>  <i class=\"fa fa-star co-or\"></i><i class=\"fa fa-star co-or\"></i><i class=\"fa fa-star co-or\"></i><i class=\"fa fa-star co-or\"></i>";
        }
        return "";
    }
    public class ConfigHelper
    {
        public static string GetByKey(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
    }
    public static string CheckBrowserCaps()
    {
        System.Web.HttpBrowserCapabilities myBrowserCaps = System.Web.HttpContext.Current.Request.Browser;
        if (((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice)
        {
            return "Mobile";
            //labelText = "Trình duyệt là một thiết bị di động.";
            // Response.Redirect(".aspx");
        }
        return "Destop";
    }


    public static string Link301()
    {
        string ssl = "";
        if (Commond.Setting("SSL").Equals("1"))
        {
            ssl = "https://";
        }
        string bc = System.Web.HttpContext.Current.Request.Url.Authority + System.Web.HttpContext.Current.Request.RawUrl.ToString();
        if (!bc.Contains("localhost"))
        {
            List<Entity.Menu> dt = SMenu.Name_Text("SELECT * FROM [Menu] where capp='" + More.SC + "'  and Status=1  and Name like N'%" + bc.Replace("/Page/Error?aspxerrorpath=", "") + "'");
            if (dt.Count > 0)
            {
                System.Web.HttpContext.Current.Response.Redirect(dt[0].Description);
            }
        }
        System.Web.HttpContext.Current.Response.Redirect("/page-404.html");
        return "";
    }
    public static string label(string id)
    {
        string language = Captionlanguage.Language;
        if (System.Web.HttpContext.Current.Session["language"] != null)
        {
            language = System.Web.HttpContext.Current.Session["language"].ToString();
        }
        else
        {
            System.Web.HttpContext.Current.Session["language"] = language;
            language = System.Web.HttpContext.Current.Session["language"].ToString();
        }
        return Captionlanguage.GetLabel(id, language);
    }
    #endregion


}
