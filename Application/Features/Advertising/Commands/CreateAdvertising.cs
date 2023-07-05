using Application.Dtos.Advertising;
using Application.Errors;
using Application.ExtensionMethods;
using Application.Features.Attachment.Commands;
using Application.Interfaces;
using Application.Services.FileStorage;
using Application.Services.UserAccessor;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Net;

namespace Application.Features.Advertising.Commands
{

    public class CreateAdvertising : IRequest<string>
    {
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
        [Required]
        public int AdCategoryId { get; set; }
        [Required]
        public List<IFormFile> AdvertisingFiles { get; set; }
        [Required]
        public int AdCountryId { get; set; }
        public List<int>? AdProvinceIds { get; set; }
        public List<int>? AdCityIds { get; set; }
        public List<int>? AdNeighborhoodIds { get; set; }
        public RequestBoostDto? RequestBoostDto { get; set; }

        public class CreateAdvertisingHandler : IRequestHandler<CreateAdvertising, string>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IMediator _mediator;
            private readonly IUserAccessor _userAccessor;
            private readonly IAttachmentRepository _attachmentRepository;
            private readonly IUnitOfWork _unitOfWork;

            public CreateAdvertisingHandler(IHttpContextAccessor httpContextAccessor, IMediator mediator, IUserAccessor userAccessor, IAttachmentRepository attachmentRepository, IFileUploader fileUploader, IUnitOfWork unitOfWork)
            {
                this._httpContextAccessor = httpContextAccessor;
                this._mediator = mediator;
                this._userAccessor = userAccessor;
                this._attachmentRepository = attachmentRepository;
                this._unitOfWork = unitOfWork;
            }
            public async Task<string> Handle(CreateAdvertising command, CancellationToken cancellationToken)
            {
                var ProfileAdvertiser = _userAccessor.GetCurrentUserNameAsync();
                Domain.Entities.Profile user = await _unitOfWork.Profiles.GetQueryList().FirstOrDefaultAsync(c => c.Username == ProfileAdvertiser);
                using (var dbContextTransaction = _unitOfWork.BeginTransaction())
                {

                    Domain.Entities.Advertising advertise = command.Adapt<Domain.Entities.Advertising>();
                    advertise.CreationDate = DateTime.Now;
                    advertise.Advertiser = user;
                    advertise.AdStatus = AdStatus.awaiting;
                    var ShortKey = RandomShortUrlMaker.GetURL();
                    while (await _unitOfWork.Advertisings.GetQueryList().AnyAsync(s => s.ShortKey == ShortKey))
                    {
                        ShortKey = RandomShortUrlMaker.GetURL();
                    }
                    advertise.ShortKey = "VW" + ShortKey;
                    ///////////////////////////
                    string url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/v1/Advertising/A/{advertise.ShortKey}";
                    var qrCode = url.QRCodeWriter().ToArray();
                    var stream = new MemoryStream(qrCode);
                    var fromFile = new FormFile(stream, 0, qrCode.Length, "qrcodeImage", "qrcodeImage.png")
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "image/png"
                    };
                    CreateAttachment createAttachment = new CreateAttachment();
                    createAttachment.Name = command.Name;
                    createAttachment.Description = "";
                    createAttachment.File = fromFile;
                    createAttachment.FolderName = "/Images/QrCode";
                    var saveToDatabase = await _mediator.Send(createAttachment);
                    advertise.QrCode = saveToDatabase;
                    //////////////////////////////
                    _unitOfWork.Advertisings.Insert(advertise);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception("خطا در ذخیره اطلاعات");
                    }
                    ////////////////////////افزودن شتابدهی به جدول واسط///////////////////
                    if (command.RequestBoostDto != null)
                    {
                        var boostSetting = await _unitOfWork.AppSettings
                      .GetQueryList().AsNoTracking().FirstOrDefaultAsync();
                        if (boostSetting == null) throw new RestException(HttpStatusCode.BadRequest, "اطلاعات تنظیمات شتابدهی وجود ندارد!!");
                        ; 
                        Domain.Entities.Boost boost = new Domain.Entities.Boost();
                        boost.NumberOfadViews = command.RequestBoostDto.NumberOfadViews < boostSetting.MinView
                            ? throw new RestException(HttpStatusCode.BadRequest, "تعداد بازدید کننده کمتر از تعداد تعریف شده است!!")
                            : command.RequestBoostDto.NumberOfadViews;
                        //boost.Debit = command.RequestBoostDto.Debit < boostSetting.MinBoostAmount
                        //    ? throw new RestException(HttpStatusCode.BadRequest, "مبلغ شتابدهی کمتر از مقدار تعریف شده است!!")
                        //    : command.RequestBoostDto.Debit;
                        var value = boost.NumberOfadViews * boostSetting.MinValuePerVisit;
                        boost.ValuePerVisit = command.RequestBoostDto.ValuePerVisit < boostSetting.MinValuePerVisit
                            ? throw new RestException(HttpStatusCode.BadRequest, "مبلغ افزایش برای کاربر کمتر از مقدار تعریف شده است!!")
                            : command.RequestBoostDto.ValuePerVisit;
                        boost.Status = (BoostStatus)command.RequestBoostDto.Status;
                        boost.Debit = value + (value*boostSetting.AppFee/100);
                        boost.Advertising = advertise;
                        _unitOfWork.Boosts.Insert(boost);
                        try
                        {
                            await _unitOfWork.CompleteAsync();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                            throw new Exception("خطا در ذخیره اطلاعات");
                        }
                    }
                    ///////////////////////////پایان افزودن شتابدهی به جدول واسط///////////////////
                    ////////////////////////عملیات افزودن موقعیت ها به جدول واسط///////////////////
                    List<AdCountry> adCountries = new List<AdCountry>();

