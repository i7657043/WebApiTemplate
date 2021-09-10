using System;
using System.Net;

namespace WebApiTemplate.Libs
{
    public class RouteNotFoundException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }
        public InnerError InnerError { get; }

        public RouteNotFoundException(InnerError innerError = null)
        {
            HttpStatusCode = HttpStatusCode.NotFound;
            InnerError = innerError;
        }
    }
}
