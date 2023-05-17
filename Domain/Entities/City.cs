using Domain.Common;

namespace Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
        public bool IsActive { get; set; }


        public ICollection<Neighborhood> Neighborhoods { get; set; }

        //طول جغرافیایی
        public decimal Longitude { get; set; }
        //عرض جغرافیایی
        public decimal Latitude { get; set; }

        public ICollection<AdCity> AdCities { get; set; }

    }
}
