using Domain.Common;

namespace Domain.Entities
{
    public class Country : BaseEntity
    {
        public string Iso { get; set; }
        public string Name { get; set; }
        public string? Iso3 { get; set; }
        public int? NumCode { get; set; }
        public int? PhoneCode { get; set; }


        public int? CurrencyId { get; set; }
        public Currency Currency { get; set; }

        //طول جغرافیایی
        public int? Longitude { get; set; }
        //عرض جغرافیایی
        public int? Latitude { get; set; }
        ///////////////////////////////
        public bool IsActive { get; set; }
        public ICollection<Province> Provinces { get; set; }

        public ICollection<AdCountry> AdCountries { get; set; }

    }
}
