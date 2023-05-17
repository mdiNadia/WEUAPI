using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Currency.Commands
{
    public class CreateCurrency : IRequest<int>
    {
        //از بین لیستی از کشورها انتخاب میکند
        public int CountryId { get; set; }
        public string CurrencyName { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public class CreateCurrencyHandler : IRequestHandler<CreateCurrency, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateCurrencyHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(CreateCurrency command, CancellationToken cancellationToken)
            {

                var country = await _unitOfWork.Countries.GetByID(command.CountryId);

                if (!country.CurrencyId.HasValue)
                {
                    var currency = new Domain.Entities.Currency();
                    currency.CurrencyName = command.CurrencyName;
                    if (command.IsDefault)
                    {
                        var IsAnyDefaultCurrency = await _unitOfWork.Currencies.GetQueryList().SingleOrDefaultAsync(c => c.IsDefault);
                        if (IsAnyDefaultCurrency != null)
                        {
                            IsAnyDefaultCurrency.IsDefault = false;
                            _unitOfWork.Currencies.Update(IsAnyDefaultCurrency);
                        }

                    }
                    currency.IsDefault = command.IsDefault;

                    if (command.IsActive)
                    {
                        var IsAnyActiveCurrency = await _unitOfWork.Currencies.GetQueryList().SingleOrDefaultAsync(c => !c.IsDefault && c.IsActive);
                        if (IsAnyActiveCurrency != null)
                        {
                            IsAnyActiveCurrency.IsActive = false;
                            _unitOfWork.Currencies.Update(IsAnyActiveCurrency);
                        }

                    }
                    currency.IsActive = command.IsActive;
                    currency.CreationDate = DateTime.Now;
                    _unitOfWork.Currencies.Insert(currency);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                    }
                    catch (Exception)
                    {

                        throw new RestException(HttpStatusCode.InternalServerError, "خطایی در درج واحدپول رخ داد، خطا مربوط به سرویس ارائه دهنده میباشد!");

                    }

                    country.CurrencyId = currency.Id;
                    _unitOfWork.Countries.Update(country);

                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return currency.Id;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }

                }

                throw new RestException(HttpStatusCode.BadRequest, "واحدپول هم اکنون وجود دارد!");




            }
        }
    }
}
