using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;

namespace Application.Features.Province.Commands
{
    public class CreateProvince : IRequest<int>
    {

        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; } = false;
        public class CreateProvinceHandler : IRequestHandler<CreateProvince, int>
        {
            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFileUploader _fileUploader;

            public CreateProvinceHandler(IMediator mediator, IUnitOfWork unitOfWork, IFileUploader fileUploader)
            {
                this._mediator = mediator;
                this._unitOfWork = unitOfWork;
                this._fileUploader = fileUploader;
            }

            public async Task<int> Handle(CreateProvince command, CancellationToken cancellationToken)
            {
                var province = new Domain.Entities.Province();
                province.Name = command.Name;
                province.Latitude = command.Latitude;
                province.Longitude = command.Longitude;
                province.CountryId = command.CountryId;
                province.CreationDate = DateTime.Now;
                province.IsActive = command.IsActive;
                _unitOfWork.Provinces.Insert(province);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return province.Id;
                }
                catch (Exception err)
                {

                    throw new Exception("خطا در ذخیره اطلاعات!");

                }
            }
        }
    }
}
