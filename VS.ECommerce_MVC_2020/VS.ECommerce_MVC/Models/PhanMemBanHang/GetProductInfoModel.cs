using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class GetProductInfo_Model
{
    public class Data
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string Unit { get; set; }
        public double UnitPrice { get; set; }
        public double Price { get; set; }
        public double InStock { get; set; }
        public double MinInStock { get; set; }
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
        public List<string> ImageUrls { get; set; }
        public List<string> ExtraLabels { get; set; }
        public List<string> ExtraValues { get; set; }
        public string Picture { get; set; }
        public string Unit_ID { get; set; }
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


}

