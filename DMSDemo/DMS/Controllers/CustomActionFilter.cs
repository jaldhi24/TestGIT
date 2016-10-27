using DMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using DMS.Services.BusinessServices;

namespace DMS.Controllers
{
    public class CustomAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// The _user login service
        /// </summary>
        private readonly UserLoginService _userLoginService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAuthorizationAttribute"/> class.
        /// </summary>
        public CustomAuthorizationAttribute()
        {
            _userLoginService = new UserLoginService();
        }

        /// <summary>
        /// Called when authorization is required.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            bool checkUser = false;
            if (ProjectSession.LoggedInUserId == 0)
            {
                var allUsers = _userLoginService.GetAllUsers();
                for (int i = 0; i < allUsers.Count; i++)
                {
                    if (allUsers[i].ServerName.Trim().ToLower() == HttpContext.Current.User.Identity.Name.Trim().ToLower())
                    {
                        checkUser = true;
                        ProjectSession.LoggedInUserId = allUsers[i].Id;
                        ProjectSession.LoggedInServerName = allUsers[i].ServerName.Split('\\')[1];
                        break;
                    }
                }
                if (checkUser == false)
                {
                    filterContext.Result = new RedirectToRouteResult(
                                       new RouteValueDictionary 
                                   {
                                       { "action", "Registration" },
                                       { "controller", "Registration" }
                                   });
                }
            }
        }
    }
}
