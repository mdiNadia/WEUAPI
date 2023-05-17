

using Application.Dtos.Profile;
using Application.Interfaces;
using Application.Services.UserAccessor;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProfileRepository : GenericRepository<Profile>, IProfileRepository
    {
        private readonly IUserAccessor _userAccessor;


        public ProfileRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IUserAccessor userAccessor) : base(context, httpContextAccessor)
        {
            this._userAccessor = userAccessor;
        }

        public ProfileRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public async Task<ProfileDto> ReadProfile(string username, string currentUserName)
        {
            var user = await GetQueryList().Include(c => c.Followers).Include(c => c.Followings).SingleOrDefaultAsync(x => x.Username == username);

            if (user == null)
                throw null;
            var currentUser = await GetQueryList().Include(c=>c.Followings).SingleOrDefaultAsync(x => x.Username == currentUserName);

            var profile = new ProfileDto();
            profile.Username = user.Username;
            profile.Name = user.Name ?? "";
            profile.AvatarId = user.AvatarId ?? 0;
            profile.Bio = user.Bio ?? "";
            profile.FollowersCount = user.Followers.Count();
            profile.FollowingCount = user.Followings.Count();

            if (currentUser.Followings.Any(x => x.TargetId == user.Id))
            {
                profile.IsFollowed = true;
            }

            return profile;
        }

    }
}