using Application.Features.LikeComment.Commands;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class LikeCommentController : BaseApiController
    {
        private readonly IUriService _uriService;

        public LikeCommentController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        ///Increase Number of LikeComment a Comment.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LikeComment(LikeCommentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
