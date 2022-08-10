using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;
using Entity;
using System.Data;
using Framwork;
using FlexCel.XlsAdapter;
using FlexCel.Core;
using System.Text;
using VS.E_Commerce;

namespace VS.ECommerce_MVC.cms.Admin.Products
{
    public partial class DetailCart : System.Web.UI.UserControl
    {
        private string status = "1";
        public int i = 1;
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        private string ID = "0";
        public string DuyetDonHang = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["lang"] != null)
            {
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["lang"] = this.lang;
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }
            if (MoreAll.MoreAll.GetCookie("URole") != null)
            {
                string[] strArray = MoreAll.MoreAll.GetCookie("URole").ToString().Trim().Split(new char[] { '|' });
                if (strArray.Length > 0)
                {
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (strArray[i].ToString().Equals("9"))
                        {
                            DuyetDonHang = "1";
                        }
                    }
                }
            }
            if (!base.IsPostBack)
            {
                if (Request["ID"] != null && !Request["ID"].Equals(""))
                {
                    ID = Request["ID"];
                }
                ChitietDonhang(ID);
            }
        }
        void ChitietDonhang(string id)
        {
            List<Entity.CartDetail> table = SCartDetail.Detail_ID_Cart(id);
            this.rpcartdetail.DataSource = table;
            this.rpcartdetail.DataBind();
            List<LCart> dt1 = db.LCarts.Where(s => s.ID == int.Parse(id)).ToList();
            if (dt1.Count > 0)
            {
                ltmadonhang.Text = dt1[0].ID.ToString();
                ltname.Text = ShowtThanhVien2(dt1[0].IDThanhVien.ToString(), dt1[0].Name.ToString());
                // ltname.Text = dt1[0].Name.ToString();
                ltdienthoai.Text = dt1[0].Phone.ToString();
                ltemail.Text = dt1[0].Email.ToString();
                ltdiachi.Text = dt1[0].Address.ToString();
                lttong.Text = AllQuery.MorePro.Detail_Price(dt1[0].Money.ToString());
                ltvietchu.Text = ConvertSoRaChu(Convert.ToDouble(dt1[0].Money));

                lthinhthucgiaohang.Text = dt1[0].Phuongthucthanhtoan.ToString();
                ltthanhtoan.Text = dt1[0].Hinhthucvanchuyen.ToString();
                ltghichu.Text = dt1[0].Contents;

                hdIDGiohang.Value = dt1[0].ID.ToString();
                hdtongtien.Value = dt1[0].Money.ToString();
                hdtongtienchienluoc.Value = dt1[0].Money.ToString();
                hdIDThanhVien.Value = dt1[0].IDThanhVien.ToString();

                if (dt1[0].Status.ToString() == "0")
                {
                    btduyet.Visible = true;
                }
                else
                {
                    btduyet.Visible = false;
                }
            }
        }

        protected string ShowtThanhVien2(string id, string Name)
        {
            string str = "";
            List<Entity.Member> dt = SMember.GET_BY_ID(id);
            if (dt.Count >= 1)
            {
                str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].ID.ToString() + "\"><span style='color:red'>" + Name + "</span></a>";
                return str;
            }
            return "<b>" + Name + " /(Thành viên này đã bị xóa)</b>";
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }
        protected string Thanhtoan(string ID)
        {
            LCart abc = db.LCarts.SingleOrDefault(p => p.ID == int.Parse(ID));
            if (abc.StatusGiaoDich.ToString().Equals("1"))
            {
                return "color:#fff; padding:3px; font-weight:bold; background:#fe0505; border-radius:5px; width:250px";
            }
            return "color:#fff; padding:3px; font-weight:bold; background:#ff619c; border-radius:5px; width:250px";
        }
        protected string Soluong(string ID_Cart)
        {
            string totalvnd = "0";
            List<Entity.CartDetail> cartdetail = SCartDetail.Detail_ID_Cart(ID_Cart);
            if (cartdetail.Count > 0)
            {
                if (cartdetail.Count > 0)
                {
                    double num = 0.0;
                    for (int i = 0; i < cartdetail.Count; i++)
                    {
                        num += Convert.ToDouble(cartdetail[i].Quantity.ToString());
                    }
                    totalvnd = num.ToString();
                }
            }
            return totalvnd;
        }
        #region Hàm đổi số ra chữ
        private string Chu(string gNumber)
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
        private string Donvi(string so)
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
        private string Tach(string tach3)
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
        public string ConvertSoRaChu(double gNum)
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
        protected string Quantity(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += dt[0].Quantity.ToString();
            }
            return str.ToString();
        }
        protected string Code(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += dt[0].Code.ToString();
            }
            return str.ToString();
        }
        protected string Name(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += dt[0].Name.ToString();
            }
            return str.ToString();
        }


        // chi tiết giỏ hàng


        protected string Anh(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += dt[0].Images.ToString();
            }
            return str.ToString();
        }
        protected string Codes(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += dt[0].Code.ToString();
            }
            return str.ToString();
        }
        protected string Kho(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str = dt[0].Quantity.ToString();
            }
            return str.ToString();
        }
        protected string Kichthuoc(string id)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count > 0)
            {
                str += dt[0].Name.ToString();
            }
            return str.ToString();
        }
        protected string Mausac(string id)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count > 0)
            {
                str += "<img src=\"" + dt[0].Images.ToString() + "\" style=\"width:28px; height:28px;border:solid 1px #d7d7d7\" />";
            }
            return str.ToString();
        }

        protected void btduyet_Click(object sender, EventArgs e)
        {
            //List<Entity.Member> dt = SMember.Name_Text("select * from Members where ID=" + hdIDThanhVien.Value + "");
            //if (dt.Count > 0)
            //{
            //    SMember.Name_Text("UPDATE [CartDetail] SET MTree='" + dt[0].MTRee + "' WHERE ID_Cart =" + hdIDGiohang.Value + "");
            //    SMember.Name_Text("UPDATE [Carts] SET MTree='" + dt[0].MTRee + "' WHERE ID =" + hdIDGiohang.Value + "");

            //}

            SCarts.Name_Text("update Carts set Status=1 where ID=" + hdIDGiohang.Value + "");
            Response.Write("<script type=\"text/javascript\">alert('Duyệt đơn hàng thành công.');window.location.href='" + Request.RawUrl.ToString() + "'; </script>");
        }
    }
}