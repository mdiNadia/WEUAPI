using Application.Dtos.Profile;
using Application.Errors;
using Application.Interfaces;
using Application.Services.UserAccessor;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Blocks.Queries
{
    public class ListReportedAds
    {
        public class ListReportedAdsQuery : IRequest<List<ReportedDto>>
        {
            private readonly IPaginationFilter _filter;
            public ListReportedAdsQuery(IPaginationFilter filter)
            {
                _filter = filter;
            }

            public class ListReportedAdsHandler : IRequestHandler<ListReportedAdsQuery, List<ReportedDto>>
            {
                private readonly IUnitOfWork _unitOfWork;
                private readonly IUserAccessor _userAccessor;

                public ListReportedAdsHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
                {
                    this._unitOfWork = unitOfWork;
                    this._userAccessor = userAccessor;
                }
                public async Task<List<ReportedDto>> Handle(ListReportedAdsQuery request, CancellationToken cancellationToken)
                {

                    var reports = new List<AdReport>();
                    var reportsResult = new List<ReportedDto>();
                    reports = await _unitOfWork.AdReports
                    .GetQueryList().AsNoTracking()
                    .Include(c => c.Observer)
                    .Include(c => c.Target)
                    .Include(c => c.Reason)
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((request._filter.PageNumber - 1) * request._filter.PageSize)
                    .Take(request._filter.PageSize)
                    .ToListAsync();
                    if (reports == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                    foreach (var user in reports)
                    {
                        var observer = user.Observer.Username;
                        var target = user.Target.Name;
                        var reason = user.Reason.Reason;
                        reportsResult.Add(new ReportedDto { Blocker = observer, Blocked = target, Reason = reason, Desciption = user.Description ?? "" });
                    }
                    return reportsResult;
                }
            }
        }

    }
}
