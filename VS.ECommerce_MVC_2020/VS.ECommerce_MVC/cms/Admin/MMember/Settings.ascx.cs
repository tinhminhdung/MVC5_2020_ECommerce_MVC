using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using Entity;
using MoreAll;

namespace VS.ECommerce_MVC.cms.Admin.MMember
{
    public partial class Settings : System.Web.UI.UserControl
    {
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
            this.Page.Form.DefaultButton = btnsetup.UniqueID;
            if (!base.IsPostBack)
            {
                this.binddata();
            }
        }

        private void binddata()
        {
            List<Entity.Setting> str = SSetting.GETBYALL(lang);
            ltmsg.Text = "";
            if (str.Count >= 1)
            {
                foreach (Entity.Setting its in str)
                {
                    if (its.Properties == "DaiLy1")
                    {
                        this.DaiLy1.Text = its.Value;
                    }
                    if (its.Properties == "DaiLy2")
                    {
                        this.DaiLy2.Text = its.Value;
                    }
                    if (its.Properties == "DaiLy3")
                    {
                        this.DaiLy3.Text = its.Value;
                    }
                    if (its.Properties == "DaiLy4")
                    {
                        this.DaiLy4.Text = its.Value;
                    }
                    //
                    if (its.Properties == "ChietKhau2")
                    {
                        this.ChietKhau2.Text = its.Value;
                    }
                    if (its.Properties == "ChietKhau3")
                    {
                        this.ChietKhau3.Text = its.Value;
                    }
                    if (its.Properties == "ChietKhau4")
                    {
                        this.ChietKhau4.Text = its.Value;
                    }
                    if (its.Properties == "ChietKhau5")
                    {
                        this.ChietKhau5.Text = its.Value;
                    }
                    if (its.Properties == "ChietKhau6")
                    {
                        this.ChietKhau6.Text = its.Value;
                    }

                    //

                    if (its.Properties == "Tim6F1")
                    {
                        this.Tim6F1.Text = its.Value;
                    }

                    //
                    if (its.Properties == "HHTNKD")
                    {
                        this.HHTNKD.Text = its.Value;
                    }
                    if (its.Properties == "Tim3DaiLyCap1")
                    {
                        this.Tim3DaiLyCap1.Text = its.Value;
                    }
                    //

                    if (its.Properties == "HHTPKD")
                    {
                        this.HHTPKD.Text = its.Value;
                    }
                    if (its.Properties == "Tim6DaiLyCap1")
                    {
                        this.Tim6DaiLyCap1.Text = its.Value;
                    }
                    //

                    if (its.Properties == "HHGDKD")
                    {
                        this.HHGDKD.Text = its.Value;
                    }
                    if (its.Properties == "Tim12DaiLyCap1")
                    {
                        this.Tim12DaiLyCap1.Text = its.Value;
                    }
                    //

                    if (its.Properties == "HHGDVung")
                    {
                        this.HHGDVung.Text = its.Value;
                    }
                    if (its.Properties == "Tim2GiamDocKinhDoanh")
                    {
                        this.Tim2GiamDocKinhDoanh.Text = its.Value;
                    }
                    //

                    if (its.Properties == "HHGDMien")
                    {
                        this.HHGDMien.Text = its.Value;
                    }
                    if (its.Properties == "Tim2GiamDocVung")
                    {
                        this.Tim2GiamDocVung.Text = its.Value;
                    }
                    if (its.Properties == "Tim3GiamDocKinhDoanh")
                    {
                        this.Tim3GiamDocKinhDoanh.Text = its.Value;
                    }

                    //
                    if (its.Properties == "DongHuongGiamDocVung")
                    {
                        this.DongHuongGiamDocVung.Text = its.Value;
                    }
                    if (its.Properties == "DongHuongGiamDocMien")
                    {
                        this.DongHuongGiamDocMien.Text = its.Value;
                    }


                    //

                    if (its.Properties == "SoTien200K")
                    {
                        this.SoTien200K.Text = its.Value;
                    }
                    if (its.Properties == "SoLuong")
                    {
                        this.SoLuong.Text = its.Value;
                    }

                    //
                    if (its.Properties == "TrucTiepCoDong")
                    {
                        this.TrucTiepCoDong.Text = its.Value;
                    }
                    if (its.Properties == "TaiTieuDung")
                    {
                        this.TaiTieuDung.Text = its.Value;
                    }


                    //
                    if (its.Properties == "SoLanRutTien")
                    {
                        this.SoLanRutTien.Text = its.Value;
                    }
                    if (its.Properties == "SoTienChuyenVeViChinh")
                    {
                        this.SoTienChuyenVeViChinh.Text = its.Value;
                    }
                }
            }
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }

