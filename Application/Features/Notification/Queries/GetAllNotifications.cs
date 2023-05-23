using Application.Dtos.Language;
using Application.Errors;
using Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Notification.Queries
{
    public class GetAllNotifications : IRequest<IEnumerable<GetNotificationDto>>
    {
        private readonly IPaginationFilter _filter;
        public GetAllNotifications(IPaginationFilter filter)
        {
            _filter = filter;
        }
        public class GetAllNotificationsHandler : IRequestHandler<GetAllNotifications, IEnumerable<GetNotificationDto>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllNotificationsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<GetNotificationDto>> Handle(GetAllNotifications query, CancellationToken cancellationToken)
            {

                try
                {
                    var model = await _unitOfWork.Notifications.GetQueryList()
                  .AsNoTracking()
                      .Select(c => new GetNotificationDto()
                      {
                          Id = c.Id,
                          Title = c.Title,
                          Body = c.Body,
                          NotificationType = c.NotificationType,
                          CreationDate = c.CreationDate,

                      })
                      .OrderByDescending(c => c.CreationDate)
                      .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                      .Take(query._filter.PageSize)
                      .ToListAsync();
                    return model;
                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی در گرفتن اطلاعات زبان رخ داد، این خطا مربوط به سرویس ارائه‌دهنده میباشد!");

                }
            }
        }
    }
}
