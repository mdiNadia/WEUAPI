using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.Neighbourhood.Commands
{
    public class DeleteNeighbourhoodById : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteNeighbourhoodByIdHandler : IRequestHandler<DeleteNeighbourhoodById, string>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteNeighbourhoodByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteNeighbourhoodById command, CancellationToken cancellationToken)
            {

                var neighbourhood = await _unitOfWork.Neighborhoods.GetByID(command.Id);
                if (neighbourhood == null) throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                _unitOfWork.Countries.Delete(neighbourhood);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{neighbourhood.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }



            }
        }
    }
}
