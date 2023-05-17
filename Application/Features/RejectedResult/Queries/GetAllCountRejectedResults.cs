using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.RejectedResult.Queries
{
    public class GetAllCountRejectedResults : IRequest<int>
    {
        public class GetAllCountRejectedResultsHandler : IRequestHandler<GetAllCountRejectedResults, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountRejectedResultsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountRejectedResults query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.RejectResults.GetQueryList().AsNoTracking()
                    .CountAsync();
                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }

            }
        }
    }
}
