
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Blocks.Queries
{
    public class BlockedUsersCount
    {
        public class BlockedUsersCountQuery : IRequest<int>
        {
            public class BlockedUsersCountHandler : IRequestHandler<BlockedUsersCountQuery, int>
            {
                private readonly IUnitOfWork _unitOfWork;
                private readonly IUserAccessor _userAccessor;

                public BlockedUsersCountHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
                {
                    this._unitOfWork = unitOfWork;
                    this._userAccessor = userAccessor;
                }
                public async Task<int> Handle(BlockedUsersCountQuery request, CancellationToken cancellationToken)
                {

                    int count = await _unitOfWork.ProfileBlocks
                        .GetQueryList().AsNoTracking()
                        .CountAsync();
                    return count;
                }
            }
        }
    }
}
