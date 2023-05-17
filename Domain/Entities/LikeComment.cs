using Domain.Common;

namespace Domain.Entities
{
    public class LikeComment: BaseCreatioDate
    {
        public int ObserverId { get; set; }
        public Profile Observer { get; set; }

        public int TargetId { get; set; }
        public Comment Target { get; set; }

    }
}
