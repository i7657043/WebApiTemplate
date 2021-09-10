using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WebApiTemplate.Libs
{
    public interface IExceptionHandler
    {
        Task HandleExceptionAsync(HttpContext context, ApiError error, HttpStatusCode httpStatusCode);
    }
}
