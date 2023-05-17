using Application.Dtos.Common;
using Application.Dtos.ReportReason;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.ReportReason.Queries
{
    public class GetReportReasonById : IRequest<GetReportReasonDto>
    {
        public int Id { get; set; }
        public class GetReportReasonByIdHandler : IRequestHandler<GetReportReasonById, GetReportReasonDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetReportReasonByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetReportReasonDto> Handle(GetReportReasonById query, CancellationToken cancellationToken)
            {

                var reportReason = await _unitOfWork.ReportReasons.GetQueryList()
                    .Include(x => x.Parent).ThenInclude(c => c.Children).AsNoTracking()
                    .Include(c => c.Children).Where(c => c.Id == query.Id)
                    .Select(c => new GetReportReasonDto()
                    {
                        Id = c.Id,
                        ReportReasonType = (int)c.ReportReasonType,
                        Parent = new GetNameAndId()
                        {
                            Id = (int)c.ParentId,
                            Name = c.Parent.Reason

                        },
                        Reason = c.Reason,
                        CreationDate = c.CreationDate,
                    }).FirstOrDefaultAsync();
                if (reportReason == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                return reportReason;
            }
        }
    }
}
