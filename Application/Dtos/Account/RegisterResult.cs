using System.Net;

namespace Application.Dtos.Account
{
    public class RegisterResult
    {
        public HttpStatusCode statusCode { get; set; }
        public string error { get; set; }
        public string Token { get;set; }
    }
}
