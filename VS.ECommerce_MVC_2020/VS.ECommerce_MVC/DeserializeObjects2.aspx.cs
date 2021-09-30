using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VS.ECommerce_MVC
{
    public partial class DeserializeObjects2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var json = "{\"data\":{\"Id\":\"ef0f1f5b-ef0f-415a-8db8-59c297f3b706\",\" Name\":\"loa 2\",\"ProductCode\":\"loa2\",\"Unit\":\"c\",\"UnitPrice\":2000.0000,\"Price\":3000.0000,\"InStock\":4.3,\"MinInS tock\":0.0,\"MaxInStock\":0.0,\"BranchName\":\"GIADUNG\",\"GroupName\":\"AM THANH HLoV\",\"GroupId\":\"f5d8084c-9d0b-4b 60-b685-03bf801c38da\",\"Description\":\"\",\"s_Specification\":\"\",\"s_Quantification\":\"\",\"s_Color\":\"\",\"s_TacDungC hinh\":\"\",\"s_NoteImport\":\"\",\"s_NoteOrder\":\"\",\"s_ThanhPhan\":\"\",\"ImageUrls\":\"\",\"ExtraLabels\":\"\",\"ExtraValues\":\"\" ,\"Picture\":null,\"Unit_ID\":null,\"f_Convert\":0.0},\"meta\":{\"status_code\":0,\"message\":\"Successfully\"}}";
            //var obj = JsonConvert.DeserializeObject<Root>(json);
            //Response.Write(obj.data.Name + " - " + obj.data.Price);

            var json = "{ \"data\": { \"Id\": \"ef0f1f5b-ef0f-415a-8db8-59c297f3b706\", \"Name\": \"loa 2\", \"ProductCode\": \"loa2\", \"Unit\": \"c\", \"UnitPrice\": 2000, \"Price\": 3000, \"InStock\": 4.3, \"MinInStock\": 0, \"MaxInStock\": 0, \"BranchName\": \"GIADUNG\", \"GroupName\": \"AM THANH HLoV\", \"GroupId\": \"f5d8084c-9d0b-4b60-b685-03bf801c38da\", \"Description\": \"\", \"s_Specification\": \"\", \"s_Quantification\": \"\", \"s_Color\": \"\", \"s_TacDungChinh\": \"\", \"s_NoteImport\": \"\", \"s_NoteOrder\": \"\", \"s_ThanhPhan\": \"\", \"ImageUrls\": [], \"ExtraLabels\": [ \"s_Color\", \"s_Quantification\", \"s_Specification\", \"s_TacDungChinh\", \"s_ThanhPhan\" ], \"ExtraValues\": [ \"\", \"\", \"\", \"\", \"\" ], \"Picture\": null, \"Unit_ID\": null, \"f_Convert\": 0 }, \"meta\": { \"status_code\": 0, \"message\": \"Successfully\" } }";
            var obj = JsonConvert.DeserializeObject<Root>(json);
            Response.Write(obj.data.Name + " - " + obj.data.Price);
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
            public string s_TacDungChinh { get; set; }
            public string s_NoteImport { get; set; }
            public string s_NoteOrder { get; set; }
            public string s_ThanhPhan { get; set; }
        //    public string ImageUrls { get; set; }
        //    public string ExtraLabels { get; set; }
        //    public string ExtraValues { get; set; }
        //    public object Picture { get; set; }
        //    public object Unit_ID { get; set; }
        //    public double f_Convert { get; set; }
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

    }
}