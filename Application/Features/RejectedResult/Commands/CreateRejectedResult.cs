
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

namespace Application.Features.RejectedResult.Commands
{

    public class CreateRejectedResult : IRequest<string>
    {
        public int AdId { get; set; }
        public string Reason { get; set; }
        public IList<GetFileWithType> AdFiles { get; set; }
        public class CreateRejectedResultHandler : IRequestHandler<CreateRejectedResult, string>
        {
            private readonly IFileUploader _fileUploader;
            private readonly IAttachmentRepository _attachmentRepository;
            private readonly IUnitOfWork _unitOfWork;

            public CreateRejectedResultHandler(IFileUploader fileUploader, IAttachmentRepository attachmentRepository, IUnitOfWork unitOfWork)
            {
                this._fileUploader = fileUploader;
                this._attachmentRepository = attachmentRepository;
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(CreateRejectedResult command, CancellationToken cancellationToken)
            {


                Domain.Entities.Advertising ad = await
                    _unitOfWork.Advertisings.GetQueryList()
                    .Where(c => c.Id == command.AdId)
                    .AsNoTracking()
                    .Include(c => c.AdvertisingAttachments).ThenInclude(c => c.Attachment)
                    .FirstOrDefaultAsync();
                if (ad == null) throw new RestException(HttpStatusCode.BadRequest, "طلاعات وجود ندارد!");
                Domain.Entities.RejectResult RejectedResult = ad.Adapt<Domain.Entities.RejectResult>();
                RejectedResult.Id = 0;
                RejectedResult.AdId = command.AdId;
                RejectedResult.Reason = command.Reason;
                var cats = await _unitOfWork.AdCategoryAdvertisings.GetQueryList()
                    .Where(c => c.AdvertisingId == command.AdId).Select(c => c.Id).ToListAsync();
                string catStr = string.Join(",", cats);
                RejectedResult.Categories = catStr;
                var countries = await _unitOfWork.AdCountries.GetQueryList()
                    .Where(c => c.AdvertisingId == command.AdId).Select(c => c.Id).ToListAsync();
                string couStr = string.Join(",", countries);
                RejectedResult.AdCountries = couStr;
                var cities = await _unitOfWork.AdCities.GetQueryList()
                .Where(c => c.AdvertisingId == command.AdId).Select(c => c.Id).ToListAsync();
                string citStr = string.Join(",", countries);
                RejectedResult.AdCities = citStr;
                var provinces = await _unitOfWork.AdProvinces.GetQueryList()
               .Where(c => c.AdvertisingId == command.AdId).Select(c => c.Id).ToListAsync();
                string proStr = string.Join(",", provinces);
                RejectedResult.AdProvinces = citStr;
                var neighbors = await _unitOfWork.AdNeighborhoods.GetQueryList()
                .Where(c => c.AdvertisingId == command.AdId).Select(c => c.Id).ToListAsync();
                string neiStr = string.Join(",", neighbors);
                RejectedResult.AdNeighborhoods = neiStr;
                RejectedResult.RejectDate = DateTime.Now;
                _unitOfWork.RejectResults.Insert(RejectedResult);
                try
                {
                    await _unitOfWork.CompleteAsync();

                }
                catch (Exception err)
                {

                    throw new Exception("خطا در ذخیره اطلاعات!");

                }
                ////////////////////////عملیات افزودن فایل ها به جدول واسط////////////////////
                foreach (var item in command.AdFiles)
                {
                    var ex = Path.GetExtension(item.Name);
                    RejectedResultAttachment cf = new RejectedResultAttachment();
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
                                fId = await _attachmentRepository.NewFileId(File, "/Videos/Rejected");


                            }
                            else
                            {

                                fId = await _attachmentRepository.NewFileId(File, "/Images/Rejected");
                            }
                            cf.RejectResult = RejectedResult;
                            cf.AttachmentId = fId;
                        }
                        _unitOfWork.RejectedResultAttachments.Insert(cf);
                        try
                        {
                            await _unitOfWork.CompleteAsync();

                        }
                        catch (Exception err) { throw new Exception("خطا در ذخیره اطلاعات!"); }
                    }

                }
                ////////////////////////پایان عملیات افزودن فایل ها به جدول واسط//////////////  
                ad.AdStatus = AdStatus.reject;
                _unitOfWork.Advertisings.Update(ad);

                try
                {
                    await _unitOfWork.CompleteAsync();
                    return $"تبریک! آیدی {RejectedResult.Id}";
                }
                catch (Exception err)
                {

                    throw new Exception("خطا در ذخیره اطلاعات!");

                }





            }
        }
    }
}
