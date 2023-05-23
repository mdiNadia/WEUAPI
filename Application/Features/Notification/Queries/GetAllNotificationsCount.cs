using Application.Errors;
using Application.Interfaces;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Notification.Queries
{
    public class GetAllCountNotifications : IRequest<int>
    {
        public class GetAllCountNotificationsHandler : IRequestHandler<GetAllCountNotifications, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountNotificationsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountNotifications query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.Notifications.GetQueryList().AsNoTracking().CountAsync();

                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }
            }
        }
    }
}
