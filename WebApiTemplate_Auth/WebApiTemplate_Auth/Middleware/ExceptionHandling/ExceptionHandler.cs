using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using WebApiTemplate_Auth;

namespace WebApiTemplate_Auth
{
    public class ExceptionHandler : IExceptionHandler
    {
#pragma warning disable CS0618
        private readonly IHostingEnvironment _environment;
#pragma warning restore CS0618

#pragma warning disable CS0618
        public ExceptionHandler(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public async Task HandleUpstreamHttpRequestExceptionAsync(HttpContext context, UpstreamHttpRequestException exception)
        {
            context.Response.ContentType = HttpResponseContentType.JSON;
            context.Response.StatusCode =  (int) HttpStatusCode.BadGateway;

            ApiError errorResponse = new ApiError
            {
                Target = context.Request.Path,
                Message = $"Problem contacting external web resource: {exception.TargetUrl} - external HTTP Status Code {(int)exception.UpstreamHttpResponseStatusCode}"
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }

        public async Task HandleCustomExceptionAsync(HttpContext context, CustomException exception)
        {
            context.Response.ContentType = HttpResponseContentType.JSON;
            context.Response.StatusCode = exception.HttpStatusCode != 0 
                ? (int)exception.HttpStatusCode 
                : (int)HttpStatusCode.InternalServerError;

            ApiError errorResponse = new ApiError
            {
                Target = context.Request.Path,
                Message = "Sorry, Something went wrong"
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = HttpResponseContentType.JSON;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ApiError errorResponse = new ApiError
            {
                Target = context.Request.Path,
                Message = "Sorry, something went wrong"
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}
