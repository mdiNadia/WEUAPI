using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Search.Queries
{
    public class SearchCountQuery : IRequest<int>
    {

        private readonly IPaginationFilter _filter;
        private readonly ISearchParams _search;

        public SearchCountQuery(IPaginationFilter filter, ISearchParams search)
        {
            _filter = filter;
            _search = search;
        }
        public class SearchCountQueryHandler : IRequestHandler<SearchCountQuery, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public SearchCountQueryHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(SearchCountQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    int count = 0;
                    if (query._search.category.Any())
                        return await _unitOfWork.Advertisings.GetQueryList().AsNoTracking()
                       .Where(c => c.Name.Contains(query._search.q) && c.AdCategoryAdvertisings.Any(d => query._search.category.Any(y => y == d.AdCategoryId))).CountAsync();
                    else
                        return await _unitOfWork.Advertisings.GetQueryList().AsNoTracking()
                       .Where(c => c.Name.Contains(query._search.q)).CountAsync();
                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }

            }
        }
    }
}
