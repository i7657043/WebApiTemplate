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
                                
                switch (httpContext.Response.StatusCode)
                {
                    case (int)HttpStatusCode.Unauthorized:
                    case (int)HttpStatusCode.Forbidden:
                        throw new HttpResponseException((HttpStatusCode)httpContext.Response.StatusCode);

                    case (int)HttpStatusCode.NotFound:
                        throw new RouteNotFoundException();
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
            catch (RouteNotFoundException ex) //For route URI not found (Not Expected behaviour)
            {
                ApiError errorResponse = new ApiError("The requested route URI does not exist", httpContext.Request.Path);

                _logger.LogInformation("Requested route URI: {requestUri} does not exist", httpContext.Request.Path);

                await _exceptionHandler.HandleExceptionAsync(httpContext, errorResponse, HttpStatusCode.NotFound);
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
