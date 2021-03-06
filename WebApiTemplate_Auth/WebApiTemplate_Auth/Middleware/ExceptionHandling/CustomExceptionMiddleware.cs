using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebApiTemplate_Auth
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
            catch (UpstreamHttpRequestException ex) //For inter-api communication exceptions (Expected behaviour)
            {
                _logger.LogError("Custom HTTP Request Error. Exception: {ex}", ex.TargetUrl, ex);

                await _exceptionHandler.HandleUpstreamHttpRequestExceptionAsync(httpContext, ex);
            }
            catch (CustomException ex) //Other exceptions (Expected behaviour)
            {
                _logger.LogError("Custom Error");

                await _exceptionHandler.HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex) //For everything else (Not Expected behaviour)
            {
                _logger.LogCritical("Unexpected Exception: {@Exception}", ex);

                await _exceptionHandler.HandleExceptionAsync(httpContext, ex);
            }
        }
    }
}
