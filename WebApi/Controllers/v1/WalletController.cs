using Application.Dtos.Wallet;
using Application.Features.Transaction.Commands;
using Application.Features.Value.Commands;
using Application.Features.View.Commands;
using Application.Features.Wallet.Commands;
using Application.Features.Wallet.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class WalletController : BaseApiController
    {
        private readonly IUriService _uriService;

        public WalletController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        /// Creates a New Wallet.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateWallet command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Updates the Wallet Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, UpdateWallet command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets all Wallets.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllWallets(filter));
            var totalRecords = await Mediator.Send(new GetAllCountWallets());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetWalletDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        /// <summary>
        /// Gets Wallet Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var wallet = await Mediator.Send(new GetWalletById { Id = id });
            return Ok(new Response<GetWalletDto>(wallet));
        }

        /// <summary>
        /// Wallet Transactions Start.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("transaction")]
        public async Task<IActionResult> Transaction(StartTransaction command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Get COin Value Start.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("View")]
        public async Task<IActionResult> View(ViewCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Transfer Value Start.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("TransferValue")]
        public async Task<IActionResult> TransferValue(TransferValue command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Charge Value Start.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("ChargeValue")]
        public async Task<IActionResult> ChargeValue(ChargeValue command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
