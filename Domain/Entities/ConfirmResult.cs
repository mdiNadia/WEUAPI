using Domain.Common;

namespace Domain.Entities
{
    public class ConfirmResult : BaseEntity
    {
        public int AdId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string ShortKey { get; set; }
        public int QrCode { get; set; }
        public DateTime ConfirmedDate { get; set; }
        public int AdvertiserId { get; set; }
        public string Categories { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ConfirmedResultAttachment> ConfirmedResultAttachments { get; set; }
        public ICollection<SavedAd> ProfileSavedAdvertisings { get; set; }
        public ICollection<AdReport> AdReporteds { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Favorite> favorites { get; set; }
        public ICollection<View> Views { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public int BoostId { get; set; }
        public bool CommentStatus { get; set; }
        public string AdCountries { get; set; }
        public string AdProvinces { get; set; }
        public string AdCities { get; set; }
        public string AdNeighborhoods { get; set; }
    }

}
