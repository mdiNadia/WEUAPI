using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Country.Queries
{
    public class GetAllCountries : IRequest<IQueryable<GetCountryDto>>
    {
        public GetAllCountries()
        {
        }
        public class GetAllCountriesHandler : IRequestHandler<GetAllCountries, IQueryable<GetCountryDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountriesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IQueryable<GetCountryDto>> Handle(GetAllCountries query, CancellationToken cancellationToken)
            {

                var countryList = _unitOfWork.Countries.GetQueryList().AsNoTracking().Select(c => new GetCountryDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Iso3 = c.Iso3,
                    CurrencyId = c.CurrencyId,
                    IsActive = c.IsActive,
                    Iso = c.Iso,
                    NumCode = c.NumCode,
                    CreationDate = c.CreationDate,
                });
                return countryList;



            }
        }
    }
}
