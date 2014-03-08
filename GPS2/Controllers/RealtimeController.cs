using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoMembership.Entities;
using MongoMembership.Helpers;
using MongoMembership.Services;

namespace MongoMembership.Web.Controllers
{
    public class RealtimeController : Controller
    {
        //
        // GET: /Realtime/

        [Authorize]
        public ActionResult Index()
        {
            TBL_USERSservices _services = new TBL_USERSservices();
            TBL_USERS user = _services.GetUserByUsername(User.Identity.Name);
            ViewBag.userTimeZone = user.user_timezone.ToString();
            return View();
        }

    }
}
