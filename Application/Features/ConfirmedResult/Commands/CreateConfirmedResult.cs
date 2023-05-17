using Application.Dtos.Advertising;
using Application.Errors;
using Application.Interfaces;
using Application.Services.FileStorage;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.ConfirmedResult.Commands
{
    public class CreateConfirmedResult : IRequest<string>
    {
        public int AdId { get; set; }
        public IList<GetFileWithType> AdFiles { get; set; }
        public class CreateConfirmedResultHandler : IRequestHandler<CreateConfirmedResult, string>
        {
            private readonly IFileUploader _fileUploader;
            private readonly IAttachmentRepository _attachmentRepository;
            private readonly IUnitOfWork _unitOfWork;

            public CreateConfirmedResultHandler(IFileUploader fileUploader, IAttachmentRepository attachmentRepository, IUnitOfWork unitOfWork)
            {
                this._fileUploader = fileUploader;
                this._attachmentRepository = attachmentRepository;
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(CreateConfirmedResult command, CancellationToken cancellationToken)
            {
                Domain.Entities.Advertising ad = await
                    _unitOfWork.Advertisings.GetQueryList()
                    .Where(c => c.Id == command.AdId)
                    .AsNoTracking()
                    .Include(c => c.AdvertisingAttachments).ThenInclude(c => c.Attachment)
                    .FirstOrDefaultAsync();
                if (ad == null) throw new RestException(HttpStatusCode.InternalServerError, "آگهی وجود ندارد!");
                var getConfirmed = await _unitOfWork.ConfirmedResults.GetQueryList()
                    .AsNoTracking().Where(c => c.AdId == command.AdId && c.IsActive).ToListAsync();
                if (getConfirmed != null)
                {
                    foreach (var item in getConfirmed)
                    {
                        item.IsActive = false;
                        _unitOfWork.ConfirmedResults.Update(item);
                    }
                    await _unitOfWork.CompleteAsync();
                }
                Domain.Entities.ConfirmResult ConfirmedResult = ad.Adapt<Domain.Entities.ConfirmResult>();
                ConfirmedResult.Id = 0;
                ConfirmedResult.AdId = command.AdId;
                var cats = await _unitOfWork.AdCategoryAdvertisings.GetQueryList()
                    .Where(c => c.AdvertisingId == command.AdId).Select(c => c.Id).ToListAsync();
                if (cats == null) throw new RestException(HttpStatusCode.InternalServerError, "دسته بندی ها وجود ندارد!");
                string catStr = string.Join(",", cats);
                ConfirmedResult.Categories = catStr;
                var countries = await _unitOfWork.AdCountries.GetQueryList()
                    .Where(c => c.AdvertisingId == command.AdId).Select(c => c.Id).ToListAsync();
                if (countries == null) throw new RestException(HttpStatusCode.InternalServerError, "کشور موردنظر وجود ندارد!");
                string couStr = string.Join(",", countries);
                ConfirmedResult.AdCountries = couStr;
                var cities = await _unitOfWork.AdCities.GetQueryList()
                .Where(c => c.AdvertisingId == command.AdId).Select(c => c.Id).ToListAsync();
                if (cities == null) throw new RestException(HttpStatusCode.InternalServerError, "شهرهای موردنظر وجود ندارد!");
                string citStr = string.Join(",", countries);
                ConfirmedResult.AdCities = citStr;
                var provinces = await _unitOfWork.AdProvinces.GetQueryList()
               .Where(c => c.AdvertisingId == command.AdId).Select(c => c.Id).ToListAsync();
                if (provinces == null) throw new RestException(HttpStatusCode.InternalServerError, "استان‌های موردنظر وجود ندارد!");
                string proStr = string.Join(",", provinces);
                ConfirmedResult.AdProvinces = citStr;
                var neighbors = await _unitOfWork.AdNeighborhoods.GetQueryList()
                .Where(c => c.AdvertisingId == command.AdId).Select(c => c.Id).ToListAsync();
                if (neighbors == null) throw new RestException(HttpStatusCode.InternalServerError, "محله‌های موردنظر وجود ندارد!");
                string neiStr = string.Join(",", neighbors);
                ConfirmedResult.AdNeighborhoods = neiStr;
                ConfirmedResult.ConfirmedDate = DateTime.Now;
                ConfirmedResult.CreationDate = DateTime.Now;
                ConfirmedResult.IsActive = true;
                _unitOfWork.ConfirmedResults.Insert(ConfirmedResult);
                try
                {
                    await _unitOfWork.CompleteAsync();

                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }

                ////////////////////////عملیات افزودن فایل ها به جدول واسط////////////////////
                foreach (var item in command.AdFiles)
                {
                    var ex = Path.GetExtension(item.Name);
                    ConfirmedResultAttachment cf = new ConfirmedResultAttachment();
                    var fId = 0;
                    if (!string.IsNullOrEmpty(item.Name))
                    {
                        string path = string.Empty;
                        if (ex == ".mp4")
                        {
                            path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles/Videos/Ad/" + item.Name));
                        }
                        else
                            path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles/Images/Ad/" + item.Name));

                        using (var stream = System.IO.File.OpenRead(path))
                        {
                            var File = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

                            if (ex == ".mp4")
                            {
                                // await _fileUploader.UploadFile(File, "/Confirmed/Videos/Confirmed/");
                                fId = await _attachmentRepository.NewFileId(File, "/Videos/Confirmed");


                            }
                            else
                            {
                                //await _fileUploader.UploadFile(File, "/Confirmed/Images/Confirmed/");
                                fId = await _attachmentRepository.NewFileId(File, "/Images/Confirmed");
                            }
                            cf.ConfirmResult = ConfirmedResult;
                            cf.AttachmentId = fId;
                        }
                        _unitOfWork.ConfirmedResultAttachments.Insert(cf);
                        try
                        {
                            await _unitOfWork.CompleteAsync();

                        }
                        catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
                    }

                }
                ////////////////////////پایان عملیات افزودن فایل ها به جدول واسط//////////////  
                ad.AdStatus = AdStatus.confirm;

                _unitOfWork.Advertisings.Update(ad);
                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"تبریک! آیدی {ConfirmedResult.Id}";
                }
                catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }





            }
        }
    }
}
