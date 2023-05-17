using Application.Dtos.ConfirmedResult;
using Application.Features.Home.Queries;
using Application.Features.Profile.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class HomeController : BaseApiController
    {
        private readonly IUriService _uriService;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(
            IUriService uriService,
            IStringLocalizer<HomeController> localizer

            )
        {
            _uriService = uriService;
            this._localizer = localizer;
        }
        [HttpGet]
        public async Task<IActionResult> Home([FromQuery] PaginationFilter filter)
        {
            var lan = CultureInfo.CurrentCulture;
            var res = lan + "__" + $"{_localizer["سلام"]}";
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetData(filter));
            var totalRecords = await Mediator.Send(new GetCountData());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetConfirmedResultDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }



    }
}
