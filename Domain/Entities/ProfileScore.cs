using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class ProfileScore : BaseEntity
    {
        public ProfileType ProfileType { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int? IconId { get; set; }
        public Attachment Icon { get; set; }
 


    }
}
