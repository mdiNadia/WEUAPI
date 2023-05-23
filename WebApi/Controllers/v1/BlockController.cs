using Application.Features.Blocks.Commands;
using Application.Features.Blocks.Queries;
using Application.Features.Profile.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class BlockController : BaseApiController
    {
        private readonly IUriService _uriService;

        public BlockController(IUriService uriService)
        {
            _uriService = uriService;
        }
        [HttpPost("{username}")]
        public async Task<ActionResult<Unit>> Block(string username)
        {
            return await Mediator.Send(new AddBlockUser.AddBlockUserCommand { Username = username });
        }

        [HttpDelete("{username}")]
        public async Task<ActionResult<Unit>> Unblock(string username)
        {
            return await Mediator.Send(new DeleteBlockedUser.DeleteBlockedUserCommand { Username = username });
        }
        //نمایش لیستی از تمام کاربران بلاک شده ی یک یوزر
        [HttpGet]
        public async Task<ActionResult<List<ProfileDto>>> GetBlockedUsersByUsername([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new ListBlockedUsersByUsername.ListBlockedUsersByUsernameQuery(filter));
            var totalRecords = await Mediator.Send(new BlockedUsersByUsernameCount.BlockedUsersByUsernameCountQuery());
            var pagedReponse = PaginationHelper.CreatePagedReponse<ProfileDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);

        }
        //ادمین نمایش لیستی از تمام کاربران بلاک شده
        [HttpGet("blackList")]
        public async Task<ActionResult<List<BlockedDto>>> GetAllBlockedUsers([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new ListBlockedUsers.ListBlockedUsersQuery(filter));
            var totalRecords = await Mediator.Send(new BlockedUsersCount.BlockedUsersCountQuery());
            var pagedReponse = PaginationHelper.CreatePagedReponse<BlockedDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
    }
}
