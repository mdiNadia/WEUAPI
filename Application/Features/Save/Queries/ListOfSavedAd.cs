using Application.Dtos.Advertising;
using Application.Errors;
using Application.Features.ConfirmedResult.Queries;
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.User.Queries
{
    public class ListOfSavedAd : IRequest<IEnumerable<GetConfirmedResultDto>>
    {
        private readonly IPaginationFilter _filter;
        public ListOfSavedAd(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class ListOfSavedAdHandler : IRequestHandler<ListOfSavedAd, IEnumerable<GetConfirmedResultDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserAccessor _userAccessor;

            public ListOfSavedAdHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
            {
                this._unitOfWork = unitOfWork;
                this._userAccessor = userAccessor;
            }
            public async Task<IEnumerable<GetConfirmedResultDto>> Handle(ListOfSavedAd query, CancellationToken cancellationToken)
            {
                var observer = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());
                if (observer == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found User");

                var savedAds = await _unitOfWork.SavedAds.GetQueryList()
                   .Include(c => c.Advertising).ThenInclude(c => c.ConfirmedResultAttachments)
                   .Where(c => c.ProfileId == observer.Id).Select(c => new GetConfirmedResultDto
                   {
                       Id = c.Advertising.Id,
                       AdId = c.Advertising.AdvertiserId,
                       Name = c.Advertising.Name,
                       Description = c.Advertising.Description,
                       Text = c.Advertising.Text,
                       CreationDate = c.Advertising.CreationDate,
                       ExpireDate = c.Advertising.ExpireDate,
                       StartDate = c.Advertising.StartDate,
                       Files = c.Advertising.ConfirmedResultAttachments.Where(s => s.ConfirmResultId == c.Advertising.Id)
                             .Select(s => new GetFileWithType
                             {
                                 Id = s.Attachment.Id,
                                 Name = s.Attachment.FileName,
                                 FileType = 0,
                             }).ToList(),
                   }).OrderByDescending(c => c.CreationDate).AsNoTracking().ToListAsync();
                if (savedAds == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                return savedAds;


            }
        }
    }
}
