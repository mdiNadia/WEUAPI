using Application.Dtos.Advertising;
using Application.Dtos.ConfirmedResult;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.ConfirmedResult.Queries
{
    public class GetConfirmedResultByShortKey : IRequest<GetConfirmedResultDto>
    {
        public string Key { get; set; }
        public class GetConfirmedResultByShortKeyHandler : IRequestHandler<GetConfirmedResultByShortKey, GetConfirmedResultDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetConfirmedResultByShortKeyHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }

            public async Task<GetConfirmedResultDto> Handle(GetConfirmedResultByShortKey request, CancellationToken cancellationToken)
            {
                var ConfirmedResult = await _unitOfWork.ConfirmedResults.GetQueryList().AsNoTracking()
                    .Where(c => c.ShortKey == request.Key)
                    .Include(c => c.ConfirmedResultAttachments).ThenInclude(c => c.Attachment)
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
                        FileType = s.Attachment.FileType.Type,
                    }).ToList(),
                    }).FirstOrDefaultAsync();
                if (ConfirmedResult == null) throw new RestException(HttpStatusCode.BadRequest, "آگهی وجود ندارد!");

                return ConfirmedResult;


            }
        }

    }
}
