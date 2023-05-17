using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.City.Commands
{
    public class UpdateCity : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int ProvinceId { get; set; }

        public bool IsActive { get; set; } = false;
        public class UpdateCityHandler : IRequestHandler<UpdateCity, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateCityHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateCity command, CancellationToken cancellationToken)
            {

                var city = await _unitOfWork.Cities.GetByID(command.Id);
                if (city == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                else
                {
                    city.Name = command.Name;
                    city.Longitude = command.Longitude;
                    city.Latitude = command.Latitude;
                    city.ProvinceId = command.ProvinceId;
                    city.IsActive = command.IsActive;
                    _unitOfWork.Cities.Update(city);
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
}
