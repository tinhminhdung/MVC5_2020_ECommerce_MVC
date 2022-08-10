using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.ECommerce_MVC.cms.Admin.MMember
{
    public partial class DiagramTree : System.Web.UI.UserControl
    {
        private Random rnd = new Random();
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        string IDThanhVien = "";
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
            if (Request["IDThanhVien"] != null && !Request["IDThanhVien"].Equals(""))
            {
                IDThanhVien = Request["IDThanhVien"];

                List<Entity.Member> table = SMember.Name_Text("select * from Members where  ID=" + (IDThanhVien.Trim().ToLower()) + " ");
                if (table.Count > 0)
                {
                    List<Entity.Member> iitem = SMember.ThanhVien_PHANTRANG1(IDThanhVien, "1");
                    if (iitem.Count() > 0)
                    {
                        ltshow.Text = ShowDanhsachthanhvien(IDThanhVien.ToString());
                        ltname.Text = table[0].HoVaTen + " - " + table[0].DienThoai;
                        ltTongsobanghi.Text = iitem.Count().ToString();
                        lttongtvht.Text = Commond.SumThanhVienCapDuoi(IDThanhVien.ToString());
                    }
                }
            }
        }

        protected string ShowDanhsachthanhvien(string MembersID)
        {
            string str = "";
            List<Entity.Member> dt = SMember.Name_Text("SELECT * FROM [Members] where   ID='" + MembersID + "'   order by ID asc");
            if (dt.Count > 0)
            {
                foreach (Entity.Member item in dt)
                {
                    str += "<li><code><a target=\"_blank\" href=\"admin.aspx?u=Thanhvien&IDThanhVien=" + item.ID.ToString() + "\">" + item.HoVaTen.ToString() + " -  " + item.DienThoai.ToString() + "-" + Commond.TrangThaiThamGiaCoDongMTRe(item.TrangThaiThamGiaCoDong.ToString()) + "Vị trí thứ :(" + (item.ViTriF1.ToString()) + ")</a></code>" + SupN3(item.ID.ToString()) + "</li>";
                }
            }
            return str.ToString();
        }
        protected string SupN3(string id)
        {
            string str = "";
            List<Entity.Member> dt = SMember.Name_Text("SELECT * FROM [Members]  where  GioiThieu=" + id + "   order by ID asc");
            if (dt.Count > 0)
            {
                str += "<ul>";
                foreach (Entity.Member item in dt)
                {
                    str += "<li><code><a class=\"active\" target=\"_blank\"  href=\"admin.aspx?u=Thanhvien&IDThanhVien=" + item.ID.ToString() + "\">" + item.HoVaTen.ToString() + " -  " + item.DienThoai.ToString() + "-" + Commond.TrangThaiThamGiaCoDongMTRe(item.TrangThaiThamGiaCoDong.ToString()) + "Vị trí thứ :(" + (item.ViTriF1.ToString()) + ")</a></code>" + SupN3(item.ID.ToString()) + "</li>";
                }
                str += "</ul>";
            }
            return str.ToString();
        }
       
        //protected void lnkxuatExel_Click(object sender, EventArgs e)
        //{
        //    string Namefile = "DanhSach_ThanhVien_DiagramTree_" + DateTime.Now.ToString();
        //    Response.Clear();
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Response.AddHeader("content-disposition", "attachment;filename=" + Namefile + ".xls");
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // "application/ms-excel";
        //    Response.ContentEncoding = System.Text.Encoding.UTF8;
        //    Response.Charset = "utf-8";
        //    Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        //    Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        //    sb.Append("<table border='1' bgcolor='#ffffff' bordercolor='#dedede' cellspacing='0' cellpadding='0' style='font-size:12px; font-family:Arial; background:white;'>");
        //    sb.Append("<tr>");
        //    sb.Append("  <th style=\"width:50px; vertical-align:middle; height: 22px;\">");
        //    sb.Append("    <b>STT</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:120px; vertical-align:middle;\">");
        //    sb.Append("    <b>Họ và tên</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
        //    sb.Append("    <b>Người giới thiệu</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:120px; vertical-align:middle;\">");
        //    sb.Append("    <b>Địện thoại</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:200px; vertical-align:middle;\">");
        //    sb.Append("    <b>Email</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:320px; vertical-align:middle;\">");
        //    sb.Append("    <b>Địa chỉ</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:120px; vertical-align:middle;\">");
        //    sb.Append("    <b>Ngày tạo</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:120px; vertical-align:middle;\">");
        //    sb.Append("    <b>Ngày Tham gia HĐ</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:120px; vertical-align:middle;\">");
        //    sb.Append("    <b>Số hợp đồng</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:120px; vertical-align:middle;\">");
        //    sb.Append("    <b>Mức hợp tác</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:120px; vertical-align:middle;\">");
        //    sb.Append("    <b>Tổng F1</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:120px; vertical-align:middle;\">");
        //    sb.Append("    <b>Tổng Thành viên</b>");
        //    sb.Append("  </th>");

        //    sb.Append("  <th style=\"width:120px; vertical-align:middle;\">");
        //    sb.Append("    <b>Lãi suất</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:120px; vertical-align:middle;\">");
        //    sb.Append("    <b>Kỳ hạn</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:180px; vertical-align:middle;\">");
        //    sb.Append("    <b>Hình thức hợp tác</b>");
        //    sb.Append("  </th>");

        //    sb.Append("  <th style=\"width:120px; vertical-align:middle;\">");
        //    sb.Append("    <b>Ngân hàng</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:120px; vertical-align:middle;\">");
        //    sb.Append("    <b>Số tài khoản</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:180px; vertical-align:middle;\">");
        //    sb.Append("    <b>Chi nhánh</b>");
        //    sb.Append("  </th>");

        //    sb.Append("  <th style=\"width:180px; vertical-align:middle;\">");
        //    sb.Append("    <b>Cấp bậc đăng ký</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:180px; vertical-align:middle;\">");
        //    sb.Append("    <b>Nội dung</b>");
        //    sb.Append("  </th>");
        //    sb.Append("  <th style=\"width:180px; vertical-align:middle;\">");
        //    sb.Append("    <b>MTREE</b>");
        //    sb.Append("  </th>");
  
        //    sb.Append("</tr>");

        //    List<Entity.Member> dt = SMember.Name_Text("SELECT * FROM [dbo].[Members] WHERE(([MTree] LIKE N'%|" + IDThanhVien + "|%')) and ID!=" + IDThanhVien + "  and TrangThai=1");
        //    if (dt.Count() > 0)
        //    {
        //        int i = 1;
        //        foreach (var item in dt)
        //        {
        //            sb.Append("<tr>");
        //            sb.Append("    <td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
        //            sb.Append("    <td style=\"vertical-align:middle;text-align: left;\">" + item.HoVaTen + " ( " + Commond.CacBacEXEL(item.CapBacTuChon.ToString()) + " ) </td>");
        //            sb.Append("    <td style=\"vertical-align:middle;text-align: left;\">" + Commond.EXELShowThanhVien(item.GioiThieu.ToString()) + "</td>");
        //            sb.Append("    <td style=\"vertical-align:middle;text-align: left;\">" + item.DienThoai + "</td>");
        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + item.Email + "</td>");
        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + item.DiaChi + " - " + Commond.ShowTinhThanh(item.TinhThanh.ToString()) + "</td>");
        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + item.NgayTao + "</td>");
        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + item.NgayThamGia + "</td>");
        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + item.SoHopDong + "</td>");
        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + AllQuery.MorePro.FormatMoney_NO(item.SoTienHopDong) + "</td>");
        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + Commond.SumThanhVienCapDuoiF1(item.ID.ToString()) + "</td>");
        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + Commond.SumThanhVienCapDuoi(item.ID.ToString()) + "</td>");

        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + item.ddlLaiSuat + " % </td>");
        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + item.ddlKyHan + " tháng</td>");
        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + Commond.HinhThucHopTac(item.HinhThucHopTac.ToString()) + "</td>");

        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + (item.NganHang) + "</td>");
        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + (item.SoTaiKHoan) + "</td>");
        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + (item.ChiNhanh) + "</td>");
        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + Commond.CacBacEXEL(item.CapBacTuChon.ToString()) + "</td>");
        //            sb.Append("    <td style=\"vertical-align:middle; text-align: left;\">" + (item.GhiChu) + "</td>");
        //            sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + item.MTree.ToString() + "</td>");
        //            sb.Append("</tr>");
        //        }
        //    }

        //    sb.Append("</table>");
        //    Response.Write(sb.ToString());
        //    Response.Flush();
        //    Response.End();
        //}

    }
}