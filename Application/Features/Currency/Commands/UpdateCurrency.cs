using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Currency.Commands
{
    public class UpdateCurrency : IRequest<int>
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }

        public class UpdateCurrencyHandler : IRequestHandler<UpdateCurrency, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateCurrencyHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateCurrency command, CancellationToken cancellationToken)
            {

                var currency = await _unitOfWork.Currencies.GetByID(command.Id);

                if (currency == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }
                else
                {
                    currency.CurrencyName = command.CurrencyName;
                    if (command.IsDefault)
                    {
                        var IsAnyDefaultCurrency = await _unitOfWork.Currencies
                            .GetQueryList()
                            .SingleOrDefaultAsync(c => c.IsDefault && c.Id != command.Id);
                        if (IsAnyDefaultCurrency != null)
                        {
                            IsAnyDefaultCurrency.IsDefault = false;
                            _unitOfWork.Currencies.Update(IsAnyDefaultCurrency);
                        }

                    }
                    currency.IsDefault = command.IsDefault;
                    if (command.IsActive)
                    {
                        var IsAnyActiveCurrency = await _unitOfWork.Currencies
                            .GetQueryList()
                            .SingleOrDefaultAsync(c => !c.IsDefault && c.IsActive && c.Id != command.Id);
                        if (IsAnyActiveCurrency != null)
                        {
                            IsAnyActiveCurrency.IsActive = false;
                            _unitOfWork.Currencies.Update(IsAnyActiveCurrency);
                        }

                    }
                    currency.IsActive = command.IsActive;
                    _unitOfWork.Currencies.Update(currency);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return currency.Id;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }

                }


            }
        }
    }
}
