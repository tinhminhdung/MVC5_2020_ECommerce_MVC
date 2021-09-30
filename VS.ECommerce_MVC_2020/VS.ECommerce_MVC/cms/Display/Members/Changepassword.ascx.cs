using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using System.Data;
using Framework;

namespace VS.ECommerce_MVC.cms.Display.Members
{
    public partial class Changepassword : System.Web.UI.UserControl
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
                txtcurrentpass.Focus();
                btnchangepassword.Text = this.label("lt_changepassword");
            }
        }
        protected void btnchangepassword_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (((this.txtcurrentpass.Text.Trim().Length < 1) || (this.txtnewpassword.Text.Length < 1)) || (this.txtnewpasswordconfirm.Text.Length < 1))
            //    {
            //        this.ltmsg.Text = label("matkhau1");
            //    }
            //    else if (this.txtnewpassword.Text.Trim().Length < 3)
            //    {
            //        this.ltmsg.Text =  label("matkhau2");
            //    }
            //    else if (!this.txtnewpassword.Text.Equals(this.txtnewpasswordconfirm.Text))
            //    {
            //        this.ltmsg.Text = label("matkhau2");
            //    }
            //    else
            //    {

            //        if (MoreAll.MoreAll.GetCookie("Members") != null)
            //        {
            //            Fusers item = new Fusers();
                    
            //            if (item.Getdetailbyunpwd(MoreAll.MoreAll.GetCookies("Members").ToString(), this.txtcurrentpass.Text).Rows.Count < 1)
            //            {
            //                this.ltmsg.Text = label("matkhau3");
            //            }
            //            else
            //            {
            //                item.users_update_updatepassword(MoreAll.MoreAll.GetCookies("Members").ToString(), this.txtnewpassword.Text.Trim());
            //                this.ltmsg.Text = label("matkhau4");
            //            }
            //        }
            //        else
            //        {
            //            base.Response.Redirect(MoreAll.MoreAll.RequestUrl(Request.Url.ToString()));
            //        }
            //    }
            //}
            //catch (Exception) { }
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}