using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Neighbourhood.Queries
{
    public class GetAllCountNeighbourhoods : IRequest<int>
    {
        public class GetAllCountNeighbourhoodsHandler : IRequestHandler<GetAllCountNeighbourhoods, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountNeighbourhoodsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountNeighbourhoods query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Neighborhoods.GetQueryList()
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
