using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using AdveChan.App_Start;
using AdveChan.Models;

namespace AdveChan
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        string username =
                            FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName]
                                .Value).Name;
                        string roles = String.Empty;
                        using (ChanContext chancontext = new ChanContext())
                        {
                            Admin admin = chancontext.Admins.SingleOrDefault(a => a.Login == username);
                            roles = admin.Role;
                        }
                        HttpContext.Current.User = new GenericPrincipal(
                            new GenericIdentity(username,"Forms"),roles.Split(';') );
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
    }
}
