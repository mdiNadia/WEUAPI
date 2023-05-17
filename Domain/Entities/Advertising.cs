using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Advertising : BaseEntity
    {

        public string Name { get; set; }
        public AdStatus AdStatus { get; set; }
        //توضیحات کوتاه آگهی
        public string Description { get; set; }
        //متن آگهی
        public string Text { get; set; }
        //تاریخ درج آگهی

        public int QrCode { get; set; }

        //تاریخ شروع اعتبار آگهی
        public DateTime? StartDate { get; set; }
        //تاریخ انقضای آگهی یعنی اتمام اعتبار آگهی
        public DateTime? ExpireDate { get; set; }
        public string ShortKey { get; set; }
        //Relations//
        //public ICollection<Comment> Comments { get; set; }
        //کسی که آگهی میذاره و حتما پروفایل دارد
        public int AdvertiserId { get; set; }
        public Profile Advertiser { get; set; }

        //جدول های واسط
        public ICollection<AdCategoryAdvertising> AdCategoryAdvertisings { get; set; }
        public ICollection<AdvertisingAttachment> AdvertisingAttachments { get; set; }

        //public ICollection<SavedAd> ProfileSavedAdvertisings { get; set; }
        //
        //public ICollection<AdReport> AdReporteds { get; set; }
        //public ICollection<Like> Likes { get; set; }
        //public ICollection<View> Views { get; set; }

        public int? BoostId { get; set; }
        public Boost Boost { get; set; }

        public ICollection<AdCountry> AdCountries { get; set; }
        public ICollection<AdProvince> AdProvinces { get; set; }
        public ICollection<AdCity> AdCities { get; set; }
        public ICollection<AdNeighborhood> AdNeighborhoods { get; set; }
    }
}
