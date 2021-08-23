using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebApiTemplate
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionHandler _exceptionHandler;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, IExceptionHandler exceptionHandler, ILogger<CustomExceptionMiddleware> logger)
        {
            _exceptionHandler = exceptionHandler;
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException ex) //For inter-api communication exceptions (Not Expected behaviour)
            {
                _logger.LogError("Custom HTTP Request Error: Problem contacting {TargetUrl}", ex.TargetUrl);

                await _exceptionHandler.HandleCustomHttpRequestExceptionAsync(httpContext, ex);
            }
            catch (Exception ex) //For everything else (Not Expected behaviour)
            {
                _logger.LogCritical("Unexpected Exception: {@Exception}", ex);

                await _exceptionHandler.HandleExceptionAsync(httpContext, ex);
            }
        }
    }
}
