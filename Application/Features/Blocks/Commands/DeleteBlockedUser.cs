using Application.Errors;
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Blocks.Commands
{
    public class DeleteBlockedUser
    {
        public class DeleteBlockedUserCommand : IRequest
        {
            public string Username { get; set; }
            public class DeleteBlockedUserHandler : IRequestHandler<DeleteBlockedUserCommand>
            {
                private readonly IUnitOfWork _unitOfWork;
                private readonly IUserAccessor _userAccessor;

                public DeleteBlockedUserHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
                {
                    this._unitOfWork = unitOfWork;
                    this._userAccessor = userAccessor;
                }

                public async Task<Unit> Handle(DeleteBlockedUserCommand request, CancellationToken cancellationToken)
                {
                    var observer = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());
                    var target = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == request.Username);

                    if (target == null)
                        throw new RestException(HttpStatusCode.NotFound, "Not found");

                    var blocked = await _unitOfWork.ProfileBlocks.GetQueryList().SingleOrDefaultAsync(x => x.ObserverId == observer.Id && x.TargetId == target.Id);

                    if (blocked == null)
                        throw new RestException(HttpStatusCode.BadRequest, "You are not blocked this user");

                    if (blocked != null)
                    {
                        _unitOfWork.ProfileBlocks.Delete(blocked);
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
