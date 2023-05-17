using System.Globalization;

namespace WEUPanel.Helpers
{

    public class BaseRequestParameter
    {

        public BaseRequestParameter()
        {
            _Base_Images_Url = "http://192.168.100.100/UploadedFiles/Images";
            _Base_Videos_Url = "http://192.168.100.100/UploadedFiles/Videos";
            _Base_Route_Url = "http://192.168.100.100";
            _Root_Url = _Base_Route_Url + "/api/v1";
            var culture = CultureInfo.CurrentCulture;
            _Culture_Url = _Root_Url + culture.DisplayName;
        }
        public string _Base_Route_Url { get; set; }
        public string _Base_Images_Url { get; set; }
        public string _Base_Videos_Url { get; set; }
        public string _Root_Url { get; set; }
        public string _Culture_Url { get; set; }
    }
}

