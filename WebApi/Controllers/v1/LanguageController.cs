
using Application.Dtos.Language;
using Application.Features.AdCategory.Queries;
using Application.Features.Language.Commands;
using Application.Features.Language.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Authorize]
    public class LanguageController : BaseApiController
    {
        private readonly IUriService _uriService;

        public LanguageController(IUriService uriService)
        {
            _uriService = uriService;
        }

        /// <summary>
        /// Creates a New Language.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateLanguage command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        /// <summary>
        /// Gets all Languages with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllLanguages(filter));
            var totalRecords = await Mediator.Send(new GetAllCountLanguages());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetLanguageDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        /// <summary>
        /// Gets Language Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Language = await Mediator.Send(new GetLanguageById { Id = id });
            return Ok(new Response<GetLanguageDto>(Language));
        }
        /// <summary>
        /// Deletes Language Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteLanguageById { Id = id });
            return Ok(result);
        }
        /// <summary>
        /// Updates the Language Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateLanguage command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLanguages()
        {
            var result = await Mediator.Send(new Languages());
            return Ok(result);
        }
    }
}
