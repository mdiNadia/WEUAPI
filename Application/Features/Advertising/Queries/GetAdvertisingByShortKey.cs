using Application.Dtos.Advertising;
using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Advertising.Queries
{
    public class GetAdvertisingByShortKey : IRequest<GetAdvertisingDto>
    {
        public string Key { get; set; }
        public class GetAdvertisingByShortKeyHandler : IRequestHandler<GetAdvertisingByShortKey, GetAdvertisingDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAdvertisingByShortKeyHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }

            public async Task<GetAdvertisingDto> Handle(GetAdvertisingByShortKey request, CancellationToken cancellationToken)
            {
                var advertising = await _unitOfWork.Advertisings.GetQueryList().AsNoTracking()
                    .Include(c => c.AdvertisingAttachments)
                    .Include(c => c.AdCategoryAdvertisings).ThenInclude(c => c.AdCategory)
                    .Where(c => c.ShortKey == request.Key)
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
                                 CreationDate = s.CreationDate,
                             }).OrderByDescending(s => s.CreationDate).ToList(),
                        Files = c.AdvertisingAttachments.Where(s => s.AdvertisingId == c.Id)
                                 .Select(s => new GetFileWithType
                                 {
                                     Id = s.Attachment.Id,
                                     Name = s.Attachment.FileName,
                                     FileType = 0,
                                 }).ToList(),
                    })
                    .FirstOrDefaultAsync();
                if (advertising == null) throw new RestException(HttpStatusCode.BadRequest, "آگهی وجود ندارد!");

                return advertising;


            }
        }

    }
}
