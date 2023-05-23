using Application.Errors;
using Application.Features.Profile.Dtos;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Explore.Queries
{

    public class ExploreByProfile : IRequest<IEnumerable<GetProfileDto>>
    {
        public string q { get; set; }
        private readonly IPaginationFilter _filter;

        public ExploreByProfile(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class ExploreByProfileHandler : IRequestHandler<ExploreByProfile, IEnumerable<GetProfileDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public ExploreByProfileHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetProfileDto>> Handle(ExploreByProfile query, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Profiles.GetQueryList()
                    .Include(c => c.Avatar)
                    .Include(c => c.User)
                    .AsNoTracking()
                    .Where(c => c.Username.Contains(query.q))
                    .Select(c => new GetProfileDto
                    {
                        Id = c.Id,
                        Name = c.Username,
                        Username = c.Username,
                        Bio = c.Bio,
                        BlockedCount = c.BlockedCount,
                        CreationDate = c.CreationDate,
                        BirthDate = c.Birthday,
                        FollowersCount = c.FollowersCount,
                        FollowingCount = c.FollowingCount,
                        Link = c.Link,
                        ProfileType = c.ProfileTypeEnum,
                        Score = c.Score,
                        Email = c.User != null ? c.User.Email : "ثبت نشده",
                        PhoneNumber = c.User.PhoneNumber ?? "ثبت نشده",
                        AvatarId = c.AvatarId ?? 0,
                        Avatar = c.Avatar != null ? c.Avatar.FileName : null

                    }).OrderByDescending(c => c.CreationDate).Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize).ToListAsync();
                if (result == null) throw new RestException(HttpStatusCode.BadRequest, "حسابی یافت نشد!");
                return result;
            }
        }
    }
}
