using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ReportReason.Queries
{
    public class GetAllCountReportReasons : IRequest<int>
    {
        public class GetAllCountReportReasonsHandler : IRequestHandler<GetAllCountReportReasons, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountReportReasonsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountReportReasons query, CancellationToken cancellationToken)
            {

                return await _unitOfWork.ReportReasons
                .GetQueryList().AsNoTracking().CountAsync();



            }
        }
    }
}
