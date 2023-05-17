using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.Country.Commands
{
    public class DeleteCountryById : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteCountryByIdHandler : IRequestHandler<DeleteCountryById, string>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCountryByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteCountryById command, CancellationToken cancellationToken)
            {

                var country = await _unitOfWork.Countries.GetByID(command.Id);
                if (country == null) throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                if (country.CurrencyId.HasValue) throw new RestException(HttpStatusCode.BadRequest, "برای این کشور ارز مشخص شده است، ابتدا ارز کشور را پاک کنید!");
                _unitOfWork.Countries.Delete(country);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{country.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
