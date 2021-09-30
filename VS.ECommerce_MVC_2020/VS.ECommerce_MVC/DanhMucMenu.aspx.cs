using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.ECommerce_MVC
{
    public partial class DanhMucMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ShowCat();
                LoadSelected();// cho vao ham update

                showAllNhom();
            }
        }
        private void ShowCat()
        {
            string Chuoi = "";
            List<Entity.Menu> list = SMenu.Name_Text("SELECT * FROM Menu WHERE capp='PR'  order by level,Orders asc");
            cblcat.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                Chuoi = "";
                for (int j = 1; j < list[i].Level.Length / 5; j++)
                {
                    Chuoi = Chuoi + "---";
                }
                cblcat.Items.Add(new ListItem(Chuoi + " " + list[i].Name + "-" + list[i].ID.ToString(), list[i].ID.ToString()));
            }
            list.Clear();
        }
        private void InsertCenter(CheckBoxList cbl)
        {
            int i = 0;
            string submn = "";
            foreach (ListItem listItem in cbl.Items)
            {
                if (listItem.Selected == true)
                {
                    if (i == 0)
                    {
                        submn += listItem.Value;
                    }
                    else
                    {
                        submn += "," + listItem.Value;
                    }
                    i++;
                }
            }
            Response.Write(submn);
        }
        // Them moi va update
        // Truoc khi update thi xoa truong icid trong bang product di xong moi cap nhat nhe
        protected void Button1_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < cblcat.Items.Count; i++)
            //{
            //    if (cblcat.Items[i].Selected)
            //    {
            //        Message.Text += cblcat.Items[i].Value + "<br />";
            //    }
            //}

            InsertCenter(cblcat);
        }

        private void LoadSelected()
        {
            if (cblcat != null)
            {
                // cach 1

                //List<Entity.Menu> lst = SMenu.Name_Text("SELECT * FROM Menu WHERE capp='PR'  and page_Home=1  order by level,Orders asc");
                //foreach (ListItem listItem in cblcat.Items)
                //{
                //    for (int j = 0; j < lst.Count; j++)
                //    {
                //        if (listItem.Value.Equals(lst[j].ID.ToString()))
                //        {
                //            listItem.Selected = lst.Count > 0;
                //        }
                //    }
                //}

                // cach 2
                string Chuoi = "792,805,794";
                string[] center = Chuoi.Split(',');
                foreach (ListItem listItem in cblcat.Items)
                {
                    foreach (string t in center)
                    {
                        if (listItem.Value.Equals(t.ToString()))
                        {
                            listItem.Selected = true;
                        }
                    }
                }


            }
        }

        // Lay ra nhom cua id 
        private void showAllNhom()
        {
            //string ketqua = "0";
            //string Chuoi = "792,805,794";
            //string[] center = Chuoi.Split(',');
            //foreach (string t in center)
            //{
            //    ketqua += "," + Commond.SubMenu(More.PR, t);
            //}
            //Response.Write(ketqua.Replace(",,", ","));


            // so sanh ID nhom can truyen requet vao
            string ketqua = "0";
            string id = "792";
            string Chuoi = "792,805,1217,888,805,1217,794,895,896";
            string[] center = Chuoi.Split(',');
            foreach (string t in center)
            {
                if (t.Equals(id))
                {
                    ketqua += "," + Commond.SubMenu(More.PR, t);
                }
            }
            Response.Write(ketqua.Replace(",,", ","));
        }
    }
}