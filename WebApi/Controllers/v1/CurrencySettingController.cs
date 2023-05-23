using Application.Features.CurrencySetting.Commands;
using Application.Features.CurrencySetting.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    public class CurrencySettingController : BaseApiController
    {
        private readonly IUriService _uriService;

        public CurrencySettingController(IUriService uriService)
        {
            _uriService = uriService;
        }

        /// <summary>
        /// Creates a New CurrencySetting.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateCurrencySetting command)
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
            var pagedData = await Mediator.Send(new GetAllCurrencySettings(filter));
            var totalRecords = await Mediator.Send(new GetAllCountCurrencySettings());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetCurrencySettingDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        /// <summary>
        /// Gets CurrencySetting Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var CurrencySetting = await Mediator.Send(new GetCurrencySettingById { Id = id });
            return Ok(new Response<GetCurrencySettingDto>(CurrencySetting));
        }
        /// <summary>
        /// Deletes CurrencySetting Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCurrencySettingById { Id = id }));
        }
        /// <summary>
        /// Updates the CurrencySetting Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, UpdateCurrencySetting command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }


        /// <summary>
        /// Gets all Currencies with paging filter and CurrencyId.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAllCurrencySettingsByCurrencyId(int id, [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllCurrencySettingsByCurrencyId(filter, id));
            var totalRecords = await Mediator.Send(new GetAllCountCurrencySettingsByCurrencyId { id = id });
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetCurrencySettingDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

    }
}
