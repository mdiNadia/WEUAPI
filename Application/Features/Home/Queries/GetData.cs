using Application.Dtos.Advertising;
using Application.Errors;
using Application.ExtensionMethods;
using Application.Features.ConfirmedResult.Queries;
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Profile.Queries
{
    public class GetData : IRequest<IEnumerable<GetConfirmedResultDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetData(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetDataHandler : IRequestHandler<GetData, IEnumerable<GetConfirmedResultDto>>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly IUnitOfWork _unitOfWork;

            public GetDataHandler(IUserAccessor userAccessor, IUnitOfWork unitOfWork)
            {
                this._userAccessor = userAccessor;
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetConfirmedResultDto>> Handle(GetData query, CancellationToken cancellationToken)
            {

                //اگر کاربر فالو شده هم داشت باشد در شرط ها اعمال میکردیم
                //لیستی از کاربران فالو شده کاربر فعلی را میگیریم و آگهی هاشون رو پیدا میکنیم
                //سوال از هر کاربر چند آگهی پیدا کنیم ؟
                var currentUser = await _unitOfWork.Profiles.GetQueryList().AsNoTracking().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());//کاربر فعلی
                if (currentUser == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "کاربر وجود ندارد!");
                }
                var followings = await _unitOfWork.UserFollowings.GetQueryList().Where(x =>
                                    x.ObserverId == currentUser.Id).ToListAsync();
                if (!followings.Any())
                {
                    throw new RestException(HttpStatusCode.BadRequest, "این کاربر دنبال کننده ای ندارد!");
                }
                var ads = await _unitOfWork.ConfirmedResults.GetQueryList().AsNoTracking()
                         .Where(c => followings.Select(c => c.TargetId).Contains(c.AdvertiserId) && c.IsActive)
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
                         .OrderByDescending(c => c.CreationDate)
                         .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                         .Take(query._filter.PageSize)
                         .OrderByDescending(c => c.CreationDate)
                         .ToListAsync();
                if (!ads.Any())
                {
                    throw new RestException(HttpStatusCode.BadRequest, "آگهی ای وجود ندارد!");
                }
                var profiles = _unitOfWork.Profiles
                    .GetQueryList();
                var likes = _unitOfWork.Likes.GetQueryList();
                var views = _unitOfWork.Views.GetQueryList();
                var saves = _unitOfWork.SavedAds.GetQueryList();
                var confirmeds = _unitOfWork.ConfirmedResults.GetQueryList();
                ads.ForEach(a =>
                {
                    a.Username = profiles.Where(c => c.Id == a.ProfilerId).Select(c => c.Username).FirstOrDefault() ?? "";
                    a.Avatar = profiles.Where(c => c.Id == a.ProfilerId).Include(c => c.Avatar).Select(c => c.Avatar.FileName).FirstOrDefault() ?? null;
                    a.IsLikedBefore = likes.Where(c => c.TargetId == a.Id && c.ObserverId == a.ProfilerId).Any();
                    a.IsSavedBefore = saves.Where(c => c.AdvertisingId == a.Id && c.ProfileId == a.ProfilerId).Any();
                    var getConfirmedData = confirmeds.Where(c => c.AdId == a.AdId).Select(c => c.Id);
                    a.Likes = likes.Where(c => getConfirmedData.Contains(c.TargetId)).Count();
                    a.Views = views.Where(c => getConfirmedData.Contains(c.TargetId)).Count();
                });
                return ads;
            }
        }

    }
}
