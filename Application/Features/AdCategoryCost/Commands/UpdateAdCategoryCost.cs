using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Application.Features.AdCategoryCost.Commands
{

    public class UpdateAdCategoryCost : IRequest<int>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public decimal Cost { get; set; }

        public class UpdateAdCategoryCostHandler : IRequestHandler<UpdateAdCategoryCost, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateAdCategoryCostHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateAdCategoryCost command, CancellationToken cancellationToken)
            {

                var catCost = await _unitOfWork.AdCategoryCosts.GetByID(command.Id);

                if (catCost == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                }
                else
                {
                    catCost.Cost = command.Cost;
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
