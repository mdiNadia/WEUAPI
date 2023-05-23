using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Neighbourhood.Queries
{
    public class GetAllNeighbourhoods : IRequest<IEnumerable<GetNeighbourhoodDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllNeighbourhoods(IPaginationFilter filter)
        {
            _filter = filter;
        }

        public class GetAllNeighbourhoodsHandler : IRequestHandler<GetAllNeighbourhoods, IEnumerable<GetNeighbourhoodDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllNeighbourhoodsHandler(IUnitOfWork unitOfWork)
            {

                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetNeighbourhoodDto>> Handle(GetAllNeighbourhoods query, CancellationToken cancellationToken)
            {
                try
                {
                    var cities = await _unitOfWork.Neighborhoods.GetQueryList()
                        .AsNoTracking()
                        .Include(c => c.City)
                        .Select(c => new GetNeighbourhoodDto
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Latitude = c.Latitude,
                            Longitude = c.Longitude,
                            IsActive = c.IsActive,

                            City = new Dtos.Common.GetNameAndId
                            {
                                Id = c.CityId,
                                Name = c.City.Name,
                                CreationDate = c.CreationDate,
                            },
                            CreationDate = c.CreationDate
                        })
                        .OrderByDescending(c => c.CreationDate)
                        .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                        .Take(query._filter.PageSize)
                        .ToListAsync();
                    if (cities == null)
                    {
                        throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                    }
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
