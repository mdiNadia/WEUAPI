using Domain.Common;

namespace Domain.Entities
{
    public class Province : BaseEntity
    {

        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public bool IsActive { get; set; }
        public ICollection<City> Cities { get; set; }
   


        //طول جغرافیایی
        public decimal Longitude { get; set; }
        //عرض جغرافیایی
        public decimal Latitude { get; set; }

        public ICollection<AdProvince> AdProvinces { get; set; }

    }
}
