using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VS.ECommerce_MVC.Models
{
    public class API_News
    {
        public int inid { get; set; }
        public int icid { get; set; }
        public string Title { get; set; }
        public string Brief { get; set; }
        public string Images { get; set; }
        public string ImagesSmall { get; set; }
        public string Create_Date;
        public int Views { get; set; }
        public string Contents { get; set; }
    }
}