using Application.Dtos.AdCategory;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.AdCategory.Queries
{
    public class Categories : IRequest<List<GetCatNameDto>>
    {
        public class CategoriesHandler : IRequestHandler<Categories, List<GetCatNameDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CategoriesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<GetCatNameDto>> Handle(Categories query, CancellationToken cancellationToken)
            {

                var adCategories = await _unitOfWork.AdCategories
                    .GetQueryList()
                    .Include(c => c.CategoryCost)
                    .AsNoTracking()
                      .Select(c => new GetCatNameDto()
                      {
                          Id = c.Id,
                          Name = c.Name,
                          Cost = c.CategoryCost.IsActive ? c.CategoryCost.Cost : 0,
                          CreationDate = c.CreationDate
                      })
                    .ToListAsync();
                if (adCategories == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "Category doesn't exists!");
                }

                return adCategories;


            }
        }
    }
}
