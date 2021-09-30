using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class DauVaoDatHang
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Product
    {
        public string name { get; set; }
        public double weight { get; set; }
        public int quantity { get; set; }
        public int product_code { get; set; }
    }

    public class Order
    {
        public string id { get; set; }
        public string pick_name { get; set; }
        public string pick_address { get; set; }
        public string pick_province { get; set; }
        public string pick_district { get; set; }
        public string pick_ward { get; set; }
        public string pick_tel { get; set; }
        public string tel { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string ward { get; set; }
        public string hamlet { get; set; }
        public string is_freeship { get; set; }
        public string pick_date { get; set; }
        public int pick_money { get; set; }
        public string note { get; set; }
        public int value { get; set; }
        public string transport { get; set; }
        public string pick_option { get; set; }
        public string deliver_option { get; set; }
        public int pick_session { get; set; }
        public object tags { get; set; }

    }

    public class Root
    {
        public List<Product> products { get; set; }
        public Order order { get; set; }
    }


}