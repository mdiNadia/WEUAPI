using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.Neighbourhood.Commands
{
    public class UpdateNeighbourhood : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int CityId { get; set; }

        public bool IsActive { get; set; } = false;
        public class UpdateNeighbourhoodHandler : IRequestHandler<UpdateNeighbourhood, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateNeighbourhoodHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateNeighbourhood command, CancellationToken cancellationToken)
            {

                var neighbourhood = await _unitOfWork.Neighborhoods.GetByID(command.Id);

                if (neighbourhood == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }
                else
                {
                    neighbourhood.Name = command.Name;
                    neighbourhood.Longitude = command.Longitude;
                    neighbourhood.Latitude = command.Latitude;
                    neighbourhood.CityId = command.CityId;
                    neighbourhood.IsActive = command.IsActive;
                    _unitOfWork.Neighborhoods.Update(neighbourhood);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return neighbourhood.Id;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }

                }


            }
        }
    }
}
