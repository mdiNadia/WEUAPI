using Application.Dtos.Common;
using Application.Errors;
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Order.Queries
{
    public class GetAllUserOrders : IRequest<IEnumerable<GetUserOrdersDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllUserOrders(IPaginationFilter filter)
        {
            _filter = filter;
        }

        public class GetAllUserOrdersHandler : IRequestHandler<GetAllUserOrders, IEnumerable<GetUserOrdersDto>>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly IUnitOfWork _unitOfWork;

            public GetAllUserOrdersHandler(IUserAccessor userAccessor,IUnitOfWork unitOfWork)
            {
                this._userAccessor = userAccessor;
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetUserOrdersDto>> Handle(GetAllUserOrders query, CancellationToken cancellationToken)
            {
                var profile = await _unitOfWork.Profiles.GetQueryList()
                    .FirstAsync(c => c.Username == _userAccessor.GetCurrentUserNameAsync());
                var userOrders = await _unitOfWork.Orders.GetQueryList()
                    .Where(c => c.Profile == profile).ToListAsync();
                var userOrderRows = await _unitOfWork.OrderRows.GetQueryList()
                    .Include(c=>c.Order)
                    .Where(c=> userOrders.Contains(c.Order))
                    .AsNoTracking()
                    .OrderByDescending(c => c.CreationDate)
                    .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                    .Take(query._filter.PageSize)
                    .Select(c => new GetUserOrdersDto()
                    {
                        CreationDate = c.CreationDate,
                        Description = c.Description,
                        Name = c.Name,
                        sign = c.sign,
                        OrderType = c.OrderType,
                    }).ToListAsync();
                if (userOrderRows == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");

                }
                return userOrderRows.AsReadOnly();


            }
        }
    }
}
