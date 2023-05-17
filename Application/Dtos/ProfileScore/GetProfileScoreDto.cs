namespace Application.Dtos.Language
{
    public record GetProfileScoreDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Score { get; init; }
        public int ProfileType { get; init; }
        public int? IconId { get; init; }
        public string? IconName { get; init; }
        public DateTime CreationDate { get; init; }
    }
}
