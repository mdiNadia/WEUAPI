namespace Application.Dtos.Account
{
    public class ResponseForgetPasswordModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public string? StatusCode { get; set; }
        public string? Data { get; set; }
    }
}
