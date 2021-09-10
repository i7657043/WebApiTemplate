using System;
using System.Net;

namespace WebApiTemplate.Libs
{
    public class UpstreamHttpRequestException : Exception
    {
        public HttpStatusCode UpstreamHttpResponseStatusCode { get; }
        public string TargetUrl { get; }
        public InnerError InnerError { get; }

        public UpstreamHttpRequestException(HttpStatusCode upstreamHttpStatusCode, string targetUrl, InnerError innerError = null)
        {
            UpstreamHttpResponseStatusCode = upstreamHttpStatusCode;
            TargetUrl = targetUrl;
            InnerError = innerError;
        }
    }
}
