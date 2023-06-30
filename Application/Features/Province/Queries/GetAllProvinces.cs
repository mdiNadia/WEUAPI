using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Province.Queries
{
    public class GetAllProvinces : IRequest<IQueryable<GetProvinceDto>>
    {
        public GetAllProvinces()
        {
        }

        public class GetAllProvincesHandler : IRequestHandler<GetAllProvinces, IQueryable<GetProvinceDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllProvincesHandler(IUnitOfWork unitOfWork)
            {

                this._unitOfWork = unitOfWork;
            }
            public async Task<IQueryable<GetProvinceDto>> Handle(GetAllProvinces query, CancellationToken cancellationToken)
            {
                var province = _unitOfWork.Provinces.GetQueryList()
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
                        CountryId = c.CountryId

                    });
                if (province == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                return province;


            }
        }
    }
}
