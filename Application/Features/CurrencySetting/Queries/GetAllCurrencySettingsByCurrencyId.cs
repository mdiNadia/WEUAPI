using Application.Dtos.CurrencySetting;
using Application.Errors;
using Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.CurrencySetting.Queries
{
    public class GetAllCurrencySettingsByCurrencyId : IRequest<IEnumerable<GetCurrencySettingDto>>
    {
        private readonly IPaginationFilter _filter;
        private readonly int _id;
        public GetAllCurrencySettingsByCurrencyId(IPaginationFilter filter, int id)
        {
            _filter = filter;
            _id = id;
        }
        public class GetAllCurrencySettingsByCurrencyIdHandler : IRequestHandler<GetAllCurrencySettingsByCurrencyId, IEnumerable<GetCurrencySettingDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCurrencySettingsByCurrencyIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetCurrencySettingDto>> Handle(GetAllCurrencySettingsByCurrencyId query, CancellationToken cancellationToken)
            {

                var currencyList = await _unitOfWork.CurrencySettings.GetQueryList()
                    .Where(c => c.CurrencyId == query._id).AsNoTracking()
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .ToListAsync();
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
