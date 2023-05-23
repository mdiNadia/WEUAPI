namespace Application.Features.FileType.Queries
{
    public record GetFileTypeDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public long Size { get; init; }
        public Domain.Enums.FileType Type { get; init; }
        public string Extension { get; init; }
        public DateTime CreationDate { get; init; }

    }
}
