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
        #endregion
    }
}