                    adCountries.Add(new AdCountry { Advertising = advertise, CountryId = command.AdCountryId });

                    _unitOfWork.AdCountries.InsertRange(adCountries);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception("خطا در ذخیره اطلاعات");
                    }

                    if (command.AdProvinceIds != null)
                    {
                        List<AdProvince> adProvinces = new List<AdProvince>();
                        if (command.AdProvinceIds.FirstOrDefault() == -1)
                        {
                            var provinces = await _unitOfWork.Provinces.GetQueryList()
                                  .Where(c => c.CountryId == command.AdCountryId)
                                  .AsNoTracking()
                                  .ToListAsync();
                            foreach (var province in provinces)
                            {
                                adProvinces.Add(new AdProvince { Advertising = advertise, ProvinceId = province.Id });
                            }
                        }
                        else
                        {
                            foreach (var province in command.AdProvinceIds)
                            {
                                adProvinces.Add(new AdProvince { Advertising = advertise, ProvinceId = province });
                            }
                        }
                        _unitOfWork.AdProvinces.InsertRange(adProvinces);
                        try
                        {
                            await _unitOfWork.CompleteAsync();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                            throw new Exception("خطا در ذخیره اطلاعات");
                        }
                    }

                    if (command.AdCityIds != null)
                    {
                        List<AdCity> adCities = new List<AdCity>();
                        if (command.AdCityIds.FirstOrDefault() == -1)
                        {
                            var cities = await _unitOfWork.Cities.GetQueryList()
                                .Where(c => command.AdProvinceIds.Contains(c.ProvinceId))
                                .AsNoTracking()
                            .ToListAsync();
                            foreach (var city in cities)
                            {
                                adCities.Add(new AdCity { Advertising = advertise, CityId = city.Id });
                            }
                        }
                        else
                        {
                            foreach (var city in command.AdCityIds)
                            {
                                adCities.Add(new AdCity { Advertising = advertise, CityId = city });
                            }
                        }

                        _unitOfWork.AdCities.InsertRange(adCities);
                        try
                        {
                            await _unitOfWork.CompleteAsync();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                            throw new Exception("خطا در ذخیره اطلاعات");
                        }
                    }

