using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class DeleteProductModel
{
    public class Meta
    {
        public int status_code { get; set; }
        public string message { get; set; }
    }
    public class Root
    {
        public object data { get; set; }
        public Meta meta { get; set; }
    }
}

