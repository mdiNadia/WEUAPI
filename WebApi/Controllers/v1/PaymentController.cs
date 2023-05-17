
using Application.Features.Transaction.Commands;
using Application.Features.Value.Commands;
using Application.Features.Payment.Commands;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PaymentController : BaseApiController
    {
        private readonly IUriService _uriService;

        public PaymentController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        /// Creates a New Payment.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Payment command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}
