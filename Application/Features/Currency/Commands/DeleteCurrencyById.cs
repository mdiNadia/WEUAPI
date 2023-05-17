using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.Currency.Commands
{
    public class DeleteCurrencyById : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteCurrencyByIdHandler : IRequestHandler<DeleteCurrencyById, string>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCurrencyByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteCurrencyById command, CancellationToken cancellationToken)
            {

                var currency = await _unitOfWork.Currencies.GetByID(command.Id);
                if (currency == null) throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                if (currency.IsActive) throw new RestException(HttpStatusCode.BadRequest, "این واحد پول فعال است و برای تبدیل از پیش فرض استفاده میشود!");
                if (currency.IsDefault) throw new RestException(HttpStatusCode.BadRequest, "واحد پول پیش فرض را تغییر دهید!");
                _unitOfWork.Currencies.Delete(currency);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{currency.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }



            }
        }
    }
}
