using Domain.Common;

namespace Domain.Entities
{
    public class AdvertisingAttachment : BaseCreatioDate
    {
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }


        public int AdvertisingId { get; set; }
        public Advertising Advertising { get; set; }


    }
}
