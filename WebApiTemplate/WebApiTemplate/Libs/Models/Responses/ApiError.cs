using System.Net;

namespace WebApiTemplate.Libs
{
    public class ApiError
    {
        //One of a server-defined set of error codes
        public int Code { get; set; }

        //A human-readable representation of the error
        public string Message { get; set; }

        //The Target of the error
        public string Target { get; set; }

        public InnerError InnerError { get; set; }

        public ApiError() { }

        public ApiError(HttpStatusCode httpStatusCode, string target = null)
        {
            Target = target;
            Message = GetResponseMessageFromHttpStatusCode(httpStatusCode);
        }

        private string GetResponseMessageFromHttpStatusCode(HttpStatusCode httpStatusCode)
        {
            switch (httpStatusCode)
            {
                case HttpStatusCode.NotFound:
                    return $"The resource at: {Target} could not be found";

                case HttpStatusCode.InternalServerError:
                    return $"Sorry, Something went wrong with the request to: {Target}";

                default:
                    return string.Empty;
            }
        }
    }
}
