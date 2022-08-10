using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;
using Entity;
using System.Text;

namespace VS.ECommerce_MVC.cms.Admin.MMember
{
    public partial class AllThanhVienCapDuoi : System.Web.UI.UserControl
    {
        private string status = "-1";
        private string TV = "0";

        // public int IDThanhVien = 0;
        DatalinqDataContext db = new DatalinqDataContext();
        private string lang = Captionlanguage.Language;
        DateTime fDate, tDate;

        public int i = 1;
        string nav = "";
        string Loc = "";
        string ID = "0";
        public string IDCD = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["lang"] != null)
            {
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["lang"] = this.lang;
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }
            if (Request["st"] != null && !Request["st"].Equals(""))
            {
                status = Request["st"];
            }
            if (Request["st"] != null && !Request["st"].Equals(""))
            {
                ddlstatus.SelectedValue = Request["st"];
            }
            if (Request["ds"] != null && !Request["ds"].Equals(""))
            {
                ddlordertype.SelectedValue = Request["ds"];
            }
            if (Request["kw"] != null && !Request["kw"].Equals(""))
            {
                txtkeyword.Text = Request["kw"];
            }
            if (Request["Tu"] != null && !Request["Tu"].Equals(""))
            {
                txtNgayThangNam.Text = Request["Tu"];
            }
            if (Request["Den"] != null && !Request["Den"].Equals(""))
            {
                txtDenNgayThangNam.Text = Request["Den"];
            }
            if (Request["TV"] != null && !Request["TV"].Equals(""))
            {
                TV = Request["TV"];
                IDCD = TV;
            }
            this.ddlstatus.Items.Add(new ListItem("Tất cả các mục", ""));
            this.ddlstatus.Items.Add(new ListItem("Kích hoạt", "1"));
            this.ddlstatus.Items.Add(new ListItem("Chưa kích hoạt", "0"));
            WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlstatus, this.status);
            if (Request["ID"] != null && !Request["ID"].Equals(""))
            {
                ID = Request["ID"];
                Loc += "&ID=" + ID;
            }
            if (IDCD != "0")
            {
                ltSum.Text = Commond.SumThanhVienCapDuoi(IDCD.ToString());
            }
            if (!base.IsPostBack)
            {
                List<Entity.Member> dt = SMember.Name_Text("SELECT * FROM Members  WHERE ID = " + ID + " ");
                if (dt.Count > 0)
                {
                    string Mtr = "|" + dt[0].MTRee.ToString();
                    ltphanmuc.Text = LoadNav(int.Parse(ID.ToString()));
                    LoadItems();
                }
                this.LoadItems();
            }
        }
        protected void btndisplay_Click(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }
        protected void lnksearch_Click(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }
        protected void Delete_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn c\x00f3 muốn x\x00f3a th\x00e0nh vi\x00ean n\x00e0y?')";
        }
        protected bool EnableLock(string status)
        {
            return status.Equals("1");
        }
        protected bool EnablecUnLock(string status)
        {
            return status.Equals("1");
        }
        protected bool EnablecLock(string status)
        {
            return status.Equals("2");
        }
        protected bool EnableUnLock(string status)
        {
            return status.Equals("0");
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }

        public void LoadItems()
        {

            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse("10");
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.Member> iitem = SMember.ThanhVien_PHANTRANG1(ID, "1");
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();
            }
            List<Entity.Member> dt = SMember.ThanhVien_PHANTRANG2(ID, "1", (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
            if (Tongsobanghi % Tongsotrang > 0)
            {
                Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
            }
            else
            {
                Tongsobanghi = Tongsobanghi / Tongsotrang;
            }
            ltpage.Text = Commond.PhantrangAdmin("/admin.aspx?u=CapDuoi&ID=" + ID + "&TV=" + IDCD + "", Tongsobanghi, pages);
        }
        private string LoadNav(int ID)
        {
            try
            {
                var item = db.Members.FirstOrDefault(s => s.ID == ID);
                if (item != null)
                {
                    nav = "<span> >> <a href=\"/admin.aspx?u=CapDuoi&ID=" + item.ID + "&TV=" + IDCD + "\">" + item.HoVaTen + "</a>" + nav;
                    if (item.GioiThieu >= int.Parse(IDCD.ToString()))
                    {
                        LoadNav(Convert.ToInt32(item.GioiThieu));
                    }
                }
            }
            catch (Exception)
            { }
            return nav;
        }

        protected void Lock_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn c\x00f3 muốn kh\x00f3a t\x00e0i khoản n\x00e0y?')";
        }
        protected string Status(string status)
        {
            if (status.Equals("1"))
            {
                return "<span style=\"color: #fff; margin-left: 10px; font-weight: bold; background: #008100; border: 14px; padding: 3px; border-radius: 3px;\">Đã duyệt</span>";
            }
            return "<span style=\"color: #fff; margin-left: 10px; font-weight: bold; background: #ed1c24; border: 14px; padding: 3px; border-radius: 3px;\">Chưa duyệt</span>";
        }
        protected void ddlordertype_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }
        protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }
        protected string Enablechon(string chon)
        {
            if (chon.Equals("1"))
            {
                return "Thành viên";
            }
            return "Nội bộ";
        }
        private void UpdateStatus(string un, string status)
        {
            SMember.UPDATE_STATUS(un, status);
        }
        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }
        protected void LoadRequest()
        {
            Response.Redirect("admin.aspx?u=CapDuoi&st=" + ddlstatus.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "&ds=" + ddlordertype.SelectedValue + "&kw=" + txtkeyword.Text + "");
        }
        protected void btxoa_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)Repeater1.Items[i].FindControl("chkid");
                    HiddenField id = (HiddenField)Repeater1.Items[i].FindControl("hiID");
                    if (chk.Checked)
                    {
                        SMember.DELETE(id.Value);
                    }
                }
                LoadItems();
            }
            catch (Exception)
            {

            }
        }

        protected void txtNgayThangNam_TextChanged(object sender, EventArgs e)
        {
            LoadItems();
        }

        protected void txtDenNgayThangNam_TextChanged(object sender, EventArgs e)
        {
            LoadItems();
        }

        protected void ddlcapbac_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
        }

        //protected void lnkxuatExel_Click(object sender, EventArgs e)
        //{
        //    string Namefile = "DanhSach_ThanhVien_CapDuoi_" + DateTime.Now.ToString();
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
        //    sb.Append("    <b>MTRee</b>");
        //    sb.Append("  </th>");
        //    sb.Append("</tr>");


        //    List<Entity.Member> dt = SMember.Name_Text("SELECT * FROM [dbo].[Members] WHERE(([MTree] LIKE N'%|" + IDCD + "|%')) and ID!=" + IDCD + "  and TrangThai=1");
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