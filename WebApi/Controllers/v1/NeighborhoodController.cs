
using Application.Dtos.Neighbourhood;
using Application.Features.Neighbourhood.Commands;
using Application.Features.Neighbourhood.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class NeighborhoodController : BaseApiController
    {
        private readonly IUriService _uriService;

        public NeighborhoodController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        /// Creates a New Neighborhood.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateNeighbourhood command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets all Neighborhoods with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllNeighbourhoods(filter));
            var totalRecords = await Mediator.Send(new GetAllCountNeighbourhoods());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetNeighbourhoodDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        /// <summary>
        /// Gets Neighborhood Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Neighborhood = await Mediator.Send(new GetNeighbourhoodById { Id = id });
            return Ok(new Response<GetNeighbourhoodDto>(Neighborhood));
        }
        /// <summary>
        /// Deletes Neighborhood Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new GetNeighbourhoodById { Id = id }));
        }
        /// <summary>
        /// Updates the Neighborhood Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, UpdateNeighbourhood command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets All Neighborhoods By Cities Id Without Paging
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNeighborhoods([FromQuery] string ids)
        {
            var des = JsonSerializer.Deserialize<List<int>>(ids);
            return Ok(await Mediator.Send(new Neighborhoods { Ids = des }));
        }

        /// <summary>
        /// Gets All Neighborhoods Without Paging
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllNeighborhoods()
        {
            return Ok(await Mediator.Send(new Application.Features.Neighborhood.Queries.GetAll()));
        }
    }
}
