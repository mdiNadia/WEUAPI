using Application.Errors;
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.User.Queries
{
    public class ListOfCountSavedAd : IRequest<int>
    {
        public class ListOfCountSavedAdHandler : IRequestHandler<ListOfCountSavedAd, int>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserAccessor _userAccessor;

            public ListOfCountSavedAdHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
            {
                this._unitOfWork = unitOfWork;
                this._userAccessor = userAccessor;
            }
            public async Task<int> Handle(ListOfCountSavedAd query, CancellationToken cancellationToken)
            {
                var observer = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());
                if (observer == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found User");

                try
                {
                    return await _unitOfWork.SavedAds.GetQueryList()
                        .Where(c => c.ProfileId == observer.Id)
                        .AsNoTracking().CountAsync();

                }
                catch (Exception err) { throw new Exception("خطا در گرفتن اطلاعات!"); }

            }
        }
    }
}
