using Domain.Common;

namespace Domain.Entities
{
    public class AdCity : BaseEntity
    {
        public int CityId { get; set; }
        public City City { get; set; }


        public int AdvertisingId { get; set; }
        public Advertising Advertising { get; set; }
    }
}
