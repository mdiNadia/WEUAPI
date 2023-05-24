
using Application.Errors;
using Application.Interfaces;
using Application.Services.UserAccessor;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Like.Commands
{
    public class LikeCommand : IRequest<bool>
    {
        public int ConfirmedResultId { get; set; }
        public class LikeHandler : IRequestHandler<LikeCommand, bool>
        {
            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserAccessor _userAccessor;

            public LikeHandler(IMediator mediator,IUnitOfWork unitOfWork, IUserAccessor userAccessor)
            {
                this._mediator = mediator;
                _unitOfWork = unitOfWork;
                this._userAccessor = userAccessor;
            }
            public async Task<bool> Handle(LikeCommand command, CancellationToken cancellationToken)
            {
                var observer = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());
                if (observer == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found User");
                var target = await _unitOfWork.ConfirmedResults.GetByID(command.ConfirmedResultId);
                if (target == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found Advertisement");
                var targetProfile = await _unitOfWork.Profiles.GetQueryList().FirstAsync(x=>x.Id == target.AdvertiserId);
                var isExcist = await _unitOfWork.Likes.GetQueryList().SingleOrDefaultAsync(c => c.ObserverId == observer.Id && c.TargetId == target.Id);
                if (isExcist != null)
                {
                    _unitOfWork.Likes.Delete(isExcist);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return false;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
                }
                if (isExcist == null)
                {
                    var like = new Domain.Entities.Like
                    {
                        Observer = observer,
                        Target = target,
                    };
                    _unitOfWork.Likes.Insert(like);

                    var notification = new Domain.Entities.Notification()
                    {
                        CreationDate = DateTime.Now,
                        Observer = observer,
                        Target = targetProfile,
                        AdvertiseId = target.Id,
                        NotificationType = NotificationType.like,
                        Title = "like",
                        Body = $"{observer.Username} liked your post"
                    };
                    _unitOfWork.Notifications.Insert(notification);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return true;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }

                }

                return false;


            }
        }
    }
}
