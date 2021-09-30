using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class DatHang
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Order
    {
        public string partner_id { get; set; }
        public string label { get; set; }
        public string area { get; set; }
        public string fee { get; set; }
        public string insurance_fee { get; set; }
        public string estimated_pick_time { get; set; }
        public string estimated_deliver_time { get; set; }
        public List<object> products { get; set; }
        public int status_id { get; set; }
    }

    public class Root
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Order order { get; set; }
    }
}
