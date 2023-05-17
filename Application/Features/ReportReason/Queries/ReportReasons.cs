
using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.ReportReason.Queries
{
    public class ReportReasons : IRequest<List<GetNameAndId>>
    {

        public class ReportReasonsHandler : IRequestHandler<ReportReasons, List<GetNameAndId>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public ReportReasonsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<GetNameAndId>> Handle(ReportReasons query, CancellationToken cancellationToken)
            {

                var reasons = await _unitOfWork.ReportReasons
                    .GetQueryList()
                    .AsNoTracking()
                    .Select(c => new GetNameAndId()
                    {
                        Id = c.Id,
                        Name = c.Reason,
                        CreationDate = c.CreationDate,
                    })
                    .OrderByDescending(c => c.CreationDate)
                    .ToListAsync();
                if (reasons == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }

                return reasons;


            }
        }
    }
}
