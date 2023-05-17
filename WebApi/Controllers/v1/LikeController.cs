using Application.Features.Like.Commands;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class LikeController : BaseApiController
    {
        private readonly IUriService _uriService;

        public LikeController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        ///Increase Number of Like a Advertising.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Like(LikeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
