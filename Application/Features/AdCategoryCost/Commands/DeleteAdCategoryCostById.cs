using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.AdCategoryCost.Commands
{
    public class DeleteAdCategoryCostById : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteAdCategoryCostByIdHandler : IRequestHandler<DeleteAdCategoryCostById, string>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteAdCategoryCostByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteAdCategoryCostById command, CancellationToken cancellationToken)
            {

                var catCost = await _unitOfWork.AdCategoryCosts.GetByID(command.Id);
                if (catCost == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                _unitOfWork.AdCategoryCosts.Delete(catCost);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{catCost.Id}";
                }
                catch (Exception err) { throw new Exception("Error occured in saving data in database!"); }
            }
        }
    }
}
