using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Comment.Commands
{
    public class CreateComment : IRequest<int>
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public int AdvertisingId { get; set; }
        public int? ParentId { get; set; }

        public class CreateCommentHandler : IRequestHandler<CreateComment, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateCommentHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(CreateComment command, CancellationToken cancellationToken)
            {

                var author = await _unitOfWork.Profiles.GetQueryList().SingleOrDefaultAsync(c => c.Username == command.UserName);
                if (author == null)
                    throw new RestException(HttpStatusCode.BadRequest, "پیام وجود ندارد!");
                var comment = new Domain.Entities.Comment();
                comment.Author = author;
                comment.Message = command.Message;
                comment.IsVisited = false;
                comment.IsActive = false;
                comment.ConfirmResultId = command.AdvertisingId;
                comment.ParentId = command.ParentId;
                comment.CreationDate = DateTime.Now;
                _unitOfWork.Comments.Insert(comment);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return comment.Id;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }




            }
        }
    }
}
