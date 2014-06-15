﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BabyPlan.DataStructure;
using System.Text;
using System.Text.RegularExpressions;

namespace BabyPlan.mvcApp.UrlRoutes
{
    /// <summary>
    /// 宝贝详情路由规则解析，重写
    /// </summary>
    public class TradeDetailRoute : RouteBase
    {
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var virtualPath = httpContext.Request.AppRelativeCurrentExecutionFilePath + httpContext.Request.PathInfo;
            virtualPath = virtualPath.ToLower().Substring(2).Trim('/');

            if (!virtualPath.StartsWith("二手宝贝") && !virtualPath.StartsWith("ershou"))
                return null;

            Regex detailRegex = new Regex(@"((二手宝贝)|(ershou))/([^/]+?/)?(?<pid>\d+)");
            if (!detailRegex.IsMatch(virtualPath))
                return null;

            int id = 0;
            Match match = detailRegex.Match(virtualPath);

            int.TryParse(match.Groups["pid"].Value, out id);

            var data = new RouteData(this, new MvcRouteHandler());
            data.Values.Add("controller", "Trade");
            data.Values.Add("action", "Detail");
            data.Values.Add("id", id);
            return data;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            if (!values.ContainsKey("controller") || !values["controller"].ToString().Equals("trade", StringComparison.OrdinalIgnoreCase))
                return null;

            if (!values.ContainsKey("action") || !values["action"].ToString().Equals("detail", StringComparison.OrdinalIgnoreCase))
                return null;

            int id = 0;
            if (values.ContainsKey("id"))
            { 
                int.TryParse(values["id"].ToString(),out id);
            }
            //string path = "二手宝贝/" + id.ToString();
            string path = "ershou/detail/" + id.ToString();

            return new VirtualPathData(this, path.ToLowerInvariant());
        }
    }
}