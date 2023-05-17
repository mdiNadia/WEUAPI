using Domain.Common;

namespace Domain.Entities
{
    public class Currency : BaseEntity
    {
        //symbol of currency (USD,EUR)
        public string CurrencyName { get; set; }
        public bool IsDefault { get; set; }
        //نرخ پیش فرض برای سکه هست؟ مثلا هر سکه 1 دلار است؟
        public bool IsDefaultPayRate { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Country> Countries { get; set; }
        public ICollection<CurrencySetting> CurrencySettings { get; set; }
        public ICollection<Wallet> Wallets { get; set; }
    }
}
