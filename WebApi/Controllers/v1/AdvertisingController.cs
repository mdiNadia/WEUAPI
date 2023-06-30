using Application.Dtos.Advertising;
using Application.Features.AdCategory.Queries;
using Application.Features.Advertising.Commands;
using Application.Features.Advertising.Queries;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class AdvertisingController : BaseApiController
    {
        private readonly IUriService _uriService;

        public AdvertisingController(IUriService uriService)
        {
            this._uriService = uriService;
        }
        /// <summary>
        /// Creates a New Advertising.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAdvertising command)
        {
            return Ok(await Mediator.Send(command));

        }
        /// <summary>
        /// Updates the Advertising Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateAdvertising command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Deletes Advertising Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteAdvertisingById { Id = id }));
        }
        /// <summary>
        /// Gets all Advertisings with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<object> GetAll(DataSourceLoadOptions loadOptions)
        {
            var result = await Mediator.Send(new GetAllAdvertisings());
            return DataSourceLoader.Load(result, loadOptions);
        }
        /// <summary>
        /// Gets Advertising Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Advertising = await Mediator.Send(new GetAdvertisingById { Id = id });
            Response<GetAdvertisingDto> res = new Response<GetAdvertisingDto>(Advertising);
            return Ok(res);
        }
        /// <summary>
        /// Gets Advertising Entity by Key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("A/{key}")]
        public async Task<IActionResult> GetByKey(string key)
        {
            var adv = await Mediator.Send(new GetAdvertisingByShortKey { Key = key });
            return Ok(new Response<GetAdvertisingDto>(adv));
        }



        /////////////////////////////////////////

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAdvertisings()
        {
            var result = await Mediator.Send(new Advertisings());
            return Ok(result);
        }



    }
}
