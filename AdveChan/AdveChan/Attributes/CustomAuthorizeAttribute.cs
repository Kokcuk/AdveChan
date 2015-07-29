using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdveChan.Attributes
{
    public class CustomAuthorizeAttribute:AuthorizeAttribute
    {
        private readonly bool _authorize;

        public CustomAuthorizeAttribute()
        {
            _authorize = false;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Response.Cookies != null)
            {
                var role = httpContext.Response.Cookies["Role"].ToString();
                if (role == "Admin")
                    return true;
            }
            return base.AuthorizeCore(httpContext);
        }
    }
}