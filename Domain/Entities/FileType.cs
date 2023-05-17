using Domain.Common;

namespace Domain.Entities
{
    public class FileType : BaseEntity
    {
        public string Name { get; set; }//a name
        public Domain.Enums.FileType Type { get; set; }//for example video or image
        public string Extension { get; set; }//for example .exe or .jpg
        public long Size { get; set; }//for example 2mb or 25kb
        


        public ICollection<Attachment> Attachments { get; set; }
    }
}
