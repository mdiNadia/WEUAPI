using Domain.Common;

namespace Domain.Entities
{
    public class AdNeighborhood : BaseEntity
    {
        public int NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }


        public int AdvertisingId { get; set; }
        public Advertising Advertising { get; set; }
    }
}
