using Application.Errors;
using Application.Features.Attachment.Commands;
using Application.Interfaces;
using Application.Services.FileStorage;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Advertising.Commands
{
    public class DeleteAdvertisingById : IRequest<string>
    {
        public int Id { get; set; }
        public class DeleteAdvertisingByIdHandler : IRequestHandler<DeleteAdvertisingById, string>
        {
            private readonly IMediator _mediator;
            private readonly IFileUploader _fileUploader;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteAdvertisingByIdHandler(IMediator mediator, IFileUploader fileUploader, IUnitOfWork unitOfWork)
            {
                this._mediator = mediator;
                this._fileUploader = fileUploader;
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(DeleteAdvertisingById command, CancellationToken cancellationToken)
            {

                var advertising = await _unitOfWork.Advertisings.GetQueryList()
                    .Where(c => c.Id == command.Id)
                    .Include(c => c.AdvertisingAttachments).ThenInclude(c => c.Attachment)
                    .ToListAsync();
                if (advertising == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات وجود ندارد!");
                var AdvCat = await _unitOfWork.AdCategoryAdvertisings.GetQueryList().Where(c => c.AdvertisingId == command.Id).ToListAsync();
                _unitOfWork.AdCategoryAdvertisings.Delete(AdvCat);
                var advSaved = await _unitOfWork.SavedAds.GetQueryList().Where(c => c.AdvertisingId == command.Id).ToListAsync();
                _unitOfWork.SavedAds.Delete(advSaved);
                _unitOfWork.Advertisings.Delete(advertising);
                //حذف از جدول واسط و جدول فایل ها//
                var AdvAttachment = advertising.Select(c => c.AdvertisingAttachments);
                foreach (var item in AdvAttachment)
                {
                    var ex = Path.GetExtension(item.Select(c => c.Attachment.Name).First());
                    if (ex == ".mp4")
                        await _mediator.Send(new DeleteAttachmentById { Id = item.Select(c => c.AttachmentId).FirstOrDefault(), FolderName = "/Videos/Ad" });
                    else
                        await _mediator.Send(new DeleteAttachmentById { Id = item.Select(c => c.AttachmentId).FirstOrDefault(), FolderName = "/Images/Ad" });

                }

                _unitOfWork.AdvertisingAttachments.Delete(AdvAttachment);
                //نکته: سیو عملیات در "هندلرِ حذفِ فایل" انجام شده
                //که اینجا دیگه نیازی نیست مجددا
                //از یونیت اف ورک سیو استفاده بشه چون باعث ارورو میشه
                //error: The database operation was expected to affect 1 row(s), but actually affected 0 row(s);
                ///////////////////////////////////
                return $"با موفقیت حذف شد";

            }
        }
    }
}
