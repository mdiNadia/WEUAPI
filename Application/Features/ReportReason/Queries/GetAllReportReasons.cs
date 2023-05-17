using Application.Dtos.Common;
using Application.Dtos.ReportReason;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.ReportReason.Queries
{
    public class GetAllReportReasons : IRequest<IEnumerable<GetReportReasonDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllReportReasons(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetAllReportReasonsHandler : IRequestHandler<GetAllReportReasons, IEnumerable<GetReportReasonDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllReportReasonsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetReportReasonDto>> Handle(GetAllReportReasons query, CancellationToken cancellationToken)
            {

                var reportReasonList = await _unitOfWork.ReportReasons
                    .GetQueryList().AsNoTracking()
                    .Include(c => c.Parent).ThenInclude(c => c.Children)
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .Select(c => new GetReportReasonDto()
                    {
                        Id = c.Id,
                        ReportReasonType = (int)c.ReportReasonType,
                        Parent = new GetNameAndId()
                        {
                            Id = (int)c.ParentId,
                            Name = c.Parent.Reason,
                            CreationDate = c.CreationDate,

                        },
                        Reason = c.Reason,
                        CreationDate = c.CreationDate,
                    }).ToListAsync();
                if (reportReasonList == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }

                return reportReasonList;


            }
        }
    }
}
