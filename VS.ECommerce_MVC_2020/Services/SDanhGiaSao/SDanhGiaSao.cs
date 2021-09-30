using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;
using Entity;

namespace Services
{
    public class SDanhGiaSao
    {
        private static FDanhGiaSao db = new FDanhGiaSao();

        #region Name_Text
        public static List<DanhGiaSao> Name_Text(string Name_Text)
        {
            return db.Name_Text(Name_Text);
        }
        #endregion
    }
}
