using Application.Features.User.Commands;
using Application.Features.User.Queries;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Wrappers;

namespace WebApi.Controllers.v1
{

    [ApiVersion("1.0")]
    public class UserController : BaseApiController
    {
        private readonly IUriService _uriService;

        public UserController(IUriService uriService)
        {
            _uriService = uriService;
        }
        /// <summary>
        /// Creates a New User.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateUser command)
        {
            return Ok(await Mediator.Send(command));
        }
        /// <summary>
        /// Gets all Users with paging filter.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        [ResponseCache(VaryByHeader = "GetAllUser", Duration = 60)]
        public async Task<object> GetAll(DataSourceLoadOptions loadOptions)
        {
            var result = await Mediator.Send(new GetAllUsers());
            return DataSourceLoader.Load(result, loadOptions);
        }

        /// <summary>
        /// Gets User Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var User = await Mediator.Send(new GetUserById { Id = id });
            return Ok(new Response<GetUserDto>(User));
        }
        /// <summary>
        /// Deletes User Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await Mediator.Send(new DeleteUserById { Id = id }));
        }

        /// <summary>
        /// Deletes User Entity based on user number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteUserByNumber(string number)
        {
            return Ok(await Mediator.Send(new DeleteUserByNumber { number = number }));
        }
        /// <summary>
        /// Updates the User Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update(string id, UpdateUser command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await Mediator.Send(new Users());
            return Ok(result);
        }
        /// <summary>
        /// Get All Users Count
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCount()
        {
            var totalRecords = await Mediator.Send(new GetAllCountUsers());
            return Ok(totalRecords);
        }

    }
}
