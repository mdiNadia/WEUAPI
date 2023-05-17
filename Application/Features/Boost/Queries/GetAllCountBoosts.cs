using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Province.Queries
{
    public class GetAllCountBoosts : IRequest<int>
    {
        public class GetAllCountBoostsHandler : IRequestHandler<GetAllCountBoosts, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountBoostsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountBoosts query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Boosts.GetQueryList()
                        .AsNoTracking().CountAsync();

                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }
            }
        }
    }
}
