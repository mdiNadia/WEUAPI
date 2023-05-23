using Application.Dtos.Common;

namespace Application.Features.AdCategoryCost.Queries
{
    public class GetAdCatCostDto
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public GetNameAndId AdCategory { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
