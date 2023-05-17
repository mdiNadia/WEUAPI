using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Country.Queries
{
    public class Countries : IRequest<List<GetNameAndId>>
    {

        public class CountriesHandler : IRequestHandler<Countries, List<GetNameAndId>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CountriesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<List<GetNameAndId>> Handle(Countries query, CancellationToken cancellationToken)
            {

                var countries = await _unitOfWork.Countries
                    .GetQueryList()
                    .AsNoTracking()
                    .Select(c => new GetNameAndId
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CreationDate = c.CreationDate,
                    })
                    .OrderByDescending(c => c.CreationDate)
                    .ToListAsync();
                if (countries == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                }
                return countries;


            }
        }
    }
}
