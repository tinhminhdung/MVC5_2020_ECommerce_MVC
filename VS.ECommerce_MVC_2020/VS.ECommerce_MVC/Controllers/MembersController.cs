using Facebook;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                var user = db.Members.SingleOrDefault(x => x.Email == email);
                if (user == null)
                {
                    string validatekey = DateTime.Now.Ticks.ToString();
                    Member obj = new Member();
                    obj.PasWord = "";
                    obj.HoVaTen = firstname + " " + middlename + " " + lastname;
                    obj.GioiTinh = 0;
                    obj.NgaySinh = DateTime.Now.ToString("dd/MM/yyyy");
                    obj.DiaChi = "";
                    obj.DienThoai = "";
                    obj.Email = email;
                    obj.AnhDaiDien = "";
                    obj.NgayTao = DateTime.Now;
                    obj.Key = validatekey;
                    obj.TrangThai = 0;
                    obj.Lang = language;
                    db.Members.InsertOnSubmit(obj);
                    db.SubmitChanges();
                }
                else
                {
                    MoreAll.MoreAll.SetCookie("Members", email.Trim().ToLower(), 5000);
                    MoreAll.MoreAll.SetCookie("MembersID", user.ID.ToString(), 5000);
                    return Redirect("/");
                }
                //var user = new User();
                //user.Email = email;
                //user.UserName = email;
                //user.Status = true;
                //user.Name = firstname + " " + middlename + " " + lastname;
                //user.CreatedDate = DateTime.Now;
                //var resultInsert = new UserDao().InsertForFacebook(user);
                //if (resultInsert > 0)
                //{
                //    var userSession = new UserLogin();
                //    userSession.UserName = user.UserName;
                //    userSession.UserID = user.ID;
                //    Session.Add(CommonConstants.USER_SESSION, userSession);
                //}
            }
            return Redirect("/");
        }

        #region Dang_Ky
        public ActionResult Dang_Ky()
        {
            return View();
        }

        //Dạng bình thường ko qua Ajax
        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Dang_Ky(FormCollection collect)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Member list = db.Members.SingleOrDefault(s => s.Email.Contains(collect["txtemail"].Trim().ToLower()));
        //        Member listDienThoai = db.Members.SingleOrDefault(s => s.DienThoai.Contains(collect["txtphone"].Trim().ToLower()));
        //        if (collect["txtHoTen"]=="")
        //        {
        //            ViewBag.ThongBao = "Vui lòng điền đầy đủ họ và tên";
        //        }
        //        else if (collect["txtphone"]=="")
        //        {
        //            ViewBag.ThongBao = "Vui lòng điền điện thoại";
        //        }
        //        else if (MoreAll.RegularExpressions.phone(collect["txtphone"]))
        //        {
        //            ViewBag.ThongBao = "Điện thoại không đúng định dạng";
        //        }
        //        else if (listDienThoai != null)
        //        {
        //            ViewBag.ThongBao = "Điện thoại đã tồn tại trong hệ thống";
        //        }
        //        else if (collect["txtaddress"]=="")
        //        {
        //            ViewBag.ThongBao = "Vui lòng điền địa chỉ";
        //        }
        //        else if (!MoreAll.RegularExpressions.IsValidEmail(collect["txtemail"]))
        //        {
        //            ViewBag.ThongBao = "Email không đúng định dạng";
        //        }
        //        else if (collect["txtemail"]=="")
        //        {
        //            ViewBag.ThongBao = "Vui lòng điền email";
        //        }
        //        else if (list != null)
        //        {
        //            ViewBag.ThongBao = "Email đã tồn tại trong hệ thống";
        //        }
        //        else
        //        {
        //            //System.Threading.Thread.Sleep(1000);
        //            //#region Senmail
        //            //if (!Commond.Setting("Emailden").Equals(""))
        //            //    Senmail(collect["txtHoTen"], collect["txtaddress"], collect["txtphone"], collect["txtemail"], collect["txttieude"], collect["txtcontent"]);
        //            //#endregion

        //            string validatekey = DateTime.Now.Ticks.ToString();
        //            Member obj = new Member();
        //            obj.PasWord = collect["txtmatkhau"];
        //            obj.HoVaTen = collect["txtHoTen"];
        //            obj.GioiTinh = 0;
        //            obj.NgaySinh = DateTime.Now;
        //            obj.DiaChi = collect["txtaddress"];
        //            obj.DienThoai = collect["txtphone"];
        //            obj.Email = collect["txtemail"];
        //            obj.AnhDaiDien = "";
        //            obj.NgayTao = DateTime.Now;
        //            obj.Key = validatekey;
        //            obj.TrangThai = 0;
        //            obj.Lang = language;
        //            db.Members.InsertOnSubmit(obj);
        //            db.SubmitChanges();
        //            ViewBag.ThongBao = "Đăng ký tài khoản thành công";
        //        }
        //    }
        //    return View();
        //}

        //Ajax

        [HttpPost]
        public ActionResult Dang_Ky(string txtHoTen, string txtemail, string txtphone, string txtaddress, string txtmatkhau)
        {
            if (ModelState.IsValid)
            {
                List<Entity.Member> list = SMember.Name_Text("select Email from Members where Email like N'%" + txtemail.Trim().ToLower() + "'");
                List<Entity.Member> listDienThoai = SMember.Name_Text("select DienThoai from Members where DienThoai like N'%" + txtemail.Trim().ToLower() + "'");

                if (listDienThoai.Count > 0)
                {
					 ViewBag.ThongBao = "Điện thoại đã tồn tại trong hệ thống";
                   // TempData["ThongBao"] = "Điện thoại đã tồn tại trong hệ thống";
                    return View();
                }
                if (list.Count > 0)
                {
					 ViewBag.ThongBao = "Email đã tồn tại trong hệ thống";
                    //TempData["ThongBao"] = "Email đã tồn tại trong hệ thống";
                    return View();
                }
                if (MoreAll.RegularExpressions.phone(txtphone))
                {
                    ViewBag.ThongBao = "Điện thoại không đúng định dạng";
                }
               
                else
                {
                    //System.Threading.Thread.Sleep(1000);
                    //#region Senmail
                    //if (!Commond.Setting("Emailden").Equals(""))
                    //    Senmail(txtHoTen, txtaddress, txtphone, txtemail, txttieude, txtcontent);
                    //#endregion

                    string validatekey = DateTime.Now.Ticks.ToString();
                    Member obj = new Member();
                    obj.PasWord = txtmatkhau;
                    obj.HoVaTen = txtHoTen;
                    obj.GioiTinh = 0;
                    obj.NgaySinh = DateTime.Now.ToString("dd/MM/yyyy");
                    obj.DiaChi = txtaddress;
                    obj.DienThoai = txtphone;
                    obj.Email = txtemail;
                    obj.AnhDaiDien = "";
                    obj.NgayTao = DateTime.Now;
                    obj.Key = validatekey;
                    obj.TrangThai = 1;
                    obj.Lang = language;
                    db.Members.InsertOnSubmit(obj);
                    db.SubmitChanges();
                    ViewBag.ThongBao = "Đăng ký tài khoản thành công";
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
                        List<Entity.Member> table = SMember.Name_Text("select * from Members where Email='" + txttendangnhap.Trim().ToLower() + "' and PasWord='" + (txtmatkhau.Trim().ToLower()) + "' and TrangThai=1");
                        if (table.Count < 1)
                        {
                            ViewBag.ThongBao = "Tài khoản không đúng hoặc chưa được kích hoạt";
                        }
                        else
                        {
                            MoreAll.MoreAll.SetCookie("Members", txttendangnhap.Trim().ToLower(), 5000);
                            MoreAll.MoreAll.SetCookie("MembersUser", table[0].HoVaTen.ToString(), 5000);
                            MoreAll.MoreAll.SetCookie("MembersID", table[0].ID.ToString(), 5000);
                            // return Redirect("/");
                            if (Url.IsLocalUrl(returnUrl))
                            {
                                ViewBag.ReturnUrl = returnUrl;
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return Redirect("/");
                                // return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                    else if (!RegularExpressions.phone(txttendangnhap.Trim().ToLower()))
                    {
                        List<Entity.Member> table = SMember.Name_Text("select * from Members where DienThoai='" + txttendangnhap.Trim().ToLower() + "' and PasWord='" + (txtmatkhau.Trim().ToLower()) + "' and TrangThai=1");
                        if (table.Count < 1)
                        {
                            ViewBag.ThongBao = "Tài khoản không đúng hoặc chưa được kích hoạt";
                        }
                        else
                        {

                            MoreAll.MoreAll.SetCookie("Members", txttendangnhap.Trim().ToLower(), 5000);
                            MoreAll.MoreAll.SetCookie("MembersID", table[0].ID.ToString(), 5000);
                            // return Redirect("/");
                            if (Url.IsLocalUrl(returnUrl))
                            {
                                ViewBag.ReturnUrl = returnUrl;
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return Redirect("/");
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
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult HoSoThanhVien(FormCollection collect, HttpPostedFileBase file)
        {
			
			// code mẫu
			//F:\Congty_APhuc\Nam2021\Thang08\VS.CongDong_MVC_DaiHocDongDO\VS.E_Commerce\Controllers
			// lấy cả htmj , javascript
			
			
			                //#region Avata
                //string Avata = "";
                //HttpPostedFileBase postedFileava = Request.Files["fileavatar"];
                //string path15 = Server.MapPath("~/Uploads/pic/avatar/");
                //if (!Directory.Exists(path15))
                //{
                //    Directory.CreateDirectory(path15);
                //}
                //string str685 = "";
                //if (postedFileava.FileName.Length > 0)
                //{
                //    str685 = Path.GetExtension(postedFileava.FileName).ToLower();
                //    Avata = DateTime.Now.Ticks.ToString() + str685;
                //    postedFileava.SaveAs(path15 + Avata);
                //}

                //#endregion

                //#region MyRegion
                //string UpFileCMT = "";
                //HttpPostedFileBase postedFileCMT = Request.Files["FileAttachment1"];
                //string path1 = Server.MapPath("~/Uploads/pic/advs/");
                //if (!Directory.Exists(path1))
                //{
                //    Directory.CreateDirectory(path1);
                //}
                //int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB
                //if (postedFileCMT.ContentLength > MaxContentLength)
                //{
                //    SetAlert("<img src='/Resources/images/iconthongbao.jpg' /> File Chứng minh thư không vượt quá 100 MB", "warning");
                //    return View();
                //}
                //string str65 = "";
                //if (postedFileCMT.FileName.Length > 0)
                //{
                //    str65 = Path.GetExtension(postedFileCMT.FileName).ToLower();
                //    UpFileCMT = DateTime.Now.Ticks.ToString() + 2 + str65;
                //    postedFileCMT.SaveAs(path1 + UpFileCMT);
                //}
                //#endregion

                //#region UpFileHocBa
                //string UpFileHocBa = "";
                //HttpPostedFileBase postedFile = Request.Files["FileAttachment2"];
                //string path = Server.MapPath("~/Uploads/pic/advs/");
                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //int MaxContentLength1 = 1024 * 1024 * 10; //Size = 10 MB
                //if (postedFile.ContentLength > MaxContentLength1)
                //{
                //    SetAlert("<img src='/Resources/images/iconthongbao.jpg' /> File Chứng minh thư không vượt quá 100 MB", "warning");
                //    return View();
                //}
                //string str66 = "";
                //if (postedFile.FileName.Length > 0)
                //{
                //    str66 = Path.GetExtension(postedFile.FileName).ToLower();
                //    UpFileHocBa = DateTime.Now.Ticks.ToString() + 1 + str66;
                //    postedFile.SaveAs(path + UpFileHocBa);
                //}
                //#endregion

                //string AnhAvatass = "";
                //if (Avata != "")
                //{
                //    AnhAvatass = "/Uploads/pic/avatar/" + Avata;
                //}
                //string FileCMCD = "";
                //if (UpFileCMT != "")
                //{
                //    FileCMCD = "/Uploads/pic/advs/" + UpFileCMT;
                //}
                //string FileHocBa = "";
                //if (UpFileHocBa != "")
                //{
                //    FileHocBa = "/Uploads/pic/advs/" + UpFileHocBa;
                //}
				
				
            List<Entity.Member> list = SMember.Name_Text("select * from Members  where Email='" + collect["txtemail"].Trim().ToLower() + "' and ID!=" + MoreAll.MoreAll.GetCookies("MembersID").ToString() + "");
            List<Entity.Member> listDienThoai = SMember.Name_Text("select * from Members  where DienThoai='" + collect["txtphone"].Trim().ToLower() + "' and ID!=" + MoreAll.MoreAll.GetCookies("MembersID").ToString() + "");

            //string vcvcc = collect["HiddenAnhDaiDien"];
            ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
           
			 string _FileName = "";
            string vimg = "";
            try
            {
                if (file.ContentLength > 0)
                {
                    _FileName = Path.GetFileName(file.FileName);
                    if (collect["HiddenAnhDaiDien"].Length > 0)
                    {
                        try
                        {
                            System.IO.File.Delete(utlitities.APPL_PHYSICAL_PATH + collect["HiddenAnhDaiDien"]);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    string str6 = Path.GetExtension(_FileName).ToLower();
                    vimg = DateTime.Now.Ticks.ToString() + str6;
                    string _path = Path.Combine(Server.MapPath("~/Uploads/avatar"), vimg);
                    file.SaveAs(_path);
                }
            }
            catch (Exception)
            { }
			

			ViewBag.Phone = collect["txtphone"];
			ViewBag.Name = collect["txtHoTen"];
			ViewBag.NgaySinh = collect["txtngaysinh"];
			ViewBag.Email = collect["txtemail"];
			ViewBag.Address = collect["txtaddress"];
					
            if (ModelState.IsValid)
            {
                if (collect["txtHoTen"] == "")
                {

                    SetAlert("Vui lòng điền đầy đủ họ và tên", "warning");
                   
                   
                }
                else if (collect["txtphone"] == "")
                {
                    SetAlert("Vui lòng điền điện thoại", "warning");
                   
                  
                }
                else if (MoreAll.RegularExpressions.phone(collect["txtphone"]))
                {
                    SetAlert("Điện thoại không đúng định dạng", "warning");
                  
                   
                }
                else if (listDienThoai.Count > 0)
                {
                    SetAlert("Điện thoại đã tồn tại trong hệ thống", "warning");
                   
                }
                else if (collect["txtaddress"] == "")
                {
                    SetAlert("Vui lòng điền địa chỉ", "warning");
                   
                }
                else if (!MoreAll.RegularExpressions.IsValidEmail(collect["txtemail"]))
                {
                    SetAlert("Email không đúng định dạng", "warning");
                   
                }
                else if (collect["txtemail"] == "")
                {
                    SetAlert("Vui lòng điền email", "warning");
                   
                }
                else if (list.Count > 0)
                {
                    SetAlert("Email đã tồn tại trong hệ thống", "warning");
                    
                }
                else
                {
                    Member obj = db.Members.SingleOrDefault(p => p.ID == int.Parse(MoreAll.MoreAll.GetCookies("MembersID").ToString()));
                    obj.HoVaTen = collect["txtHoTen"];
                    obj.GioiTinh = 0;
                    obj.NgaySinh = collect["txtngaysinh"];
                    obj.DiaChi = collect["txtaddress"];
                    obj.DienThoai = collect["txtphone"];
                    obj.Email = collect["txtemail"];
                    if (vimg.Length > 0)
                    {
                        obj.AnhDaiDien = "/Uploads/avatar/" + vimg;
                    }
                    obj.Lang = language;
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
                    ViewBag.Email = dt.Email;
                    ViewBag.Phone = dt.DienThoai;
                    ViewBag.NgaySinh = dt.NgaySinh;
                    if (dt.AnhDaiDien.Length > 0)
                    {
                        ViewBag.AnhDaiDiens = dt.AnhDaiDien;
                        ViewBag.Avata = " <img src=\"" + dt.AnhDaiDien + "\" style=\" width:100px\" />";
                    }
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

                //string sql = "select * from Carts where IDThanhVien=" + MoreAll.MoreAll.GetCookies("MembersID").ToString() + "";
                //sql = sql + " order by Create_Date desc";
                //List<LCart> dt = db.ExecuteQuery<LCart>(@"" + sql + "").ToList();
                //if (dt.Count > 0)
                //{
                //    return View(dt);
                //}
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
    }
}