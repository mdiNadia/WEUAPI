using Application.Features.Profile.Dtos;
using Application.Interfaces;
using Application.Services.UserAccessor;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Followers.Queries
{
    public class GetApplicationUsersByNumber : IRequest<List<ProfileDto>>
    {
        public List<string> Numbers { get; set; }
        public class GetApplicationUsersByNumberHandler : IRequestHandler<GetApplicationUsersByNumber, List<ProfileDto>>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly IUnitOfWork _unitOfWork;
            public GetApplicationUsersByNumberHandler(IUserAccessor userAccessor, IUnitOfWork unitOfWork)
            {
                this._userAccessor = userAccessor;
                this._unitOfWork = unitOfWork;
            }

            public async Task<List<ProfileDto>> Handle(GetApplicationUsersByNumber query, CancellationToken cancellationToken)
            {
                try
                {
                    var currentUserName = _userAccessor.GetCurrentUserNameAsync();

                    var profiles = new List<ProfileDto>();


                    var users = await _unitOfWork.Users.GetQueryList()
                        .Where(c => query.Numbers.Contains(c.PhoneNumber)).ToListAsync();

                    foreach (var follower in users)
                    {
                        profiles.Add(await _unitOfWork.Profiles.ReadProfile(follower.UserName, currentUserName));
                    }
                    return profiles;
                }
                catch (Exception err) { throw new Exception("خطا!"); }

            }
        }
    }
}
