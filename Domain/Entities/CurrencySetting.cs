using Domain.Common;

namespace Domain.Entities
{
    public class CurrencySetting : BaseEntity
    {
        //نرخ خرید
        public decimal Buy { get; set; }
        //نرخ فروش
        public decimal Sale { get; set; }

 
        public DateTime UpdatedDate { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

    }
}
