using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

namespace DMS.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult</returns>
        [CustomAuthorization]
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        /// <summary>
        /// Unauthorizes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Unauthorize()
        {
            return View("Registration", "~/Views/Shared/_SignupLayout.cshtml"); ;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetLoggedInUser()
        {
            return Json(User.Identity.Name.ToString(), JsonRequestBehavior.AllowGet);
        }    
    }

}
