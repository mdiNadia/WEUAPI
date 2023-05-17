using Application.Dtos.Country;
using Application.Errors;
using Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Country.Queries
{
    public class GetAllCountries : IRequest<IEnumerable<GetCountryDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllCountries(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetAllCountriesHandler : IRequestHandler<GetAllCountries, IEnumerable<GetCountryDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountriesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetCountryDto>> Handle(GetAllCountries query, CancellationToken cancellationToken)
            {

                var countryList = await _unitOfWork.Countries.GetQueryList()
                    .AsNoTracking()
                    .Include(c => c.Currency)
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .ToListAsync();
                if (countryList == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                }
                var result = countryList.Adapt<IEnumerable<GetCountryDto>>();
                if (result.Any())
                    return result;
                else
                    throw new RestException(HttpStatusCode.BadRequest, "خطایی در نوع اطلاعات برگشتی رخ داد!");



            }
        }
    }
}
