using Domain.Common;

namespace Domain.Entities
{
    public class AdCategoryCost : BaseEntity
    {
        public decimal Cost { get; set; }
        public int AdCategoryId { get; set; }
        public AdCategory AdCategory { get; set; }
        public bool IsActive { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}
