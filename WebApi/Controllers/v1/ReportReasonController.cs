using Application.Features.ReportReason.Commands;
using Application.Features.ReportReason.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ReportReasonController : BaseApiController
    {
        private readonly IUriService _uriService;

        public ReportReasonController(IUriService uriService)
        {
            _uriService = uriService;
        }

        /// <summary>
        /// Creates a New ReportReason.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateReportReason command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Updates the ReportReason Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, UpdateReportReason command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Deletes ReportReason Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteReportReasonById { Id = id }));
        }

        /// <summary>
        /// Gets ReportReason Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Comment = await Mediator.Send(new GetReportReasonById { Id = id });
            return Ok(new Response<GetReportReasonDto>(Comment));
        }

        /// <summary>
        /// Gets all ReportReason with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllReportReasons(filter));
            var totalRecords = await Mediator.Send(new GetAllCountReportReasons());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetReportReasonDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetReportReasons()
        {
            var result = await Mediator.Send(new ReportReasons());
            return Ok(result);
        }
    }
}
