
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
                    .Where(c => c.Id == query.Id).Select(c => new GetNotificationDto()
                    {
                        Id = query.Id,
                        CreationDate = c.CreationDate,
                        Body = c.Body,
                        NotificationType = c.NotificationType,
                        Title = c.Title,
                    })
                   .FirstOrDefaultAsync();
                if (model == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                return model;


            }
        }
    }
}
