using Application.Dtos.Common;

namespace Application.Features.Neighbourhood.Queries
{
    public record GetNeighbourhoodDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public decimal Longitude { get; init; }
        public decimal Latitude { get; init; }
        public GetNameAndId City { get; init; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; init; }

    }
}
