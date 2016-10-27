using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.Models
{
    public class ProjectSession 
    {
        //private static int _LoggedInUserId = 0;

        /// <summary>
        /// Gets or sets the logged in user detail.
        /// </summary>
        /// <value>
        /// The logged in user detail.
        /// </value>
        public static int LoggedInUserId
        {
            get
            {
                if (HttpContext.Current.Session["LoggedInUserId"] == null)
                    return 0;
                return Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"].ToString());
            }

            set
            {
                HttpContext.Current.Session["LoggedInUserId"] = value;
            }
        }

        public static string LoggedInServerName
        {
            get
            {
                if (HttpContext.Current.Session["LoggedInServerName"] == null)
                    return string.Empty;;
                return HttpContext.Current.Session["LoggedInServerName"].ToString();
            }

            set
            {
                HttpContext.Current.Session["LoggedInServerName"] = value;
            }
        }
    }
}