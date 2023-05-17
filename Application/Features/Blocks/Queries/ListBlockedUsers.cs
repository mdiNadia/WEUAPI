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
    public class ListBlockedUsers
    {
        public class ListBlockedUsersQuery : IRequest<List<BlockedDto>>
        {
            private readonly IPaginationFilter _filter;
            public ListBlockedUsersQuery(IPaginationFilter filter)
            {
                _filter = filter;
            }

            public class ListBlockedUsersHandler : IRequestHandler<ListBlockedUsersQuery, List<BlockedDto>>
            {
                private readonly IUnitOfWork _unitOfWork;
                private readonly IUserAccessor _userAccessor;

                public ListBlockedUsersHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
                {
                    this._unitOfWork = unitOfWork;
                    this._userAccessor = userAccessor;
                }
                public async Task<List<BlockedDto>> Handle(ListBlockedUsersQuery request, CancellationToken cancellationToken)
                {
                    var currentUserName = _userAccessor.GetCurrentUserNameAsync();
                    if (currentUserName == null) throw new RestException(HttpStatusCode.BadRequest, "کاربر یافت نشد!");

                    var Blocks = new List<ProfileBlock>();
                    var BlocksResult = new List<BlockedDto>();
                    Blocks = await _unitOfWork.ProfileBlocks
                    .GetQueryList().AsNoTracking()
                    .Include(c => c.Observer)
                    .Include(c => c.Target)
                    .OrderByDescending(c => c.BlockedDate)
                    .Skip((request._filter.PageNumber - 1) * request._filter.PageSize)
                    .Take(request._filter.PageSize)
                    .ToListAsync();

                    foreach (var user in Blocks)
                    {
                        var observer = user.Observer.Username;
                        var target = user.Target.Username;
                        BlocksResult.Add(new BlockedDto { Blocker = observer, Blocked = target });
                    }
                    return BlocksResult;
                }
            }
        }

    }
}
