using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using System.Data;
using Services;
using Entity;
using Framwork;
using Framework;
using WebApplication2;
using API_NganLuong;

namespace VS.ECommerce_MVC.cms.Display.Products
{
    public partial class Cart : System.Web.UI.UserControl
    {
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["language"] != null)
            {
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["language"] = this.language;
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            this.Page.Form.DefaultButton = btnorder.UniqueID;
            if (!base.IsPostBack)
            {
                if (Commond.Setting("chknganluong").Equals("0"))
                {
                    TQNganLuong.Style.Add("display", "none");
                }
                if (Commond.Setting("CheckPaypal").Equals("0"))
                {
                    rdoPayPal.Visible = false;
                    plPayPal.Visible = false;
                }
                if (Commond.Setting("CheckOnepay").Equals("0"))
                {
                    rdoOnepay.Visible = false;
                    plOnepay.Visible = false;
                }
                ltgiohang.Text = Commond.Setting("GioHang");
                this.LoadCart();
                if (Request.QueryString["success"] != null)
                {
                    var orderid = Request.QueryString["id"];
                    if (Request.QueryString["success"] == "true")
                    {
                        Panel1.Visible = true;
                        pnmessage.Visible = false;
                        LCart abc = db.LCarts.SingleOrDefault(p => p.ID == int.Parse(orderid.ToString()));
                        abc.StatusGiaoDich = 1;
                        db.SubmitChanges();
                        string strKQ = "";
                        strKQ += "<span id='lblStatus'>Bạn đã giao dịch trả phí thành công theo mã đơn hàng <strong>" + orderid.ToString() + "</strong> với số tiền là <strong>" + AllQuery.MorePro.FormatMoneyCart(abc.Money.ToString()) + "</strong></span>";
                        strKQ += "<div class='cartInfo'>Chúng tôi sẽ kiểm tra lại và giao dịch cho quý khách ngay sau khi nhận được thông tin.";
                        strKQ += "</br>Chậm nhất trong vòng 24h, quý khách sẽ nhận được thông báo chuyển sản phẩm.";
                        strKQ += "</br>Cảm ơn quý khách đã sử dụng dịch vụ của chúng tôi.</div>";
                        lblKQ.Text = strKQ;
                    }
                    Session["cart"] = null;
                    Session["Session_Size"] = null;
                    Session["Session_MauSac"] = null;
                    Session["customerInfo"] = null;
                    Session["totalPrice"] = null;
                    Session["strCartCode"] = null;
                }

                this.btnSendOrder.Text = this.label("l_produc_sder");
                this.btnorder.Text = this.label("l_produc_sder");
                this.btnEditCart.Text = this.label("l_produc_Edit_Cart");
                this._btctnew.Text = this.label("l_produc_Continue_Shopping");
                this.btnext.Text = this.label("l_produc_Continue_Shopping");
                this.btnCancelOrder.Text = this.label("l_produc_Cancel_Order");
                this.btndelete.Text = this.label("l_produc_Cancel_Order");

                //if (MoreAll.MoreAll.GetCookies("Members").ToString() != null)
                //{
                //    Fusers item = new Fusers();
                //    DataTable table = item.Detailvuserun(MoreAll.MoreAll.GetCookies("Members").ToString());
                //    if (table.Rows.Count > 0)
                //    {
                //        this.txtName.Text =  table.Rows[0]["vfname"].ToString();
                //        this.txtAddress.Text = table.Rows[0]["vaddress"].ToString();
                //        this.txtEmail.Text = table.Rows[0]["vemail"].ToString();
                //        this.txtPhone.Text = table.Rows[0]["vphone"].ToString();
                //    }
                //}

            }
        }

