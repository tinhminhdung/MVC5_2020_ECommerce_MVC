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
using KhuyenMai;

namespace VS.ECommerce_MVC.Api
{
    public class KhuyenMaiController : ApiControllerBase
    {
        [Route("Mau/listLastestActive")]
        [HttpPost]
        public async Task<IHttpActionResult> listLastestActive()
        {
            var RootKM = new RootKM();
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
            List<KhuyenMai.NewKhuyenKhai> list = DataAccess.Sget_New_All();
            return Ok(list);
        }

    }
}