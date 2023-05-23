using Application.Features.Currency.Commands;
using Application.Features.Currency.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CurrencyController : BaseApiController
    {
        private readonly IUriService _uriService;

        public CurrencyController(IUriService uriService)
        {
            _uriService = uriService;
        }

        /// <summary>
        /// Creates a New Currency.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateCurrency command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets all Currencies with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllCurrencies(filter));
            var totalRecords = await Mediator.Send(new GetAllCountCurrencies());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetCurrencyDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        /// <summary>
        /// Gets Currency Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Currency = await Mediator.Send(new GetCurrencyById { Id = id });
            return Ok(new Response<GetCurrencyDto>(Currency));
        }
        /// <summary>
        /// Deletes Currency Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCurrencyById { Id = id }));
        }
        /// <summary>
        /// Updates the Currency Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, UpdateCurrency command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets All Currencies Without Paging
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCurrencies()
        {
            var result = await Mediator.Send(new Currencies());
            return Ok(result);
        }

    }
}
