using System.Net;

namespace WebApiTemplate
{
    public class ProviderResponse<T>
    {
        public ProviderResponse(HttpStatusCode responseCode, T responseData)
        {
            ResponseCode = responseCode;
            ResponseData = responseData;
        }

        public HttpStatusCode ResponseCode { get; set; }
        public T ResponseData { get; set; }
    }
}
