using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebApiTemplate.Libs
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
                _logger.LogError("Upstream HTTP Request Exception. Exception: {@Exception}", ex);

                await _exceptionHandler.HandleUpstreamHttpRequestExceptionAsync(httpContext, ex);
            }
            catch (HttpResponseException ex) //For 4XX and 5XX responses (Expected behaviour)
            {
                _logger.LogInformation("HTTP Response Exception. HTTP Status Code: {httpStatusCode}", ex.HttpStatusCode);

                await _exceptionHandler.HandleHttpResponseExceptionAsync(httpContext, ex);
            }
            catch (Exception ex) //For everything else (Not Expected behaviour)
            {
                _logger.LogCritical("Unexpected Exception: {@Exception}", ex);

                await _exceptionHandler.HandleExceptionAsync(httpContext, ex);
            }
        }
    }
}
