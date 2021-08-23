using System;
using System.Net;

namespace WebApiTemplate_Auth
{
    public class CustomException : Exception
    {
        public InnerError InnerError { get; }
        public string TargetUrl { get; }
        public HttpStatusCode HttpStatusCode { get; }

        public CustomException(HttpStatusCode httpStatusCode, string targetUrl, InnerError innerError = null)
        {
            HttpStatusCode = httpStatusCode;
            TargetUrl = targetUrl;
            InnerError = innerError;
        }
    }
}
