using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace DMS.Filters
{
    /// <summary>
    /// Initialize Simple Membership Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The _initializer{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
        /// </summary>
        private static SimpleMembershipInitializer _initializer;

        /// <summary>
        /// The _initializer lock{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
        /// </summary>
        private static object _initializerLock = new object();

        /// <summary>
        /// The _is initialized{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
        /// </summary>
        private static bool _isInitialized;

        /// <summary>
        /// Called when [action executing].
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        /// <summary>
        /// Simple Membership Initializer
        /// </summary>
        private class SimpleMembershipInitializer
        {
            //public SimpleMembershipInitializer()
            //{
            //    Database.SetInitializer<UsersContext>(null);

            //    try
            //    {
            //        using (var context = new UsersContext())
            //        {
            //            if (!context.Database.Exists())
            //            {
            //                // Create the SimpleMembership database without Entity Framework migration schema
            //                ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
            //            }
            //        }

            //        WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
            //    }
            //}
        }
    }
}
