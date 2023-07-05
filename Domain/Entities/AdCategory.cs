using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class AdCategory : BaseEntity
    {
        [Required(ErrorMessage = "نام لازم است")]
        [Display(Name = "نام")]

        public string Name { get; set; }

        [Required(ErrorMessage = "توضیحات لازم است")]
        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        public int? ParentId { get; set; }
        public AdCategory? Parent { get; set; }

        public ICollection<AdCategory> Children { get; set; }

        public ICollection<AdCategoryAdvertising> AdCategoryAdvertisings { get; set; }

        public int? CategoryCostId { get; set; }
        public AdCategoryCost CategoryCost { get; set; }


    }
}
