using Domain.Common;

namespace Domain.Entities
{
    public class UserFollowing: BaseCreatioDate
    {
        public int ObserverId { get; set; }
        public Profile Observer { get; set; }
        public int TargetId { get; set; }
        public Profile Target { get; set; }

    }
}
