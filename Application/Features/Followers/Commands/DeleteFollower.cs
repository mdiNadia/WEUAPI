
using Application.Errors;
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Followers
{
    public class DeleteFollower
    {
        public class DeleteFollowerCommand : IRequest
        {
            public string Username { get; set; }

            public class DeleteFollowerHandler : IRequestHandler<DeleteFollowerCommand>
            {
                private readonly IUnitOfWork _unitOfWork;
                private readonly IUserAccessor _userAccessor;
                public DeleteFollowerHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
                {
                    _unitOfWork = unitOfWork;
                    _userAccessor = userAccessor;
                }

                public async Task<Unit> Handle(DeleteFollowerCommand request, CancellationToken cancellationToken)
                {
                    var observer = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());

                    var target = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == request.Username);

                    if (target == null)
                        throw new RestException(HttpStatusCode.NotFound, "Not found");

                    var following = await _unitOfWork.UserFollowings.GetQueryList().SingleOrDefaultAsync(x => x.ObserverId == observer.Id && x.TargetId == target.Id);

                    if (following == null)
                        throw new RestException(HttpStatusCode.BadRequest, "You are not following this user");
                    if (following != null)
                    {
                        _unitOfWork.UserFollowings.Delete(following);
                    }

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