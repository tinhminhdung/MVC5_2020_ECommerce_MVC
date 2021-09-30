using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using System.Data;
using Framework;
using Services;
using Entity;

namespace VS.ECommerce_MVC.cms.Display.Members
{
    public partial class Register : System.Web.UI.UserControl
    {
        private string language = Captionlanguage.Language;
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
            if (!base.IsPostBack)
            {
                new DataTable();
                this.btnregister.Text = this.label("l_register");
                this.btncancel.Text = this.label("l_redo");

            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            this.txtemail.Text = "";
            this.txtlastname.Text = "";
            this.txtusername.Text = "";
            this.txt_add.Text = "";
            this.txt_phone.Text = "";
            this.ltmsg.Text = "";
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //   // Fusers item = new Fusers();
            //    //if (item.Detailvuserun(this.txtusername.Text.Trim().ToLower()).Rows.Count > 0)
            //    //{
            //    //    this.ltmsg.Text = label("login5");
            //    //}
            //    //else if (item.Detailemail(this.txtemail.Text.Trim().ToLower()).Rows.Count > 0)
            //    //{
            //    //    this.ltmsg.Text = label("login6");
            //    //}
            //    //else if (ContainsUnicodeCharacter(this.txtusername.Text.Trim()))
            //    //{
            //    //    this.ltmsg.Text = label("login7");
            //    //}
            //    //else if (checkspace(this.txtusername.Text.Trim()))
            //    //{
            //    //    this.ltmsg.Text = label("login7");
            //    //}
            //    //else
            //    //{
            //        //string validatekey = DateTime.Now.Ticks.ToString();
            //        //Entity.users obj = new Entity.users();
            //        //obj.vuserun = txtusername.Text.Trim().ToLower();
            //        //obj.vuserpwd = this.txtpassword.Text;
            //        //obj.vfname = this.txtlastname.Text;
            //        //obj.vlname = this.txtlastname.Text;
            //        //obj.igender = int.Parse("0");
            //        //obj.dbirthday = DateTime.Now;
            //        //obj.vidcard = "0";
            //        //obj.vaddress = this.txt_add.Text;
            //        //obj.vphone = this.txt_phone.Text;
            //        //obj.vemail = this.txtemail.Text.Trim().ToLower();
            //        //obj.iregionid = int.Parse("0");
            //        //obj.vavatar = "";
            //        //obj.vavatartitle = "";
            //        //obj.dcreatedate = DateTime.Now;
            //        //obj.dlastvisited = DateTime.Now;
            //        //obj.vvalidatekey = validatekey;
            //        //obj.istatus = int.Parse("1");
            //        //obj.lang = language;
            //        //Susers.INSERT(obj);
            //        this.MultiView1.ActiveViewIndex = 1;
            //        MoreAll.MoreAll.SetCookie("Members", txtusername.Text, 5000);
            //    }
            //}
            //catch (Exception) { }
        }
		protected bool checkspace(string text)
        {
            string[] arrtxt = text.Split(' ');
            if (arrtxt.Count() > 1)
            {
                return true;
            }
            return false;
        }
        public bool ContainsUnicodeCharacter(string input)
        {
            const int MaxAnsiCode = 255;
            return input.Any(c => c > MaxAnsiCode);
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}