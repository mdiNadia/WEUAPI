namespace WEUPanel.Pages.AdvertiseCategory
{
    public class AdvertiseCategoryModels
    {
        public class AdvertiseCategory
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int? CostId { get; set; }
            public bool IsActiveCost { get; set; }
            public int ParentId { get; set; }
            public string ParentName { get; set; }
            public DateTime CreationDate { get; set; }
            //public IList<GetNameAndId> Children { get; set; }
        }

        public class CreateAdvertiseCategory
        {
            public string Name { get; set; }
            public string Description { get; set; }

            public int ParentId { get; set; }
        }

        public class EditAdvertiseCategory
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int ParentId { get; set; }

        }

        public class GetCatNameDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Cost { get; set; }
        }
    }
}
