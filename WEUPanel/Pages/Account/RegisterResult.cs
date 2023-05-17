using System.Net;

namespace WEUPanel.Pages.Account
{
    public class RegisterResult
    {
        public HttpStatusCode statusCode { get; set; }
        public string error { get; set; }
    }
}
