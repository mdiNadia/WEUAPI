using Application.Errors;
using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;
using System.Net;

namespace Application.Features.Attachment.Commands
{
    public class DeleteAttachmentById : IRequest<string>
    {
        public int Id { get; set; }
        public string FolderName { get; set; }

        public class DeleteAttachmentByIdHandler : IRequestHandler<DeleteAttachmentById, string>
        {
            private readonly IFileUploader _fileUploader;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteAttachmentByIdHandler(IFileUploader fileUploader, IUnitOfWork unitOfWork)
            {
                this._fileUploader = fileUploader;
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteAttachmentById command, CancellationToken cancellationToken)
            {
                var attachment = await _unitOfWork.Attachments.GetByID(command.Id);
                if (attachment == null) throw new RestException(HttpStatusCode.BadRequest, "فایل وجود ندارد!");
                if (!string.IsNullOrEmpty(attachment.FileName))
                    await _fileUploader.DeleteFile(attachment.FileName, command.FolderName);
                _unitOfWork.Attachments.Delete(attachment);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"{attachment.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
