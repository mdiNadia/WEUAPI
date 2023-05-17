using Application.Dtos.CurrencySetting;
using Application.Errors;
using Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.CurrencySetting.Queries
{
    public class GetAllCurrencySettings : IRequest<IEnumerable<GetCurrencySettingDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllCurrencySettings(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetAllCurrencySettingsHandler : IRequestHandler<GetAllCurrencySettings, IEnumerable<GetCurrencySettingDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCurrencySettingsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetCurrencySettingDto>> Handle(GetAllCurrencySettings query, CancellationToken cancellationToken)
            {

                var currencyList = await _unitOfWork.CurrencySettings.GetQueryList().AsNoTracking()
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize).ToListAsync();
                if (currencyList == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                try
                {
                    var result = currencyList.Adapt<IEnumerable<GetCurrencySettingDto>>();
                    return result;
                }
                catch (Exception err) { throw new Exception("خطا در گرفتن اطلاعات!"); }




            }
        }
    }
}
