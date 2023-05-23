using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Province.Queries
{
    public class GetAllProvinces : IRequest<IEnumerable<GetProvinceDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllProvinces(IPaginationFilter filter)
        {
            _filter = filter;
        }

        public class GetAllProvincesHandler : IRequestHandler<GetAllProvinces, IEnumerable<GetProvinceDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllProvincesHandler(IUnitOfWork unitOfWork)
            {

                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetProvinceDto>> Handle(GetAllProvinces query, CancellationToken cancellationToken)
            {
                var province = await _unitOfWork.Provinces.GetQueryList()
                    .AsNoTracking()
                    .Include(c => c.Country)
                    .Select(c => new GetProvinceDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Latitude = c.Latitude,
                        Longitude = c.Longitude,
                        IsActive = c.IsActive,
                        CreationDate = c.CreationDate,
                        Country = new Dtos.Common.GetNameAndId
                        {
                            Id = c.CountryId,
                            Name = c.Country.Name,
                            CreationDate = c.Country.CreationDate,
                        }
                    })
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize).ToListAsync();
                if (province == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                return province;


            }
        }
    }
}
