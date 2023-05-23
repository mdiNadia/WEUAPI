
using Application.Errors;
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Order.Queries
{
    public class GetAllUserOrdersCount : IRequest<int>
    {
        public class GetAllUserOrdersCountHandler : IRequestHandler<GetAllUserOrdersCount, int>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly IUnitOfWork _unitOfWork;

            public GetAllUserOrdersCountHandler(IUserAccessor userAccessor,IUnitOfWork unitOfWork)
            {
                this._userAccessor = userAccessor;
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllUserOrdersCount query, CancellationToken cancellationToken)
            {
                try
                {
                    var profile = await _unitOfWork.Profiles.GetQueryList()
                   .FirstAsync(c => c.Username == _userAccessor.GetCurrentUserNameAsync());
                    var userOrders = await _unitOfWork.Orders.GetQueryList()
                        .Where(c => c.Profile == profile).ToListAsync();
                    var userOrderRows = await _unitOfWork.OrderRows.GetQueryList()
                        .Include(c => c.Order)
                        .Where(c => userOrders.Contains(c.Order)).CountAsync();
                       
                    return await _unitOfWork.OrderRows.GetQueryList().AsNoTracking().CountAsync();

                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }
            }
        }
    }
}
