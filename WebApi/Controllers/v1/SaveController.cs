using Application.Features.Advertising.Commands;
using Application.Features.ConfirmedResult.Queries;
using Application.Features.User.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SaveController : BaseApiController
    {
        private readonly IUriService _uriService;

        public SaveController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        ///Adds a new ad to the Profile's saved ads.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save(SaveCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// List Of User SavedAds
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> ListOfUserSavedAds([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new ListOfSavedAd(filter));
            var totalRecords = await Mediator.Send(new ListOfCountSavedAd());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetConfirmedResultDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);

        }
    }
}
