using Application.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.AdCategory.Commands
{
    public class CreateAdCategory : IRequest<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int? ParentId { get; set; }

        public class CreateAdCategoryHandler : IRequestHandler<CreateAdCategory, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateAdCategoryHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(CreateAdCategory command, CancellationToken cancellationToken)
            {
                var adCategory = new Domain.Entities.AdCategory();
                adCategory.Name = command.Name;
                adCategory.Description = command.Description;
                adCategory.ParentId = command.ParentId == 0 ? null : command.ParentId;
                adCategory.CreationDate = DateTime.Now;
                _unitOfWork.AdCategories.Insert(adCategory);
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
