
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Blocks.Queries
{
    public class ReportedUsersCount
    {
        public class ReportedUsersCountQuery : IRequest<int>
        {
            public class ReportedUsersCountHandler : IRequestHandler<ReportedUsersCountQuery, int>
            {
                private readonly IUnitOfWork _unitOfWork;
                private readonly IUserAccessor _userAccessor;

                public ReportedUsersCountHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
                {
                    this._unitOfWork = unitOfWork;
                    this._userAccessor = userAccessor;
                }
                public async Task<int> Handle(ReportedUsersCountQuery request, CancellationToken cancellationToken)
                {
                    int count = await _unitOfWork.ProfileReports
                        .GetQueryList().AsNoTracking()
                        .CountAsync();
                    return count;
                }
            }
        }


    }
}
