using Application.Dtos.Advertising;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.ConfirmedResult.Queries
{
    public class GetAllConfirmedResults : IRequest<IEnumerable<GetConfirmedResultDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllConfirmedResults(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetAllConfirmedResultsHandler : IRequestHandler<GetAllConfirmedResults, IEnumerable<GetConfirmedResultDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllConfirmedResultsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetConfirmedResultDto>> Handle(GetAllConfirmedResults query, CancellationToken cancellationToken)
            {

                var advertisingList = await _unitOfWork.ConfirmedResults.GetQueryList()
                    .AsNoTracking()
                    .Include(c => c.ConfirmedResultAttachments).ThenInclude(c => c.Attachment)
                    .OrderByDescending(s => s.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .Select(c => new GetConfirmedResultDto
                    {
                        Id = c.Id,
                        AdId = c.AdvertiserId,
                        Name = c.Name,
                        Description = c.Description,
                        Text = c.Text,
                        CreationDate = c.CreationDate,
                        ExpireDate = c.ExpireDate,
                        StartDate = c.StartDate,
                        Files = c.ConfirmedResultAttachments.Where(s => s.ConfirmResultId == c.Id)
                    .Select(s => new GetFileWithType
                    {
                        Id = s.Attachment.Id,
                        Name = s.Attachment.FileName,
                        FileType = 0,
                    }).ToList(),
                    })
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
