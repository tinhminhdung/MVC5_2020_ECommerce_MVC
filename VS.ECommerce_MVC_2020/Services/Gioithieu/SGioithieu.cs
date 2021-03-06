using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;
using Entity;

namespace Services
{
    public class SGioithieu
    {
        private static FGioithieu db = new FGioithieu();

        #region GET BY ID
        public static List<Gioithieu> GET_BY_ID(string ID)
        {
            return db.GETBYID(ID);
        }
        #endregion

        #region GET BY TOP
        public static List<Gioithieu> GET_BY_TOP(string Top, string Where, string Order)
        {
            return db.GETBYTOP(Top, Where, Order);
        }
        #endregion

        #region GET BY ALL
        public static List<Gioithieu> GET_BY_ALL(string Lang)
        {
            return db.GETBYALL(Lang);
        }
        #endregion

        #region PAGING
        public static List<Gioithieu> PAGING(string CurentPage, string PageSize, string Lang)
        {
            return db.PAGING(CurentPage, PageSize, Lang);
        }
        #endregion

        #region INSERT
        public static bool INSERT(Gioithieu obj)
        {
            return db.Insert(obj);
        }
        #endregion

        #region UPDATE
        public static bool UPDATE(Gioithieu obj)
        {
            return db.Update(obj);
        }
        #endregion

        #region DELETE
        public static void DELETE(string ID)
        {
            db.DELETE(ID);
        }
        #endregion

        /////////////////////////////

        #region CATEGORY ADMIN
        public static List<Gioithieu> CATEGORY_ADMIN(string orderby, string searchkeyword, string[] searchfields, string lang, string Status)
        {
            return db.CATEGORY_ADMIN(orderby, searchkeyword, searchfields, lang, Status);
        }

        #endregion

        #region GET DETAIL BY ID
        public static List<Gioithieu> GET_DETAIL_BYID(string ID)
        {
            return db.GET_CATEGORY_BYID(ID);
        }
        #endregion

        #region CATE_DELETE_MENU_ID
        public static void CATE_DELETE_MENUID(string Menu_ID, string imcID)
        {
            db.CATE_DELETE_MENUID(Menu_ID, imcID);
        }
        #endregion

        #region GET DETAIL BY MENU ID
        public static List<Gioithieu> GET_DETAIL_BY_MENUID(string ID_Menu, string imcID)
        {
            return db.GETDETAIL_BYMENUID(ID_Menu, imcID);
        }
        #endregion

        #region UPDATE VIEWS
        public static bool UPDATE_VIEWS(string ID, int iviews)
        {
            return db.UPDATEVIEWS(ID, iviews);
        }
        #endregion

        #region UPDATE VIEW TIMES
        public static bool UPDAE_VIEW_TIMES(string ID)
        {
            return db.UPDATE_VIEWSTIME(ID);
        }
        #endregion

        #region UPDATE STATUS
        public static bool UPDATE_STATUS(string ID, string status)
        {
            return db.UPDATE_STATUS(ID, status);
        }
        #endregion

        #region UPDATE IMG
        public static bool UPDATE_IMG(string ID)
        {
            return db.UPDATEIMG(ID);
        }
        #endregion

        #region CATEGORY
        public static List<Gioithieu> CATEGORY(string lang)
        {
            return db.CATEGORY(lang);
        }
        #endregion

        #region CHECKDATA
        public static bool CHECKDATA(string ID, string Chekdata, DateTime Create_Date, DateTime Modified_Date)
        {
            return db.CHECKDATA(ID, Chekdata, Create_Date, Modified_Date);
        }
        #endregion

        #region UPDATE DATETIME
        public static bool UPDATE_DATETIME(string ID, DateTime Create_Date, DateTime Modified_Date)
        {
            return db.UPDATE_DATETIME(ID, Modified_Date, Modified_Date);
        }
        #endregion

        #region NEWS OTHER LAST
        public static List<Gioithieu> OTHERLAST(string nID, int top, string lang)
        {
            return db.NEWS_OTHERLAST(nID, top, lang);
        }
        #endregion

        #region NEWS OTEHR FIRST
        public static List<Gioithieu> OTHERFIRST(string nID, int top, string lang)
        {
            return db.NEWS_OTHERFIRST(nID, top, lang);
        }
        #endregion

        #region Search
        public static List<Gioithieu> Search(string keyword, string lang)
        {
            return db.Search(keyword, lang);
        }
        #endregion

        #region Name_StoredProcedure
        public static List<Gioithieu> Name_StoredProcedure(string Name_StoredProcedure)
        {
            return db.Name_StoredProcedure(Name_StoredProcedure);
        }
        #endregion

        #region Name_Text
        public static List<Gioithieu> Name_Text(string Name_Text)
        {
            return db.Name_Text(Name_Text);
        }
        #endregion
    }
}
