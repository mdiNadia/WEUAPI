using WEUPanel.Wrappers;

namespace WEUPanel.Pages.Neighborhood
{
    public class NeighborhoodModels
    {
        public class Neighborhood
        {
            public int Id { get; init; }
            public string Name { get; init; }
            public decimal Longitude { get; init; }
            public decimal Latitude { get; init; }
            public GetNameAndId City { get; init; }
            public bool IsActive { get; set; }
            public DateTime CreationDate { get; set; }

        }
        public class CreateNeighborhood
        {
            public string Name { get; set; }
            public decimal Longitude { get; set; }
            public decimal Latitude { get; set; }
            public int CityId { get; set; }
            public bool IsActive { get; set; }

        }
        public class EditNeighborhood
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Longitude { get; set; }
            public decimal Latitude { get; set; }
            public int CityId { get; set; }
            public bool IsActive { get; set; }

        }
    }
}
