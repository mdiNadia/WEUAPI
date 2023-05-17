
using Application.Errors;
using Application.Interfaces;
using Application.Services.UserAccessor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.LikeComment.Commands
{
    public class LikeCommentCommand : IRequest<bool>
    {
        public int CommentId { get; set; }
        public class LikeCommentHandler : IRequestHandler<LikeCommentCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserAccessor _userAccessor;

            public LikeCommentHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor)
            {
                _unitOfWork = unitOfWork;
                this._userAccessor = userAccessor;
            }
            public async Task<bool> Handle(LikeCommentCommand command, CancellationToken cancellationToken)
            {
                var observer = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(x => x.Username == _userAccessor.GetCurrentUserNameAsync());
                if (observer == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found User");
                var target = await _unitOfWork.Comments.GetByID(command.CommentId);
                if (target == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found Advertisement");
                var isExcist = await _unitOfWork.LikeComments.GetQueryList().SingleOrDefaultAsync(c => c.ObserverId == observer.Id && c.TargetId == target.Id);
                if (isExcist != null)
                {
                    _unitOfWork.LikeComments.Delete(isExcist);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return false;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
                }
                if (isExcist == null)
                {
                    var LikeComment = new Domain.Entities.LikeComment
                    {
                        Observer = observer,
                        Target = target,
                    };
                    _unitOfWork.LikeComments.Insert(LikeComment);
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
