using Entity;
using Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace VS.ECommerce_MVC
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int count = 0;
            List<EHoaHongThanhVien> dt = SHoaHongThanhVien.Name_Text("select * from HoaHongThanhVien");
            foreach (var item in dt)
            {
                //if (item.NgayTao.Day <= 7)
                //{
                //    count = 1;
                //}
                //else if (item.NgayTao.Day > 7 && item.NgayTao.Day <= 14)
                //{
                //    count = 2;
                //}
                //else if (item.NgayTao.Day > 14 && item.NgayTao.Day <= 21)
                //{
                //    count = 3;
                //}
                //else if (item.NgayTao.Day > 21 && item.NgayTao.Day <= 28)
                //{
                //    count = 4;
                //}
                //else if (item.NgayTao.Day > 28)
                //{
                //    count = 5;
                //}
                SHoaHongThanhVien.Name_Text("update HoaHongThanhVien set Tuan=" + SetTuanTrongThang(item.NgayTao) + "  where ID=" + item.ID.ToString() + "");
                //SHoaHongThanhVien.Name_Text("update HoaHongThanhVien set Tuan=" + GetWeekOrderInYear(item.NgayTao.ToString()) + "  where ID=" + item.ID.ToString() + "");
            }

            //SELECT
            //Tuan,
            //SUM(CONVERT(float,SoCoin)) AS TongTien
            //FROM HoaHongThanhVien
            //WHERE year(NgayTao)=2020 and   month(NgayTao)=11
            //GROUP BY Tuan

            //var d = new DateTime(2020, 12, 31);
            //CultureInfo cul = CultureInfo.CurrentCulture;
            //var firstDayWeek = cul.Calendar.GetWeekOfYear(
            //    d,
            //    CalendarWeekRule.FirstDay,
            //    DayOfWeek.Monday);
            //int weekNum = cul.Calendar.GetWeekOfYear(
            //    d,
            //    CalendarWeekRule.FirstDay,
            //    DayOfWeek.Monday);
            //int year = weekNum == 52 && d.Month == 1 ? d.Year - 1 : d.Year;
            //Response.Write("Year: {" + year + "} Week: {" + weekNum + "}");
        }

        public static int SetTuanTrongThang(DateTime NgayTao)
        {
            //Sét 5 tuần trong 1 tháng
            //VD: SetTuanTrongThang("2020-11-30"); = tuần 5
            int Tuan = 0;
            if (NgayTao.Day <= 7)
            {
                Tuan = 1;
            }
            else if (NgayTao.Day > 7 && NgayTao.Day <= 14)
            {
                Tuan = 2;
            }
            else if (NgayTao.Day > 14 && NgayTao.Day <= 21)
            {
                Tuan = 3;
            }
            else if (NgayTao.Day > 21 && NgayTao.Day <= 28)
            {
                Tuan = 4;
            }
            else if (NgayTao.Day > 28)
            {
                Tuan = 5;
            }
            return Tuan;
        }
        public static int SetTuan(string time)
        {
            //Sét 52 tuần trong 1 năm
            //VD: SetTuan("2020-11-30");
            CultureInfo myCI = CultureInfo.CurrentCulture;
            System.Globalization.Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
            return myCal.GetWeekOfYear(Convert.ToDateTime(time), myCWR, myFirstDOW);
        }

    }
}