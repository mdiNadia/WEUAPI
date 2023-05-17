using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Explore.Queries
{
    public class ExploreByCategoryCount : IRequest<int>
    {
        public string q { get; set; }
        public class ExploreByCategoryCountHandler : IRequestHandler<ExploreByCategoryCount, int>
        {
            private readonly IUnitOfWork _unitOfWork;
            public ExploreByCategoryCountHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(ExploreByCategoryCount query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.AdCategories.GetQueryList()
                    .AsNoTracking()
                    .Where(c => c.Name.Contains(query.q))
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
