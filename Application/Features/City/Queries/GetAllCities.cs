using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.City.Queries
{
    public class GetAllCities : IRequest<IQueryable<GetCityDto>>
    {
        public GetAllCities()
        {
        }

        public class GetAllCitiesHandler : IRequestHandler<GetAllCities, IQueryable<GetCityDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCitiesHandler(IUnitOfWork unitOfWork)
            {

                this._unitOfWork = unitOfWork;
            }
            public async Task<IQueryable<GetCityDto>> Handle(GetAllCities query, CancellationToken cancellationToken)
            {
                var cities = _unitOfWork.Cities.GetQueryList()
                    .AsNoTracking()
                    .Include(c => c.Province)
                    .Select(c => new GetCityDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Latitude = c.Latitude,
                        Longitude = c.Longitude,
                        IsActive = c.IsActive,
                        ProvinceId = c.ProvinceId,
                        Province = new Dtos.Common.GetNameAndId
                        {
                            Id = c.ProvinceId,
                            Name = c.Province.Name,
                        },
                        CreationDate = c.CreationDate,
                    });
                return cities;
            }
        }
    }
}
