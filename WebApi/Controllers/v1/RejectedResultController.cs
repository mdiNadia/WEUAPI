using Application.Features.RejectedResult.Commands;
using Application.Features.RejectedResult.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Authorize]
    public class RejectedResultController : BaseApiController
    {
        private readonly IUriService _uriService;

        public RejectedResultController(IUriService uriService)
        {
            this._uriService = uriService;
        }
        /// <summary>
        /// Creates a New RejectedResult.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateRejectedResult command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        /// <summary>
        /// Gets all RejectedResult with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllRejectedResults(filter));
            var totalRecords = await Mediator.Send(new GetAllCountRejectedResults());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetRejectedResultDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        /// <summary>
        /// Gets RejectedResult Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var RejectedResult = await Mediator.Send(new GetRejectedResultById { Id = id });
            Response<GetRejectedResultDto> res = new Response<GetRejectedResultDto>(RejectedResult);
            return Ok(res);
        }
    }
}

