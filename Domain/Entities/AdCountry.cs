using Domain.Common;

namespace Domain.Entities
{
    public class AdCountry : BaseEntity
    {
        public int CountryId { get; set; }
        public Country Country { get; set; }



        public int AdvertisingId { get; set; }
        public Advertising Advertising { get; set; }
    }
}
