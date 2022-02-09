using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using AutoMapper;
using VS.ECommerce_MVC.Models;
using Framework;
using Entity;
using System.IO;
using System.Web;
using NganLuongAPI;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VS.ECommerce_MVC.Api
{
    //[Route("api/[controller]")]
    public class MauController : ApiControllerBase
    {
        DatalinqDataContext db = new DatalinqDataContext();
        //Check Authorization
        //swagger/ui/index#!/Default/Default_GetAll
        [Route("Mau/getall")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            string res = "OK";
            var re = Request;
            var headers = re.Headers;

            string f_token = "";
            if (headers.Contains("Authorization"))
            {
                f_token = headers.GetValues("Authorization").First();
            }
            else
            {
                res = "Không tìm thấy token";
            }
            //check format Auth
            if (!f_token.Contains("Bearer"))
            {
                res = "Authorization không đúng định dạng";
            }

            // kiểm tra db xem có key :Authorization: "Bearer 123333"
            //        //check account
            //        //var acc = DataAccess.DataAccess.sp_BBM_Partner_get_USER(0, 1, new user_Item { userName = "", password = "", token = f_token.Replace("Bearer", null).Trim() });

            //        //if (acc.userName == null)
            //        //{
            //        //    return BadRequest("account not found or blocked");
            //        //}
            //        //else
            //        //{

            //        //    return Ok(bn);
            //        //}
            if (f_token != "Bearer q9a2rWAxkQ1fwAM0EcdWj5FbyemqY9AMx3qQKY9Srm46i8Y80BsvWFSvyGy9os9ENLJTS")
            {
                res = "Token ko đúng Authorization Bearer";
            }

            var responseData = res.ToString();
            var response = Request.CreateResponse(HttpStatusCode.OK, responseData);
            return response;
        }


        // Chuẩn
        //Check Authorization
        //làm theo luồng bibo, get. post, delete qua db
        [Route("Mau/Test")]
        [HttpPost]
        public async Task<IHttpActionResult> Test()
        {
            #region CheckAuthorization
            var re = Request;
            var headers = re.Headers;
            string f_token = "";
            if (headers.Contains("Authorization"))
            {
                f_token = headers.GetValues("Authorization").First();
            }
            else
            {
                return BadRequest("Không tìm thấy token");
            }
            //check format Auth
            if (!f_token.Contains("Bearer"))
            {
                return BadRequest("Authorization không đúng định dạng");
            }
            if (f_token != "Bearer q9a2rWAxkQ1fwAM0EcdWj5FbyemqY9AMx3qQKY9Srm46i8Y80BsvWFSvyGy9os9ENLJTS")
            {
                return BadRequest("Token ko đúng Authorization Bearer");
            }
            #endregion
            return Ok("OK - 200");
        }


        // Chuẩn
        //Check Authorization
        //làm theo luồng bibo, get. post, delete qua db
        [Route("Mau/Test2")]
        [HttpPost]
        public async Task<IHttpActionResult> Test2()
        {
            #region CheckAuthorization
            var re = Request;
            var headers = re.Headers;
            string f_token = "";
            if (headers.Contains("Authorization"))
            {
                f_token = headers.GetValues("Authorization").First();
            }
            else
            {
                return BadRequest("Không tìm thấy token");
            }
            //check format Auth
            if (!f_token.Contains("Bearer"))
            {
                return BadRequest("Authorization không đúng định dạng");
            }
            if (f_token != "Bearer q9a2rWAxkQ1fwAM0EcdWj5FbyemqY9AMx3qQKY9Srm46i8Y80BsvWFSvyGy9os9ENLJTS")
            {
                return BadRequest("Token ko đúng Authorization Bearer");
            }
            #endregion
            return Ok("OK - 200");
        }

        [Route("Mau/Get_Detail_New/id")]
        [HttpPost]
        public async Task<IHttpActionResult> Get_Detail_New(string id)
        {
            #region CheckAuthorization
            var re = Request;
            var headers = re.Headers;
            string f_token = "";
            if (headers.Contains("Authorization"))
            {
                f_token = headers.GetValues("Authorization").First();
            }
            else
            {
                return BadRequest("Không tìm thấy token");
            }
            //check format Auth
            if (!f_token.Contains("Bearer"))
            {
                return BadRequest("Authorization không đúng định dạng");
            }
            if (f_token != "Bearer q9a2rWAxkQ1fwAM0EcdWj5FbyemqY9AMx3qQKY9Srm46i8Y80BsvWFSvyGy9os9ENLJTS")
            {
                return BadRequest("Token ko đúng Authorization Bearer");
            }
            #endregion

            var list = DataAccess.Sget_New_Detail(id);
            if (list.Count > 0)
            {
                return Ok(list);
                // return Ok(JsonConvert.SerializeObject(list));
            }
            else
            {
                return Ok(new { StatusCode = 0, message = " Lỗi " });
            }
            return Ok(new { StatusCode = 0, message = " Lỗi " });
        }


        [Route("LienHe/PostContacts")]
        [HttpPost]
        public async Task<IHttpActionResult> PostContacts(API_Contact it)
        {
            #region CheckAuthorization
            var re = Request;
            var headers = re.Headers;
            string f_token = "";
            if (headers.Contains("Authorization"))
            {
                f_token = headers.GetValues("Authorization").First();
            }
            else
            {
                return BadRequest("Không tìm thấy token");
            }
            //check format Auth
            if (!f_token.Contains("Bearer"))
            {
                return BadRequest("Authorization không đúng định dạng");
            }
            if (f_token != "Bearer q9a2rWAxkQ1fwAM0EcdWj5FbyemqY9AMx3qQKY9Srm46i8Y80BsvWFSvyGy9os9ENLJTS")
            {
                return BadRequest("Token ko đúng Authorization Bearer");
            }
            #endregion

            #region Contacts
            Entity.Contacts obj = new Entity.Contacts();
            obj.vtitle = it.vtitle;
            obj.vname = it.vname;
            obj.vaddress = it.vaddress;
            obj.vphone = it.vphone;
            obj.vemail = it.vemail;
            obj.vcontent = it.vcontent;
            obj.dcreatedate = DateTime.Now;
            obj.lang = "VIE";
            obj.istatus = 0;
            if (SContacts.INSERT(obj) == true)
            {
                return Ok(new { Status = 200, message = Commond.label("lienhes7") });
                //TempData["ThongBao"] 
                //#region Senmail
                //if (!Commond.Setting("Emailden").Equals(""))
                //    Senmail(collect["txtHoTen"], collect["txtaddress"], collect["txtphone"], collect["txtemail"], collect["txttieude"], collect["txtcontent"]);
                //#endregion
            }
            #endregion
            return Ok(new { Status = 0, message = "Lỗi" });
        }


        //Check Authorization
        //[Route("api/SMSSend/v1")]
        //[HttpPost]
        //public async Task<IHttpActionResult> SMSSend(CSKH_v2 it)
        //{

        //    var re = Request;
        //    var headers = re.Headers;

        //    string f_token = "";

        //    try
        //    {
        //        if (headers.Contains("Authorization"))
        //        {
        //            f_token = headers.GetValues("Authorization").First();
        //            LogBuild.CreateLogger(f_token, "SMSSend");
        //        }
        //        else
        //        {
        //            return BadRequest("token lost!");
        //        }

        //        //check format Auth
        //        if (!f_token.Contains("Bearer"))
        //        {
        //            return BadRequest("Authorization wrong format");
        //        }


        //        //check account
        //        var acc = DataAccess.DataAccess.sp_BBM_Partner_get_USER(0, 1, new user_Item { userName = "", password = "", token = f_token.Replace("Bearer", null).Trim() });

        //        if (acc.userName == null)
        //        {
        //            return BadRequest("account not found or blocked");
        //        }
        //        else
        //        {
        //            LogBuild.CreateLogger("start :" + JsonConvert.SerializeObject(it), "SMSSend");

        //            var str_token = DataAccess.DataAccess.SMSv2_get_tokenSMS();

        //            //if (int.Parse(it.type.ToString()) == 1)
        //            //{
        //            //    var bn = await DataAccess.DataAccess.SMSv2_send_CSKH(it, str_token);
        //            //    LogBuild.CreateLogger("Return fillter :" + JsonConvert.SerializeObject(bn), "EmailSend");
        //            //    return Ok(bn);
        //            //}
        //            //else
        //            //{
        //            //    var bn = await DataAccess.DataAccess.SMSv2_send_KM(it, str_token);
        //            //    LogBuild.CreateLogger("Return fillter :" + JsonConvert.SerializeObject(bn), "EmailSend");
        //            //    return Ok(bn);
        //            //}

        //            var bn = await DataAccess.DataAccess.SMSv2_send_CSKH(it, str_token);
        //            LogBuild.CreateLogger("Return fillter :" + JsonConvert.SerializeObject(bn), "EmailSend");
        //            return Ok(bn);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBuild.CreateLogger(JsonConvert.SerializeObject(ex), "MobileAppNotify");
        //        return BadRequest("try again later");
        //    }
        //}


        // Check Authorization
        //public async static Task<API_return> API_PUSH_SMS_KHUYENMAI_MARCOM(Models.ERP_API.Marcom.ObjSMS_KM it)
        //{
        //    LogBuild.CreateLogger(JsonConvert.SerializeObject(it), "API_PUSH_SMS_KHUYENMAI_MARCOM");
        //    API_return rt = new API_return();
        //    try
        //    {
        //        var client = new HttpClient();
        //        string key = "q9a2rWAxkQ1fwAM0EcdWj5FbyemqY9AMx3qQKY9Srm46i8Y80BsvWFSvyGy9os9ENLJTS";
        //        string url = APIDomain + "/api/SMSSendKM/v1";

        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + key);

        //        string strParam = JsonConvert.SerializeObject(it);
        //        var content = new StringContent(strParam, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await client.PostAsync(url, content);

        //        string res = await response.Content.ReadAsStringAsync();
        //        //JObject json = JObject.Parse(res);
        //        if ((int)response.StatusCode == 200)
        //        {
        //            rt.codeReturn = true;
        //            rt.orderNo = JsonConvert.SerializeObject(res);
        //        }
        //        else
        //        {
        //            rt.codeReturn = false;
        //            rt.orderNo = JObject.Parse(res)["Message"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        rt.codeReturn = false;
        //        rt.orderNo = JsonConvert.SerializeObject(ex);
        //    }
        //    LogBuild.CreateLogger(JsonConvert.SerializeObject(rt), "API_PUSH_SMS_KHUYENMAI_MARCOM");
        //    return rt;
        //}


    }
}
