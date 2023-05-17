using Application.Dtos.AdCategoryCost;
using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.AdCategoryCost.Queries
{
    public class GetAllAdCategoryCosts : IRequest<IEnumerable<GetAdCatCostDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllAdCategoryCosts(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetAllAdCategoryCostsHandler : IRequestHandler<GetAllAdCategoryCosts, IEnumerable<GetAdCatCostDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllAdCategoryCostsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetAdCatCostDto>> Handle(GetAllAdCategoryCosts query, CancellationToken cancellationToken)
            {
                var catCost = await _unitOfWork.AdCategoryCosts
                   .GetQueryList().AsNoTracking()
                   .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                   .Take(query._filter.PageSize)
                   .Include(c => c.AdCategory)
                   .Select(c => new GetAdCatCostDto()
                   {
                       Id = c.Id,
                       Cost = c.Cost,
                       CreationDate = c.CreationDate,
                       UpdatedDate = c.UpdatedDate,
                       IsActive = c.IsActive,
                       AdCategory = new GetNameAndId()
                       {
                           Id = c.AdCategoryId,
                           Name = c.AdCategory.Name
                       }

                   })
                 .OrderByDescending(c => c.CreationDate)
                .ToListAsync();
                if (catCost == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }

                return catCost;


            }
        }
    }
}
