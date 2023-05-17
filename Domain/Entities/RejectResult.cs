using Domain.Common;

namespace Domain.Entities
{
    public class RejectResult : BaseEntity
    {
        public string Reason { get; set; }
        public DateTime RejectDate { get; set; }
        public int AdId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string ShortKey { get; set; }
        public int QrCode { get; set; }
        public int AdvertiserId { get; set; }
        public string Categories { get; set; }
        public ICollection<RejectedResultAttachment> RejectedAdAttachment { get; set; }
        public int BoostId { get; set; }
        public string AdCountries { get; set; }
        public string AdProvinces { get; set; }
        public string AdCities { get; set; }
        public string AdNeighborhoods { get; set; }
    }
}
