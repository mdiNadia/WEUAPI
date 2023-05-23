using Application.Errors;
using Application.Features.Boost.Queries;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Province.Queries
{
    public class GetAllBoosts : IRequest<IEnumerable<GetBoostDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllBoosts(IPaginationFilter filter)
        {
            _filter = filter;
        }

        public class GetAllBoostsHandler : IRequestHandler<GetAllBoosts, IEnumerable<GetBoostDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllBoostsHandler(IUnitOfWork unitOfWork)
            {

                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetBoostDto>> Handle(GetAllBoosts query, CancellationToken cancellationToken)
            {
                var cities = await _unitOfWork.Boosts.GetQueryList()
                    .AsNoTracking()
                    .Include(c => c.Advertising)
                    .Select(c => new GetBoostDto
                    {

                        Debit = c.Debit,
                        ValuePerVisit = c.ValuePerVisit,
                        NumberOfadViews = c.NumberOfadViews,
                        Advertising = new Dtos.Common.GetNameAndId
                        {
                            Id = c.AdvertisingId,
                            Name = c.Advertising.Name,
                        },
                        CreationDate = c.CreationDate,
                    })
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .ToListAsync();
                if (cities == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                return cities;


            }
        }
    }
}
