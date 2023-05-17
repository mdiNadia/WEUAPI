namespace WEUPanel.Pages.ReportsAds
{
    public class ReportedModels
    {
        public class Reported
        {
            public string Blocker { get; set; }
            public string Blocked { get; set; }
            public string Reason { get; set; }
            public string Desciption { get; set; }
            public DateTime CreationDate { get; set; }
        }
        public class BlackList
        {
            public string Blocker { get; set; }
            public string Blocked { get; set; }
            public DateTime CreationDate { get; set; }

        }
    }
}
