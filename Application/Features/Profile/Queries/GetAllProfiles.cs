using Application.Dtos.Common;
using Application.Errors;
using Application.Features.Profile.Dtos;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Profile.Queries
{
    public class GetAllProfiles : IRequest<IEnumerable<GetProfileDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllProfiles(IPaginationFilter filter)
        {
            _filter = filter;
        }

        public class GetAllProfilesHandler : IRequestHandler<GetAllProfiles, IEnumerable<GetProfileDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllProfilesHandler(IUnitOfWork unitOfWork)
            {

                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetProfileDto>> Handle(GetAllProfiles query, CancellationToken cancellationToken)
            {

                var profileList = await _unitOfWork.Profiles.GetQueryList()
                    .AsNoTracking()
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .Select(c => new GetProfileDto()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CreationDate = c.CreationDate,
                        BirthDate = c.Birthday,
                        Bio = c.Bio,
                        Link = c.Link,
                        Username = c.Username,
                        BlockedCount = c.BlockedCount,
                        FollowersCount = c.FollowersCount,
                        FollowingCount = c.FollowingCount,
                        Score = c.Score,
                        ProfileType = c.ProfileTypeEnum,
                        AvatarId = c.AvatarId ?? 0,
                        Avatar = c.Avatar != null ? c.Avatar.FileName : null,
                        User = new GetNameAndIdString
                        {
                            Id = c.UserId,
                            Name = c.User.FirstName + c.User.LastName,
                        },
                        PhoneNumber = c.User.PhoneNumber ?? "ثبت نشده",
                        Email = c.User.Email ?? "ثبت نشده",

                    }).ToListAsync();
                if (profileList == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }
                return profileList.AsReadOnly();


            }
        }
    }
}
