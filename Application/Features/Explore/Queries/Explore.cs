using Application.Dtos.Advertising;
using Application.Dtos.ConfirmedResult;
using Application.Errors;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Explore.Queries
{
    public class Explore : IRequest<IEnumerable<GetConfirmedResultDto>>
    {
        /// <summary>
        /// CategoryId
        /// </summary>
        public string? id { get; set; }
        private readonly IPaginationFilter _filter;

        public Explore(IPaginationFilter filter)
        {
            _filter = filter;

        }
        public class ExploreHandler : IRequestHandler<Explore, IEnumerable<GetConfirmedResultDto>>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly IUnitOfWork _unitOfWork;

            public ExploreHandler(IUserAccessor userAccessor, IUnitOfWork unitOfWork)
            {
                this._userAccessor = userAccessor;
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetConfirmedResultDto>> Handle(Explore query, CancellationToken cancellationToken)
            {
                var currentUser = await _unitOfWork.Profiles.GetQueryList().AsNoTracking().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());//کاربر فعلی
                if (currentUser == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "کاربر وجود ندارد!");
                }
                List<GetConfirmedResultDto> ads;
                if (query.id != null)
                {
                    ads = await _unitOfWork.ConfirmedResults.GetQueryList()
                                .Include(c => c.ConfirmedResultAttachments).AsNoTracking()
                                .Where(c => c.Categories == query.id && c.IsActive)
                                .Select(c => new GetConfirmedResultDto
                                {
                                    Id = c.Id,
                                    Name = c.Name,
                                    AdId = c.AdId,
                                    ProfilerId = currentUser.Id,
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

                                }).OrderByDescending(c => c.CreationDate).Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                               .Take(query._filter.PageSize)
                               .ToListAsync();
                }
                else
                {
                    ads = await _unitOfWork.ConfirmedResults.GetQueryList()
                              .Include(c => c.ConfirmedResultAttachments).Where(c => c.IsActive).AsNoTracking()
                              .Select(c => new GetConfirmedResultDto
                                {
                                    Id = c.Id,
                                    Name = c.Name,
                                    AdId = c.AdId,
                                    ProfilerId = currentUser.Id,
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
                                  }).ToList()
                                })
                              .OrderByDescending(c => c.CreationDate)
                              .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                              .Take(query._filter.PageSize)
                              .ToListAsync();
                }
                if (ads == null) throw new RestException(HttpStatusCode.BadRequest, "آگهی یافت نشد!");
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
