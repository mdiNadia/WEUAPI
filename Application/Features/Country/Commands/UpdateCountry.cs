using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.Country.Commands
{
    public class UpdateCountry : IRequest<int>
    {
        public int Id { get; set; }
        public string Iso { get; set; }
        public string Name { get; set; }
        public string? Iso3 { get; set; }
        public int? NumCode { get; set; }
        public int? PhoneCode { get; set; }
        public bool IsActive { get; set; }
        public class UpdateCountryHandler : IRequestHandler<UpdateCountry, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateCountryHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateCountry command, CancellationToken cancellationToken)
            {

                var country = await _unitOfWork.Countries.GetByID(command.Id);

                if (country == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                }
                else
                {
                    country.PhoneCode = command.PhoneCode;
                    country.NumCode = command.NumCode;
                    country.Name = command.Name;
                    country.Iso = command.Iso;
                    country.Iso3 = command.Iso3;
                    country.IsActive = command.IsActive;
                    _unitOfWork.Countries.Update(country);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return country.Id;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }

                }
            }
        }
    }
}