        protected void btnsetup_Click(object sender, EventArgs e)
        {

            Entity.Setting obj = new Entity.Setting();
            if (Page.IsValid)
            {
                obj.Lang = lang;
                obj.Properties = "DaiLy1";
                obj.Value = DaiLy1.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "DaiLy2";
                obj.Value = DaiLy2.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "DaiLy3";
                obj.Value = DaiLy3.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "DaiLy4";
                obj.Value = DaiLy4.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                //
                obj.Lang = lang;
                obj.Properties = "ChietKhau2";
                obj.Value = ChietKhau2.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "ChietKhau3";
                obj.Value = ChietKhau3.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "ChietKhau4";
                obj.Value = ChietKhau4.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "ChietKhau5";
                obj.Value = ChietKhau5.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "ChietKhau6";
                obj.Value = ChietKhau6.Text.Replace(",", "");
                SSetting.UPDATE(obj);


                //
                obj.Lang = lang;
                obj.Properties = "Tim6F1";
                obj.Value = Tim6F1.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                //
                obj.Lang = lang;
                obj.Properties = "HHTNKD";
                obj.Value = HHTNKD.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "Tim3DaiLyCap1";
                obj.Value = Tim3DaiLyCap1.Text.Replace(",", "");
                SSetting.UPDATE(obj);
                //
                obj.Lang = lang;
                obj.Properties = "HHTPKD";
                obj.Value = HHTPKD.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "Tim6DaiLyCap1";
                obj.Value = Tim6DaiLyCap1.Text.Replace(",", "");
                SSetting.UPDATE(obj);
                //
                obj.Lang = lang;
                obj.Properties = "HHGDKD";
                obj.Value = HHGDKD.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "Tim12DaiLyCap1";
                obj.Value = Tim12DaiLyCap1.Text.Replace(",", "");
                SSetting.UPDATE(obj);
                //
                obj.Lang = lang;
                obj.Properties = "HHGDVung";
                obj.Value = HHGDVung.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "Tim2GiamDocKinhDoanh";
                obj.Value = Tim2GiamDocKinhDoanh.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                //
                obj.Lang = lang;
                obj.Properties = "HHGDMien";
                obj.Value = HHGDMien.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "Tim2GiamDocVung";
                obj.Value = Tim2GiamDocVung.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "Tim3GiamDocKinhDoanh";
                obj.Value = Tim3GiamDocKinhDoanh.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "DongHuongGiamDocVung";
                obj.Value = DongHuongGiamDocVung.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "DongHuongGiamDocMien";
                obj.Value = DongHuongGiamDocMien.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                //
                obj.Lang = lang;
                obj.Properties = "SoTien200K";
                obj.Value = SoTien200K.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "SoLuong";
                obj.Value = SoLuong.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                //

                obj.Lang = lang;
                obj.Properties = "TrucTiepCoDong";
                obj.Value = TrucTiepCoDong.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "TaiTieuDung";
                obj.Value = TaiTieuDung.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "SoLanRutTien";
                obj.Value = SoLanRutTien.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "SoTienChuyenVeViChinh";
                obj.Value = SoTienChuyenVeViChinh.Text.Replace(",", "");
                SSetting.UPDATE(obj);

                this.binddata();
                this.ltmsg.Text = "Thiết lập th\x00e0nh c\x00f4ng!";
            }
        }
    }
}