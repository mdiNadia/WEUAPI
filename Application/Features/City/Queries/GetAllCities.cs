using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.City.Queries
{
    public class GetAllCities : IRequest<IEnumerable<GetCityDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllCities(IPaginationFilter filter)
        {
            _filter = filter;
        }

        public class GetAllCitiesHandler : IRequestHandler<GetAllCities, IEnumerable<GetCityDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCitiesHandler(IUnitOfWork unitOfWork)
            {

                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetCityDto>> Handle(GetAllCities query, CancellationToken cancellationToken)
            {
                var cities = await _unitOfWork.Cities.GetQueryList()
                    .AsNoTracking()
                    .Include(c => c.Province)
                    .Select(c => new GetCityDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Latitude = c.Latitude,
                        Longitude = c.Longitude,
                        IsActive = c.IsActive,
                        Province = new Dtos.Common.GetNameAndId
                        {
                            Id = c.ProvinceId,
                            Name = c.Province.Name,
                        },
                        CreationDate = c.CreationDate,
                    })
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .ToListAsync();
                if (cities == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "شهری ثبت نشده است!");
                }
                return cities;
            }
        }
    }
}
