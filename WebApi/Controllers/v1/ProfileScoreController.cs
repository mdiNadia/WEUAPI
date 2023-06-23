using Application.Features.Profile.Queries;
using Application.Features.ProfileScore.Commands;
using Application.Features.ProfileScore.Queries;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProfileScoreController : BaseApiController
    {
        private readonly IUriService _uriService;

        public ProfileScoreController(IUriService uriService)
        {
            this._uriService = uriService;
        }
        /// <summary>
        ///  Creates a New ProfileScore.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProfileScore command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Updates the ProfileScore Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateProfileScore command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets all ProfileScores.
        /// </summary>
        /// <returns></returns>
        /// [HttpGet]
        [HttpGet("GetAll")]
        public async Task<object> GetAll(DataSourceLoadOptions loadOptions)
        {
            var result = await Mediator.Send(new GetAllProfileScores());
            return DataSourceLoader.Load(result, loadOptions);
        }


        /// <summary>
        /// Gets ProfileScore Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Language = await Mediator.Send(new GetProfileScoreById { Id = id });
            return Ok(new Response<GetProfileScoreDto>(Language));
        }

        /// <summary>
        /// Deletes ProfileScore Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteProfileScoreById { Id = id }));
        }
    }
}
