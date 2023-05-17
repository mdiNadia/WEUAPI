using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Blocks.Queries
{
    public class ReportedAdsCount
    {
        public class ReportedAdsCountQuery : IRequest<int>
        {
            public class ReportedAdsCountHandler : IRequestHandler<ReportedAdsCountQuery, int>
            {
                private readonly IUnitOfWork _unitOfWork;
                private readonly IUserAccessor _userAccessor;

                public ReportedAdsCountHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
                {
                    this._unitOfWork = unitOfWork;
                    this._userAccessor = userAccessor;
                }
                public async Task<int> Handle(ReportedAdsCountQuery request, CancellationToken cancellationToken)
                {
                    try
                    {
                        int count = await _unitOfWork.AdReports
                            .GetQueryList().AsNoTracking()
                            .CountAsync();
                        return count;
                    }
                    catch (Exception err) { throw new Exception("خطا در گرفتن اطلاعات!"); }


                }
            }

        }

    }
}
