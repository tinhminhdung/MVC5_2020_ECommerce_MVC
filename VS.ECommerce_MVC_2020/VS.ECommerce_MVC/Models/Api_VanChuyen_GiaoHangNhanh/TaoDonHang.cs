using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaoDonHang
{
    public class Category
    {
        public string level1 { get; set; }
    }

    public class ItemsItem
    {
        public string name { get; set; }
        public string code { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public int length { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Category category { get; set; }
    }

    public class Root
    {
        public int payment_type_id { get; set; }
        public string note { get; set; }
        public string required_note { get; set; }
        public string return_phone { get; set; }
        public string return_address { get; set; }
        public string return_district_id { get; set; }
        public string return_ward_code { get; set; }
        public string client_order_code { get; set; }
        public string to_name { get; set; }
        public string to_phone { get; set; }
        public string to_address { get; set; }
        public string to_ward_code { get; set; }
        public int to_district_id { get; set; }
        public int cod_amount { get; set; }
        public string content { get; set; }
        public int weight { get; set; }
        public int length { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int pick_station_id { get; set; }
        public string deliver_station_id { get; set; }
        public int insurance_value { get; set; }
        public int service_id { get; set; }
        public int service_type_id { get; set; }
        public int order_value { get; set; }
        public string coupon { get; set; }
        public string pick_shift { get; set; }
        public List<ItemsItem> item { get; set; }
    }


}
