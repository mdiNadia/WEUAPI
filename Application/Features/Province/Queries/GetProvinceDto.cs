using Application.Dtos.Common;

namespace Application.Features.Province.Queries
{
    public record GetProvinceDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public decimal Longitude { get; init; }
        public decimal Latitude { get; init; }
        public GetNameAndId Country { get; init; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; init; }

    }
}
