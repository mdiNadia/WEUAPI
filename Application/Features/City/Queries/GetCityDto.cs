using Application.Dtos.Common;

namespace Application.Features.City.Queries
{
    public record GetCityDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public decimal Longitude { get; init; }
        public decimal Latitude { get; init; }
        public GetNameAndId Province { get; init; }
        public bool IsActive { get; init; }
        public DateTime CreationDate { get; init; }

    }
}
