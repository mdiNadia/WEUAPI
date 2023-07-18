using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace Application.Features.Province.Queries
{
    public class GetProvinceById : IRequest<GetProvinceDto>
    {
        public int Id { get; set; }
        public class GetProvinceByIdHandler : IRequestHandler<GetProvinceById, GetProvinceDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetProvinceByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetProvinceDto> Handle(GetProvinceById query, CancellationToken cancellationToken)
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
                        CountryId = c.CountryId,
                        CreationDate = c.CreationDate
                    })
                    .OrderByDescending(c => c.CreationDate).FirstOrDefaultAsync();
                if (province == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                return province;


            }
        }
    }
}
