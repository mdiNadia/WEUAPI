using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.CurrencySetting.Commands
{
    public class DeleteCurrencySettingById : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteCurrencySettingByIdHandler : IRequestHandler<DeleteCurrencySettingById, string>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCurrencySettingByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteCurrencySettingById command, CancellationToken cancellationToken)
            {

                var currencySetting = await _unitOfWork.CurrencySettings.GetByID(command.Id);
                if (currencySetting == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                _unitOfWork.CurrencySettings.Delete(currencySetting);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{currencySetting.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }





            }
        }
    }
}
