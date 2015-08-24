namespace AdveChan.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Common;
    using Entities;
    using Models;

    public class AccountController : Controller
    {
        private const string Salt =
            "ohGPRtQpvRYcKscrFntAFBXQ9xIhiLtHlyRg81Hg2sUe4IcbV74zo3dNuuCzZdtIzWiBPakP9QR6hP+OHV/DTw==";

        private readonly ICryptoProvider _cryptoProvider;
        private readonly ChanContext _dContext;

        public AccountController(ChanContext dContext, ICryptoProvider cryptoProvider)
        {
            _dContext = dContext;
            _cryptoProvider = cryptoProvider;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var login = model.Login;
                var passwordHash = _cryptoProvider.GetHash(model.Password, Salt);

                var userEntity =
                    _dContext.Users.FirstOrDefault(x => x.Login == login && x.PasswordHash == passwordHash);

                if (userEntity != null)
                {
                    var authTicket = new FormsAuthenticationTicket(
                        1,
                        login,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(20),
                        true,
                        userEntity.UserRole.ToString()
                        );
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "The user login or password provided is incorrect.");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}