using Application.Interfaces;
using Application.Services.FileStorage;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Attachment.Commands
{
    public class CreateAttachment : IRequest<int>
    {
        //public int FileTypeId { get; set; }
        public IFormFile? File { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FolderName { get; set; }

        public class CreateAttachmentHandler : IRequestHandler<CreateAttachment, int>
        {
            private readonly IFileUploader _fileUploader;
            private readonly IUnitOfWork _unitOfWork;

            public CreateAttachmentHandler(IFileUploader fileUploader, IUnitOfWork unitOfWork)
            {
                this._fileUploader = fileUploader;
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(CreateAttachment command, CancellationToken cancellationToken)
            {
                Domain.Entities.Attachment attachment = command.Adapt<Domain.Entities.Attachment>();
                if (command.File != null)
                {
                    var FileUploader = await _fileUploader.UploadFile(command.File, command.FolderName);
                    attachment.FileName = FileUploader;
                }
                else
                    attachment.FileName = "";
                attachment.CreationDate = DateTime.Now;
                _unitOfWork.Attachments.Insert(attachment);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return attachment.Id;
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
            }
        }
    }
}
