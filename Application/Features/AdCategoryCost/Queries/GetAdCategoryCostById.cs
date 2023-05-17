using Application.Dtos.AdCategoryCost;
using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.AdCategoryCost.Queries
{
    public class GetAdCategoryCostById : IRequest<GetAdCatCostDto>
    {
        public int Id { get; set; }
        public class GetAdCategoryCostByIdHandler : IRequestHandler<GetAdCategoryCostById, GetAdCatCostDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAdCategoryCostByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetAdCatCostDto> Handle(GetAdCategoryCostById query, CancellationToken cancellationToken)
            {
                var catCost = await _unitOfWork.AdCategoryCosts
                    .GetQueryList()
                    .AsNoTracking()
                    .Where(c => c.Id == query.Id)
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

                    }).SingleOrDefaultAsync();
                if (catCost == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                return catCost;


            }
        }
    }
}
