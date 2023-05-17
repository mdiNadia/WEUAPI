using Application.Dtos.Currency;
using Application.Errors;
using Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Currency.Queries
{
    public class GetAllCurrencies : IRequest<IEnumerable<GetCurrencyDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllCurrencies(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetAllCurrenciesHandler : IRequestHandler<GetAllCurrencies, IEnumerable<GetCurrencyDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCurrenciesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetCurrencyDto>> Handle(GetAllCurrencies query, CancellationToken cancellationToken)
            {

                var currencyList = await _unitOfWork.Currencies.GetQueryList().AsNoTracking()
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .ToListAsync();
                if (currencyList == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                var result = currencyList.Adapt<IEnumerable<GetCurrencyDto>>();
                return result;


            }
        }
    }
}
