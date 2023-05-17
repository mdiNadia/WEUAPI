using Application.Errors;
using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Attachment.Commands
{
    public class DeleteAttachmentsByIds : IRequest<string>
    {
        public IList<int> Id { get; set; }
        public string FolderName { get; set; }

        public class DeleteAttachmentsByIdsHandler : IRequestHandler<DeleteAttachmentsByIds, string>
        {
            private readonly IFileUploader _fileUploader;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteAttachmentsByIdsHandler(IFileUploader fileUploader, IUnitOfWork unitOfWork)
            {
                this._fileUploader = fileUploader;
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteAttachmentsByIds command, CancellationToken cancellationToken)
            {

                var attachment = await _unitOfWork.Attachments.GetQueryList()
                .Where(c => command.Id.Contains(c.Id)).ToListAsync();
                if (attachment == null) throw new RestException(HttpStatusCode.BadRequest, "فایل‌ها وجود ندارد!");
                await _fileUploader.DeleteFiles(attachment.Select(c => c.FileName).ToList(), command.FolderName);
                _unitOfWork.Attachments.Delete(attachment);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"با موفقیت حذف شدند";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }

            }
        }
    }
}
