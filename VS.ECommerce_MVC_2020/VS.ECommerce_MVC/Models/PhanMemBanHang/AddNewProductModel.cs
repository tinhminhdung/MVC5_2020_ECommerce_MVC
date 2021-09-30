using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class AddNewProductModel
{
    public class ThemMoiSanPham
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Unit { get; set; }
        public int UnitPrice { get; set; }
        public int PurchasePrice { get; set; }
        public int MinInStock { get; set; }
        public int MaxInStock { get; set; }
        public string GroupId { get; set; }
        public string Description { get; set; }
        public string s_NoteImport { get; set; }
        public string s_NoteOrder { get; set; }
        public string Picture { get; set; }
    }

    public class DataThemMoi
    {
        public DataThemMoi()
        {
            ImageUrls = new List<string>();
            ExtraLabels = new List<string>();
            ExtraValues = new List<string>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
        public float InStock { get; set; }
        public float MinInStock { get; set; }
        public float MaxInStock { get; set; }
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
        public float f_Convert { get; set; }
    }
    public class DataThemMoisss
    {
        public string Id { get; set; }
        // [JsonProperty(" Name")]
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
        public object s_Specification { get; set; }
        public object s_Quantification { get; set; }
        public object s_Color { get; set; }
        public object s_TacDungChinh { get; set; }
        public string s_NoteImport { get; set; }
        public string s_NoteOrder { get; set; }
        public object s_ThanhPhan { get; set; }
        public List<object> ImageUrls { get; set; }
        public List<object> ExtraLabels { get; set; }
        public List<object> ExtraValues { get; set; }
        public object Picture { get; set; }
        public object Unit_ID { get; set; }
        public int f_Convert { get; set; }
    }

    public class MetaThemMoi
    {
        public double status_code { get; set; }
        public string message { get; set; }
    }

    public class RootThemMoi
    {
        public DataThemMoi data { get; set; }
        public MetaThemMoi meta { get; set; }
    }

}

