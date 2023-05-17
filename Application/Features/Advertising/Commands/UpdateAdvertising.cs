using Application.Dtos.Advertising;
using Application.Errors;
using Application.ExtensionMethods;
using Application.Features.Attachment.Commands;
using Application.Interfaces;
using Application.Services.FileStorage;
using Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Application.Features.Advertising.Commands
{
    public class UpdateAdvertising : IRequest<int>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string ExpireDate { get; set; }
        public List<RequestUpdateFilesInAdvertisingDto>? AdvertisingFiles { get; set; }

        public class UpdateAdvertisingHandler : IRequestHandler<UpdateAdvertising, int>
        {
            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;

            public UpdateAdvertisingHandler(IMediator mediator, IFileUploader fileUploader, IAttachmentRepository attachmentRepository, IUnitOfWork unitOfWork)
            {
                this._mediator = mediator;
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(UpdateAdvertising command, CancellationToken cancellationToken)
            {

                var advertising = await _unitOfWork.Advertisings.GetByID(command.Id);

                if (advertising == null)
                {

                    throw new RestException(HttpStatusCode.BadRequest, "آگهی پیدا نشد!");

                }
                else
                {

                    advertising.Name = command.Name;
                    advertising.Description = command.Description;
                    advertising.Text = command.Text;
                    advertising.StartDate = command.StartDate.StringToDateTime();
                    advertising.ExpireDate = command.ExpireDate.StringToDateTime();
                    advertising.AdStatus = AdStatus.awaiting;
                    if (command.AdvertisingFiles != null)
                    {

                        foreach (var item in command.AdvertisingFiles)
                        {
                            int v = 0;
                            if (item.FileType == Domain.Enums.FileType.Video)
                            {
                                v++;
                                var AlreadyExcist = advertising.AdvertisingAttachments.Select(c => c.Attachment)
                                    .Any(c => c.FileType.Type == Domain.Enums.FileType.Video && c.Id != item.AttachmentId);
                                if (AlreadyExcist)
                                    throw new RestException(HttpStatusCode.BadRequest, "تنها یک فایل از نوع ویدیو میتوانید آپلود کنید!");
                                if (v > 1)
                                    throw new RestException(HttpStatusCode.BadRequest, "تنها یک فایل از نوع ویدیو میتوانید آپلود کنید!");
                            }
                            if (item.IsChanged)
                            {
                                UpdateAttachment updateAttachment = new UpdateAttachment();
                                updateAttachment.File = item.UpdatedFile;
                                updateAttachment.Id = item.AttachmentId;
                                if (item.FileType == 0)
                                    updateAttachment.FolderName = "/Images/Ad";
                                else
                                    updateAttachment.FolderName = "/Videos/Ad";
                                await _mediator.Send(updateAttachment);
                            }
                        }

                    }
                    _unitOfWork.Advertisings.Update(advertising);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                        return advertising.Id;
                    }
                    catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }

                }


            }
        }
    }
}
