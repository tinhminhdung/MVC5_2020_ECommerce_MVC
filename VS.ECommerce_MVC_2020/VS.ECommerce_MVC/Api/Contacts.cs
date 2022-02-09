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
using System.Threading.Tasks;

namespace VS.ECommerce_MVC.Api
{
    public class ContactController : ApiControllerBase
    {
        DatalinqDataContext db = new DatalinqDataContext();
        private string language = "VIE";
        [Route("api/Contact/PostContacts")]
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

        // Xem ảnh đã làm demo ở Api\Note\lienhe.png
        [HttpPost]
        public HttpResponseMessage Save(HttpRequestMessage request, Entity.Contacts person)
        {
            Entity.Contacts obj = new Entity.Contacts();
            obj.vtitle = person.vtitle;
            obj.vname = person.vname;
            obj.vaddress = person.vaddress;
            obj.vphone = person.vphone;
            obj.vemail = person.vemail;
            obj.vcontent = person.vcontent;
            obj.dcreatedate = DateTime.Now;
            obj.lang = "VIE";
            obj.istatus = 0;
            if (SContacts.INSERT(obj) == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse("Lỗi");
        }

        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, Entity.Contacts person)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Entity.Contacts obj = new Entity.Contacts();
                    obj.vtitle = person.vtitle;
                    obj.vname = person.vname;
                    obj.vaddress = person.vaddress;
                    obj.vphone = person.vphone;
                    obj.vemail = person.vemail;
                    obj.vcontent = person.vcontent;
                    obj.dcreatedate = DateTime.Now;
                    obj.lang = "VIE";
                    obj.istatus = 0;
                    if (SContacts.INSERT(obj) == true)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    response = request.CreateResponse(HttpStatusCode.Created, person);
                }
                return response;
            });
        }
    }
}
