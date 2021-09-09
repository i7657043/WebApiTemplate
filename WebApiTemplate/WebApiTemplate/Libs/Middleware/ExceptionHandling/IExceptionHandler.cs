using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebApiTemplate.Libs
{
    public interface IExceptionHandler
    {
        Task HandleUpstreamHttpRequestExceptionAsync(HttpContext context, UpstreamHttpRequestException exception);
        Task HandleHttpResponseExceptionAsync(HttpContext context, HttpResponseException exception);
        Task HandleExceptionAsync(HttpContext context, Exception exception);
    }
}
