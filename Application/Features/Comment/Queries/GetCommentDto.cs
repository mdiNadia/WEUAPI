using Application.Dtos.Common;

namespace Application.Features.Comment.Queries
{
    public record GetCommentDto
    {

        public int Id { get; init; }
        public string Username { get; init; }
        public string Message { get; init; }
        public bool IsVisited { get; init; }
        public bool IsActive { get; init; }
        public int? ParentId { get; init; }
        public string? Photo { get; init; }
        public GetNameAndId Advertising { get; init; }
        public string CreationDate { get; set; }
        public IList<GetCommentDto>? Children { get; init; }

    }
}
