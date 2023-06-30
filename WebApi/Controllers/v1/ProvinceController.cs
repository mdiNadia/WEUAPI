using Application.Features.Province.Commands;
using Application.Features.Province.Queries;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProvinceController : BaseApiController
    {
        private readonly IUriService _uriService;

        public ProvinceController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        /// Creates a New Province.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateProvince command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets all Countries with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<object> GetAll(DataSourceLoadOptions loadOptions)
        {
            var result = await Mediator.Send(new GetAllProvinces());
            return DataSourceLoader.Load(result, loadOptions);
        }

        /// <summary>
        /// Gets Province Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Province = await Mediator.Send(new GetProvinceById { Id = id });
            return Ok(new Response<GetProvinceDto>(Province));
        }
        /// <summary>
        /// Deletes Province Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteProvinceById { Id = id }));
        }
        /// <summary>
        /// Updates the Province Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, UpdateProvince command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }


        /// <summary>
        /// Gets All Provinces By Country Id Without Paging
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProvinces(int id)
        {
            return Ok(await Mediator.Send(new Provinces { Id = id }));
        }

        /// <summary>
        /// Gets All Provinces Without Paging
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymousAttribute]
        [Route("GetLookup")]
        public async Task<object> GetLookup(DataSourceLoadOptions loadOptions)
        {
            var responseResult = await Mediator.Send(new GetAll());
            return DataSourceLoader.Load(responseResult, loadOptions);
        }
    }
}
