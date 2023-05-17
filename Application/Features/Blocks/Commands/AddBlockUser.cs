using Application.Errors;
using Application.Interfaces;
using Application.Services.UserAccessor;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Blocks.Commands
{
    public class AddBlockUser
    {
        public class AddBlockUserCommand : IRequest
        {
            public string Username { get; set; }
            public int? ReportId { get; set; }
            public class AddBlockUserHandler : IRequestHandler<AddBlockUserCommand>
            {
                private readonly IUnitOfWork _unitOfWork;
                private readonly IUserAccessor _userAccessor;

                public AddBlockUserHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
                {
                    this._unitOfWork = unitOfWork;
                    this._userAccessor = userAccessor;
                }
                public async Task<Unit> Handle(AddBlockUserCommand request, CancellationToken cancellationToken)
                {
                    var observer = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());
                    var target = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == request.Username);
                    if (target == null)
                        throw new RestException(HttpStatusCode.NotFound, "Not found");
                    var blocked = await _unitOfWork.ProfileBlocks.GetQueryList().SingleOrDefaultAsync(x => x.ObserverId == observer.Id && x.TargetId == target.Id);

                    if (blocked != null)
                        throw new RestException(HttpStatusCode.BadRequest, "You are already blocked this user");
                    if (blocked == null)
                    {
                        blocked = new ProfileBlock
                        {
                            Observer = observer,
                            Target = target
                        };
                        blocked.BlockedDate = DateTime.Now;

                        _unitOfWork.ProfileBlocks.Insert(blocked);
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
