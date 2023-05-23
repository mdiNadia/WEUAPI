using Application.Errors;
using Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Country.Queries
{
    public class GetWhole : IRequest<IEnumerable<GetWholeDto>>
    {
        public int Id { get; set; }
        private readonly IPaginationFilter _filter;
        public GetWhole(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetWholeHandler : IRequestHandler<GetWhole, IEnumerable<GetWholeDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetWholeHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetWholeDto>> Handle(GetWhole query, CancellationToken cancellationToken)
            {
                var data = await _unitOfWork.Countries.GetQueryList()
                      .Include(c => c.Provinces).ThenInclude(c => c.Cities).ThenInclude(c => c.Neighborhoods)
                      .Where(c => c.Id == query.Id)
                      .Select(c => new GetWholeDto()
                      {
                          Id = c.Id,
                          Name = c.Name,
                          Children = c.Provinces.Select(p => new GetAllProvinces()
                          {
                              Id = p.Id,
                              Name = p.Name,
                              CountryId = p.CountryId,
                              CountryName = p.Country.Name,
                              Children = p.Cities.Select(s => new GetAllCities()
                              {
                                  Id = s.Id,
                                  Name = s.Name,
                                  ProvinceId = s.ProvinceId,
                                  ProvinceName = s.Province.Name,
                                  CountryId = s.Province.CountryId,
                                  CountryName = s.Province.Country.Name,
                                  Children = s.Neighborhoods.Select(n => new GetAllNeighbourhoods()
                                  {
                                      Id = n.Id,
                                      Name = n.Name,
                                      CountryId = n.City.Province.CountryId,
                                      CountryName = n.City.Province.Country.Name,
                                      CityId = n.CityId,
                                      CityName = n.City.Name,
                                      ProvinceId = n.City.ProvinceId,
                                      ProvinceName = n.City.Province.Name,

                                  }).ToList(),
                              }).ToList()
                          }).ToList()
                      })
                      .OrderByDescending(c => c.Id)
                      .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                      .Take(query._filter.PageSize)
                      .ToListAsync();
                if (data == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                }
                var result = data.Adapt<IEnumerable<GetWholeDto>>();
                if (result.Any())
                    return result;
                else
                    throw new RestException(HttpStatusCode.BadRequest, "خطایی در نوع اطلاعات برگشتی رخ داد!");



            }
        }
    }
}
