namespace Domain.Entities
{
    public class ProfileBlock
    {

        public int ObserverId { get; set; }
        public Profile Observer { get; set; }
        public int TargetId { get; set; }
        public Profile Target { get; set; }

        public DateTime BlockedDate { get; set; }


        public int? ProfileReportId { get; set; }
        public ProfileReport ProfileReport { get; set; }


    }
}
