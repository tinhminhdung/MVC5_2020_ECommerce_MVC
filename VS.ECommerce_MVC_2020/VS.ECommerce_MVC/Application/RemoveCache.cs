using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

public class RemoveCache
{
    #region Xóa Cache Database
    public static string XoaCahCheDatabase(string cacheKey)
    {
        var lstCaches = MemoryCache.Default.Where(x => x.Key.ToLower().Contains(cacheKey.ToLower())).ToList();
        for (int i = 0; i < lstCaches.Count; i++)
        {
            MemoryCache.Default.Remove(lstCaches[i].Key);
        }
        return "";
    }
    #endregion

    public static string Home()
    {
        HttpContext.Current.Cache.Remove("Title");
        HttpContext.Current.Cache.Remove("Header");
        return "";
    }
    public static string Menu()
    {
        HttpContext.Current.Cache.Remove("MenuTop");
        HttpContext.Current.Cache.Remove("ShowMenuPage");
        HttpContext.Current.Cache.Remove("MenuBottom");
        return "";
    }
    public static string Products()
    {
        HttpContext.Current.Cache.Remove("ShowNhomsanpham");
     
        return "";
    }
    public static string News()
    {
        //HttpContext.Current.Cache.Remove("CacheShowNews");

        XoaCahCheDatabase("CacheHtml");
        return "";
    }
    public static string Album()
    {
        //HttpContext.Current.Cache.Remove("ShowNhomsanpham");
        return "";
    }
    public static string Video()
    {
        //HttpContext.Current.Cache.Remove("ShowNhomsanpham");

        // Cache Database 
        MemoryCache.Default.Remove("DanhMuc");
        return "";
    }

    public static string NewsFooter()
    {
        //HttpContext.Current.Cache.Remove("ShowNhomsanpham");
        return "";
    }
    public static string QuangCao()
    {
        HttpContext.Current.Cache.Remove("PhuongThucVanChuyen");
        return "";
    }
}
