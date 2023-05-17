using Domain.Common;

namespace Domain.Entities
{
    public class AdReport : BaseCreatioDate
    {
        public int ObserverId { get; set; }
        public Profile Observer { get; set; }
        public int TargetId { get; set; }
        public ConfirmResult Target { get; set; }
        public string? Description { get; set; }

        

        //
        public int ReasonId { get; set; }
        public ReportReason Reason { get; set; }
    }
}
