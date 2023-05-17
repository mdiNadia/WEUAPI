using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class ReportReason : BaseEntity
    {
        public ReportReasonType ReportReasonType { get; set; }
        public string Reason { get; set; }
        public int? ParentId { get; set; }
        public ReportReason Parent { get; set; }


        public ICollection<ReportReason> Children { get; set; }

        //رابطه ها
        public ICollection<ProfileReport> ProfileReports { get; set; }
        public ICollection<AdReport> AdReports { get; set; }
    }
}
