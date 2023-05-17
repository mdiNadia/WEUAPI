using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;

namespace Application.Features.Neighbourhood.Commands
{
    public class CreateNeighbourhood : IRequest<int>
    {


        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int CityId { get; set; }

        public bool IsActive { get; set; } = false;
        public class CreateNeighbourhoodHandler : IRequestHandler<CreateNeighbourhood, int>
        {
            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFileUploader _fileUploader;

            public CreateNeighbourhoodHandler(IMediator mediator, IUnitOfWork unitOfWork, IFileUploader fileUploader)
            {
                this._mediator = mediator;
                this._unitOfWork = unitOfWork;
                this._fileUploader = fileUploader;
            }

            public async Task<int> Handle(CreateNeighbourhood command, CancellationToken cancellationToken)
            {

                var neighbourhood = new Domain.Entities.Neighborhood();
                neighbourhood.Name = command.Name;
                neighbourhood.Latitude = command.Latitude;
                neighbourhood.Longitude = command.Longitude;
                neighbourhood.CityId = command.CityId;
                neighbourhood.IsActive = command.IsActive;
                neighbourhood.CreationDate = DateTime.Now;
                _unitOfWork.Neighborhoods.Insert(neighbourhood);
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
