
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Notification.Queries
{
    public class GetNotificationById : IRequest<GetNotificationDto>
    {
        public int Id { get; set; }
        public class GetNotificationByIdHandler : IRequestHandler<GetNotificationById, GetNotificationDto>
        {

            private readonly IUnitOfWork _unitOfWork;

            public GetNotificationByIdHandler(IUnitOfWork unitOfWork)
            {

                this._unitOfWork = unitOfWork;
            }
            public async Task<GetNotificationDto> Handle(GetNotificationById query, CancellationToken cancellationToken)
            {

                var model = await _unitOfWork.Notifications.GetQueryList()
                    .Where(c => c.Id == query.Id)
                    //.Include(c => c.Observer).ThenInclude(c => c.Avatar)
                    .Include(c => c.Target).ThenInclude(c => c.Avatar)
                    .Select(c => new GetNotificationDto()
                    {
                        Id = query.Id,
                        CreationDate = c.CreationDate,
                        Body = c.Body,
                        NotificationType = c.NotificationType,
                        Title = c.Title,
                        ObserverId = c.ObserverId,
                        ObserverUserName = c.Observer.Username,
                        ObserverImage = c.Observer.Avatar.FileName,

                        TargeterId = c.TargetId,
                        TargeterUserName = c.Target.Username,
                        TargeterImage = c.Target.Avatar.FileName,

                    })
                   .FirstOrDefaultAsync();

                ////////////////get images of advertise//////////////////////
                var advertise = _unitOfWork.ConfirmedResults.GetQueryList()
                       .Include(c => c.ConfirmedResultAttachments).ThenInclude(c => c.Attachment)
                       .Where(c => c.IsActive);
       
                    var f = advertise
                    .Where(c => c.ConfirmedResultAttachments.Select(o => o.ConfirmResultId == model.AdvertiseId).First())
                    .Select(c => c.ConfirmedResultAttachments.First());
                    model.AdvertiseImage = f.Select(e => e.Attachment.FileName).FirstOrDefault();
              
                if (model == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                return model;


            }
        }
    }
}
