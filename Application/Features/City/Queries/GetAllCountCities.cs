using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.City.Queries
{
    public class GetAllCountCities : IRequest<int>
    {
        public class GetAllCountCitiesHandler : IRequestHandler<GetAllCountCities, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountCitiesHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountCities query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Cities.GetQueryList()
                        .AsNoTracking().CountAsync();

                }
                catch (Exception err) { throw new Exception("خطا در گرفتن اطلاعات!"); }
            }
        }
    }
}
