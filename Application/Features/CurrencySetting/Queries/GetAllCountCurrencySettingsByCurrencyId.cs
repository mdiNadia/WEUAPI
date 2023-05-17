using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.CurrencySetting.Queries
{
    public class GetAllCountCurrencySettingsByCurrencyId : IRequest<int>
    {
        public int id { get; set; }
        public class GetAllCountCurrencySettingsByCurrencyIdHandler : IRequestHandler<GetAllCountCurrencySettingsByCurrencyId, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountCurrencySettingsByCurrencyIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountCurrencySettingsByCurrencyId query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.CurrencySettings.GetQueryList()
                                .Where(c => c.CurrencyId == query.id)
                                .AsNoTracking().CountAsync();
                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }

            }
        }
    }
}
