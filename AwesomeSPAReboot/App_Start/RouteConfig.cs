using System.Web.Mvc;
using System.Web.Routing;

namespace AwesomeSPAReboot.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }
    }
}