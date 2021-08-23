using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace WebApiTemplate
{
    public static class ResponseExtensions
    {
        public static IActionResult SendResponse<T>(this ProviderResponse<T> providerResponse, string createdAtRoute = null)
        {
            switch (providerResponse.ResponseCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(providerResponse.ResponseData);

                case HttpStatusCode.Created:
                    return new CreatedResult(createdAtRoute, providerResponse.ResponseData);

                case HttpStatusCode.NotFound:
                    return new NotFoundResult();
            }

            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

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
