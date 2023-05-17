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
    public class ListBlockedUsersByUsername
    {
        public class ListBlockedUsersByUsernameQuery : IRequest<List<ProfileDto>>
        {
            private readonly IPaginationFilter _filter;

            public ListBlockedUsersByUsernameQuery(IPaginationFilter filter)
            {
                this._filter = filter;
            }

            public class ListBlockedUsersByUsernameHandler : IRequestHandler<ListBlockedUsersByUsernameQuery, List<ProfileDto>>
            {
                private readonly IUnitOfWork _unitOfWork;
                private readonly IUserAccessor _userAccessor;

                public ListBlockedUsersByUsernameHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
                {
                    this._unitOfWork = unitOfWork;
                    this._userAccessor = userAccessor;
                }
                public async Task<List<ProfileDto>> Handle(ListBlockedUsersByUsernameQuery request, CancellationToken cancellationToken)
                {
                    var currentUserName = _userAccessor.GetCurrentUserNameAsync();
                    if (currentUserName == null) throw new RestException(HttpStatusCode.BadRequest, "کاربر یافت نشد!");

                    var userBlocks = new List<ProfileBlock>();
                    var userBlocksResult = new List<ProfileDto>();
                    userBlocks = await _unitOfWork.ProfileBlocks
                    .GetQueryList()
                    .AsNoTracking()
                    .Include(c => c.Observer)
                    .Include(c => c.Target)
                    .OrderByDescending(c => c.BlockedDate)
                    .Skip((request._filter.PageNumber - 1) * request._filter.PageSize)
                    .Take(request._filter.PageSize)
                    .Where(c => c.Observer.Username == currentUserName).ToListAsync();

                    foreach (var user in userBlocks)
                    {
                        userBlocksResult.Add(await _unitOfWork.Profiles.ReadProfile(user.Target.Username, currentUserName));
                    }
                    return userBlocksResult;
                }
            }
        }

    }
}
