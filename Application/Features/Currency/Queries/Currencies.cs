using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Currency.Queries
{
    public class Currencies : IRequest<List<GetNameAndId>>
    {

        public class CurrenciesHandler : IRequestHandler<Currencies, List<GetNameAndId>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CurrenciesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<GetNameAndId>> Handle(Currencies query, CancellationToken cancellationToken)
            {

                var currencies = await _unitOfWork.Currencies
                    .GetQueryList().AsNoTracking()
                    .Select(c => new GetNameAndId
                    {
                        Id = c.Id,
                        Name = c.CurrencyName,
                        CreationDate = c.CreationDate,
                    })
                    .OrderByDescending(c => c.CreationDate)
                    .ToListAsync();
                if (currencies == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }
                return currencies;


            }
        }
    }
}
