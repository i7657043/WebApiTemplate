using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebApiTemplate.Libs;

namespace WebApiTemplate
{
    [Route("[controller]")]
    public class WordsController : Controller
    {
        #region vars and constructor
        private readonly IWordsProvider _apiProvider;

        public WordsController(IWordsProvider apiProvider)
        {
            _apiProvider = apiProvider;
        }
        #endregion

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<string>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
        [CustomModelStateValidation]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            List<string> response = await _apiProvider.GetAsync(id);

            return new OkObjectResult(response);
        }

        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(List<string>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiError))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity, Type = typeof(ApiError))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ApiError))]
        [CustomModelStateValidation]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddAsync([FromBody] Word word)
        {
            Word response = await _apiProvider.AddAsync(word);

            return new CreatedResult(ControllerContext.GetCreatedAtRoute(word.Id.ToString()), response);
        }
    }
}
