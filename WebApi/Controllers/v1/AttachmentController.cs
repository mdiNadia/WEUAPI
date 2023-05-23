using Application.Features.Attachment.Commands;
using Application.Features.Attachment.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    public class AttachmentController : BaseApiController
    {
        private readonly IUriService _uriService;

        public AttachmentController(IUriService uriService)
        {
            _uriService = uriService;
        }

        /// <summary>
        /// Creates a New Attachment.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAttachment command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets all Attachments with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllAttachments(filter));
            var totalRecords = await Mediator.Send(new GetAllCountAttachments());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetAttachmentDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        /// <summary>
        /// Gets Attachment Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Attachment = await Mediator.Send(new GetAttachmentById { Id = id });
            return Ok(new Response<GetAttachmentDto>(Attachment));
        }
        /// <summary>
        /// Deletes Attachment Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteAttachmentById { Id = id }));
        }
        /// <summary>
        /// Updates the Attachment Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateAttachment command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
