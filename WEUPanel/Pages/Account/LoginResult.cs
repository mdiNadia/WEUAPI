using System.Net;
using System.Text.Json.Serialization;

namespace WEUPanel.Pages.Account
{
    public class LoginResult
    {
        public string userName { get; set; }
        public string[] roles { get; set; }
        public string token { get; set; }
        public HttpStatusCode statusCode { get; set; }
        public string error { get; set; }
        public string refreshTokenExpiration { get; set; }
    }
    public class AuthenticationModel
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        //public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
