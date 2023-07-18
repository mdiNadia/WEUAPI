using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.City.Queries
{
    public class GetCityById : IRequest<GetCityDto>
    {
        public int Id { get; set; }
        public class GetCityByIdHandler : IRequestHandler<GetCityById, GetCityDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCityByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetCityDto> Handle(GetCityById query, CancellationToken cancellationToken)
            {

                var city = await _unitOfWork.Cities.GetQueryList()
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
                    })
                    .FirstOrDefaultAsync();
                if (city == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "شهری با این اطلاعات وجود ندارد!");
                }
                return city;


            }
        }
    }
}
