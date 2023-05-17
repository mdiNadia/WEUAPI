using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Home.Queries
{
    public class GetCountData : IRequest<int>
    {
        public class GetCountDataHandler : IRequestHandler<GetCountData, int>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserAccessor _userAccessor;

            public GetCountDataHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
            {
                this._unitOfWork = unitOfWork;
                this._userAccessor = userAccessor;
            }
            public async Task<int> Handle(GetCountData query, CancellationToken cancellationToken)
            {

                try
                {
                    var currentUser = await _unitOfWork.Profiles.GetQueryList().AsNoTracking()
                        .SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());//کاربر فعلی
                    var followings = await _unitOfWork.UserFollowings.GetQueryList().Where(x =>
                    x.ObserverId == currentUser.Id).ToListAsync();

                    return await _unitOfWork.ConfirmedResults.GetQueryList().AsNoTracking()
                         .Where(c => followings.Select(c => c.TargetId).Contains(c.AdvertiserId) && c.IsActive)
                         .CountAsync();
                }
                catch (Exception err) { throw new Exception("خطا در گرفتن اطلاعات!"); }


            }

        }
    }
}
