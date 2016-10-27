using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Http.WebHost;
using System.Web.SessionState;
using System.Web;

namespace DMS
{
    /// <summary>
    /// Web API Config
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
           RouteTable.Routes.MapHttpRoute(
                name: "SessionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            ).RouteHandler = new SessionRouteHandler();            
         
        }
 
        public class SessionRouteHandler : IRouteHandler
        {
            IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
            {
                return new SessionControllerHandler(requestContext.RouteData);
            }
        }
 
        public class SessionControllerHandler : HttpControllerHandler, IRequiresSessionState
        {
            public SessionControllerHandler(RouteData routeData)
                : base(routeData)
            { }      
        }
    }
 
}