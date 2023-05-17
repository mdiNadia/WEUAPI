using Domain.Common;

namespace Domain.Entities
{
    public class Neighborhood : BaseEntity
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public bool IsActive { get; set; }



        //طول جغرافیایی
        public decimal Longitude { get; set; }
        //عرض جغرافیایی
        public decimal Latitude { get; set; }

        public ICollection<AdNeighborhood> AdNeighborhoods { get; set; }

    }
}
