using Framework;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VS.ECommerce_MVC;

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class DiagramTree : System.Web.UI.Page
    {
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        string IDTHanhVien = "";
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
            if (Request["IDTHanhVien"] != null && !Request["IDTHanhVien"].Equals(""))
            {
                IDTHanhVien = Request["IDTHanhVien"];
                FMember item = new FMember();
                List<Entity.Member> table = SMember.Name_Text("select * from Members where  ID=" + (IDTHanhVien.Trim().ToLower()) + " ");
                if (table.Count > 0)
                {
                    ltshow.Text = ShowDanhsachthanhvien(IDTHanhVien.ToString());
                    ltname.Text = table[0].HoVaTen + " - (Level:" + table[0].CapBac + ") - " + table[0].DienThoai;
                   // Response.Redirect("/cms/Admin/Member/DiagramTree.aspx");
                }
            }
        }

        protected string ShowDanhsachthanhvien(string MembersID)
        {
            string str = "";
            List<Entity.Member> dt = SMember.Name_Text("SELECT * FROM Members where   ID='" + MembersID + "'  and TrangThai=1 order by ID asc");
            if (dt.Count > 0)
            {
                foreach (Entity.Member item in dt)
                {
                    str += "<li><code>" + item.HoVaTen.ToString() + "(Level:" + item.CapBac + ") <br /> </code>" + SupN3(item.ID.ToString()) + "</li>";
                }
            }
            return str.ToString();
        }
        protected string SupN3(string id)
        {
            string str = "";
            List<Entity.Member> dt = SMember.Name_Text("SELECT * FROM Members  where  GioiThieu=" + id + "  and TrangThai=1 order by ID asc");
            if (dt.Count > 0)
            {
                str += "<ul>";
                foreach (Entity.Member item in dt)
                {
                    str += "<li><code>" + item.HoVaTen.ToString() + "(Level:" + item.CapBac + ") <br /> </code>" + SupN3(item.ID.ToString()) + "</li>";
                }
                str += "</ul>";
            }
            return str.ToString();
        }

    }
}