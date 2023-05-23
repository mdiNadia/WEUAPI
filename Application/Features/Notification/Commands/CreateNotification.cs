using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using Application.Services.FileStorage;
using Application.Services.UserAccessor;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notification.Commands
{
    public class CreateNotification : IRequest<int>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public NotificationType NotificationType { get; set; }

        public class CreateNotificationHandler : IRequestHandler<CreateNotification, int>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFileUploader _fileUploader;

            public CreateNotificationHandler(IUserAccessor userAccessor, IUnitOfWork unitOfWork, IFileUploader fileUploader)
            {
                this._userAccessor = userAccessor;
                this._unitOfWork = unitOfWork;
                this._fileUploader = fileUploader;
            }

            public async Task<int> Handle(CreateNotification command, CancellationToken cancellationToken)
            {
                var profile = await _unitOfWork.Profiles.GetQueryList()
                    .FirstAsync(c=>c.Username == _userAccessor.GetCurrentUserNameAsync());
                var model = new Domain.Entities.Notification();
                model.Observer = profile;
                model.Title = command.Title;
                model.Body = command.Body;
                model.NotificationType = command.NotificationType;
                _unitOfWork.Notifications.Insert(model);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return 1;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}

