using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using VS.ECommerce_MVC;

public class CacheDatabase
{

    #region CacheDatabase Product
    public static List<Entity.Category_Product> ProductCategory(IEnumerable<Entity.Category_Product> Get, string NameCache)
    {
        List<Entity.Category_Product> result = new List<Entity.Category_Product>();
        var cache = MemoryCache.Default;
        if (cache.Get(NameCache) == null)
        {
            var cachePolicty = new CacheItemPolicy();
            cachePolicty.AbsoluteExpiration = DateTime.Now.AddHours(200);
            result = Get.ToList();//SMenu.GETBYALL("VIE");
            cache.Add(NameCache, result, cachePolicty);
        }
        else
        {
            result = (List<Entity.Category_Product>)cache.Get(NameCache);
        }
        return result;
    }
    public static List<Entity.Products> Product(IEnumerable<Entity.Products> Get, string NameCache)
    {
        List<Entity.Products> result = new List<Entity.Products>();
        var cache = MemoryCache.Default;
        if (cache.Get(NameCache) == null)
        {
            var cachePolicty = new CacheItemPolicy();
            cachePolicty.AbsoluteExpiration = DateTime.Now.AddHours(200);
            result = Get.ToList();//SMenu.GETBYALL("VIE");
            cache.Add(NameCache, result, cachePolicty);
        }
        else
        {
            result = (List<Entity.Products>)cache.Get(NameCache);
        }
        return result;
    }
    #endregion

    #region CacheDatabase News
    public static List<Entity.Category_News> NewsCategory(IEnumerable<Entity.Category_News> Get, string NameCache)
    {
        List<Entity.Category_News> result = new List<Entity.Category_News>();
        var cache = MemoryCache.Default;
        if (cache.Get(NameCache) == null)
        {
            var cachePolicty = new CacheItemPolicy();
            cachePolicty.AbsoluteExpiration = DateTime.Now.AddHours(200);
            result = Get.ToList();//SMenu.GETBYALL("VIE");
            cache.Add(NameCache, result, cachePolicty);
        }
        else
        {
            result = (List<Entity.Category_News>)cache.Get(NameCache);
        }
        return result;
    }
    public static List<Entity.News> News(IEnumerable<Entity.News> Get, string NameCache)
    {
        List<Entity.News> result = new List<Entity.News>();
        var cache = MemoryCache.Default;
        if (cache.Get(NameCache) == null)
        {
            var cachePolicty = new CacheItemPolicy();
            cachePolicty.AbsoluteExpiration = DateTime.Now.AddHours(200);
            result = Get.ToList();//SMenu.GETBYALL("VIE");
            cache.Add(NameCache, result, cachePolicty);
        }
        else
        {
            result = (List<Entity.News>)cache.Get(NameCache);
        }
        return result;
    }
    #endregion


    // Ví dụ
    //public ActionResult CacheHtml()
    //{
    //    List<Entity.Category_News> table = SNews.Name_Text_Rg("SELECT inid,Alt,TangName,Title,Images,ImagesSmall,Brief,Create_Date FROM [News] WHERE new=1 AND lang='" + language + "'  AND Status=1  order by Create_Date desc");
    //    if (table.Count >= 1)
    //    {
    //        // Cách 2  Ko có  Cache
    //        ViewBag.Category = table;
    //    }
    //    // Cách 2  chạy có Cache Dabase
    //     ViewBag.data = CacheDatabase.NewsCategory(table, "CacheHtml");

    //    return View();
    //}
}
