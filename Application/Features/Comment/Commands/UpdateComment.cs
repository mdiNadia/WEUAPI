using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.Comment.Commands
{
    public class UpdateComment : IRequest<int>
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisite { get; set; }
        public class UpdateCommentHandler : IRequestHandler<UpdateComment, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateCommentHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateComment command, CancellationToken cancellationToken)
            {
                var comment = await _unitOfWork.Comments.GetByID(command.Id);

                if (comment == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "پیام وجود ندارد!");
                }
                else
                {
                    comment.IsActive = command.IsActive;
                    comment.IsVisited = command.IsVisite;
                    _unitOfWork.Comments.Update(comment);
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
}
