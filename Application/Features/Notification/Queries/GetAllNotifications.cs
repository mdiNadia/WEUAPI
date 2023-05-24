
using Application.Errors;
using Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
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
                  //.Include(c => c.Observer)
                  //.ThenInclude(c=>c.Avatar)
                  .Include(c => c.Target).ThenInclude(c=>c.Avatar)
                      .Select(c => new GetNotificationDto()
                      {
                          Id = c.Id,
                          Title = c.Title,
                          Body = c.Body,
                          NotificationType = c.NotificationType,
                          CreationDate = c.CreationDate,
                          AdvertiseId = c.AdvertiseId,
                          ObserverId = c.ObserverId,
                          ObserverUserName = c.Observer.Username,
                          ObserverImage = c.Observer.Avatar.FileName,

                          TargeterId = c.TargetId,
                          TargeterUserName = c.Target.Username,
                          TargeterImage = c.Target.Avatar.FileName,

                      })
                      .OrderByDescending(c => c.CreationDate)
                      .Skip((query._filter.PageNumber - 1) * query._filter.PageSize)
                      .Take(query._filter.PageSize)
                      .ToListAsync();
                    var advertise = _unitOfWork.ConfirmedResults.GetQueryList()
                        .Include(c => c.ConfirmedResultAttachments).ThenInclude(c => c.Attachment)
                        .Where(c=>c.IsActive);
                    model.ForEach(m =>
                    {
                        var f = advertise
                        .Where(c => c.ConfirmedResultAttachments.Select(o => o.ConfirmResultId == m.AdvertiseId).First())
                        .Select(c => c.ConfirmedResultAttachments.First());
                        m.AdvertiseImage = f.Select(e=>e.Attachment.FileName).FirstOrDefault();
                    });
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
