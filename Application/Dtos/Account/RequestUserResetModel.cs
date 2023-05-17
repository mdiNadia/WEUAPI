namespace Application.Dtos.Account
{
    public class RequestUserResetModel
    {
        public string token { get; set; }
        public string username { get; set; }
        public string email { get; set; }
    }
}
