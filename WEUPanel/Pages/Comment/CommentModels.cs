using WEUPanel.Wrappers;

namespace WEUPanel.Pages.Comment
{
    public class CommentModels
    {
        public class Comment
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Message { get; set; }
            public bool IsVisited { get; set; }
            public bool IsActive { get; set; }
            public int? ParentId { get; set; }
            public DateTime CreationDate { get; set; }
            public GetNameAndId Advertising { get; set; }
        }
        public class CreateComment
        {
            public string UserName { get; set; }
            public string Message { get; set; }
            public bool IsVisited { get; set; }
            public bool IsActive { get; set; }
            public int AdvertisingId { get; set; }
            public int? ParentId { get; set; }
        }
        public class EditComment
        {
            public int Id { get; set; }
            public bool IsActive { get; set; }
            public bool IsVisite { get; set; } = false;
        }
    }
}
