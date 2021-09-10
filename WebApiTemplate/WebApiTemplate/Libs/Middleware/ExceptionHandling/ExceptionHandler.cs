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

        public async Task HandleExceptionAsync(HttpContext context, ApiError error, HttpStatusCode httpStatusCode)
        {
            context.Response.ContentType = HttpResponseContentType.JSON;
            context.Response.StatusCode = (int)httpStatusCode;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
}
