using GHTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http;
using System.IO;
namespace VS.ECommerce_MVC.Controllers
{
    public class VanCHuyenController : BaseController
    {
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    return View();
        //}
        string LinkApi = "https://services.giaohangtietkiem.vn/authentication-request-sample";
        string Tokiem = "89ceA31EC615B829085cDe3f6f155bFcFc8987Af";

        public class Authentication
        {
            public bool success { get; set; }
            public string message { get; set; }
            // public GhtkFeeModel fee { get; set; }
        }

        public async Task Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://hungphong.com.vn");
                var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", "login")
            });
                var result = await client.PostAsync("/News/News_GetAll", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                ViewBag.Thongbao = resultContent;
            }
        }
        
    }
}