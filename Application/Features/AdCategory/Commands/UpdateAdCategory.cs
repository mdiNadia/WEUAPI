using Application.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.AdCategory.Commands
{
    public class UpdateAdCategory : IRequest<int>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public class UpdateAdCategoryHandler : IRequestHandler<UpdateAdCategory, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateAdCategoryHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateAdCategory command, CancellationToken cancellationToken)
            {
                var adCategory = await _unitOfWork.AdCategories.GetByID(command.Id);

                if (adCategory == null)
                {
                    return default;
                }
                else
                {
                    adCategory.Name = command.Name;
                    adCategory.Description = command.Description;
                    adCategory.ParentId = command.ParentId == 0 ? null : command.ParentId;


                    _unitOfWork.AdCategories.Update(adCategory);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return adCategory.Id;
                    }
                    catch (Exception err) { throw new Exception("Error occured in saving data in database!"); }

                }


            }
        }
    }
}
