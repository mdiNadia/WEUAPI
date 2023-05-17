using Domain.Common;

namespace Domain.Entities
{
    public class AdProvince : BaseEntity
    {
        public int ProvinceId { get; set; }
        public Province Province { get; set; }


        public int AdvertisingId { get; set; }
        public Advertising Advertising { get; set; }
    }
}
