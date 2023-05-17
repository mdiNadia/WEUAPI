using System.Net;

namespace Application.Errors
{
    public class RestException : Exception
    {
        public RestException(HttpStatusCode code, string errors = null)
        {
            Code = code;
            Error = errors;
        }

        public HttpStatusCode Code { get; }
        public string Error { get; }
    }
}
