using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApiTemplate.Libs
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
            context.Response.StatusCode = (int)HttpStatusCode.BadGateway;

            ApiError errorResponse = new ApiError()
            {
                Target = context.Request.Path,
                Message = $"Problem contacting external web resource: {exception.TargetUrl} (HTTP Status Code: {(int)exception.UpstreamHttpResponseStatusCode})"
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }

        public async Task HandleHttpResponseExceptionAsync(HttpContext context, HttpResponseException exception)
        {
            context.Response.ContentType = HttpResponseContentType.JSON;
            context.Response.StatusCode = (int)exception.HttpStatusCode;

            ApiError errorResponse = new ApiError(exception.HttpStatusCode, $"{context.Request.Method}: {context.Request.Path}");

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = HttpResponseContentType.JSON;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ApiError errorResponse = new ApiError(HttpStatusCode.InternalServerError, $"{context.Request.Method}: {context.Request.Path}");

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}
