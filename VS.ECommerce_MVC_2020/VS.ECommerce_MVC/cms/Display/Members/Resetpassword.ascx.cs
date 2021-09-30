using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Framework;
using System.Data;

namespace VS.ECommerce_MVC.cms.Display.Members
{
    public partial class Resetpassword : System.Web.UI.UserControl
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
            this.Page.Form.DefaultButton = btnresets.UniqueID;
            if (!IsPostBack)
            {
                RequiredFieldValidator8.Text = label("thanhvien4");
                RequiredFieldValidator4.Text = label("thanhvien3");
                btnresets.Text = label("thanhvien5");
                #region UpdatePanel
                this.Page.Form.Enctype = "multipart/form-data";
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnresets);
                #endregion
            }
        }
        protected void btnregisters_Click(object sender, EventArgs e)
        {
            //Fusers item = new Fusers();
            //System.Threading.Thread.Sleep(1000);
            //if (item.Detailemail(this.txtemail.Text.Trim().ToLower()).Rows.Count < 1)
            //{
            //    this.ltmsg.Text = "Email của bạn không tồn tại trong hệ thống";
            //}
            //else
            //{
            //    try
            //    {

            //        DataTable table = item.Detailemail(txtemail.Text.Trim().ToLower());
            //        if (table.Rows.Count > 0)
            //        {
            //            string newpassword = DateTime.Now.Ticks.ToString();
            //            newpassword = newpassword.Substring(newpassword.Length - 8, 7);
            //            item.users_update_updatepassword(table.Rows[0]["vuserun"].ToString(), newpassword);
            //            string info = "Thông tin tài khoản từ web : http://" + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "/" + "<br>Thông tin tài khoản của bạn!<br>Tên đăng nhập: <b>" + table.Rows[0]["vuserun"].ToString() + " </b><br>Mật khẩu mới:  <b>" + newpassword + " </b>";
            //            string title = "Cập nhật lại mật khẩu Mới!";

            //            string email = Email.email();
            //            string password = Email.password();
            //            int str6 = Convert.ToInt32(Email.port());
            //            string host = Email.host();

            //            MailUtilities.SendMail("Cập nhật lại mật khẩu Mới!", email, password, table.Rows[0]["vemail"].ToString(), host, Convert.ToInt32(str6), title, info);
            //            item.Detailemail(table.Rows[0]["vemail"].ToString());
            //        }
            //        this.MultiView1.ActiveViewIndex = 1;
            //        this.ltresult.Text = "Email x\x00e1c nhận đ\x00e3 được gởi đến t\x00e0i khoản Email của bạn. <br>Vui l\x00f2ng check Email để x\x00e1c nhận";
            //    }
            //    catch (Exception)
            //    {
            //        this.ltresult.Text = "Có lỗi xảy ra khi gửi mail";
            //    }
            //}
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}