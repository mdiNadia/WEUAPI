using Application.Dtos.Lookup;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Country.Queries
{
    public class Countries : IRequest<IQueryable<LookupDto>>
    {

        public class CountriesHandler : IRequestHandler<Countries, IQueryable<LookupDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CountriesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IQueryable<LookupDto>> Handle(Countries query, CancellationToken cancellationToken)
            {

                var countries = _unitOfWork.Countries
                    .GetQueryList()
                    .AsNoTracking()
                    .Select(c => new LookupDto
                    {
                        Id = c.Id,
                        Title = c.Name,
                        //CreationDate = c.CreationDate,
                    });

                return countries;

            }
        }
    }
}
