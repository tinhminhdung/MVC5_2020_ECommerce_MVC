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
        protected void Page_Load(object sender, EventArgs e)
        {
            string strs = "";
            List<ThanhVien> dt = ShowThongTinThanhVien();
            if (dt != null)
            {
                foreach (var item in dt)
                {
                    strs += item.SanPham + " -" + item.Name + " -" + item.ThuTu + "<br>";
                }
            }
            Response.Write(strs);
        }

        // VD1 lấy ra 2 bảng và trả về theo phương thức được khai báo trong ThanhVien
        public static List<ThanhVien> ShowThongTinThanhVien()
        {
            DatalinqDataContext db = new DatalinqDataContext();
            var query = (from s in db.Tes1s
                         join c in db.Tes2s on s.ID equals c.IDTes1
                         select new ThanhVien()
                         {
                             Name = s.Ten,
                             SanPham = c.Name,
                             ThuTu = c.ID
                         }).OrderBy(x => x.ThuTu);
            return query.ToList();
        }
        public class ThanhVien
        {
            public string Name { get; set; }
            public string SanPham { get; set; }
            public int ThuTu { get; set; }
            public Double Tong { get; set; }
        }
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