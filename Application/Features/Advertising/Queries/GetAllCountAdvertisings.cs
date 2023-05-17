using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Advertising.Queries
{
    public class GetAllCountAdvertisings : IRequest<int>
    {
        public class GetAllCountAdvertisingsHandler : IRequestHandler<GetAllCountAdvertisings, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountAdvertisingsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountAdvertisings query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Advertisings.GetQueryList().AsNoTracking().CountAsync();

                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }
            }
        }
    }
}
