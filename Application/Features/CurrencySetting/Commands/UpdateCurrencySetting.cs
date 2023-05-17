using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.CurrencySetting.Commands
{
    public class UpdateCurrencySetting : IRequest<int>
    {
        public int Id { get; set; }
        public decimal Buy { get; set; }
        public decimal Sale { get; set; }
        public class UpdateCurrencySettingHandler : IRequestHandler<UpdateCurrencySetting, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateCurrencySettingHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateCurrencySetting command, CancellationToken cancellationToken)
            {

                var currencySetting = await _unitOfWork.CurrencySettings.GetByID(command.Id);
                if (currencySetting == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                currencySetting.Buy = command.Buy;
                currencySetting.Sale = command.Sale;
                currencySetting.UpdatedDate = DateTime.Now;

                _unitOfWork.CurrencySettings.Update(currencySetting);
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
