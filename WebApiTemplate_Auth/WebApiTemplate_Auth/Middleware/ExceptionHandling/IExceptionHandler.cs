using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebApiTemplate_Auth
{
    public interface IExceptionHandler
    {
        Task HandleCustomHttpRequestExceptionAsync(HttpContext context, CustomHttpRequestException exception);
        Task HandleExceptionAsync(HttpContext context, Exception exception);
    }
}
