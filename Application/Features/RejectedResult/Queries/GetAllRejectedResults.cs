using Application.Dtos.Advertising;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.RejectedResult.Queries
{
    public class GetAllRejectedResults : IRequest<IEnumerable<GetRejectedResultDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllRejectedResults(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetAllRejectedResultsHandler : IRequestHandler<GetAllRejectedResults, IEnumerable<GetRejectedResultDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllRejectedResultsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetRejectedResultDto>> Handle(GetAllRejectedResults query, CancellationToken cancellationToken)
            {
                var advertisingList = await _unitOfWork.ConfirmedResults.GetQueryList()
                    .AsNoTracking()
                    .Include(c => c.ConfirmedResultAttachments).ThenInclude(c => c.Attachment)
                    .OrderByDescending(s => s.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .Select(c => new GetRejectedResultDto
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


                    }).ToListAsync();
                if (advertisingList == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "آگهی وجود ندارد!");

                }
                return advertisingList;
            }
        }
    }
}
