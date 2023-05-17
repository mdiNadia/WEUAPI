using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Language : BaseEntity
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool IsDefault { get; set; }
        public Direction Direction { get; set; }
        public int? IconId { get; set; }
        public Attachment Icon { get; set; }
   


    }
}
