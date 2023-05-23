using Application.Features.Transaction.Commands;
using Application.Features.Transaction.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers.v1
{

    public class TransactionController : BaseApiController
    {
        private readonly IUriService _uriService;

        public TransactionController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        /// Creates a New Transaction.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateTransaction command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        ///Logic Deletes Transaction Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteTransactionById { Id = id }));
        }
        /// <summary>
        /// Gets all Transaction with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllTransactions(filter));
            var totalRecords = await Mediator.Send(new GetAllCountTransactions());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetTransactionDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        /// <summary>
        /// Gets all Transaction with paging filter and WalletId.
        /// </summary>
        ///       
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllByWalletId(int id, [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllTransactionsByWalletId(filter, id));
            var totalRecords = await Mediator.Send(new GetAllCountTransactionsByWalletId { Id = id });
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetTransactionDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

    }
}
