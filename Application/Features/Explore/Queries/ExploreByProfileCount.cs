using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Explore.Queries
{
    public class ExploreByProfileCount : IRequest<int>
    {
        public string q { get; set; }
        public class ExploreByProfileCountHandler : IRequestHandler<ExploreByProfileCount, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public ExploreByProfileCountHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(ExploreByProfileCount query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Profiles.GetQueryList()
                      .AsNoTracking()
                      .Where(c => c.Username.Contains(query.q))
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
