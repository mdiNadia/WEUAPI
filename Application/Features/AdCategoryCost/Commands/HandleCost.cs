using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.AdCategoryCost.Commands
{
    public class HandleCost : IRequest<int>
    {
        public int Id { get; set; }
        public class ActiveAdCategoryCostHandler : IRequestHandler<HandleCost, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public ActiveAdCategoryCostHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(HandleCost command, CancellationToken cancellationToken)
            {
                var catCost = await _unitOfWork.AdCategoryCosts.GetByID(command.Id);

                if (catCost == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                else
                {
                    catCost.IsActive = !catCost.IsActive;
                    catCost.UpdatedDate = DateTime.Now;
                    _unitOfWork.AdCategoryCosts.Update(catCost);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return catCost.Id;
                    }
                    catch (Exception err) { throw new Exception("Error occured in saving data in database!"); }

                }


            }
        }
    }
}
