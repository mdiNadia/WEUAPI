using Application.Dtos.Lookup;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AdCategory.Queries
{
    public class Categories : IRequest<List<LookupDto>>
    {
        public class CategoriesHandler : IRequestHandler<Categories, List<LookupDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CategoriesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<LookupDto>> Handle(Categories query, CancellationToken cancellationToken)
            {

                var adCategories = await _unitOfWork.AdCategories
                    .GetQueryList()
                    .Include(c => c.CategoryCost)
                    .AsNoTracking()
                      .Select(c => new LookupDto()
                      {
                          Id = c.Id,
                          Title = c.Name,
                      })
                    .ToListAsync();

                return adCategories;
            }
        }
    }
}
