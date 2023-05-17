using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;

namespace Application.Features.City.Commands
{
    public class CreateCity : IRequest<int>
    {
        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int ProvinceId { get; set; }
        public bool IsActive { get; set; } = false;
        public class CreateCityHandler : IRequestHandler<CreateCity, int>
        {
            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFileUploader _fileUploader;

            public CreateCityHandler(IMediator mediator, IUnitOfWork unitOfWork, IFileUploader fileUploader)
            {
                this._mediator = mediator;
                this._unitOfWork = unitOfWork;
                this._fileUploader = fileUploader;
            }

            public async Task<int> Handle(CreateCity command, CancellationToken cancellationToken)
            {
                var city = new Domain.Entities.City();
                city.Name = command.Name;
                city.Latitude = command.Latitude;
                city.Longitude = command.Longitude;
                city.ProvinceId = command.ProvinceId;
                city.IsActive = command.IsActive;
                city.CreationDate = DateTime.Now;
                _unitOfWork.Cities.Insert(city);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return city.Id;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
