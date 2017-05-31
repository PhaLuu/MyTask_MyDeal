using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyTask.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
               name: "home",
               url: "",
               defaults: new { controller = "PNL", action = "Index" }
           );
           routes.MapRoute(
               name: "addPassenger",
               url: "add",
               defaults: new { controller = "PNL", action = "AddPassenger" }
           );
           routes.MapRoute(
              name: "editPassenger",
              url: "edit",
              defaults: new { controller = "PNL", action = "EditPassenger" }
          );
        }
    }
}
