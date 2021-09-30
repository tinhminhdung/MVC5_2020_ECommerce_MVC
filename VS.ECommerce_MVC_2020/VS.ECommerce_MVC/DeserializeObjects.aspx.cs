using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;
using System.Collections;
using System.Reflection;

namespace VS.ECommerce_MVC
{
    public partial class DeserializeObjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         //   Guid g = Guid.NewGuid();
           // Response.Write(GetContent("https://congdong.edu.vn/?aff=8888888"));

            // Create cookie

          //  var json = "{\"data\":{\"Id\":\"ef0f1f5b-ef0f-415a-8db8-59c297f3b706\",\" Name\":\"loa 2\",\"ProductCode\":\"loa2\",\"Unit\":\"c\",\"UnitPrice\":2000.0000,\"Price\":3000.0000,\"InStock\":4.3,\"MinInS tock\":0.0,\"MaxInStock\":0.0,\"BranchName\":\"GIADUNG\",\"GroupName\":\"AM THANH HLoV\",\"GroupId\":\"f5d8084c-9d0b-4b 60-b685-03bf801c38da\",\"Description\":\"\",\"s_Specification\":\"\",\"s_Quantification\":\"\",\"s_Color\":\"\",\"s_TacDungC hinh\":\"\",\"s_NoteImport\":\"\",\"s_NoteOrder\":\"\",\"s_ThanhPhan\":\"\",\"ImageUrls\":\"\",\"ExtraLabels\":\"\",\"ExtraValues\":\"\" ,\"Picture\":null,\"Unit_ID\":null,\"f_Convert\":0.0},\"meta\":{\"status_code\":0,\"message\":\"Successfully\"}}";
          //  var obj = JsonConvert.DeserializeObject<Root>(json);
          ////  foreach (var item in obj.data)
          //  //{
          //  Response.Write(obj.data.Name + " - " + obj.data.Price);
          //  //}
          
        }

        public static string GetContent(string URL)
        {
            return GetContent(URL, false);
        }

        public static string GetContent(string URL, bool Method)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                string address = null;
                string userAgent = string.Empty;
                try
                {
                    userAgent = HttpContext.Current.Request.UserAgent;
                }
                catch
                {
                }
                if (Check(userAgent))
                {
                    request.UserAgent = userAgent;
                }
                else
                {
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/534.57.2 (KHTML, like Gecko) Version/5.1.7 Safari/534.57.2";
                }
                request.Proxy = new WebProxy(address, true);
                request.CookieContainer = new CookieContainer();
                request.Method = Method ? "POST" : "GET";
                StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.UTF8);
                return reader.ReadToEnd();
            }
            catch
            {
                return "";
            }
        }
        public static bool Check(object String)
        {
            return ((String != null) && (String.ToString().Trim().Length > 0));
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Data
        {
            public string Id { get; set; }
           // [JsonProperty(" Name")]
            public string Name { get; set; }
            public string ProductCode { get; set; }
            public string Unit { get; set; }
            public double UnitPrice { get; set; }
            public double Price { get; set; }
            public double InStock { get; set; }
            public double MinInSTock { get; set; }
            public double MaxInStock { get; set; }
            public string BranchName { get; set; }
            public string GroupName { get; set; }
            public string GroupId { get; set; }
            public string Description { get; set; }
            public string s_Specification { get; set; }
            public string s_Quantification { get; set; }
            public string s_Color { get; set; }
            public string STacDungCHinh { get; set; }
            public string s_NoteImport { get; set; }
            public string s_NoteOrder { get; set; }
            public string s_ThanhPhan { get; set; }
            public string ImageUrls { get; set; }
            public string ExtraLabels { get; set; }
            public string ExtraValues { get; set; }
            public object Picture { get; set; }
            public object Unit_ID { get; set; }
            public double f_Convert { get; set; }
        }

        public class Meta
        {
            public int status_code { get; set; }
            public string message { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public Meta meta { get; set; }
        }



        //public class RootObject
        //{
        //    public List<Item> response { get; set; }
        //}
        //public class Item
        //{
        //    public string first_name { get; set; }
        //    public string last_name { get; set; }
        //    public string domain { get; set; }
        //    public string photo_50 { get; set; }
        //}
    }
}