using Application.Features.Profile.Dtos;
using Application.Interfaces;
using Application.Services.UserAccessor;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Features.Blocks.Queries
{
    public class ListReportedUsers
    {
        public class ListReportedUsersQuery : IRequest<List<ReportedDto>>
        {
            private readonly IPaginationFilter _filter;
            public ListReportedUsersQuery(IPaginationFilter filter)
            {
                _filter = filter;
            }

            public class ListReportedUsersHandler : IRequestHandler<ListReportedUsersQuery, List<ReportedDto>>
            {
                private readonly IUnitOfWork _unitOfWork;
                private readonly IUserAccessor _userAccessor;

                public ListReportedUsersHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
                {
                    this._unitOfWork = unitOfWork;
                    this._userAccessor = userAccessor;
                }
                public async Task<List<ReportedDto>> Handle(ListReportedUsersQuery request, CancellationToken cancellationToken)
                {

                    var reports = new List<ProfileReport>();
                    var reportsResult = new List<ReportedDto>();
                    reports = await _unitOfWork.ProfileReports
                    .GetQueryList().AsNoTracking()
                    .Include(c => c.Observer)
                    .Include(c => c.Target)
                    .Include(c => c.Reason)
                    .Skip((request._filter.PageNumber - 1) * request._filter.PageSize)
                    .Take(request._filter.PageSize)
                    .ToListAsync();
                    foreach (var user in reports)
                    {
                        var observer = user.Observer.Username;
                        var target = user.Target.Username;
                        var reason = user.Reason.Reason;
                        reportsResult.Add(new ReportedDto { Blocker = observer, Blocked = target, Reason = reason, Desciption = user.Description ?? "" });
                    }
                    return reportsResult;
                }
            }
        }

    }
}
