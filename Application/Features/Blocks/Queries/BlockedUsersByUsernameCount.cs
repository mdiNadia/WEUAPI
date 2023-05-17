
using Application.Errors;
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Blocks.Queries
{
    public class BlockedUsersByUsernameCount
    {
        public class BlockedUsersByUsernameCountQuery : IRequest<int>
        {
            public class BlockedUsersByUsernameCountHandler : IRequestHandler<BlockedUsersByUsernameCountQuery, int>
            {
                private readonly IUnitOfWork _unitOfWork;
                private readonly IUserAccessor _userAccessor;

                public BlockedUsersByUsernameCountHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
                {
                    this._unitOfWork = unitOfWork;
                    this._userAccessor = userAccessor;
                }
                public async Task<int> Handle(BlockedUsersByUsernameCountQuery request, CancellationToken cancellationToken)
                {
                    var currentUserName = _userAccessor.GetCurrentUserNameAsync();
                    if (currentUserName == null) throw new RestException(HttpStatusCode.BadRequest, "کاربر یافت نشد!");

                    int count = await _unitOfWork.ProfileReports
                        .GetQueryList()
                        .AsNoTracking()
                        .Include(c => c.Observer)
                        .Where(c => c.Observer.Username == currentUserName).CountAsync();


                    return count;
                }
            }
        }


    }
}
