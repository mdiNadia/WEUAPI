using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.Province.Commands
{
    public class UpdateProvince : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; } = false;
        public class UpdateProvinceHandler : IRequestHandler<UpdateProvince, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateProvinceHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateProvince command, CancellationToken cancellationToken)
            {

                var province = await _unitOfWork.Provinces.GetByID(command.Id);
                if (province == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }
                else
                {
                    province.Name = command.Name;
                    province.Longitude = command.Longitude;
                    province.Latitude = command.Latitude;
                    province.CountryId = command.CountryId;
                    province.IsActive = command.IsActive;
                    _unitOfWork.Provinces.Update(province);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return province.Id;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }

                }
            }
        }
    }
}
