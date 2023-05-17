using WEUPanel.Wrappers;

namespace WEUPanel.Pages.City
{
    public class CityModels
    {
        public class City
        {
            public int Id { get; init; }
            public string Name { get; init; }
            public decimal Longitude { get; init; }
            public decimal Latitude { get; init; }
            public GetNameAndId Province { get; init; }
            public bool IsActive { get; set; }
            public DateTime CreationDate { get; set; }

        }
        public class CreateCity
        {
            public string Name { get; set; }
            public decimal Longitude { get; set; }
            public decimal Latitude { get; set; }
            public int ProvinceId { get; set; }
            public bool IsActive { get; set; }

        }
        public class EditCity
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Longitude { get; set; }
            public decimal Latitude { get; set; }
            public int ProvinceId { get; set; }
            public bool IsActive { get; set; }

        }
    }
}
