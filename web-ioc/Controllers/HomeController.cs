using System.Web.Mvc;
using web_ioc.Models;

namespace web_ioc.Controllers
{
    public class HomeController : Controller
    {
        private ISessionModel _session;

        public HomeController(ISessionModel session)
        {
            _session = session;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            _session.Value = true;

            return View();
        }

        public ActionResult Logoff()
        {
            Session.Abandon();

            return View("Index");
        }
    }
}
