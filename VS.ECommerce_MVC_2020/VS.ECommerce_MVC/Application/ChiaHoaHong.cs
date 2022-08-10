using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VS.ECommerce_MVC;

public class ChiaHoaHong
{
    public static string HoaHongF1_F6Tang_Combo_TaiTieuDung(string IDThanhVien, string IDDonHang, string TienDauVaos, string SoLuongs)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        Double TongTienDaChia = 0;
        double TienDauVao = Convert.ToDouble(TienDauVaos);
        double TaiTieuDung = Convert.ToDouble(Commond.Setting("TaiTieuDung"));

        List<Entity.Member> F1 = SMember.Name_Text("select * from Members where ID=" + IDThanhVien.ToString() + "");
        if (F1.Count > 0)
        {
            #region HH đại lý 1,2 triệu, 2.1 triệu, 3.150 triệu_ đối với thành viên hưởng trực tếp ko cần mua hàng vẫn dc hh
            if (F1[0].GioiThieu.ToString() != "0")// && F1[0].TrangThaiMuaHang.ToString() == "1"
            {
                if (TaiTieuDung != 0)
                {
                    double PTHH = ((TaiTieuDung * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                    ThemHoaHong("TrucTiep", "Trực tiếp tiêu dùng", TienDauVao.ToString(), IDThanhVien.ToString(), F1[0].GioiThieu.ToString(), PTHH.ToString(), TaiTieuDung.ToString(), IDDonHang.ToString());
                }
            }
            #endregion

            #region HHoa Hồng 6 tầng và cấp bậc
            if (F1[0].GioiThieu.ToString() != "0")
            {
                // Tầng 1: 600 K
                List<Entity.Member> TF1 = SMember.Name_Text("select * from Members where ID=" + F1[0].GioiThieu.ToString() + "");
                if (TF1.Count > 0)// Tầng này chính là đại lý 1,2 triệu, 2.1 triệu, 3.150 triệu đã cho ở trên rồi
                {
                    if (TF1[0].GioiThieu.ToString() != "0")
                    {
                        #region Tang2
                        List<Entity.Member> TF2 = SMember.Name_Text("select * from Members where ID=" + TF1[0].GioiThieu.ToString() + "");
                        if (TF2.Count > 0)
                        {
                            if (TF2[0].TrangThaiMuaHang.ToString() == "1")
                            {
                                double Tang2 = Convert.ToDouble(Commond.Setting("TaiTieuDung"));
                                if (Tang2 != 0)
                                {
                                    double PTHH = ((Tang2 * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                    ThemHoaHong("ChietKhau", "Chiết khấu đại lý cấp 2", TienDauVao.ToString(), IDThanhVien.ToString(), TF2[0].ID.ToString(), PTHH.ToString(), Tang2.ToString(), IDDonHang.ToString());
                                }
                            }
                            if (TF2[0].GioiThieu.ToString() != "0")
                            {
                                #region TF3
                                List<Entity.Member> TF3 = SMember.Name_Text("select * from Members where ID=" + TF2[0].GioiThieu.ToString() + "");
                                if (TF3.Count > 0)
                                {

                                    if (TF3[0].TrangThaiMuaHang.ToString() == "1")
                                    {
                                        double Tang3 = Convert.ToDouble(Commond.Setting("TaiTieuDung"));
                                        if (Tang3 != 0)
                                        {
                                            double PTHH = ((Tang3 * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                            ThemHoaHong("ChietKhau", "Chiết khấu đại lý cấp 3", TienDauVao.ToString(), IDThanhVien.ToString(), TF3[0].ID.ToString(), PTHH.ToString(), Tang3.ToString(), IDDonHang.ToString());
                                        }
                                    }
                                    if (TF3[0].GioiThieu.ToString() != "0")
                                    {
                                        #region TF4
                                        List<Entity.Member> TF4 = SMember.Name_Text("select * from Members where ID=" + TF3[0].GioiThieu.ToString() + "");
                                        if (TF4.Count > 0)
                                        {

                                            if (TF4[0].TrangThaiMuaHang.ToString() == "1")
                                            {
                                                double Tang4 = Convert.ToDouble(Commond.Setting("TaiTieuDung"));
                                                if (Tang4 != 0)
                                                {
                                                    double PTHH = ((Tang4 * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                    ThemHoaHong("ChietKhau", "Chiết khấu đại lý cấp 4", TienDauVao.ToString(), IDThanhVien.ToString(), TF4[0].ID.ToString(), PTHH.ToString(), Tang4.ToString(), IDDonHang.ToString());
                                                }
                                            }
                                            if (TF4[0].GioiThieu.ToString() != "0")
                                            {
                                                #region TF5

                                                List<Entity.Member> TF5 = SMember.Name_Text("select * from Members where ID=" + TF4[0].GioiThieu.ToString() + "");
                                                if (TF5.Count > 0)
                                                {

                                                    if (TF5[0].TrangThaiMuaHang.ToString() == "1")
                                                    {
                                                        double Tang5 = Convert.ToDouble(Commond.Setting("TaiTieuDung"));
                                                        if (Tang5 != 0)
                                                        {
                                                            double PTHH = ((Tang5 * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                            ThemHoaHong("ChietKhau", "Chiết khấu đại lý cấp 5", TienDauVao.ToString(), IDThanhVien.ToString(), TF5[0].ID.ToString(), PTHH.ToString(), Tang5.ToString(), IDDonHang.ToString());
                                                        }
                                                    }
                                                    if (TF5[0].GioiThieu.ToString() != "0")
                                                    {
                                                        #region TF6
                                                        List<Entity.Member> TF6 = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                        if (TF6.Count > 0)
                                                        {
                                                            if (TF6[0].TrangThaiMuaHang.ToString() == "1")
                                                            {
                                                                double Tang6 = Convert.ToDouble(Commond.Setting("TaiTieuDung"));
                                                                if (Tang6 != 0)
                                                                {
                                                                    double PTHH = ((Tang6 * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                    ThemHoaHong("ChietKhau", "Chiết khấu đại lý cấp 6", TienDauVao.ToString(), IDThanhVien.ToString(), TF6[0].ID.ToString(), PTHH.ToString(), Tang6.ToString(), IDDonHang.ToString());
                                                                }
                                                            }

                                                            #region Từ tầng số 6 sẽ tìm các cấp bậc để cho hoa hồng
                                                            #region Trưởng nhóm kinh doanh
                                                            if (!Tim_CapBac(TF6[0].GioiThieu.ToString(), "2").Equals("0"))
                                                            {
                                                                List<Entity.Member> TNKD = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                                if (TNKD.Count > 0)
                                                                {
                                                                    if (TNKD[0].TrangThaiMuaHang.ToString() == "1")
                                                                    {
                                                                        double HHTNKD = Convert.ToDouble(Commond.Setting("HHTNKD"));
                                                                        if (HHTNKD != 0)
                                                                        {
                                                                            double PTHH = ((HHTNKD * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                            ThemHoaHong("CapBac", "Trưởng nhóm kinh doanh", TienDauVao.ToString(), IDThanhVien.ToString(), Tim_CapBac(TF6[0].GioiThieu.ToString(), "2"), PTHH.ToString(), HHTNKD.ToString(), IDDonHang.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            #region Trưởng phòng kinh doanh
                                                            if (!Tim_CapBac(TF6[0].GioiThieu.ToString(), "3").Equals("0"))
                                                            {
                                                                List<Entity.Member> TNKD = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                                if (TNKD.Count > 0)
                                                                {
                                                                    if (TNKD[0].TrangThaiMuaHang.ToString() == "1")
                                                                    {
                                                                        double HHTPKD = Convert.ToDouble(Commond.Setting("HHTPKD"));
                                                                        if (HHTPKD != 0)
                                                                        {
                                                                            double PTHH = ((HHTPKD * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                            ThemHoaHong("CapBac", "Trưởng phòng kinh doanh", TienDauVao.ToString(), IDThanhVien.ToString(), Tim_CapBac(TF6[0].GioiThieu.ToString(), "3"), PTHH.ToString(), HHTPKD.ToString(), IDDonHang.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            #region Giám đốc kinh doanh
                                                            if (!Tim_CapBac(TF6[0].GioiThieu.ToString(), "4").Equals("0"))
                                                            {
                                                                List<Entity.Member> TNKD = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                                if (TNKD.Count > 0)
                                                                {
                                                                    if (TNKD[0].TrangThaiMuaHang.ToString() == "1")
                                                                    {
                                                                        double HHGDKD = Convert.ToDouble(Commond.Setting("HHGDKD"));
                                                                        if (HHGDKD != 0)
                                                                        {
                                                                            double PTHH = ((HHGDKD * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                            ThemHoaHong("CapBac", "Giám đốc kinh doanh", TienDauVao.ToString(), IDThanhVien.ToString(), Tim_CapBac(TF6[0].GioiThieu.ToString(), "4"), PTHH.ToString(), HHGDKD.ToString(), IDDonHang.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            #region Giám đốc Vùng
                                                            if (!Tim_CapBac(TF6[0].GioiThieu.ToString(), "5").Equals("0"))
                                                            {
                                                                List<Entity.Member> TNKD = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                                if (TNKD.Count > 0)
                                                                {
                                                                    if (TNKD[0].TrangThaiMuaHang.ToString() == "1")
                                                                    {
                                                                        double HHGDVung = Convert.ToDouble(Commond.Setting("HHGDVung"));
                                                                        if (HHGDVung != 0)
                                                                        {
                                                                            double PTHH = ((HHGDVung * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                            ThemHoaHong("CapBac", "Giám đốc Vùng", TienDauVao.ToString(), IDThanhVien.ToString(), Tim_CapBac(TF6[0].GioiThieu.ToString(), "5"), PTHH.ToString(), HHGDVung.ToString(), IDDonHang.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            #region Giám đốc Mien
                                                            if (!Tim_CapBac(TF6[0].GioiThieu.ToString(), "6").Equals("0"))
                                                            {
                                                                List<Entity.Member> TNKD = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                                if (TNKD.Count > 0)
                                                                {
                                                                    if (TNKD[0].TrangThaiMuaHang.ToString() == "1")
                                                                    {
                                                                        double HHGDMien = Convert.ToDouble(Commond.Setting("HHGDMien"));
                                                                        if (HHGDMien != 0)
                                                                        {
                                                                            double PTHH = ((HHGDMien * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                            ThemHoaHong("CapBac", "Giám đốc Miền", TienDauVao.ToString(), IDThanhVien.ToString(), Tim_CapBac(TF6[0].GioiThieu.ToString(), "6"), PTHH.ToString(), HHGDMien.ToString(), IDDonHang.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            #endregion
                                                        }

                                                        #endregion
                                                    }
                                                }

                                                #endregion
                                            }
                                        }

                                        #endregion
                                    }
                                }

                                #endregion
                            }
                        }
                    }
                        #endregion
                }
            }
            #endregion

            #region Vi Loi Nhuan sau khi da chia HH
            try
            {
                Double TongTienHoaHongDaChiaCapBac = Convert.ToDouble(KiemTraTongTienHoaHongDaChiaCapBac(IDDonHang, IDThanhVien));
                Double TongCong = (TienDauVao - TongTienHoaHongDaChiaCapBac);
                LoiNhuanMuaBan abln = new LoiNhuanMuaBan();
                abln.IDThanhVienMua = int.Parse(IDThanhVien.ToString());
                abln.IDDonHang = IDDonHang.ToString();
                abln.MoTa = "";
                abln.NgayTao = DateTime.Now;
                abln.TongTien = TienDauVao.ToString();
                abln.TongTienCon = TongCong.ToString();
                abln.TongTienDaChia = TongTienHoaHongDaChiaCapBac.ToString();
                abln.MTreeIDThanhVienMua = Commond.ShowMTree(IDThanhVien.ToString());
                abln.NguoiDuyet = MoreAll.MoreAll.GetCookies("UName").ToString();
                db.LoiNhuanMuaBans.InsertOnSubmit(abln);
                db.SubmitChanges();
            }
            catch (Exception)
            { }
            #endregion
        }
        return "";
    }
    public static string HoaHongF1_F6Tang_Combo_CoDong(string IDThanhVien, string IDDonHang, string TienDauVaos, string SoLuongs)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        Double TongTienDaChia = 0;
        double TienDauVao = Convert.ToDouble(TienDauVaos);
        List<Entity.Member> F1 = SMember.Name_Text("select * from Members where ID=" + IDThanhVien.ToString() + "");
        if (F1.Count > 0)
        {
            #region
            if (F1[0].GioiThieu.ToString() != "0")// && F1[0].TrangThaiMuaHang.ToString() == "1"
            {
                double TrucTiepCoDong = 0;
                TrucTiepCoDong = Convert.ToDouble(Commond.Setting("TrucTiepCoDong"));
                if (TrucTiepCoDong != 0)
                {
                    //** KHi A là thanh viên bình thường giới thiệu ra B mà B tham gia gói cổ đông
                    //--> thì khi B và gói cổ đông thì chia cho A vẫn là dạng 70 và 30
                    //** Khi B là cổ đông giới thiệu ra C tham gia gói cổ đông 
                    //--> Khi C đi mua gói cổ đông thì B sẽ nhận về ví rút tiền là 100%  
                    //---> chú ý: các tầng khác vẫn chia hoa hồng 70 và 30 

                    List<Entity.Member> CoDong = SMember.Name_Text("select * from Members  where ID=" + F1[0].GioiThieu.ToString() + "");
                    if (CoDong.Count() > 0)
                    {
                        if (F1[0].TrangThaiThamGiaCoDong.ToString() == "1" && CoDong[0].TrangThaiThamGiaCoDong.ToString() == "1")
                        {
                            // Nhận đủ 100% về ví chính
                            double PTHH = ((TrucTiepCoDong * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                            ThemHoaHong_CoDong("TrucTiep", "Chiết khấu đại lý cổ đông", TienDauVao.ToString(), IDThanhVien.ToString(), F1[0].GioiThieu.ToString(), PTHH.ToString(), TrucTiepCoDong.ToString(), IDDonHang.ToString());
                        }
                        else
                        {
                            // nhận hoa hồng thành 2 ví
                            double PTHH = ((TrucTiepCoDong * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                            ThemHoaHong("TrucTiep", "Chiết khấu đại lý cổ đông", TienDauVao.ToString(), IDThanhVien.ToString(), F1[0].GioiThieu.ToString(), PTHH.ToString(), TrucTiepCoDong.ToString(), IDDonHang.ToString());
                        }
                    }
                }
            }
            #endregion

            #region HHoa Hồng 6 tầng và cấp bậc
            if (F1[0].GioiThieu.ToString() != "0")
            {
                // Tầng 1: 600 K
                List<Entity.Member> TF1 = SMember.Name_Text("select * from Members where ID=" + F1[0].GioiThieu.ToString() + "");
                if (TF1.Count > 0)// Tầng này chính là đại lý 1,2 triệu, 2.1 triệu, 3.150 triệu đã cho ở trên rồi
                {
                    #region Tang2
                    if (TF1[0].GioiThieu.ToString() != "0")
                    {
                        List<Entity.Member> TF2 = SMember.Name_Text("select * from Members where ID=" + TF1[0].GioiThieu.ToString() + "");
                        if (TF2.Count > 0)
                        {
                            if (TF2[0].TrangThaiMuaHang.ToString() == "1")
                            {
                                double Tang2 = Convert.ToDouble(Commond.Setting("ChietKhau2"));
                                if (Tang2 != 0)
                                {
                                    double PTHH = ((Tang2 * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                    ThemHoaHong("ChietKhau", "Chiết khấu đại lý cấp 2", TienDauVao.ToString(), IDThanhVien.ToString(), TF2[0].ID.ToString(), PTHH.ToString(), Tang2.ToString(), IDDonHang.ToString());
                                }
                            }
                            if (TF2[0].GioiThieu.ToString() != "0")
                            {
                                #region TF3
                                List<Entity.Member> TF3 = SMember.Name_Text("select * from Members where ID=" + TF2[0].GioiThieu.ToString() + "");
                                if (TF3.Count > 0)
                                {
                                    if (TF3[0].TrangThaiMuaHang.ToString() == "1")
                                    {
                                        double Tang3 = Convert.ToDouble(Commond.Setting("ChietKhau3"));
                                        if (Tang3 != 0)
                                        {
                                            double PTHH = ((Tang3 * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                            ThemHoaHong("ChietKhau", "Chiết khấu đại lý cấp 3", TienDauVao.ToString(), IDThanhVien.ToString(), TF3[0].ID.ToString(), PTHH.ToString(), Tang3.ToString(), IDDonHang.ToString());
                                        }
                                    }
                                    if (TF3[0].GioiThieu.ToString() != "0")
                                    {
                                        #region TF4
                                        List<Entity.Member> TF4 = SMember.Name_Text("select * from Members where ID=" + TF3[0].GioiThieu.ToString() + "");
                                        if (TF4.Count > 0)
                                        {
                                            if (TF4[0].TrangThaiMuaHang.ToString() == "1")
                                            {
                                                double Tang4 = Convert.ToDouble(Commond.Setting("ChietKhau4"));
                                                if (Tang4 != 0)
                                                {
                                                    double PTHH = ((Tang4 * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                    ThemHoaHong("ChietKhau", "Chiết khấu đại lý cấp 4", TienDauVao.ToString(), IDThanhVien.ToString(), TF4[0].ID.ToString(), PTHH.ToString(), Tang4.ToString(), IDDonHang.ToString());
                                                }
                                            }
                                            if (TF4[0].GioiThieu.ToString() != "0")
                                            {
                                                #region TF5
                                                List<Entity.Member> TF5 = SMember.Name_Text("select * from Members where ID=" + TF4[0].GioiThieu.ToString() + "");
                                                if (TF5.Count > 0)
                                                {

                                                    if (TF5[0].TrangThaiMuaHang.ToString() == "1")
                                                    {
                                                        double Tang5 = Convert.ToDouble(Commond.Setting("ChietKhau5"));
                                                        if (Tang5 != 0)
                                                        {
                                                            double PTHH = ((Tang5 * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                            ThemHoaHong("ChietKhau", "Chiết khấu đại lý cấp 5", TienDauVao.ToString(), IDThanhVien.ToString(), TF5[0].ID.ToString(), PTHH.ToString(), Tang5.ToString(), IDDonHang.ToString());
                                                        }
                                                    }
                                                    if (TF5[0].GioiThieu.ToString() != "0")
                                                    {
                                                        #region TF6
                                                        List<Entity.Member> TF6 = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                        if (TF6.Count > 0)
                                                        {
                                                            if (TF6[0].TrangThaiMuaHang.ToString() == "1")
                                                            {
                                                                double Tang6 = Convert.ToDouble(Commond.Setting("ChietKhau6"));
                                                                if (Tang6 != 0)
                                                                {
                                                                    double PTHH = ((Tang6 * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                    ThemHoaHong("ChietKhau", "Chiết khấu đại lý cấp 6", TienDauVao.ToString(), IDThanhVien.ToString(), TF6[0].ID.ToString(), PTHH.ToString(), Tang6.ToString(), IDDonHang.ToString());
                                                                }
                                                            }

                                                            #region Từ tầng số 6 sẽ tìm các cấp bậc để cho hoa hồng
                                                            #region Trưởng nhóm kinh doanh
                                                            if (!Tim_CapBac(TF6[0].GioiThieu.ToString(), "2").Equals("0"))
                                                            {
                                                                List<Entity.Member> TNKD = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                                if (TNKD.Count > 0)
                                                                {
                                                                    if (TNKD[0].TrangThaiMuaHang.ToString() == "1")
                                                                    {
                                                                        double HHTNKD = Convert.ToDouble(Commond.Setting("HHTNKD"));
                                                                        if (HHTNKD != 0)
                                                                        {
                                                                            double PTHH = ((HHTNKD * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                            ThemHoaHong("CapBac", "Trưởng nhóm kinh doanh", TienDauVao.ToString(), IDThanhVien.ToString(), Tim_CapBac(TF6[0].GioiThieu.ToString(), "2"), PTHH.ToString(), HHTNKD.ToString(), IDDonHang.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            #region Trưởng phòng kinh doanh
                                                            if (!Tim_CapBac(TF6[0].GioiThieu.ToString(), "3").Equals("0"))
                                                            {
                                                                List<Entity.Member> TNKD = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                                if (TNKD.Count > 0)
                                                                {
                                                                    if (TNKD[0].TrangThaiMuaHang.ToString() == "1")
                                                                    {
                                                                        double HHTPKD = Convert.ToDouble(Commond.Setting("HHTPKD"));
                                                                        if (HHTPKD != 0)
                                                                        {
                                                                            double PTHH = ((HHTPKD * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                            ThemHoaHong("CapBac", "Trưởng phòng kinh doanh", TienDauVao.ToString(), IDThanhVien.ToString(), Tim_CapBac(TF6[0].GioiThieu.ToString(), "3"), PTHH.ToString(), HHTPKD.ToString(), IDDonHang.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            #region Giám đốc kinh doanh
                                                            if (!Tim_CapBac(TF6[0].GioiThieu.ToString(), "4").Equals("0"))
                                                            {
                                                                List<Entity.Member> TNKD = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                                if (TNKD.Count > 0)
                                                                {
                                                                    if (TNKD[0].TrangThaiMuaHang.ToString() == "1")
                                                                    {
                                                                        double HHGDKD = Convert.ToDouble(Commond.Setting("HHGDKD"));
                                                                        if (HHGDKD != 0)
                                                                        {
                                                                            double PTHH = ((HHGDKD * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                            ThemHoaHong("CapBac", "Giám đốc kinh doanh", TienDauVao.ToString(), IDThanhVien.ToString(), Tim_CapBac(TF6[0].GioiThieu.ToString(), "4"), PTHH.ToString(), HHGDKD.ToString(), IDDonHang.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            #region Giám đốc Vùng
                                                            if (!Tim_CapBac(TF6[0].GioiThieu.ToString(), "5").Equals("0"))
                                                            {
                                                                List<Entity.Member> TNKD = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                                if (TNKD.Count > 0)
                                                                {
                                                                    if (TNKD[0].TrangThaiMuaHang.ToString() == "1")
                                                                    {
                                                                        double HHGDVung = Convert.ToDouble(Commond.Setting("HHGDVung"));
                                                                        if (HHGDVung != 0)
                                                                        {
                                                                            double PTHH = ((HHGDVung * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                            ThemHoaHong("CapBac", "Giám đốc Vùng", TienDauVao.ToString(), IDThanhVien.ToString(), Tim_CapBac(TF6[0].GioiThieu.ToString(), "5"), PTHH.ToString(), HHGDVung.ToString(), IDDonHang.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            #region Giám đốc Mien
                                                            if (!Tim_CapBac(TF6[0].GioiThieu.ToString(), "6").Equals("0"))
                                                            {
                                                                List<Entity.Member> TNKD = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                                if (TNKD.Count > 0)
                                                                {
                                                                    if (TNKD[0].TrangThaiMuaHang.ToString() == "1")
                                                                    {
                                                                        double HHGDMien = Convert.ToDouble(Commond.Setting("HHGDMien"));
                                                                        if (HHGDMien != 0)
                                                                        {
                                                                            double PTHH = ((HHGDMien * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                            ThemHoaHong("CapBac", "Giám đốc Miền", TienDauVao.ToString(), IDThanhVien.ToString(), Tim_CapBac(TF6[0].GioiThieu.ToString(), "6"), PTHH.ToString(), HHGDMien.ToString(), IDDonHang.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            #endregion
                                                        }

                                                        #endregion
                                                    }
                                                }

                                                #endregion
                                            }
                                        }

                                        #endregion
                                    }
                                }

                                #endregion
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region Vi Loi Nhuan sau khi da chia HH
            try
            {
                Double TongTienHoaHongDaChiaCapBac = Convert.ToDouble(KiemTraTongTienHoaHongDaChiaCapBac(IDDonHang, IDThanhVien));
                Double TongCong = (TienDauVao - TongTienHoaHongDaChiaCapBac);
                LoiNhuanMuaBan abln = new LoiNhuanMuaBan();
                abln.IDThanhVienMua = int.Parse(IDThanhVien.ToString());
                abln.IDDonHang = IDDonHang.ToString();
                abln.MoTa = "";
                abln.NgayTao = DateTime.Now;
                abln.TongTien = TienDauVao.ToString();
                abln.TongTienCon = TongCong.ToString();
                abln.TongTienDaChia = TongTienHoaHongDaChiaCapBac.ToString();
                abln.MTreeIDThanhVienMua = Commond.ShowMTree(IDThanhVien.ToString());
                abln.NguoiDuyet = MoreAll.MoreAll.GetCookies("UName").ToString();
                db.LoiNhuanMuaBans.InsertOnSubmit(abln);
                db.SubmitChanges();
            }
            catch (Exception)
            { }
            #endregion
        }
        return "";
    }
    public static string HoaHongF1_F6Tang_Combo_BinhThuong(string IDThanhVien, string IDDonHang, string TienDauVaos, string SoLuongs)
    {
        double SoLuong = Convert.ToDouble(SoLuongs);
        double SumSoLuong = 0;
        double SoTienHaiTram = Convert.ToDouble(Commond.Setting("SoTien200K"));
        double TypeSoLuong = 0;
        double SoLuongCu = Convert.ToDouble(Commond.Setting("SoLuong"));
        if (SoLuong >= SoLuongCu)
        {
            TypeSoLuong = 1;
            SumSoLuong = (SoLuong - SoLuongCu);
        }

        DatalinqDataContext db = new DatalinqDataContext();
        Double TongTienDaChia = 0;
        double TienDauVao = Convert.ToDouble(TienDauVaos);
        List<Entity.Member> F1 = SMember.Name_Text("select * from Members where ID=" + IDThanhVien.ToString() + "");
        if (F1.Count > 0)
        {
            #region HH đại lý 1,2 triệu, 2.1 triệu, 3.150 triệu_ đối với thành viên hưởng trực tếp ko cần mua hàng vẫn dc hh
            if (F1[0].GioiThieu.ToString() != "0")// && F1[0].TrangThaiMuaHang.ToString() == "1"
            {
                double HHDaiLy = 0;
                string ChuoiHH = "";
                if (F1[0].ViTriF1.ToString() == "1")// Kiểm tra ng mua hàng đang ở đại lý 1 hay 2 hay 3 thì cho A tương ứng số tiền theo cấu hình vị trí
                {
                    HHDaiLy = Convert.ToDouble(Commond.Setting("DaiLy1"));
                    ChuoiHH = "Chiết khấu đại lý thứ 1";
                }
                else if (F1[0].ViTriF1.ToString() == "2")// Kiểm tra ng mua hàng đang ở đại lý 1 hay 2 hay 3 thì cho A tương ứng số tiền theo cấu hình vị trí
                {
                    HHDaiLy = Convert.ToDouble(Commond.Setting("DaiLy2"));
                    ChuoiHH = "Chiết khấu đại lý thứ 2";
                }
                else if (F1[0].ViTriF1.ToString() == "3")// Kiểm tra ng mua hàng đang ở đại lý 1 hay 2 hay 3 thì cho A tương ứng số tiền theo cấu hình vị trí
                {
                    HHDaiLy = Convert.ToDouble(Commond.Setting("DaiLy3"));
                    ChuoiHH = "Chiết khấu đại lý thứ 3";
                }
                else if (F1[0].ViTriF1.ToString() == "4")// Kiểm tra ng mua hàng đang ở đại lý 1 hay 2 hay 3 thì cho A tương ứng số tiền theo cấu hình vị trí
                {
                    HHDaiLy = Convert.ToDouble(Commond.Setting("DaiLy4"));
                    ChuoiHH = "Chiết khấu đại lý thứ 4";
                }
                // double TienHoaHong = HHDaiLy;
                if (HHDaiLy != 0)
                {
                    #region Công thức tính 4 số lượng đầu vẫn chia hoa hồng 1.1, 2.1, 3.150 triệu , Nhưng số lượng thứ 5 thì chỉ chia thêm là 200 ngàn nữa thôi
                    double TSum = 0;
                    if (TypeSoLuong == 1)
                    {
                        // Số lượng là 4 thì chia số lượng 4 * số tiền cấu hình
                        // Số lượng Còn lại thì * số tiền cấu hình là 200
                        TSum += HHDaiLy * SoLuongCu;
                        TSum += SoTienHaiTram * SumSoLuong;
                    }
                    else
                    {
                        TSum = HHDaiLy;// Số lượng là 1 đến 3 thì chia bình thường 
                    }
                    double TienHoaHong = TSum;
                    #endregion
                    double PTHH = ((TSum * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                    ThemHoaHong("TrucTiep", ChuoiHH, TienDauVao.ToString(), IDThanhVien.ToString(), F1[0].GioiThieu.ToString(), PTHH.ToString(), TienHoaHong.ToString(), IDDonHang.ToString());
                }
            }
            #endregion

            #region HHoa Hồng 6 tầng và cấp bậc
            if (F1[0].GioiThieu.ToString() != "0")
            {
                // Tầng 1: 600 K
                List<Entity.Member> TF1 = SMember.Name_Text("select * from Members where ID=" + F1[0].GioiThieu.ToString() + "");
                if (TF1.Count > 0)// Tầng này chính là đại lý 1,2 triệu, 2.1 triệu, 3.150 triệu đã cho ở trên rồi
                {
                    #region Tang2
                    if (TF1[0].GioiThieu.ToString() != "0")
                    {
                        List<Entity.Member> TF2 = SMember.Name_Text("select * from Members where ID=" + TF1[0].GioiThieu.ToString() + "");
                        if (TF2.Count > 0)
                        {

                            if (TF2[0].TrangThaiMuaHang.ToString() == "1")
                            {
                                double Tang2 = Convert.ToDouble(Commond.Setting("ChietKhau2"));
                                if (Tang2 != 0)
                                {
                                    #region Công thức tính 4 số lượng đầu vẫn chia hoa hồng 1.1, 2.1, 3.150 triệu , Nhưng số lượng thứ 5 thì chỉ chia thêm là 200 ngàn nữa thôi
                                    double TSum = 0;
                                    if (TypeSoLuong == 1)
                                    {
                                        // Số lượng là 4 thì chia số lượng 4 * số tiền cấu hình
                                        // Số lượng Còn lại thì * số tiền cấu hình là 200
                                        TSum += Tang2 * SoLuongCu;
                                        TSum += SoTienHaiTram * SumSoLuong;
                                    }
                                    else
                                    {
                                        TSum = Tang2;// Số lượng là 1 đến 3 thì chia bình thường 
                                    }
                                    double TienHoaHong = TSum;
                                    #endregion
                                    double PTHH = ((TSum * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                    ThemHoaHong("ChietKhau", "Chiết khấu đại lý cấp 2", TienDauVao.ToString(), IDThanhVien.ToString(), TF2[0].ID.ToString(), PTHH.ToString(), TienHoaHong.ToString(), IDDonHang.ToString());
                                }
                            }
                            if (TF2[0].GioiThieu.ToString() != "0")
                            {
                                #region TF3

                                List<Entity.Member> TF3 = SMember.Name_Text("select * from Members where ID=" + TF2[0].GioiThieu.ToString() + "");
                                if (TF3.Count > 0)
                                {

                                    if (TF3[0].TrangThaiMuaHang.ToString() == "1")
                                    {
                                        double Tang3 = Convert.ToDouble(Commond.Setting("ChietKhau3"));
                                        if (Tang3 != 0)
                                        {
                                            #region Công thức tính 4 số lượng đầu vẫn chia hoa hồng 1.1, 2.1, 3.150 triệu , Nhưng số lượng thứ 5 thì chỉ chia thêm là 200 ngàn nữa thôi
                                            double TSum = 0;
                                            if (TypeSoLuong == 1)
                                            {
                                                // Số lượng là 4 thì chia số lượng 4 * số tiền cấu hình
                                                // Số lượng Còn lại thì * số tiền cấu hình là 200
                                                TSum += Tang3 * SoLuongCu;
                                                TSum += SoTienHaiTram * SumSoLuong;
                                            }
                                            else
                                            {
                                                TSum = Tang3;// Số lượng là 1 đến 3 thì chia bình thường 
                                            }
                                            double TienHoaHong = TSum;
                                            #endregion
                                            double PTHH = ((TSum * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                            ThemHoaHong("ChietKhau", "Chiết khấu đại lý cấp 3", TienDauVao.ToString(), IDThanhVien.ToString(), TF3[0].ID.ToString(), PTHH.ToString(), TienHoaHong.ToString(), IDDonHang.ToString());
                                        }
                                    }
                                    if (TF3[0].GioiThieu.ToString() != "0")
                                    {
                                        #region TF4

                                        List<Entity.Member> TF4 = SMember.Name_Text("select * from Members where ID=" + TF3[0].GioiThieu.ToString() + "");
                                        if (TF4.Count > 0)
                                        {

                                            if (TF4[0].TrangThaiMuaHang.ToString() == "1")
                                            {
                                                double Tang4 = Convert.ToDouble(Commond.Setting("ChietKhau4"));
                                                if (Tang4 != 0)
                                                {
                                                    #region Công thức tính 4 số lượng đầu vẫn chia hoa hồng 1.1, 2.1, 3.150 triệu , Nhưng số lượng thứ 5 thì chỉ chia thêm là 200 ngàn nữa thôi
                                                    double TSum = 0;
                                                    if (TypeSoLuong == 1)
                                                    {
                                                        // Số lượng là 4 thì chia số lượng 4 * số tiền cấu hình
                                                        // Số lượng Còn lại thì * số tiền cấu hình là 200
                                                        TSum += Tang4 * SoLuongCu;
                                                        TSum += SoTienHaiTram * SumSoLuong;
                                                    }
                                                    else
                                                    {
                                                        TSum = Tang4;// Số lượng là 1 đến 3 thì chia bình thường 
                                                    }
                                                    double TienHoaHong = TSum;
                                                    #endregion
                                                    double PTHH = ((TSum * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                    ThemHoaHong("ChietKhau", "Chiết khấu đại lý cấp 4", TienDauVao.ToString(), IDThanhVien.ToString(), TF4[0].ID.ToString(), PTHH.ToString(), TienHoaHong.ToString(), IDDonHang.ToString());
                                                }
                                            }
                                            if (TF4[0].GioiThieu.ToString() != "0")
                                            {
                                                #region TF5

                                                List<Entity.Member> TF5 = SMember.Name_Text("select * from Members where ID=" + TF4[0].GioiThieu.ToString() + "");
                                                if (TF5.Count > 0)
                                                {

                                                    if (TF5[0].TrangThaiMuaHang.ToString() == "1")
                                                    {
                                                        double Tang5 = Convert.ToDouble(Commond.Setting("ChietKhau5"));
                                                        if (Tang5 != 0)
                                                        {
                                                            #region Công thức tính 4 số lượng đầu vẫn chia hoa hồng 1.1, 2.1, 3.150 triệu , Nhưng số lượng thứ 5 thì chỉ chia thêm là 200 ngàn nữa thôi
                                                            double TSum = 0;
                                                            if (TypeSoLuong == 1)
                                                            {
                                                                // Số lượng là 4 thì chia số lượng 4 * số tiền cấu hình
                                                                // Số lượng Còn lại thì * số tiền cấu hình là 200
                                                                TSum += Tang5 * SoLuongCu;
                                                                TSum += SoTienHaiTram * SumSoLuong;
                                                            }
                                                            else
                                                            {
                                                                TSum = Tang5;// Số lượng là 1 đến 3 thì chia bình thường 
                                                            }
                                                            double TienHoaHong = TSum;
                                                            #endregion
                                                            double PTHH = ((TSum * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                            ThemHoaHong("ChietKhau", "Chiết khấu đại lý cấp 5", TienDauVao.ToString(), IDThanhVien.ToString(), TF5[0].ID.ToString(), PTHH.ToString(), TienHoaHong.ToString(), IDDonHang.ToString());
                                                        }
                                                    }
                                                    if (TF5[0].GioiThieu.ToString() != "0")
                                                    {
                                                        #region TF6
                                                        List<Entity.Member> TF6 = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                        if (TF6.Count > 0)
                                                        {
                                                            if (TF6[0].TrangThaiMuaHang.ToString() == "1")
                                                            {
                                                                double Tang6 = Convert.ToDouble(Commond.Setting("ChietKhau6"));
                                                                if (Tang6 != 0)
                                                                {
                                                                    #region Công thức tính 4 số lượng đầu vẫn chia hoa hồng 1.1, 2.1, 3.150 triệu , Nhưng số lượng thứ 5 thì chỉ chia thêm là 200 ngàn nữa thôi
                                                                    double TSum = 0;
                                                                    if (TypeSoLuong == 1)
                                                                    {
                                                                        // Số lượng là 4 thì chia số lượng 4 * số tiền cấu hình
                                                                        // Số lượng Còn lại thì * số tiền cấu hình là 200
                                                                        TSum += Tang6 * SoLuongCu;
                                                                        TSum += SoTienHaiTram * SumSoLuong;
                                                                    }
                                                                    else
                                                                    {
                                                                        TSum = Tang6;// Số lượng là 1 đến 3 thì chia bình thường 
                                                                    }
                                                                    double TienHoaHong = TSum;
                                                                    #endregion
                                                                    double PTHH = ((TSum * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                    ThemHoaHong("ChietKhau", "Chiết khấu đại lý cấp 6", TienDauVao.ToString(), IDThanhVien.ToString(), TF6[0].ID.ToString(), PTHH.ToString(), TienHoaHong.ToString(), IDDonHang.ToString());
                                                                }
                                                            }

                                                            #region Từ tầng số 6 sẽ tìm các cấp bậc để cho hoa hồng
                                                            #region Trưởng nhóm kinh doanh
                                                            if (!Tim_CapBac(TF6[0].GioiThieu.ToString(), "2").Equals("0"))
                                                            {
                                                                List<Entity.Member> TNKD = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                                if (TNKD.Count > 0)
                                                                {
                                                                    if (TNKD[0].TrangThaiMuaHang.ToString() == "1")
                                                                    {
                                                                        double HHTNKD = Convert.ToDouble(Commond.Setting("HHTNKD"));
                                                                        if (HHTNKD != 0)
                                                                        {
                                                                            double PTHH = ((HHTNKD * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                            ThemHoaHong("CapBac", "Trưởng nhóm kinh doanh", TienDauVao.ToString(), IDThanhVien.ToString(), Tim_CapBac(TF6[0].GioiThieu.ToString(), "2"), PTHH.ToString(), HHTNKD.ToString(), IDDonHang.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            #region Trưởng phòng kinh doanh
                                                            if (!Tim_CapBac(TF6[0].GioiThieu.ToString(), "3").Equals("0"))
                                                            {
                                                                List<Entity.Member> TNKD = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                                if (TNKD.Count > 0)
                                                                {
                                                                    if (TNKD[0].TrangThaiMuaHang.ToString() == "1")
                                                                    {
                                                                        double HHTPKD = Convert.ToDouble(Commond.Setting("HHTPKD"));
                                                                        if (HHTPKD != 0)
                                                                        {
                                                                            double PTHH = ((HHTPKD * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                            ThemHoaHong("CapBac", "Trưởng phòng kinh doanh", TienDauVao.ToString(), IDThanhVien.ToString(), Tim_CapBac(TF6[0].GioiThieu.ToString(), "3"), PTHH.ToString(), HHTPKD.ToString(), IDDonHang.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            #region Giám đốc kinh doanh
                                                            if (!Tim_CapBac(TF6[0].GioiThieu.ToString(), "4").Equals("0"))
                                                            {
                                                                List<Entity.Member> TNKD = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                                if (TNKD.Count > 0)
                                                                {
                                                                    if (TNKD[0].TrangThaiMuaHang.ToString() == "1")
                                                                    {
                                                                        double HHGDKD = Convert.ToDouble(Commond.Setting("HHGDKD"));
                                                                        if (HHGDKD != 0)
                                                                        {
                                                                            double PTHH = ((HHGDKD * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                            ThemHoaHong("CapBac", "Giám đốc kinh doanh", TienDauVao.ToString(), IDThanhVien.ToString(), Tim_CapBac(TF6[0].GioiThieu.ToString(), "4"), PTHH.ToString(), HHGDKD.ToString(), IDDonHang.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            #region Giám đốc Vùng
                                                            if (!Tim_CapBac(TF6[0].GioiThieu.ToString(), "5").Equals("0"))
                                                            {
                                                                List<Entity.Member> TNKD = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                                if (TNKD.Count > 0)
                                                                {
                                                                    if (TNKD[0].TrangThaiMuaHang.ToString() == "1")
                                                                    {
                                                                        double HHGDVung = Convert.ToDouble(Commond.Setting("HHGDVung"));
                                                                        if (HHGDVung != 0)
                                                                        {
                                                                            double PTHH = ((HHGDVung * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                            ThemHoaHong("CapBac", "Giám đốc Vùng", TienDauVao.ToString(), IDThanhVien.ToString(), Tim_CapBac(TF6[0].GioiThieu.ToString(), "5"), PTHH.ToString(), HHGDVung.ToString(), IDDonHang.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            #region Giám đốc Mien
                                                            if (!Tim_CapBac(TF6[0].GioiThieu.ToString(), "6").Equals("0"))
                                                            {
                                                                List<Entity.Member> TNKD = SMember.Name_Text("select * from Members where ID=" + TF5[0].GioiThieu.ToString() + "");
                                                                if (TNKD.Count > 0)
                                                                {
                                                                    if (TNKD[0].TrangThaiMuaHang.ToString() == "1")
                                                                    {
                                                                        double HHGDMien = Convert.ToDouble(Commond.Setting("HHGDMien"));
                                                                        if (HHGDMien != 0)
                                                                        {
                                                                            double PTHH = ((HHGDMien * 100 / TienDauVao));// công thức tính ra bao nhiêu %
                                                                            ThemHoaHong("CapBac", "Giám đốc Miền", TienDauVao.ToString(), IDThanhVien.ToString(), Tim_CapBac(TF6[0].GioiThieu.ToString(), "6"), PTHH.ToString(), HHGDMien.ToString(), IDDonHang.ToString());
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion

                                                            #endregion
                                                        }

                                                        #endregion
                                                    }
                                                }

                                                #endregion
                                            }
                                        }

                                        #endregion
                                    }
                                }

                                #endregion
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region Vi Loi Nhuan sau khi da chia HH
            try
            {
                Double TongTienHoaHongDaChiaCapBac = Convert.ToDouble(KiemTraTongTienHoaHongDaChiaCapBac(IDDonHang, IDThanhVien));
                Double TongCong = (TienDauVao - TongTienHoaHongDaChiaCapBac);
                LoiNhuanMuaBan abln = new LoiNhuanMuaBan();
                abln.IDThanhVienMua = int.Parse(IDThanhVien.ToString());
                abln.IDDonHang = IDDonHang.ToString();
                abln.MoTa = "";
                abln.NgayTao = DateTime.Now;
                abln.TongTien = TienDauVao.ToString();
                abln.TongTienCon = TongCong.ToString();
                abln.TongTienDaChia = TongTienHoaHongDaChiaCapBac.ToString();
                abln.MTreeIDThanhVienMua = Commond.ShowMTree(IDThanhVien.ToString());
                abln.NguoiDuyet = MoreAll.MoreAll.GetCookies("UName").ToString();
                db.LoiNhuanMuaBans.InsertOnSubmit(abln);
                db.SubmitChanges();
            }
            catch (Exception)
            { }
            #endregion
        }
        return "";
    }
    public static string Tim_CapBac(string id, string CapBac)
    {
        string str = "0";
        List<Entity.Member> dt = SMember.Name_Text("select top 1 * from Members  where ID=" + id + " ");
        if (dt.Count > 0)
        {
            if (dt[0].CapBac.ToString() == CapBac)
            {
                return dt[0].ID.ToString();
            }
            else
            {
                str = dt[0].GioiThieu.ToString();
                return Tim_CapBac(str, CapBac);
            }
        }
        return str;
    }
    public static string KiemTraTongTienHoaHongDaChiaCapBac(string IDDonHang, string IDThanhVien)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        Double num = 0;
        List<HoaHong> items = db.HoaHongs.Where(s => s.IDDonHang == IDDonHang && s.IDThanhVienMua == int.Parse(IDThanhVien)).ToList();// xóa nhiều
        if (items.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                num += Convert.ToDouble(items[i].SoTienDuocHuong.ToString());
            }
        }
        return num.ToString();
    }
    public static string KiemTraTongTienHoaHongDaChiaCapBac_DongHuong(string IDDonHang)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        Double num = 0;
        List<HoaHong> items = db.HoaHongs.Where(s => s.IDDonHang == IDDonHang).ToList();// xóa nhiều
        if (items.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                num += Convert.ToDouble(items[i].SoTienDuocHuong.ToString());
            }
        }
        return num.ToString();
    }
    public static void ThemHoaHong(string KieuHoaHong, string KieuHH, string TienDonHang, string IDThanhVienMua, string IDThanhVienHuong, string PhanTram, string SoTienDuocHuong, string IDDonHang)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        List<Entity.Member> F1 = SMember.Name_Text("select * from Members  where ID=" + IDThanhVienHuong.ToString() + " ");//DaKichHoat
        if (F1.Count() > 0)
        {
            #region HoaHongThanhVien
            HoaHong obj = new HoaHong();
            obj.KieuHoaHong = KieuHoaHong;
            obj.KieuHH = KieuHH;
            obj.TienDonHang = TienDonHang;
            obj.SoTienDuocHuong = SoTienDuocHuong;
            obj.PhanTram = PhanTram;
            obj.IDThanhVienMua = int.Parse(IDThanhVienMua);
            obj.IDThanhVienHuong = int.Parse(IDThanhVienHuong);
            obj.NgayTao = DateTime.Now;
            obj.IDDonHang = IDDonHang;
            db.HoaHongs.InsertOnSubmit(obj);
            db.SubmitChanges();
            #endregion
            List<Entity.Member> iitem = SMember.Name_Text("select * from Members where ID=" + IDThanhVienHuong.ToString() + "");
            if (iitem.Count > 0)
            {
                if (iitem[0].SauThangKhiThamGiaGoiCoDong == 1)
                {
                    CongTien_CoDong(IDThanhVienHuong, SoTienDuocHuong, IDDonHang);// nhận về ví chính 100%
                }
                else
                {
                    CongTien(IDThanhVienHuong, SoTienDuocHuong, IDDonHang);
                }
            }
        }
    }
    public static void CongTien(string IDUserNguoiDuocHuong, string SoTienDuocHuong, string maGD)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        #region Cộng điểm theo hoa hồng


        List<Entity.Member> iitem = SMember.Name_Text("select * from Members where ID=" + IDUserNguoiDuocHuong.ToString() + "");
        if (iitem.Count > 0)
        {
            double ViBayMuoiPT = Convert.ToDouble(iitem[0].ViBayMuoiPT);
            double ViBaMuoiPT = Convert.ToDouble(iitem[0].ViBaMuoiPT);
            double TongTienNapVao = Convert.ToDouble(SoTienDuocHuong);

            double TongBayMoi = (TongTienNapVao * 70) / 100;
            double TongBaMuoi = (TongTienNapVao * 30) / 100;

            double Conglai_BayMoi = 0;
            Conglai_BayMoi = ((ViBayMuoiPT) + (TongBayMoi));

            double Conglai_BaMoi = 0;
            Conglai_BaMoi = ((ViBaMuoiPT) + (TongBaMuoi));

            double TongTienCongDon = Convert.ToDouble(iitem[0].TongTienCongLai);
            double ConglaiCD = 0;
            ConglaiCD = ((TongTienCongDon) + (TongTienNapVao));
            if (Conglai_BayMoi > 0 && Conglai_BaMoi > 0)
                SMember.Name_Text("update Members set ViBayMuoiPT=" + Conglai_BayMoi.ToString() + ",ViBaMuoiPT=" + Conglai_BaMoi.ToString() + ",TongTienCongLai=" + ConglaiCD + "  where ID=" + iitem[0].ID.ToString() + "");

            try
            {
                if (TongBayMoi > 0)
                    Save_LichSuViGiaoDich(iitem[0].ID.ToString(), "HoaHong", "Hoa hồng Ví rút", TongBayMoi.ToString(), maGD);
                if (TongBaMuoi > 0)
                    Save_LichSuViGiaoDich(iitem[0].ID.ToString(), "HoaHong", "Hoa hồng Ví tạm giữ", TongBaMuoi.ToString(), maGD);
            }
            catch (Exception)
            { }

            try
            {
                CapNhat_CapBac(IDUserNguoiDuocHuong);
            }
            catch (Exception)
            {
            }

        }
        #endregion
    }
    public static void ThemHoaHong_CoDong(string KieuHoaHong, string KieuHH, string TienDonHang, string IDThanhVienMua, string IDThanhVienHuong, string PhanTram, string SoTienDuocHuong, string IDDonHang)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        List<Entity.Member> F1 = SMember.Name_Text("select * from Members  where ID=" + IDThanhVienHuong.ToString() + " ");//DaKichHoat
        if (F1.Count() > 0)
        {
            #region HoaHongThanhVien
            HoaHong obj = new HoaHong();
            obj.KieuHoaHong = KieuHoaHong;
            obj.KieuHH = KieuHH;
            obj.TienDonHang = TienDonHang;
            obj.SoTienDuocHuong = SoTienDuocHuong;
            obj.PhanTram = PhanTram;
            obj.IDThanhVienMua = int.Parse(IDThanhVienMua);
            obj.IDThanhVienHuong = int.Parse(IDThanhVienHuong);
            obj.NgayTao = DateTime.Now;
            obj.IDDonHang = IDDonHang;
            db.HoaHongs.InsertOnSubmit(obj);
            db.SubmitChanges();
            #endregion
            try
            {
                // Nang_Cap_Bac_ThanhVien(IDThanhVienHuong);
            }
            catch (Exception)
            { }
            CongTien_CoDong(IDThanhVienHuong, SoTienDuocHuong, IDDonHang);
        }
    }
    public static void CongTien_CoDong(string IDUserNguoiDuocHuong, string SoTienDuocHuong, string maGD)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        #region Cộng điểm theo hoa hồng
        List<Entity.Member> iitem = SMember.Name_Text("select * from Members where ID=" + IDUserNguoiDuocHuong.ToString() + "");
        if (iitem.Count > 0)
        {
            double ViBayMuoiPT = Convert.ToDouble(iitem[0].ViBayMuoiPT);
            double TongTienNapVao = Convert.ToDouble(SoTienDuocHuong);

            double Conglai_BayMoi = 0;
            Conglai_BayMoi = ((ViBayMuoiPT) + (TongTienNapVao));

            double TongTienCongDon = Convert.ToDouble(iitem[0].TongTienCongLai);
            double ConglaiCD = 0;
            ConglaiCD = ((TongTienCongDon) + (TongTienNapVao));

            if (Conglai_BayMoi > 0)
                SMember.Name_Text("update Members set ViBayMuoiPT=" + Conglai_BayMoi.ToString() + ",TongTienCongLai=" + ConglaiCD + "  where ID=" + iitem[0].ID.ToString() + "");

            try
            {
                if (TongTienNapVao > 0)
                    Save_LichSuViGiaoDich(iitem[0].ID.ToString(), "HoaHong", "Hoa hồng Ví rút", TongTienNapVao.ToString(), maGD);
                // Save_LichSuViGiaoDich(iitem[0].ID.ToString(), "HoaHong", "Hoa hồng Ví tạm giữ", TongBaMuoi.ToString(), maGD);
            }
            catch (Exception)
            { }

            try
            {
                CapNhat_CapBac(IDUserNguoiDuocHuong);
            }
            catch (Exception)
            {
            }

        }
        #endregion
    }

  
    public static string TimF1_XacDinhViTri(string IDThanhVien, string GioiThieu)
    {
        List<Entity.Member> dt = SMember.Name_Text("select * from Members where ID=" + GioiThieu + "");
        if (dt.Count > 0)
        {
            // Tìm ông A xem đang giới thiệu được mấy ng rồi
            string Vitri = "1";
            if (dt[0].TongViTriCuaA.ToString() == "0")
            {
                Vitri = "1";
            }
            else if (dt[0].TongViTriCuaA.ToString() == "1")
            {
                Vitri = "2";
            }
            else if (dt[0].TongViTriCuaA.ToString() == "2")
            {
                Vitri = "3";
            }
            else if (dt[0].TongViTriCuaA.ToString() == "3")
            {
                Vitri = "4";
            }
            List<Entity.Member> F1 = SMember.Name_Text("select * from Members where ID=" + IDThanhVien + " and ViTriF1=0");
            if (F1.Count > 0)
            {
                // Sét lại cho ông A vị trí
                SMember.Name_Text("UPDATE [Members] SET TongViTriCuaA=" + Vitri + " WHERE ID=" + GioiThieu.ToString() + "");// Sét ng đi mua hàng là đang ở vị trí thứ 1 hay 2 hay là 3 để dựa vào còn cho tiền F1 khi đi Mình đi mua hàng
                // Tìm F1 và xem mình đang là ở vị trí thứ mấy để cho HH F1 với : 1,1, 2.1, 3.1 triệu
                // Chỉ sét cho F1 khi F1 có ViTriF1=0 còn khi có rồi ko cần sét
                // Ông F chỉ cần sét 1 lần thôi
                SMember.Name_Text("UPDATE [Members] SET ViTriF1=" + Vitri + " WHERE ID=" + IDThanhVien.ToString() + "");// Sét ng đi mua hàng là đang ở vị trí thứ 1 hay 2 hay là 3 để dựa vào còn cho tiền F1 khi đi Mình đi mua hàng
            }
        }
        return "0";
    }
    public static string CheckSauKhiTaiTieuDung_ChuyenVIBaMuoi_SangViBayMuoi(string IDThanhVien, string MaGD)
    {
        // Yêu cầu mới ngày 24/07 cứ mua hàng gói combo thì thì tự động được chuyetn số tiền 10 triệu về ví chính 
        List<Entity.Member> dt = SMember.Name_Text("select * from [Members]  where ID =" + IDThanhVien + " and TongSoLanMuaHang>1");
        if (dt.Count > 0)
        {
            if (dt[0].ViBaMuoiPT.ToString() != "0")
            {
                // Sét ví TrangThaiRuTienBangKhong=0
                double SoTienChuyenVeViChinh = Convert.ToDouble(Commond.Setting("SoTienChuyenVeViChinh"));
                double ViBayMuoiPT = Convert.ToDouble(dt[0].ViBayMuoiPT);
                double ViBaMuoiPT = Convert.ToDouble(dt[0].ViBaMuoiPT);
                if (ViBaMuoiPT > SoTienChuyenVeViChinh)
                {
                    double Sum = ViBaMuoiPT - SoTienChuyenVeViChinh;
                    double Conglai = ViBayMuoiPT + SoTienChuyenVeViChinh;
                    if (Sum > 0 && Conglai > 0)
                        SMember.Name_Text("update Members set TrangThaiRuTienBangKhong=0,ViBaMuoiPT=" + Sum + ",ViBayMuoiPT='" + Conglai + "' where ID=" + IDThanhVien + "");
                    try
                    {
                        ChiaHoaHong.Save_LichSuViGiaoDich(MoreAll.MoreAll.GetCookies("MembersID").ToString(), "ChuyenVi", "Ví tạm giữ sang Ví rút", SoTienChuyenVeViChinh.ToString(), MaGD);
                    }
                    catch (Exception)
                    { }
                }
                else
                {
                    // Nếu < 10 triệu theo cấu hình thì chuyển hết  về ví chính
                    double Sum = ViBayMuoiPT + ViBaMuoiPT;
                    if (Sum > 0)
                    {
                        SMember.Name_Text("update Members set TrangThaiRuTienBangKhong=0,ViBaMuoiPT=0,ViBayMuoiPT='" + Sum + "' where ID=" + IDThanhVien + "");
                        try
                        {
                            ChiaHoaHong.Save_LichSuViGiaoDich(MoreAll.MoreAll.GetCookies("MembersID").ToString(), "ChuyenVi", "Ví tạm giữ sang Ví rút", ViBaMuoiPT.ToString(), MaGD);
                        }
                        catch (Exception)
                        { }
                    }
                }

            }

        }
        return "";
    }
    public static void Save_LichSuViGiaoDich(string IDThanhViens, string TypeKieuVis, string KieuTexts, string TongTiens, string MaGiaoDich)
    {
        try
        {
            DatalinqDataContext db = new DatalinqDataContext();
            LichSuViGiaoDich obj = new LichSuViGiaoDich();
            obj.IDThanhVien = Convert.ToInt32(IDThanhViens);
            obj.TypeKieuVi = TypeKieuVis;
            obj.KieuText = KieuTexts;
            obj.TongTien = TongTiens;
            obj.NgayTao = DateTime.Now;
            obj.MaGiaoDich = MaGiaoDich;
            db.LichSuViGiaoDiches.InsertOnSubmit(obj);
            db.SubmitChanges();
        }
        catch (Exception)
        {
        }
    }

    public static void CapNhat_CapBac_DaiLy(string IDThanhVien)
    {
        DatalinqDataContext db = new DatalinqDataContext();
        #region  Chỉ cần đi mua 1 gói combo thài trở thành Đại lý Cấp 1
        List<Entity.Member> Fcap = SMember.Name_Text("SELECT *  FROM Members  WHERE CapBac=0 and  ID=" + IDThanhVien + "");
        if (Fcap.Count > 0)
        {
            SMember.Name_Text("update Members set CapBac=1 where ID=" + IDThanhVien.ToString() + "");
        }
        #endregion
    }
    public static void CapNhat_CapBac(string IDThanhVien)
    {

        DatalinqDataContext db = new DatalinqDataContext();
        List<Entity.Member> F1 = SMember.Name_Text("select * from Members  where ID=" + IDThanhVien.ToString() + " and TrangThaiMuaHang=1");//DaKichHoat
        if (F1.Count() > 0)
        {

            //#region  Đại lý Cấp 1 , Tìm ra 6 F1
            //if (Other.Giatri("Tim6F1") != "" && Other.Giatri("Tim6F1") != "0")
            //{
            //    List<Entity.Member> Fcap = SMember.Name_Text("SELECT top " + Other.Giatri("Tim6F1") + " *  FROM Members  WHERE  Gioithieu=" + IDThanhVien + " and TrangThaiMuaHang=1");
            //    if (Fcap.Count > 0)
            //    {
            //        if (Convert.ToDouble(Fcap.Count) >= Convert.ToDouble(Other.Giatri("Tim6F1")))
            //            SMember.Name_Text("update Members set CapBac=1 where ID=" + IDThanhVien.ToString() + "");
            //    }
            //}
            //#endregion


            #region TN Kinh Doanh, Tìm ra 3 Đại lý cấp 1
            if (Other.Giatri("Tim3DaiLyCap1") != "" && Other.Giatri("Tim3DaiLyCap1") != "0")
            {
                List<Entity.Member> Fcap = SMember.Name_Text("SELECT top " + Other.Giatri("Tim3DaiLyCap1") + " *  FROM Members  WHERE  Gioithieu=" + IDThanhVien + " and CapBac=1 and TrangThaiMuaHang=1");
                if (Fcap.Count > 0)
                {
                    if (Convert.ToDouble(Fcap.Count) >= Convert.ToDouble(Other.Giatri("Tim3DaiLyCap1")))
                        SMember.Name_Text("update Members set CapBac=2 where ID=" + IDThanhVien.ToString() + "");
                }
            }
            #endregion


            #region TP Kinh Doanh, Tìm ra 3 Đại lý cấp 1
            if (Other.Giatri("Tim6DaiLyCap1") != "" && Other.Giatri("Tim6DaiLyCap1") != "0")
            {
                List<Entity.Member> Fcap = SMember.Name_Text("SELECT top " + Other.Giatri("Tim6DaiLyCap1") + " *  FROM Members  WHERE  Gioithieu=" + IDThanhVien + " and CapBac=1 and TrangThaiMuaHang=1");
                if (Fcap.Count > 0)
                {
                    if (Convert.ToDouble(Fcap.Count) >= Convert.ToDouble(Other.Giatri("Tim6DaiLyCap1")))
                        SMember.Name_Text("update Members set CapBac=3 where ID=" + IDThanhVien.ToString() + "");
                }
            }
            #endregion


            #region GĐ Kinh Doanh, Tìm ra 3 Đại lý cấp 1
            if (Other.Giatri("Tim12DaiLyCap1") != "" && Other.Giatri("Tim12DaiLyCap1") != "0")
            {
                List<Entity.Member> Fcap = SMember.Name_Text("SELECT top " + Other.Giatri("Tim12DaiLyCap1") + " *  FROM Members  WHERE  Gioithieu=" + IDThanhVien + " and CapBac=1 and TrangThaiMuaHang=1");
                if (Fcap.Count > 0)
                {
                    if (Convert.ToDouble(Fcap.Count) >= Convert.ToDouble(Other.Giatri("Tim12DaiLyCap1")))
                        SMember.Name_Text("update Members set CapBac=4 where ID=" + IDThanhVien.ToString() + "");
                }
            }
            #endregion


            #region GĐ Vùng, Tìm ra 3 Giám đốc kinh doanh
            if (Other.Giatri("Tim2GiamDocKinhDoanh") != "" && Other.Giatri("Tim2GiamDocKinhDoanh") != "0")
            {
                List<Entity.Member> Fcap = SMember.Name_Text("SELECT top " + Other.Giatri("Tim2GiamDocKinhDoanh") + " *  FROM Members  WHERE  Gioithieu=" + IDThanhVien + " and CapBac=4 and TrangThaiMuaHang=1");
                if (Fcap.Count > 0)
                {
                    if (Convert.ToDouble(Fcap.Count) >= Convert.ToDouble(Other.Giatri("Tim2GiamDocKinhDoanh")))
                        SMember.Name_Text("update Members set CapBac=5 where ID=" + IDThanhVien.ToString() + "");
                }
            }
            #endregion


            #region GĐ Miền, Tìm ra 3 Giám đốc kinh doanh

            //Tim2GiamDocVung
            if (Other.Giatri("Tim2GiamDocVung") != "" && Other.Giatri("Tim2GiamDocVung") != "0")
            {
                List<Entity.Member> Fcap = SMember.Name_Text("SELECT top " + Other.Giatri("Tim2GiamDocVung") + " *  FROM Members  WHERE  Gioithieu=" + IDThanhVien + " and CapBac=5 and TrangThaiMuaHang=1");
                if (Fcap.Count > 0)
                {
                    if (Convert.ToDouble(Fcap.Count) >= Convert.ToDouble(Other.Giatri("Tim2GiamDocVung")))
                        SMember.Name_Text("update Members set CapBac=6 where ID=" + IDThanhVien.ToString() + "");
                }
            }

            //Tim3GiamDocKinhDoanh
            if (Other.Giatri("Tim3GiamDocKinhDoanh") != "" && Other.Giatri("Tim3GiamDocKinhDoanh") != "0")
            {
                List<Entity.Member> Fcap = SMember.Name_Text("SELECT top " + Other.Giatri("Tim3GiamDocKinhDoanh") + " *  FROM Members  WHERE  Gioithieu=" + IDThanhVien + " and CapBac=4 and TrangThaiMuaHang=1");
                if (Fcap.Count > 0)
                {
                    if (Convert.ToDouble(Fcap.Count) >= Convert.ToDouble(Other.Giatri("Tim3GiamDocKinhDoanh")))
                        SMember.Name_Text("update Members set CapBac=6 where ID=" + IDThanhVien.ToString() + "");
                }
            }

            #endregion

        }
    }

    // Có 3 loại chia hoa hồng
    //1: Mua Combo bình thường  , F1 dc 1 hoặc 2, hoặc 2 hoặc 3 triệu trực tiếp, các F2 đến F6 được 600, 600, 300, 300 ,200 , các cấp bậc ko quan tâm 
    // Nhưng số lượng >4 theo cấu hình thì F1 đến F6 chỉ dc nhận thêm 200 K theo 1 số lượng
    //2: Mua Combo Cổ đông  , F1 dc 18 trực tiếp, các F2 đến F6 được 600, 600, 300, 300 ,200 , các cấp bậc ko quan tâm 
    //3: Mua Combo tái tiêu dùng sau 4 lần rút tiền về ví chính =0 đồng   , F1 , các F2 đến F6 được 200, 200, 200, 200 ,200 , các cấp bậc ko quan tâm

    // Ví tiền 
    // Sau khi rút ví tiền từ ví tạm giữ sang ví chính 4 lần theo cấu hình thì đơn hàng thứ 4 trở đi sẽ được chia theo hoa hồng tái tieu dùng


    //Nâng cấp ngày 24/07 tối ngày chạy code đầu tiên 

    //---------Chia hoa hồng Gói cổ đông----------

    //** KHi A là thanh viên bình thường giới thiệu ra B mà B tham gia gói cổ đông
    // --> thì khi B và gói cổ đông thì chia cho A vẫn là dạng 70 và 30
    //** Khi B là cổ đông giới thiệu ra C tham gia gói cổ đông 
    //--> Khi C đi mua gói cổ đông thì B sẽ nhận về ví rút tiền là 100%  
    //---> chú ý: các tầng khác vẫn chia hoa hồng 70 và 30 
    //---------Gói cổ đông----------

    //Khi B và C đã tham gia gói cổ đông thì trong vòng 6 tháng tính từ ngày mua gói cổ đông sẽ được nhận tất cả các hoa hồng từ gói combo bình thường đến gói
    //combo cổ đông đều nhận về ví chính là 100% , Sau 6 tháng thì lại tính quy luật nhận hoa hồng thành 2 ví 70 và 30

    //-------Rút tiền------------

    //*,Ví 70 thích rút bao nhiều thì rút, Ko cần quan tâm giới hạn số tiền rút là bao nhiêu.

    //*,Ví 30:
    //--> Giả sử ví 30 có 20 triệu
    //--> Muốn rút 20 triệu thì phải mua gói combo 4,5 triệu thì tự động ví tạm giữ sẽ đổ về ví chính là 10 triệu
    //--> Mua lần thứ 2 thì 10 triệu còn lại sẽ đổ nốt về ví chính 10 triệu nữa là tròn 20 triệu

    //Phải chia thành 2 lần đi mua thì 20 triệu mới về hết ví chính



}
