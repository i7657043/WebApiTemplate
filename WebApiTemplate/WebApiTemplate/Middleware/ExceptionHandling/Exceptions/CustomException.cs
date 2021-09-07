using System;
using System.Net;

namespace WebApiTemplate
{
    public class CustomException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }
        public InnerError InnerError { get; }

        public CustomException(HttpStatusCode httpStatusCode, InnerError innerError = null)
        {
            HttpStatusCode = httpStatusCode;
            InnerError = innerError;
        }
    }
}
