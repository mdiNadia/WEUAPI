using Domain.Common;

namespace Domain.Entities
{
    public class ProfileSetting : BaseEntity
    {
        public string Language { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }
        public int ProfileId { get; set; }
    }
}
