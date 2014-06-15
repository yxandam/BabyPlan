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
    /// 宝贝列表路由规则解析，重写
    /// </summary>
    public class TradeListRoute : RouteBase
    {
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var virtualPath = httpContext.Request.AppRelativeCurrentExecutionFilePath + httpContext.Request.PathInfo;
            virtualPath = virtualPath.ToLower().Substring(2).Trim('/');

            if (!virtualPath.StartsWith("二手宝贝") && !virtualPath.StartsWith("ershou"))
                return null;

            var param = TradeListParamManager.ParamParse(virtualPath);

            var data = new RouteData(this, new MvcRouteHandler());
            data.Values.Add("controller", "Trade");
            data.Values.Add("action", "List");
            data.Values.Add("category", param.Category);
            data.Values.Add("sex", param.Sex);
            data.Values.Add("age", param.Age);
            data.Values.Add("sort", param.Sort);
            data.Values.Add("range", param.Range);
            return data;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            if (!values.ContainsKey("controller") || !values["controller"].ToString().Equals("trade", StringComparison.OrdinalIgnoreCase))
                return null;

            if (!values.ContainsKey("action") || !values["action"].ToString().Equals("list", StringComparison.OrdinalIgnoreCase))
                return null;
            string path = "ershou/" + TradeListParamManager.GeneratParamUrl(requestContext.RouteData.Values, values);

            return new VirtualPathData(this, path.ToLowerInvariant());
        }
    }
}