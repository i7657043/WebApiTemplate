using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
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
                                
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized || httpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                    throw new HttpResponseException((HttpStatusCode)httpContext.Response.StatusCode);

                //For Route URI not found
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    ApiError errorResponse = new ApiError("The route URI you requested does not exist", httpContext.Request.Path);

                    await _exceptionHandler.HandleExceptionAsync(httpContext, errorResponse, HttpStatusCode.NotFound);
                }
            }
            catch (UpstreamHttpRequestException ex) //For inter-api communication exceptions (Expected behaviour)
            {
                ApiError errorResponse = new ApiError(HttpStatusCode.BadGateway, httpContext.Request.Path, new InnerError(new List<string> 
                {
                    $"Problem contacting external web resource: {ex.TargetUrl} - external HTTP status code: {(int)ex.UpstreamHttpResponseStatusCode}"
                }));

                _logger.LogError("Upstream HTTP Request Exception. Exception: {@Exception}. Response: {@Response}", ex, errorResponse);

                await _exceptionHandler.HandleExceptionAsync(httpContext, errorResponse, HttpStatusCode.BadGateway);
            }
            catch (HttpResponseException ex) //For 4XX and 5XX responses (Expected behaviour)
            {
                ApiError errorResponse = new ApiError(ex.HttpStatusCode, httpContext.Request.Path, ex.InnerError);

                _logger.LogInformation("HTTP Response Exception. HTTP Status Code: {httpStatusCode}. Response: {@Response}", ex.HttpStatusCode, errorResponse);

                await _exceptionHandler.HandleExceptionAsync(httpContext, errorResponse, ex.HttpStatusCode);
            }
            catch (Exception ex) //For everything else (Not Expected behaviour)
            {
                ApiError errorResponse = new ApiError(HttpStatusCode.InternalServerError, httpContext.Request.Path);

                _logger.LogCritical("Unexpected Exception: {@Exception}. Response: {@Response}", ex, errorResponse);

                await _exceptionHandler.HandleExceptionAsync(httpContext, errorResponse, HttpStatusCode.InternalServerError);
            }
        }
    }
}
