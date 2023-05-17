using Application.Errors;
using Application.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.FileType.Commands
{
    public class DeleteFileTypeById : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteFileTypeByIdHandler : IRequestHandler<DeleteFileTypeById, string>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteFileTypeByIdHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteFileTypeById command, CancellationToken cancellationToken)
            {

                var fileType = await _unitOfWork.FileTypes.GetByID(command.Id);
                if (fileType == null) throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                _unitOfWork.FileTypes.Delete(fileType);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{fileType.Id}";

                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }


            }
        }
    }
}
