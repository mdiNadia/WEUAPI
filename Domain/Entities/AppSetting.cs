using Domain.Common;

namespace Domain.Entities
{
    public class AppSetting : BaseEntity
    {
        public decimal MinBoostAmount { get; set; }
        public int MinValuePerVisit { get; set; }
        public int MinView { get; set; }
        public int AppFee { get; set; }
        //WEU Value
        public decimal Value { get; set; }
    

    }
}