        protected void ItemDataBound_RP(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemIndex != -1) && (e.Item.ItemType != ListItemType.Separator))
            {
                Label lb_tt = (Label)e.Item.FindControl("lb_tt");
                lb_tt.Text = (e.Item.ItemIndex + 1).ToString();
            }
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }

        private void LoadCart()
        {
            if (Session["cart"] != null)
            {
                DataTable dtcart = (DataTable)Session["cart"];
                if (dtcart.Rows.Count > 0)
                {
                    Repeater1.DataSource = dtcart;
                    Repeater1.DataBind();
                    string inumofproducts = "";
                    string totalvnd = "";
                    if (dtcart.Rows.Count > 0)
                    {
                        double num = 0.0;
                        int num2 = 0;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                            num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                        }
                        totalvnd = num.ToString();
                        inumofproducts = num2.ToString();
                    }
                    this.lttotalvnd.Text = AllQuery.MorePro.FormatMoney_Cart_Total(totalvnd.ToString());
                    this.ltnumberofproducts.Text = inumofproducts;
                    float total = 0;
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        total += Convert.ToSingle(dtcart.Rows[i]["Money"]);
                    }

                    this.pncart.Visible = true;
                    this.pnOrder.Visible = false;
                    this.pnmessage.Visible = false;
                }
                else
                {
                    this.pnOrder.Visible = false;
                    this.pncart.Visible = false;
                    this.pnmessage.Visible = true;
                }
            }
            else
            {
                this.pnOrder.Visible = false;
                this.pncart.Visible = false;
                this.pnmessage.Visible = true;
            }
        }

        private void LoadCartOrder()
        {
            if (Session["cart"] != null)
            {
                DataTable dtcart = (DataTable)Session["cart"];
                if (dtcart.Rows.Count > 0)
                {
                    Repeater2.DataSource = dtcart;
                    Repeater2.DataBind();
                    string inumofproducts = "";
                    string totalvnd = "";
                    if (dtcart.Rows.Count > 0)
                    {
                        double num = 0.0;
                        int num2 = 0;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                            num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                        }
                        totalvnd = num.ToString();
                        inumofproducts = num2.ToString();
                    }
                    this.ltTotalOrder.Text = AllQuery.MorePro.FormatMoney_Cart_Total(totalvnd.ToString());
                    this.ltProdinCart.Text = inumofproducts;
                    float total = 0;
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        total += Convert.ToSingle(dtcart.Rows[i]["Money"]);
                    }

                    this.pncart.Visible = false;
                    this.pnmessage.Visible = false;
                    this.pnOrder.Visible = true;
                }
                else
                {
                    this.pnOrder.Visible = false;
                    this.pncart.Visible = false;
                    this.pnmessage.Visible = true;
                }
            }
            else
            {
                this.pnOrder.Visible = false;
                this.pncart.Visible = false;
                this.pnmessage.Visible = true;
            }
        }

        protected void lnkorder_Click(object sender, EventArgs e)
        {
            this.LoadCartOrder();
        }

        protected void lnkdeletecart_Click(object sender, EventArgs e)
        {
            Session["cart"] = null;
            base.Response.Redirect("/Message.html");
        }

        protected void btnSendOrder_Click(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                if (this.txtName.Text.Length < 1)
                {
                    this.lblMsg.Text = "Bạn phải nhập họ t\x00ean!!!";
                }
                else if (this.txtAddress.Text.Length < 1)
                {
                    this.lblMsg.Text = "Bạn phải nhập địa chỉ!!!";
                }
                else if (this.txtPhone.Text.Length < 1)
                {
                    this.lblMsg.Text = "Bạn phải nhập số điện thoại li\x00ean hệ!!!";
                }
                else if (this.txtEmail.Text.Length < 1)
                {
                    this.lblMsg.Text = "Bạn phải nhập địa chỉ email!!!";
                }
                else if (!ValidateUtilities.IsValidEmail(this.txtEmail.Text.Trim()))
                {
                    this.lblMsg.Text = "Địa chỉ email kh\x00f4ng hợp lệ!!!";
                }
                else
                {
                    string chuoi1 = "";
                    string chuoi2 = "";
                    if (giaohangtannoi.Checked == true)
                    {
                        chuoi1 += giaohangtannoi.Value;
                    }
                    else if (Chuyenphatnhanh.Checked == true)
                    {
                        chuoi1 += Chuyenphatnhanh.Value;
                    }
                    if (rdotructiep.Checked == true)
                    {
                        chuoi2 += "Thanh toán tại của hàng";
                    }
                    else if (rdoATM.Checked == true)
                    {
                        chuoi2 += "Thanh toán qua ATM";
                    }
                    else if (VISA.Checked == true)
                    {
                        chuoi2 += "Thanh toán VISA - Ngân lượng";
                    }
                    else if (ATM_ONLINE.Checked == true)
                    {
                        chuoi2 += "Thanh toán ATM ONLINE - Ngân lượng";
                    }
                    else if (NL.Checked == true)
                    {
                        chuoi2 += "Thanh toán Ngân lượng";
                    }
                    else if (rdoPayPal.Checked == true)
                    {
                        chuoi2 += "Thanh toán Paypal";
                    }
                    else if (rdoOnepay.Checked == true)
                    {
                        chuoi2 += "Thanh toán Onepay";
                    }
                    Session["Phuongthucvanchuyen"] = chuoi1;
                    Session["Hinhthucthanhtoan"] = chuoi2;
						try
						{
							if (!Commond.Setting("Emailden").Equals(""))
								Senmail();
						}
						catch (Exception)
						{ }
                    if (rdotructiep.Checked == true || rdoATM.Checked == true)
                    {
                        ThanhtoanGiohang();
                    }
                    //else if (rdoNganluong.Checked == true)
                    //{
                    //    if (Commond.Setting("chknganluong").Equals("1"))
                    //    {
                    //        GiohangKemtheo();
                    //        Ngan_luong();

                    //        System.Web.HttpContext.Current.Session["cart"] = null;
                    //    }
                    //}
                    else if (rdoPayPal.Checked == true)
                    {
                        if (Commond.Setting("CheckPaypal").Equals("1"))
                        {
                            GiohangKemtheo();
                            Paypal();
                            System.Web.HttpContext.Current.Session["cart"] = null;
                        }
                    }
                    else if (rdoOnepay.Checked == true)
                    {
                        if (Commond.Setting("CheckOnepay").Equals("1"))
                        {
                            GiohangKemtheo();
                            Onepay();
                            System.Web.HttpContext.Current.Session["cart"] = null;
                        }
                    }
                    else if (VISA.Checked == true)
                    {
                        if (Commond.Setting("chknganluong").Equals("1"))
                        {
                            GiohangKemtheo();
                            Ngan_luongNews("VISA");
                            System.Web.HttpContext.Current.Session["cart"] = null;
                        }
                    }
                    else if (ATM_ONLINE.Checked == true)
                    {
                        if (Commond.Setting("chknganluong").Equals("1"))
                        {
                            GiohangKemtheo();
                            Ngan_luongNews("ATM_ONLINE");
                            System.Web.HttpContext.Current.Session["cart"] = null;
                        }
                    }
                    else if (NL.Checked == true)// check trong admin xem cho phép hình thức nào dc thanh toán
                    {
                        if (Commond.Setting("chknganluong").Equals("1"))
                        {
                            GiohangKemtheo();// lưu vào giỏ hàng 
                            Ngan_luongNews("NL");// thanh toán ngân lượng
                            System.Web.HttpContext.Current.Session["cart"] = null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                this.lblMsg.Text = "Sự cố lỗi xẩy ra liên quan tới Email của hệ thống chưa chính xác, Bạn có thể liên hệ với quản trị viên websie";
            }
        }

        protected void ThanhtoanGiohang()
        {
            // 1. them gio hang
            string inumofproducts = "0";
            string totalvnd = "0";
            DataTable dtcart = new DataTable();
            dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
            if (dtcart.Rows.Count > 0)
            {
                double num = 0.0;
                int num2 = 0;
                for (int i = 0; i < dtcart.Rows.Count; i++)
                {
                    num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                    num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                }
                totalvnd = num.ToString();
                inumofproducts = num2.ToString();
            }
            // 2. them chi tiet gio hang
            Carts obj = new Carts();
            #region MyRegion
            string chuoi1 = "";
            string chuoi2 = "";
            if (giaohangtannoi.Checked == true)
            {
                chuoi1 += giaohangtannoi.Value;
            }
            else if (Chuyenphatnhanh.Checked == true)
            {
                chuoi1 += Chuyenphatnhanh.Value;
            }
            if (rdotructiep.Checked == true)
            {
                chuoi2 += "Thanh toán tại của hàng";
            }
            else if (rdoATM.Checked == true)
            {
                chuoi2 += "Thanh toán qua ATM";
            }
            //else if (rdoNganluong.Checked == true)
            //{
            //    chuoi2 += "Thanh toán Ngân lượng";
            //}
            else if (VISA.Checked == true)
            {
                chuoi2 += "Thanh toán VISA - Ngân lượng";
            }
            else if (ATM_ONLINE.Checked == true)
            {
                chuoi2 += "Thanh toán ATM ONLINE - Ngân lượng";
            }
            else if (NL.Checked == true)
            {
                chuoi2 += "Thanh toán Ngân lượng";
            }
            else if (rdoPayPal.Checked == true)
            {
                chuoi2 += "Thanh toán Paypal";
            }
            else if (rdoOnepay.Checked == true)
            {
                chuoi2 += "Thanh toán Onepay";
            }
            obj.Name = this.txtName.Text.Trim();
            obj.Address = this.txtAddress.Text.Trim();
            obj.Phone = this.txtPhone.Text.Trim();
            obj.Email = this.txtEmail.Text.Trim();
            obj.Contents = this.txtnoidung.Text.Trim();
            obj.Create_Date = Convert.ToDateTime(DateTime.Now.ToString());
            obj.Money = int.Parse(totalvnd);
            obj.lang = language;
            obj.Status = 0;
            #endregion
            int cartid = FCarts.Insert(this.txtName.Text.Trim(), this.txtAddress.Text.Trim(), this.txtPhone.Text.Trim(), this.txtEmail.Text.Trim(), this.txtnoidung.Text.Trim(), totalvnd, language, "0", chuoi1, chuoi2, "0", "0");
            #region Marketing
            Entity.Marketing ob = new Entity.Marketing();
            ob.Name = txtName.Text;
            ob.Email = txtEmail.Text;
            ob.Phone = txtPhone.Text;
            ob.Address = txtAddress.Text;
            ob.dcreatedate = DateTime.Now;
            ob.istatus = 0;
            SMarketing.INSERT(ob);
            #endregion
            Entity.CartDetail oj = new Entity.CartDetail();
            if (Session["cart"] != null)
            {
                for (int i = 0; i < dtcart.Rows.Count; i++)
                {
                    #region MyRegion
                    oj.ID_Cart = int.Parse(cartid.ToString());
                    oj.ipid = int.Parse(dtcart.Rows[i]["PID"].ToString());
                    oj.Price = Convert.ToSingle(dtcart.Rows[i]["Price"].ToString());
                    oj.Quantity = int.Parse(dtcart.Rows[i]["Quantity"].ToString());
                    oj.Money = Convert.ToSingle(dtcart.Rows[i]["Money"].ToString());
                    oj.Mausac = int.Parse(dtcart.Rows[i]["Mausac"].ToString());
                    oj.Kichco = int.Parse(dtcart.Rows[i]["Kichco"].ToString());
                    oj.IDThanhVien = 0;
                    #endregion
                    SCartDetail.CartDetail_Insert(oj);
                }
            }
            System.Web.HttpContext.Current.Session["cart"] = null;
            System.Web.HttpContext.Current.Session["Session_Size"] = null;
            System.Web.HttpContext.Current.Session["Session_MauSac"] = null;
            base.Response.Redirect("/Ordersucess.html");
        }

        protected void GiohangKemtheo()
        {
            // 1. them gio hang
            string inumofproducts = "0";
            string totalvnd = "0";
            DataTable dtcart = new DataTable();
            dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
            if (dtcart.Rows.Count > 0)
            {
                double num = 0.0;
                int num2 = 0;
                for (int i = 0; i < dtcart.Rows.Count; i++)
                {
                    num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                    num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                }
                totalvnd = num.ToString();
                inumofproducts = num2.ToString();
            }
            // 2. them chi tiet gio hang
            Carts obj = new Carts();
            #region MyRegion
            obj.Name = this.txtName.Text.Trim();
            obj.Address = this.txtAddress.Text.Trim();
            obj.Phone = this.txtPhone.Text.Trim();
            obj.Email = this.txtEmail.Text.Trim();
            obj.Contents = this.txtnoidung.Text.Trim();
            obj.Create_Date = Convert.ToDateTime(DateTime.Now.ToString());
            obj.Money = int.Parse(totalvnd);
            obj.lang = language;
            obj.Status = 0;
            #endregion

            // Hàm chuỗi 1 và chuỗi 2 này chỉ để phân biệt tên phương thức thanh toán để lưu vào DB thôi
            string chuoi1 = "";
            string chuoi2 = "";
            if (giaohangtannoi.Checked == true)
            {
                chuoi1 += giaohangtannoi.Value;
            }
            else if (Chuyenphatnhanh.Checked == true)
            {
                chuoi1 += Chuyenphatnhanh.Value;
            }
            if (rdotructiep.Checked == true)
            {
                chuoi2 += "Thanh toán tại của hàng";
            }
            else if (rdoATM.Checked == true)
            {
                chuoi2 += "Thanh toán qua ATM";
            }
            //else if (rdoNganluong.Checked == true)
            //{
            //    chuoi2 += "Thanh toán Ngân lượng";
            //}
            else if (VISA.Checked == true)
            {
                chuoi2 += "Thanh toán VISA - Ngân lượng";
            }
            else if (ATM_ONLINE.Checked == true)
            {
                chuoi2 += "Thanh toán ATM ONLINE - Ngân lượng";
            }
            else if (NL.Checked == true)
            {
                chuoi2 += "Thanh toán Ngân lượng";
            }
            else if (rdoPayPal.Checked == true)
            {
                chuoi2 += "Thanh toán Paypal";
            }
            else if (rdoOnepay.Checked == true)
            {
                chuoi2 += "Thanh toán Onepay";
            }
            int cartid = FCarts.Insert(this.txtName.Text.Trim(), this.txtAddress.Text.Trim(), this.txtPhone.Text.Trim(), this.txtEmail.Text.Trim(), this.txtnoidung.Text.Trim(), totalvnd, language, "0", chuoi1, chuoi2, "0", "0");
            #region Marketing
            Entity.Marketing ob = new Entity.Marketing();
            ob.Name = txtName.Text;
            ob.Email = txtEmail.Text;
            ob.Phone = txtPhone.Text;
            ob.Address = txtAddress.Text;
            ob.dcreatedate = DateTime.Now;
            ob.istatus = 0;
            SMarketing.INSERT(ob);
            #endregion
            Entity.CartDetail oj = new Entity.CartDetail();
            if (Session["cart"] != null)
            {
                for (int i = 0; i < dtcart.Rows.Count; i++)
                {
                    #region MyRegion
                    oj.ID_Cart = int.Parse(cartid.ToString());
                    oj.ipid = int.Parse(dtcart.Rows[i]["PID"].ToString());
                    oj.Price = Convert.ToSingle(dtcart.Rows[i]["Price"].ToString());
                    oj.Quantity = int.Parse(dtcart.Rows[i]["Quantity"].ToString());
                    oj.Money = Convert.ToSingle(dtcart.Rows[i]["Money"].ToString());
                    oj.Mausac = int.Parse(dtcart.Rows[i]["Mausac"].ToString());
                    oj.Kichco = int.Parse(dtcart.Rows[i]["Kichco"].ToString());
                    oj.IDThanhVien = 0;

                    #endregion
                    SCartDetail.CartDetail_Insert(oj);
                }
            }
            LCart customer = db.LCarts.OrderByDescending(s => s.ID).FirstOrDefault();
            Session["totalPrice"] = totalvnd.ToString();
            Session["strCartCode"] = customer.ID.ToString();
        }

        protected void Ngan_luongNews(string payment_method)
        {
            string str_bankcode = "";
            #region MyRegion
            if (VCB.Checked == true)
            {
                str_bankcode = "VCB";
            }
            else if (DAB.Checked == true)
            {
                str_bankcode = "DAB";
            }
            else if (TCB.Checked == true)
            {
                str_bankcode = "TCB";
            }
            else if (MB.Checked == true)
            {
                str_bankcode = "MB";
            }
            else if (SHB.Checked == true)
            {
                str_bankcode = "SHB";
            }
            else if (VIB.Checked == true)
            {
                str_bankcode = "VIB";
            }
            else if (ICB.Checked == true)
            {
                str_bankcode = "ICB";
            }
            else if (EXB.Checked == true)
            {
                str_bankcode = "EXB";
            }
            else if (ACB.Checked == true)
            {
                str_bankcode = "ACB";
            }
            else if (HDB.Checked == true)
            {
                str_bankcode = "HDB";
            }
            else if (MSB.Checked == true)
            {
                str_bankcode = "MSB";
            }
            else if (NVB.Checked == true)
            {
                str_bankcode = "NVB";
            }
            else if (VAB.Checked == true)
            {
                str_bankcode = "VAB";
            }
            else if (VPB.Checked == true)
            {
                str_bankcode = "VPB";
            }
            else if (SCB.Checked == true)
            {
                str_bankcode = "SCB";
            }
            else if (OJB.Checked == true)
            {
                str_bankcode = "OJB";
            }
            else if (PGB.Checked == true)
            {
                str_bankcode = "PGB";
            }
            else if (GPB.Checked == true)
            {
                str_bankcode = "GPB";
            }
            else if (AGB.Checked == true)
            {
                str_bankcode = "AGB";
            }
            else if (SGB.Checked == true)
            {
                str_bankcode = "SGB";
            }
            else if (NAB.Checked == true)
            {
                str_bankcode = "NAB";
            }
            else if (BAB.Checked == true)
            {
                str_bankcode = "BAB";
            }
            #endregion
            RequestInfo info = new RequestInfo();
            info.Merchant_id = MoreAll.Ngan_Luong.MerchantSiteCode();
            info.Merchant_password = MoreAll.Ngan_Luong.PasswordNL();
            info.Receiver_email = MoreAll.Ngan_Luong.Receive();

            info.cur_code = "vnd";
            info.bank_code = str_bankcode;// str_bankcode;

            info.Order_code = "Đơn hàng: " + Session["strCartCode"] + " đặt từ website" + Request.Url.Authority;
            info.Total_amount = Session["totalPrice"].ToString();
            info.fee_shipping = "0";
            info.Discount_amount = "0";
            info.order_description = txtnoidung.Text;
            info.return_url = "http://" + MoreAll.MoreAll.RequestUrl(Request.Url.Authority);
            info.cancel_url = "http://" + MoreAll.MoreAll.RequestUrl(Request.Url.Authority);

            info.Buyer_fullname = txtName.Text;
            info.Buyer_email = txtEmail.Text;
            info.Buyer_mobile = txtPhone.Text;

            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseInfo result = objNLChecout.GetUrlCheckout(info, payment_method);
            if (result.Error_code == "00")
            {
                Response.Redirect(result.Checkout_url);
            }
            else
                lblMsg.Text = result.Description;
        }

        protected void Ngan_luong()
        {
            // Thanh toán Nganluong
            string inumofproducts = "0";
            string totalvnd = "0";
            string Names = "";
            DataTable dtcart = new DataTable();
            dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
            if (dtcart.Rows.Count > 0)
            {
                double num = 0.0;
                int num2 = 0;
                string ten = "";
                for (int i = 0; i < dtcart.Rows.Count; i++)
                {
                    num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                    num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                    ten += Name_Product(dtcart.Rows[i]["PID"].ToString(), i);
                }
                totalvnd = num.ToString();
                inumofproducts = num2.ToString();
                Names = ten.ToString();
            }
            Double price = Convert.ToDouble(totalvnd);
            NL_Checkout nlcheckout = new NL_Checkout();
            string return_url = Request.Url.Scheme + "://" + Request.Url.Authority + "/nlSuccess.aspx";
            Int32 Soluong = Convert.ToInt32(inumofproducts);
            string transaction_info = "Đơn hàng: " + Session["strCartCode"] + " đặt từ website" + Request.Url.Authority;//Thông tin giao dịch
            string rs = nlcheckout.buildCheckoutUrlNew(return_url, transaction_info, transaction_info, price.ToString(), "vnd", Soluong, 0, 0, 0, 0, txtnoidung.Text.Trim(), "", "");
            Response.Write(rs);
            Response.Redirect(rs);
        }

        protected void Paypal()
        {
            string inumofproducts = "0";
            string totalvnd = "0";
            string Names = "";
            DataTable dtcart = new DataTable();
            dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
            if (dtcart.Rows.Count > 0)
            {
                double num = 0.0;
                int num2 = 0;
                string ten = "";
                for (int i = 0; i < dtcart.Rows.Count; i++)
                {
                    num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                    num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                    ten += Name_Product(dtcart.Rows[i]["PID"].ToString(), i);
                }
                totalvnd = num.ToString();
                inumofproducts = num2.ToString();
                Names = ten.ToString();
            }
            int dola = Convert.ToInt32(Commond.Setting("Usd"));
            Double price = Convert.ToDouble(totalvnd);
            String order_id = Names.ToString();//Mã đơn hàng
            String total_amount = String.Format("{0:#.##}", (price / dola)).Replace(",", ".");
            String no_shipping = "1"; //1 = true = không có ship
            String order_des = txtnoidung.Text;
            Paypal pp = new Paypal();
            Session["OrderPaypal"] = pp.SetOrderingValue("Payment in: " + Request.Url.Authority, order_id, total_amount, no_shipping, "1", order_des);
            Response.Redirect("/paypal_process.aspx");
        }

        protected void Onepay()
        {
            string[] customers = new string[] { txtName.Text.Trim(), txtAddress.Text.Trim(), txtPhone.Text.Trim(), txtEmail.Text.Trim(), "", txtnoidung.Text.Trim(), "" };
            Session["customerInfo"] = string.Join(";", customers);

            #region OnePay
            string cus = Session["customerInfo"].ToString();
            string SECURE_SECRET = Commond.Setting("Hashcode");
            // Khoi tao lop thu vien va gan gia tri cac tham so gui sang cong thanh toan
            string OPURL = "";
            if (Commond.Setting("CheckTest") == "1")
            {
                OPURL = "https://mtf.onepay.vn/onecomm-pay/vpc.op";
            }
            else
            {
                OPURL = "https://onepay.vn/onecomm-pay/Vpcdps.op";
            }
            VPCRequest conn = new VPCRequest(OPURL);
            conn.SetSecureSecret(SECURE_SECRET);
            // Add the Digital Order Fields for the functionality you wish to use
            // Core Transaction Fields
            conn.AddDigitalOrderField("Title", "Onepay paygate");
            conn.AddDigitalOrderField("vpc_Locale", "vn");//Chon ngon ngu hien thi tren cong thanh toan (vn/en)
            conn.AddDigitalOrderField("vpc_Version", "2");
            conn.AddDigitalOrderField("vpc_Command", "pay");
            conn.AddDigitalOrderField("vpc_Merchant", "ONEPAY");
            conn.AddDigitalOrderField("vpc_AccessCode", Commond.Setting("Accesscode"));
            conn.AddDigitalOrderField("vpc_MerchTxnRef", Session["strCartCode"].ToString());
            conn.AddDigitalOrderField("vpc_OrderInfo", Session["strCartCode"].ToString());
            conn.AddDigitalOrderField("vpc_Amount", (int.Parse(Session["totalPrice"].ToString()) * 100).ToString());
            conn.AddDigitalOrderField("vpc_Currency", "VND");
            conn.AddDigitalOrderField("vpc_ReturnURL", "http://" + Request.Url.Authority + "/OnpaySuccess.aspx");
            // Thong tin them ve khach hang. De trong neu khong co thong tin
            string[] infoCustomer = cus.Split(Convert.ToChar(";"));
            conn.AddDigitalOrderField("vpc_SHIP_Street01", "");
            conn.AddDigitalOrderField("vpc_SHIP_Provice", "");
            conn.AddDigitalOrderField("vpc_SHIP_City", "");
            conn.AddDigitalOrderField("vpc_SHIP_Country", infoCustomer[1]);
            conn.AddDigitalOrderField("vpc_Customer_Phone", infoCustomer[2]);
            conn.AddDigitalOrderField("vpc_Customer_Email", infoCustomer[3]);
            conn.AddDigitalOrderField("vpc_Customer_Id", infoCustomer[6]);
            // Dia chi IP cua khach hang
            conn.AddDigitalOrderField("vpc_TicketNo", "");
            // Chuyen huong trinh duyet sang cong thanh toan
            String url = conn.Create3PartyQueryString();
            Page.Response.Redirect(url);
            #endregion
        }

        protected void Senmail()
        {
            try
            {
                System.Text.StringBuilder strb = new System.Text.StringBuilder();
                strb.AppendLine("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"color: rgb(0, 0, 0); font-family: &quot;Times New Roman&quot;; font-size: medium; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial; border-collapse: collapse; width: 569pt;\" width=\"758\">");
                strb.AppendLine("   <tr height=\"32\" style=\"height: 24pt;\">");
                strb.AppendLine("       <td colspan=\"7\" height=\"32\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 12pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: center; height: 24pt;\"></td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"32\" style=\"height: 24pt;\">");
                strb.AppendLine("       <td colspan=\"7\" height=\"32\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 12pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: center; height: 24pt;\">BẢNG CHI TIẾT ĐƠN HÀNG (" + DateTime.Now + ")</td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"32\" style=\"height: 24pt;\">");
                strb.AppendLine("       <td colspan=\"7\" height=\"32\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 12pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: left; height: 24pt;\"><b>Website</b>: <span style=\"color:red\">" + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "</span><br /></td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"26\" style=\"height: 19.5pt;\">");
                strb.AppendLine("       <td colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt;\"><b>Khách hàng</b>: " + this.txtName.Text.Trim() + "</td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"26\" style=\"height: 19.5pt;\">");
                strb.AppendLine("       <td colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt;\"><b>Địa chỉ</b>: " + this.txtAddress.Text.Trim() + "</td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"26\" style=\"height: 19.5pt;\">");
                strb.AppendLine("       <td colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt;\"><b>Điện thoại</b>: " + this.txtPhone.Text.Trim() + "</td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"26\" style=\"height: 19.5pt;\">");
                strb.AppendLine("       <td  colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt;\"><b>Email</b>: " + this.txtEmail.Text.Trim() + "</td>");
                strb.AppendLine("   </tr>");
                try
                {
                    strb.AppendLine("    <tr height=\"26\" style=\"height: 19.5pt;\">");
                    strb.AppendLine("       <td  colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt;\"><b>Phương thức vận chuyển</b>: " + Session["Phuongthucvanchuyen"] + "</td>");
                    strb.AppendLine("   </tr>");

                    strb.AppendLine("    <tr height=\"26\" style=\"height: 19.5pt;\">");
                    strb.AppendLine("       <td  colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt;\"><b>Hình thức vận chuyển</b>: " + Session["Hinhthucthanhtoan"] + "</td>");
                    strb.AppendLine("   </tr>");
                }
                catch (Exception)
                { }
                strb.AppendLine("      <tr height=\"26\" style=\"height: 19.5pt;\">");
                strb.AppendLine("       <td  colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt;\"><b>Nội dung</b> : " + this.txtnoidung.Text.Trim() + "</td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("      <tr height=\"26\" style=\"height: 19.5pt;\">");
                strb.AppendLine("       <td  colspan=\"7\" height=\"26\" style=\"border-style: none; border-color: inherit; border-width: medium; padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: left; height: 19.5pt; color:red; font-weight:bold; text-transform:uppercase\">Thông tin đơn hàng:</td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"45\" style=\"height: 33.75pt;\">");
                strb.AppendLine("       <td height=\"45\" style=\"border-style: solid; border-color: windowtext; border-width: 1pt 0.5pt 1pt 1pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: center; height: 33.75pt;\">STT</td>");
                strb.AppendLine("       <td  style=\"border-style: solid none; border-top-color: windowtext; border-right-color: inherit; border-bottom-color: windowtext; border-left-color: inherit; border-width: 1pt medium; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: center;\">Mã SP</td>");
                strb.AppendLine("       <td style=\"border-style: solid none solid solid; border-top-color: windowtext; border-right-color: inherit; border-bottom-color: windowtext; border-left-color: windowtext; border-width: 1pt medium 1pt 0.5pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: normal; text-align: center; width: 210pt;\" width=\"280\">Tên sản phẩm</td>");
                strb.AppendLine("       <td style=\"border-style: solid; border-color: windowtext; border-width: 1pt 0.5pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: normal; text-align: center; width: 100px;\" width=\"53\">Số lượng</td>");
                strb.AppendLine("       <td style=\"border-style: solid; border-color: windowtext; border-width: 1pt 0.5pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: normal; text-align: center; width: 100px;\" width=\"50\">Đ.Vtính</td>");
                strb.AppendLine("       <td style=\"border-style: solid; border-color: windowtext; border-width: 1pt 0.5pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: normal; text-align: center; width: 100px;\" width=\"65\">Đơn giá</td>");
                strb.AppendLine("       <td style=\"border-style: solid; border-color: windowtext; border-width: 1pt 1pt 1pt 0.5pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: normal; text-align: center; width: 150pt;\">Thành tiền</td>");
                strb.AppendLine("   </tr>");
                DataTable dtcart = new DataTable();
                dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                if (Session["cart"] != null)
                {
                    if (dtcart.Rows.Count > 0)
                    {
                        int j = 1;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            strb.AppendLine("   <tr height=\"28\" style=\"height: 21pt;\">");
                            strb.AppendLine("       <td height=\"28\" style=\"border: 1px solid rgb(0, 0, 0); padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: center; height: 21pt;\">" + j++ + "</td>");
                            strb.AppendLine("       <td style=\"border: 1px solid rgb(0, 0, 0); padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: center;\">" + Name_Code(dtcart.Rows[i]["PID"].ToString(), i) + "</td>");
                            strb.AppendLine("       <td style=\"border: 1px solid rgb(0, 0, 0); padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: left;\">" + Name_Product(dtcart.Rows[i]["PID"].ToString(), i) + "</td>");
                            strb.AppendLine("       <td style=\"border: 1px solid rgb(0, 0, 0); padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: center;\">" + dtcart.Rows[i]["Quantity"].ToString() + "</td>");
                            strb.AppendLine("       <td style=\"border: 1px solid rgb(0, 0, 0); padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: center;\">VNĐ</td>");
                            strb.AppendLine("       <td  style=\"border: 1px solid rgb(0, 0, 0); padding: 0px; color: black; font-size: 10pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: normal; text-align: center; width: 49pt;\" width=\"65\">" + AllQuery.MorePro.FormatMoney_NO(dtcart.Rows[i]["Price"].ToString()) + "</td>");
                            strb.AppendLine("       <td style=\"border: 1px solid rgb(0, 0, 0); padding: 0px; color: black; font-size: 11pt; font-weight: 400; font-style: normal; text-decoration: none; font-family: Calibri, sans-serif; vertical-align: bottom; white-space: nowrap; text-align: center;\" width=\"76\">" + AllQuery.MorePro.FormatMoney_NO(dtcart.Rows[i]["Money"].ToString()) + "</td>");
                            strb.AppendLine("   </tr>");
                        }
                    }
                }
                string Soluong = "0";
                string Tongtien = "0";
                dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                if (dtcart.Rows.Count > 0)
                {
                    double num = 0.0;
                    int num2 = 0;
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                        num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                    }
                    Tongtien = num.ToString();
                    Soluong = num2.ToString();
                }
                strb.AppendLine("   <tr height=\"33\" style=\"height: 24.75pt;\">");
                strb.AppendLine("       <td height=\"33\" style=\"border-style: solid none solid solid; border-top-color: windowtext; border-right-color: inherit; border-bottom-color: windowtext; border-left-color: windowtext; border-width: 0.5pt medium 0.5pt 1pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: center; height: 24.75pt;\">&nbsp;</td>");
                strb.AppendLine("       <td  style=\"border: 1px solid rgb(0, 0, 0); font-size: 10pt; font-weight: 700; font-style: normal; text-align: center;\">Tổng cộng</td>");
                strb.AppendLine("       <td  style=\"border: 1px solid rgb(0, 0, 0); font-size: 10pt; font-weight: 700; font-style: normal; text-align: center;\">&nbsp;</td>");
                strb.AppendLine("       <td style=\"border: 0.5pt solid windowtext; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: center;\">" + Soluong + "</td>");
                strb.AppendLine("       <td colspan=\"2\" style=\"border: 0.5pt solid windowtext; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: center;\">&nbsp;</td>");
                strb.AppendLine("       <td  colspan=\"1\" style=\"border-style: solid; border-color: windowtext; border-width: 0.5pt 1px 0.5pt 0.5pt; padding: 0px; color: black; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none; font-family: &quot;Times New Roman&quot;, serif; vertical-align: middle; white-space: nowrap; text-align: center; border-image: initial;\">" + AllQuery.MorePro.FormatMoney_NO(Tongtien) + " VNĐ</td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("   <tr height=\"39\" style=\"height: 29.25pt;\">");
                strb.AppendLine("       <td  colspan=\"7\" style=\"font-size: 10pt; font-weight: 700; font-style: normal; border: 1px solid rgb(0, 0, 0);  text-align: center;color:red\">Tổng số tiền (viết bằng chữ): " + ConvertSoRaChu(int.Parse(Tongtien)) + ".</td>");
                strb.AppendLine("   </tr>");
                strb.AppendLine("</table>");

                string email = Email.email();
                string password = Email.password();
                int port = Convert.ToInt32(Email.port());
                string host = Email.host();
                MailUtilities.SendMail("Thông tin đặt hàng trên website " + Request.Url.Host + " từ: " + txtName.Text, email, password, Commond.Setting("Emailden"), host, port, "Thông tin đặt hàng trên website " + Request.Url.Host + " từ: " + txtName.Text, strb.ToString());
               // MailUtilities.SendMail("Thông tin đặt hàng trên website " + Request.Url.Host + " từ: " + txtName.Text, email, password, txtEmail.Text.Trim(), host, port, "Thông tin đặt hàng trên website " + Request.Url.Host + " từ: " + txtName.Text, strb.ToString());
            }
            catch (Exception)
            { }
        }

        public string Name_Product(string pid, int i)
        {
            string code = "";
            DataTable dt = new DataTable();
            SProducts.Detail_ID(dt, pid);
            if (dt.Rows.Count > 0)
            {
                code = dt.Rows[0]["Name"].ToString();
            }
            return code;
        }
        public string Name_Code(string pid, int i)
        {
            string code = "";
            DataTable dt = new DataTable();
            SProducts.Detail_ID(dt, pid);
            if (dt.Rows.Count > 0)
            {
                code = dt.Rows[0]["Code"].ToString();
            }
            return code;
        }
        protected void btnEditCart_Click(object sender, EventArgs e)
        {
            this.pnOrder.Visible = false;
            this.pnmessage.Visible = false;
            this.pncart.Visible = true;
        }

        protected void Delete_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('X\x00f3a sản phẩm n\x00e0y?')";
        }

        protected void Empty_Load(object sender, EventArgs e)
        {
            ((Button)sender).Attributes["onclick"] = "return confirm('Hủy bỏ giỏ h\x00e0ng?')";
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            {
                DataTable dtcart = new DataTable();
                string str = e.CommandName.ToString();
                string pid = e.CommandArgument.ToString();
                string str3 = str;
                if (str3 != null)
                {
                    if (str3 == "Giam")
                    {
                        if ((HttpContext.Current.Request.Form[pid] != null) && ValidateUtilities.IsValidInt(HttpContext.Current.Request.Form[pid].ToString().Trim()))
                        {
                            dtcart = (DataTable)HttpContext.Current.Session["cart"];
                            SessionCarts.Cart_Updatequantity(ref dtcart, pid, HttpContext.Current.Request.Form[pid].ToString().Trim());
                            HttpContext.Current.Session["cart"] = dtcart;
                        }
                    }
                    if (str3 == "delete")
                    {
                        dtcart = (DataTable)HttpContext.Current.Session["cart"];
                        SessionCarts.ShoppingCart_RemoveProduct(e.CommandArgument.ToString());
                        Response.Redirect("/gio-hang.html");
                        HttpContext.Current.Session["cart"] = dtcart;
                    }
                    if (str3 == "update")
                    {
                        if ((HttpContext.Current.Request.Form[pid] != null) && ValidateUtilities.IsValidInt(HttpContext.Current.Request.Form[pid].ToString().Trim()))
                        {
                            dtcart = (DataTable)HttpContext.Current.Session["cart"];
                            SessionCarts.Cart_Updatequantity(ref dtcart, pid, HttpContext.Current.Request.Form[pid].ToString().Trim());
                            HttpContext.Current.Session["cart"] = dtcart;
                        }
                    }
                }
                LoadCart();
            }
        }

        protected void btnCancelOrder_Click(object sender, EventArgs e)
        {
            this.btndelete_Click(sender, e);
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["cart"] = null;
            base.Response.Redirect("/Message.html");
        }

        protected void _btctnew_Click(object sender, EventArgs e)
        {
            Response.Redirect("/");
        }

        protected void btnext_Click(object sender, EventArgs e)
        {
            Response.Redirect("/");
        }

        protected string Kichthuoc(string id)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count > 0)
            {
                str += "<div class=\"Kichhuoc\"><a class=\"size active\"><span>" + dt[0].Name.ToString() + "</span><div class=\"pl\"><img src=\"/Resources/images/activee.png\" /></div></a></div>";
            }
            return str.ToString();
        }

        protected string Mausac(string id)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count > 0)
            {
                str += "<div class=\"Mausac\"><a class=\"Color active\"><img src=\"" + dt[0].Images.ToString() + "\" style=\"width:28px; height:28px;border:solid 1px #d7d7d7\" /><div class=\"pl\"><img src=\"/Resources/images/activee.png\" style=' height: 16px !important;width: 18px !important;' /></div></a></div>";
                str += "";
            }
            return str.ToString();
        }

        protected void txtxQuantity_TextChanged(object sender, EventArgs e)
        {
            TextBox Quantity = (TextBox)sender;
            var b = (HiddenField)Quantity.FindControl("hiID");
            DataTable dtcart = new DataTable();
            dtcart = (DataTable)HttpContext.Current.Session["cart"];
            SessionCarts.Cart_Updatequantity(ref dtcart, b.Value, Quantity.Text);
            HttpContext.Current.Session["cart"] = dtcart;
            LoadCart();
        }

        protected void rdotructiep_CheckedChanged(object sender, EventArgs e)
        {
            Pltructiep.Visible = true;
            platm.Visible = false;
            plOnepay.Visible = false;
            //  plNganluong.Visible = false;
            plPayPal.Visible = false;
        }

        protected void rdoATM_CheckedChanged(object sender, EventArgs e)
        {
            Pltructiep.Visible = false;
            platm.Visible = true;
            plOnepay.Visible = false;
            // plNganluong.Visible = false;
            plPayPal.Visible = false;
        }

        protected void rdoOnepay_CheckedChanged(object sender, EventArgs e)
        {
            Pltructiep.Visible = false;
            platm.Visible = false;
            plOnepay.Visible = true;
            // plNganluong.Visible = false;
            plPayPal.Visible = false;
        }
        protected void rdoNganluong_CheckedChanged(object sender, EventArgs e)
        {
            Pltructiep.Visible = false;
            platm.Visible = false;
            plOnepay.Visible = false;
            // plNganluong.Visible = true;
            plPayPal.Visible = false;
        }
        protected void rdoPayPal_CheckedChanged(object sender, EventArgs e)
        {
            Pltructiep.Visible = false;
            platm.Visible = false;
            plOnepay.Visible = false;
            // plNganluong.Visible = false;
            plPayPal.Visible = true;
        }

        protected void VISA_CheckedChanged(object sender, EventArgs e)
        {
            CVISA.Style.Add("display", "block");
            CATM_ONLINE.Style.Add("display", "none");
            CNL.Style.Add("display", "none");
        }
        protected void ATM_ONLINE_CheckedChanged(object sender, EventArgs e)
        {
            CVISA.Style.Add("display", "none");
            CATM_ONLINE.Style.Add("display", "block");
            CNL.Style.Add("display", "none");
        }

        protected void NL_CheckedChanged(object sender, EventArgs e)
        {
            CVISA.Style.Add("display", "none");
            CATM_ONLINE.Style.Add("display", "none");
            CNL.Style.Add("display", "block");
        }

        #region Hàm đổi số ra chữ
        private static string Chu(string gNumber)
        {
            string result = "";
            switch (gNumber)
            {
                case "0":
                    result = "không";
                    break;
                case "1":
                    result = "một";
                    break;
                case "2":
                    result = "hai";
                    break;
                case "3":
                    result = "ba";
                    break;
                case "4":
                    result = "bốn";
                    break;
                case "5":
                    result = "năm";
                    break;
                case "6":
                    result = "sáu";
                    break;
                case "7":
                    result = "bảy";
                    break;
                case "8":
                    result = "tám";
                    break;
                case "9":
                    result = "chín";
                    break;
            }
            return result;
        }
        private static string Donvi(string so)
        {
            string Kdonvi = "";
            if (so.Equals("1"))
                Kdonvi = "";
            if (so.Equals("2"))
                Kdonvi = "nghìn";
            if (so.Equals("3"))
                Kdonvi = "triệu";
            if (so.Equals("4"))
                Kdonvi = "tỷ";
            if (so.Equals("5"))
                Kdonvi = "nghìn tỷ";
            if (so.Equals("6"))
                Kdonvi = "triệu tỷ";
            if (so.Equals("7"))
                Kdonvi = "tỷ tỷ";

            return Kdonvi;
        }
        private static string Tach(string tach3)
        {
            string Ktach = "";
            if (tach3.Equals("000"))
                return "";
            if (tach3.Length == 3)
            {
                string tr = tach3.Trim().Substring(0, 1).ToString().Trim();
                string ch = tach3.Trim().Substring(1, 1).ToString().Trim();
                string dv = tach3.Trim().Substring(2, 1).ToString().Trim();
                if (tr.Equals("0") && ch.Equals("0"))
                    Ktach = " không trăm lẻ " + Chu(dv.ToString().Trim()) + " ";
                if (!tr.Equals("0") && ch.Equals("0") && dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm ";
                if (!tr.Equals("0") && ch.Equals("0") && !dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm lẻ " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (tr.Equals("0") && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm mười " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("0"))
                    Ktach = " không trăm mười ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("5"))
                    Ktach = " không trăm mười lăm ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười " + Chu(dv.Trim()).Trim() + " ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười lăm ";
            }
            return Ktach;
        }
        public static string ConvertSoRaChu(double gNum)
        {
            if (gNum == 0)
                return "Không đồng";
            string lso_chu = "";
            string tach_mod = "";
            string tach_conlai = "";
            double Num = Math.Round(gNum, 0);
            string gN = Convert.ToString(Num);
            int m = Convert.ToInt32(gN.Length / 3);
            int mod = gN.Length - m * 3;
            string dau = "[+]";
            // Dau [+ , - ]
            if (gNum < 0)
                dau = "[-]";
            dau = "";
            // Tach hang lon nhat
            if (mod.Equals(1))
                tach_mod = "00" + Convert.ToString(Num.ToString().Trim().Substring(0, 1)).Trim();
            if (mod.Equals(2))
                tach_mod = "0" + Convert.ToString(Num.ToString().Trim().Substring(0, 2)).Trim();
            if (mod.Equals(0))
                tach_mod = "000";
            // Tach hang con lai sau mod :
            if (Num.ToString().Length > 2)
                tach_conlai = Convert.ToString(Num.ToString().Trim().Substring(mod, Num.ToString().Length - mod)).Trim();
            ///don vi hang mod
            int im = m + 1;
            if (mod > 0)
                lso_chu = Tach(tach_mod).ToString().Trim() + " " + Donvi(im.ToString().Trim());
            /// Tach 3 trong tach_conlai
            int i = m;
            int _m = m;
            int j = 1;
            string tach3 = "";
            string tach3_ = "";
            while (i > 0)
            {
                tach3 = tach_conlai.Trim().Substring(0, 3).Trim();
                tach3_ = tach3;
                lso_chu = lso_chu.Trim() + " " + Tach(tach3.Trim()).Trim();
                m = _m + 1 - j;
                if (!tach3_.Equals("000"))
                    lso_chu = lso_chu.Trim() + " " + Donvi(m.ToString().Trim()).Trim();
                tach_conlai = tach_conlai.Trim().Substring(3, tach_conlai.Trim().Length - 3);

                i = i - 1;
                j = j + 1;
            }
            if (lso_chu.Trim().Substring(0, 1).Equals("k"))
                lso_chu = lso_chu.Trim().Substring(10, lso_chu.Trim().Length - 10).Trim();
            if (lso_chu.Trim().Substring(0, 1).Equals("l"))
                lso_chu = lso_chu.Trim().Substring(2, lso_chu.Trim().Length - 2).Trim();
            if (lso_chu.Trim().Length > 0)
                lso_chu = dau.Trim() + " " + lso_chu.Trim().Substring(0, 1).Trim().ToUpper() + lso_chu.Trim().Substring(1, lso_chu.Trim().Length - 1).Trim() + " đồng chẵn.";
            return lso_chu.ToString().Trim();
        }
        #endregion

    }
}