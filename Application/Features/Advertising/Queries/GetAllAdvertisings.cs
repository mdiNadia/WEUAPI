using Application.Dtos.Advertising;
using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Advertising.Queries
{
    public class GetAllAdvertisings : IRequest<IEnumerable<GetAdvertisingDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllAdvertisings(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetAllAdvertisingsHandler : IRequestHandler<GetAllAdvertisings, IEnumerable<GetAdvertisingDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllAdvertisingsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetAdvertisingDto>> Handle(GetAllAdvertisings query, CancellationToken cancellationToken)

            {


                var advertisingList = await _unitOfWork.Advertisings.GetQueryList()
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
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .ToListAsync();
                if (advertisingList == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "آگهی وجود ندارد!");

                }

                return advertisingList;


            }
        }
    }
}
