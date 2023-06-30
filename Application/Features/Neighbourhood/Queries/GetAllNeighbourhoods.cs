using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Neighbourhood.Queries
{
    public class GetAllNeighbourhoods : IRequest<IQueryable<GetNeighbourhoodDto>>
    {
        public GetAllNeighbourhoods()
        {
        }

        public class GetAllNeighbourhoodsHandler : IRequestHandler<GetAllNeighbourhoods, IQueryable<GetNeighbourhoodDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllNeighbourhoodsHandler(IUnitOfWork unitOfWork)
            {

                this._unitOfWork = unitOfWork;
            }
            public async Task<IQueryable<GetNeighbourhoodDto>> Handle(GetAllNeighbourhoods query, CancellationToken cancellationToken)
            {
                try
                {
                    var cities = _unitOfWork.Neighborhoods.GetQueryList()
                        .AsNoTracking()
                        .Include(c => c.City)
                        .Select(c => new GetNeighbourhoodDto
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Latitude = c.Latitude,
                            Longitude = c.Longitude,
                            IsActive = c.IsActive,
                            CityId = c.CityId,
                            CreationDate = c.CreationDate
                        });
                    return cities;
                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }

            }
        }
    }
}
