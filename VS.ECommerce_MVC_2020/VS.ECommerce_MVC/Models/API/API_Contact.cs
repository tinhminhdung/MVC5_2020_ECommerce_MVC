using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VS.ECommerce_MVC.Models
{
    public class API_Contact
    {
        public string vtitle { set; get; }
        public string vname { set; get; }
        public string vaddress { set; get; }
        public string vphone { set; get; }
        public string vemail { set; get; }
        public string vcontent { set; get; }
    }
}