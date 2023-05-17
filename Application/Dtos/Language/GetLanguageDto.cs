namespace Application.Dtos.Language
{
    public record GetLanguageDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string ShortName { get; init; }
        public int Direction { get; init; }
        public int? IconId { get; init; }
        public string? IconName { get; init; }
        public DateTime CreationDate { get; init; }
    }
}
