using Domain.Common;

namespace Domain.Entities
{
    public class RejectedResultAttachment : BaseCreatioDate
    {
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }


        public int RejectResultId { get; set; }
        public RejectResult RejectResult { get; set; }


    }
}
