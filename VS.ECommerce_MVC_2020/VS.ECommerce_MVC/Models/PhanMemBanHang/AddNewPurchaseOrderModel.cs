using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class AddNewPurchaseOrderModel
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class OrderDetail
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }
        public double Qty { get; set; }
        public double f_Discount { get; set; }
        public double m_Discount { get; set; }
        public string Description { get; set; }
        public double f_Convert { get; set; }
        public string StoreId { get; set; }
    }

    public class Root
    {
        public string Id { get; set; }
        public string OrderCode { get; set; }
        public string EmployeeId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public double OrderTotal { get; set; }
        public double f_Vat { get; set; }
        public double m_Vat { get; set; }
        public double OrderTotalDiscount { get; set; }
        public double f_Discount { get; set; }
        public double m_Discount { get; set; }
        public double m_TotalMoney { get; set; }
        public string Discription { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Location_Id { get; set; }
    }

}

