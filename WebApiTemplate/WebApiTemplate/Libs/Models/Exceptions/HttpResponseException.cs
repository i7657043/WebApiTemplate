using System;
using System.Net;

namespace WebApiTemplate.Libs
{
    public class HttpResponseException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }
        public InnerError InnerError { get; }

        public HttpResponseException(HttpStatusCode httpStatusCode, InnerError innerError = null)
        {
            HttpStatusCode = httpStatusCode;
            InnerError = innerError;
        }
    }
}
