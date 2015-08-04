using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Chat.Infrastructure;
using MVC_Chat.Models;
using System.Web;
using System.Net.Mail;
using System.Web.Helpers;
using System.Web.Security;
using MVC_Chat.Filtres;

namespace MVC_Chat.Controllers
{
    public class CheckController : Controller
    {
         UserContext UC = new UserContext();

        [HttpPost]
        public ActionResult Entering(string login, string password, string action)
        {
            if (action == "Entering")
            {
                var u = UC.Users.Find(login);
                if (u != null)
                {
                    if (u.Nickname == login && u.Password == password)
                    {                        
                        TempData["nickname"] = u.Nickname;
                        HttpContext.Response.Cookies.Set(new HttpCookie("Nickname", u.Nickname));
                        return Redirect("/Chat/Index");
                    }
                    else
                        return HttpNotFound();
                }
                return HttpNotFound();
                
            }
            else
            {
                return View("~/Views/Check/Registration.cshtml");
            }
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (ModelState.IsValid)
            {
                var informationToAdd = new User {Nickname = user.Nickname, Password = user.Password, Email = user.Email};
                UC.Users.Add(informationToAdd);
                UC.SaveChanges();
                string textOfMail = "Thank you for registration in our chat.\n Your nickname: " + user.Nickname +
                                    "\n Your password: " + user.Password;
                try
                {
                    SendMail(user.Email, "Thank you for registration", textOfMail);
                }
                catch (Exception)
                {

                    return View("Thanks", user);
                }
                
                return View("Thanks", user);
            }
                return View();
        }

        public void SendMail(string reciever, string caption, string message)
        {           
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 587;
            WebMail.EnableSsl = true;
            WebMail.UserName = "Email";
            WebMail.Password = "Password";
            WebMail.From = "shamilbasaev359@gmail.com";
            WebMail.Send(reciever, caption, message);

        }
        protected override void Dispose(bool disposing)
        {
            UC.Dispose();
            base.Dispose(disposing);
        }
    }
}