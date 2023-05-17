using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.ConfirmedResult.Queries
{
    public class GetAllCountConfirmedResults : IRequest<int>
    {
        public class GetAllCountConfirmedResultsHandler : IRequestHandler<GetAllCountConfirmedResults, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountConfirmedResultsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountConfirmedResults query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.ConfirmedResults.GetQueryList().AsNoTracking()
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
