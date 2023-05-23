
using Application.Errors;
using Application.Interfaces;
using Application.Services.UserAccessor;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Followers
{
    public class AddFollower
    {
        public class AddFollowerCommand : IRequest
        {
            public string Username { get; set; }
            public class AddFollowerHandler : IRequestHandler<AddFollowerCommand>
            {
                private readonly IUnitOfWork _unitOfWork;
                private readonly IUserAccessor _userAccessor;
                public AddFollowerHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
                {
                    _unitOfWork = unitOfWork;
                    _userAccessor = userAccessor;
                }

                public async Task<Unit> Handle(AddFollowerCommand request, CancellationToken cancellationToken)
                {
                    var observer = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());

                    var target = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == request.Username);

                    if (target == null)
                        throw new RestException(HttpStatusCode.NotFound, "Not found");

                    var following = await _unitOfWork.UserFollowings.GetQueryList().SingleOrDefaultAsync(x => x.ObserverId == observer.Id && x.TargetId == target.Id);

                    if (following != null)
                        throw new RestException(HttpStatusCode.BadRequest, "You are already following this user");

                    if (following == null)
                    {
                        following = new UserFollowing
                        {
                            Observer = observer,
                            Target = target
                        };

                        _unitOfWork.UserFollowings.Insert(following);
                        var notification = new Domain.Entities.Notification()
                        {
                            CreationDate = DateTime.Now,
                            Observer = observer,
                            Target = target,
                            NotificationType = NotificationType.following,
                            Title = "following",
                            Body = $"{observer.Username} start following you"
                        };
                        _unitOfWork.Notifications.Insert(notification);

                    }
                    following.CreationDate = DateTime.Now;
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return Unit.Value;

                    }
                    catch (Exception err)
                    {
                        throw err;
                    }

                    throw new Exception("Problem saving changes");
                }
            }
        }


    }
}