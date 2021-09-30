using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.ECommerce_MVC
{
    public partial class Test : System.Web.UI.Page
    {
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {

            string Create_Date = "2021-09-05 00:00:00.000";
            string Modified_Date = "2021-09-09 00:00:00.000";
           // Response.Write(((Convert.ToDateTime(Modified_Date).Ticks - Convert.ToDateTime(Create_Date).Ticks) / 0xc92a69c000L).ToString());



            DateTime ngaymuon = Convert.ToDateTime("2021-09-05 00:00:00.000");
            DateTime ngaytra = Convert.ToDateTime("2021-09-09 00:00:00.000");
            TimeSpan Time = ngaytra - ngaymuon;
            int TongSoNgay = Time.Days;
            Response.Write("2:<br>" + TongSoNgay);

            //string mang = "0,0,0,1630,0,0,1221,0";
            //string[] strArray = mang.ToString().Split(new char[] { ',' });
            //var b = strArray.Distinct();
            //var c = b.ToList();
            //string chuoi = "0";
            //foreach (var item in c)
            //{
            //    chuoi += "," + item;
            //}
            //chuoi = chuoi.Replace(",0,", ",") + ",0";
            //chuoi = chuoi.Replace(",,", ",");
            //Response.Write(chuoi);

            //string strs = "";
            //List<ThanhVien> dt = ShowThongTinThanhVien();
            //if (dt != null)
            //{
            //    foreach (var item in dt)
            //    {
            //        strs += item.SanPham + " -" + item.Name + " -" + item.ThuTu + "<br>";
            //    }
            //}
            //Response.Write(strs);
        }

        protected string Danhsachtin()
        {
            string str = "";
            var cc = from News in db.News
                     where (new int[] { 786, 787 }).Contains(News.icid.GetValueOrDefault()) && News.lang == "VIE" && News.Status == 1 && (News.Create_Date <= DateTime.Now && DateTime.Now <= News.Modified_Date)
                     orderby News.Create_Date descending
                     select News;
            foreach (var item in cc)
            {
                str += item.Title.ToString() + "</br>-==-";
            }
            return str.ToString();
        }


        ////// VD1 lấy ra 2 bảng và trả về theo phương thức được khai báo trong ThanhVien
        ////public static List<ThanhVien> ShowThongTinThanhVien()
        ////{
        ////    DatalinqDataContext db = new DatalinqDataContext();
        ////    var query = (from s in db.Tes1s
        ////                 join c in db.Tes2s on s.ID equals c.IDTes1
        ////                 select new ThanhVien()
        ////                 {
        ////                     Name = s.Ten,
        ////                     SanPham = c.Name,
        ////                     ThuTu = c.ID
        ////                 }).OrderBy(x => x.ThuTu);
        ////    return query.ToList();
        ////}
        ////public class ThanhVien
        ////{
        ////    public string Name { get; set; }
        ////    public string SanPham { get; set; }
        ////    public int ThuTu { get; set; }
        ////    public Double Tong { get; set; }
        ////}
        // VD2 lấy 1 bảng
        //public static List<ThanhVien> ShowThongTinThanhVien2()
        //{
        //    DatalinqDataContext db = new DatalinqDataContext();
        //    var query = from p in db.Members
        //             select new ThanhVien
        //             {
        //                 Name = p.HoVaTen
        //             };
        //    return query.ToList();
        //}



    }
}