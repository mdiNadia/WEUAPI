using Application.Features.Blocks.Queries;
using Application.Features.Profile.Dtos;
using Application.Features.Report.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers.v1
{
    public class ReportController : BaseApiController
    {
        private readonly IUriService _uriService;

        public ReportController(IUriService uriService)
        {
            _uriService = uriService;
        }
        [HttpPost("{username}")]
        public async Task<ActionResult<Unit>> Report(string username, AddReportUser.AddReportUserCommand command)
        {
            return await Mediator.Send(command);
        }

        //برای ادمین نمایش لیستی از تمام کاربران گزارش شده
        [HttpGet]
        public async Task<ActionResult<List<ReportedDto>>> GetAllReportedProfiles([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new ListReportedUsers.ListReportedUsersQuery(filter));
            var totalRecords = await Mediator.Send(new ReportedUsersCount.ReportedUsersCountQuery());
            var pagedReponse = PaginationHelper.CreatePagedReponse<ReportedDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        //آگهی
        [HttpPost("[action]")]
        public async Task<ActionResult<Unit>> Ad(int adId, AddReportAd.AddReportAdCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<ReportedDto>>> GetAllReportedAds([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var pagedData = await Mediator.Send(new ListReportedAds.ListReportedAdsQuery(filter));
            var totalRecords = await Mediator.Send(new ReportedAdsCount.ReportedAdsCountQuery());
            var pagedReponse = PaginationHelper.CreatePagedReponse<ReportedDto>(pagedData, filter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
    }
}
