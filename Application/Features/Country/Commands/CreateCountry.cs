using Application.Interfaces;
using MediatR;

namespace Application.Features.Country.Commands
{
    public class CreateCountry : IRequest<int>
    {

        public string Iso { get; set; }
        public string Name { get; set; }
        public string? Iso3 { get; set; }
        public int? NumCode { get; set; }
        public int? PhoneCode { get; set; }
        public bool IsActive { get; set; }
        public class CreateCountryHandler : IRequestHandler<CreateCountry, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateCountryHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(CreateCountry command, CancellationToken cancellationToken)
            {
                var country = new Domain.Entities.Country();
                country.Iso = command.Iso;
                country.Name = command.Name;
                country.Iso3 = command.Iso3;
                country.NumCode = command.NumCode;
                country.PhoneCode = command.PhoneCode;
                country.IsActive = command.IsActive;
                country.CreationDate = DateTime.Now;
                _unitOfWork.Countries.Insert(country);
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
