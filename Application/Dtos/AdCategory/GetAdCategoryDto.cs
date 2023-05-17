namespace Application.Dtos.AdCategory
{
    public record GetAdCategoryDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int? CostId { get; init; }
        public bool IsActiveCost { get; init; }
        public GetAdCategoryDto? Parent { get; init; }
        public int? ParentId { get; init; }
        public string ParentName { get; init; }

        public DateTime CreationDate { get; init; }
        public IList<GetAdCategoryDto> Children { get; init; }

    }

}
