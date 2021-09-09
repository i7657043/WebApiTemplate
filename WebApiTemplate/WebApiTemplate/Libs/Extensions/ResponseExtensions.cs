﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace WebApiTemplate
{
    public static class ResponseExtensions
    {
        public static string GetCreatedAtRoute(this ControllerContext controllerContext, string id)
        {
            return
                $"{controllerContext.HttpContext.Request.Scheme}://" +
                $"{controllerContext.HttpContext.Request.Host}/" +
                $"{controllerContext.RouteData.Values.GetValueOrDefault("controller")}/" +
                $"{controllerContext.RouteData.Values.GetValueOrDefault("action")}" +
                $"{id}";
        }
    }
}