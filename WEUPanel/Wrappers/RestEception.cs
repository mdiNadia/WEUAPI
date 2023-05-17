using System.Net;

namespace WEUPanel.Wrappers
{
    //public class RestEception:Exception
    //{
    //    public HttpStatusCode Code { get; }

    //    public string Error { get; }
    //}
    public class RestEception
    {
        public string type { get; set; }
        public string title { get; set; }
        public HttpStatusCode status { get; set; }
        public string traceId { get; set; }
        public string error { get; set; }
        public Dictionary<string, string[]> errors { get; set; }
    }


}
