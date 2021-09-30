using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    public class DanhGiaSao
    {
        #region[Entity Private]
        private int _ID;
        private int _IDThanhVien;
        private int _IDSanPham;
        private int _IDGiaoVien;
        private int _MotSao;
        private int _HaiSao;
        private int _BaSao;
        private int _BonSao;
        private int _NamSao;
        private string _NoiDung;
        private DateTime _Create_Date;
        #endregion

        #region[Properties]
        public int  ID { get { return _ID; } set { _ID = value; } }
        public int IDThanhVien { get { return _IDThanhVien; } set { _IDThanhVien = value; } }
        public int IDSanPham { get { return _IDSanPham; } set { _IDSanPham = value; } }
        public int IDGiaoVien { get { return _IDGiaoVien; } set { _IDGiaoVien = value; } }
        public int MotSao { get { return _MotSao; } set { _MotSao = value; } }
        public int HaiSao { get { return _HaiSao; } set { _HaiSao = value; } }
        public int BaSao { get { return _BaSao; } set { _BaSao = value; } }
        public int BonSao { get { return _BonSao; } set { _BonSao = value; } }
        public int NamSao { get { return _NamSao; } set { _NamSao = value; } }
        public string NoiDung { get { return _NoiDung; } set { _NoiDung = value; } }
        public DateTime Create_Date { get { return _Create_Date; } set { _Create_Date = value; } }
        #endregion

    }
}
