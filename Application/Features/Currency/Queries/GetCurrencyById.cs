using Application.Dtos.Currency;
using Application.Errors;
using Application.Interfaces;
using Mapster;
using MediatR;
using System.Net;

namespace Application.Features.Currency.Queries
{
    public class GetCurrencyById : IRequest<GetCurrencyDto>
    {
        public int Id { get; set; }
        public class GetCurrencyByIdHandler : IRequestHandler<GetCurrencyById, GetCurrencyDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCurrencyByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetCurrencyDto> Handle(GetCurrencyById query, CancellationToken cancellationToken)
            {

                var currency = await _unitOfWork.Currencies.GetByID(query.Id);
                if (currency == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                try
                {
                    var result = currency.Adapt<GetCurrencyDto>();
                    return result;
                }
                catch (Exception err) { throw new Exception("خطا در گرفتن اطلاعات!"); }




            }
        }
    }
}
