using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebApiTemplate.Libs;

namespace WebApiTemplate
{
    [Route("[controller]")]
    public class ExampleApiController : Controller
    {
        #region vars and constructor
        private readonly IExampleApiProvider _apiProvider;

        public ExampleApiController(IExampleApiProvider apiProvider)
        {
            _apiProvider = apiProvider;
        }
        #endregion

        /// <summary>
        /// Get all Anatomy Resources
        /// </summary>
        /// <param name="anatomyId"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(List<string>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity, Type = typeof(ApiError))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(int id = 0)
        {
            List<string> response = await _apiProvider.GetAsync(id);

            return new OkObjectResult(response);

            //Use below for Creation (Insert) responses
            //return new CreatedResult(ControllerContext.GetCreatedAtRoute(id.ToString()), response);
        }        
    }
}
