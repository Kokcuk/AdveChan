namespace AdveChan.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Boards()
        {
            return View();
        }
    }
}