using Common;
using Facebook;
using MoreAll;
using QRCoder;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VS.ECommerce_MVC.Controllers
{
    // kế thừ từ BaseController nhé
    //public class MembersController : Controller
    public class MembersController : BaseController
    {
        private string language = Captionlanguage.SetLanguage();
        DatalinqDataContext db = new DatalinqDataContext();
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        #region Dang_Ky
        public ActionResult Dang_Ky(string info)
        {
            if (info != "")// trường hợp có coupon và không có link ?aff
            {
                List<Entity.Member> iEmail = SMember.Name_Text("select * from Members  where DienThoai='" + info + "' ");//and TrangThaiMuaHang=1 and iuser_id !=" + MoreAll.MoreAll.GetCookies("MembersID") + " 
                if (iEmail.Count > 0)
                {
                    ViewBag.txtnguoigioithieu = iEmail[0].DienThoai.ToString();
                    ViewBag.ltgoithieu = "Người giới thiệu: " + iEmail[0].HoVaTen.ToString();
                    ViewBag.style = "<style>.nguoigioithieu{pointer-events: none; opacity: 0.6;}</style>";
                    //  txtnguoigioithieu.Style.Add("pointer-events", "none");
                    // txtnguoigioithieu.Style.Add("opacity", "0.6");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Dang_Ky(string txtnguoigioithieu, string txtHoTen, string txtphone, string txtaddress, string txtmatkhau)
        {
            if (ModelState.IsValid)
            {
                //List<Entity.Member> list = SMember.Name_Text("select * from Members  where Email='" + txtemail.Trim().ToLower() + "'");
                List<Entity.Member> listDienThoai = SMember.Name_Text("select * from Members  where DienThoai='" + txtphone.Trim().ToLower() + "'");
                if (MoreAll.RegularExpressions.phone(txtphone))
                {
                    @Session["err"] = "Điện thoại không đúng định dạng";
                    return View();
                }
                else if (listDienThoai.Count > 0)
                {
                    @Session["err"] = "Điện thoại đã tồn tại trong hệ thống";
                    return View();
                }
                //else if (list.Count > 0)
                //{
                //    @Session["err"] = "Email đã tồn tại trong hệ thống";
                //    return View();
                //}
                if (txtnguoigioithieu == "")
                {
                    @Session["err"] = "Vui lòng nhập Mã người giới thiệu.";
                    return View();
                }
                else
                {
                    //System.Threading.Thread.Sleep(1000);
                    //#region Senmail
                    //if (!Commond.Setting("Emailden").Equals(""))
                    //    Senmail(txtHoTen, txtaddress, txtphone, txtemail, txttieude, txtcontent);
                    //#endregion

                    string Nguoigioithieu = "0";
                    string VTree = "0";
                    if (txtnguoigioithieu.Length > 0)
                    {
                        if (txtphone.Trim() != txtnguoigioithieu.Trim())
                        {
                            List<Entity.Member> iEmail = SMember.Name_Text("select * from Members  where DienThoai='" + txtnguoigioithieu.Trim().ToLower() + "'  ");// and DuyetTienDanap=1/  //and iuser_id !=" + MoreAll.MoreAll.GetCookies("MembersID") + " 
                            if (iEmail.Count > 0)
                            {
                                Nguoigioithieu = iEmail[0].ID.ToString();
                                VTree = iEmail[0].MTRee.ToString();
                            }
                            else
                            {
                                @Session["err"] = " Người giới thiệu không tồn tại hoặc chưa được kích hoạt trong hệ thống.";
                                return View();
                            }
                        }
                    }
                    String mtree = "|0|";
                    if (Nguoigioithieu != "0")
                    {
                        mtree = VTree;
                    }
                    String mtrees = mtree;

                    string validatekey = DateTime.Now.Ticks.ToString();
                    Member obj = new Member();
                    obj.PasWord = txtmatkhau;
                    obj.HoVaTen = txtHoTen;
                    obj.GioiTinh = 0;
                    obj.NgaySinh = DateTime.Now.ToString("dd/MM/yyyy");
                    obj.DiaChi = txtaddress;
                    obj.DienThoai = txtphone;
                    obj.Email = "";// txtemail;
                    obj.AnhDaiDien = "";
                    obj.NgayTao = DateTime.Now;
                    obj.Key = validatekey;
                    obj.TrangThai = 1;
                    obj.Lang = language;
                    obj.ViBaMuoiPT = "0";
                    obj.ViBayMuoiPT = "0";
                    obj.TongTienDaRut = "0";
                    obj.TongTienCongLai = "0";
                    obj.CapBac = 0;
                    obj.TongViTriCuaA = 0;
                    obj.ViTriF1 = 0;
                    obj.GioiThieu = int.Parse(Nguoigioithieu);
                    if (Nguoigioithieu == "0")
                    {
                        obj.MTRee = "|0|";
                    }
                    else
                    {
                        obj.MTRee = mtrees.Replace("|0|", "|");
                    }
                    obj.TrangThaiRutTien = 0;
                    obj.TrangThaiMuaHang = 0;

                    obj.TrangThaiRuTienBangKhong = 0;
                    obj.TongSoLanRutTienBangKhong = 0;

                    obj.TenNganHang = "";
                    obj.SoTaiKhoan = "";
                    obj.ChuTaiKhoan = "";
                    obj.ChiNhanh = "";

                    obj.TinhThanh = "0";
                    obj.QuanHuyen = "0";
                    obj.PhuongXa = "0";
                    obj.TrangThaiThamGiaCoDong = 0;
                    obj.SauThangKhiThamGiaGoiCoDong = 0;
                    obj.TongSoLanMuaHang = 0;
                    db.Members.InsertOnSubmit(obj);
                    db.SubmitChanges();

                    List<Entity.Member> Them = SMember.Name_Text("select top 1 * from Members order by ID desc");
                    if (Them.Count > 0)
                    {
                        string Cay = mtrees.Replace("|0|", "|") + Them[0].ID.ToString() + "|";
                        SMember.Name_Text("UPDATE [Members] SET MTRee='" + Cay + "' WHERE ID =" + Them[0].ID.ToString() + "");
                        MoreAll.MoreAll.SetCookie("Members", Them[0].DienThoai.Trim().ToLower(), 5000);
                        MoreAll.MoreAll.SetCookie("MembersUser", Them[0].HoVaTen.ToString(), 5000);
                        MoreAll.MoreAll.SetCookie("DienThoai", Them[0].DienThoai.ToString(), 5000);
                        MoreAll.MoreAll.SetCookie("MembersID", Them[0].ID.ToString(), 5000);
                        return Redirect("/wallet.html");
                    }

                    @Session["err"] = "Đăng ký tài khoản thành công";
                }
            }
            return View();
        }

        #endregion

        #region Dang_Nhap

        public ActionResult Dang_Nhap(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //Ajax
        [HttpPost]
        public ActionResult Dang_Nhap(string txttendangnhap, string txtmatkhau, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if ((txttendangnhap.Trim().Length < 1) || (txtmatkhau.Trim().Length < 1))
                {
                    ViewBag.ThongBao = "Vui lòng điền thông tin đăng nhập";
                }
                else
                {
                    if (RegularExpressions.IsValidEmail(txttendangnhap.Trim().ToLower()))
                    {
                        List<Entity.Member> table = SMember.Name_Text("select * from Members where Email='" + txttendangnhap.Trim().ToLower() + "' and PasWord='" + (txtmatkhau.Trim().ToLower()) + "' and TrangThai=1");//
                        if (table.Count < 1)
                        {
                            ViewBag.ThongBao = "Tài khoản không đúng hoặc chưa được kích hoạt";
                        }
                        else
                        {
                            MoreAll.MoreAll.SetCookie("Members", txttendangnhap.Trim().ToLower(), 5000);
                            MoreAll.MoreAll.SetCookie("MembersUser", table[0].HoVaTen.ToString(), 5000);
                            MoreAll.MoreAll.SetCookie("DienThoai", table[0].DienThoai.ToString(), 5000);
                            MoreAll.MoreAll.SetCookie("MembersID", table[0].ID.ToString(), 5000);
                            // return Redirect("/");
                            if (Url.IsLocalUrl(returnUrl))
                            {
                                ViewBag.ReturnUrl = returnUrl;
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return Redirect("/wallet.html");
                                // return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                    else if (!RegularExpressions.phone(txttendangnhap.Trim().ToLower()))
                    {
                        List<Entity.Member> table = SMember.Name_Text("select * from Members where DienThoai='" + txttendangnhap.Trim().ToLower() + "' and PasWord='" + (txtmatkhau.Trim().ToLower()) + "' and TrangThai=1");// and TrangThai=1
                        if (table.Count < 1)
                        {
                            ViewBag.ThongBao = "Tài khoản không đúng hoặc chưa được kích hoạt";
                        }
                        else
                        {

                            MoreAll.MoreAll.SetCookie("Members", txttendangnhap.Trim().ToLower(), 5000);
                            MoreAll.MoreAll.SetCookie("MembersUser", table[0].HoVaTen.ToString(), 5000);
                            MoreAll.MoreAll.SetCookie("DienThoai", table[0].DienThoai.ToString(), 5000);
                            MoreAll.MoreAll.SetCookie("MembersID", table[0].ID.ToString(), 5000);

                            // return Redirect("/");
                            if (Url.IsLocalUrl(returnUrl))
                            {
                                ViewBag.ReturnUrl = returnUrl;
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return Redirect("/wallet.html");
                                // return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                }
            }
            return View();
        }

        #endregion

        #region XinChao Thoat
        public ActionResult XinChao()
        {
            if (MoreAll.MoreAll.GetCookies("Members").ToString() != "")
            {
                Member dt = db.Members.SingleOrDefault(p => p.ID == int.Parse(MoreAll.MoreAll.GetCookies("Members").ToString()));
                if (dt == null)
                {
                    return HttpNotFound();
                }
                else if (dt != null)
                {
                    return View(dt);
                }
            }
            return PartialView();
        }

        [HttpGet]
        public ActionResult Thoat()
        {
            MoreAll.MoreAll.SetCookie("Members", "", -1);
            MoreAll.MoreAll.SetCookie("MembersID", "", -1);
            Response.Redirect("/");
            return View();
        }

        #endregion

        #region HoSoThanhVien
        [HttpGet]
        public ActionResult HoSoThanhVien()
        {
            // load lại thế này sẽ ko bị mất giá trị trong from text
            ShowLoad();
            ShowLoad_GioiTinh();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult HoSoThanhVien(FormCollection collect, string CMTAT)
        {
            //List<Entity.Member> list = SMember.Name_Text("select * from Members  where Email='" + collect["txtemail"].Trim().ToLower() + "' and ID!=" + MoreAll.MoreAll.GetCookies("MembersID").ToString() + "");
            List<Entity.Member> listDienThoai = SMember.Name_Text("select * from Members  where DienThoai='" + collect["txtphone"].Trim().ToLower() + "' and ID!=" + MoreAll.MoreAll.GetCookies("MembersID").ToString() + "");
            ServerInfoUtlitities utlitities = new ServerInfoUtlitities();

            ViewBag.Phone = collect["txtphone"];
            ViewBag.Name = collect["txtHoTen"];
            ViewBag.NgaySinh = collect["txtngaysinh"];
            //  ViewBag.Email = collect["txtemail"];
            ViewBag.Address = collect["txtaddress"];

            ViewBag.TenNganHang = collect["TenNganHang"];
            ViewBag.SoTaiKhoan = collect["SoTaiKhoan"];
            ViewBag.ChuTaiKhoan = collect["ChuTaiKhoan"];
            ViewBag.TinhThanh = collect["ddlCountry"];
            ViewBag.QuanHuyen = collect["ddlProvince"];
            ViewBag.PhuongXa = collect["ddlDistrict"];
            ViewBag.AnhDaiDiens = collect["CMTAT"];
            ViewBag.Avata = collect["CMTAT"];

            if (ModelState.IsValid)
            {
                if (collect["txtHoTen"] == "")
                {
                    ShowLoad_GioiTinh();
                    SetAlert("Vui lòng điền đầy đủ họ và tên", "warning");
                }
                else if (collect["txtphone"] == "")
                {
                    ShowLoad_GioiTinh();
                    SetAlert("Vui lòng điền điện thoại", "warning");
                }
                else if (MoreAll.RegularExpressions.phone(collect["txtphone"]))
                {
                    ShowLoad_GioiTinh();
                    SetAlert("Điện thoại không đúng định dạng", "warning");
                }
                else if (listDienThoai.Count > 0)
                {
                    ShowLoad_GioiTinh();
                    SetAlert("Điện thoại đã tồn tại trong hệ thống", "warning");

                }
                else if (collect["txtaddress"] == "")
                {
                    ShowLoad_GioiTinh();
                    SetAlert("Vui lòng điền địa chỉ", "warning");

                }
                //else if (!MoreAll.RegularExpressions.IsValidEmail(collect["txtemail"]))
                //{
                //    SetAlert("Email không đúng định dạng", "warning");

                //}
                //else if (collect["txtemail"] == "")
                //{
                //    SetAlert("Vui lòng điền email", "warning");
                //}
                //else if (list.Count > 0)
                //{
                //    SetAlert("Email đã tồn tại trong hệ thống", "warning");

                //}
                else
                {
                    Member obj = db.Members.SingleOrDefault(p => p.ID == int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString()));
                    obj.HoVaTen = collect["txtHoTen"];
                    obj.GioiTinh = 1;
                    obj.NgaySinh = collect["txtngaysinh"];
                    obj.DiaChi = collect["txtaddress"];
                    obj.DienThoai = collect["txtphone"];
                    obj.Email = "";// collect["txtemail"];
                    obj.AnhDaiDien = CMTAT;
                    obj.Lang = language;

                    obj.TenNganHang = collect["TenNganHang"];
                    obj.SoTaiKhoan = collect["SoTaiKhoan"];
                    obj.ChuTaiKhoan = collect["ChuTaiKhoan"];
                    obj.ChiNhanh = "";// collect["ChiNhanh"];

                    obj.TinhThanh = collect["ddlCountry"];
                    obj.QuanHuyen = collect["ddlProvince"];
                    obj.PhuongXa = collect["ddlDistrict"];

                    db.SubmitChanges();
                    SetAlert("Cập nhật tài khoản thành công", "success");
                    // ViewBag.ThongBao = "Cập nhật tài khoản thành công";
                    // load lại thế này sẽ ko bị mất giá trị trong from text
                    ShowLoad();
                }
            }
            // ModelState.AddModelError("", "Mật khẩu không đúng.");
            //return RedirectToAction("HoSoThanhVien");
            return View();
        }
        public void ShowLoad()
        {
            if (MoreAll.MoreAll.GetCookies("MembersID").ToString() != "")
            {
                Member dt = db.Members.SingleOrDefault(p => p.ID == int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString()));
                if (dt != null)
                {
                    ViewBag.Name = dt.HoVaTen;
                    ViewBag.Address = dt.DiaChi;
                    //  ViewBag.Email = dt.Email;
                    ViewBag.Phone = dt.DienThoai;
                    ViewBag.NgaySinh = dt.NgaySinh;


                    ViewBag.TenNganHang = dt.TenNganHang;
                    ViewBag.SoTaiKhoan = dt.SoTaiKhoan;
                    ViewBag.ChuTaiKhoan = dt.ChuTaiKhoan;
                    // ViewBag.ChiNhanh = dt.ChiNhanh;

                    ViewBag.TinhThanh = dt.TinhThanh;
                    ViewBag.QuanHuyen = dt.QuanHuyen;
                    ViewBag.PhuongXa = dt.PhuongXa;
                    // ViewBag.GioiTinh = dt.GioiTinh;

                    if (dt.AnhDaiDien.Length > 0)
                    {
                        ViewBag.AnhDaiDiens = dt.AnhDaiDien;
                        ViewBag.Avata = dt.AnhDaiDien;
                    }
                }
            }
        }
        public void ShowLoad_GioiTinh()
        {
            if (MoreAll.MoreAll.GetCookies("MembersID").ToString() != "")
            {
                Member dt = db.Members.SingleOrDefault(p => p.ID == int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString()));
                if (dt != null)
                {
                    ViewBag.GioiTinh = dt.GioiTinh;
                }
            }
        }
        #endregion

        #region DoiMatKhau
        public ActionResult DoiMatKhau()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DoiMatKhau(FormCollection collect)
        {
            if (ModelState.IsValid)
            {
                if (((collect["txtmatkhaumoicu"] == "") || (collect["txtmatkhaumoi"] == "")) || (collect["txtmatkhauxacnhan"] == ""))
                {
                    ViewBag.ThongBao = AlertMessage(Commond.label("matkhau1"), "warning");
                }
                else if (collect["txtmatkhaumoi"].Length < 3)
                {
                    ViewBag.ThongBao = AlertMessage(Commond.label("matkhau2"), "warning");
                }
                else if (!collect["txtmatkhaumoi"].Equals(collect["txtmatkhauxacnhan"]))
                {
                    ViewBag.ThongBao = AlertMessage(Commond.label("matkhau2"), "warning");
                }
                else
                {
                    if (MoreAll.MoreAll.GetCookie("Members") != null)
                    {
                        Member itel = db.Members.SingleOrDefault(p => p.ID == int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString()) && p.PasWord == collect["txtmatkhaumoicu"].Trim());
                        if (itel == null)
                        {
                            ViewBag.ThongBao = AlertMessage(Commond.label("matkhau3"), "warning");
                        }
                        else
                        {
                            Member abc = db.Members.SingleOrDefault(p => p.ID == int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString()));
                            abc.PasWord = collect["txtmatkhaumoi"].Trim();
                            db.SubmitChanges();
                            ViewBag.ThongBao = AlertMessage(Commond.label("matkhau4"), "success");
                        }
                    }
                    else
                    {
                        base.Response.Redirect(MoreAll.MoreAll.RequestUrl(Request.Url.ToString()));
                    }
                }
            }
            return View();
        }
        #endregion

        #region QuenMatKhau
        public ActionResult QuenMatKhau()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult QuenMatKhau(FormCollection collect)
        {
            if (ModelState.IsValid)
            {
                if (collect["txtemail"] == "")
                {
                    SetAlert("Vui lòng nhập địa chỉ Email", "warning");
                }
                else if (collect["txtemail"].Length < 3)
                {
                    SetAlert("Vui lòng nhập địa chỉ Email", "warning");
                }
                else
                {
                    Member itel = db.Members.SingleOrDefault(p => p.Email == collect["txtemail"].Trim());
                    if (itel == null)
                    {
                        SetAlert("Email của bạn không tồn tại trong hệ thống.", "warning");
                    }
                    else
                    {
                        string newpassword = DateTime.Now.Ticks.ToString();
                        newpassword = newpassword.Substring(newpassword.Length - 8, 7);
                        Member abc = db.Members.SingleOrDefault(p => p.ID == int.Parse(itel.ID.ToString()));
                        abc.PasWord = newpassword;
                        db.SubmitChanges();

                        string info = "Thông tin tài khoản từ web : http://" + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "/Dang-nhap.html" + "<br>Thông tin tài khoản của bạn!<br>Tên đăng nhập: <b>" + itel.Email + " </b><br>Mật khẩu mới:  <b>" + newpassword + " </b>";
                        string title = "Cập nhật lại mật khẩu Mới!";

                        string email = Email.email();
                        string password = Email.password();
                        int str6 = Convert.ToInt32(Email.port());
                        string host = Email.host();

                        MailUtilities.SendMail("Cập nhật lại mật khẩu Mới!", email, password, itel.Email.ToString(), host, Convert.ToInt32(str6), title, info);

                        SetAlert("Email x\x00e1c nhận đ\x00e3 được gởi đến t\x00e0i khoản Email của bạn. <br>Vui l\x00f2ng check Email để x\x00e1c nhận.", "success");

                    }
                }
            }
            return View();
        }
        #endregion

        #region LichSuMuaHang
        public ActionResult LichSuMuaHang(int page = 1, int pageSize = 20)
        {
            // Phân trang bằng MVC
            if (MoreAll.MoreAll.GetCookies("MembersID").ToString() != null)
            {
                var model = SQLCart.ListLichSu(page, pageSize, MoreAll.MoreAll.GetCookies("MembersID").ToString());// truy vấn sang class khác cho gọn
                return View(model);
            }
            else
            {
                Response.Redirect("/dang-nhap.html");
            }
            return View();
        }

        public ActionResult ChiTietDonHang(int ID)
        {
            if (MoreAll.MoreAll.GetCookies("MembersID").ToString() != "")
            {
                List<LCart> dt = db.LCarts.Where(x => x.IDThanhVien == int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString()) && x.ID == ID).ToList();
                if (dt != null)
                {
                    ViewBag.ltmadonhang = dt[0].ID.ToString();
                    ViewBag.ltngaydathang = dt[0].Create_Date.ToString();
                    ViewBag.lttrangthai = AllQuery.MorePro.ShowTrangThai(dt[0].Status.ToString());
                    ViewBag.lthovaten = dt[0].Name.ToString();
                    ViewBag.ltdiachi = dt[0].Address.ToString();
                    ViewBag.ltdienthoai = dt[0].Phone.ToString();
                    ViewBag.lttongtien = AllQuery.MorePro.Detail_Price(dt[0].Money.ToString());
                    ViewBag.lttongtienbangchu = MoreAll.Hamdoisorachu.So_chu(Convert.ToDouble(dt[0].Money.ToString()));

                    List<Entity.CartDetail> model = SCartDetail.Detail_ID_Cart(dt[0].ID.ToString());
                    if (model.Count > 0)
                    {
                        ViewBag.Chitiet = model;
                    }
                }
            }
            else
            {
                Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
            }
            return View();
        }
        #endregion

        #region ViDiem
        public ActionResult ViDiem(string TuNgay, string DenNgay)
        {
            if (MoreAll.MoreAll.GetCookies("MembersID").ToString() == "")
            {
                return Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
            }
            if (MoreAll.MoreAll.GetCookies("MembersID") != "")
            {
                try
                {
                    if (TuNgay == null && DenNgay == null)
                    {
                        ViewBag.TuNgay = FFDate2(GetFirstDayOfMonth());
                        ViewBag.DenNgay = FFDate2(GetLastDayOfMonth());
                        ThongKeThanhVienFitter(FFDate2(GetFirstDayOfMonth()), FFDate2(GetLastDayOfMonth()));
                        ViewBag.Doanhsothang = FFDateThang(GetFirstDayOfMonth());
                    }
                    else
                    {
                        ThongKeThanhVienFitter(TuNgay, DenNgay);
                        ViewBag.Doanhsothang = "Từ ngày " + FFDateThang(TuNgay) + " Đến ngày " + FFDateThang(DenNgay);
                    }
                }
                catch (Exception)
                { }

                ShowInfo();
            }
            return View();
        }
        public static string FFDate(object date)
        {
            return (Convert.ToDateTime(date).ToString("MM/dd/yyyy"));
        }
        public static string FFDate2(object date)
        {
            return (Convert.ToDateTime(date).ToString("yyyy-MM-dd"));
        }

        public static string FFDateThang(object date)
        {
            return (Convert.ToDateTime(date).ToString("MM"));
        }
        public static DateTime GetFirstDayOfMonth()//Ngày đầu tiên
        {
            DateTime dtResult = DateTime.Now;
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        public static DateTime GetLastDayOfMonth()// ngày cuối cùng
        {
            DateTime dtResult = DateTime.Now;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
        public void ThongKeThanhVienFitter(string TuNgay, string DenNgay)
        {
            if (MoreAll.MoreAll.GetCookies("MembersID") != "" || MoreAll.MoreAll.GetCookies("MembersID") != null)
            {
                List<Entity.Member> table = SMember.Name_Text("select * from Members where  ID =" + MoreAll.MoreAll.GetCookies("MembersID").ToString() + " ");
                if (table.Count > 0)
                {
                    string TuNgays = "";
                    string DenNgays = "";
                    try
                    {
                        TuNgays = FFDate(TuNgay);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        DenNgays = FFDate(DenNgay);
                    }
                    catch (Exception)
                    {
                    }


                    try
                    {
                        ViewBag.TuNgay = FFDate2(TuNgay);
                        ViewBag.DenNgay = FFDate2(DenNgay);

                        var tablett = db.Show_Type_HoaHong_TongHoaHong_TongDoanhSo_Thang(MoreAll.MoreAll.GetCookies("MembersID").ToString(),TuNgays.ToString(), DenNgays.ToString()).ToList();
                        if (tablett.Count > 0)
                        {
                            ViewBag.SumF1 = AllQuery.MorePro.Detail_Price(tablett[0].Money.ToString());
                        }
                        else
                        {
                            ViewBag.SumF1 = "0";
                        }
                       
                    }
                    catch (Exception)
                    { }

                }
            }
            else
            {
                Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
            }
        }

        private void ShowInfo()
        {
            string ssl = "http://";
            if (Commond.Setting("SSL").Equals("1"))
            {
                ssl = "https://";
            }
            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                Member table = db.Members.SingleOrDefault(p => p.ID == int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString()));
                if (table != null)
                {

                    try
                    {
                        string url = ssl + Request.Url.Host + "/dang-ky.html?info=" + table.DienThoai.ToString() + "";
                        ViewBag.txtlinkgioithieu = url;
                        string code = url;
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                        imgBarCode.Height = 350;
                        imgBarCode.Width = 350;
                        string anh = "";
                        using (Bitmap bitMap = qrCode.GetGraphic(20))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                byte[] byteImage = ms.ToArray();
                                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                                anh = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                            }
                        }
                        ViewBag.QRCode = anh.ToString();

                    }
                    catch (Exception)
                    { }

                    try
                    {
                      //  ChiaHoaHong.CapNhat_CapBac_DaiLy(table.ID.ToString());
                       // ChiaHoaHong.CapNhat_CapBac(table.ID.ToString());
                    }
                    catch (Exception)
                    {
                    }

                    if (table.ViBaMuoiPT.ToString() == "0")
                    {
                        ViewBag.ViBaMuoiPT = "0";
                    }
                    else
                    {
                        ViewBag.ViBaMuoiPT = AllQuery.MorePro.Detail_Price(table.ViBaMuoiPT.ToString());
                    }
                    if (table.ViBayMuoiPT.ToString() == "0")
                    {
                        ViewBag.ViBayMuoiPT = "0";
                    }
                    else
                    {
                        ViewBag.ViBayMuoiPT = AllQuery.MorePro.Detail_Price(table.ViBayMuoiPT.ToString());
                    }

                    if (table.TongTienDaRut.ToString() == "0")
                    {
                        ViewBag.TongTienDaRut = "0";
                    }
                    else
                    {
                        ViewBag.TongTienDaRut = AllQuery.MorePro.Detail_Price(table.TongTienDaRut.ToString());
                    }

                    try
                    {

                        var tablett = db.Show_Type_HoaHong_TongHoaHong(table.ID.ToString()).ToList();
                        if (tablett.Count > 0)
                        {
                            ViewBag.TongDoanhSoTrongNhom = AllQuery.MorePro.Detail_Price(tablett[0].Money.ToString());
                        }
                        else
                        {
                            ViewBag.TongDoanhSoTrongNhom = "0";
                        }

                    }
                    catch (Exception)
                    { }


                    ViewBag.CapBac = Commond.ShowCapbac(table.CapBac.ToString());
                    ViewBag.lttongthanhvien = Commond.SumThanhVienCapDuoiF1(table.ID.ToString());
                    ViewBag.TongTVToanHThong = Commond.SumTongTVHeThong(table.ID.ToString());
                    ViewBag.linkgioithieu = ssl + Request.Url.Host + "/dang-ky.html?info=" + table.DienThoai.ToString() + "";
                }
            }
        }
        #endregion

        #region MHoaHong
        public ActionResult MHoaHong()
        {
            string Category = "0";
            string TuNgay = "";
            string DenNgay = "";
            string loc = "";
            if ((Request.QueryString["Category"] != null) && (Request.QueryString["Category"] != ""))
            {
                Category = Request.QueryString["Category"].Trim();
                loc += "&Category=" + Category + "";
            }
            if ((Request.QueryString["TuNgay"] != null) && (Request.QueryString["TuNgay"] != ""))
            {
                TuNgay = Request.QueryString["TuNgay"].Trim();
                loc += "&TuNgay=" + TuNgay + "";
            }
            if ((Request.QueryString["DenNgay"] != null) && (Request.QueryString["DenNgay"] != ""))
            {
                DenNgay = Request.QueryString["DenNgay"].Trim();
                loc += "&DenNgay=" + DenNgay + "";
            }

            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                //DateTime fDate;
                //DateTime tDate;
                string sql = "";

                //if (Commond.Check(TuNgay))
                //    fDate = Commond.ConvertStringToDate(TuNgay, "dd/MM/yyyy");
                //if (Commond.Check(DenNgay))
                //    tDate = Commond.ConvertStringToDate(DenNgay, "dd/MM/yyyy");


                if (TuNgay != "" && DenNgay != "")
                {
                    sql += " AND (CAST([NgayTao] AS DATE) >= (CAST('" + Commond.FormatDate(TuNgay) + "' AS DATE) )) AND (CAST([NgayTao] AS DATE) <= CAST('" + Commond.FormatDate(DenNgay) + "' AS DATE))   ";
                }
                else if (TuNgay == "" && DenNgay != "")
                {
                    sql += " (CAST([NgayTao] AS DATE) <= CAST('" + Commond.FormatDate(DenNgay) + "' AS DATE))   ";
                }
                else if (TuNgay != "" && DenNgay == "")
                {
                    sql += " AND (CAST([NgayTao] AS DATE) >= (CAST('" + Commond.FormatDate(TuNgay) + "' AS DATE) )) ";
                }

                if (Category != "0")
                {
                    sql += " and KieuHoaHong ='" + Category + "'";
                }
                sql += " and IDThanhVienHuong =" + MoreAll.MoreAll.GetCookies("MembersID") + " ";
                int Tongsobanghi = 0;
                Int16 pages = 1;
                int Tongsotrang = int.Parse("20");
                if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
                {
                    pages = Convert.ToInt16(Request.QueryString["page"].Trim());
                }
                List<HoaHong> iitem = db.ExecuteQuery<HoaHong>(@"SELECT * FROM HoaHong where 1=1 " + sql + " order by NgayTao desc").ToList();
                if (iitem.Count() > 0)
                {
                    double Tien = 0.0;
                    for (int i = 0; i < iitem.Count; i++)
                    {
                        Tien += Convert.ToDouble(iitem[i].SoTienDuocHuong.ToString());
                    }
                    ViewBag.ltCoin = AllQuery.MorePro.Detail_Price(Tien.ToString());
                    Tongsobanghi = iitem.Count();
                }
                int PageIndex = (pages - 1);
                int Tongpage = Tongsotrang;
                int StartRecord = PageIndex * Tongpage;
                int EndRecord = StartRecord + Tongpage + 1;
                List<HoaHong> dt = db.ExecuteQuery<HoaHong>(@"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY NgayTao DESC) AS rowindex ,*  FROM  HoaHong  where 1=1 " + sql + " ) AS A WHERE  ( A.rowindex >  " + StartRecord.ToString() + " AND A.rowindex < " + EndRecord + ")").ToList();
                if (dt.Count >= 1)
                {


                    ViewBag.Show = dt;
                }

                if (Tongsobanghi % Tongsotrang > 0)
                {
                    Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
                }
                else
                {
                    Tongsobanghi = Tongsobanghi / Tongsotrang;
                }
                ViewBag.Phantrang = Commond.Phantrang_loc("/lich-su-hoa-hong.html", "&Category=" + Category + "&TuNgay=" + TuNgay + "&DenNgay=" + DenNgay + "", Tongsobanghi, pages);

            }
            else
            {
                Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
            }
            ViewBag.Categorys = Category;
            ViewBag.TuNgays = TuNgay;
            ViewBag.DenNgays = DenNgay;
            return View();
        }
        #endregion

        #region ThanhToan
        public ActionResult ThanhToan()
        {
            if (MoreAll.MoreAll.GetCookies("MembersID").ToString() == "")
            {
                return Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
            }
            if (MoreAll.MoreAll.GetCookies("MembersID") != "")
            {
                ShowThanhToan();
            }
            return View();
        }

        private void ShowThanhToan()
        {
            if (MoreAll.MoreAll.GetCookies("MembersID") != "")
            {
                List<Entity.Member> table = SMember.Name_Text("select * from Members where ID=" + MoreAll.MoreAll.GetCookies("MembersID") + "");
                if (table != null)
                {
                    if (table[0].TenNganHang.ToString().Length < 1 || table[0].ChuTaiKhoan.ToString().Length < 1 || table[0].ToString().Length < 1)
                    {
                        ViewBag.ltmsg = ("<script type=\"text/javascript\">alert('Vui lòng cập nhật hồ sơ đầy đủ phần số tài khoản trước khi rút tiền');window.location.href='/ho-so-thanh-vien.html'; </script>");
                    }
                    ViewBag.TenNganHang = table[0].TenNganHang;
                    ViewBag.ChuTaiKhoan = table[0].ChuTaiKhoan;
                    ViewBag.SoTaiKhoan = table[0].SoTaiKhoan;
                    // ViewBag.ChiNhanh = table[0].ChiNhanh;
                    ViewBag.ViBayMuoiPT = AllQuery.MorePro.Detail_Price(table[0].ViBayMuoiPT);

                    if (table[0].ViBayMuoiPT.ToString() != "0")
                    {
                        ViewBag.lttongtien = AllQuery.MorePro.Detail_Price(table[0].ViBayMuoiPT);
                    }
                    else
                    {
                        ViewBag.lttongtien = "0";
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult ThanhToan(string txtsotiencanrut, string txttennganhang, string txthovaten, string txtsotaikhoan, string txtnoidungchuyentien)
        {
            if (MoreAll.MoreAll.GetCookies("MembersID").ToString() == "")
            {
                return Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
            }
            if (MoreAll.MoreAll.GetCookies("MembersID") != "")
            {
                #region Trừ tiền trong ví
                Member iitem = db.Members.SingleOrDefault(p => p.ID == int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString()));
                if (iitem != null)
                {
                    double ConglaiCoin = 0;
                    double TongSoCoinDaCo = Convert.ToDouble(iitem.ViBayMuoiPT);
                    double TongTienCanRutCoin = Convert.ToDouble(txtsotiencanrut.Replace(",", "").Replace(".", ""));

                    #region Chỉ được rút tiền khi điểm lớn hơn hoặc = 200 điểm
                    if (txtsotiencanrut == "" || txttennganhang == "" || txthovaten == "" || txtsotaikhoan == "")
                    {
                        ViewBag.ltmsg = "<div class=\"ruttienthongbaos\">Vui lòng điền đầy đủ thông tin</div>";
                        ShowThanhToan();
                        return View();
                    }
                    // if (TongSoCoinDaCo <= 200000)
                    //if (TongTienCanRutCoin > 10000000)
                    //{
                    //    ViewBag.ltmsg = "<div class=\"ruttienthongbaos\">Số tiền tối đa chỉ được 10,000,000 VNĐ trên 1 lần rút</div>";
                    //    ShowThanhToan();
                    //    return View();
                    //}
                    #endregion
                    //if (iitem.F1 < Convert.ToInt32(Commond.Setting("trxtduF1")))
                    //{
                    //    ViewBag.ltmsg = ("<script type=\"text/javascript\">alert('Bạn phải đủ giới thiệu ra " + Commond.Setting("trxtduF1") + " F1 thì mới rút được điểm.');window.location.href='/wallet.html'; </script>");
                    //    return View();
                    //}
                    //if (iitem.TrangThaiMuaHangDatTongTien == 0)
                    //{
                    //    ViewBag.ltmsg = ("<script type=\"text/javascript\">alert('Tài khoản của bạn phải có đơn hàng có giá trị : " + Commond.Setting("txtgiatridonhang") + " triệu trở lên thì mới rút được tiền.');window.location.href='/rut-tien.html'; </script>");
                    //    return View();
                    //}

                    // double QuYVNDRaCoin = (TongTienCanRutCoin) / 1000;

                    if (TongSoCoinDaCo == TongTienCanRutCoin)
                    {
                        SMember.Name_Text("update Members set TrangThaiRuTienBangKhong=1,TongSoLanRutTienBangKhong=TongSoLanRutTienBangKhong+1  where ID=" + int.Parse(MoreAll.MoreAll.GetCookies("MembersID")) + "");// sét trạng thai mua hàng =1 khi mua hàng lần đầu sau khi rút tiền 
                    }
                    if (TongSoCoinDaCo >= TongTienCanRutCoin)
                    {
                        if (TongSoCoinDaCo.ToString() != "0")// Nếu trong ví có lớn hơn 0 đồng thì cộng tiếp
                        {

                            double TongTien = Convert.ToDouble(iitem.ViBayMuoiPT);
                            double TongRuts = Convert.ToDouble(TongTienCanRutCoin);
                            double ConglaiTT = 0;
                            ConglaiTT = ((TongTien) - (TongRuts));

                            LichSuRutTien obj = new LichSuRutTien();
                            obj.IDThanhVien = int.Parse(MoreAll.MoreAll.GetCookies("MembersID"));
                            obj.TongTienTrongVi = ConglaiTT.ToString();// iitem.TongTienCongLai;
                            obj.SoTienCanRut = txtsotiencanrut;
                            obj.SoCoin = ConglaiCoin.ToString();
                            obj.TenNganHang = txttennganhang;
                            obj.HoVaTen = txthovaten;
                            obj.SoTaiKHoan = txtsotaikhoan;
                            obj.ChiNhanh = "";// txtchinhanh;
                            obj.NoiDungChuyenTien = txtnoidungchuyentien;
                            obj.GhiChu = "";
                            obj.TrangThai = 0;
                            obj.NgayTao = DateTime.Now;
                            obj.NgayDuyet = "";
                            obj.NguoiDuyet = "";
                            db.LichSuRutTiens.InsertOnSubmit(obj);
                            db.SubmitChanges();

                            if (TongSoCoinDaCo.ToString() != "0")// Nếu trong ví có lớn hơn 0 đồng thì cộng tiếp
                            {
                                try
                                {
                                    LichSuRutTien tbn = db.LichSuRutTiens.Where(s => s.IDThanhVien == int.Parse(MoreAll.MoreAll.GetCookies("MembersID"))).OrderByDescending(s => s.ID).FirstOrDefault();
                                    string proid = tbn.ID.ToString();
                                    ChiaHoaHong.Save_LichSuViGiaoDich(MoreAll.MoreAll.GetCookies("MembersID").ToString(), "RutTien", "Rút tiền", TongTienCanRutCoin.ToString(), proid);
                                }
                                catch (Exception)
                                { }
                                CongTienDaRut(MoreAll.MoreAll.GetCookies("MembersID"), TongTienCanRutCoin.ToString(), ConglaiCoin.ToString());
                            }

                            ViewBag.ltmsg += "<div class=\"thongbaos\">Bạn đã gửi yêu cầu rút tiền thành công.</div> ";
                            ViewBag.ltmsg += "<div class=\"thongbaos\">Số tiền đã rút :" + txtsotiencanrut + " điểm</div>";
                            ViewBag.ThongBao += ("<script type=\"text/javascript\">alert('Bạn đã gửi yêu cầu rút tiền thành công.(giao dịch sẽ được thực hiện trong 12-24h)');window.location.href='/wallet.html'; </script>");

                            // Sent Mail
                            string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/SentMail/RutTien.html"));
                            content = content.Replace("{{CustomerName}}", txthovaten);
                            content = content.Replace("{{Phone}}", iitem.DienThoai);
                            content = content.Replace("{{Email}}", iitem.Email);
                            content = content.Replace("{{Address}}", iitem.DiaChi);
                            content = content.Replace("{{Total}}", txtsotiencanrut);

                            content = content.Replace("{{Tennganhang}}", txttennganhang);
                            content = content.Replace("{{Sotaikhoan}}", txtsotaikhoan);
                            // content = content.Replace("{{Chinhanh}}", txtchinhanh);
                            content = content.Replace("{{Noidung}}", txtnoidungchuyentien);
                            // content = content.Replace("{{Ghichu}}", txtghichu.Text);

                            //emailnhanthongbaorutien
                            try
                            {
                                var EmailContTy = Commond.Setting("Emailden");
                                // var emailnhanthongbaorutien = Commond.Setting("emailnhanthongbaorutien");
                                if (!EmailContTy.Equals(""))
                                    new MailHelper().SendMail(EmailContTy, "Thành viên " + txthovaten + " rút tiền trên hệ thống", content);
                                //if (!emailnhanthongbaorutien.Equals(""))
                                // new MailHelper().SendMail(emailnhanthongbaorutien, "Thành viên " + txthovaten.Text + " rút tiền trên hệ thống V-Paris", content);
                            }
                            catch (Exception)
                            { }

                            // ShowInfo();
                        }
                    }
                    else
                    {
                        ViewBag.ltmsg = "Số tiền không đủ để thanh toán ";
                        return View();
                    }
                }
                #endregion
            }
            ShowThanhToan();
            return View();
        }
        void CongTienDaRut(string IDThanhVien, string TongRut, string CongTien)
        {
            #region Cộng điểm theo hoa hồng
            List<Entity.Member> iitem = SMember.Name_Text("select * from Members where ID=" + IDThanhVien.ToString() + "");
            if (iitem != null)
            {
                double TongTien = Convert.ToDouble(iitem[0].ViBayMuoiPT);
                double TongRuts = Convert.ToDouble(TongRut);
                double ConglaiTT = 0;
                ConglaiTT = ((TongTien) - (TongRuts));
                SMember.Name_Text("update Members set ViBayMuoiPT=" + ConglaiTT.ToString() + "  where ID=" + iitem[0].ID.ToString() + "");


                double TongSoCoinDaCo = Convert.ToDouble(iitem[0].TongTienDaRut);

                double Conglai = 0;
                Conglai = ((TongSoCoinDaCo) + (TongRuts));
                SMember.Name_Text("update Members set TongTienDaRut=" + Conglai.ToString() + "  where ID=" + iitem[0].ID.ToString() + "");
            }
            #endregion
        }

        #endregion

        #region MLichSuThanhToan
        public ActionResult MLichSuThanhToan()
        {
            if (MoreAll.MoreAll.GetCookies("MembersID").ToString() == "")
            {
                return Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
            }
            if (MoreAll.MoreAll.GetCookies("MembersID") != "")
            {
                string TuNgay = "";
                string DenNgay = "";
                string loc = "";
                if ((Request.QueryString["TuNgay"] != null) && (Request.QueryString["TuNgay"] != ""))
                {
                    TuNgay = Request.QueryString["TuNgay"].Trim();
                    loc += "&TuNgay=" + TuNgay + "";
                }
                if ((Request.QueryString["DenNgay"] != null) && (Request.QueryString["DenNgay"] != ""))
                {
                    DenNgay = Request.QueryString["DenNgay"].Trim();
                    loc += "&DenNgay=" + DenNgay + "";
                }
                string sql = "";

                if (TuNgay != "" && DenNgay != "")
                {
                    sql += " AND (CAST([NgayTao] AS DATE) >= (CAST('" + Commond.FormatDate(TuNgay) + "' AS DATE) )) AND (CAST([NgayTao] AS DATE) <= CAST('" + Commond.FormatDate(DenNgay) + "' AS DATE))   ";
                }
                else if (TuNgay == "" && DenNgay != "")
                {
                    sql += " (CAST([NgayTao] AS DATE) <= CAST('" + Commond.FormatDate(DenNgay) + "' AS DATE))   ";
                }
                else if (TuNgay != "" && DenNgay == "")
                {
                    sql += " AND (CAST([NgayTao] AS DATE) >= (CAST('" + Commond.FormatDate(TuNgay) + "' AS DATE) )) ";
                }
                sql += " and IDThanhVien =" + MoreAll.MoreAll.GetCookies("MembersID") + " ";

                int Tongsobanghi = 0;
                Int16 pages = 1;
                int Tongsotrang = int.Parse("20");
                if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
                {
                    pages = Convert.ToInt16(Request.QueryString["page"].Trim());
                }
                List<LichSuRutTien> iitem = db.ExecuteQuery<LichSuRutTien>(@"SELECT * FROM LichSuRutTien where 1=1 " + sql + " order by NgayTao desc").ToList();
                if (iitem.Count() > 0)
                {
                    double Tien = 0.0;
                    for (int i = 0; i < iitem.Count; i++)
                    {
                        Tien += Convert.ToDouble(iitem[i].SoTienCanRut.ToString());
                    }
                    ViewBag.ltCoin = AllQuery.MorePro.Detail_Price(Tien.ToString());

                    Tongsobanghi = iitem.Count();
                }
                int PageIndex = (pages - 1);
                int Tongpage = Tongsotrang;
                int StartRecord = PageIndex * Tongpage;
                int EndRecord = StartRecord + Tongpage + 1;
                List<LichSuRutTien> dt = db.ExecuteQuery<LichSuRutTien>(@"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY NgayTao DESC) AS rowindex ,*  FROM  LichSuRutTien  where 1=1 " + sql + " ) AS A WHERE  ( A.rowindex >  " + StartRecord.ToString() + " AND A.rowindex < " + EndRecord + ")").ToList();
                if (dt.Count >= 1)
                {


                    ViewBag.Show = dt;
                }

                if (Tongsobanghi % Tongsotrang > 0)
                {
                    Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
                }
                else
                {
                    Tongsobanghi = Tongsobanghi / Tongsotrang;
                }
                ViewBag.Phantrang = Commond.Phantrang_loc("/lich-su-thanh-toan.html", "&TuNgay=" + TuNgay + "&DenNgay=" + DenNgay + "", Tongsobanghi, pages);
                ViewBag.TuNgays = TuNgay;
                ViewBag.DenNgays = DenNgay;
            }
            return View();
        }
        #endregion

        #region MThanhVien
        public ActionResult MThanhVien()
        {
            DatalinqDataContext db = new DatalinqDataContext();
            string ID = MoreAll.MoreAll.GetCookies("MembersID").ToString();
            if (MoreAll.MoreAll.GetCookies("MembersID").ToString() == "")
            {
                return Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
            }
            if (MoreAll.MoreAll.GetCookies("MembersID") != "")
            {
                if (Request["ID"] != null && !Request["ID"].Equals(""))
                {
                    ID = Request["ID"];
                }
                List<Entity.Member> dt = SMember.Name_Text("SELECT * FROM Members  WHERE ID = " + ID + " ");
                if (dt.Count > 0)
                {
                    string Mtr = "|" + dt[0].MTRee.ToString();
                    if (Mtr.Contains("|" + MoreAll.MoreAll.GetCookies("MembersID").ToString() + "|"))
                    {
                        ViewBag.ltphanmuc = LoadNav(int.Parse(ID.ToString()));
                        LoadItems();
                    }
                    else
                    {
                        ViewBag.ThongBao = "Bạn ko có quyền xem ID này. Vì ID này không nằm trong hệ thống của bạn";
                        //WebMsgBox.Show("Bạn ko có quyền xem ID này. Vì ID này không nằm trong hệ thống của bạn");
                    }
                }
            }
            return View();
        }
        public void LoadItems()
        {
            string Loc = "";
            string ID = MoreAll.MoreAll.GetCookies("MembersID").ToString();
            if (Request["ID"] != null && !Request["ID"].Equals(""))
            {
                ID = Request["ID"];
                Loc += "&ID=" + ID;
            }
            if (MoreAll.MoreAll.GetCookies("MembersID") != "")
            {
                int Tongsobanghi = 0;
                Int16 pages = 1;
                int Tongsotrang = int.Parse("10");
                if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
                {
                    pages = Convert.ToInt16(Request.QueryString["page"].Trim());
                }
                List<Member> iitem = db.ExecuteQuery<Member>(@"SELECT * FROM Members where 1=1 and GioiThieu=" + ID + " order by ID desc").ToList();
                if (iitem.Count() > 0)
                {
                    Tongsobanghi = iitem.Count();
                }
                int PageIndex = (pages - 1);
                int Tongpage = Tongsotrang;
                int StartRecord = PageIndex * Tongpage;
                int EndRecord = StartRecord + Tongpage + 1;
                List<Member> dt = db.ExecuteQuery<Member>(@"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY ID DESC) AS rowindex ,*  FROM  Members  where 1=1 and GioiThieu=" + ID + ") AS A WHERE  ( A.rowindex >  " + StartRecord.ToString() + " AND A.rowindex < " + EndRecord + ")").ToList();
                {
                    ViewBag.Show = dt;
                }
                if (Tongsobanghi % Tongsotrang > 0)
                {
                    Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
                }
                else
                {
                    Tongsobanghi = Tongsobanghi / Tongsotrang;
                }
                ViewBag.ltpage = Commond.PhantrangAdmin("/danh-sach-thanh-vien.html?ID=" + ID + "", Tongsobanghi, pages);
            }
        }
        private string LoadNav(int ID)
        {
            string nav = "";
            try
            {
                var item = db.Members.FirstOrDefault(s => s.ID == ID);
                if (item != null)
                {
                    nav = "<span> <i class=\"fa fa-angle-right\"></i> </span> <a href=\"/Danh-sach-thanh-vien.html?ID=" + item.ID + "\">" + item.HoVaTen + "</a>" + nav;
                    if (item.GioiThieu >= int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString()))
                    {
                        LoadNav(Convert.ToInt32(item.GioiThieu));
                    }
                }
            }
            catch (Exception)
            { }
            return nav;
        }
        #endregion

        #region LinkGioiThieu
        public ActionResult LinkGioiThieu()
        {
            if (MoreAll.MoreAll.GetCookies("MembersID").ToString() == "")
            {
                return Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
            }
            string ssl = "http://";
            if (Commond.Setting("SSL").Equals("1"))
            {
                ssl = "https://";
            }
            if (MoreAll.MoreAll.GetCookies("MembersID") != "")
            {
                Member table = db.Members.SingleOrDefault(p => p.ID == int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString()));
                if (table != null)
                {
                    //if (table.KichHoatThanhVien == 0)
                    //{
                    //    Response.Write("<script type=\"text/javascript\">alert('Bạn không thể sử dụng tính năng này. Yêu cầu kích hoạt tài khoản thành viên.');window.location.href='/vi-tien.html'; </script>");
                    //}
                    ViewBag.txtlinkgioithieu = ssl + Request.Url.Host + "/dang-ky.html?info=" + table.ID.ToString() + "";
                    ViewBag.ltshare = "<div style='margin-left: 6px;' class=\"zalo-share-button\" data-href=\"https://" + Request.Url.Host + "?info=" + table.DienThoai.ToString() + "\"  data-oaid=\"3853758560685742933\" data-layout=\"1\" data-color=\"blue\" data-customize=false></div>";
                }
            }
            return View();
        }
        #endregion

        #region MLSViTien
        public ActionResult MLSViTien()
        {
            if (MoreAll.MoreAll.GetCookies("MembersID").ToString() == "")
            {
                return Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
            }
            if (MoreAll.MoreAll.GetCookies("Members").ToString() != null && MoreAll.MoreAll.GetCookies("Members").ToString().Length > 0)
            {
            }
            else
            {
                Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
            }

            string Category = "0";
            string TuNgay = "";
            string DenNgay = "";
            string loc = "";
            if ((Request.QueryString["Category"] != null) && (Request.QueryString["Category"] != ""))
            {
                Category = Request.QueryString["Category"].Trim();
                loc += "&Category=" + Category + "";
            }
            if ((Request.QueryString["TuNgay"] != null) && (Request.QueryString["TuNgay"] != ""))
            {
                TuNgay = Request.QueryString["TuNgay"].Trim();
                loc += "&TuNgay=" + TuNgay + "";
            }
            if ((Request.QueryString["DenNgay"] != null) && (Request.QueryString["DenNgay"] != ""))
            {
                DenNgay = Request.QueryString["DenNgay"].Trim();
                loc += "&DenNgay=" + DenNgay + "";
            }

            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                string sql = "";

                if (TuNgay != "" && DenNgay != "")
                {
                    sql += " AND (CAST([NgayTao] AS DATE) >= (CAST('" + Commond.FormatDate(TuNgay) + "' AS DATE) )) AND (CAST([NgayTao] AS DATE) <= CAST('" + Commond.FormatDate(DenNgay) + "' AS DATE))   ";
                }
                else if (TuNgay == "" && DenNgay != "")
                {
                    sql += " (CAST([NgayTao] AS DATE) <= CAST('" + Commond.FormatDate(DenNgay) + "' AS DATE))   ";
                }
                else if (TuNgay != "" && DenNgay == "")
                {
                    sql += " AND (CAST([NgayTao] AS DATE) >= (CAST('" + Commond.FormatDate(TuNgay) + "' AS DATE) )) ";
                }

                if (Category != "0")
                {
                    sql += " and TypeKieuVi ='" + Category + "'";
                }
                sql += " and IDThanhVien =" + MoreAll.MoreAll.GetCookies("MembersID") + " ";
                int Tongsobanghi = 0;
                Int16 pages = 1;
                int Tongsotrang = int.Parse("20");
                if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
                {
                    pages = Convert.ToInt16(Request.QueryString["page"].Trim());
                }
                List<LichSuViGiaoDich> iitem = db.ExecuteQuery<LichSuViGiaoDich>(@"SELECT * FROM LichSuViGiaoDich where 1=1 " + sql + " order by NgayTao desc").ToList();
                if (iitem.Count() > 0)
                {
                    double Tien = 0.0;
                    for (int i = 0; i < iitem.Count; i++)
                    {
                        Tien += Convert.ToDouble(iitem[i].TongTien.ToString());
                    }
                    ViewBag.ltCoin = AllQuery.MorePro.Detail_Price(Tien.ToString());
                    Tongsobanghi = iitem.Count();
                }
                int PageIndex = (pages - 1);
                int Tongpage = Tongsotrang;
                int StartRecord = PageIndex * Tongpage;
                int EndRecord = StartRecord + Tongpage + 1;
                List<LichSuViGiaoDich> dt = db.ExecuteQuery<LichSuViGiaoDich>(@"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY NgayTao DESC) AS rowindex ,*  FROM  LichSuViGiaoDich  where 1=1 " + sql + " ) AS A WHERE  ( A.rowindex >  " + StartRecord.ToString() + " AND A.rowindex < " + EndRecord + ")").ToList();
                if (dt.Count >= 1)
                {


                    ViewBag.Show = dt;
                }

                if (Tongsobanghi % Tongsotrang > 0)
                {
                    Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
                }
                else
                {
                    Tongsobanghi = Tongsobanghi / Tongsotrang;
                }
                ViewBag.Phantrang = Commond.Phantrang_loc("/lich-su-vi-tien.html", "&Category=" + Category + "&TuNgay=" + TuNgay + "&DenNgay=" + DenNgay + "", Tongsobanghi, pages);

            }
            else
            {
                Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
            }
            ViewBag.Categorys = Category;
            ViewBag.TuNgays = TuNgay;
            ViewBag.DenNgays = DenNgay;
            return View();
        }
        #endregion

    }
}