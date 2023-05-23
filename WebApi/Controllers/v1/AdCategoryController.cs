using Application.Features.AdCategory.Commands;
using Application.Features.AdCategory.Queries;
using Application.Services.UserAccessor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Net;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.SharedResources;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    //[Authorize(Roles ="User")]
    //[Authorize]
    [ApiVersion("1.0")]
    public class AdCategoryController : BaseApiController
    {
        private readonly IUriService _uriService;
        private readonly IUserAccessor _userAccessor;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IStringLocalizer<DataAnnotationTranslate> _dataAnnotLocalizer;
        public AdCategoryController(IUriService uriService,
            IUserAccessor userAccessor,
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
        /// Creates a New AdCategory.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateAdCategory command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets all AdCategories with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<GetAdCategoryDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new GetAllAdCategories(filter));
            var totalRecords = await Mediator.Send(new GetAllCountAdCategories());
            var pagedReponse = PaginationHelper.CreatePagedReponse<GetAdCategoryDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        /// <summary>
        /// Gets AdCategory Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var AdCategory = await Mediator.Send(new GetAdCategoryById { Id = id });
            return Ok(new Response<GetAdCategoryDto>(AdCategory));
        }
        /// <summary>
        /// Deletes AdCategory Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteAdCategoryById { Id = id }));
        }
        /// <summary>
        /// Updates the AdCategory Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, UpdateAdCategory command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets All Categories Without Paging
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await Mediator.Send(new Categories());
            return Ok(result);
        }
    }
}
