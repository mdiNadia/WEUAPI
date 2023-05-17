using Domain.Common;

namespace Domain.Entities
{
    public class ProfileReport : BaseEntity
    {

        public int ObserverId { get; set; }
        public Profile Observer { get; set; }
        public int TargetId { get; set; }
        public Profile Target { get; set; }



        public DateTime ReportDate { get; set; }

        public DateTime UpdateDate { get; set; }
        //تعداد ریپورت های مشابه از سمت یک کاربر
        public int Count { get; set; } = 1;
        public string? Description { get; set; }
        public int ReasonId { get; set; }
        public ReportReason Reason { get; set; }
        public ICollection<ProfileBlock> ProfileBlocks { get; set; }

    }
}
