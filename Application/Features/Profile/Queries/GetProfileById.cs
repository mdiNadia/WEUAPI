using Application.Dtos.Common;
using Application.Dtos.Profile;
using Application.Errors;
using Application.Interfaces;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Profile.Queries
{
    public class GetProfileById : IRequest<GetProfileDto>
    {
        public int Id { get; set; }
        public class GetProfileByIdHandler : IRequestHandler<GetProfileById, GetProfileDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetProfileByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetProfileDto> Handle(GetProfileById query, CancellationToken cancellationToken)
            {

                var profile = await _unitOfWork.Profiles.GetQueryList()
                    .AsNoTracking()
                    .Include(c => c.Avatar)
                    .Include(c => c.User)
                    .Where(c => c.Id == query.Id)
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
                    }).FirstOrDefaultAsync();
                if (profile == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                return profile;


            }
        }
    }
}
