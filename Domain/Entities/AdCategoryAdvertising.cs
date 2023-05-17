using Domain.Common;

namespace Domain.Entities
{
    public class AdCategoryAdvertising : BaseEntity
    {

        public int AdCategoryId { get; set; }
        public AdCategory AdCategory { get; set; }

        public int AdvertisingId { get; set; }
        public Advertising Advertising { get; set; }


    }
}
