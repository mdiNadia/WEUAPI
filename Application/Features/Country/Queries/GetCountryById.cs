using Application.Dtos.Country;
using Application.Errors;
using Application.Interfaces;
using Mapster;
using MediatR;
using System.Net;

namespace Application.Features.Country.Queries
{
    public class GetCountryById : IRequest<GetCountryDto>
    {
        public int Id { get; set; }
        public class GetCountryByIdHandler : IRequestHandler<GetCountryById, GetCountryDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCountryByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetCountryDto> Handle(GetCountryById query, CancellationToken cancellationToken)
            {

                var country = await _unitOfWork.Countries.GetByID(query.Id);
                if (country == null) throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                var result = country.Adapt<GetCountryDto>();
                if (result != null)
                    return result;
                else
                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، این خطا مربوط به سرویس ارائه دهنده میباشد!");
            }
        }
    }
}
