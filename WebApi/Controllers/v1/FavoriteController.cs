using Application.Features.Favorite.Commands;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class FavoriteController : BaseApiController
    {
        private readonly IUriService _uriService;

        public FavoriteController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        ///Increase Number of Favorite a Advertising.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Favorite(FavoriteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
