using Application.Errors;
using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Application.Features.Attachment.Commands
{
    public class UpdateAttachment : IRequest<int>
    {
        public int Id { get; set; }
        //public int? FileType { get; set; }
        public IFormFile? File { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string FolderName { get; set; }

        public class UpdateAttachmentHandler : IRequestHandler<UpdateAttachment, int>
        {
            private readonly IFileUploader _fileUploader;
            private readonly IUnitOfWork _unitOfWork;

            public UpdateAttachmentHandler(IFileUploader fileUploader, IUnitOfWork unitOfWork)
            {
                this._fileUploader = fileUploader;
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateAttachment command, CancellationToken cancellationToken)
            {
                var attachment = await _unitOfWork.Attachments.GetByID(command.Id);

                if (attachment == null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "فایل وجود ندارد!");

                }
                else
                {

                    attachment.Name = command.Name ?? attachment.Name;
                    attachment.Description = command.Description ?? attachment.Description;
                    //attachment.FileType =command.FileType ?? attachment.FileType);
                    if (command.File != null)
                    {
                        var FileUploader = await _fileUploader.UploadFile(command.File, command.FolderName);
                        if (!string.IsNullOrEmpty(attachment.FileName))
                            await _fileUploader.DeleteFile(attachment.FileName, command.FolderName);
                        attachment.FileName = FileUploader;
                    }
                    _unitOfWork.Attachments.Update(attachment);
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
}
