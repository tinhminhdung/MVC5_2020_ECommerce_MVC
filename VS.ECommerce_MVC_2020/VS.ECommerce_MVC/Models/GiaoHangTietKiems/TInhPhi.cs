using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class TinhPhi
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Fee
    {
        public string name { get; set; }
        public int fee { get; set; }
        public int insurance_fee { get; set; }
        public int include_vat { get; set; }
        public int cost_id { get; set; }
        public string delivery_type { get; set; }
        public string a { get; set; }
        public string dt { get; set; }
        public List<object> extFees { get; set; }
        public int ship_fee_only { get; set; }
        public string promotion_key { get; set; }
        public bool delivery { get; set; }
    }

    public class Root
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Fee fee { get; set; }
    }

}