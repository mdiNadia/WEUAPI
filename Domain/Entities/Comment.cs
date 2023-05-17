using Domain.Common;

namespace Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Message { get; set; }


        public bool IsVisited { get; set; }
        public bool IsActive { get; set; }
        public int? ParentId { get; set; }
        public Comment Parent { get; set; }
        public ICollection<Comment> Children { get; set; }
        public int ConfirmResultId { get; set; }
        public ConfirmResult ConfirmResult { get; set; }
        public ICollection<LikeComment> LikeComments { get; set; }
        public int? AuthorId { get; set; }
        public Profile Author { get; set; }
    }
}
