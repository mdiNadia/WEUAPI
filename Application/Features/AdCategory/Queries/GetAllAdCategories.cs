using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AdCategory.Queries
{
    public class GetAllAdCategories : IRequest<IQueryable<GetAdCategoryDto>>
    {
        public GetAllAdCategories()
        {
        }
        public class GetAllAdCategoriesHandler : IRequestHandler<GetAllAdCategories, IQueryable<GetAdCategoryDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllAdCategoriesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IQueryable<GetAdCategoryDto>> Handle(GetAllAdCategories query, CancellationToken cancellationToken)
            {
                var adCategoryList = _unitOfWork.AdCategories.GetQueryList()
                    .AsNoTracking()
                    .Include(c => c.Parent).ThenInclude(c => c.Children)
                    .Include(c => c.CategoryCost)
                    .Select(c => new GetAdCategoryDto()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        CreationDate = c.CreationDate,
                        ParentId = c.ParentId ?? 0,
                        ParentName = c.Parent != null ? c.Parent.Name : "بدون دسته‌بندی",
                        CostId = c.CategoryCostId ?? 0,
                        IsActiveCost = c.CategoryCost != null ? c.CategoryCost.IsActive : false,
                        Children = c.Children.Select(c1 => new GetAdCategoryDto
                        {
                            Id = c1.Id,
                            Name = c1.Name,
                            Description = c1.Description,
                            CreationDate = c1.CreationDate,
                            ParentId = c1.ParentId ?? 0,
                            ParentName = c1.Parent != null ? c1.Parent.Name : "بدون دسته‌بندی",
                            CostId = c1.CategoryCostId ?? 0,
                            IsActiveCost = c1.CategoryCost != null ? c1.CategoryCost.IsActive : false,
                            Children = c1.Children.Select(c2 => new GetAdCategoryDto
                            {
                                Id = c2.Id,
                                Name = c2.Name,
                                Description = c2.Description,
                                CreationDate = c2.CreationDate,
                                ParentId = c2.ParentId ?? 0,
                                ParentName = c2.Parent != null ? c2.Parent.Name : "بدون دسته‌بندی",
                                CostId = c2.CategoryCostId ?? 0,
                                IsActiveCost = c2.CategoryCost != null ? c2.CategoryCost.IsActive : false,
                            }).ToList()
                        }).ToList()

                    })
                    .OrderByDescending(c => c.CreationDate);


                return adCategoryList;
            }
        }
    }
}
