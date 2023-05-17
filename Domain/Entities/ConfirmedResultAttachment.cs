using Domain.Common;

namespace Domain.Entities
{
    public class ConfirmedResultAttachment : BaseCreatioDate
    {
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }


        public int ConfirmResultId { get; set; }
        public ConfirmResult ConfirmResult { get; set; }

   

    }
}
