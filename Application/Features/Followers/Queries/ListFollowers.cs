using Application.Features.Profile.Dtos;
using Application.Interfaces;
using Application.Services.UserAccessor;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Followers
{
    public class ListFollowers
    {
        public class ListFollowersQuery : IRequest<List<ProfileDto>>
        {
            public string Username { get; set; }
            public string Predicate { get; set; }

            public class ListFollowersHandler : IRequestHandler<ListFollowersQuery, List<ProfileDto>>
            {
                private readonly IUnitOfWork _unitOfWork;
                private readonly IUserAccessor _userAccessor;

                public ListFollowersHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
                {
                    _unitOfWork = unitOfWork;
                    this._userAccessor = userAccessor;
                }

                public async Task<List<ProfileDto>> Handle(ListFollowersQuery request, CancellationToken cancellationToken)
                {
                    var queryable = _unitOfWork.UserFollowings.GetQueryList().Include(c => c.Target).Include(c => c.Observer);

                    var userFollowings = new List<UserFollowing>();
                    var profiles = new List<ProfileDto>();
                    var currentUserName = _userAccessor.GetCurrentUserNameAsync();

                    switch (request.Predicate)
                    {
                        case "followers":
                            {
                                userFollowings = await queryable.Where(x =>
                                    x.Target.Username == request.Username).ToListAsync();

                                foreach (var follower in userFollowings)
                                {
                                    profiles.Add(await _unitOfWork.Profiles.ReadProfile(follower.Observer.Username, currentUserName));
                                }
                                break;
                            }
                        case "following":
                            {
                                userFollowings = await queryable.Where(x =>
                                    x.Observer.Username == request.Username).ToListAsync();

                                foreach (var follower in userFollowings)
                                {
                                    profiles.Add(await _unitOfWork.Profiles.ReadProfile(follower.Target.Username, currentUserName));
                                }
                                break;
                            }
                    }

                    return profiles;
                }
            }
        }


    }
}