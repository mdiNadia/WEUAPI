using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Application.Features.AdCategoryCost.Commands
{
    public class CreateAdCategoryCost : IRequest<int>
    {
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public int AdCategoryId { get; set; }

        public class CreateAdCategoryCostHandler : IRequestHandler<CreateAdCategoryCost, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateAdCategoryCostHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(CreateAdCategoryCost command, CancellationToken cancellationToken)
            {

                var catCost = new Domain.Entities.AdCategoryCost();
                catCost.Cost = command.Cost;
                catCost.AdCategoryId = command.AdCategoryId;
                catCost.CreationDate = DateTime.Now;
                catCost.IsActive = true;
                _unitOfWork.AdCategoryCosts.Insert(catCost);
                try
                {
                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception)
                {
                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی در درج ارزش دسته‌بندی رخ داد، این خطا مربوط به سرویس ارائه دهنده میباشد!");

                }
                var adCategory = await _unitOfWork.AdCategories.GetByID(command.AdCategoryId);
                adCategory.CategoryCost = catCost;
                _unitOfWork.AdCategories.Update(adCategory);
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
