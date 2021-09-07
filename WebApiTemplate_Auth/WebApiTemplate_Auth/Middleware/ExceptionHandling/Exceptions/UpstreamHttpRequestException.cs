using System;
using System.Net;

namespace WebApiTemplate_Auth
{
    public class UpstreamHttpRequestException : Exception
    {
        public HttpStatusCode UpstreamHttpResponseStatusCode { get; }
        public string TargetUrl { get; }
        public InnerError InnerError { get; }

        public UpstreamHttpRequestException(HttpStatusCode httpStatusCode, string targetUrl, InnerError innerError = null)
        {
            UpstreamHttpResponseStatusCode = httpStatusCode;
            TargetUrl = targetUrl;
            InnerError = innerError;
        }
    }
}
