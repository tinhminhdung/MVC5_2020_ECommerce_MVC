using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using System.Data;
using Framework;
using System.IO;
using Services;

namespace VS.ECommerce_MVC.cms.Display.Members
{
    public partial class Changinfo : System.Web.UI.Page
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
            if (!base.IsPostBack)
            {
                btnsave.Text = label("l_update");
                try
                {
                    this.loadinformation();
                    LoadInfo();
                }
                catch (Exception)
                { }
            }
        }
        private void loadinformation()
        {
            try
            {
                Member table = db.Members.SingleOrDefault(p => p.ID == int.Parse(MoreAll.MoreAll.GetCookies("Members").ToString()));
                if (table != null)
                {
                    hdid.Value = table.ID.ToString();
                    this.txtname.Text = table.HoVaTen.ToString();
                    this.txtaddress.Text = table.DiaChi.ToString();
                  //  this.txtbirthday.Text =table.NgaySinh).ToString("yyyy-MM-dd");
                    this.txtphone.Text = table.DienThoai.ToString();
                }
            }
            catch (Exception) { }
        }
        private void LoadInfo()
        {
            try
            {
                Member table = db.Members.SingleOrDefault(p => p.ID == int.Parse(MoreAll.MoreAll.GetCookies("Members").ToString()));
                if (table != null)
                {
                    if (table.AnhDaiDien.ToString().Length < 1)
                    {
                        this.ltimg.Text = "<img src='/Uploads/avatar/no_avatar.png' class=admavatarimg>";
                    }
                    else
                    {
                        this.ltimg.Text = "<img src='/Uploads/avatar/" + table.AnhDaiDien.ToString() + "' class=admavatarimg>";
                    }
                    this.hdimg.Value = table.AnhDaiDien.ToString();
                }
            }
            catch (Exception) { }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            DatalinqDataContext db = new DatalinqDataContext();
            try
            {

                if (this.flavatar.HasFile)
                {
                    if ((this.flavatar.PostedFile.ContentLength / 0x400) > 0x400)
                    {
                        this.ltmsg.Text = "Cập nhật với dung lượng 1 M";
                        return;
                    }
                    ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                    string extension = Path.GetExtension(Path.GetFileName(this.flavatar.PostedFile.FileName));
                    if (this.hdimg.Value.Length > 0)
                    {
                        try
                        {
                            File.Delete(utlitities.APPL_PHYSICAL_PATH + "/Uploads/avatar/" + this.hdimg.Value);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    string str = DateTime.Now.Ticks.ToString() + extension;
                    this.hdimg.Value = str;
                    try
                    {
                        this.flavatar.PostedFile.SaveAs(utlitities.APPL_PHYSICAL_PATH + "/Uploads/avatar/" + str);
                    }
                    catch (Exception)
                    {
                    }
                }
                if (MoreAll.MoreAll.GetCookies("Members").ToString() != null)
                {
                    //Member table = db.Member.SingleOrDefault(p => p.vMemberun == MoreAll.MoreAll.GetCookies("Members").ToString());
                    //if (table != null)
                    //{
                    //    Entity.Member obj = new Entity.Member();
                    //    obj.vMemberun = table.vMemberun.ToString();
                    //    obj.vavatar = this.hdimg.Value;
                    //    obj.vavatartitle = "";
                    //    SMember.Member_update_updateavatar(obj);
                    //}
                }
                this.ltmsg.Text = "";

                //Member data = db.Member.SingleOrDefault(p => p.iMember_id == int.Parse(hdid.Value));
                //data.iMember_id = int.Parse(hdid.Value);
                //data.vMemberun = MoreAll.MoreAll.GetCookies("Members").ToString();
                //data.vfname = this.txtname.Text;
                //data.vlname = this.txtname.Text;
                //data.igender = 0;
                //data.dbirthday = Convert.ToDateTime(this.txtbirthday.Text);
                //data.vaddress = this.txtaddress.Text;
                //data.vphone = this.txtphone.Text;
                //data.vavatartitle = "";
                //data.vemail = txtemail.Text;
                //db.SubmitChanges();

                ltmsg.Text = "Thông tin đã được thay đổi";
                LoadInfo();

            }
            catch (Exception)
            { }
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}