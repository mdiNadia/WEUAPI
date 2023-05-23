using Application.Features.AdCategoryCost.Commands;
using Application.Features.AdCategoryCost.Queries;
using Application.Services.UserAccessor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.SharedResources;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class AdCategoryCostController : BaseApiController
    {
        private readonly IUriService _uriService;
        private readonly IUserAccessor _userAccessor;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IStringLocalizer<DataAnnotationTranslate> _dataAnnotLocalizer;

        public AdCategoryCostController(IUriService uriService,
            IUserAccessor userAccessor
,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IStringLocalizer<DataAnnotationTranslate> dataAnnotLocalizer

            )
        {
            _uriService = uriService;
            this._userAccessor = userAccessor;
            _sharedLocalizer = sharedLocalizer;
            _dataAnnotLocalizer = dataAnnotLocalizer;
        }
        /// <summary>
        /// Creates a New AdCategoryCost.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateAdCategoryCost command)
        {

            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets all AdCategories with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllAdCategoryCosts(filter));
            var totalRecords = await Mediator.Send(new GetAllCountAdCategoryCosts());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetAdCatCostDto>(pagedData, filter, totalRecords, _uriService, route);


            return Ok(pagedReponse);
        }
        /// <summary>
        /// Gets AdCategoryCost Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var AdCategoryCost = await Mediator.Send(new GetAdCategoryCostById { Id = id });
            return Ok(new Response<GetAdCatCostDto>(AdCategoryCost));
        }
        /// <summary>
        /// Deletes AdCategoryCost Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteAdCategoryCostById { Id = id }));
        }
        /// <summary>
        /// Updates the AdCategoryCost Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, UpdateAdCategoryCost command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Gets AdCategoryCost By AdCategoryId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]")]

        public async Task<IActionResult> GetByCategoryId(int id)
        {
            var AdCategoryCost = await Mediator.Send(new GetAdCategoryCostByCategoryId { Id = id });
            return Ok(new Response<GetAdCatCostDto>(AdCategoryCost));
        }
        /// <summary>
        /// Handle IsActive Cost By CatCostId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> HandleCost(int id)
        {
            return Ok(await Mediator.Send(new HandleCost { Id = id }));
        }
    }
}
