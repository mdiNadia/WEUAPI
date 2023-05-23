using Application.Errors;
using Application.Features.Attachment.Commands;
using Application.Interfaces;
using Application.Services.FileStorage;
using Application.Services.UserAccessor;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Notification.Commands
{
    public class UpdateNotification : IRequest<int>
    {
    
        public class UpdateNotificationHandler : IRequestHandler<UpdateNotification, int>
        {
            private readonly IUserAccessor _userAccessor;
      
            private readonly IUnitOfWork _unitOfWork;


            public UpdateNotificationHandler(IUserAccessor userAccessor, IUnitOfWork unitOfWork)
            {
                this._userAccessor = userAccessor;

                this._unitOfWork = unitOfWork;
      

            }
            public async Task<int> Handle(UpdateNotification command, CancellationToken cancellationToken)
            {
              
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return 0;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }






            }
        }
    }
}
