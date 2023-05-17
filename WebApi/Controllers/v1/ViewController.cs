using Application.Features.View.Commands;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]

    public class ViewController : BaseApiController
    {
        private readonly IUriService _uriService;

        public ViewController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        ///Increases the number of ad views.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> View(ViewCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
