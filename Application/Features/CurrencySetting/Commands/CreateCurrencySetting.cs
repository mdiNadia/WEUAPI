using Application.Interfaces;
using MediatR;

namespace Application.Features.CurrencySetting.Commands
{
    public class CreateCurrencySetting : IRequest<int>
    {
        public decimal Buy { get; set; }
        public decimal Sale { get; set; }
        public int CurrencyId { get; set; }
        public class CreateCurrencySettingHandler : IRequestHandler<CreateCurrencySetting, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateCurrencySettingHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(CreateCurrencySetting command, CancellationToken cancellationToken)
            {
                var currencySetting = new Domain.Entities.CurrencySetting();
                currencySetting.CurrencyId = command.CurrencyId;
                currencySetting.Buy = command.Buy;
                currencySetting.Sale = command.Sale;
                currencySetting.CreationDate = DateTime.Now;
                currencySetting.UpdatedDate = DateTime.Now;

                _unitOfWork.CurrencySettings.Insert(currencySetting);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return currencySetting.Id;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }

            }
        }
    }
}
