using WEUPanel.Wrappers;

namespace WEUPanel.Pages.AdCategoryCost
{
    public class AdCategoryCostModels
    {
        public class AdcategoryCost
        {
            public int Id { get; set; }
            public decimal Cost { get; set; }
            public GetNameAndId AdCategory { get; set; }

            public DateTime CreationDate { get; set; }
            public DateTime UpdatedDate { get; set; }
        }
        public class CreateAdcategoryCost
        {
            public decimal Cost { get; set; }
            public int AdCategoryId { get; set; }
        }
        public class EditAdcategoryCost
        {
            public int Id { get; set; }
            public decimal Cost { get; set; }

        }
    }
}
