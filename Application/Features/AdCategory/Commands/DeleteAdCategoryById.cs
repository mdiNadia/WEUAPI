using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.AdCategory.Commands
{
    public class DeleteAdCategoryById : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteAdCategoryByIdHandler : IRequestHandler<DeleteAdCategoryById, string>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteAdCategoryByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteAdCategoryById command, CancellationToken cancellationToken)
            {

                var adCategory = await _unitOfWork.AdCategories.GetByID(command.Id);
                if (adCategory == null) throw new RestException(HttpStatusCode.BadRequest, "Category doesn't exists!");
                var CheckIfHasChildren = await _unitOfWork.AdCategories.CheckIfHasChildren(command.Id);
                if (CheckIfHasChildren) throw new RestException(HttpStatusCode.BadRequest, "Parent has children!");
                _unitOfWork.AdCategories.Delete(adCategory);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{adCategory.Id}";
                }
                catch (Exception err) { throw new Exception("Error occured in saving data in database!"); }


            }
        }
    }
}
