using Application.Dtos.Advertising;
using Application.Dtos.RejectedResult;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.RejectedResult.Queries
{
    public class GetRejectedResultById : IRequest<GetRejectedResultDto>
    {
        public int Id { get; set; }
        public class GetRejectedResultByIdHandler : IRequestHandler<GetRejectedResultById, GetRejectedResultDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetRejectedResultByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetRejectedResultDto> Handle(GetRejectedResultById query, CancellationToken cancellationToken)
            {
                var RejectedResult = await _unitOfWork.RejectResults.GetQueryList().AsNoTracking()
                    .Where(c => c.Id == query.Id)
                    .Include(c => c.RejectedAdAttachment).ThenInclude(c => c.Attachment)
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
                        Files = c.RejectedAdAttachment.Where(s => s.RejectResultId == c.Id)
                    .Select(s => new GetFileWithType
                    {
                        Id = s.Attachment.Id,
                        Name = s.Attachment.FileName,
                        FileType = s.Attachment.FileType.Type,
                    }).ToList(),


                    }).FirstOrDefaultAsync();
                if (RejectedResult == null) throw new RestException(HttpStatusCode.BadRequest, "آگهی وجود ندارد!");
                return RejectedResult;


            }
        }
    }
}
