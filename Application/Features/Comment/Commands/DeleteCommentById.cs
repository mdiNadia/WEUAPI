using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.Comment.Commands
{
    public class DeleteCommentById : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteCommentByIdHandler : IRequestHandler<DeleteCommentById, string>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteCommentByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteCommentById command, CancellationToken cancellationToken)
            {

                var comment = await _unitOfWork.Comments.GetByID(command.Id);
                if (comment == null) throw new RestException(HttpStatusCode.BadRequest, "دسته بندی وجود ندارد!");
                var CheckIfHasChildren = await _unitOfWork.Comments.CheckIfHasChildren(command.Id);
                if (CheckIfHasChildren) throw new RestException(HttpStatusCode.BadRequest, "دسته بندی دارای فرزند میباشد!");
                _unitOfWork.Comments.Delete(comment);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{comment.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }


            }
        }
    }
}
