using Application.Features.ConfirmedResult.Queries;
using Application.Features.Language.Queries;
using Application.Features.Profile.Commands;
using Application.Features.Profile.Dtos;
using Application.Features.Profile.Queries;
using Application.Features.ProfileScore.Queries;
using Application.Features.Wallet.Queries;
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
    public class ProfileController : BaseApiController
    {
        private readonly IUriService _uriService;

        public ProfileController(IUriService uriService)
        {
            this._uriService = uriService;
        }
        /// <summary>
        /// Creates a New Profile.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateProfile command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Updates the Profile Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromForm] UpdateProfile command)
        {
         
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets all Profils.
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [HttpGet("GetAll")]
        public async Task<object> GetAll(DataSourceLoadOptions loadOptions)
        {
            var result = await Mediator.Send(new GetAllProfileScores());
            return DataSourceLoader.Load(result, loadOptions);
        }


        /// <summary>
        /// Gets Profile Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var User = await Mediator.Send(new GetProfileById { Id = id });
            return Ok(new Response<GetProfileDto>(User));
        }


        /// <summary>
        /// Gets Profile Entity by UserName.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("[action]/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var User = await Mediator.Send(new GetProfileByUsername { Username = username });
            return Ok(new Response<Tuple<bool, GetProfileDto, IEnumerable<GetConfirmedResultDto>>>(User));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProfileInfo()
        {
            var profileInfo = await Mediator.Send(new GetProfileInformation());
            return Ok(new Response<Tuple<bool, GetProfileDto, IEnumerable<GetConfirmedResultDto>,GetWalletDto>>(profileInfo));
        }
    }
}
