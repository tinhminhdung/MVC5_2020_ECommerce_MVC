using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;
using Entity;

namespace VS.ECommerce_MVC.cms.Admin.MMember
{
    public partial class AMembers : System.Web.UI.UserControl
    {
        private string status = "-1";
        private string IDThanhVien = "0";
        DatalinqDataContext db = new DatalinqDataContext();
        private string lang = Captionlanguage.Language;
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
            if (Request["st"] != null && !Request["st"].Equals(""))
            {
                status = Request["st"];
            }
            if (!base.IsPostBack)
            {
                if (Request["st"] != null && !Request["st"].Equals(""))
                {
                    ddlstatus.SelectedValue = Request["st"];
                }
                if (Request["us"] != null && !Request["us"].Equals(""))
                {
                    ddlorderby.SelectedValue = Request["us"];
                }
                if (Request["ds"] != null && !Request["ds"].Equals(""))
                {
                    ddlordertype.SelectedValue = Request["ds"];
                }
                if (Request["kw"] != null && !Request["kw"].Equals(""))
                {
                    txtkeyword.Text = Request["kw"];
                }
                if (Request["cb"] != null && !Request["cb"].Equals(""))
                {
                    ddlcapdo.SelectedValue = Request["cb"];
                }
                if (Request["cd"] != null && !Request["cd"].Equals(""))
                {
                    ddlcodong.SelectedValue = Request["cd"];
                }
                if (Request["IDThanhVien"] != null && !Request["IDThanhVien"].Equals(""))
                {
                    IDThanhVien = Request["IDThanhVien"];
                }
                this.ddlstatus.Items.Add(new ListItem("Tất cả các mục", "-1"));
                this.ddlstatus.Items.Add(new ListItem("Kích hoạt", "1"));
                this.ddlstatus.Items.Add(new ListItem("Chưa kích hoạt", "0"));
                WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlstatus, this.status);
                this.LoadItems();
            }
        }
        protected void btndisplay_Click(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }
        protected void lnksearch_Click(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }
        protected void Delete_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn c\x00f3 muốn x\x00f3a th\x00e0nh vi\x00ean n\x00e0y?')";
        }
        protected bool EnableLock(string status)
        {
            return status.Equals("1");
        }
        protected bool EnablecUnLock(string status)
        {
            return status.Equals("1");
        }
        protected bool EnablecLock(string status)
        {
            return status.Equals("2");
        }
        protected bool EnableUnLock(string status)
        {
            return status.Equals("0");
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }
        //void LoadItems()
        //{
        //    //try
        //    //{
        //    //  string[] searchfields = new string[] { "HoVaTen", "DiaChi", "DienThoai", "Email" };
        //    //   string orderby = this.ddlorderby.SelectedValue + " " + this.ddlordertype.SelectedValue;
        //    // List<Entity.Member> dt = SMember.CATEGORY_ADMIN(orderby, this.txtkeyword.Text.Replace("&nbsp;", ""), searchfields, lang, ddlstatus.SelectedValue);
        //    string shortbydate = "";
        //    string orderby = this.ddlorderby.SelectedValue + " " + this.ddlordertype.SelectedValue;
        //    string sql = "select * from Members where lang='" + lang + "' ";
        //    if (!ddlstatus.SelectedValue.Equals("-1"))
        //    {
        //        sql += " and TrangThai=" + ddlstatus.SelectedValue;
        //    }
        //    if (txtkeyword.Text.Length > 0)
        //    {
        //        sql += " and  (dbo.fuConvertToUnsign(HoVaTen) LIKE N'" + Framwork.FCarts.SearchApproximate.Exec(Framwork.FCarts.ConvertVN.Convert(txtkeyword.Text)) + "' OR dbo.fuConvertToUnsign(DiaChi)  LIKE N'" + Framwork.FCarts.SearchApproximate.Exec(Framwork.FCarts.ConvertVN.Convert(txtkeyword.Text)) + "' OR DienThoai LIKE N'" + Framwork.FCarts.SearchApproximate.Exec(Framwork.FCarts.ConvertVN.Convert(txtkeyword.Text)) + "' OR Email LIKE N'" + Framwork.FCarts.SearchApproximate.Exec(Framwork.FCarts.ConvertVN.Convert(txtkeyword.Text)) + "') ";
        //    }
        //    if (orderby.Length < 1)
        //    {
        //        shortbydate = " order by NgayTao desc";
        //    }
        //    else
        //    {
        //        shortbydate = " order by " + orderby;
        //    }
        //    sql += shortbydate;
        //    List<Member> dt = db.ExecuteQuery<Member>(@"" + sql + "").ToList();
        //    CollectionPager1.DataSource = dt;
        //    CollectionPager1.BindToControl = Repeater1;
        //    CollectionPager1.MaxPages = 10000;
        //    CollectionPager1.PageSize = 30;
        //    Repeater1.DataSource = CollectionPager1.DataSourcePaged;
        //    Repeater1.DataBind();
        //    //}
        //    //catch (Exception)
        //    //{ }
        //}

        public void LoadItems()
        {
            string sql1 = "";
            if (!IDThanhVien.Equals("0"))
            {
                sql1 += " and ID=" + IDThanhVien + " ";
            }
            if (ddlcapdo.SelectedValue != "-1")
            {
                sql1 += " and CapBac=" + ddlcapdo.SelectedValue + " ";
            }
            if (ddlcodong.SelectedValue != "-1")
            {
                sql1 += " and TrangThaiThamGiaCoDong=" + ddlcodong.SelectedValue + " ";
            }

            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse("30");
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<Entity.Member> iitem = SMember.CATEGORY_PHANTRANG1(sql1, txtkeyword.Text.Replace("&nbsp;", ""), ddlstatus.SelectedValue);
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();
                ltSum.Text = iitem.Count().ToString();
            }
            List<Entity.Member> dt = SMember.CATEGORY_PHANTRANG2(sql1, txtkeyword.Text.Replace("&nbsp;", ""), ddlstatus.SelectedValue, (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
            if (Tongsobanghi % Tongsotrang > 0)
            {
                Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
            }
            else
            {
                Tongsobanghi = Tongsobanghi / Tongsotrang;
            }
            ltpage.Text = Commond.PhantrangAdmin("/admin.aspx?u=Thanhvien&st=" + ddlstatus.SelectedValue + "cd=" + ddlcodong.SelectedValue + "&cb=" + ddlcapdo.SelectedValue + "&us=" + ddlorderby.SelectedValue + "&ds=" + ddlordertype.SelectedValue + "&kw=" + txtkeyword.Text + "", Tongsobanghi, pages);
        }
        protected void Lock_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn c\x00f3 muốn kh\x00f3a t\x00e0i khoản n\x00e0y?')";
        }

        //-----------
        void Deletefrom()
        {
            HoVaTen.Text = "";
            DienThoai.Text = "";
            DiaChi.Text = "";
            lblmsg.Text = "";
            TenNganHang.Text = "";
            ChuTaiKhoan.Text = "";
            SoTaiKHoan.Text = "";
            Session["txtkeywords"] = "";
            ltimgs.Text = "";
            txtImage.Text = "";
            hdimgnews.Value = "";
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            hd_insertupdate.Value = "";
            Panel1.Visible = true;
            Panel2.Visible = false;
            lblmsg.Text = "";
            Deletefrom();
            LoadItems();
        }
        protected void btthemmoi_Click(object sender, EventArgs e)
        {
            Panel2.Visible = true;
            Panel1.Visible = false;
            hd_insertupdate.Value = "insert";
            hd_page_edit_id.Value = "-1";
            lblmsg.Text = "";
            Deletefrom();
        }
        protected void btn_InsertUpdate_Click(object sender, EventArgs e)
        {
            if (HoVaTen.Text.Trim().Length < 1 && DiaChi.Text.Trim().Length < 1)
            {
                lblmsg.Text = "Xin vui lòng kiểm tra dữ liệu đầu vào của bạn";
            }
            else
            {
                string str5 = hd_insertupdate.Value.Trim();
                if (str5 != null)
                {
                    if (!(str5 == "update"))
                    {
                        if (str5 == "insert")
                        {

                            List<Entity.Member> listDienThoai = SMember.Name_Text("select * from Members  where DienThoai='" + DienThoai.Text.Trim().ToLower() + "'");
                            if (MoreAll.RegularExpressions.phone(DienThoai.Text.Trim().ToLower()))
                            {
                                this.lblmsg.Text = "Điện thoại không đúng định dạng";
                                return;
                            }
                            else if (listDienThoai.Count > 0)
                            {
                                this.lblmsg.Text = "Điện thoại đã tồn tại trong hệ thống"; return;
                            }

                            //string Nguoigioithieu = "0";
                            //string VTree = "0";
                            //if (NguoiGioiThieu.Text.Trim().Length > 0)
                            //{
                            //    if (DienThoai.Text.Trim() != NguoiGioiThieu.Text.Trim())
                            //    {
                            //        List<Entity.Member> iEmail = SMember.Name_Text("select * from Members  where DienThoai='" + NguoiGioiThieu.Text.Trim().ToLower() + "'");// and DuyetTienDanap=1/  //and iuser_id !=" + MoreAll.MoreAll.GetCookies("MembersID") + " 
                            //        if (iEmail.Count > 0)
                            //        {
                            //            Nguoigioithieu = iEmail[0].ID.ToString();
                            //            VTree = iEmail[0].MTree.ToString();
                            //        }
                            //        else
                            //        {
                            //            this.lblmsg.Text = " Người giới thiệu không tồn tại trong hệ thống."; return;

                            //        }
                            //    }
                            //}
                            //String mtree = "|0|";
                            //if (Nguoigioithieu != "0")
                            //{
                            //    mtree = VTree;
                            //}
                            //String mtrees = mtree;

                            //string validatekey = DateTime.Now.Ticks.ToString();
                            //Member obj = new Member();
                            //obj.PasWord = "123456789";
                            //obj.HoVaTen = HoVaTen.Text;
                            //obj.GioiTinh = 0;
                            //obj.NgaySinh = DateTime.Now.ToString("dd/MM/yyyy");
                            //obj.DiaChi = DiaChi.Text;
                            //obj.DienThoai = DienThoai.Text;
                            //obj.Email = "";
                            //obj.AnhDaiDien = "";
                            //obj.NgayTao = DateTime.Now;
                            //obj.Key = validatekey;
                            //obj.TrangThai = 1;
                            //obj.Lang = lang;


                            //db.Members.InsertOnSubmit(obj);
                            //db.SubmitChanges();
                            //List<Entity.Member> Them = SMember.Name_Text("select top 1 * from Members order by ID desc");
                            //if (Them.Count > 0)
                            //{
                            //    string Cay = mtrees.Replace("|0|", "|") + Them[0].ID.ToString() + "|";
                            //    SMember.Name_Text("UPDATE [Members] SET MTree='" + Cay + "' WHERE ID =" + Them[0].ID.ToString() + "");
                            //}
                        }
                    }
                    else
                    {
                        //Entity.Member obj = new Entity.Member();
                        //obj.ID = int.Parse();
                        //// Member obj = db.Members.SingleOrDefault(p => p.ID == int.Parse(hd_page_edit_id.Value));
                        //obj.HoVaTen = ;
                        //// obj.NgaySinh = DateTime.Now.ToString("dd/MM/yyyy");
                        //obj.DiaChi = ;
                        //obj.DienThoai = ;
                        //obj.Email = Email.Text;

                        Member obj = db.Members.SingleOrDefault(p => p.ID == int.Parse(hd_id.Value));
                        obj.HoVaTen = HoVaTen.Text;
                        obj.GioiTinh = 1;

                        obj.DiaChi = DiaChi.Text;
                        obj.DienThoai = DienThoai.Text;
                        obj.Email = "";// collect["txtemail"];
                        obj.AnhDaiDien = txtImage.Text;

                        obj.TenNganHang = TenNganHang.Text;
                        obj.SoTaiKhoan = SoTaiKHoan.Text;
                        obj.ChuTaiKhoan = ChuTaiKhoan.Text;
                        obj.ChiNhanh = "";// collect["ChiNhanh"];


                        obj.TinhThanh = hdddlCountry.Value;
                        obj.QuanHuyen = hdddlProvince.Value;
                        obj.PhuongXa = hdddlDistrict.Value;
                        db.SubmitChanges();
                    }
                }
                LoadItems();
                Panel1.Visible = true;
                Panel2.Visible = false;
                hd_insertupdate.Value = "";
                Deletefrom();
            }

        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string str2 = e.CommandArgument.ToString().Trim();
            switch (e.CommandName)
            {
                // ko cho sửa ng giơi thieu và mre
                case "EditDetail":
                    List<Entity.Member> table = SMember.GET_BY_ID(str2);
                    if (table.Count > 0)
                    {
                        Session["txtkeywords"] = "";
                        Panel1.Visible = false;
                        Panel2.Visible = true;
                        hd_insertupdate.Value = "update";
                        hd_page_edit_id.Value = str2.Trim();
                        hd_id.Value = table[0].ID.ToString();
                        TenNganHang.Text = table[0].TenNganHang;
                        SoTaiKHoan.Text = table[0].SoTaiKhoan;
                        ChuTaiKhoan.Text = table[0].ChuTaiKhoan;
                        //WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlHinhThucHopTac, table[0].HinhThucHopTac.ToString());

                        HoVaTen.Text = table[0].HoVaTen;
                        DienThoai.Text = table[0].DienThoai;
                        DiaChi.Text = table[0].DiaChi;

                        hdddlCountry.Value = table[0].TinhThanh;
                        hdddlProvince.Value = table[0].QuanHuyen;
                        hdddlDistrict.Value = table[0].PhuongXa;

                        txtImage.Text = table[0].AnhDaiDien;
                        ltimgs.Text = MoreImage.Image(table[0].AnhDaiDien);
                        hdimgnews.Value = table[0].AnhDaiDien;


                        //List<Entity.Member> table2 = SMember.GET_BY_ID(table[0].GioiThieu.ToString());
                        //if (table2.Count > 0)
                        //{
                        //    NguoiGioiThieu.Text = table2[0].DienThoai;
                        //}
                        //WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddTinhThanhAdd, table[0].TinhThanh.ToString());
                        //GhiChu.Text = table[0].GhiChu;
                        //NguoiGioiThieu.Style.Add("pointer-events", "none");
                        //NguoiGioiThieu.Style.Add("opacity", "0.6");
                    }
                    return;

                case "delete":
                    SMember.DELETE(e.CommandArgument.ToString().Trim());
                    this.LoadItems();
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;
                case "lock":
                    this.UpdateStatus(e.CommandArgument.ToString().Trim(), "0");
                    this.LoadItems();
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;
                case "unlock":
                    this.UpdateStatus(e.CommandArgument.ToString().Trim(), "1");
                    this.LoadItems();
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;

            }
        }
        protected string Status(string status)
        {
            if (status.Equals("1"))
            {
                return "Đang hoạt động";
            }
            return "Đ\x00e3 kh\x00f3a";
        }
        protected void ddlordertype_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }
        protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }
        protected string Enablechon(string chon)
        {
            if (chon.Equals("1"))
            {
                return "Thành viên";
            }
            return "Nội bộ";
        }
        private void UpdateStatus(string un, string status)
        {
            SMember.UPDATE_STATUS(un, status);
        }
        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }
        protected void LoadRequest()
        {
            Response.Redirect("admin.aspx?u=Thanhvien&st=" + ddlstatus.SelectedValue + "&cd=" + ddlcodong.SelectedValue + "&cb=" + ddlcapdo.SelectedValue + "&us=" + ddlorderby.SelectedValue + "&ds=" + ddlordertype.SelectedValue + "&kw=" + txtkeyword.Text + "");
        }
        protected void btxoa_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)Repeater1.Items[i].FindControl("chkid");
                    HiddenField id = (HiddenField)Repeater1.Items[i].FindControl("hiID");
                    if (chk.Checked)
                    {
                        SMember.DELETE(id.Value);
                    }
                }
                LoadItems();
            }
            catch (Exception)
            {

            }
        }
        protected void ddlcapdo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }

        protected void ddlcodong_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }

    }
}