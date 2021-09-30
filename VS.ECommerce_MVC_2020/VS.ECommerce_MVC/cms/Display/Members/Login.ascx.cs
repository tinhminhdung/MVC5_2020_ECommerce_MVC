using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using System.Web.Security;
using System.Data;
using Framework;
using Entity;
using Services;

namespace VS.ECommerce_MVC.cms.Display.Members
{
    public partial class Login : System.Web.UI.UserControl
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
            this.Page.Form.DefaultButton = btnlogin.UniqueID;
            if (!base.IsPostBack)
            {
            }
        }
        protected void btnlogin_Click(object sender, EventArgs e)
        {
            if ((this.txt_Uname.Text.Trim().Length < 1) || (this.txt_password.Text.Trim().Length < 1))
            {
                this.ltmsg.Text = label("login1");
            }
            else
            {
                //Fusers item = new Fusers();
                //List<Entity.users> table = Susers.Name_Text("select * from users where vuserun='" + this.txt_Uname.Text.Trim().ToLower() + "' and vuserpwd='" + (this.txt_password.Text.Trim().ToLower()) + "' and istatus=1");
                //if (table.Count < 1)
                //{
                //    this.ltmsg.Text = label("login2");
                //}
                //else
                //{
               
                //    //// Chỗ cần kiểm chuyển link sang đăng ký hoặc đăng nhập 
                //    #region Ví dụ
                //    //// chỗ cần kiểm chuyển link sang đăng ký hoặc đăng nhập 

                //    //TH1:
                //    //if (MoreAll.MoreAll.GetCookies("Members") != "")
                //    //{
                //    //    Session["Url"] = "";
                //    //}
                //    //else
                //    //{
                //    //    Session["Url"] = Request.RawUrl.ToString();
                //    //    Response.Redirect("/dang-nhap.html");
                //    //}

                //    //Hoặc Request

                //    //TH2:
                //    //if (MoreAll.MoreAll.GetCookies("Members") != "")
                //    //{
                //    //    Session["ReturnUrl"] = "";
                //    //}
                //    //else
                //    //{
                //    //    string ab = Request.RawUrl.ToString();
                //    //    Response.Redirect("/dang-nhap.html?ReturnUrl=" + ab + "");
                //    //}

                //    // TH3: đặt link 
                //    //http://abc.com/dang-nhap.html?ReturnUrl=gio-hang.html 
                //    #endregion
				
                //    FormsAuthentication.SetAuthCookie(txt_Uname.Text, false);
                //    MoreAll.MoreAll.SetCookie("Members", txt_Uname.Text, 5000);
				
                //    #region ReturnUrl
                //    if (MoreAll.MoreAll.GetCookies("Members") != "" && Session["Url"].ToString() != "")
                //    {
                //        Response.Redirect(Session["Url"].ToString());
                //    }
                //    // Hoặc
                //    if (MoreAll.MoreAll.GetCookies("Members") != "" && Request["ReturnUrl"].ToString() != "" && Request["ReturnUrl"] != null)
                //    {
                //        Response.Redirect(Request["ReturnUrl"].ToString());
                //    }
                //    #endregion
                //}
            }
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}