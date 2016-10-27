using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMS.Controllers
{
    public class RegistrationController : Controller
    {
        //
        // GET: /Registration/

        public ActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Gets the logged in user.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetLoggedInUser()
        {
            return Json(User.Identity.Name.ToString(), JsonRequestBehavior.AllowGet);
        }
    }
}