                    if (command.AdNeighborhoodIds != null)
                    {
                        List<AdNeighborhood> adNeighborhoods = new List<AdNeighborhood>();
                        if (command.AdNeighborhoodIds.FirstOrDefault() == -1)
                        {
                            var neighborhoodIds = await _unitOfWork.Neighborhoods.GetQueryList()
                                .Where(c => command.AdCityIds.Contains(c.CityId))
                                .AsNoTracking()
                            .ToListAsync();
                            foreach (var neighborhood in neighborhoodIds)
                            {
                                adNeighborhoods.Add(new AdNeighborhood { Advertising = advertise, NeighborhoodId = neighborhood.Id });
                            }
                        }
                        else
                        {
                            foreach (var neighborhood in command.AdNeighborhoodIds)
                            {
                                adNeighborhoods.Add(new AdNeighborhood { Advertising = advertise, NeighborhoodId = neighborhood });
                            }
                        }

                        _unitOfWork.AdNeighborhoods.InsertRange(adNeighborhoods);
                        try
                        {
                            await _unitOfWork.CompleteAsync();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                            throw new Exception("خطا در ذخیره اطلاعات");
                        }
                    }
                    ////////////////////////عملیات افزودن موقعیت ها به جدول واسط//////////////////
                    ////////////////////////عملیات افزودن فایل ها به جدول واسط////////////////////
                    int index = 8;
                    int v = 0;
                    foreach (var item in command.AdvertisingFiles)
                    {
                        var ex = Path.GetExtension(item.FileName);
                        if (ex == ".mp4")
                            v++;
                        if (v > 1) throw new RestException(HttpStatusCode.BadRequest, "تعداد ویدیوی آپلودی بیش از حد مجاز است!!");
                        var fileTypes = await _unitOfWork.FileTypes.GetQueryList().AsNoTracking().ToListAsync();
                        var types = fileTypes.Select(c => c.Type).ToList();
                        var extensions = fileTypes.Select(c => c.Extension).ToList();
                        var sizes = fileTypes.Select(c => c.Size).ToList();
                        if (!types.Contains(Domain.Enums.FileType.Image) && !extensions.Contains(ex) &&
                            !((item.Length / 1024) <= fileTypes?.FirstOrDefault(c => c.Extension == ex)?.Size))
                        {
                            dbContextTransaction.Rollback();
                            throw new RestException(HttpStatusCode.BadRequest, "اطلاعات در تنظیمات فایل وجود ندارد!!");

                        }
                        else if (!types.Contains(Domain.Enums.FileType.Video) && !extensions.Contains(ex) &&
                            !((item.Length / 1024) <= fileTypes?.FirstOrDefault(c => c.Extension == ex)?.Size))
                        {
                            dbContextTransaction.Rollback();
                            throw new RestException(HttpStatusCode.BadRequest, "اطلاعات در تنظیمات فایل وجود ندارد!!");
                        }
                        else
                        {
                            AdvertisingAttachment advImage = new AdvertisingAttachment();
                            int FileId;
                            if (ex == ".png"
                                || ex == ".jpg"
                                || ex == ".jpeg"
                                || ex == ".webp"
                                || ex == ".svg")
                            {

                                FileId = await _attachmentRepository.NewFileId(item, "/Images/Ad");
                                advImage.AdvertisingId = advertise.Id;
                                advImage.AttachmentId = FileId;
                            }
                            else
                            {
                                FileId = await _attachmentRepository.NewFileId(item, "/Videos/Ad");
                                advImage.AdvertisingId = advertise.Id;
                                advImage.AttachmentId = FileId;

                            }
                            _unitOfWork.AdvertisingAttachments.Insert(advImage);
                            try
                            {
                                await _unitOfWork.CompleteAsync();
                            }
                            catch (Exception)
                            {
                                dbContextTransaction.Rollback();
                                throw new Exception("خطا در ذخیره اطلاعات");
                            }
                        }
                        index--;
                    }
                    while (index > 0)
                    {
                        int FileId = await _attachmentRepository.NewFileId(null, "/Images/Ad");
                        AdvertisingAttachment advImage = new AdvertisingAttachment();

                        advImage.AdvertisingId = advertise.Id;
                        advImage.AttachmentId = FileId;
                        _unitOfWork.AdvertisingAttachments.Insert(advImage);
                        try
                        {
                            await _unitOfWork.CompleteAsync();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                            throw new Exception("خطا در ذخیره اطلاعات");
                        }
                        index--;
                    }
                    ////////////////////////پایان عملیات افزودن فایل ها به جدول واسط//////////////
                    ////////////////////////عملیات افزودن دسته‌بندی‌ها به جدول واسط////////////////
                    List<AdCategoryAdvertising> adCat = new List<AdCategoryAdvertising>();
                   adCat.Add(new AdCategoryAdvertising { AdvertisingId = advertise.Id, AdCategoryId = command.AdCategoryId });
                    _unitOfWork.AdCategoryAdvertisings.InsertRange(adCat);
                    try
                    {
                        await _unitOfWork.CompleteAsync();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception("خطا در ذخیره اطلاعات");
                    }
                    ////////////////////////پایان عملیات افزودن دسته‌بندی‌ها به جدول واسط///////////
                    dbContextTransaction.Commit();
                    return $"تبریک! آیدی {advertise.Id}";
                }

            }
        }
        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }



}
