using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User.Queries
{
    public class GetAllCountUsers : IRequest<int>
    {
        public class GetAllCountUsersHandler : IRequestHandler<GetAllCountUsers, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountUsersHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountUsers query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Users.GetQueryList().AsNoTracking().CountAsync();

                }
                catch (Exception err) { throw new Exception("خطا در گرفتن اطلاعات!"); }

            }
        }
    }
}
