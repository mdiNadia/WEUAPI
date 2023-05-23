using Application.Features.Country.Commands;
using Application.Features.Country.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CountryController : BaseApiController
    {
        private readonly IUriService _uriService;

        public CountryController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        /// Creates a New Country.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateCountry command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets all Countries with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllCountries(filter));
            var totalRecords = await Mediator.Send(new GetAllCountCountries());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetCountryDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        /// <summary>
        /// Gets Country Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Country = await Mediator.Send(new GetCountryById { Id = id });
            return Ok(new Response<GetCountryDto>(Country));
        }
        /// <summary>
        /// Deletes Country Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteCountryById { Id = id });
            return Ok(result);
        }
        /// <summary>
        /// Updates the Country Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, UpdateCountry command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }


        /// <summary>
        /// Gets All Countries Without Paging
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCountries()
        {
            var result = await Mediator.Send(new Countries());
            return Ok(result);
        }

        /// <summary>
        /// Gets All Country's provinces, cities and neighbourhoods
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetWhole(int id, [FromQuery] PaginationFilter filter)
        {
            var result = await Mediator.Send(new GetWhole(filter) { Id = id });
            return Ok(result);
        }

    }
}
