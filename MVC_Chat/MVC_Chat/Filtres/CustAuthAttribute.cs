using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Chat.Infrastructure;

namespace MVC_Chat.Filtres
{
    public class CustAuthAttribute:AuthorizeAttribute
    {
        UserContext _userContext = new UserContext();

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuth = false;
            if (httpContext==null) throw new ArgumentNullException("httpContext");
            if (httpContext.User.Identity.IsAuthenticated) return true;
            try
            {
                var httpCookie = httpContext.Response.Cookies["Nickname"];
                if (httpCookie != null)
                    isAuth = true;
            }
            catch
            { }
            return isAuth;
        }
    }
}