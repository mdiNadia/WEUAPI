using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AdCategory.Queries
{
    public class GetAllCountAdCategories : IRequest<int>
    {
        public class GetAllCountAdCategoriesHandler : IRequestHandler<GetAllCountAdCategories, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountAdCategoriesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountAdCategories query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.AdCategories.GetQueryList().AsNoTracking().CountAsync();

                }
                catch (Exception err) { throw new Exception("Error occured in saving data in database!"); }

            }
        }
    }
}
