using FlexCel.XlsAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.ECommerce_MVC.Views.PhatTrienDangKy
{
    public partial class ImportExel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

      //  protected void Import_Click(object sender, EventArgs e)
        //{
        //    string Emails = "";
        //    string vuserpwd = "";
        //    string vfname = "";
        //    string NgaySinh = "";
        //    string DiaChi = "";
        //    string vphone = "";
        //    string CMTND = "";
        //    string GioiTinh = "";
        //    string NamTotNghiep = "";
        //    string HoKhauThuongChu = "";


        //    string DanToc = "";
        //    string KhoaHoc = "";
        //    string NgayCapChungMinh = "";
        //    string NoiCapChungMinh = "";
        //    string TenVanBangTuyenSinhDauVao = "";
        //    string SoVanBang = "";
        //    string NgayCapVanBang = "";


        //    string NoiCapVanBang = "";
        //    string TrinhDoNgoaiNgu = "";
        //    string TenNgoaiNgu = "";
        //    string LevelNgoaiNgu = "";
        //    string KhenThuong = "";
        //    string SoVoiKhungSauBac = "";

        //    if (ddlcategories.SelectedValue == "-1")
        //    {
        //        ltmsgs.Text = "Vui lòng chọn ngành học trước khi Import Exel !"; return;
        //    }
        //    int dem = 0;
        //    int demtbEM = 0;
        //    int TongThem = 0;
        //    if (Request.Files.Count > 0)
        //    {
        //        for (int i = 0; i < Request.Files.Count; i++)
        //        {
        //            HttpPostedFile file = Request.Files[i];
        //            if (file.FileName != "")
        //            {
        //                if (file.FileName.Split('.')[1] == "xls" || file.FileName.Split('.')[1] == "xlsx")
        //                {
        //                    file.SaveAs(Server.MapPath("uploads/excel/") + file.FileName);
        //                    XlsFile xls = new XlsFile(Server.MapPath("uploads/excel/") + file.FileName);
        //                    if (xls.GetSheetName(1) == "Sheet1")
        //                    {
        //                        xls.ActiveSheetByName = "Sheet1";
        //                        string regex = "[A-G]";
        //                        for (int row = 2; row <= xls.RowCount; row++)
        //                        {
        //                            //   là 5 nhé//  for (int colIndex = 1; colIndex <= 5; colIndex++)
        //                            #region for colIndex
        //                            //Số colIndex <= 10 này là số trên exel tổng có 10 cột như file exel mẫu
        //                            for (int colIndex = 1; colIndex <= 24; colIndex++)
        //                            {
        //                                object cell = xls.GetCellValue(row, colIndex);
        //                                switch (colIndex)
        //                                {
        //                                    case 1:
        //                                        if (cell != null)
        //                                            vfname = cell.ToString();
        //                                        break;
        //                                    case 2:
        //                                        if (cell != null)
        //                                            Emails = cell.ToString();
        //                                        break;
        //                                    case 3:
        //                                        if (cell != null)
        //                                            vphone = cell.ToString();
        //                                        break;
        //                                    case 4:
        //                                        if (cell != null)
        //                                            vuserpwd = cell.ToString();
        //                                        break;
        //                                    case 5:
        //                                        if (cell != null)
        //                                            DiaChi = cell.ToString();
        //                                        break;
        //                                    case 6:
        //                                        if (cell != null)
        //                                            NgaySinh = cell.ToString();
        //                                        break;
        //                                    case 7:
        //                                        if (cell != null)
        //                                            GioiTinh = cell.ToString();
        //                                        break;
        //                                    case 8:
        //                                        if (cell != null)
        //                                            DanToc = cell.ToString();
        //                                        break;
        //                                    case 9:
        //                                        if (cell != null)
        //                                            HoKhauThuongChu = cell.ToString();
        //                                        break;
        //                                    case 10:
        //                                        if (cell != null)
        //                                            NamTotNghiep = cell.ToString();
        //                                        break;
        //                                    case 11:
        //                                        if (cell != null)
        //                                            KhoaHoc = cell.ToString();
        //                                        break;

        //                                    case 12:
        //                                        if (cell != null)
        //                                            KhoaHoc = cell.ToString();
        //                                        break;

        //                                    case 13:
        //                                        if (cell != null)
        //                                            CMTND = cell.ToString();
        //                                        break;
        //                                    case 14:
        //                                        if (cell != null)
        //                                            NgayCapChungMinh = cell.ToString();
        //                                        break;
        //                                    case 15:
        //                                        if (cell != null)
        //                                            NoiCapChungMinh = cell.ToString();
        //                                        break;
        //                                    case 16:
        //                                        if (cell != null)
        //                                            TenVanBangTuyenSinhDauVao = cell.ToString();
        //                                        break;
        //                                    case 17:
        //                                        if (cell != null)
        //                                            SoVanBang = cell.ToString();
        //                                        break;
        //                                    case 18:
        //                                        if (cell != null)
        //                                            NgayCapVanBang = cell.ToString();
        //                                        break;
        //                                    case 19:
        //                                        if (cell != null)
        //                                            NoiCapVanBang = cell.ToString();
        //                                        break;
        //                                    case 20:
        //                                        if (cell != null)
        //                                            TrinhDoNgoaiNgu = cell.ToString();
        //                                        break;
        //                                    case 21:
        //                                        if (cell != null)
        //                                            TenNgoaiNgu = cell.ToString();
        //                                        break;
        //                                    case 22:
        //                                        if (cell != null)
        //                                            LevelNgoaiNgu = cell.ToString();
        //                                        break;
        //                                    case 23:
        //                                        if (cell != null)
        //                                            SoVoiKhungSauBac = cell.ToString();
        //                                        break;
        //                                    case 24:
        //                                        if (cell != null)
        //                                            KhenThuong = cell.ToString();
        //                                        break;
        //                                }
        //                            }
        //                            #endregion
        //                            #region MyRegion
        //                            {
        //                                List<Entity.users> dt11 = Susers.Name_Text("select * from users where vemail='" + Emails.ToString().Trim() + "' ");
        //                                if (dt11.Count > 0)
        //                                {
        //                                    demtbEM++;
        //                                }
        //                                List<Entity.users> dt2 = Susers.Name_Text("select * from users where vphone='" + vphone.ToString().Trim() + "' ");
        //                                if (dt2.Count > 0)
        //                                {
        //                                    dem++;
        //                                }

        //                                List<Entity.users> dt1 = Susers.Name_Text("select * from users where vemail='" + Emails.ToString().Trim() + "' ");
        //                                if (dt1.Count <= 0)
        //                                {
        //                                    List<Entity.users> dt = Susers.Name_Text("select * from users where vphone='" + vphone.ToString().Trim() + "' ");
        //                                    if (dt.Count <= 0)
        //                                    {
        //                                        if (Emails != "" && vuserpwd != "" && vfname != "" && vphone != "")
        //                                        {
        //                                            TongThem++;
        //                                            string validatekey = DateTime.Now.Ticks.ToString();
        //                                            user obj = new user();
        //                                            obj.IDNhomCapDoDaiLy = 0;
        //                                            obj.vuserun = Emails;// email_register.Trim().ToLower();
        //                                            obj.vuserpwd = vuserpwd;
        //                                            obj.vfname = vfname;
        //                                            obj.vlname = vfname;
        //                                            obj.igender = 0;
        //                                            obj.dbirthday = NgaySinh;
        //                                            obj.vidcard = "0";
        //                                            obj.vaddress = DiaChi;
        //                                            obj.vphone = vphone;
        //                                            obj.vemail = Emails;// email_register.Trim().ToLower();
        //                                            obj.iregionid = int.Parse("0");
        //                                            obj.Kieuavata = 0;
        //                                            obj.vavatar = "";
        //                                            obj.vavatartitle = "";
        //                                            obj.dcreatedate = DateTime.Now;
        //                                            obj.dlastvisited = DateTime.Now;
        //                                            obj.vvalidatekey = validatekey;
        //                                            obj.istatus = 0;
        //                                            obj.IDThanhPho = 0;
        //                                            obj.IDQuanHuyen = 0;
        //                                            obj.lang = "VIE";
        //                                            obj.Type = "Học viên";
        //                                            obj.LevelAff = 0;
        //                                            obj.TongThuNhap = "0";
        //                                            obj.TongKhachHang = 0;
        //                                            obj.CouPon = "";
        //                                            obj.PhamTramGiamGia = "";
        //                                            obj.TenNganHang = "";
        //                                            obj.SoTaiKhoan = "";
        //                                            obj.ChiNhanh = "";
        //                                            obj.ActiveAff = 0;
        //                                            obj.TongSoTienMuaHangCuaDaiLy = "0";
        //                                            obj.NguoiGioiThieuLamDaiLy = "";
        //                                            obj.ActiveDaLy = 0;
        //                                            obj.TongTienThanhVienDaMuaKhoaHoc = "0";
        //                                            obj.ThanhVienMuaTheTaiLanHai = 0;

        //                                            obj.DanToc = DanToc;
        //                                            obj.KhoaHoc = KhoaHoc;
        //                                            obj.CMTND = CMTND;
        //                                            obj.NgayCapChungMinh = NgayCapChungMinh;
        //                                            obj.NoiCapChungMinh = NoiCapChungMinh;
        //                                            obj.Khac = "";
        //                                            obj.TenVanBangTuyenSinhDauVao = TenVanBangTuyenSinhDauVao;
        //                                            obj.SoVanBang = SoVanBang;
        //                                            obj.NgayCapVanBang = NgayCapVanBang;
        //                                            obj.NoiCapVanBang = NoiCapVanBang;
        //                                            obj.TrinhDoNgoaiNgu = TrinhDoNgoaiNgu;
        //                                            obj.TenNgoaiNgu = TenNgoaiNgu;
        //                                            obj.LevelNgoaiNgu = LevelNgoaiNgu;
        //                                            obj.SoVoiKhungSauBac = SoVoiKhungSauBac;
        //                                            obj.KhenThuong = KhenThuong;
        //                                            obj.NganhTheoHocIDNhomNganh = int.Parse(ddlcategories.SelectedValue);
        //                                            obj.GioiTinh = GioiTinh;
        //                                            obj.NamTotNghiep = NamTotNghiep;
        //                                            obj.HoKhauThuongChu = HoKhauThuongChu;
        //                                            obj.LopHoc = 0;
        //                                            db.users.InsertOnSubmit(obj);
        //                                            db.SubmitChanges();

        //                                            user tbn = db.users.Where(s => s.lang == lang).OrderByDescending(s => s.iuser_id).FirstOrDefault();
        //                                            string proid = tbn.iuser_id.ToString();
        //                                            Commond.LichSu_ThaoTac_HeThong("1", MoreAll.MoreAll.GetCookies("AdminID").ToString(), "Thêm học viên qua Exel", "1", "2", proid);
                                               
                                                
        //                                        }
        //                                    }
        //                                }



        //                                Emails = "";
        //                                vuserpwd = "";
        //                                vfname = "";
        //                                NgaySinh = "";
        //                                DiaChi = "";
        //                                vphone = "";
        //                                CMTND = "";
        //                                GioiTinh = "";
        //                                NamTotNghiep = "";
        //                                HoKhauThuongChu = "";
        //                                DanToc = "";
        //                                KhoaHoc = "";
        //                                NgayCapChungMinh = "";
        //                                NoiCapChungMinh = "";
        //                                TenVanBangTuyenSinhDauVao = "";
        //                                SoVanBang = "";
        //                                NgayCapVanBang = "";
        //                                NoiCapVanBang = "";
        //                                TrinhDoNgoaiNgu = "";
        //                                TenNgoaiNgu = "";
        //                                LevelNgoaiNgu = "";
        //                                KhenThuong = "";
        //                                SoVoiKhungSauBac = "";

        //                            }
        //                            #endregion
        //                        }
        //                        string chuoi2 = "";
        //                        string chuoi3 = "";
        //                        string chuoi1 = "";
        //                        if (dem > 0)
        //                        {
        //                            chuoi1 += " (" + dem + " Số điện thoại bị trùng)";
        //                        }
        //                        if (demtbEM > 0)
        //                        {
        //                            chuoi1 += " (" + demtbEM + " Email điện thoại bị trùng)";
        //                        }
        //                        if (TongThem > 0)
        //                        {
        //                            chuoi2 += " (Đã thêm" + TongThem + " Học viên)";
        //                            chuoi2 += chuoi1;
        //                        }
        //                        else
        //                        {
        //                            chuoi3 = "Không có học viên nào được thêm.";
        //                            chuoi3 += chuoi1;
        //                        }
        //                        string chuoi = chuoi3 + chuoi2;

        //                        Response.Write("<script type=\"text/javascript\">alert('" + chuoi + "');window.location.href='/admin.aspx?u=Thanhvien'; </script>");

        //                    }
        //                    else
        //                        ltmsgs.Text = "File excel không định dạng đúng mẫu template!";
        //                }
        //                else
        //                    ltmsgs.Text = "File upload không đúng định dạng!";
        //            }
        //            else
        //                ltmsgs.Text = "Không có file upload!'";
        //        }
        //    }

       // }
    }
}