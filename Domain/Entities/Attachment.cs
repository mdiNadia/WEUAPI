using Domain.Common;

namespace Domain.Entities
{
    public class Attachment : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //اسم فیلم یا عکس که در دیتابیس ذخیره میشود
        public string FileName { get; set; }
        public int? FileTypeId { get; set; }
        public FileType FileType { get; set; }


        //Relations
        public ICollection<AdvertisingAttachment> AdvertisingAttachments { get; set; }
        public ICollection<ConfirmedResultAttachment> ConfirmedResultAttachments { get; set; }

        public ICollection<Profile> Profiles { get; set; }
        public ICollection<ProfileScore> ProfileScores { get; set; }

        public ICollection<Language> Languages { get; set; }
    }
}
