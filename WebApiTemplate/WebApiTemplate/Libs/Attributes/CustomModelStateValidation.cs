using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Net;
using WebApiTemplate.Libs;

namespace WebApiTemplate.Libs
{
    public class CustomModelStateValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> errorDetails = new List<string>();

                foreach (KeyValuePair<string, ModelStateEntry> modelError in context.ModelState)
                {
                    errorDetails.Add($"Invalid value in request body. JSON Key: [{modelError.Key.Replace("$.", string.Empty)}]");
                }

                throw new HttpResponseException(HttpStatusCode.BadRequest, new InnerError(errorDetails));
            }
        }
    }
}
