namespace Application.Dtos.Common
{
    public record GetNameAndId
    {
        public string Name { get; init; }
        public int Id { get; init; }
        public DateTime CreationDate { get; init; }
    }

    public record GetNameAndIdString
    {
        public string Name { get; init; }
        public string Id { get; init; }
        public DateTime CreationDate { get; init; }
    }
}
