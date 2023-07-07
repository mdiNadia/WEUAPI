using Application.Features.City.Commands;
using Application.Features.City.Queries;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CityController : BaseApiController
    {
        private readonly IUriService _uriService;

        public CityController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        /// Creates a New City.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateCity command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets all Cities with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<object> GetAll(DataSourceLoadOptions loadOptions)
        {
            var result = await Mediator.Send(new GetAllCities());
            return DataSourceLoader.Load(result, loadOptions);
        }
        /// <summary>
        /// Gets City Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var City = await Mediator.Send(new GetCityById { Id = id });
            return Ok(new Response<GetCityDto>(City));
        }
        /// <summary>
        /// Deletes City Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCityById { Id = id }));
        }
        /// <summary>
        /// Updates the City Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, UpdateCity command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets All Cities by Provinces Id Without Paging
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCities([FromQuery] string ids)
        {
            var des = JsonSerializer.Deserialize<List<int>>(ids);
            return Ok(await Mediator.Send(new Cities { ids = des }));

        }

        /// <summary>
        /// Gets All Cities Without Paging
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCities()
        {
            return Ok(await Mediator.Send(new GetAll()));
        }

        [HttpGet, AllowAnonymousAttribute]
        [Route("GetLookup")]
        public async Task<object> GetByProvinceIds(DataSourceLoadOptions loadOptions, string provincesIds)
        {
            var responseResult = await Mediator.Send(new GetAll());
            return DataSourceLoader.Load(responseResult, loadOptions);
        }
    }
}
