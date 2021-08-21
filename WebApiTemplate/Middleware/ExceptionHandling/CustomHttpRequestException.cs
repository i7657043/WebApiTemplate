using System;
using System.Net;

namespace WebApiTemplate
{
    public class CustomHttpRequestException : Exception
    {
        public InnerError InnerError { get; }
        public string TargetUrl { get; }
        public HttpStatusCode HttpStatusCode { get; }

        public CustomHttpRequestException(HttpStatusCode httpStatusCode, string targetUrl, InnerError innerError = null)
        {
            HttpStatusCode = httpStatusCode;
            TargetUrl = targetUrl;
            InnerError = innerError;
        }
    }
}
