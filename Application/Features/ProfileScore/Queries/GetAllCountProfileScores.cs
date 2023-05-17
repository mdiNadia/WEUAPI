using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.ProfileScore.Queries
{
    public class GetAllCountProfileScores : IRequest<int>
    {
        public class GetAllCountProfileScoresHandler : IRequestHandler<GetAllCountProfileScores, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountProfileScoresHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountProfileScores query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.ProfileScores.GetQueryList().AsNoTracking().CountAsync();

                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }
            }
        }
    }
}
