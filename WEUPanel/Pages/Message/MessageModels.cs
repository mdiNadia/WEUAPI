namespace WEUPanel.Pages.Message
{
    public class MessageModels
    {
        public class Message
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public string SenderUsername { get; set; }
            public int RecipientId { get; set; }
            public string RecipientUsername { get; set; }

            public string Content { get; set; }
            public DateTime? DateRead { get; set; }
            public DateTime MessageSent { get; set; }
            public bool SenderDeleted { get; set; }
            public bool RecipientDeleted { get; set; }
            public DateTime CreationDate { get; set; }
        }

        public class MessageParams
        {
            public string Username { get; set; }
            public string Container { get; set; } = "Unread";
        }

    }
}
