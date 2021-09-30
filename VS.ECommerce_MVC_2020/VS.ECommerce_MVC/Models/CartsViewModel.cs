using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VS.ECommerce_MVC.Models;

public class CartsViewModel
{
    public string Name { set; get; }
    public string Address { set; get; }
    public string Phone { set; get; }
    public string Email { set; get; }
    public string Contents { set; get; }
    //public DateTime Create_Date { set; get; }
  //  public string Money { set; get; }
}


public class CartsViewModelNews
{
    public string Name { set; get; }
    public string Address { set; get; }
    public string Phone { set; get; }
    public string Email { set; get; }
    public string Contents { set; get; }
    public List<DetailCarts> DetailCart { get; set; }// truyền nhiều dữ liệu
}

public class DetailCarts
{
    public int ipid { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public int Money { get; set; }
}
