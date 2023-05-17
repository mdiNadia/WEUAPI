using WEUPanel.Wrappers;

namespace WEUPanel.Pages.Province
{
    public class ProvinceModels
    {
        public class Province
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Longitude { get; set; }
            public decimal Latitude { get; set; }
            public bool IsActive { get; set; }

            public GetNameAndId Country { get; set; }
            public DateTime CreationDate
            {
                get; set;
            }
        }
        public class CreateProvince
        {
            public string Name { get; set; }
            public decimal Longitude { get; set; }
            public decimal Latitude { get; set; }
            public int CountryId { get; set; }
            public bool IsActive { get; set; }

        }
        public class EditProvince
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Longitude { get; set; }
            public decimal Latitude { get; set; }
            public int CountryId { get; set; }
            public bool IsActive { get; set; }

        }
    }
}
