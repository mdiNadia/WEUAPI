using Application.Dtos.Comment;
using Application.Features.Comment.Commands;
using Application.Features.Comment.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    public class CommentController : BaseApiController
    {
        private readonly IUriService _uriService;

        public CommentController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        /// Creates a New Comment.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateComment command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets all Comments with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllComments(filter));
            var totalRecords = await Mediator.Send(new GetAllCountComments());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetCommentDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        /// <summary>
        /// Gets Comment Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Comment = await Mediator.Send(new GetCommentById { Id = id });
            return Ok(new Response<GetCommentDto>(Comment));
        }
        /// <summary>
        /// Deletes Comment Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCommentById { Id = id }));
        }
        /// <summary>
        /// Updates the Comment Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, UpdateComment command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAllByAdId(int id)
        {
            var Comment = await Mediator.Send(new GetAllCommentsByAdId { Id = id });
            return Ok(Comment);
        }
    }
}
