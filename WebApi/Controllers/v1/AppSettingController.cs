
using Application.Dtos.AppSetting;
using Application.Features.AppSetting.Commands;
using Application.Features.AppSetting.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class AppSettingController : BaseApiController
    {
        private readonly IUriService _uriService;

        public AppSettingController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        /// Gets BoostSetting Entity.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAppSetting()
        {
            var BoostSetting = await Mediator.Send(new GetAppSetting());
            return Ok(new Response<GetAppsettingDto>(BoostSetting));
        }
        /// <summary>
        /// Updates the BoostSetting Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, UpdateAppSetting command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

    }
}
