using Application.Dtos.Advertising;
using Application.Errors;
using Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Search.Queries
{
    public class SearchQuery : IRequest<IEnumerable<GetAdvertisingDto>>
    {

        private readonly IPaginationFilter _filter;
        private readonly ISearchParams _search;

        public SearchQuery(IPaginationFilter filter, ISearchParams search)
        {
            _filter = filter;
            _search = search;
        }
        public class SearchQueryHandler : IRequestHandler<SearchQuery, IEnumerable<GetAdvertisingDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public SearchQueryHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetAdvertisingDto>> Handle(SearchQuery query, CancellationToken cancellationToken)
            {

                //نام دیتابیسی و استرینگ سرچ باید هر دو حروف کوچک شود
                var ads = _unitOfWork.ConfirmedResults.GetQueryList().AsNoTracking()
                    .Where(c => c.Name.Contains(query._search.q));

                if (ads == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }

                IEnumerable<Domain.Entities.ConfirmResult> filtered = ads;
                if (query._search.category.Any())
                    filtered = filtered.Where(c => query._search.category.Contains(int.Parse(c.Categories)))
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .ToList();

                //filtered = filtered.Where(c => c.AdCategoryAdvertisings.Any(d => query._search.category.Any(y => y == d.AdCategoryId)))
                //.Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                //.Take(query._filter.PageSize)
                //.ToList();
                else
                    filtered = filtered
                        .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                        .Take(query._filter.PageSize)
                        .ToList();
                try
                {
                    var result = filtered.Adapt<IEnumerable<GetAdvertisingDto>>();
                    return result;
                }
                catch (Exception err) { throw new Exception("خطا در برگشت اطلاعات!"); }

            }
        }
    }
}
