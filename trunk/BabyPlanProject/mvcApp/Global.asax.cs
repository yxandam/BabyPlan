﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BabyPlan.mvcApp.Infras;
using BabyPlan.mvcApp.UrlRoutes;

namespace BabyPlan.mvcApp
{

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add(new TradeDetailRoute());

            routes.Add(new TradeListRoute());

            routes.MapRoute(
                "TradeDetailRoute", // Route name
                "Trade/Detail/{pid}", // URL with parameters
                new { controller = "Trade", action = "Detail", pid = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "TradeListRoute", // Route name
                "Trade/List/{category}/{*values}", // URL with parameters
                new { controller = "Trade", action = "List", category = "All" } // Parameter defaults
            );

            routes.MapRoute(
                "TradeRoute", // Route name
                "Trade/{action}/{pid}", // URL with parameters
                new { controller = "Trade", action = "Detail", pid = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "LoginCallback", // 第三方登录回调
                "Account/Social_Callback/{type}", // 带有参数的 URL
                new { controller = "Account", action = "Social_Callback" } // 参数默认值
            );
            routes.MapRoute(
                "Upload", // 第三方登录回调
                "Upload/Uploader/{token}", // 带有参数的 URL
                new { controller = "Upload", action = "Uploader", token = UrlParameter.Optional } // 参数默认值
            );
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngineEx());


            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}