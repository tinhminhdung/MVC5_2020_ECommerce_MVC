using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class Member
    {
        #region[Entity Private]
        private int _ID;
        private string _PasWord;
        private string _HoVaTen;
        private int _GioiTinh;
        private string _NgaySinh;
        private string _DiaChi;
        private string _DienThoai;
        private string _Email;
        private string _AnhDaiDien;
        private DateTime _NgayTao;
        private string _Key;
        private int _TrangThai;
        private string _Lang;

        private string _ViBaMuoiPT;
        private string _ViBayMuoiPT;
        private string _TongTienDaRut;
        private string _TongTienCongLai;
        private int _CapBac;
        private int _TongViTriCuaA;
        private int _ViTriF1;
        private int _GioiThieu;
        private string _MTRee;
        private int _TrangThaiRutTien;
        private int _TrangThaiMuaHang;

        private int _TrangThaiRuTienBangKhong;
        private int _TongSoLanRutTienBangKhong;

        private string _TenNganHang;
        private string _SoTaiKhoan;
        private string _ChuTaiKhoan;
        private string _ChiNhanh;

        private string _TinhThanh;
        private string _QuanHuyen;
        private string _PhuongXa;

        private int _TrangThaiThamGiaCoDong;// trạng thái tham gia goi cổ đông
        private int _SauThangKhiThamGiaGoiCoDong;// trạng thái khi tham gia gói cổ đông thì dc 6 tháng nhận hoa hồng về ví 100% sau 6 tháng thì sẽ nhận về 2 ví 70 và 30
        private DateTime _ThoiGianThamGiaGoiCoDong;// ngày tham gia gói cổ đông
        private DateTime _NgayKetThucThamGiaGoiCoDong;// ngày kế thúc gia gói cổ đông
        private DateTime _ThoiGianMuaHang;
        private int _TongSoLanMuaHang;

        #endregion

        #region[Properties]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string PasWord { get { return _PasWord; } set { _PasWord = value; } }
        public string HoVaTen { get { return _HoVaTen; } set { _HoVaTen = value; } }
        public int GioiTinh { get { return _GioiTinh; } set { _GioiTinh = value; } }
        public string NgaySinh { get { return _NgaySinh; } set { _NgaySinh = value; } }
        public string DiaChi { get { return _DiaChi; } set { _DiaChi = value; } }
        public string DienThoai { get { return _DienThoai; } set { _DienThoai = value; } }
        public string Email { get { return _Email; } set { _Email = value; } }
        public string AnhDaiDien { get { return _AnhDaiDien; } set { _AnhDaiDien = value; } }
        public DateTime NgayTao { get { return _NgayTao; } set { _NgayTao = value; } }
        public string Key { get { return _Key; } set { _Key = value; } }
        public int TrangThai { get { return _TrangThai; } set { _TrangThai = value; } }
        public string Lang { get { return _Lang; } set { _Lang = value; } }
        public string ViBaMuoiPT { get { return _ViBaMuoiPT; } set { _ViBaMuoiPT = value; } }
        public string ViBayMuoiPT { get { return _ViBayMuoiPT; } set { _ViBayMuoiPT = value; } }
        public string TongTienDaRut { get { return _TongTienDaRut; } set { _TongTienDaRut = value; } }
        public string TongTienCongLai { get { return _TongTienCongLai; } set { _TongTienCongLai = value; } }
        public int CapBac { get { return _CapBac; } set { _CapBac = value; } }
        public int TongViTriCuaA { get { return _TongViTriCuaA; } set { _TongViTriCuaA = value; } }
        public int ViTriF1 { get { return _ViTriF1; } set { _ViTriF1 = value; } }
        public int GioiThieu { get { return _GioiThieu; } set { _GioiThieu = value; } }
        public string MTRee { get { return _MTRee; } set { _MTRee = value; } }
        public int TrangThaiRutTien { get { return _TrangThaiRutTien; } set { _TrangThaiRutTien = value; } }
        public int TrangThaiMuaHang { get { return _TrangThaiMuaHang; } set { _TrangThaiMuaHang = value; } }

        public int TrangThaiRuTienBangKhong { get { return _TrangThaiRuTienBangKhong; } set { _TrangThaiRuTienBangKhong = value; } }
        public int TongSoLanRutTienBangKhong { get { return _TongSoLanRutTienBangKhong; } set { _TongSoLanRutTienBangKhong = value; } }

        public string TenNganHang { get { return _TenNganHang; } set { _TenNganHang = value; } }
        public string SoTaiKhoan { get { return _SoTaiKhoan; } set { _SoTaiKhoan = value; } }
        public string ChuTaiKhoan { get { return _ChuTaiKhoan; } set { _ChuTaiKhoan = value; } }
        public string ChiNhanh { get { return _ChiNhanh; } set { _ChiNhanh = value; } }

        public string TinhThanh { get { return _TinhThanh; } set { _TinhThanh = value; } }
        public string QuanHuyen { get { return _QuanHuyen; } set { _QuanHuyen = value; } }
        public string PhuongXa { get { return _PhuongXa; } set { _PhuongXa = value; } }

        public int TrangThaiThamGiaCoDong { get { return _TrangThaiThamGiaCoDong; } set { _TrangThaiThamGiaCoDong = value; } }
        public int SauThangKhiThamGiaGoiCoDong { get { return _SauThangKhiThamGiaGoiCoDong; } set { _SauThangKhiThamGiaGoiCoDong = value; } }
        public DateTime ThoiGianThamGiaGoiCoDong { get { return _ThoiGianThamGiaGoiCoDong; } set { _ThoiGianThamGiaGoiCoDong = value; } }
        public DateTime NgayKetThucThamGiaGoiCoDong { get { return _NgayKetThucThamGiaGoiCoDong; } set { _NgayKetThucThamGiaGoiCoDong = value; } }
        public DateTime ThoiGianMuaHang { get { return _ThoiGianMuaHang; } set { _ThoiGianMuaHang = value; } }
        public int TongSoLanMuaHang { get { return _TongSoLanMuaHang; } set { _TongSoLanMuaHang = value; } }
        #endregion
    }
}
