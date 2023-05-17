using WEUPanel.Wrappers;

namespace WEUPanel.Pages.ReportAdsReason
{
    public class ReportReasonModels
    {
        public class ReportReason
        {
            public int Id { get; set; }
            public int ReportReasonType { get; set; }
            public GetNameAndId Parent { get; set; }
            public string Reason { get; set; }
            public DateTime CreationDate { get; set; }
        }


        public class CreateReportReason
        {
            public string Reason { get; set; }
            public int ParentId { get; set; }
            public int ReasonType { get; set; }
        }

        public class EditReportReason
        {
            public int Id { get; set; }
            public string Reason { get; set; }
            public int ParentId { get; set; }
        }
    }
}
