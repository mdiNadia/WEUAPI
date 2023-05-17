using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Boost : BaseEntity
    {
        public int NumberOfadViews { get; set; }
        public int ValuePerVisit { get; set; }
        public decimal Debit { get; set; }
        public BoostStatus Status { get; set; }
        public int AdvertisingId { get; set; }
        public Advertising Advertising { get; set; }


    }
}
