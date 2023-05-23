

using Application.Dtos.Common;

using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Neighbourhood.Queries
{
    public class GetNeighbourhoodById : IRequest<GetNeighbourhoodDto>
    {
        public int Id { get; set; }
        public class GetNeighbourhoodByIdHandler : IRequestHandler<GetNeighbourhoodById, GetNeighbourhoodDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetNeighbourhoodByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetNeighbourhoodDto> Handle(GetNeighbourhoodById query, CancellationToken cancellationToken)
            {
                try
                {
                    var city = await _unitOfWork.Neighborhoods
                        .GetQueryList()
                        .AsNoTracking()
                        .Include(c => c.City)
                        .Select(c => new GetNeighbourhoodDto
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Latitude = c.Latitude,
                            Longitude = c.Longitude,
                            IsActive = c.IsActive,
                            City = new GetNameAndId
                            {
                                Id = c.CityId,
                                Name = c.City.Name,
                            },
                            CreationDate = c.CreationDate,
                        })
                        .FirstOrDefaultAsync();
                    if (city == null)
                    {
                        throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                    }
                    return city;
                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");
                }

            }
        }
    }
}
