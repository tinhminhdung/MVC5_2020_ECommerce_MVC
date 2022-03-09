using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomAuthetication
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authroized = base.AuthorizeCore(httpContext);
            if (!authroized)
            {
                // người dùng không được xác thực hoặc xác thực biểu mẫu
                // cookie đã hết hạn
                return false;
            }

            // Bây giờ kiểm tra phiên:
            //  var myvar = httpContext.Session["User"];
            // if (myvar == null)
            if (MoreAll.MoreAll.GetCookies("MembersIDA").ToString() == null)
            {
                // phiên đã hết hạn
                return false;
            }
            return true;
        }
    }

}
