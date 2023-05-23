using Application.Dtos.Advertising;
using Application.Dtos.Common;
using Application.Dtos.ConfirmedResult;
using Application.Dtos.Profile;
using Application.Errors;
using Application.ExtensionMethods;
using Application.Features.Wallet.Queries;
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Profile.Queries
{
    public class GetProfileInformation : IRequest<Tuple<bool, GetProfileDto, IEnumerable<GetConfirmedResultDto>, GetWalletDto>>
    {
        public class GetProfileInformationHandler : IRequestHandler<GetProfileInformation, Tuple<bool, GetProfileDto, IEnumerable<GetConfirmedResultDto>, GetWalletDto>>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly IUnitOfWork _unitOfWork;

            public GetProfileInformationHandler(IUserAccessor userAccessor, IUnitOfWork unitOfWork)
            {
                this._userAccessor = userAccessor;
                this._unitOfWork = unitOfWork;
            }
            public async Task<Tuple<bool, GetProfileDto, IEnumerable<GetConfirmedResultDto>, GetWalletDto>> Handle(GetProfileInformation query, CancellationToken cancellationToken)
            {
                var UserName = _userAccessor.GetCurrentUserNameAsync();
                var currentProfile = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == UserName);
                bool isOwn;
                if (currentProfile?.Username == UserName)
                {
                    isOwn = true;
                }
                else
                {
                    isOwn = false;
                }
                var profile = await _unitOfWork.Profiles.GetQueryList()
                    .Include(x => x.Avatar).Include(x => x.User)
                .Where(c => c.Username == UserName)
                .AsNoTracking().Select(c => new GetProfileDto()
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
                profile.FollowingCount = await _unitOfWork.UserFollowings.GetQueryList()
                    .Where(c => c.ObserverId == profile.Id).CountAsync();
                profile.FollowersCount = await _unitOfWork.UserFollowings.GetQueryList()
                .Where(c => c.TargetId == profile.Id).CountAsync();
                profile.AdsCount = await _unitOfWork.ConfirmedResults.GetQueryList()
                    .Where(c => c.AdvertiserId == profile.Id).CountAsync();
                var ads = await _unitOfWork.ConfirmedResults.GetQueryList()
                 .Include(c => c.ConfirmedResultAttachments)
                 .Where(c => c.AdvertiserId == profile.Id)
                     .Select(c => new GetConfirmedResultDto
                     {
                         Id = c.Id,
                         Name = c.Name,
                         AdId = c.AdId,
                         ProfilerId = c.AdvertiserId,
                         CreationDate = c.CreationDate,
                         ExpireDate = c.ExpireDate,
                         ConfirmedDate = c.ConfirmedDate.TimeAgo(),
                         StartDate = c.StartDate,
                         Description = c.Description,
                         Likes = c.Likes.Count(),
                         Views = c.Views.Count(),
                         ShortKey = c.ShortKey,
                         Text = c.Text,

                         Files = c.ConfirmedResultAttachments.Where(s => s.ConfirmResultId == c.Id)
                                  .Select(s => new GetFileWithType()
                                  {
                                      Id = s.Attachment.Id,
                                      Name = s.Attachment.FileName,
                                      FileType = 0,
                                  }).ToList(),


                     })
                 .ToListAsync();
                var profiles = _unitOfWork.Profiles
                    .GetQueryList();
                var likes = _unitOfWork.Likes.GetQueryList();
                ads.ForEach(a =>
                {
                    a.Username = profiles.Where(c => c.Id == a.ProfilerId).Select(c => c.Username).FirstOrDefault() ?? "";
                    a.Avatar = profiles.Where(c => c.Id == a.ProfilerId).Include(c => c.Avatar).Select(c => c.Avatar.FileName).FirstOrDefault() ?? null;
                    a.IsLikedBefore = likes.Where(c => c.TargetId == a.Id && c.ObserverId == a.ProfilerId).Any();
                });
                var wallet =await _unitOfWork.Wallets.GetQueryList()
                    .Where(c => c.Profile == currentProfile)
                    .Select(c => new GetWalletDto
                    {
                        CreationDate = c.CreationDate,
                        Description = c.Description,
                        IsActive = c.IsActive,
                        Name = c.Name,
                        UpdateDate = c.UpdateDate,
                        TotalCredit = c.TotalCredit,
                        Value = c.Value,

                    }).FirstAsync();
                var result = new Tuple<bool, GetProfileDto, IEnumerable<GetConfirmedResultDto>, GetWalletDto>(isOwn, profile, ads, wallet);
                return result;


            }
        }
    }
}
