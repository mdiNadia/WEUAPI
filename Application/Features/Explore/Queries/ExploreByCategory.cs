using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Explore.Queries
{
    public class ExploreByCategory : IRequest<IEnumerable<GetNameAndId>>
    {
        public string q { get; set; }
        private readonly IPaginationFilter _filter;

        public ExploreByCategory(IPaginationFilter filter)
        {
            _filter = filter;

        }
        public class ExploreByCategoryHandler : IRequestHandler<ExploreByCategory, IEnumerable<GetNameAndId>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public ExploreByCategoryHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetNameAndId>> Handle(ExploreByCategory query, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.AdCategories.GetQueryList()
                   .AsNoTracking()
                   .Where(c => c.Name.Contains(query.q))
                   .Select(c => new GetNameAndId
                   {
                       Id = c.Id,
                       Name = c.Name,
                       CreationDate = c.CreationDate,
                   }).OrderByDescending(c => c.CreationDate).Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                   .Take(query._filter.PageSize).ToListAsync();
                if (result == null) throw new RestException(HttpStatusCode.BadRequest, "دسته‌بندی یافت نشد!");
                return result;

            }
        }
    }
}
