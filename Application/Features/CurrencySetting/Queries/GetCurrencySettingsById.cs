using Application.Dtos.CurrencySetting;
using Application.Errors;
using Application.Interfaces;
using Mapster;
using MediatR;
using System.Net;

namespace Application.Features.CurrencySetting.Queries
{
    public class GetCurrencySettingById : IRequest<GetCurrencySettingDto>
    {
        public int Id { get; set; }
        public class GetCurrencySettingByIdHandler : IRequestHandler<GetCurrencySettingById, GetCurrencySettingDto>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCurrencySettingByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<GetCurrencySettingDto> Handle(GetCurrencySettingById query, CancellationToken cancellationToken)
            {

                var currencySetting = await _unitOfWork.CurrencySettings.GetByID(query.Id);
                if (currencySetting == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                try
                {
                    var result = currencySetting.Adapt<GetCurrencySettingDto>();
                    return result;
                }
                catch (Exception err) { throw new Exception("خطا در گرفتن اطلاعات!"); }



            }
        }
    }
}
