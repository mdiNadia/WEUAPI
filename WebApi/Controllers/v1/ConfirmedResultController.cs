using Application.Dtos.ConfirmedResult;
using Application.Features.ConfirmedResult.Commands;
using Application.Features.ConfirmedResult.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Authorize]
    public class ConfirmedResultController : BaseApiController
    {
        private readonly IUriService _uriService;

        public ConfirmedResultController(IUriService uriService)
        {
            this._uriService = uriService;
        }
        /// <summary>
        /// Creates a New ConfirmedResult.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateConfirmedResult command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        /// <summary>
        /// Deletes ConfirmedResult Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteConfirmedResultById { Id = id }));
        }
        /// <summary>
        /// Gets all ConfirmedResultResults with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllConfirmedResults(filter));
            var totalRecords = await Mediator.Send(new GetAllCountConfirmedResults());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetConfirmedResultDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        /// <summary>
        /// Gets ConfirmedResult Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ConfirmedResult = await Mediator.Send(new GetConfirmedResultById { Id = id });
            Response<GetConfirmedResultDto> res = new Response<GetConfirmedResultDto>(ConfirmedResult);
            return Ok(res);
        }
        /// <summary>
        /// Gets ConfirmedResult Entity by Key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("A/{key}")]
        public async Task<IActionResult> GetByKey(string key)
        {
            var adv = await Mediator.Send(new GetConfirmedResultByShortKey { Key = key });
            return Ok(new Response<GetConfirmedResultDto>(adv));
        }

    }
}

