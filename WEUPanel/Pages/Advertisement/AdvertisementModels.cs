using Microsoft.AspNetCore.Http;
using WEUPanel.Wrappers;

namespace WEUPanel.Pages.Advertisement
{
    public class AdvertisementModels
    {
        public class Advertisement
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Like { get; set; } = 0;
            public int View { get; set; } = 0;
            public AdStatus AdStatus { get; set; }
            public string Description { get; set; }
            public string Text { get; set; }
            public string CreationDate { get; set; }
            public string? StartDate { get; set; }

            public string? ExpireDate { get; set; }
            public int AdvertiserId { get; set; }
            public List<GetNameAndId> Categories { get; set; }
            public List<GetFileWithType> Files { get; set; }

        }
        public class CreateAdvertisement
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int AdFileTypeEnum { get; set; }
            public string Text { get; set; }
            public string? StartDate { get; set; }
            public string? ExpireDate { get; set; }
            //public string Advertiser { get; set; }
            //public List<int> AdCategoryIds { get; set; }
            public int AdCategoryIds { get; set; }
            public List<IFormFile> AdvertisingFiles { get; set; }

            //public List<int> AdCountryIds { get; set; }
            public int AdCountryId { get; set; }
            public List<int>? AdProvinceIds { get; set; }
            public List<int>? AdCityIds { get; set; }
            public List<int>? AdNeighborhoodIds { get; set; }
            public int NumberOfadViews { get; set; }
            public decimal PricePerVisit { get; set; }
            //public decimal Debit { get; set; }
            public int Status { get; set; }
        }
        public class EditAdvertisement
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int AdFileTypeEnum { get; set; }
            public string Text { get; set; }
            public string? StartDate { get; set; }
            public string? ExpireDate { get; set; }
            public int AdvertiserId { get; set; }
            public List<GetNameAndId> Files { get; set; } = new List<GetNameAndId>();
            public List<int> AdCategoryIds { get; set; }
            public List<RequestUpdateFilesInAdvertisingDto> AdvertisingFiles { get; set; }
        }
        public class RequestUpdateFilesInAdvertisingDto
        {
            public int AttachmentId { get; set; }
            public bool IsChanged { get; set; }
            public WEUPanel.Wrappers.FileType FileType { get; set; }
            public IFormFile UpdatedFile { get; set; }
        }

        public class RequestFiles
        {
            public int AttachmentId { get; set; }
            public bool IsChanged { get; set; }
            public WEUPanel.Wrappers.FileType FileType { get; set; }
            public GetFileModel GetFileModel { get; set; }
        }

        public class UpdateDisplayedField
        {
            public int Id { get; set; }
            public bool Displayed { get; set; }

        }
        public class UpdateIsActiveField
        {
            public int Id { get; set; }
            public bool IsActive { get; set; }

        }


        public class RequestBoostDto
        {
            public int NumberOfadViews { get; set; }
            public decimal PricePerVisit { get; set; }
            
        }



    }

}
