using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AdveChan.Models;

namespace AdveChan.Controllers
{
    public class StartPageController : Controller
    {
        private readonly ChanContext _chanContext ;

        public StartPageController()
        {
            _chanContext = new ChanContext();
        }

        public ActionResult ShowStartPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Admin model)
        {
            string username = model.Login;
            string password = model.Password;

            bool userValid = _chanContext.Admins.Any(x => x.Login == username && x.Password == password);

            if (userValid)
            {
                FormsAuthentication.SetAuthCookie(username,false);
            } 
            //var admin = _chanContext.Admins.FirstOrDefault(x => x.Login == login);
            //if (admin.Password==password)
            //{ HttpContext.Response.Cookies.Set(new HttpCookie("Role","Admin"));}
            
            return RedirectToAction("ShowStartPage", "StartPage");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("ShowStartPage", "StartPage");
        }
    }
}