using System.Net;

namespace WebApiTemplate.Libs
{
    public class ApiError
    {
        //A human-readable representation of the error
        public string Message { get; set; }

        //The Target of the error
        public string Target { get; set; }

        public InnerError InnerError { get; set; }

        public ApiError() { }

        public ApiError(string message, string target, InnerError innerError = null)
        {
            Message = message;
            Target = target;
            InnerError = innerError;
        }

        public ApiError(HttpStatusCode httpStatusCode, string target, InnerError innerError = null)
        {
            Message = httpStatusCode.GetResponseMessageFromHttpStatusCode();
            Target = target;
            InnerError = innerError;
        }
    }
}
