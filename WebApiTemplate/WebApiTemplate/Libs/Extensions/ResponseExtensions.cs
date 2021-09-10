using Microsoft.AspNetCore.Mvc;
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

        public static string GetResponseMessageFromHttpStatusCode(this HttpStatusCode httpStatusCode)
        {
            switch (httpStatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return $"A bad request was made";

                case HttpStatusCode.NotFound:
                    return $"The resource could not be found";

                case HttpStatusCode.Unauthorized:
                    return $"You are not authorised to access this resource";

                case HttpStatusCode.Forbidden:
                    return $"You are forbidden access to this resource";

                case HttpStatusCode.UnprocessableEntity:
                    return $"The resource included within the request cannot be created";

                case HttpStatusCode.InternalServerError:
                    return $"Sorry, something went wrong";

                case HttpStatusCode.BadGateway:
                    return $"Sorry, something went wrong with an upstream server";

                default:
                    return string.Empty;
            }
        }
    }
}
