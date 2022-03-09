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
using CustomAuthetication;

namespace VS.ECommerce_MVC.Api
{
    //[Route("api/[controller]")]
    public class TinTucController : ApiControllerBase
    {
        DatalinqDataContext db = new DatalinqDataContext();

        [CustomAuthorize]
        [Route("TinTuc/GetDetail/id")]
        public async Task<IHttpActionResult> GetDetail(string id)
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
            if (f_token != "Bearer 1234")
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
            return Ok(new { Status = 0, message = "Lỗi" });
        }


    }
}
