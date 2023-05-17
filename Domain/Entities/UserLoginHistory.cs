using Domain.Common;

namespace Domain.Entities
{
    public class UserLoginHistory : BaseEntity
    {
        public string UserName { get; set; }
        public DateTime LoginDate { get; set; }
    }
}
