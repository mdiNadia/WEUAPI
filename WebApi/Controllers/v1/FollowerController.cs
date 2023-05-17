using Application.Dtos.Profile;
using Application.Features.AdCategory.Queries;
using Application.Features.Followers.Queries;
using Application.Followers;
using Auth0.ManagementApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace WebApi.Controllers.v1
{

    [ApiVersion("1.0")]
    public class FollowerController : BaseApiController
    {
        private readonly IUriService _uriService;

        public FollowerController(IUriService uriService)
        {
            _uriService = uriService;
        }
        [HttpPost("{username}/follow")]
        public async Task<ActionResult<Unit>> Follow(string username)
        {
            return await Mediator.Send(new AddFollower.AddFollowerCommand { Username = username });
        }

        [HttpDelete("{username}/follow")]
        public async Task<ActionResult<Unit>> Unfollow(string username)
        {
            return await Mediator.Send(new DeleteFollower.DeleteFollowerCommand { Username = username });
        }

        [HttpGet("{username}/follow")]
        public async Task<ActionResult<List<ProfileDto>>> GetFollowings(string username, string predicate)
        {
            return await Mediator.Send(new ListFollowers.ListFollowersQuery { Username = username, Predicate = predicate });
        }
        [HttpPost("GetContacts")]
        public async Task<ActionResult<List<ProfileDto>>> GetContacts(GetApplicationUsersByNumber query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);

        }
    }
}
