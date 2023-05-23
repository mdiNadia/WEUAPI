using Application.Dtos.Common;
using Application.Features.ConfirmedResult.Queries;
using Application.Features.Explore.Queries;
using Application.Features.Profile.Dtos;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers.v1
{
    public class ExploreController : BaseApiController
    {
        private readonly IUriService _uriService;

        public ExploreController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        /// Gets all Data by queryString and Account/Category.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter, [FromQuery] string q = "", [FromQuery] int by = 0)
        {
            if (by == 0)
            {
                var route = Request.Path.Value;
                var pagedData = await Mediator.Send(new ExploreByProfile(filter) { q = q });
                var totalRecords = await Mediator.Send(new ExploreByProfileCount() { q = q });
                var pagedReponse = PaginationHelper.CreatePagedReponse<GetProfileDto>(pagedData, filter, totalRecords, _uriService, route);
                return Ok(pagedReponse);
            }
            else
            {
                var route = Request.Path.Value;
                var pagedData = await Mediator.Send(new ExploreByCategory(filter) { q = q });
                var totalRecords = await Mediator.Send(new ExploreByCategoryCount() { q = q });
                var pagedReponse = PaginationHelper.CreatePagedReponse<GetNameAndId>(pagedData, filter, totalRecords, _uriService, route);
                return Ok(pagedReponse);
            }

        }
        /// <summary>
        ///Get all ads by CategoryId as string
        ///whenever we select a category its data show by this method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> DataPage([FromQuery] PaginationFilter filter, [FromQuery] string? id)
        {

            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new Explore(filter) { id = id });
            var totalRecords = await Mediator.Send(new ExploreCount() { id = id });
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetConfirmedResultDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
    }
}
