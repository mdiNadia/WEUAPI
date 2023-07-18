using Application.Dtos.Advertising;
using Application.Dtos.Common;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Advertising.Queries
{
    public class GetAllAdvertisings : IRequest<IQueryable<GetAdvertisingDto>>
    {
        public GetAllAdvertisings()
        {
        }
        public class GetAllAdvertisingsHandler : IRequestHandler<GetAllAdvertisings, IQueryable<GetAdvertisingDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllAdvertisingsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IQueryable<GetAdvertisingDto>> Handle(GetAllAdvertisings query, CancellationToken cancellationToken)
            {
                var advertisingList = _unitOfWork.Advertisings.GetQueryList()
                    .AsNoTracking()
                    .Include(c => c.AdvertisingAttachments).ThenInclude(c => c.Attachment)
                    .Include(c => c.AdCategoryAdvertisings).ThenInclude(c => c.AdCategory)
                    .Select(c => new GetAdvertisingDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Text = c.Text,
                        CreationDate = c.CreationDate,
                        ExpireDate = c.ExpireDate,
                        StartDate = c.StartDate,
                        AdStatus = c.AdStatus,
                        Categories = c.AdCategoryAdvertisings.Where(s => s.AdvertisingId == c.Id)
                    .Select(s => new GetNameAndId
                    {
                        Id = s.AdCategory.Id,
                        Name = s.AdCategory.Name,
                        CreationDate = s.CreationDate
                    }).OrderByDescending(s => s.CreationDate).ToList(),
                        Files = c.AdvertisingAttachments.Where(s => s.AdvertisingId == c.Id)
                    .Select(s => new GetFileWithType
                    {
                        Id = s.AttachmentId,
                        Name = s.Attachment.FileName,
                        FileType = 0,
                    }).ToList(),
                    })
                    .OrderByDescending(c => c.CreationDate);

                return advertisingList;


            }
        }
    }
}
