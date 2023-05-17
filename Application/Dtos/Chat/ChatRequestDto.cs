namespace ChatRequestDto
{
    public class MessageDto
    {
        public string user { get; set; }
        public string msgText { get; set; }
        //public class OneByOneMessageRequestDto
        //{
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
        public string Message { get; set; }
        //}

    }
}