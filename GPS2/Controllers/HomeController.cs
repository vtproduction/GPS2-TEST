using System;
using System.Web.Mvc;
using System.Web.Security;

namespace MongoMembership.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string currentUserName = User.Identity.Name;
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            //MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
            //string currentName = (Request.IsAuthenticated)? currentUser.Email : "welcome !";
            //ViewBag.Message = currentName;
            return RedirectToAction("index","realtime");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
